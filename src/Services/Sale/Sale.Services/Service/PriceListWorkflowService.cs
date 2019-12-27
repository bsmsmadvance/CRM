using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using Database.Models.SAL;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using Sale.Params.Filters;
using Sale.Params.Inputs;
using Sale.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Services
{
    public class PriceListWorkflowService : IPriceListWorkflowService
    {
        private readonly DatabaseContext DB;

        public PriceListWorkflowService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<PriceListWorkflow> CreatePriceListWorkflowAsync(Guid quotationID, Guid bookingID, Guid priceListWorkflowStageMasterCenterID)
        {
            var booking = await DB.Bookings.Where(o => o.ID == bookingID).FirstAsync();

            #region InitialDataFromQuotation
            var quotation = await DB.Quotations.Where(o => o.ID == quotationID).Include(o => o.Unit).ThenInclude(o => o.UnitStatus).FirstOrDefaultAsync();
            var quotationUnitPrice = await DB.QuotationUnitPrices.Where(o => o.QuotationID == quotationID).FirstOrDefaultAsync();
            var quotationUnitPriceItems = await DB.QuotationUnitPriceItems.Where(o => o.QuotationUnitPriceID == quotationUnitPrice.ID).ToListAsync();

            var quotationSellingPrice = quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Amount).FirstOrDefault();
            var quotationNetSellingPrice = quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Amount).FirstOrDefault();
            var quotationBookingAmount = quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Amount).FirstOrDefault();
            var quotationContractAmount = quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Amount).FirstOrDefault();
            var quotationDownAmount = quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Amount).FirstOrDefault();
            var quotationTransferAmount = quotationNetSellingPrice - quotationBookingAmount - quotationContractAmount - quotationDownAmount;
            var quotationInstallment = quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Installment).FirstOrDefault();
            var quotationInstallmentAmount = quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.InstallmentAmount).FirstOrDefault();
            var quotationInstallmentNormalCount = (quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Installment).FirstOrDefault()
                                             - (quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault() != null
                                               ? quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments.Split(',').ToList().Count()).FirstOrDefault() : 0));
            var quotationSpecialInstallmentAmount = quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault();

            var quotationSpecialInstallmentCount = (quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault() != null
                                                ? quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments.Split(',').ToList().Count()).FirstOrDefault() : 0);
            var quotationSpecialInstallment = quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault();
            #endregion

            #region InitialDataFromMasterPriceList
            var priceList = await DB.PriceLists.Where(o => o.ID == quotationUnitPrice.FromPriceListID).FirstOrDefaultAsync();
            var priceListItems = await DB.PriceListItems.Where(o => o.PriceListID == priceList.ID).Include(o => o.MasterPriceItem).ToListAsync();

            var masterSellingPrice = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Amount).FirstOrDefault();
            var masterNetSellingPrice = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Amount).FirstOrDefault();
            var masterBookingAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Amount).FirstOrDefault();
            var masterContractAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Amount).FirstOrDefault();
            var masterDownAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Amount).FirstOrDefault();
            var masterTransferAmount = masterNetSellingPrice - masterBookingAmount - masterContractAmount - masterDownAmount;
            var masterInstallment = priceListItems.Where(o => o.MasterPriceItem.ID == MasterPriceItemKeys.DownAmount).Select(o => o.Installment).FirstOrDefault();
            var masterInstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.InstallmentAmount).FirstOrDefault();
            var masterInstallmentNormalCount = (priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Installment).FirstOrDefault()
                                              - (priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault() != null
                                                ? priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments.Split(',').ToList().Count()).FirstOrDefault() : 0));
            var masterSpecialInstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault();
            var masterSpecialInstallment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault();
            var masterSpecialInstallmentCount = (priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault() != null
                                                ? priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments.Split(',').ToList().Count()).FirstOrDefault() : 0);
            #endregion

            var roleLCM = await DB.Roles.Where(o => o.Code == "LCM").FirstOrDefaultAsync();

            var priceListWorkflow = new PriceListWorkflow()
            {
                ProjectID = quotation.ProjectID,
                UnitID = quotation.UnitID,
                QuotationID = quotation.ID,
                BookingID = booking.ID,
                UnitStatusMasterCenterID = quotation.Unit.UnitStatusMasterCenterID,
                PriceListWorkflowStageMasterCenterID = priceListWorkflowStageMasterCenterID,
                // Master
                MasterSellingPrice = masterSellingPrice,
                MasterBookingAmount = masterBookingAmount,
                MasterContractAmount = masterContractAmount,
                MasterInstallment = masterInstallment,
                MasterNormalInstallment = masterInstallmentNormalCount,
                MasterInstallmentAmount = masterInstallmentAmount,
                MasterSpecialInstallments = masterSpecialInstallment,
                MasterSpecialInstallmentAmounts = masterSpecialInstallmentAmount,
                // Quotation
                SellingPrice = quotationSellingPrice,
                BookingAmount = quotationBookingAmount,
                ContractAmount = quotationContractAmount,
                Installment = quotationInstallment,
                NormalInstallment = quotationInstallmentNormalCount,
                InstallmentAmount = quotationInstallmentAmount,
                SpecialInstallments = quotationSpecialInstallment,
                SpecialInstallmentAmounts = quotationSpecialInstallmentAmount,
                RoleID = roleLCM.ID
            };

            return priceListWorkflow;
        }

        public async Task<PriceListWorkflowPaging> GetPriceListWorkflowListAsync(PriceListWorkflowFilter filter, PageParam pageParam, PriceListWorkflowSortByParam sortByParam)
        {
            IQueryable<PriceListWorkflowQueryResult> query = DB.PriceListWorkflows
                .Include(o => o.UpdatedBy)
                .Include(o => o.Quotation)
                    .ThenInclude(o => o.Unit)
                        .ThenInclude(o => o.UnitStatus)
                .Include(o => o.Quotation)
                    .ThenInclude(o => o.CreatedBy)
                .Where(o => o.IsApproved == null)
                .Select(o => new PriceListWorkflowQueryResult
                {
                    PriceListWorkflow = o,
                    PriceListWorkflowStage = o.PriceListWorkflowStage,
                    Project = o.Project,
                    Unit = o.Unit,
                    UnitStatus = o.UnitStatus,
                    Quotation = o.Quotation,
                    ApprovedBy = o.ApprovedBy,
                    UpdatedBy = o.UpdatedBy
                });


            #region Filter
            if (filter.ProjectID != null && filter.ProjectID != Guid.Empty)
            {
                query = query.Where(o => o.Project.ID == filter.ProjectID);
            }
            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(o => o.Unit.UnitNo.Contains(filter.UnitNo));
            }
            if (!string.IsNullOrEmpty(filter.UnitStatusKey))
            {
                var unitStatusMasterCenterID = await DB.MasterCenters.Where(o => o.Key == filter.UnitStatusKey
                                                                 && o.MasterCenterGroupKey == "UnitStatus")
                                                                .Select(o => o.ID).FirstAsync();
                query = query.Where(o => o.UnitStatus.ID == unitStatusMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.PriceListWorkflowStageKey))
            {
                var priceListWorkflowStageMasterCenterID = await DB.MasterCenters.Where(o => o.Key == filter.PriceListWorkflowStageKey
                                                                 && o.MasterCenterGroupKey == "PriceListWorkflowStage")
                                                                .Select(o => o.ID).FirstAsync();
                query = query.Where(o => o.PriceListWorkflowStage.ID == priceListWorkflowStageMasterCenterID);
            }

            #endregion

            PriceListWorkflowDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<PriceListWorkflowQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => PriceListWorkflowDTO.CreateFromQueryResult(o)).ToList();

            return new PriceListWorkflowPaging()
            {
                PageOutput = pageOutput,
                PriceListWorkflowDTOs = results
            };
        }

        public async Task<PriceListWorkflowDTO> GetPriceListWorkflowAsync(Guid id)
        {
            var model = await DB.PriceListWorkflows.Where(o => o.ID == id)
                                                   .Include(o => o.Project)
                                                   .Include(o => o.Unit)
                                                   .Include(o => o.UnitStatus)
                                                   .Include(o => o.PriceListWorkflowStage)
                                                   .Include(o => o.Quotation)
                                                      .ThenInclude(o => o.Unit)
                                                         .ThenInclude(o => o.UnitStatus)
                                                   .Include(o => o.Quotation)
                                                      .ThenInclude(o => o.CreatedBy)
                                                   .Include(o => o.ApprovedBy)
                                                   .Include(o => o.UpdatedBy)
                                                   .FirstAsync();

            var result = PriceListWorkflowDTO.CreateFromModel(model);

            return result;
        }

        public async Task<PriceListWorkflowDTO> ApproveAsync(Guid id, Guid userID)
        {
            var model = await DB.PriceListWorkflows.Where(o => o.ID == id).Include(o => o.Role).FirstAsync();
            var user = await DB.Users.Where(o => o.ID == userID).FirstAsync();
            var bookingStatusWaitingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus && o.Key == BookingStatusKeys.WaitForBookingConfirmation).Select(o => o.ID).FirstAsync();
            var bookingStatusWaitingApproveUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus && o.Key == BookingStatusKeys.WaitingForApproveUnit).Select(o => o.ID).FirstAsync();
            var booking = await DB.Bookings.Where(o => o.ID == model.BookingID).FirstAsync();
            var unitStatusWatingBookingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus && o.Key == UnitStatusKeys.Available).Select(o => o.ID).FirstAsync();
            var unitStatusWatingConfirmBookingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus && o.Key == UnitStatusKeys.WaitingForConfirmBooking).Select(o => o.ID).FirstAsync();
            var unit = await DB.Units.Where(o => o.ID == booking.UnitID).Include(o => o.UnitStatus).FirstAsync();

            #region DataFromQuotation

            var quotation = await DB.Quotations.Where(o => o.ID == model.QuotationID)
                                               .Include(o => o.Unit)
                                               .Include(o => o.Unit.UnitStatus)
                                               .FirstOrDefaultAsync();
            var quotationUnitPrice = await DB.QuotationUnitPrices.Where(o => o.QuotationID == model.QuotationID).FirstOrDefaultAsync();
            var quotationUnitPriceItems = await DB.QuotationUnitPriceItems.Where(o => o.QuotationUnitPriceID == quotationUnitPrice.ID).ToListAsync();

            #endregion

            #region CreateNewMasterPriceListFromQuotation

            #region PriceList
            PriceList pl = new PriceList();
            pl.UnitID = quotation.UnitID;
            pl.ActiveDate = DateTime.Now;
            //pl.IsUsed = true;

            #endregion

            #region PriceListitems

            List<PriceListItem> priceListItems = new List<PriceListItem>();
            foreach (var item in quotationUnitPriceItems)
            {
                var priceListItem = new PriceListItem
                {
                    PriceListID = pl.ID,
                    Order = item.Order,
                    MasterPriceItemID = item.MasterPriceItemID,
                    Name = item.Name,
                    PriceUnitAmount = item.PriceUnitAmount,
                    PriceUnitMasterCenterID = item.PriceUnitMasterCenterID,
                    PricePerUnitAmount = item.PricePerUnitAmount,
                    Amount = item.Amount,
                    IsToBePay = item.IsToBePay,
                    Installment = item.Installment,
                    InstallmentAmount = item.InstallmentAmount,
                    SpecialInstallmentAmounts = item.SpecialInstallmentAmounts,
                    SpecialInstallments = item.SpecialInstallments,
                    PriceTypeMasterCenterID = item.PriceTypeMasterCenterID
                };
                priceListItems.Add(priceListItem);
            }
            #endregion

            #endregion

            await ValidateUserRoleLCM(userID, model.ID);

            model.IsApproved = true;
            model.ApprovedByUserID = user.ID;
            model.ApprovedTime = DateTime.Now;


            var fromChangeUnit = await CheckChangeUnitWorkFlow(booking.ID);
            if (fromChangeUnit)
            {
                model.UnitStatusMasterCenterID = unit.UnitStatusMasterCenterID;
                DB.Entry(model).State = EntityState.Modified;

                booking.BookingStatusMasterCenterID = bookingStatusWaitingApproveUnitMasterCenterID;
                DB.Entry(booking).State = EntityState.Modified;
            }
            else
            {
                model.UnitStatusMasterCenterID = unitStatusWatingConfirmBookingMasterCenterID;
                DB.Entry(model).State = EntityState.Modified;

                // เปลี่ยนสถานะแปลง
                unit.UnitStatusMasterCenterID = unitStatusWatingConfirmBookingMasterCenterID;
                DB.Entry(unit).State = EntityState.Modified;

                // เปลี่ยนสถานะ Booking
                booking.BookingStatusMasterCenterID = bookingStatusWaitingMasterCenterID;
                DB.Entry(booking).State = EntityState.Modified;
            }
            await DB.PriceLists.AddAsync(pl);
            await DB.PriceListItems.AddRangeAsync(priceListItems);
            await DB.SaveChangesAsync();
            var result = await this.GetPriceListWorkflowAsync(model.ID);

            return result;
        }

        public async Task<PriceListWorkflowDTO> RejectAsync(Guid id, Guid userID, RejectParam input)
        {
            var model = await DB.PriceListWorkflows.Where(o => o.ID == id).FirstAsync();
            var user = await DB.Users.Where(o => o.ID == userID).FirstAsync();
            var booking = await DB.Bookings.Where(o => o.ID == model.BookingID).FirstOrDefaultAsync();
            var unitStatusWatingBookingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus && o.Key == UnitStatusKeys.Available).Select(o => o.ID).FirstAsync();
            var unitStatusWatingConfirmBookingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus && o.Key == UnitStatusKeys.WaitingForConfirmBooking).Select(o => o.ID).FirstAsync();
            var unitStatusAvaliableMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus && o.Key == UnitStatusKeys.Available).Select(o => o.ID).FirstAsync();

            // เปลี่ยนสถานะแปลง
            var unit = await DB.Units.Where(o => o.ID == model.UnitID).FirstAsync();
            if (unit.UnitStatusMasterCenterID == unitStatusWatingConfirmBookingMasterCenterID)
            {
                unit.UnitStatusMasterCenterID = unitStatusWatingBookingMasterCenterID;
                DB.Entry(unit).State = EntityState.Modified;
            }

            model.RejectComment = input.Comment;
            model.IsApproved = false;
            model.ApprovedByUserID = user.ID;
            model.ApprovedTime = DateTime.Now;

            var fromChangeUnit = await CheckChangeUnitWorkFlow(booking.ID);
            if (fromChangeUnit)
            {
                unit.UnitStatusMasterCenterID = unitStatusAvaliableMasterCenterID;
                DB.Entry(unit).State = EntityState.Modified;

                booking.IsDeleted = true;
                DB.Entry(booking).State = EntityState.Modified;
            }
            else
            {
                model.UnitStatusMasterCenterID = unitStatusWatingBookingMasterCenterID;
                DB.Entry(model).State = EntityState.Modified;

                booking.IsDeleted = true;
                DB.Entry(booking).State = EntityState.Modified;
            }

            await DB.SaveChangesAsync();
            var result = await this.GetPriceListWorkflowAsync(model.ID);

            return result;
        }

        private async Task ValidateUserRoleLCM(Guid userID, Guid priceListWorkFlowID)
        {
            ValidateException ex = new ValidateException();
            var user = await DB.Users.Where(o => o.ID == userID).FirstOrDefaultAsync();
            var priceListWorkflow = await DB.PriceListWorkflows.Where(o => o.ID == priceListWorkFlowID).Include(o => o.Role).FirstOrDefaultAsync();
            var userRoles = await DB.UserRoles.Where(o => o.UserID == user.ID).Include(o => o.Role).ToListAsync();
            //validate unique
            if (userRoles.Where(o => o.Role.Code == priceListWorkflow.Role.Code).Count() == 0)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR9999").FirstAsync();
                string desc = "เฉพาะ Role LCM";
                var msg = errMsg.Message.Replace("[message]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        private async Task<bool> CheckChangeUnitWorkFlow(Guid bookingID)
        {
            var changeUnitWorkflow = await DB.ChangeUnitWorkflows.Where(o => o.ToBookingID == bookingID).FirstOrDefaultAsync();
            return changeUnitWorkflow != null ? true : false;
        }
    }
}
