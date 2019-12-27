using Customer.Params.Outputs;
using Customer.Services.LeadSyncService;
using Database.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using models = Database.Models;

namespace Customer.Background
{
    public class LeadSyncHostedService : IHostedService
    {
        public IServiceProvider Services { get; }
        private Timer _timer;

        public LeadSyncHostedService(IServiceProvider services)
        {
            Services = services;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer((state) =>
            {
                if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
                {
                    using (var scope = Services.CreateScope())
                    {
                        var db = scope.ServiceProvider.GetService<DatabaseContext>();
                        var leadService = scope.ServiceProvider.GetService<ILeadSyncService>();

                        models.CTM.LeadSyncJob model = new models.CTM.LeadSyncJob()
                        {
                            Name = "Lead Sync",
                            Status = BackgroundJobStatus.InProgress
                        };

                        db.LeadSyncJobs.Add(model);
                        db.SaveChanges();

                        Exception error = null;

                        using (var tran = db.Database.BeginTransaction())
                        {
                            try
                            {
                                var now = DateTime.Now;
                                DateTime startTime = now.AddDays(-1);
                                DateTime endTime = now;

                                var task = Task.Run(async () =>
                                {
                                    var resutls = new LeadSyncResponse();
                                    var data = await leadService.SyncLeadsFromCRMAfterSale(startTime, endTime);
                                    resutls.Created = data.Created;

                                    return resutls;
                                });

                                if (task.IsFaulted)
                                {
                                    throw task.Exception;
                                }

                                var result = task.Result;
                                model.Status = BackgroundJobStatus.Completed;
                                model.ResponseMessage = JsonConvert.SerializeObject(result, Formatting.Indented);
                                db.Update(model);
                                db.SaveChanges();
                                tran.Commit();
                            }
                            catch (Exception ex)
                            {
                                tran.Rollback();
                                error = ex;
                            }
                        }

                        if (error != null)
                        {
                            model.Status = BackgroundJobStatus.Failed;
                            model.ResponseMessage = error.ToString();
                            db.Update(model);
                            db.SaveChanges();
                        }
                    }
                }
            }, null, TimeSpan.Zero,
            TimeSpan.FromMinutes(1));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
