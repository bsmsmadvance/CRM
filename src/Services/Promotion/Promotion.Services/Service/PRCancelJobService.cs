using Database.Models;
using Database.Models.PRM;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion.Services
{
    public class PRCancelJobService : IPRCancelJobService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelperSap;

        public PRCancelJobService(IConfiguration configuration, DatabaseContext db)
        {
            this.Configuration = configuration;
            this.DB = db;

            var minioSapEndpoint = Configuration["MinioSAP:Endpoint"];
            var minioSapAccessKey = Configuration["MinioSAP:AccessKey"];
            var minioSapSecretKey = Configuration["MinioSAP:SecretKey"];
            var minioSapWithSSL = Configuration["MinioSAP:WithSSL"];

            this.FileHelperSap = new FileHelper(minioSapEndpoint, minioSapAccessKey, minioSapSecretKey, "prd", "", minioSapWithSSL == "true");
        }

        public async Task CreateNewPreSalePRCancelJobAsync(List<PreSalePromotionRequestItem> preSaleRequestItems)
        {
            if (preSaleRequestItems.Count() > 0)
            {
                var pRCancelJob = new PRCancelJob();
                var pRCancelJobItems = new List<PRCancelJobItem>();
                var pRCancelJobStatusSyncingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PRCancelJobStatus" && o.Key == "1").Select(o => o.ID).FirstAsync();
                pRCancelJob.Status = BackgroundJobStatus.Waiting;
                foreach (var item in preSaleRequestItems)
                {
                    var pRCancelJobItem = new PRCancelJobItem();
                    pRCancelJobItem.PreSalePromotionRequestItemID = item.ID;
                    pRCancelJobItem.PRCancelJobID = pRCancelJob.ID;
                    pRCancelJobItem.PRCancelJobStatusMasterCenterID = pRCancelJobStatusSyncingMasterCenterID;
                    pRCancelJobItem.UserName = item.UpdatedBy?.EmployeeNo;
                    pRCancelJobItem.PRNo = item.PRNo;
                    pRCancelJobItem.ItemNo = item.MasterPreSalePromotionItem?.ItemNo;
                    pRCancelJobItem.Retry = 0;
                    pRCancelJobItems.Add(pRCancelJobItem);
                }

                await DB.PRCancelJobs.AddAsync(pRCancelJob);
                await DB.PRCancelJobItems.AddRangeAsync(pRCancelJobItems);
                await DB.SaveChangesAsync();
            }
        }
        public async Task RunWaitingSyncJobAsync()
        {
            var waitingSyncJobs = await DB.PRCancelJobs.Where(o => o.Status == BackgroundJobStatus.Waiting).ToListAsync();
            waitingSyncJobs.ForEach(o => o.Status = BackgroundJobStatus.InProgress);
            DB.PRCancelJobs.UpdateRange(waitingSyncJobs);

            await DB.SaveChangesAsync();

            var pRCancelJobItems = new List<PRCancelJobItem>();

            foreach (var item in waitingSyncJobs)
            {
                try
                {
                    var prSyncItems = await DB.PRCancelJobItems.Where(o => o.PRCancelJobID == item.ID).ToListAsync();
                    item.FileName = "CRMPRDEL_" + item.Created.Value.ToString("yyyyMMddHHmmssfff") + ".txt";
                    item.Status = BackgroundJobStatus.WaitingForResult;

                    using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                    using (StreamWriter output = new StreamWriter(stream))
                    {
                        foreach (var item1 in prSyncItems)
                        {
                            output.WriteLine(item.FileName + ";"
                                            + item1.ID + ";"
                                            + item1.UserName + ";"
                                            + item1.PRNo + ";"
                                            + item1.ItemNo 
                            );
                        }
                        output.Flush();
                        Stream fileStream = new MemoryStream(stream.ToArray());
                        string fileName = item.FileName;
                        string filePath = "data/";
                        string contentType = "text/*";

                        await this.FileHelperSap.UploadFileFromStreamWithOutGuid(fileStream, "prd", filePath, fileName, contentType);

                        DB.PRCancelJobs.Update(item);
                        await DB.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    item.Status = BackgroundJobStatus.Failed;
                    item.ErrorMessage = "Error occurs when write text file to SAP: " + ex.ToString();
                    DB.PRCancelJobs.Update(item);
                    await DB.SaveChangesAsync();
                }
            }
            waitingSyncJobs.ForEach(o => o.Status = BackgroundJobStatus.Completed);
            DB.PRCancelJobs.UpdateRange(waitingSyncJobs);
            await DB.SaveChangesAsync();
        }

        public async Task ReadSyncResultFromSAPAsync()
        {
            var getFileNameFromResults = await this.FileHelperSap.GetListFile("prd", "result/");
            var pRCancelJobStatusSuccessMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PRCancelJobStatus" && o.Key == "2").Select(o => o.ID).FirstAsync();
            var pRCancelJobStatusRetryingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PRCancelJobStatus" && o.Key == "3").Select(o => o.ID).FirstAsync();
            var sAPPRStatusFailedMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "SAPPRStatus" && o.Key == SAPPRStatusKeys.Failed).Select(o => o.ID).FirstOrDefaultAsync();
            var sAPPRStatusCompleteMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "SAPPRStatus" && o.Key == SAPPRStatusKeys.Completed).Select(o => o.ID).FirstOrDefaultAsync();
            var promotionRequestPRStatusApproveMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionRequestPRStatus" && o.Key == "1").Select(o => o.ID).FirstOrDefaultAsync();
            var promotionRequestPRStatusApproveSomeUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionRequestPRStatus" && o.Key == "2").Select(o => o.ID).FirstOrDefaultAsync();
            foreach (var item in getFileNameFromResults)
            {
                var fileName = item.Split("/").Last();
                if (fileName != "empty.txt")
                {
                    var temp = await FileHelperSap.DownLoadToTempFileAsync("prd", "result/", fileName);
                    var listCancelResults = new List<PRCancelJobItemResult>();
                    using (StreamReader streamReader = new StreamReader(temp, Encoding.UTF8))
                    {
                        string line;
                        var content = new List<string>();
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            content.Add(line);
                        }
                        foreach (var data in content)
                        {
                            var detail = data.Split(';').ToList();
                            var syncResult = new PRCancelJobItemResult();
                            syncResult.PRCancelJobItemID = new Guid(detail[1]);
                            syncResult.IsError = detail[2].ToLower() == "x" ? true : false;
                            syncResult.ErrorCode = detail[3];
                            syncResult.ErrorDescription = detail[4];
                            syncResult.IsFMCreatePR = detail[5].ToLower() == "x" ? true : false;
                            syncResult.PRNo = detail[6];
                            syncResult.ItemNo = detail[7];
                            syncResult.SAPDeleteFlag = detail[8].ToLower() == "x" ? true : false;
                            syncResult.SAPCreateBy = detail[9];
                            syncResult.SAPCreateDateTime = DateTime.ParseExact(detail[10] + " " + detail[11], "yyyyMMdd HH:mm:ss", null);
                            listCancelResults.Add(syncResult);
                        }
                    }
                    var listPreSalePromotionRequestUnitID = new List<Guid>();
                    foreach (var itemResults in listCancelResults)
                    {
                        var cancelItem = await DB.PRCancelJobItems.Where(o => o.ID == itemResults.PRCancelJobItemID).FirstOrDefaultAsync();
                        if (cancelItem != null)
                        {
                            var cancelJob = await DB.PRCancelJobs.Where(o => o.ID == cancelItem.PRCancelJobID).FirstOrDefaultAsync();
                            if (cancelJob != null)
                            {
                                try
                                {
                                    var prCancelItem = await DB.PreSalePromotionRequestItems.Where(o => o.ID == cancelItem.PreSalePromotionRequestItemID).Include(o => o.PreSalePromotionRequestUnit).FirstOrDefaultAsync();

                                    listPreSalePromotionRequestUnitID.Add((Guid)prCancelItem.PreSalePromotionRequestUnitID);

                                    if (!itemResults.IsError)
                                    {
                                        cancelItem.PRCancelJobStatusMasterCenterID = pRCancelJobStatusSuccessMasterCenterID;
                                    }
                                    else
                                    {
                                        var presaleRequestUnit = await DB.PreSalePromotionRequestUnits.Where(o => o.ID == prCancelItem.PreSalePromotionRequestUnitID).FirstOrDefaultAsync();
                                        presaleRequestUnit.SAPPRStatusMasterCenterID = sAPPRStatusFailedMasterCenterID;
                                        cancelItem.PRCancelJobStatusMasterCenterID = pRCancelJobStatusRetryingMasterCenterID;
                                        DB.PreSalePromotionRequestUnits.Update(presaleRequestUnit);
                                    }
                                    cancelJob.Status = BackgroundJobStatus.Completed;
                                    cancelJob.SAPResultFileName = fileName;
                                    DB.PRCancelJobs.Update(cancelJob);
                                    DB.PRCancelJobItems.Update(cancelItem);


                                }
                                catch (Exception ex)
                                {
                                    if (cancelJob != null)
                                    {
                                        cancelJob.Status = BackgroundJobStatus.Failed;
                                        cancelJob.ErrorMessage = "Error occurs when read text file from SAP: " + ex.ToString();
                                        DB.PRCancelJobs.Update(cancelJob);
                                        await DB.SaveChangesAsync();
                                    }
                                }
                            }
                        }
                    }
                    var checkDatas = await DB.PRCancelJobItems.Where(o => listCancelResults.Select(p => p.PRCancelJobItemID).Contains(o.ID)).CountAsync();
                    if (checkDatas == listCancelResults.Count())
                    {
                        var listPreSaleUnitUpdate = new List<PreSalePromotionRequestUnit>();
                        var listPreSaleRequestID = new List<Guid>();
                        var listPreSaleRequestUpdate = new List<PreSalePromotionRequest>();

                        foreach (var presaleUnitID in listPreSalePromotionRequestUnitID.Distinct())
                        {
                            var presaleUnit = await DB.PreSalePromotionRequestUnits.Where(o => o.ID == presaleUnitID).FirstOrDefaultAsync();
                            var presaleItems = await DB.PreSalePromotionRequestItems.Where(o => o.PreSalePromotionRequestUnitID == presaleUnitID).ToListAsync();
                            if (presaleItems.TrueForAll(o => !string.IsNullOrEmpty(o.PRNo)) && presaleUnit.SAPPRStatusMasterCenterID != sAPPRStatusFailedMasterCenterID)
                            {
                                presaleUnit.SAPPRStatusMasterCenterID = sAPPRStatusCompleteMasterCenterID;
                                listPreSaleUnitUpdate.Add(presaleUnit);
                            }
                            listPreSaleRequestID.Add(presaleUnit.PreSalePromotionRequestID);
                        }
                        await DB.PRCancelJobItemResults.AddRangeAsync(listCancelResults);
                        DB.PreSalePromotionRequestUnits.UpdateRange(listPreSaleUnitUpdate);
                        await DB.SaveChangesAsync();

                        foreach (var preSaleRequestID in listPreSaleRequestID.Distinct())
                        {
                            var presalerequest = await DB.PreSalePromotionRequests.Where(o => o.ID == preSaleRequestID).FirstOrDefaultAsync();
                            var presalueUnits = await DB.PreSalePromotionRequestUnits.Where(o => o.PreSalePromotionRequestID == preSaleRequestID).ToListAsync();
                            if (presalueUnits.TrueForAll(o => o.SAPPRStatusMasterCenterID == sAPPRStatusCompleteMasterCenterID))
                            {
                                presalerequest.PromotionRequestPRStatusMasterCenterID = promotionRequestPRStatusApproveMasterCenterID;
                                presalerequest.PRCompletedDate = DateTime.Now;
                                listPreSaleRequestUpdate.Add(presalerequest);
                            }
                            else if (presalueUnits.Any(o => o.SAPPRStatusMasterCenterID == sAPPRStatusCompleteMasterCenterID))
                            {
                                presalerequest.PromotionRequestPRStatusMasterCenterID = promotionRequestPRStatusApproveSomeUnitMasterCenterID;
                                presalerequest.PRCompletedDate = DateTime.Now;
                                listPreSaleRequestUpdate.Add(presalerequest);
                            }
                        }
                        DB.PreSalePromotionRequests.UpdateRange(listPreSaleRequestUpdate);
                        await DB.SaveChangesAsync();
                        await this.FileHelperSap.MoveAndRemoveFileAsync("prd", "result/" + fileName, "prd", "result_backup/" + fileName);
                    }
                }
            }
        }

        public async Task CreateRetrySyncJobAsync(Guid requestUnitID)
        {
            var pRCancelJobStatusRetryingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PRCancelJobStatus" && o.Key == "3").Select(o => o.ID).FirstAsync();
            var pRCancelJobStatusSyncingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PRCancelJobStatus" && o.Key == "1").Select(o => o.ID).FirstAsync();

            var allItemID = await DB.PreSalePromotionRequestItems.Where(o => o.PreSalePromotionRequestUnitID == requestUnitID).Select(o => (Guid?)o.ID).ToListAsync();
            var pRCancelJobItems = await DB.PRCancelJobItems.Where(o => o.PRCancelJobStatusMasterCenterID == pRCancelJobStatusRetryingMasterCenterID && allItemID.Contains(o.PreSalePromotionRequestItemID)).ToListAsync();

            if (pRCancelJobItems.Count() > 0)
            {
                var pRCancelJob = new PRCancelJob();
                var pRCancelJobItems_new = new List<PRCancelJobItem>();

                pRCancelJob.Status = BackgroundJobStatus.Waiting;

                foreach (var item in pRCancelJobItems)
                {
                    var pRCancelJobItem = new PRCancelJobItem();
                    pRCancelJobItem.PRCancelJobID = pRCancelJob.ID;
                    pRCancelJobItem.PRCancelJobStatusMasterCenterID = pRCancelJobStatusSyncingMasterCenterID;
                    pRCancelJobItem.UserName = item.UserName;
                    pRCancelJobItem.PreSalePromotionRequestItemID = item.PreSalePromotionRequestItemID;
                    pRCancelJobItem.Retry = ++item.Retry;
                    pRCancelJobItem.ItemNo = item.ItemNo;
                    pRCancelJobItem.PRNo = item.PRNo;
                    pRCancelJobItems_new.Add(pRCancelJobItem);
                }

                await DB.PRCancelJobs.AddAsync(pRCancelJob);
                await DB.PRCancelJobItems.AddRangeAsync(pRCancelJobItems_new);
                await DB.SaveChangesAsync();
            }
        }
    }
}
