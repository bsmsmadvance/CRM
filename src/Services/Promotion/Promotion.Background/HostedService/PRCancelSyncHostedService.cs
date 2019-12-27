using Database.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Promotion.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Promotion.Background
{
    public class PRCancelSyncHostedService : IHostedService
    {
        public IServiceProvider Services { get; }
        private Timer _timer;

        public PRCancelSyncHostedService(IServiceProvider services)
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
                        var pRCancelJobService = scope.ServiceProvider.GetService<IPRCancelJobService>();
                        using (var tran = db.Database.BeginTransaction())
                        {
                            try
                            {
                                await pRCancelJobService.RunWaitingSyncJobAsync();
                                await pRCancelJobService.ReadSyncResultFromSAPAsync();
                                tran.Commit();
                            }
                            catch (Exception ex)
                            {
                                tran.Rollback();
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
