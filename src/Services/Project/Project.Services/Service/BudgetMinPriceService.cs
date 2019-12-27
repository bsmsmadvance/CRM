using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using ExcelExtensions;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using PagingExtensions;
using Project.Params.Filters;
using Project.Services.Excels;
using Project.Params.Outputs;
using ErrorHandling;
using System.ComponentModel;
using System.Reflection;

namespace Project.Services
{
    public class BudgetMinPriceService : IBudgetMinPriceService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;

        public BudgetMinPriceService(IConfiguration configuration, DatabaseContext db)
        {
            this.Configuration = configuration;
            this.DB = db;

            var minioEndpoint = Configuration["Minio:Endpoint"];
            var minioAccessKey = Configuration["Minio:AccessKey"];
            var minioSecretKey = Configuration["Minio:SecretKey"];
            var minioBucketName = Configuration["Minio:DefaultBucket"];
            var minioTempBucketName = Configuration["Minio:TempBucket"];
            var minioWithSSL = Configuration["Minio:WithSSL"];
            this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, minioWithSSL == "true");
        }
        public async Task<BudgetMinPricePaging> GetBudgetMinPriceListAsync(BudgetMinPriceFilter filter, PageParam pageParam, BudgetMinPriceSortByParam sortByParam)
        {
            ValidateException ex = new ValidateException();
            BudgetMinPriceDTO MsgDTO = new BudgetMinPriceDTO();
            if (filter.Quarter != null && filter.Quarter > 4)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0100").FirstAsync();
                string desc = MsgDTO.GetType().GetProperty(nameof(BudgetMinPriceDTO.Quarter)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[message]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (filter.Year != null && filter.Year > 2300)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0101").FirstAsync();
                //string desc = MsgDTO.GetType().GetProperty(nameof(BudgetMinPriceDTO.Quarter)).GetCustomAttribute<DescriptionAttribute>().Description;
                //var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }

            var result = new BudgetMinPricePaging();
            result = GetBudgetMinPriceUnitListAsync(filter, pageParam, sortByParam);
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137948/preview
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private BudgetMinPriceDTO GetBudgetMinPriceAsync(BudgetMinPriceFilter filter)
        {
            var temp = DB.BudgetMinPrices.Where(o => o.ProjectID == filter.ProjectID && o.Quarter == filter.Quarter && o.Year == filter.Year)
                                                .Include(o => o.BudgetMinPriceType)
                                                .Include(o => o.UpdatedBy)
                                                .Select(o => new BudgetMinPriceQueryResult
                                                {
                                                    BudgetMinPrice = o,
                                                    Project = o.Project,
                                                })
                                                .ToList();
            if (temp.Count() > 0)
            {
                var model = temp.GroupBy(o => new { o.Project, o.BudgetMinPrice.Year, o.BudgetMinPrice.Quarter }).Select(o => new BudgetMinPriceQueryResult
                {
                    Project = o.Key.Project,
                    BudgetMinPrice = o.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
                    BudgetMinPriceQuarterly = o.Where(p => p.BudgetMinPrice.BudgetMinPriceType?.Key == BudgetMinPriceTypeKeys.Quarterly).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
                    BudgetMinPriceTransfer = o.Where(p => p.BudgetMinPrice.BudgetMinPriceType?.Key == BudgetMinPriceTypeKeys.TransferPromotion).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
                }).FirstOrDefault();

                var result = BudgetMinPriceDTO.CreateFromQueryResult(model);

                return result;
            }
            else
            {
                var project = DB.Projects.Where(o => o.ID == filter.ProjectID.Value).FirstOrDefault();

                //var masterCenterQuarterlyID = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType && o.Key == BudgetMinPriceTypeKeys.Quarterly).Select(o => o.ID).FirstOrDefault();
                //var masterCenterTransferID = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType && o.Key == BudgetMinPriceTypeKeys.Quarterly).Select(o => o.ID).FirstOrDefault();

                var result = new BudgetMinPriceDTO
                {
                    ID = Guid.NewGuid(),
                    Project = ProjectDropdownDTO.CreateFromModel(project),
                    Quarter = filter.Quarter ?? 0,
                    Year = filter.Year ?? 0,
                    TransferTotalAmount = 0,
                    TransferTotalUnit = 0,
                    QuarterlyTotalAmount = 0
                };

                //var quarterly = new BudgetMinPrice();
                //result.ToModelQuarterly(ref quarterly);
                //quarterly.BudgetMinPriceTypeMasterCenterID = masterCenterQuarterlyID;

                //var transfer = new BudgetMinPrice();
                //result.ToModelTransfer(ref transfer);
                //transfer.BudgetMinPriceTypeMasterCenterID = masterCenterTransferID;

                //await DB.BudgetMinPrices.AddAsync(quarterly);
                //await DB.BudgetMinPrices.AddAsync(transfer);
                //await DB.SaveChangesAsync();

                return result;
            }
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137948/preview
        /// *ดึงข้อมูลโดย query จาก unit ทั้งหมดของโครงการนั้นก่อน (ข้อมูลโครงการอยู่ใน BudgetMinPriceFilter)
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private BudgetMinPricePaging GetBudgetMinPriceUnitListAsync(BudgetMinPriceFilter filter, PageParam pageParam, BudgetMinPriceSortByParam sortByParam)
        {
            #region
            //var budget = DB.BudgetMinPrices.Where(o => o.ProjectID == filter.ProjectID
            //                                                && o.Quarter == filter.Quarter
            //                                                && o.Year == filter.Year
            //                                                && o.BudgetMinPriceType.Key == BudgetMinPriceTypeKeys.Quarterly
            //                                                && o.ActiveDate <= DateTime.Now
            //                               ).OrderByDescending(o => o.ActiveDate).FirstOrDefault();
            //if (budget == null)
            //{
            //    var project = DB.Projects.Where(o => o.ID == filter.ProjectID.Value).FirstOrDefault();

            //    var masterCenterQuarterlyID = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType && o.Key == BudgetMinPriceTypeKeys.Quarterly).Select(o => o.ID).FirstOrDefault();
            //    var masterCenterTransferID = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType && o.Key == BudgetMinPriceTypeKeys.Quarterly).Select(o => o.ID).FirstOrDefault();

            //    var result = new BudgetMinPriceDTO
            //    {
            //        Project = ProjectDropdownDTO.CreateFromModel(project),
            //        Quarter = filter.Quarter ?? 0,
            //        Year = filter.Year ?? 0,
            //        TransferTotalAmount = 0,
            //        TransferTotalUnit = 0,
            //        QuarterlyTotalAmount = 0
            //    };

            //    var quarterly = new BudgetMinPrice();
            //    result.ToModelQuarterly(ref quarterly);
            //    quarterly.BudgetMinPriceTypeMasterCenterID = masterCenterQuarterlyID;

            //    //var transfer = new BudgetMinPrice();
            //    //result.ToModelTransfer(ref transfer);
            //    //transfer.BudgetMinPriceTypeMasterCenterID = masterCenterTransferID;

            //    //await DB.BudgetMinPrices.AddAsync(quarterly);
            //    //await DB.BudgetMinPrices.AddAsync(transfer);
            //    //await DB.SaveChangesAsync();

            //    budget = quarterly;

            //}
            #endregion

            var BudgetMinPrice = GetBudgetMinPriceAsync(filter);
            var budgetMinPriceQuarterlyID = BudgetMinPrice.ID;
            // IQueryable<BudgetMinPriceUnitQueryResult> 
            var query = from unit in DB.Units.Include(o => o.UnitStatus).Where(o => o.ProjectID == filter.ProjectID)

                        join BudgetMinPriceUnit in DB.BudgetMinPriceUnits.Where(o => o.BudgetMinPriceID == budgetMinPriceQuarterlyID)
                        on unit.ID equals BudgetMinPriceUnit.UnitID into BudgetMinPriceUnitGroup
                        from BudgetMinPriceUnitModel in BudgetMinPriceUnitGroup.DefaultIfEmpty()

                        join UpdatedBy in DB.Users
                        on BudgetMinPriceUnitModel.UpdatedByUserID equals UpdatedBy.ID into UpdatedByGroup
                        from UpdatedByModel in UpdatedByGroup.DefaultIfEmpty()

                        select new BudgetMinPriceUnitQueryResult
                        {
                            Unit = unit,
                            BudgetMinPriceUnit = BudgetMinPriceUnitModel ?? new BudgetMinPriceUnit(),
                            User = UpdatedByModel ?? new Database.Models.USR.User()
                        };


            #region filter
            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(x => (x.Unit.UnitNo ?? "").Contains(filter.UnitNo));
            }
            if (filter.AmonutFrom != null)
            {
                query = query.Where(x => x.BudgetMinPriceUnit.Amount >= filter.AmonutFrom);
            }
            if (filter.AmonutTo != null)
            {
                query = query.Where(x => x.BudgetMinPriceUnit.Amount <= filter.AmonutTo);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => (x.User.DisplayName ?? "").Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.BudgetMinPriceUnit.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.BudgetMinPriceUnit.Updated <= filter.UpdatedTo);
            }
            if (filter.UnitStatus != null)
            {
                query = query.Where(x => x.Unit.UnitStatus.ID == filter.UnitStatus);
            }
            #endregion


            BudgetMinPriceUnitDTO.SortBy(sortByParam, ref query);
            var pageOuput = PagingHelper.Paging<BudgetMinPriceUnitQueryResult>(pageParam, ref query);
            var queryResults = query.ToList();
            var results = queryResults.Select(o => BudgetMinPriceUnitDTO.CreateFromQueryResult(o)).ToList();

            var resultBudgetMinPricec = new BudgetMinPricePaging();
            resultBudgetMinPricec.BudgetMinPriceListDTO = new BudgetMinPriceListDTO();
            resultBudgetMinPricec.BudgetMinPriceListDTO.BudgetMinPriceUnitDTO = results;
            resultBudgetMinPricec.BudgetMinPriceListDTO.BudgetMinPriceDTO = new BudgetMinPriceDTO();
            resultBudgetMinPricec.PageOutput = pageOuput;
            resultBudgetMinPricec.BudgetMinPriceListDTO.BudgetMinPriceDTO = BudgetMinPrice;

            return resultBudgetMinPricec;
        }


        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137948/preview
        /// สร้างหรือแก้ไข (โดยเช็คจาก project id, quarter, year)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BudgetMinPriceDTO> SaveBudgetMinPriceAsync(BudgetMinPriceFilter filter, BudgetMinPriceDTO input)
        {
            var project = await DB.Projects.Where(o => o.ID == filter.ProjectID.Value).FirstAsync();
            var masterCenterQuarterlyID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType && o.Key == BudgetMinPriceTypeKeys.Quarterly).Select(o => o.ID).FirstAsync();
            var masterCenterTransferID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType && o.Key == BudgetMinPriceTypeKeys.TransferPromotion).Select(o => o.ID).FirstAsync();
            var temp = await DB.BudgetMinPrices.Where(o => o.ProjectID == filter.ProjectID && o.Quarter == filter.Quarter && o.Year == filter.Year)
                                                .Include(o => o.BudgetMinPriceType)
                                                .Select(o => new BudgetMinPriceQueryResult
                                                {
                                                    BudgetMinPrice = o,
                                                    Project = o.Project,
                                                })
                                                .ToListAsync();
            var model = temp.GroupBy(o => o.Project).Select(o => new BudgetMinPriceQueryResult
            {
                Project = o.Key,
                BudgetMinPrice = o.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
                BudgetMinPriceQuarterly = o.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now && p.BudgetMinPrice.BudgetMinPriceType?.Key == BudgetMinPriceTypeKeys.Quarterly).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
                BudgetMinPriceTransfer = o.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now && p.BudgetMinPrice.BudgetMinPriceType?.Key == BudgetMinPriceTypeKeys.TransferPromotion).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
            }).FirstOrDefault();

            if (model.BudgetMinPriceQuarterly.TotalAmount != input.QuarterlyTotalAmount)
            {
                var quarterly = new BudgetMinPrice();
                input.ToModelQuarterly(ref quarterly);
                quarterly.BudgetMinPriceTypeMasterCenterID = masterCenterQuarterlyID;
                quarterly.UsedAmount = await GetOldUsedAmount(model.BudgetMinPriceQuarterly.ID);
                await DB.BudgetMinPrices.AddAsync(quarterly);

                var allunitinquarterly = await DB.BudgetMinPriceUnits.Where(o => o.BudgetMinPriceID == model.BudgetMinPriceQuarterly.ID).ToListAsync();
                var newUnitQuarterly = new List<BudgetMinPriceUnit>();

                foreach (var item in allunitinquarterly)
                {
                    var newUnitModel = new BudgetMinPriceUnit();
                    newUnitModel.BudgetMinPriceID = quarterly.ID;
                    newUnitModel.UnitID = item.UnitID;
                    newUnitModel.Amount = item.Amount;
                    newUnitQuarterly.Add(newUnitModel);
                }

                await DB.BudgetMinPriceUnits.AddRangeAsync(newUnitQuarterly);
            }
            if (model.BudgetMinPriceTransfer?.TotalAmount != input.TransferTotalAmount || model.BudgetMinPriceTransfer?.UnitAmount != input.TransferTotalUnit)
            {
                var transfer = new BudgetMinPrice();
                input.ToModelTransfer(ref transfer);
                transfer.BudgetMinPriceTypeMasterCenterID = masterCenterTransferID;
                await DB.BudgetMinPrices.AddAsync(transfer);
            }
            await DB.SaveChangesAsync();

            //var result = await this.GetBudgetMinPriceAsync(filter);

            return new BudgetMinPriceDTO();
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137948/preview
        /// ถ้าไม่เคยมี budget min price unit ให้สร้างใหม่
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public async Task SaveBudgetMinPriceUnitListAsync(BudgetMinPriceListDTO inputs)
        {

            ValidateException ex = new ValidateException();
            BudgetMinPriceDTO MsgDTO = new BudgetMinPriceDTO();
            if (inputs.BudgetMinPriceDTO.Quarter != null && inputs.BudgetMinPriceDTO.Quarter > 4)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0100").FirstAsync();
                string desc = MsgDTO.GetType().GetProperty(nameof(BudgetMinPriceDTO.Quarter)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[message]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (inputs.BudgetMinPriceDTO.Year != null && inputs.BudgetMinPriceDTO.Year > 2300)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0101").FirstAsync();
                ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
            var budgetMinPriceQuarterly = DB.BudgetMinPrices.Include(o => o.BudgetMinPriceType).Where(x => x.ID == inputs.BudgetMinPriceDTO.ID).FirstOrDefault() ?? null;
            var temp = DB.BudgetMinPrices.Where(o => o.ProjectID == inputs.BudgetMinPriceDTO.Project.Id && o.Quarter == inputs.BudgetMinPriceDTO.Quarter && o.Year == inputs.BudgetMinPriceDTO.Year)
                                               .Include(o => o.BudgetMinPriceType)
                                               .Include(o => o.UpdatedBy)
                                               .Select(o => new BudgetMinPriceQueryResult
                                               {
                                                   BudgetMinPrice = o,
                                                   Project = o.Project,
                                               })
                                               .ToList();


            var masterCenterQuarterlyID = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType && o.Key == BudgetMinPriceTypeKeys.Quarterly).Select(o => o.ID).FirstOrDefault();
            var masterCenterTransferID = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType && o.Key == BudgetMinPriceTypeKeys.TransferPromotion).Select(o => o.ID).FirstOrDefault();

            if (temp.Count > 0)
            {
                var BudgetMinPriceQuarterly = temp.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now && p.BudgetMinPrice.BudgetMinPriceType?.Key == BudgetMinPriceTypeKeys.Quarterly).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).FirstOrDefault();
                var BudgetMinPriceTransfer = temp.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now && p.BudgetMinPrice.BudgetMinPriceType?.Key == BudgetMinPriceTypeKeys.TransferPromotion).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).FirstOrDefault();

                //var resultQuarterly = BudgetMinPriceDTO.CreateFromQueryResult(BudgetMinPriceQuarterly);
                //resultQuarterly.QuarterlyTotalAmount = inputs.BudgetMinPriceDTO.QuarterlyTotalAmount;

                var quarterly = new BudgetMinPrice();
                inputs.BudgetMinPriceDTO.ToModelQuarterly(ref quarterly);
                quarterly.BudgetMinPriceTypeMasterCenterID = masterCenterQuarterlyID;

                // var resultTransfer = BudgetMinPriceDTO.CreateFromQueryResult(BudgetMinPriceTransfer);
                //resultTransfer.TransferTotalAmount = inputs.BudgetMinPriceDTO.TransferTotalAmount;
                //resultTransfer.TransferTotalUnit = inputs.BudgetMinPriceDTO.TransferTotalUnit;
                var transfer = new BudgetMinPrice();
                inputs.BudgetMinPriceDTO.ToModelTransfer(ref transfer);
                transfer.BudgetMinPriceTypeMasterCenterID = masterCenterTransferID;

                await DB.BudgetMinPrices.AddAsync(quarterly);
                await DB.BudgetMinPrices.AddAsync(transfer);
                await DB.SaveChangesAsync();

                Guid newID = quarterly.ID;
                IQueryable<BudgetMinPriceUnitQueryResult> query = from unit in DB.Units.Include(o => o.UnitStatus).Where(o => o.ProjectID == inputs.BudgetMinPriceDTO.Project.Id)

                                                                  join BudgetMinPriceUnit in DB.BudgetMinPriceUnits.Include(o => o.UpdatedBy).Where(o => o.BudgetMinPriceID == BudgetMinPriceQuarterly.BudgetMinPrice.ID)
                                                                  on unit.ID equals BudgetMinPriceUnit.UnitID into BudgetMinPriceUnitGroup
                                                                  from BudgetMinPriceUnitModel in BudgetMinPriceUnitGroup.DefaultIfEmpty()

                                                                  select new BudgetMinPriceUnitQueryResult
                                                                  {
                                                                      Unit = unit,
                                                                      BudgetMinPriceUnit = BudgetMinPriceUnitModel ?? new BudgetMinPriceUnit(),
                                                                  };

                var queryToList = query.ToList();
                var createBudgetMinPriceUnits = new List<BudgetMinPriceUnit>();

                foreach (var item in queryToList)
                {
                    var model = inputs.BudgetMinPriceUnitDTO.Where(o => o.Unit.Id == item.Unit.ID).FirstOrDefault();
                    if (model == null)
                    {
                        var newModel = new BudgetMinPriceUnit();
                        var BudgetMinPriceUnit = new BudgetMinPriceUnitDTO()
                        {
                            Unit = UnitDropdownDTO.CreateFromModel(item.Unit),
                            UnitAmount = item.BudgetMinPriceUnit?.Amount ?? 0.000m,
                        };
                        BudgetMinPriceUnit.ToModel(ref newModel);
                        newModel.BudgetMinPriceID = newID;
                        createBudgetMinPriceUnits.Add(newModel);
                    }
                    else
                    {
                        var newModel = new BudgetMinPriceUnit();
                        model.ToModel(ref newModel);
                        newModel.BudgetMinPriceID = newID;
                        createBudgetMinPriceUnits.Add(newModel);
                    }
                }
                await DB.BudgetMinPriceUnits.AddRangeAsync(createBudgetMinPriceUnits);
                await DB.SaveChangesAsync();
            }
            else
            {

                var quarterly = new BudgetMinPrice();
                inputs.BudgetMinPriceDTO.ToModelQuarterly(ref quarterly);
                quarterly.BudgetMinPriceTypeMasterCenterID = masterCenterQuarterlyID;

                var transfer = new BudgetMinPrice();
                inputs.BudgetMinPriceDTO.ToModelTransfer(ref transfer);
                transfer.BudgetMinPriceTypeMasterCenterID = masterCenterTransferID;


                await DB.BudgetMinPrices.AddAsync(quarterly);
                await DB.BudgetMinPrices.AddAsync(transfer);
                await DB.SaveChangesAsync();
                Guid newID = quarterly.ID;

                IQueryable<BudgetMinPriceUnitQueryResult> query = from unit in DB.Units.Include(o => o.UnitStatus).Where(o => o.ProjectID == inputs.BudgetMinPriceDTO.Project.Id)

                                                                  select new BudgetMinPriceUnitQueryResult
                                                                  {
                                                                      Unit = unit,
                                                                      BudgetMinPriceUnit = new BudgetMinPriceUnit()
                                                                  };
                var queryToList = query.ToList();
                var createBudgetMinPriceUnits = new List<BudgetMinPriceUnit>();

                foreach (var item in queryToList)
                {
                    var model = inputs.BudgetMinPriceUnitDTO.Where(o => o.Unit.Id == item.Unit.ID).FirstOrDefault();
                    if (model == null)
                    {
                        var newModel = new BudgetMinPriceUnit();
                        //var BudgetMinPriceUnit = BudgetMinPriceUnitDTO.CreateFromModel(item.BudgetMinPriceUnit);
                        var BudgetMinPriceUnit = new BudgetMinPriceUnitDTO()
                        {
                            Unit = UnitDropdownDTO.CreateFromModel(item.Unit),
                            UnitAmount = item.BudgetMinPriceUnit?.Amount ?? 0.000m,
                        };
                        BudgetMinPriceUnit.ToModel(ref newModel);
                        newModel.BudgetMinPriceID = newID;
                        createBudgetMinPriceUnits.Add(newModel);
                    }
                    else
                    {
                        var newModel = new BudgetMinPriceUnit();
                        model.ToModel(ref newModel);
                        newModel.BudgetMinPriceID = newID;
                        createBudgetMinPriceUnits.Add(newModel);
                    }
                }
                await DB.BudgetMinPriceUnits.AddRangeAsync(createBudgetMinPriceUnits);
                await DB.SaveChangesAsync();
            }
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137948/preview
        /// ถ้าไม่เคยมี budget min price unit ให้สร้างใหม่
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BudgetMinPriceUnitDTO> SaveBudgetMinPriceUnitAsync(BudgetMinPriceFilter filter, BudgetMinPriceUnitDTO input)
        {
            var budgetMinPriceQuarterlyID = await DB.BudgetMinPrices.Where(o => o.ProjectID == filter.ProjectID
                                                           && o.Quarter == filter.Quarter
                                                           && o.Year == filter.Year
                                                           && o.BudgetMinPriceType.Key == BudgetMinPriceTypeKeys.Quarterly
                                                           && o.ActiveDate <= DateTime.Now
                                          ).OrderByDescending(o => o.ActiveDate).Select(o => o.ID).FirstAsync();
            var budgetMinPriceUnits = await DB.BudgetMinPriceUnits.Where(o => o.BudgetMinPriceID == budgetMinPriceQuarterlyID).ToListAsync();
            var model = budgetMinPriceUnits.Where(o => o.UnitID == input.Unit.Id).FirstOrDefault();
            var result = new BudgetMinPriceUnitDTO();
            if (model == null)
            {
                var newModel = new BudgetMinPriceUnit();
                input.ToModel(ref newModel);
                newModel.BudgetMinPriceID = budgetMinPriceQuarterlyID;
                await DB.BudgetMinPriceUnits.AddAsync(newModel);
                result = BudgetMinPriceUnitDTO.CreateFromModel(newModel);
            }
            else
            {
                input.ToModel(ref model);
                model.BudgetMinPriceID = budgetMinPriceQuarterlyID;
                DB.BudgetMinPriceUnits.Update(model);
                result = BudgetMinPriceUnitDTO.CreateFromModel(model);
            }
            await DB.SaveChangesAsync();

            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137949/preview
        /// อัพไฟล์ แล้ว return dto (ยังไม่ต้อง save ลง db) ไม่ต้องทำ paging
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BudgetMinPriceQuarterlyDTO> ImportQuarterlyBudgetAsync(FileDTO input)
        {
            var dt = await this.ConvertExcelToDataTableQuarterly(input);
            /// Valudate Header
            if (dt.Columns.Count != 3)
            {
                throw new Exception("Invalid File Format");
            }
            //Read Excel Model
            var budgetMinPriceQuarterlyExcelModels = new List<BudgetMinPriceQuarterlyExcelModel>();
            foreach (DataRow r in dt.Rows)
            {
                var excelModel = BudgetMinPriceQuarterlyExcelModel.CreateFromDataRow(r, DB);
                budgetMinPriceQuarterlyExcelModels.Add(excelModel);
            }
            var budgetMinPriceQuarterly = new BudgetMinPriceQuarterlyDTO { BudgetMinPrice = new BudgetMinPriceDTO(), Units = new List<BudgetMinPriceUnitDTO>() };
            var header = await this.GetHeaderQuarterly(input);
            var project = await DB.Projects.Where(o => o.ProjectNo == header.ProjectNo).FirstOrDefaultAsync();

            var budgetMinQuarterly = DB.BudgetMinPrices.Where(o => o.ProjectID == project.ID
                                               && o.Quarter == header.Quarter
                                               && o.Year == header.Year
                                               && o.BudgetMinPriceType.Key == BudgetMinPriceTypeKeys.Quarterly
                               ).OrderByDescending(o => o.ActiveDate).FirstOrDefault();

            budgetMinPriceQuarterly.BudgetMinPrice.Project = ProjectDropdownDTO.CreateFromModel(project);
            budgetMinPriceQuarterly.BudgetMinPrice.QuarterlyTotalAmount = header.QuarterlyTotalAmount;
            budgetMinPriceQuarterly.BudgetMinPrice.Quarter = header.Quarter;
            budgetMinPriceQuarterly.BudgetMinPrice.Year = header.Year;

            var allUnit = await DB.Units.Include(o => o.UnitStatus).Where(o => o.ProjectID == project.ID).ToListAsync();
            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0089").FirstAsync();
            if (budgetMinQuarterly != null)
            {
                var budgetMinPriceUnits = await DB.BudgetMinPriceUnits.Where(o => o.BudgetMinPriceID == budgetMinQuarterly.ID).ToListAsync();



                var query = (from unit in allUnit
                             join budgetminpriceunit in budgetMinPriceUnits.DefaultIfEmpty()
                             on unit.ID equals budgetminpriceunit.UnitID into ps
                             select new BudgetMinPriceUnitQueryResult
                             {
                                 Unit = unit,
                                 BudgetMinPriceUnit = ps.FirstOrDefault()
                             }
                            );
                var queryResults = query.OrderBy(o => o.Unit.UnitNo).ToList();

                foreach (var item in budgetMinPriceQuarterlyExcelModels)
                {
                    var unit = allUnit.Where(o => o.UnitNo == item.UnitNo).FirstOrDefault();
                    var oldBudgetMinPriceUnit = queryResults.Where(o => o.Unit.UnitNo == item.UnitNo).FirstOrDefault();
                    BudgetMinPriceUnitDTO budgetMinPriceUnit = new BudgetMinPriceUnitDTO();
                    //budgetMinPriceUnit.Unit = new UnitDropdownDTO { UnitNo = unit.UnitNo};
                    budgetMinPriceUnit.Unit = UnitDropdownDTO.CreateFromModel(unit);
                    budgetMinPriceUnit.UnitAmount = Convert.ToDecimal(item.BudgetAmount) == 0 ? 0 : Convert.ToDecimal(item.BudgetAmount);
                    budgetMinPriceUnit.OldUnitAmount = Convert.ToDecimal(oldBudgetMinPriceUnit?.BudgetMinPriceUnit?.Amount) == 0 ? 0 : Convert.ToDecimal(oldBudgetMinPriceUnit?.BudgetMinPriceUnit?.Amount);
                    budgetMinPriceUnit.UpdatedByUserID = oldBudgetMinPriceUnit.BudgetMinPriceUnit.UpdatedByUserID;

                    if (unit == null)
                    {
                        budgetMinPriceUnit.isCorrected = false;
                        var msg = errMsg.Message.Replace("[field]", item.UnitNo);
                        budgetMinPriceUnit.Remark = msg;
                    }
                    else
                    {
                        budgetMinPriceUnit.isCorrected = true;
                    }
                    budgetMinPriceQuarterly.Units.Add(budgetMinPriceUnit);
                   
                }
                budgetMinPriceQuarterly.BudgetMinPrice.TotalSuccess = budgetMinPriceQuarterly.Units.Where(x => x.isCorrected == true).Count();
                budgetMinPriceQuarterly.BudgetMinPrice.TotalError = budgetMinPriceQuarterly.Units.Where(x => x.isCorrected == false).Count();
            }
            else
            {
                foreach (var item in budgetMinPriceQuarterlyExcelModels)
                {
                    var unit = allUnit.Where(o => o.UnitNo == item.UnitNo).FirstOrDefault();
                    BudgetMinPriceUnitDTO budgetMinPriceUnit = new BudgetMinPriceUnitDTO();
                    //budgetMinPriceUnit.Unit = new UnitDropdownDTO { UnitNo = item.UnitNo };
                    budgetMinPriceUnit.Unit = UnitDropdownDTO.CreateFromModel(unit);
                    budgetMinPriceUnit.UnitAmount = Convert.ToDecimal(item.BudgetAmount) == 0 ? 0 : Convert.ToDecimal(item.BudgetAmount);
                    budgetMinPriceUnit.OldUnitAmount = 0;

                    if (unit == null)
                    {
                        budgetMinPriceUnit.isCorrected = false;
                        var msg = errMsg.Message.Replace("[field]", item.UnitNo);
                        budgetMinPriceUnit.Remark = msg;

                    }
                    else
                    {
                        budgetMinPriceUnit.isCorrected = true;
                    }

                    budgetMinPriceQuarterly.Units.Add(budgetMinPriceUnit);  
                }
                budgetMinPriceQuarterly.BudgetMinPrice.TotalSuccess = budgetMinPriceQuarterly.Units.Where(x => x.isCorrected == true).Count();
                budgetMinPriceQuarterly.BudgetMinPrice.TotalError = budgetMinPriceQuarterly.Units.Where(x => x.isCorrected == false).Count();
            }
            return budgetMinPriceQuarterly;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137949/preview
        /// ส่ง List dto เพื่อ save เข้า db
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ConfirmImportQuarterlyBudgetAsync(BudgetMinPriceQuarterlyDTO input)
        {
            if (input.BudgetMinPrice != null || input.Units != null)
            {
                var chkCorrected = input.Units.Where(x => x.isCorrected == false).ToList();
                if (chkCorrected.Count > 0)
                {
                    ValidateException ex = new ValidateException();
                    foreach (var item in chkCorrected)
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0089").FirstAsync();
                        string desc = item.Unit.UnitNo;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    if (ex.HasError)
                    {
                        throw ex;
                    }
                }

                var newInput = new BudgetMinPriceListDTO
                {
                    BudgetMinPriceDTO = input.BudgetMinPrice,
                    BudgetMinPriceUnitDTO = input.Units
                };
                await SaveBudgetMinPriceUnitListAsync(newInput);
            }
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137950/preview
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<FileDTO> ExportQuarterlyBudgetAsync(BudgetMinPriceFilter filter)
        {
            ExportExcel result = new ExportExcel();
            var budgetMinPriceQuarterly = await DB.BudgetMinPrices.Where(o => o.ProjectID == filter.ProjectID
                                                            && o.Quarter == filter.Quarter
                                                            && o.Year == filter.Year
                                                            && o.BudgetMinPriceType.Key == BudgetMinPriceTypeKeys.Quarterly
                                           //&& o.ActiveDate <= DateTime.Now
                                           ).OrderByDescending(o => o.ActiveDate).FirstAsync();

            var budgetMinPriceUnits = DB.BudgetMinPriceUnits.Include(o => o.UpdatedBy).Where(o => o.BudgetMinPriceID == budgetMinPriceQuarterly.ID).ToList();

            var allUnit = await DB.Units.Include(o => o.UnitStatus).Where(o => o.ProjectID == filter.ProjectID).ToListAsync();

            var query = (from unit in allUnit
                         join budgetminpriceunit in budgetMinPriceUnits.DefaultIfEmpty()
                         on unit.ID equals budgetminpriceunit.UnitID into ps
                         select new BudgetMinPriceUnitQueryResult
                         {
                             Unit = unit,
                             BudgetMinPriceUnit = ps.FirstOrDefault()
                         }
                        );
            var project = await DB.Projects.Where(o => o.ID == filter.ProjectID).FirstAsync();
            var results = query.OrderBy(o => o.Unit.UnitNo).ToList();
            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "QuarterlySaleBudget.xlsx");
            byte[] tmp = File.ReadAllBytes(path);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _unitNoIndex = BudgetMinPriceQuarterlyExcelModel._unitNoIndex + 1;
                int _budgetAmountIndex = BudgetMinPriceQuarterlyExcelModel._budgetAmountIndex + 1;
                int _unitStatusIndex = BudgetMinPriceQuarterlyExcelModel._unitStatusIndex + 1;

                worksheet.Cells[1, 2].Value = budgetMinPriceQuarterly.Project.ProjectNo;
                worksheet.Cells[1, 4].Value = budgetMinPriceQuarterly.Project.ProjectNameTH;
                worksheet.Cells[2, 2].Value = budgetMinPriceQuarterly.TotalAmount;
                worksheet.Cells[3, 2].Value = budgetMinPriceQuarterly.Year.ToString();
                worksheet.Cells[4, 2].Value = budgetMinPriceQuarterly.Quarter.ToString();

                for (int c = 7; c < results.Count + 7; c++)
                {
                    worksheet.Cells[c, _unitNoIndex].Value = results[c - 7].Unit?.UnitNo;
                    worksheet.Cells[c, _budgetAmountIndex].Value = results[c - 7].BudgetMinPriceUnit?.Amount ?? 0;
                    worksheet.Cells[c, _unitStatusIndex].Value = results[c - 7].Unit?.UnitStatus?.Name;
                }

                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = "QuarterlySaleBudget_" + project.ProjectNo + "_" + filter.Year + "_" + filter.Quarter + ".xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }

            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = $"{result.FileName}_{Guid.NewGuid()}";
            string contentType = result.FileType;
            string filePath = $"budget-minprices/";

            var uploadResult = await this.FileHelper.UploadFileFromStream(fileStream, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = uploadResult.Name,
                Url = uploadResult.Url
            };
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137951/preview
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BudgetMinPriceTransferDTO> ImportTransferBudgetAsync(FileDTO input)
        {
            var dt = await this.ConvertExcelToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 6)
            {
                throw new Exception("Invalid File Format");
            }
            //Read Excel Model
            var budgetMinPriceTransferExcelModels = new List<BudgetMinPriceTransferExcelModel>();
            foreach (DataRow r in dt.Rows)
            {
                var excelModel = BudgetMinPriceTransferExcelModel.CreateFromDataRow(r, DB);
                budgetMinPriceTransferExcelModels.Add(excelModel);
            }
            var budgetMinPriceTransfers = new BudgetMinPriceTransferDTO { BudgetMinPrices = new List<BudgetMinPriceDTO>(), TotalAmount = 0 };
            var allProject = await DB.Projects.ToListAsync();
            var temp = await DB.BudgetMinPrices
                        .Include(o => o.BudgetMinPriceType)
                        .Select(o => new BudgetMinPriceQueryResult
                        {
                            BudgetMinPrice = o,
                            Project = o.Project,
                        })
                        .ToListAsync();
            var queryResults = temp.GroupBy(o => new { o.Project, o.BudgetMinPrice.Year, o.BudgetMinPrice.Quarter }).Select(o => new BudgetMinPriceQueryResult
            {
                Project = o.Key.Project,
                BudgetMinPrice = o.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
                BudgetMinPriceQuarterly = o.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now && p.BudgetMinPrice.BudgetMinPriceType?.Key == BudgetMinPriceTypeKeys.Quarterly).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
                BudgetMinPriceTransfer = o.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now && p.BudgetMinPrice.BudgetMinPriceType?.Key == BudgetMinPriceTypeKeys.TransferPromotion).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
            }).OrderBy(o => o.Project.ProjectNo).ThenBy(o => o.BudgetMinPrice.Year).ThenBy(o => o.BudgetMinPrice.Quarter).ToList();
            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0089").FirstAsync();
            foreach (var item in budgetMinPriceTransferExcelModels)
            {
                BudgetMinPriceDTO budgetMinPrice = new BudgetMinPriceDTO();
                var project = allProject.Where(o => o.ProjectNo == item.ProjectNo).FirstOrDefault();
                if (project != null)
                {
                    var oldbudgetminprice = queryResults.Where(o => o.BudgetMinPrice.Year == item.Year && o.BudgetMinPrice.Quarter == item.Quarter && o.BudgetMinPrice.ProjectID == project.ID).FirstOrDefault();
                    budgetMinPrice.OldTransferTotalAmount = oldbudgetminprice?.BudgetMinPriceTransfer?.TotalAmount ?? 0;
                    budgetMinPrice.OldTransferTotalUnit = oldbudgetminprice?.BudgetMinPriceTransfer?.UnitAmount ?? 0;
                }
                else
                {
                    budgetMinPrice.OldTransferTotalAmount = 0;
                    budgetMinPrice.OldTransferTotalUnit = 0;
                }
                budgetMinPrice.Project = ProjectDropdownDTO.CreateFromModel(project);
                budgetMinPrice.Year = item.Year;
                budgetMinPrice.Quarter = item.Quarter;
                budgetMinPrice.TransferTotalAmount = Convert.ToDecimal(item.TransferTotalAmount) == 0 ? 0 : Convert.ToDecimal(item.TransferTotalAmount);
                budgetMinPrice.TransferTotalUnit = Convert.ToDecimal(item.TransferTotalUnit) == 0 ? 0 : Convert.ToDecimal(item.TransferTotalUnit);

                if (project == null
                   || budgetMinPrice.Year == 0
                   || budgetMinPrice.Quarter == 0
                   || budgetMinPrice.TransferTotalUnit < budgetMinPrice.TransferTotalUnit
                   )
                {
                    budgetMinPrice.isCorrected = false;
                    var msg = errMsg.Message.Replace("[field]", item.ProjectNo + "-" + item.ProjectName);
                    budgetMinPrice.Remark = msg;
                }
                else
                {
                    budgetMinPrice.isCorrected = true;
                }

                budgetMinPriceTransfers.BudgetMinPrices.Add(budgetMinPrice);
            }
            budgetMinPriceTransfers.TotalAmount = budgetMinPriceTransfers.BudgetMinPrices.Sum(o => o.TransferTotalAmount);
            budgetMinPriceTransfers.TotalSuccess = budgetMinPriceTransfers.BudgetMinPrices.Where(x => x.isCorrected == true).Count();
            budgetMinPriceTransfers.TotalError = budgetMinPriceTransfers.BudgetMinPrices.Where(x => x.isCorrected == false).Count();
            return budgetMinPriceTransfers;
        }


        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137951/preview
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public async Task ConfirmImportTransferBudgetAsync(BudgetMinPriceTransferDTO inputs)
        {
            var masterCenterTransferID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType && o.Key == BudgetMinPriceTypeKeys.TransferPromotion).Select(o => o.ID).FirstAsync();
            var allproject = await DB.Projects.ToListAsync();
            var chkCorrected = inputs.BudgetMinPrices.Where(x => x.isCorrected == false).ToList();

            if (chkCorrected.Count > 0)
            {
                ValidateException ex = new ValidateException();
                foreach (var item in chkCorrected)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0089").FirstAsync();
                    string desc = item.Project.ProjectNo + "-" + item.Project.ProjectNameTH;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                if (ex.HasError)
                {
                    throw ex;
                }
            }

            var budgetMinPrices = new List<BudgetMinPrice>();
            foreach (var item in inputs.BudgetMinPrices)
            {
                var project = allproject.Where(o => o.ProjectNo == item.Project.ProjectNo).FirstOrDefault();
                if (project != null)
                {
                    var model = new BudgetMinPrice();
                    model.ProjectID = project.ID;
                    model.TotalAmount = item.TransferTotalAmount;
                    model.UnitAmount = item.TransferTotalUnit;
                    model.Quarter = item.Quarter;
                    model.Year = item.Year;
                    model.ActiveDate = DateTime.Now;
                    model.BudgetMinPriceTypeMasterCenterID = masterCenterTransferID;
                    budgetMinPrices.Add(model);
                }
            }
            await DB.BudgetMinPrices.AddRangeAsync(budgetMinPrices);
            await DB.SaveChangesAsync();
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137952/preview
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<FileDTO> ExportTransferBudgetAsync(BudgetMinPriceFilter filter)
        {
            ExportExcel result = new ExportExcel();
            var temp = await DB.BudgetMinPrices
                                    .Include(o => o.BudgetMinPriceType)
                                    .Select(o => new BudgetMinPriceQueryResult
                                    {
                                        BudgetMinPrice = o,
                                        Project = o.Project,
                                    })
                                    .ToListAsync();
            #region Filter
            if (filter.ProjectID != null && filter.ProjectID != Guid.Empty)
            {
                temp = temp.Where(o => o.Project.ID == filter.ProjectID).ToList();
            }
            if (filter.Year != null)
            {
                temp = temp.Where(o => o.BudgetMinPrice.Year == filter.Year).ToList();
            }
            if (filter.Quarter != null)
            {
                temp = temp.Where(o => o.BudgetMinPrice.Quarter == filter.Quarter).ToList();
            }
            #endregion

            var query = temp.GroupBy(o => new { o.Project, o.BudgetMinPrice.Year, o.BudgetMinPrice.Quarter }).Select(o => new BudgetMinPriceQueryResult
            {
                Project = o.Key.Project,
                BudgetMinPrice = o.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
                BudgetMinPriceQuarterly = o.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now && p.BudgetMinPrice.BudgetMinPriceType?.Key == BudgetMinPriceTypeKeys.Quarterly).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
                BudgetMinPriceTransfer = o.Where(p => p.BudgetMinPrice.ActiveDate <= DateTime.Now && p.BudgetMinPrice.BudgetMinPriceType?.Key == BudgetMinPriceTypeKeys.TransferPromotion).OrderByDescending(p => p.BudgetMinPrice.ActiveDate).Select(p => p.BudgetMinPrice).FirstOrDefault(),
            }).OrderBy(o => o.Project.ProjectNo).ThenBy(o => o.BudgetMinPrice.Year).ThenBy(o => o.BudgetMinPrice.Quarter).ToList();


            var results = query.Select(o => BudgetMinPriceDTO.CreateFromQueryResult(o)).ToList();

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "TransferSaleBudget.xlsx");
            byte[] tmp = File.ReadAllBytes(path);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _projectNoIndex = BudgetMinPriceTransferExcelModel._projectNoIndex + 1;
                int _projectNameIndex = BudgetMinPriceTransferExcelModel._projectNameIndex + 1;
                int _yearIndex = BudgetMinPriceTransferExcelModel._yearIndex + 1;
                int _quarterIndex = BudgetMinPriceTransferExcelModel._quarterIndex + 1;
                int _transferTotalAmountIndex = BudgetMinPriceTransferExcelModel._transferTotalAmountIndex + 1;
                int _transferTotalUnitIndex = BudgetMinPriceTransferExcelModel._transferTotalUnitIndex + 1;

                for (int c = 2; c < results.Count + 2; c++)
                {
                    worksheet.Cells[c, _projectNoIndex].Value = results[c - 2].Project?.ProjectNo;
                    worksheet.Cells[c, _projectNameIndex].Value = results[c - 2].Project?.ProjectNameTH;
                    worksheet.Cells[c, _yearIndex].Value = results[c - 2].Year;
                    worksheet.Cells[c, _quarterIndex].Value = results[c - 2].Quarter;
                    worksheet.Cells[c, _transferTotalAmountIndex].Value = results[c - 2].TransferTotalAmount;
                    worksheet.Cells[c, _transferTotalUnitIndex].Value = results[c - 2].TransferTotalUnit;
                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                if (filter.ProjectID == null)
                {
                    result.FileName = "TransferSaleBudget_0_" + filter.Year + "_" + filter.Quarter + ".xlsx";
                }
                else
                {
                    var project = await DB.Projects.Where(o => o.ID == filter.ProjectID).FirstAsync();
                    result.FileName = "TransferSaleBudget_" + project.ProjectNo + "_" + filter.Year + "_" + filter.Quarter + ".xlsx";
                }
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }

            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = $"{Guid.NewGuid()}_{result.FileName}";
            string contentType = result.FileType;
            string filePath = $"budget-minprices/";

            var uploadResult = await this.FileHelper.UploadFileFromStream(fileStream, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = uploadResult.Name,
                Url = uploadResult.Url
            };
        }

        private async Task<DataTable> ConvertExcelToDataTable(FileDTO input)
        {
            try
            {
                var excelStream = await FileHelper.GetStreamFromUrlAsync(input.Url);
                string fileName = input.Name;
                var fileExtention = fileName != null ? fileName.Split('.').ToList().Last() : null;

                bool hasHeader = true;
                using (Stream stream = new MemoryStream(XLSToXLSXConverter.ReadFully(excelStream)))
                {
                    byte[] excelByte;
                    if (fileExtention.ToLower() == "xls")
                    {
                        excelByte = XLSToXLSXConverter.Convert(stream);
                    }
                    else
                    {
                        excelByte = XLSToXLSXConverter.ReadFully(stream);
                    }
                    using (System.IO.MemoryStream xlsxStream = new System.IO.MemoryStream(excelByte))
                    using (var pck = new OfficeOpenXml.ExcelPackage(xlsxStream))
                    {
                        var ws = pck.Workbook.Worksheets.First();


                        DataTable tbl = new DataTable();
                        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                        {
                            tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                        }
                        var startRow = hasHeader ? 2 : 1;
                        for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                        {
                            var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                            DataRow row = tbl.Rows.Add();
                            foreach (var cell in wsRow)
                            {
                                row[cell.Start.Column - 1] = cell.Text;
                            }
                        }

                        return tbl;
                    }
                }
            }
            catch
            {
                throw new Exception("Invalid File Format");
            }
        }



        private async Task<DataTable> ConvertExcelToDataTableQuarterly(FileDTO input)
        {
            var excelStream = await FileHelper.GetStreamFromUrlAsync(input.Url);
            string fileName = input.Name;
            var fileExtention = fileName != null ? fileName.Split('.').ToList().Last() : null;

            bool hasHeader = true;
            using (Stream stream = new MemoryStream(XLSToXLSXConverter.ReadFully(excelStream)))
            {
                byte[] excelByte;
                if (fileExtention.ToLower() == "xls")
                {
                    excelByte = XLSToXLSXConverter.Convert(stream);
                }
                else
                {
                    excelByte = XLSToXLSXConverter.ReadFully(stream);
                }
                using (System.IO.MemoryStream xlsxStream = new System.IO.MemoryStream(excelByte))
                using (var pck = new OfficeOpenXml.ExcelPackage(xlsxStream))
                {
                    var ws = pck.Workbook.Worksheets.First();
                    DataTable tbl = new DataTable();
                    foreach (var firstRowCell in ws.Cells[6, 1, 6, ws.Dimension.End.Column])
                    {
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                    }
                    var startRow = hasHeader ? 7 : 6;


                    ValidateException ex = new ValidateException();
                    BudgetMinPriceDTO MsgDTO = new BudgetMinPriceDTO();
                    int Quarter;
                    if (!int.TryParse(ws.Cells[3, 2].Text, out Quarter))
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0100").FirstAsync();
                        string desc = MsgDTO.GetType().GetProperty(nameof(BudgetMinPriceDTO.Quarter)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[message]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    else if (Quarter >= 1 || Quarter >= 4)
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0100").FirstAsync();
                        string desc = MsgDTO.GetType().GetProperty(nameof(BudgetMinPriceDTO.Quarter)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[message]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }


                    int Year;
                    if (!int.TryParse(ws.Cells[3, 2].Text, out Year))
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0101").FirstAsync();
                        ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                    }
                    else if (Year < 2000 || Year > 2300)
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0101").FirstAsync();
                        ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                    }

                    if (ex.HasError)
                    {
                        throw ex;
                    }


                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        DataRow row = tbl.Rows.Add();
                        foreach (var cell in wsRow)
                        {

                            row[cell.Start.Column - 1] = cell.Text;
                        }
                    }

                    return tbl;
                }
            }
        }

        private async Task<HeaderQuarterlyExcelModel> GetHeaderQuarterly(FileDTO input)
        {
            var excelStream = await FileHelper.GetStreamFromUrlAsync(input.Url);
            string fileName = input.Name;
            var fileExtention = fileName != null ? fileName.Split('.').ToList().Last() : null;
            BudgetMinPriceDTO MsgDTO = new BudgetMinPriceDTO();
            using (Stream stream = new MemoryStream(XLSToXLSXConverter.ReadFully(excelStream)))
            {
                byte[] excelByte;
                if (fileExtention.ToLower() == "xls")
                {
                    excelByte = XLSToXLSXConverter.Convert(stream);
                }
                else
                {
                    excelByte = XLSToXLSXConverter.ReadFully(stream);
                }
                using (System.IO.MemoryStream xlsxStream = new System.IO.MemoryStream(excelByte))
                using (var pck = new OfficeOpenXml.ExcelPackage(xlsxStream))
                {
                    var ws = pck.Workbook.Worksheets.First();
                    HeaderQuarterlyExcelModel headerModel = new HeaderQuarterlyExcelModel();
                    headerModel.ProjectNo = ws.Cells[1, 2].Text;
                    ValidateException ex = new ValidateException();


                    headerModel.QuarterlyTotalAmount = Convert.ToDecimal(ws.Cells[2, 2].Text);
                    if (headerModel.QuarterlyTotalAmount <= 0)
                    {
                        var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0102").FirstOrDefault();
                        ex.AddError(errMsg.Key, errMsg.Message + "- QuarterlyTotalAmount ", (int)errMsg.Type);
                        throw ex;
                    }
                    int quarter;
                    if (int.TryParse(ws.Cells[4, 2].Text, out quarter))
                    {
                        if (quarter >= 1 || quarter <= 4)
                        {
                            var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0100").FirstOrDefault();
                            string desc = MsgDTO.GetType().GetProperty(nameof(BudgetMinPriceDTO.Quarter)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[message]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                        else
                        {
                            headerModel.Quarter = quarter;
                        }

                    }
                    else
                    {
                        var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0101").FirstOrDefault();
                        ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                    }
                    int year;
                    if (int.TryParse(ws.Cells[3, 2].Text, out year))
                    {
                        headerModel.Year = year;
                    }
                    else if (year < 2000 || year > 2300)
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0101").FirstAsync();
                        ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                    }

                    if (ex.HasError)
                    {
                        throw ex;
                    }
                    return headerModel;
                }
            }
        }


        private async Task<decimal> GetOldUsedAmount(Guid budgetMinPriceID)
        {
            var usedAmount = await DB.BudgetMinPrices.Where(o => o.ID == budgetMinPriceID).Select(o => o.UsedAmount).FirstAsync();

            return usedAmount;
        }

    }
}
