using Database.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Background
{
    public class BudgetPromotionSyncHostedService : IHostedService
    {
        public IServiceProvider Services { get; }
        private Timer _timer;

        public BudgetPromotionSyncHostedService(IServiceProvider services)
        {
            Services = services;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer((state) =>
            {
                var task = Task.Run(async () =>
                {
                    using (var scope = Services.CreateScope())
                    {
                        var db = scope.ServiceProvider.GetService<DatabaseContext>();
                        var budgetService = scope.ServiceProvider.GetService<IBudgetPromotionService>();
                        using (var tran = db.Database.BeginTransaction())
                        {
                            try
                            {
                                await budgetService.RunWaitingSyncJobAsync();
                                await budgetService.ReadSyncResultFromSAPAsync();
                                await budgetService.CreateRetrySyncJobAsync();
                                tran.Commit();
                            }
                            catch (Exception ex)
                            {
                                tran.Rollback();
                                throw ex;
                            }
                        }
                    }
                });
            }, null, TimeSpan.Zero,
            TimeSpan.FromMinutes(5));

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
