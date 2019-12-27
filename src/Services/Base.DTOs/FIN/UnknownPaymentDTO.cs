using Database.Models.MST;
using Database.Models.USR;
using System;
using System.ComponentModel;
using System.Linq;
using Database.Models.ACC;
using Database.Models.PRJ;
using Database.Models.SAL;
using Database.Models.FIN;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Database.Models;
using System.Threading.Tasks;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Collections.Generic;
using Database.Models.MasterKeys;

namespace Base.DTOs.FIN
{
    public class UnknownPaymentDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ PI
        /// </summary>
        [Description("เลขที่ PI")]
        public string UnknownPaymentCode { get; set; }

        /// <summary>
        /// วันที่เงินเข้า
        /// </summary>
        [Description("วันที่เงินเข้า")]
        public DateTime ReceiveDate { get; set; }

        /// <summary>
        /// บริษัท
        /// </summary>
        [Description("บริษัท")]
        public CompanyDropdownDTO Company { get; set; }

        /// <summary>
        /// บัญชีธนาคาร filter ตามข้อมูลบริษัท
        /// </summary>
        [Description("บัญชีธนาคาร")]
        public BankAccountDropdownDTO BankAccount { get; set; }

        /// <summary>
        /// เลขที่ Booking
        /// </summary>
        [Description("เลขที่ Booking")]
        public Guid? BookingID { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        [Description("โครงการ")]
        public ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        [Description("แปลง")]
        public UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// เงินตั้งพัก
        /// </summary>
        [Description("เงินตั้งพัก")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Post PO
        /// </summary>
        [Description("Post PO")]
        public bool IsPostPO { get; set; }

        /// <summary>
        /// เงินตั้งพัก
        /// </summary>
        [Description("เลขที่ RV")]
        public string RVNumber { get; set; }

        /// <summary>
        /// สถานะ
        /// </summary>
        [Description("สถานะ")]
        public MasterCenterDropdownDTO UnknownPaymentStatus { get; set; }

        /// <summary>
        /// ผู้บันทึก
        /// </summary>
        [Description("ผู้บันทึก")]
        public UserDTO CreatedBy { get; set; }

        /// <summary>
        /// ผู้กลับรายการ
        /// </summary>
        [Description("ผู้กลับรายการ")]
        public UserDTO ReversedBy { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [Description("หมายเหตุ")]
        public string Remark { get; set; }

        /// <summary>
        /// CancelRemark
        /// </summary>
        [Description("หมายเหตุยกเลิก")]
        public string CancelRemark { get; set; }

        /// <summary>
        /// หมายเหตุรายการด้าน SAP
        /// </summary>
        [Description("หมายเหตุรายการด้าน SAP")]
        public string SAPRemark { get; set; }

        public List<UnknownPaymentReverseDTO> UnknownPaymentReverse { get; set; }

        public static void SortBy(UnknownPaymentSortByParam sortByParam, ref IQueryable<UnknownPaymentQueryResult> query)
        {
            IOrderedQueryable<UnknownPaymentQueryResult> orderQuery = query.OrderBy(o => o.ReceiptTempHeader.ReceiveDate);

            if (sortByParam.SortBy != null)
            {
                //ReceiveDate,
                //ReverseDate,
                //CompanyID,
                //BankAccountID,
                //ProjectID,
                //Unit,
                //UnknowPaymentStatus,
                //UnknowPaymentCode,
                //Amount,
                //RVDocumentCode,
                //Updated

                switch (sortByParam.SortBy.Value)
                {
                    case UnknownPaymentSortBy.ReceiveDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ReceiptTempHeader.ReceiveDate);
                        else orderQuery = query.OrderByDescending(o => o.ReceiptTempHeader.ReceiveDate);
                        break;
                    case UnknownPaymentSortBy.ReverseDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.PaymentUnknownPayment.Created);
                        else orderQuery = query.OrderByDescending(o => o.PaymentUnknownPayment.Created);
                        break;
                    case UnknownPaymentSortBy.Company:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Company.Code);
                        else orderQuery = query.OrderByDescending(o => o.Company.Code);
                        break;
                    case UnknownPaymentSortBy.BankAccount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.BankAccount.BankAccountNo);
                        else orderQuery = query.OrderByDescending(o => o.BankAccount.BankAccountNo);
                        break;
                    case UnknownPaymentSortBy.Project:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Project.ProjectNo);
                        else orderQuery = query.OrderByDescending(o => o.Project.ProjectNo);
                        break;
                    case UnknownPaymentSortBy.Unit:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Unit.UnitNo);
                        else orderQuery = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case UnknownPaymentSortBy.UnknownPaymentStatus:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.UnknownPayment.UnknownPaymentStatus.Name);
                        else orderQuery = query.OrderByDescending(o => o.UnknownPayment.UnknownPaymentStatus.Name);
                        break;

                    case UnknownPaymentSortBy.UnknownPaymentCode:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.UnknownPayment.UnknownPaymentCode);
                        else orderQuery = query.OrderByDescending(o => o.UnknownPayment.UnknownPaymentCode);
                        break;

                    case UnknownPaymentSortBy.Amount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Payment.TotalAmount);
                        else orderQuery = query.OrderByDescending(o => o.Payment.TotalAmount);
                        break;
                    case UnknownPaymentSortBy.RVDocumentCode:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Payment.TotalAmount);
                        else orderQuery = query.OrderByDescending(o => o.Payment.TotalAmount);
                        break;
                    case UnknownPaymentSortBy.Updated:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Payment.TotalAmount);
                        else orderQuery = query.OrderByDescending(o => o.Payment.TotalAmount);
                        break;
                    default:
                        orderQuery = query.OrderBy(o => o.ReceiptTempHeader.ReceiveDate);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.Project.ProjectNo);
            }

            query = orderQuery;
        }

        public static UnknownPaymentDTO CreateFromQueryResult(UnknownPaymentQueryResult model)
        {
            if (model != null)
            {
                UnknownPaymentDTO result = new UnknownPaymentDTO()
                {
                    Id = model.UnknownPayment.ID,
                    UnknownPaymentCode = model.UnknownPayment.UnknownPaymentCode,
                    ReceiveDate = model.UnknownPayment.ReceiveDate,
                    Company = CompanyDropdownDTO.CreateFromModel(model.Company),
                    BankAccount = BankAccountDropdownDTO.CreateFromModel(model.BankAccount),
                    BookingID = model.UnknownPayment.BookingID,
                    Project = ProjectDropdownDTO.CreateFromModel(model.Project),
                    Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
                    Amount = model.UnknownPayment.Amount,
                    IsPostPO = (model.PostGLAccount.DocCode ?? "") == "" ? false : true,
                    RVNumber = null, // #kim
                    UnknownPaymentStatus = MasterCenterDropdownDTO.CreateFromModel(model.UnknownPaymentStatus),
                    CreatedBy = UserDTO.CreateFromModel(model.CreatedBy),
                    ReversedBy = UserDTO.CreateFromModel(model.ReversedBy),
                    Remark = model.UnknownPayment.Remark,
                    CancelRemark = model.UnknownPayment.CancelRemark,
                    SAPRemark = model.UnknownPayment.SAPRemark
                };

                result.Updated = (model.PaymentUnknownPayment.Updated != null) ? model.PaymentUnknownPayment.Updated : model.UnknownPayment.Updated;

                return result;
            }
            else
            {
                return null;
            }
        }

        public void ToModel(ref UnknownPayment model)
        {
            model.Amount = Amount;
            model.BookingID = BookingID;
            model.ReceiveDate = ReceiveDate;
            model.BankAccountID = BankAccount.Id;
            model.IsDeleted = false;
            model.CancelRemark = null;

            model.UnknownPaymentStatusID = model.UnknownPaymentStatusID;

            model.SAPRemark = null;
            model.Remark = Remark;
        }

        public async Task ValidateOnUpdateAsync(DatabaseContext DB)
        {
            ValidateException ex = new ValidateException();

            var Company = DB.Companies.Where(o => o.ID == this.Company.Id).FirstOrDefault();
            var BankAccount = DB.BankAccounts.Where(o => o.ID == this.BankAccount.Id).FirstOrDefault();

            var IsLockCalendar = await DB.CheckCalendarAsync(Company.ID, ReceiveDate);

            if (Company == null)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0089").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(Company)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (BankAccount == null)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0089").FirstAsync();
                string desc = GetType().GetProperty(nameof(BankAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ReceiveDate == null)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0089").FirstAsync();
                string desc = GetType().GetProperty(nameof(ReceiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (Amount == 0)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0089").FirstAsync();
                string desc = GetType().GetProperty(nameof(Amount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            /* ** วันที่ปิดบัญชี ** */
            if (IsLockCalendar)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0091").FirstAsync();
                string CompanyName = string.Format("{0} - {1}", Company.Code, Company.NameTH);
                string strDepositDate = ReceiveDate.ToString("dd/MM/yyyy");
                var msg = errMsg.Message.Replace("[company]", CompanyName);
                msg = errMsg.Message.Replace("[date]", strDepositDate);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public async Task ValidateBeforeUpdateAsync(int UpdateType, DatabaseContext DB)
        {
            ValidateException ex = new ValidateException();

            if (UpdateType == 0)
                return;

            var headerModel = DB.UnknownPayments.Where(e => e.ID == Id).FirstOrDefault() ?? new UnknownPayment();

            var MasterCenter = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnknownPaymentStatus && o.Key == UnknownPaymentStatusKeys.SAP).FirstOrDefault() ?? new MasterCenter();

            var detailModel = DB.PaymentUnknownPayments.Where(e => e.UnknownPaymentID == Id && !e.IsDeleted).ToList();
            var postGLModel = DB.PostGLAccounts.Where(e => e.DocCode == UnknownPaymentCode && !e.IsDeleted).FirstOrDefault() ?? new PostGLAccount();

            bool canEdit = false;
            bool canReverse = false;

            bool canTransferToSAP = false;
            bool canDelete = false;

            if ((postGLModel.DocCode ?? "") == "" && !detailModel.Any() && (headerModel.UnknownPaymentStatusID ?? Guid.Empty) != MasterCenter.ID && !headerModel.IsDeleted)
                canEdit = true;

            if ((postGLModel.DocCode ?? "") != "" && (headerModel.UnknownPaymentStatusID ?? Guid.Empty) != MasterCenter.ID && !headerModel.IsDeleted)
                canReverse = true;

            if ((postGLModel.DocCode ?? "") != "" && !detailModel.Any() && !headerModel.IsDeleted)
                canTransferToSAP = true;

            if ((postGLModel.DocCode ?? "") == "" && !detailModel.Any() && !headerModel.IsDeleted)
                canDelete = true;

            if (UpdateType == 1 && !canEdit)
            {
                var msg = new ErrorMessage();
                if ((postGLModel.DocCode ?? "") != "")
                    msg = DB.ErrorMessages.Where(o => o.Key == "ERR0095").FirstOrDefault() ?? new ErrorMessage();

                if (detailModel.Any())
                    msg = DB.ErrorMessages.Where(o => o.Key == "ERR0096").FirstOrDefault() ?? new ErrorMessage();

                if ((headerModel.UnknownPaymentStatusID ?? Guid.Empty) != MasterCenter.ID)
                    msg = DB.ErrorMessages.Where(o => o.Key == "ERR0097").FirstOrDefault() ?? new ErrorMessage();

                var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0092").FirstOrDefault() ?? new ErrorMessage();
                ex.AddError(errMsg.Key, errMsg.Message.Replace("[msg]", msg.Message ?? ""), (int)errMsg.Type);
            }

            if (UpdateType == 2 && !canReverse)
            {
                var msg = new ErrorMessage();
                if ((postGLModel.DocCode ?? "") == "")
                    msg = DB.ErrorMessages.Where(o => o.Key == "ERR0098").FirstOrDefault() ?? new ErrorMessage();

                if ((headerModel.UnknownPaymentStatusID ?? Guid.Empty) == MasterCenter.ID)
                    msg = DB.ErrorMessages.Where(o => o.Key == "ERR0097").FirstOrDefault() ?? new ErrorMessage();

                var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0093").FirstOrDefault() ?? new ErrorMessage();
                ex.AddError(errMsg.Key, errMsg.Message.Replace("[msg]", msg.Message ?? ""), (int)errMsg.Type);
            }

            if (UpdateType == 3 && !canTransferToSAP)
            {
                var msg = new ErrorMessage();
                if ((postGLModel.DocCode ?? "") != "")
                    msg = DB.ErrorMessages.Where(o => o.Key == "ERR0098").FirstOrDefault() ?? new ErrorMessage();

                if ((headerModel.UnknownPaymentStatusID ?? Guid.Empty) == MasterCenter.ID)
                    msg = DB.ErrorMessages.Where(o => o.Key == "ERR0097").FirstOrDefault() ?? new ErrorMessage();

                var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0092").FirstOrDefault() ?? new ErrorMessage();
                ex.AddError(errMsg.Key, errMsg.Message.Replace("[msg]", msg.Message ?? ""), (int)errMsg.Type);
            }

            if (UpdateType == 4 && !canDelete)
            {
                var msg = new ErrorMessage();
                if ((postGLModel.DocCode ?? "") != "")
                    msg = DB.ErrorMessages.Where(o => o.Key == "ERR0095").FirstOrDefault() ?? new ErrorMessage();

                if (detailModel.Any())
                    msg = DB.ErrorMessages.Where(o => o.Key == "ERR0096").FirstOrDefault() ?? new ErrorMessage();

                if ((headerModel.UnknownPaymentStatusID ?? Guid.Empty) == MasterCenter.ID)
                    msg = DB.ErrorMessages.Where(o => o.Key == "ERR0097").FirstOrDefault() ?? new ErrorMessage();

                var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0094").FirstOrDefault() ?? new ErrorMessage();
                ex.AddError(errMsg.Key, errMsg.Message.Replace("[msg]", msg.Message ?? ""), (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }
    }

    public class UnknownPaymentQueryResult
    {

        /*
            SELECT * FROM FIN.UnknownPayment
                SELECT * FROM SAL.Booking
                    SELECT * FROM PRJ.Project
                    SELECT * FROM PRJ.Unit
                SELECT * FROM MST.BankAccount
                    SELECT * FROM MST.Company
                SELECT * FROM USR.[User]
                SELECT * FROM ACC.PostGLAccount
            SELECT * FROM FIN.PaymentUnknownPayment
                SELECT * FROM FIN.PaymentMethod
                SELECT * FROM USR.[User]
        */

        public UnknownPayment UnknownPayment { get; set; }
        public MasterCenter UnknownPaymentStatus { get; set; }

        public Booking Booking { get; set; }
        public Project Project { get; set; }
        public Unit Unit { get; set; }

        public BankAccount BankAccount { get; set; }
        public Company Company { get; set; }
        public User CreatedBy { get; set; }
        public PostGLAccount PostGLAccount { get; set; }

        public PaymentUnknownPayment PaymentUnknownPayment { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Payment Payment { get; set; }
        public ReceiptTempHeader ReceiptTempHeader { get; set; }

        public User ReversedBy { get; set; }

    }

}
