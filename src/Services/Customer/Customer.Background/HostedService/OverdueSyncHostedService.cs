using Customer.Params.Outputs;
using Customer.Services.ContactServices;
using Database.Models;
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
    public class OverdueSyncHostedService : IHostedService
    {
        public IServiceProvider Services { get; }
        private Timer _timer;

        public OverdueSyncHostedService(IServiceProvider services)
        {
            Services = services;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer((state) =>
            {
                if (DateTime.Now.Hour == 1 && DateTime.Now.Minute == 0)
                {
                    using (var scope = Services.CreateScope())
                    {
                        var db = scope.ServiceProvider.GetService<DatabaseContext>();
                        var activityService = scope.ServiceProvider.GetService<IActivityService>();

                        models.CTM.ActivityTaskUpdateOverdueJob model = new models.CTM.ActivityTaskUpdateOverdueJob()
                        {
                            Status = BackgroundJobStatus.InProgress
                        };

                        db.ActivityTaskUpdateOverdueJobs.Add(model);
                        db.SaveChanges();

                        Exception error = null;

                        using (var tran = db.Database.BeginTransaction())
                        {
                            try
                            {
                                var task = Task.Run(async () =>
                                {
                                    var resutls = await activityService.UpdateActivityTaskOverdueAsync();

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
