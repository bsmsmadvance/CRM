using Base.DTOs.PRJ;
using Base.DTOs.MST;
using Database.Models.FIN;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Collections.Generic;
using Database.Models.USR;

namespace Base.DTOs.FIN
{
    public class ReceiptDetailDTO : BaseDTO
    {

        /// <summary>
        /// ข้อมูลใบเสร็จ
        /// </summary>
        public ReceiptTempHeader ReceiptTempHeader { get; set; }

        /// <summary>
        /// รายละเอียดใบเสร็จ
        /// </summary>
        public List<ReceiptTempDetail> ReceiptTempDetail { get; set; }


        public List<PaymentMethod> PaymentMethod { get; set; }

        ///// <summary>
        ///// ID ข้อมูลการรับชำระเงินโอนต่างประเทศ
        ///// </summary>
        //public PaymentForeignBankTransferDTO PaymentForeignBankTransfer { get; set; }

        ///// <summary>
        ///// ID ข้อมูลการรับชำระเงินบัตรเครดิต
        ///// </summary>
        //public PaymentCreditCardDTO PaymentCreditCard { get; set; }

        ///// <summary>
        ///// Booking
        ///// </summary>
        //public Guid? BookingID { get; set; }

        ///// <summary>
        ///// ชื่อลูกค้า
        ///// </summary>
        //public string CustomerName { get; set; }

        ///// <summary>
        ///// จำนวนเงินที่ขอ ReceiptInfo
        ///// </summary>
        //public decimal ReceiptInfoAmount { get; set; }

        //// <summary>
        //// หมายเหตุ
        //// </summary>
        //public string Remark { get; set; }

        ///// <summary>
        ///// สถานะการขอ ReceiptInfo
        ///// </summary>
        //public MasterCenterDTO ReceiptInfoStatus { get; set; }

        ///// <summary>
        ///// File แนบ Credit Advice
        ///// </summary>
        //public FileDTO AttachFile { get; set; }

        //// <summary>
        //// หมายเหตุ
        //// </summary>
        //public string CancelRemark { get; set; }

        ///// <summary>
        ///// User ตีเอกสารกลับไปให้ LC
        ///// </summary>
        //public USR.UserDTO RejectByUserID { get; set; }

        ///// <summary>
        ///// วันที่ การตีเอกสารกลับไปให้ LC
        ///// </summary>
        //public DateTime? RejectDate { get; set; }

        //// <summary>
        //// หมายเหตุ
        //// </summary>
        //[Description("หมายเหตุ Reject")]
        //public string RejectRemark { get; set; }


        //public UnitDropdownDTO Unit { get; set; }
        //public ProjectDropdownDTO Project { get; set; }


        //public int? countReceiptInfo { get; set; }
        //public int? countUnit { get; set; }
        //public decimal? SumAmountReceiptInfo { get; set; }

        //public User PaymentUserOwner { get; set; }

        ////public DepositHeader DepositHeader { get; set; }


        //// <DepositNo>
        //// เลขที่นำฝาก
        //// </DepositNo>
        //public string DepositNo { get; set; }

        //// <ReceiptTempNo>
        //// วันที่ใบเสร็จ
        //// </ReceiptTempNo>
        //public string ReceiptTempNo { get; set; }

        //public DateTime? ReceiveDate { get; set; }

        //public MasterCenterDropdownDTO ReceiptInfoRequesterMasterCenter { get; set; }

        //public MasterCenterDropdownDTO ReceiptInfoStatusMasterCenter { get; set; }

        //public MasterCenterDropdownDTO PaymentMethodTypeMasterCenter { get; set; }

        //public BankDropdownDTO Bank { get; set; }


        ////public CancelRemark CancelRemark { get; set; }

        ////-----------------------------------------------ReceiptInfoQueryResult----------------------------------------------------------------------------------------------------

        public class ReceiptInfoQueryResult
        {
            public ReceiptTempHeader ReceiptTempHeader { get; set; }

            public ReceiptTempDetail ReceiptTempDetail { get; set; }

            public Company Company { get; set; }

            public Project Project { get; set; }

            public Unit Unit { get; set; }

            public BankAccount BankAccount { get; set; }

            public Payment Payment { get; set; }

            public PaymentMethod PaymentMethod { get; set; }

            public PaymentMethodToItem PaymentMethodToItem { get; set; }

        }


        //public static ReceiptInfoDTO CreateFromQuery(ReceiptInfoQueryResult model, DatabaseContext db)
        //{
        //    if (model != null)
        //    {
        //        ReceiptInfoDTO result = new ReceiptInfoDTO()
        //        {
        //            Id = model.ReceiptInfo.ID,
        //            PaymentForeignBankTransfer = PaymentForeignBankTransferDTO.CreateFromModel(model.PaymentForeignBankTransfer),
        //            PaymentCreditCard = PaymentCreditCardDTO.CreateFromModel(model.PaymentCreditCard),
        //            BookingID = model.Booking?.ID,
        //            ReceiptInfoAmount = model.ReceiptInfo.Amount,
        //            Remark = model.ReceiptInfo.Remark,
        //            ReceiptInfoStatus = MasterCenterDTO.CreateFromModel(model.ReceiptInfo.ReceiptInfoStatus),
        //            CancelRemark = model.ReceiptInfo.CancelRemark,
        //            RejectByUserID = USR.UserDTO.CreateFromModel(model.ReceiptInfo.RejectByUser),
        //            RejectDate = model.ReceiptInfo.RejectDate,
        //            RejectRemark = model.ReceiptInfo.RejectRemark,
        //            Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
        //            Project = ProjectDropdownDTO.CreateFromModel(model.Project),
        //            Bank = BankDropdownDTO.CreateFromModel(model.Bank),
        //            DepositNo = model.DepositHeader?.DepositNo,

