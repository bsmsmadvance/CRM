using System;
using System.Threading;
using System.Threading.Tasks;
using Database.Models;
using models = Database.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Identity.Services;
using Newtonsoft.Json;

namespace Identity.Background
{
    public class UserSyncHostedService : IHostedService
    {
        public IServiceProvider Services { get; }
        private Timer _timer;

        public UserSyncHostedService(IServiceProvider services)
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
                        var userService = scope.ServiceProvider.GetService<IUserSyncService>();
                        models.USR.UserBackgroundJob model = new models.USR.UserBackgroundJob()
                        {
                            Name = "User Sync",
                            Status = BackgroundJobStatus.InProgress
                        };

                        db.UserBackgroundJobs.Add(model);
                        db.SaveChanges();

                        Exception error = null;

                        //using (var tran = db.Database.BeginTransaction())
                        //{
                        //    try
                        //    {

                        //        tran.Commit();
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        tran.Rollback();
                        //        error = ex;
                        //    }
                        //}
                        var result = await userService.SyncUserDataAsync();
                        await userService.SyncRoleOfUserDataAsync();

                        model.Status = BackgroundJobStatus.Completed;
                        model.ResponseMessage = JsonConvert.SerializeObject(result, Formatting.Indented);
                        db.Update(model);
                        db.SaveChanges();

                        if (error != null)
                        {
                            model.Status = BackgroundJobStatus.Failed;
                            model.ResponseMessage = error.ToString();
                            db.Update(model);
                            db.SaveChanges();
                        }
                    }
                });

                if (task.IsFaulted)
                {
                    throw task.Exception;
                }

            }, null, TimeSpan.Zero,
            TimeSpan.FromHours(6));

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
