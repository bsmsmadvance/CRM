using Base.DTOs.SAL;
using Database.Models;
using PagingExtensions;
using Sale.Params.Filters;
using Sale.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using Database.Models.FIN;

namespace Sale.Services
{
    public class UnitInfoService : IUnitInfoService
    {
        private readonly DatabaseContext DB;

        public UnitInfoService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<UnitInfoListPaging> GetUnitInfoListAsync(UnitInfoListFilter filter, PageParam pageParam, UnitInfoListSortByParam sortByParam)
        {
            var query = from u in DB.Units
                        .Include(o => o.Project)
                        .Include(o => o.UnitStatus)

                        join b in DB.Bookings on u.ID equals b.UnitID into bData
                        from bModel in bData.DefaultIfEmpty()

                        join blc in DB.Users on bModel.CreatedByUserID equals blc.ID into blcData
                        from blcModel in blcData.DefaultIfEmpty()

                        join ag in DB.Agreements on bModel.ID equals ag.UnitID into agData
                        from agModel in agData.DefaultIfEmpty()

                        join agc in DB.Users on agModel.CreatedByUserID equals agc.ID into agcData
                        from agcModel in agcData.DefaultIfEmpty()

                        join tf in DB.Transfers on agModel.ID equals tf.AgreementID into tfData
                        from tfModel in tfData.DefaultIfEmpty()

                        join bo in DB.BookingOwners on bModel.ID equals bo.BookingID into boData
                        from boModel in boData.Where(o => o.IsMainOwner == true).DefaultIfEmpty()

                        select new UnitInfoListQueryResult
                        {
                            Unit = u,
                            Project = u.Project,
                            UnitStatus = u.UnitStatus,
                            Booking = bModel,
                            Agreement = agModel,
                            Transfer = tfModel,
                            BookingOwner = boModel,
                            LCOwner = agcModel ?? blcModel
                        };

            #region Filter
            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(q => q.Unit.UnitNo.Contains(filter.UnitNo));
            }

            if (filter.TowerID != null)
            {
                query = query.Where(q => q.Unit.TowerID == filter.TowerID);
            }

            if (!string.IsNullOrEmpty(filter.HouseNo))
            {
                query = query.Where(q => q.Unit.HouseNo.Contains(filter.HouseNo));
            }

            if (!string.IsNullOrEmpty(filter.FullName))
            {
                query = query.Where(q => string.Format("{0}{1}", q.BookingOwner.FirstNameTH, q.BookingOwner.LastNameTH).Contains(filter.FullName.Trim()));
            }

            if (filter.ContactID != null)
            {
                query = query.Where(q => q.BookingOwner.FromContactID == filter.ContactID);
            }

            if (filter.ProjectID != null)
            {
                query = query.Where(q => q.Unit.ProjectID == filter.ProjectID);
            }

            if (!string.IsNullOrEmpty(filter.BookingNo))
            {
                query = query.Where(q => q.Booking.BookingNo.Contains(filter.BookingNo));
            }

            if (!string.IsNullOrEmpty(filter.UnitStatusKeys))
            {
                var keys = filter.UnitStatusKeys.Split(',').ToList();
                var topicMasterCenterIDs = await DB.MasterCenters
                    .Where(x => keys.Contains(x.Key) && x.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus)
                    .Select(x => x.ID).ToListAsync();
                query = query.Where(q => topicMasterCenterIDs.Contains(q.Unit.UnitStatusMasterCenterID ?? Guid.Empty));
            }

            #endregion Filter

            UnitInfoListDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(async o => await UnitInfoListDTO.CreateFromQueryResultAsync(o, DB)).Select(o => o.Result).ToList();

            return new UnitInfoListPaging()
            {
                PageOutput = pageOutput,
                Units = results
            };
        }

        public async Task<UnitInfoDTO> GetUnitInfoAsync(Guid unitID)
        {
            var query = await DB.Units.Include(o => o.UnitStatus)
                                      .Include(o => o.Project)
                                        .ThenInclude(o => o.ProductType)
                              .GroupJoin(DB.Bookings.Include(o => o.BookingStatus), units => units.ID, bookings => bookings.UnitID, (units, bookings) => new { Unit = units, Booking = bookings })
                              .Where(o => o.Unit.ID == unitID)
                              .Select(o => new UnitInfoQueryResult
                              {
                                  Project = o.Unit.Project,
                                  Unit = o.Unit,
                                  Booking = o.Booking.FirstOrDefault() ?? new Booking()
                              }).FirstOrDefaultAsync();

            var result = await UnitInfoDTO.CreateFromQueryResultAsync(query, DB);
            return result;
        }

        public async Task<UnitInfoBookingPromotionDTO> GetUnitInfoBookingPromotionAsync(Guid unitID)
        {
            var booking = await DB.Bookings.Where(o => o.UnitID == unitID).FirstOrDefaultAsync();
            if (booking != null)
            {
                var model = await DB.BookingPromotions
                .Include(o => o.MasterPromotion)
                .Include(o => o.UpdatedBy)
                .Where(o => o.BookingID == booking.ID && o.IsActive == true).FirstOrDefaultAsync();

                var result = await UnitInfoBookingPromotionDTO.CreateFromModelAsync(model, DB);
                return result;
            }
            else
            {
                var result = await UnitInfoBookingPromotionDTO.CreateFromUnitAsync(unitID, DB);
                return result;
            }
        }

