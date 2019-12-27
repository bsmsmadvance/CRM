using Database.Models;
using Database.Models.MST;
using Database.Models.PRM;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Promotion.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Promotion.Background
{
    public class PromotionMaterialSyncHostedService : IHostedService
    {
        public IServiceProvider Services { get; }
        private readonly IConfiguration Configuration;
        private Timer _timer;
        private FileHelper FileHelperSap;

        public PromotionMaterialSyncHostedService(IConfiguration configuration, IServiceProvider services)
        {
            this.Configuration = configuration;
            Services = services;

            var minioSapEndpoint = Configuration["MinioSAP:Endpoint"];
            var minioSapAccessKey = Configuration["MinioSAP:AccessKey"];
            var minioSapSecretKey = Configuration["MinioSAP:SecretKey"];
            var minioSapWithSSL = Configuration["MinioSAP:WithSSL"];

            this.FileHelperSap = new FileHelper(minioSapEndpoint, minioSapAccessKey, minioSapSecretKey, "mmt", "", minioSapWithSSL == "true");
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
                        var proService = scope.ServiceProvider.GetService<IPromotionMaterialService>();
                        var model = new SAPMaterialSyncJob()
                        {
                            Status = BackgroundJobStatus.InProgress
                        };
                        string year = Convert.ToString(DateTime.Today.Year);
                        string month = DateTime.Today.Month.ToString("00");
                        string day = DateTime.Today.Day.ToString("00");
                        var key = year + month + day;
                        var type = "PRM.SAPMaterialSyncJob";
                        var runningno = db.RunningNumberCounters.Where(o => o.Key == key && o.Type == type).FirstOrDefault();
                        if (runningno == null)
                        {
                            var runningNumberCounter = new RunningNumberCounter
                            {
                                Key = key,
                                Type = type,
                                Count = 1
                            };
                            db.RunningNumberCounters.Add(runningNumberCounter);
                            db.SaveChanges();

                            model.JobNo = key + runningNumberCounter.Count.ToString("0000");
                            runningNumberCounter.Count++;
                            db.Entry(runningNumberCounter).State = EntityState.Modified;
                        }
                        else
                        {
                            model.JobNo = key + runningno.Count.ToString("0000");
                            runningno.Count++;
                            db.Entry(runningno).State = EntityState.Modified;
                        }

                        db.SAPMaterialSyncJobs.Add(model);
                        db.SaveChanges();

                        Exception error = null;

                        using (var tran = db.Database.BeginTransaction())
                        {
                            try
                            {
                                var task = Task.Run(async () =>
                                {

                                    var getFileNameFromResults = await this.FileHelperSap.GetListFile("mmt", "result/");
                                    foreach (var item in getFileNameFromResults)
                                    {
                                        var fileName = item.Split("/").Last();
                                        if (fileName != "empty.txt")
                                        {
                                            var temp = await FileHelperSap.DownLoadToTempFileAsync("mmt", "result/", fileName);
                                            using (StreamReader streamReader = new StreamReader(temp, Encoding.UTF8))
                                            {
                                                string line;
                                                var content = new List<string>();
                                                while ((line = streamReader.ReadLine()) != null)
                                                {
                                                    content.Add(line);
                                                }
                                                await proService.ReadMaterialMasterFromSAPAsync(content.ToArray());
                                            }
                                            await this.FileHelperSap.MoveAndRemoveFileAsync("mmt", "result/" + fileName, "mmt", "result_backup/" + fileName);
                                        }
                                    }
                                    var getFileNamePromotionMaterialItems = await this.FileHelperSap.GetListFile("agm", "result/");
                                    foreach (var item in getFileNamePromotionMaterialItems)
                                    {
                                        var fileName = item.Split("/").Last();
                                        if (fileName != "empty.txt")
                                        {
                                            var temp = await FileHelperSap.DownLoadToTempFileAsync("agm", "result/", fileName);
                                            using (StreamReader streamReader = new StreamReader(temp, Encoding.UTF8))
                                            {
                                                string line;
                                                var content = new List<string>();
                                                while ((line = streamReader.ReadLine()) != null)
                                                {
                                                    content.Add(line);
                                                }
                                                await proService.ReadMaterialAgreementFromSAPAsync(content.ToArray());
                                            }
                                            await this.FileHelperSap.MoveAndRemoveFileAsync("agm", "result/" + fileName, "agm", "result_backup/" + fileName);
                                        }
                                    }
                                });

                                if (task.IsFaulted)
                                {
                                    throw task.Exception;
                                }

                                model.Status = BackgroundJobStatus.Completed;
                                db.SAPMaterialSyncJobs.Update(model);
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
                            db.SAPMaterialSyncJobs.Update(model);
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
