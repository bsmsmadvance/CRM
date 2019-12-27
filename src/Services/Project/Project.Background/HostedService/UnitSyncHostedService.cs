using Database.Models;
using Database.Models.PRJ;
using FileStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Project.Params.Outputs;
using Project.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Background
{
    public class UnitSyncHostedService : IHostedService
    {
        public IServiceProvider Services { get; }
        private readonly IConfiguration Configuration;
        private Timer _timer;
        private FileHelper FileHelperSap;

        public UnitSyncHostedService(IConfiguration configuration, IServiceProvider services)
        {
            this.Configuration = configuration;
            Services = services;
            var minioSapEndpoint = Configuration["MinioSAP:Endpoint"];
            var minioSapAccessKey = Configuration["MinioSAP:AccessKey"];
            var minioSapSecretKey = Configuration["MinioSAP:SecretKey"];
            var minioSapWithSSL = Configuration["MinioSAP:WithSSL"];

            this.FileHelperSap = new FileHelper(minioSapEndpoint, minioSapAccessKey, minioSapSecretKey, "wbs", "", minioSapWithSSL == "true");
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
                        var unitService = scope.ServiceProvider.GetService<IUnitService>();

                        var model = new SAPWBSProSyncJob()
                        {
                            Status = BackgroundJobStatus.InProgress
                        };

                        db.SAPWBSProSyncJobs.Add(model);
                        db.SaveChanges();

                        Exception error = null;

                        using (var tran = db.Database.BeginTransaction())
                        {
                            try
                            {
                                var task = Task.Run(async () =>
                                {
                                    var getFileNameFromResults = await this.FileHelperSap.GetListFile("wbs", "result/");
                                    var resutls = new UnitSyncResponse { SAPWBSNoNotFound = new List<string>(), Update = 0 };
                                    foreach (var item in getFileNameFromResults)
                                    {
                                        var fileName = item.Split("/").Last();
                                        if (fileName != "empty.txt")
                                        {
                                            var temp = await FileHelperSap.DownLoadToTempFileAsync("wbs", "result/", fileName);
                                            using (StreamReader streamReader = new StreamReader(temp, Encoding.UTF8))
                                            {
                                                string line;
                                                var content = new List<string>();
                                                while ((line = streamReader.ReadLine()) != null)
                                                {
                                                    content.Add(line);
                                                }
                                                var data = await unitService.ReadSAPWBSPromotionTextFileAsync(content.ToArray());
                                                resutls.SAPWBSNoNotFound.AddRange(data.SAPWBSNoNotFound);
                                                resutls.Update += (data.Update);
                                            }
                                            await this.FileHelperSap.MoveAndRemoveFileAsync("wbs", "result/" + fileName, "wbs", "result_backup/" + fileName);
                                        }
                                    }

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
