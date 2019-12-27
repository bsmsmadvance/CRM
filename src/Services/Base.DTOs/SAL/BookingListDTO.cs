using Base.DTOs.SAL.Sortings;
using Database.Models;
using Database.Models.MasterKeys;
using System;
using System.Collections.Generic;
using System.Linq;
using models = Database.Models;

namespace Base.DTOs.SAL
{
    public class BookingListDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ใบจอง
        /// </summary>
        public string BookingNo { get; set; }

        /// <summary>
        /// แปลง
        ///  Project/api/Projects/{projectID}/Units/DropdownList
        /// </summary>
        public PRJ.UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// ผู้จอง
        ///  Sale/api/Bookings/{bookingID}/BookingOwners/DropdownList
        /// </summary>
        public BookingOwnerDropdownDTO Owner { get; set; }

        /// <summary>
        /// วันที่จอง
        /// </summary>
        public DateTime? BookingDate { get; set; }

        /// <summary>
        /// วันที่อนุมัติ
        /// </summary>
        public DateTime? ApproveDate { get; set; }

        /// <summary>
        /// วันที่ทำสัญญา
        /// </summary>
        public DateTime? ContractDate { get; set; }

        /// <summary>
        /// สถานะ Booking
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=BookingStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO BookingStatus { get; set; }

        /// <summary>
        /// ลบ
        /// </summary>
        public bool? CanDelete { get; set; }

        /// <summary>
        /// สร้างใบจองจากระบบไหน
        /// </summary>
        public MST.MasterCenterDropdownDTO CreateBookingFrom { get; set; }

        /// <summary>
        /// ยืนยันโดย
        /// </summary>
        public USR.UserListDTO ConfirmBy { get; set; }

        /// <summary>
        /// วันที่ยืนยัน
        /// </summary>
        public DateTime? ConfirmDate { get; set; }

        /// <summary>
        /// ยกเลิกใบจอง
        /// </summary>
        public bool? IsCancelled { get; set; }

        /// <summary>
        /// ราคาขายสุทธิหน้าสัญญา (จำนวนเงิน)
        /// </summary>
        public decimal SellingPrice { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จรับเงินชั่วคราว
        /// </summary>
        public string ReceiptTempNo { get; set; }

        public static BookingListDTO CreateFromQueryResult(BookingQueryResult model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new BookingListDTO()
                {
                    Id = model.Booking.ID,
                    Updated = model.Booking.Updated,
                    UpdatedBy = model.Booking.UpdatedBy?.DisplayName,
                    BookingNo = model.Booking.BookingNo,
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit),
                    BookingDate = model.Booking.BookingDate,
                    ApproveDate = model.Booking.ApproveDate,
                    ContractDate = model.Booking.ContractDate,
                    BookingStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.BookingStatus),
                    CreateBookingFrom = MST.MasterCenterDropdownDTO.CreateFromModel(model.CreateBookingFrom),
                    ConfirmBy = USR.UserListDTO.CreateFromModel(model.ConfirmBy),
                    ConfirmDate = model.Booking.ConfirmDate,
                    IsCancelled = model.Booking.IsCancelled
                };

                var bookingOwner = DB.BookingOwners.Where(o => o.BookingID == model.Booking.ID && o.IsMainOwner == true).FirstOrDefault();
                result.Owner = BookingOwnerDropdownDTO.CreateFromModel(bookingOwner);

                var payments = DB.Payments.Where(o => o.BookingID == model.Booking.ID).ToList();
                result.CanDelete = payments.Count() > 0 ? false : true;

                var unitPriceModel = DB.UnitPrices.Where(o => o.BookingID == model.Booking.ID && o.UnitPriceStage.Key == UnitPriceStageKeys.Booking).FirstOrDefault();
                if (unitPriceModel != null)
                {
                    var unitPriceItemModel = DB.UnitPriceItems.Where(o => o.UnitPriceID == unitPriceModel.ID).ToList();
                    var sellingPrice = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Amount).FirstOrDefault();
                    var cashDiscount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.CashDiscount).Select(o => o.Amount).FirstOrDefault();

                    result.SellingPrice = sellingPrice - cashDiscount;
                }
                
                return result;
            }
            else
            {
                return null;
            }
        }

        public static BookingListDTO CreateFromModel(models.SAL.Booking model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new BookingListDTO()
                {
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    BookingNo = model.BookingNo,
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit),
                    BookingDate = model.BookingDate,
                    ApproveDate = model.ApproveDate,
                    ContractDate = model.ContractDate,
                    BookingStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.BookingStatus)
                };

                var bookingOwner = DB.BookingOwners.Where(o => o.BookingID == model.ID && o.IsMainOwner == true).FirstOrDefault();
                result.Owner = BookingOwnerDropdownDTO.CreateFromModel(bookingOwner);
                var payments = DB.Payments.Where(o => o.BookingID == model.ID).ToList();
                result.CanDelete = payments.Count() > 0 ? false : true;

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(BookingListSortByParam sortByParam, ref IQueryable<BookingQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case BookingListSortBy.BookingNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Booking.BookingNo);
                        else query = query.OrderByDescending(o => o.Booking.BookingNo);
                        break;
                    case BookingListSortBy.UnitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case BookingListSortBy.FullName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => (o.Owner.FirstNameTH)).ThenByDescending(o => o.Owner.LastNameTH);
                        else query = query.OrderByDescending(o => (o.Owner.FirstNameTH)).ThenByDescending(o => o.Owner.LastNameTH);
                        break;
                    case BookingListSortBy.BookingDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Booking.BookingDate);
                        else query = query.OrderByDescending(o => o.Booking.BookingDate);
                        break;
                    case BookingListSortBy.ApproveDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Booking.ApproveDate);
                        else query = query.OrderByDescending(o => o.Booking.ApproveDate);
                        break;
                    case BookingListSortBy.ContractDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Booking.ContractDate);
                        else query = query.OrderByDescending(o => o.Booking.ContractDate);
                        break;
                    case BookingListSortBy.BookingStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BookingStatus.Name);
                        else query = query.OrderByDescending(o => o.BookingStatus.Name);
                        break;
                    case BookingListSortBy.CreateBookingFrom:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CreateBookingFrom.Name);
                        else query = query.OrderByDescending(o => o.CreateBookingFrom.Name);
                        break;
                    case BookingListSortBy.ConfirmBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ConfirmBy.DisplayName);
                        else query = query.OrderByDescending(o => o.ConfirmBy.DisplayName);
                        break;
                    case BookingListSortBy.ConfirmDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Booking.ConfirmDate);
                        else query = query.OrderByDescending(o => o.Booking.ConfirmDate);
                        break;
                    default:
                        query = query.OrderByDescending(o => o.Booking.Created);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(o => o.Booking.Created);
            }
        }
    }

    public class BookingQueryResult
    {
        public models.SAL.Booking Booking { get; set; }
        public models.PRJ.Unit Unit { get; set; }
        public models.MST.MasterCenter BookingStatus { get; set; }
        public models.MST.MasterCenter CreateBookingFrom { get; set; }
        public models.SAL.BookingOwner Owner { get; set; }
        public models.USR.User ConfirmBy { get; set; }
    }
}
