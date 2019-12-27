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
    public class PRRequestJobService : IPRRequestJobService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelperSap;
        public PRRequestJobService(IConfiguration configuration, DatabaseContext db)
        {
            this.Configuration = configuration;
            this.DB = db;

            var minioSapEndpoint = Configuration["MinioSAP:Endpoint"];
            var minioSapAccessKey = Configuration["MinioSAP:AccessKey"];
            var minioSapSecretKey = Configuration["MinioSAP:SecretKey"];
            var minioSapWithSSL = Configuration["MinioSAP:WithSSL"];

            this.FileHelperSap = new FileHelper(minioSapEndpoint, minioSapAccessKey, minioSapSecretKey, "prc", "", minioSapWithSSL == "true");
        }

        public async Task CreateNewPreSalePRRequestJobAsync(List<PreSalePromotionRequestItem> inputs)
        {
            if (inputs.Count > 0)
            {
                var vendors = inputs.Select(o => o.MasterPreSalePromotionItem.SAPVendor).Distinct().ToList();
                //var fixDocType = inputs.
                foreach (var vendor in vendors)
                {
                    var model = new PRRequestJob();
                    model.FileName = "CRMPR_" + DateTime.Now.ToString("yyyyMMddHHmmssfff_" + vendor) + ".txt";
                    var listPRSyncItem = new List<PRRequestJobItem>();
                    var pRRequestJobStatusSyncingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PRRequestJobStatus" && o.Key == PRRequestJobStatusKeys.Syncing).Select(o => o.ID).FirstAsync();
                    model.Status = BackgroundJobStatus.Waiting;
                    var groupVenders = inputs.Where(o => o.MasterPreSalePromotionItem.SAPVendor == vendor);
                    foreach (var item in groupVenders)
                    {
                        var prSyncItem = new PRRequestJobItem();
                        var materialGroup = await DB.PromotionMaterialGroups.Where(o => o.Key == item.MasterPreSalePromotionItem.MaterialGroupKey).FirstOrDefaultAsync();
                        prSyncItem.PRRequestJobStatusMasterCenterID = pRRequestJobStatusSyncingMasterCenterID;
                        prSyncItem.PRRequestJobID = model.ID;
                        prSyncItem.PreSalePromotionRequestItemID = item.ID;
                        prSyncItem.UserName = item.UpdatedBy?.EmployeeNo;
                        prSyncItem.PromotionNo = item.MasterPreSalePromotionItem?.MasterPreSalePromotion?.PromotionNo;
                        prSyncItem.DocType = materialGroup?.DocType;
                        prSyncItem.PurchasingGroup = item.MasterPreSalePromotionItem?.SAPPurchasingGroup;
                        prSyncItem.PurchasingOrg = item.MasterPreSalePromotionItem?.SAPPurchasingOrg;
                        prSyncItem.Requester = item.UpdatedBy?.DisplayName;
                        prSyncItem.Plant = item.MasterPreSalePromotionItem?.MasterPreSalePromotion?.Project?.Plant;
                        prSyncItem.AccountAssignmentCategory = "P";
                        prSyncItem.MaterialNo = item.MasterPreSalePromotionItem?.MaterialCode;
                        prSyncItem.ShortText = item.MasterPreSalePromotionItem?.MaterialName;
                        prSyncItem.Quantity = item.Quantity;
                        prSyncItem.TotalPrice = item.TotalPrice;
                        prSyncItem.DeliveryDate = Convert.ToDateTime(item.ReceiveDate);
                        prSyncItem.PriceUnit = "1";
                        prSyncItem.AgreementNo = item.MasterPreSalePromotionItem?.AgreementNo;
                        prSyncItem.ItemNo = item.MasterPreSalePromotionItem?.ItemNo;
                        prSyncItem.GoodReceiptIndicator = "X";
                        prSyncItem.InvoiceReceiptIndicator = "X";
                        prSyncItem.CreatedByDisplayName = item.UpdatedBy?.DisplayName;
                        prSyncItem.SerialNo = "01";
                        prSyncItem.GoodRecipient = item.UpdatedBy?.DisplayName;
                        prSyncItem.GLAccountNo = item.MasterPreSalePromotionItem?.GLAccountNo;
                        prSyncItem.SAPWBSObject_P = item.PreSalePromotionRequestUnit?.Unit?.SAPWBSObject_P;
                        prSyncItem.SAPWBSNo_P = item.PreSalePromotionRequestUnit?.Unit?.SAPWBSNo_P;
                        prSyncItem.PromotionName = item.MasterPreSalePromotionItem?.MasterPreSalePromotion?.Name;
                        prSyncItem.ApproveName = item.UpdatedBy?.DisplayName;
                        prSyncItem.TextB01 = "";
                        prSyncItem.TextB02 = "";
                        prSyncItem.TextB03 = "";
                        prSyncItem.TextB04 = "";
                        prSyncItem.Retry = 0;
                        listPRSyncItem.Add(prSyncItem);
                    }
                    await DB.PRRequestJobs.AddAsync(model);
                    await DB.PRRequestJobItems.AddRangeAsync(listPRSyncItem);
                    await DB.SaveChangesAsync();
                }
            }
        }

        public async Task RunWaitingSyncJobAsync()
        {
            try
            {
                var waitingSyncJobs = await DB.PRRequestJobs.Where(o => o.Status == BackgroundJobStatus.Waiting).ToListAsync();
                waitingSyncJobs.ForEach(o => o.Status = BackgroundJobStatus.InProgress);
                DB.PRRequestJobs.UpdateRange(waitingSyncJobs);

                await DB.SaveChangesAsync();

                var listPRSyncItem = new List<PRRequestJobItem>();

                foreach (var item in waitingSyncJobs)
                {
                    try
                    {
                        var prSyncItems = await DB.PRRequestJobItems.Where(o => o.PRRequestJobID == item.ID).ToListAsync();
                        item.Status = BackgroundJobStatus.WaitingForResult;
                        using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                        using (StreamWriter output = new StreamWriter(stream))
                        {
                            foreach (var item1 in prSyncItems)
                            {
                                output.WriteLine(item.FileName + ";"
                                                + item1.ID + ";"
                                                + item1.UserName + ";"
                                                + item1.PromotionNo + ";"
                                                + item1.DocType + ";"
                                                + item1.PurchasingGroup + ";"
                                                + item1.PurchasingOrg + ";"
                                                + item1.Requester + ";"
                                                + item1.Plant + ";"
                                                + item1.AccountAssignmentCategory + ";"
                                                + item1.MaterialNo + ";"
                                                + item1.ShortText + ";"
                                                + item1.Quantity + ";"
                                                + item1.TotalPrice + ";"
                                                + item1.DeliveryDate.ToString("yyyyMMdd") + ";"
                                                + item1.PriceUnit + ";"
                                                + item1.AgreementNo + ";"
                                                + item1.ItemNo + ";"
                                                + item1.GoodReceiptIndicator + ";"
                                                + item1.InvoiceReceiptIndicator + ";"
                                                + item1.CreatedByDisplayName + ";"
                                                + item1.SerialNo + ";"
                                                + item1.GoodRecipient + ";"
                                                + item1.GLAccountNo + ";"
                                                + item1.SAPWBSNo_P + ";"
                                                + item1.PromotionName + ";"
                                                + item1.ApproveName + ";"
                                                + item1.TextB01 + ";"
                                                + item1.TextB02 + ";"
                                                + item1.TextB03 + ";"
                                                + item1.TextB04 
                                );
                            }
                            output.Flush();
                            Stream fileStream = new MemoryStream(stream.ToArray());
                            string fileName = item.FileName;
                            string filePath = $"data/";
                            string contentType = "text/*";
                            await this.FileHelperSap.UploadFileFromStreamWithOutGuid(fileStream, "prc", filePath, fileName, contentType);

                            DB.PRRequestJobs.Update(item);
                            await DB.SaveChangesAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        item.Status = BackgroundJobStatus.Failed;
                        item.ErrorMessage = "Error occurs when write text file to SAP: " + ex.ToString();
                        DB.PRRequestJobs.Update(item);
                        await DB.SaveChangesAsync();
                    }
                }
                waitingSyncJobs.ForEach(o => o.Status = BackgroundJobStatus.Completed);
                DB.PRRequestJobs.UpdateRange(waitingSyncJobs);
                await DB.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task ReadSyncResultFromSAPAsync()
        {
            var getFileNameFromResults = await this.FileHelperSap.GetListFile("prc", "result/");
            var pRRequestJobStatusSuccessMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PRRequestJobStatus" && o.Key == PRRequestJobStatusKeys.Success).Select(o => o.ID).FirstAsync();
            var pRRequestJobStatusRetryingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PRRequestJobStatus" && o.Key == PRRequestJobStatusKeys.Retrying).Select(o => o.ID).FirstAsync();
            var sAPPRStatusFailedMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "SAPPRStatus" && o.Key == SAPPRStatusKeys.Failed).Select(o => o.ID).FirstOrDefaultAsync();
            var sAPPRStatusCompleteMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "SAPPRStatus" && o.Key == SAPPRStatusKeys.Completed).Select(o => o.ID).FirstOrDefaultAsync();
            var promotionRequestPRStatusApproveMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionRequestPRStatus" && o.Key == "1").Select(o => o.ID).FirstOrDefaultAsync();
            var promotionRequestPRStatusApproveSomeUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionRequestPRStatus" && o.Key == "2").Select(o => o.ID).FirstOrDefaultAsync();
            foreach (var item in getFileNameFromResults)
            {
                var fileName = item.Split("/").Last();
                if (fileName != "empty.txt")
                {
                    var temp = await FileHelperSap.DownLoadToTempFileAsync("prc", "result/", fileName);
                    var listSyncResults = new List<PRRequestJobItemResult>();
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
                            var syncResult = new PRRequestJobItemResult();
                            syncResult.PRRequestJobItemID = new Guid(detail[1]);
                            syncResult.IsError = detail[2].ToLower() == "x" ? true : false;
                            syncResult.ErrorCode = detail[3];
                            syncResult.ErrorDescription = detail[4];
                            syncResult.IsFMCreatePR = detail[5].ToLower() == "x" ? true : false;
                            syncResult.PRNo = detail[6];
                            syncResult.ItemNo = detail[7];
                            syncResult.MaterialNo = detail[8];
                            syncResult.SAPCreateBy = detail[9];
                            syncResult.SAPCreateDateTime = DateTime.ParseExact(detail[10] + " " + detail[11], "yyyyMMdd HH:mm:ss", null);
                            listSyncResults.Add(syncResult);
                        }
                    }
                    var listPreSalePromotionRequestUnitID = new List<Guid>();
                    foreach (var itemResults in listSyncResults)
                    {
                        var syncItem = await DB.PRRequestJobItems.Where(o => o.ID == itemResults.PRRequestJobItemID).FirstOrDefaultAsync();
                        if (syncItem != null)
                        {
                            var syncJob = await DB.PRRequestJobs.Where(o => o.ID == syncItem.PRRequestJobID).FirstOrDefaultAsync();
                            if (syncJob != null)
                            {
                                try
                                {
                                    var prRequestItem = await DB.PreSalePromotionRequestItems.Where(o => o.ID == syncItem.PreSalePromotionRequestItemID).Include(o => o.PreSalePromotionRequestUnit).FirstOrDefaultAsync();
                                    prRequestItem.PRNo = itemResults.PRNo;
                                    listPreSalePromotionRequestUnitID.Add((Guid)prRequestItem.PreSalePromotionRequestUnitID);

                                    if (!itemResults.IsError)
                                    {
                                        syncItem.PRRequestJobStatusMasterCenterID = pRRequestJobStatusSuccessMasterCenterID;
                                    }
                                    else
                                    {
                                        var presaleRequestUnit = await DB.PreSalePromotionRequestUnits.Where(o => o.ID == prRequestItem.PreSalePromotionRequestUnitID).FirstOrDefaultAsync();
                                        presaleRequestUnit.SAPPRStatusMasterCenterID = sAPPRStatusFailedMasterCenterID;
                                        syncItem.PRRequestJobStatusMasterCenterID = pRRequestJobStatusRetryingMasterCenterID;
                                        DB.PreSalePromotionRequestUnits.Update(presaleRequestUnit);
                                    }
                                    syncJob.Status = BackgroundJobStatus.Completed;
                                    syncJob.SAPResultFileName = fileName;
                                    DB.PRRequestJobs.Update(syncJob);
                                    DB.PRRequestJobItems.Update(syncItem);
                                    DB.PreSalePromotionRequestItems.Update(prRequestItem);

                                }
                                catch (Exception ex)
                                {
                                    if (syncJob != null)
                                    {
                                        syncJob.Status = BackgroundJobStatus.Failed;
                                        syncJob.ErrorMessage = "Error occurs when read text file from SAP: " + ex.ToString();
                                        DB.PRRequestJobs.Update(syncJob);
                                        await DB.SaveChangesAsync();
                                    }
                                }
                            }
                        }
                    }
                    var checkDatas = await DB.PRRequestJobItems.Where(o => listSyncResults.Select(p => p.PRRequestJobItemID).Contains(o.ID)).CountAsync();
                    if (checkDatas == listSyncResults.Count())
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
                        await DB.PRRequestJobItemResults.AddRangeAsync(listSyncResults);
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
                        await this.FileHelperSap.MoveAndRemoveFileAsync("prc", "result/" + fileName, "prc", "result_backup/" + fileName);
                    }
                }
            }
        }

        public async Task CreateRetrySyncJobAsync(Guid requestUnitID)
        {
            var pRRequestJobStatusRetryingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PRRequestJobStatus" && o.Key == PRRequestJobStatusKeys.Retrying).Select(o => o.ID).FirstAsync();
            var pRRequestJobStatusSyncingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PRRequestJobStatus" && o.Key == PRRequestJobStatusKeys.Syncing).Select(o => o.ID).FirstAsync();

            var allItemID = await DB.PreSalePromotionRequestItems.Where(o => o.PreSalePromotionRequestUnitID == requestUnitID).Select(o => (Guid?)o.ID).ToListAsync();
            var syncItems = await DB.PRRequestJobItems.Where(o => o.PRRequestJobStatusMasterCenterID == pRRequestJobStatusRetryingMasterCenterID && allItemID.Contains(o.PreSalePromotionRequestItemID)).ToListAsync();

            if (syncItems.Count() > 0)
            {
                var requestJob = new PRRequestJob();
                var requestJobItems = new List<PRRequestJobItem>();

                requestJob.Status = BackgroundJobStatus.Waiting;

                foreach (var item in syncItems)
                {
                    var synItem = new PRRequestJobItem();
                    synItem.PRRequestJobID = requestJob.ID;
                    synItem.PRRequestJobStatusMasterCenterID = pRRequestJobStatusSyncingMasterCenterID;
                    synItem.PreSalePromotionRequestItemID = item.PreSalePromotionRequestItemID;
                    synItem.UserName = item.UserName;
                    synItem.PromotionNo = item.PromotionNo;
                    synItem.DocType = item.DocType;
                    synItem.PurchasingGroup = item.PurchasingGroup;
                    synItem.PurchasingOrg = item.PurchasingOrg;
                    synItem.Requester = item.Requester;
                    synItem.Plant = item.Plant;
                    synItem.AccountAssignmentCategory = item.AccountAssignmentCategory;
                    synItem.MaterialNo = item.MaterialNo;
                    synItem.ShortText = item.ShortText;
                    synItem.Quantity = item.Quantity;
                    synItem.TotalPrice = item.TotalPrice;
                    synItem.DeliveryDate = item.DeliveryDate;
                    synItem.PriceUnit = item.PriceUnit;
                    synItem.AgreementNo = item.AgreementNo;
                    synItem.ItemNo = item.ItemNo;
                    synItem.GoodReceiptIndicator = item.GoodReceiptIndicator;
                    synItem.InvoiceReceiptIndicator = item.InvoiceReceiptIndicator;
                    synItem.CreatedByDisplayName = item.CreatedByDisplayName;
                    synItem.SerialNo = item.SerialNo;
                    synItem.GoodRecipient = item.GoodRecipient;
                    synItem.GLAccountNo = item.GLAccountNo;
                    synItem.SAPWBSObject_P = item.SAPWBSObject_P;
                    synItem.SAPWBSNo_P = item.SAPWBSNo_P;
                    synItem.PromotionName = item.PromotionName;
                    synItem.Retry = ++item.Retry;
                    synItem.ApproveName = item.ApproveName;
                    synItem.TextB01 = item.TextB01;
                    synItem.TextB02 = item.TextB02;
                    synItem.TextB03 = item.TextB03;
                    synItem.TextB04 = item.TextB04;
                    requestJobItems.Add(synItem);
                }

                await DB.PRRequestJobs.AddAsync(requestJob);
                await DB.PRRequestJobItems.AddRangeAsync(requestJobItems);
                await DB.SaveChangesAsync();
            }
        }
    }
}