        //            CustomerName = model.ReceiptInfo.CustomerName,

        //            PaymentUserOwner = (model.Payment?.CreatedBy == null) ? model.ReceiptInfo.CreatedBy : model.Payment?.CreatedBy,
        //            ReceiveDate = model.Payment?.ReceiveDate,

        //            ReceiptInfoRequesterMasterCenter = MasterCenterDropdownDTO.CreateFromModel(model.ReceiptInfoRequesterMasterCenter),
        //            ReceiptInfoStatusMasterCenter = MasterCenterDropdownDTO.CreateFromModel(model.ReceiptInfoStatusMasterCenter),
        //            PaymentMethodTypeMasterCenter = MasterCenterDropdownDTO.CreateFromModel(model.PaymentMethodTypeMasterCenter)
        //        };

        //        result.AttachFile = new FileDTO();
        //        result.AttachFile.Name = model.ReceiptInfo.AttachFileName;
        //        result.AttachFile.Url = model.ReceiptInfo.AttachFileUrl;

        //        return result;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}


        ////----------------------------------------------ReceiptInfoQueryResultViewUnit-----------------------------------------------------------------------------------------------------
        //public static void SortBy(ReceiptInfoSortByParam sortByParam, ref IQueryable<ReceiptInfoQueryResult> query)
        //{
        //    if (sortByParam.SortBy != null)
        //    {
        //        switch (sortByParam.SortBy.Value)
        //        {
        //            case ReceiptInfoSortBy.Project:
        //                if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo);
        //                else query = query.OrderByDescending(o => o.Project.ProjectNo);
        //                break;
        //            case ReceiptInfoSortBy.ReceiveDate:
        //                if (sortByParam.Ascending) query = query.OrderBy(o => o.Payment.ReceiveDate);
        //                else query = query.OrderByDescending(o => o.Payment.ReceiveDate);
        //                break;
        //            case ReceiptInfoSortBy.UnitNo:
        //                if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
        //                else query = query.OrderByDescending(o => o.Unit.UnitNo);
        //                break;
        //            case ReceiptInfoSortBy.ReceiptInfoNumber:
        //                if (sortByParam.Ascending) query = query.OrderBy(o => o.ReceiptInfo.ReceiptInfoStatusMasterCenterID);
        //                else query = query.OrderByDescending(o => o.ReceiptInfo.ReceiptInfoStatusMasterCenterID);
        //                break;
        //            case ReceiptInfoSortBy.ReceiptInfoAmount:
        //                if (sortByParam.Ascending) query = query.OrderBy(o => o.PaymentMethod.PayAmount);
        //                else query = query.OrderByDescending(o => o.PaymentMethod.PayAmount);
        //                break;
        //            case ReceiptInfoSortBy.Company:
        //                if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.Company);
        //                else query = query.OrderByDescending(o => o.Project.Company);
        //                break;
        //            case ReceiptInfoSortBy.DepositCode:
        //                if (sortByParam.Ascending) query = query.OrderBy(o => o.DepositHeader.DepositNo);
        //                else query = query.OrderByDescending(o => o.DepositHeader.DepositNo);
        //                break;
        //            case ReceiptInfoSortBy.ReceiptAmount:
        //                if (sortByParam.Ascending) query = query.OrderBy(o => o.PaymentMethod.PayAmount);
        //                else query = query.OrderByDescending(o => o.PaymentMethod.PayAmount);
        //                break;
        //            case ReceiptInfoSortBy.Updated:
        //                if (sortByParam.Ascending) query = query.OrderBy(o => o.ReceiptInfo.Updated);
        //                else query = query.OrderByDescending(o => o.ReceiptInfo.Updated);
        //                break;
        //            case ReceiptInfoSortBy.UpdateByuser:
        //                if (sortByParam.Ascending) query = query.OrderBy(o => o.ReceiptInfo.UpdatedByUserID);
        //                else query = query.OrderByDescending(o => o.ReceiptInfo.UpdatedByUserID);
        //                break;

        //            default:
        //                query = query.OrderBy(o => o.Project.ProjectNo);
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        query = query.OrderBy(o => o.Project.ProjectNo);
        //    }

        //}


        ////public void ToModel(ref ReceiptInfo model)
        ////{
        ////    model.ReceiptInfoRequesterMasterCenterID = ReceiptInfoRequesterMasterCenter.Id;
        ////    model.ProjectID = Project.Id;
        ////    model.BookingID = BookingID;
        ////    model.CustomerName = CustomerName;

        ////    model.Amount = ReceiptInfoAmount;

        ////    model.Remark = Remark;
        ////}

        //public async Task ValidateAsync(DatabaseContext db)
        //{
        //    ValidateException ex = new ValidateException();

        //    if (ReceiptInfoAmount == 0)
        //    {
        //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
        //        string desc = GetType().GetProperty(nameof(ReceiptInfoAmount)).GetCustomAttribute<DescriptionAttribute>().Description;
        //        var msg = errMsg.Message.Replace("[field]", desc);
        //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
        //    }

        //    if (ex.HasError)
        //    {
        //        throw ex;
        //    }
        //}


    }
}