        public async Task<List<UnitInfoPromotionExpenseDTO>> GetUnitInfoPromotionExpensesAsync(Guid unitID)
        {
            var booking = await DB.Bookings.Where(o => o.UnitID == unitID).FirstOrDefaultAsync();
            if (booking != null)
            {
                var promotion = await DB.BookingPromotions.Where(o => o.BookingID == booking.ID).FirstAsync();
                var unitPrice = await DB.UnitPrices.Where(o => o.BookingID == booking.ID).Select(o => o.ID).FirstAsync();

                var models = await DB.BookingPromotionExpenses
                    .Include(o => o.MasterPriceItem)
                    .Include(o => o.ExpenseReponsibleBy)
                    .Include(o => o.BookingPromotion)
                    .Where(o => o.BookingPromotionID == promotion.ID)
                    .ToListAsync();

                var results = models.Select(async o => await UnitInfoPromotionExpenseDTO.CreateFromModelAsync(o, DB))
                                    .Select(o => o.Result)
                                    .OrderBy(o => o.Order)
                                    .ToList();
                return results;
            }
            else
            {
                var result = await UnitInfoPromotionExpenseDTO.CreateDraftFromUnitAsync(unitID, DB);

                return result;
            }

        }

        public async Task<UnitInfoPreSalePromotionDTO> GetUnitInfoPreSalePromotionAsync(Guid unitID)
        {
            var booking = await DB.Bookings.Where(o => o.UnitID == unitID).FirstOrDefaultAsync();
            if (booking != null)
            {
                var model = await DB.PreSalePromotions
                .Include(o => o.MasterPromotion)
                .Include(o => o.UpdatedBy)
                .Where(o => o.BookingID == booking.ID).FirstOrDefaultAsync();

                var result = await UnitInfoPreSalePromotionDTO.CreateFromModelAsync(model, DB);

                return result;
            }
            else
            {
                var result = await UnitInfoPreSalePromotionDTO.CreateFromUnitAsync(unitID, DB);

                return result;
            }
        }

        public async Task<UnitInfoPriceListDTO> GetPriceListAsync(Guid unitID)
        {
            var booking = await DB.Bookings.Where(o => o.UnitID == unitID).FirstOrDefaultAsync();
            if (booking != null)
            {
                var unitPrice = await DB.UnitPrices
                                    .Include(o => o.Booking)
                                    .ThenInclude(o => o.ReferContact)
                                    .Where(o => o.BookingID == booking.ID)
                                    .FirstOrDefaultAsync();

                var result = await UnitInfoPriceListDTO.CreateFromModelAsync(unitPrice, DB);
                return result;
            }
            else
            {
                var result = await UnitInfoPriceListDTO.CreateDraftFromUnitAsync(unitID, DB);
                return result;
            }
        }

        public async Task<UnitInfoStatusCountDTO> GetUnitInfoCountAsync(Guid? projectID)
        {

            var result = new UnitInfoStatusCountDTO();

            if (projectID != null)
            {
                var query = DB.Units
                .Include(o => o.Project)
                .Include(o => o.UnitStatus)
                .Where(o => o.Project.IsActive == true);

                query = query.Where(o => o.ProjectID == projectID);

                result = new UnitInfoStatusCountDTO
                {
                    All = await query.CountAsync(),
                    Available = await query.Where(o => o.UnitStatus.Key == UnitStatusKeys.Available).CountAsync(),
                    WaitingForConfirmBooking = await query.Where(o => o.UnitStatus.Key == UnitStatusKeys.WaitingForConfirmBooking).CountAsync(),
                    WaitingForAgreement = await query.Where(o => o.UnitStatus.Key == UnitStatusKeys.WaitingForAgreement).CountAsync(),
                    WaitingForTransfer = await query.Where(o => o.UnitStatus.Key == UnitStatusKeys.WaitingForTransfer).CountAsync(),
                    Transfer = await query.Where(o => o.UnitStatus.Key == UnitStatusKeys.Transfer).CountAsync(),
                    PreTransfer = await query.Where(o => o.UnitStatus.Key == UnitStatusKeys.PreTransfer).CountAsync()
                };
            }

            return result;
        }

        public async Task<UnitInfoSumPaymentDTO> GetUnitInfoPaymentAsync(Guid unitID)
        {
            var result = new UnitInfoSumPaymentDTO();

            if (unitID != null)
            {
                var query = from o in DB.Units

                            join b in DB.Bookings on o.ID equals b.UnitID into bData
                            from bModel in bData.DefaultIfEmpty()

                            join p in DB.Payments on bModel.ID equals p.BookingID into tfData
                            from pModel in tfData.DefaultIfEmpty()

                            select new UnitInfoSumPaymentQueryResult
                            {
                                Unit = o,
                                Booking = bModel ?? new Booking(),
                                Payment = pModel ?? new Payment(),
                            };

                query = query.Where(e => e.Unit.ID == unitID);

                var Booking = query.Select(e => e.Booking).FirstOrDefault() ?? new Booking();

                result = new UnitInfoSumPaymentDTO
                {
                    SumPayment = await query.Select(e => e.Payment.TotalAmount).SumAsync(),

                    SumOverdueBalance = 0,
                    SumOverduePeriod = 0,

                    OtherPayment = await DB.UnknownPayments.Where(e => e.BookingID == Booking.ID).Select(e => e.Amount).SumAsync()
                };

            }

            return result;
        }
    }
}
