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
using Database.Models.ACC;

namespace Base.DTOs.FIN
{
    public class ReceiptInfoDTO : BaseDTO
    {

        /// <summary>
        /// เลขที่ใบเสร็จ
        /// </summary>
        public string ReceiptTempNo { get; set; }

        /// <summary>
        /// วันที่ใบเสร็จ
        /// </summary>
        public DateTime ReceiveDate { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        public ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        public UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// เลขที่บัญชี
        /// </summary>
        public BankAccountDropdownDTO BankAccount { get; set; }

        /// <summary>
        /// ประเภทการชำระ
        /// </summary>
        public List<MasterCenterDropdownDTO> PaymentType { get; set; }

        /// <summary>
        /// ค่าใช้จ่าย
        /// </summary>
        public List<string> ReceiptDescription { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// เลขที่นำฝาก
        /// </summary>
        public List<string> DepositNo { get; set; }

        /// <summary>
        /// เลขที่ RV
        /// </summary>
        public List<string> RVNumber { get; set; }

        /// <summary>
        /// สถานะใบเสร็จ
        /// </summary>
        public bool? ReceiptStatus { get; set; }

        ////public CancelRemark CancelRemark { get; set; }

        public class ReceiptInfoQueryResult
        {
            public ReceiptTempHeader ReceiptTempHeader { get; set; }

            public ReceiptTempDetail ReceiptTempDetail { get; set; }

            public Company Company { get; set; }

            public Project Project { get; set; }

            public Unit Unit { get; set; }

            public PaymentItem PaymentItem { get; set; }
            public PaymentMethodToItem PaymentMethodToItem { get; set; }
            public PaymentMethod PaymentMethod { get; set; }

            public MasterCenter PaymentMethodType { get; set; }

            public DepositHeader DepositHeader { get; set; }
            public PostGLHeader PostGLHeader { get; set; }
        }
        public class ReceiptInfoQueryResultList
        {
            public ReceiptTempHeader ReceiptTempHeader { get; set; }
            public Company Company { get; set; }
            public Project Project { get; set; }
            public Unit Unit { get; set; }

            public List<ReceiptTempDetail> ReceiptTempDetail { get; set; }
            public List<PaymentItem> PaymentItem { get; set; }
            public List<PaymentMethodToItem> PaymentMethodToItem { get; set; }
            public List<PaymentMethod> PaymentMethod { get; set; }
            public List<MasterCenter> PaymentMethodType { get; set; }
            public List<DepositHeader> DepositHeader { get; set; }
            public List<PostGLHeader> PostGLHeader { get; set; }
        }
        ////-----------------------------------------------ReceiptInfoQueryResult----------------------------------------------------------------------------------------------------
        public static ReceiptInfoDTO CreateFromQueryList(ReceiptInfoQueryResultList model, DatabaseContext db)
        {
            if (model != null)
            {
                ReceiptInfoDTO result = new ReceiptInfoDTO()
                {
                    Id = model.ReceiptTempHeader.ID,
                    ReceiptTempNo = model.ReceiptTempHeader.ReceiptTempNo,
                    ReceiveDate = model.ReceiptTempHeader.ReceiveDate,
                    Project = ProjectDropdownDTO.CreateFromModel(model.Project),
                    Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
                    PaymentType = model.PaymentMethodType.Select(x => MasterCenterDropdownDTO.CreateFromModel(x)).ToList(),
                    ReceiptDescription = model.ReceiptTempDetail.Select(x => x.Description).ToList(),
                    Amount = model.ReceiptTempDetail.Sum( x => x.Amount),
                    DepositNo = model.DepositHeader.Select(x => x.DepositNo).ToList(),
                    RVNumber = model.PostGLHeader.Select(x => x.DocumentNo).ToList(),
                    ReceiptStatus = !model.ReceiptTempHeader.IsDeleted
                };

                BankAccount BankAccount = null;
                if (model.PaymentMethodType.Any(x => x.Key == PaymentMethodKeys.BankTransfer))
                {
                    BankAccount = db.PaymentBankTransfers.Include(o => o.BankAccount)
                        .Select(o => o.BankAccount).FirstOrDefault();
                }
                else if (model.PaymentMethodType.Any(x => x.Key == PaymentMethodKeys.BillPayment))
                {
                    BankAccount = db.PaymentBillPayments.Include(o => o.BillPaymentDetail).ThenInclude(o => o.BillPayment).ThenInclude(o => o.BankAccount)
                        .Select(o => o.BillPaymentDetail.BillPayment.BankAccount).FirstOrDefault();
                }
                else if (model.PaymentMethodType.Any(x => x.Key == PaymentMethodKeys.QRCode))
                {
                    BankAccount = db.PaymentQRCodes.Include(o => o.BankAccount)
                        .Where(o => model.PaymentMethod.Any(z => z.ID == o.PaymentMethodID))
                        .Select(o => o.BankAccount).FirstOrDefault();
                }
                else if (model.PaymentMethodType.Any(x => x.Key == PaymentMethodKeys.UnknowPayment))
                {
                    BankAccount = db.PaymentUnknownPayments.Include(o => o.UnknownPayment).ThenInclude(o => o.BankAccount)
                          .Where(o => model.PaymentMethod.Any(z => z.ID == o.PaymentMethodID))
                        .Select(o => o.UnknownPayment.BankAccount).FirstOrDefault();
                }
                else if (model.PaymentMethodType.Any(x => x.Key == PaymentMethodKeys.ForeignBankTransfer))
                {
                    BankAccount = db.PaymentForeignBankTransfers.Include(o => o.BankAccount)
                          .Where(o => model.PaymentMethod.Any(z => z.ID == o.PaymentMethodID))
                        .Select(o => o.BankAccount).FirstOrDefault();
                }

                result.BankAccount = BankAccountDropdownDTO.CreateFromModel(BankAccount);

                return result;
            }
            else
            {
                return null;
            }
        }
        /////////////////////////////////////////////////////////////////////

        /// <summary>

        //public static ReceiptInfoDTO CreateFromQuery(ReceiptInfoQueryResult model, DatabaseContext db)
        //{
        //    if (model != null)
        //    {
        //        ReceiptInfoDTO result = new ReceiptInfoDTO()
        //        {
        //            Id = model.ReceiptTempDetail.ID,
        //            ReceiptTempNo = model.ReceiptTempHeader.ReceiptTempNo,
        //            ReceiveDate = model.ReceiptTempHeader.ReceiveDate,
        //            Project = ProjectDropdownDTO.CreateFromModel(model.Project),
        //            Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
        //            PaymentType = MasterCenterDropdownDTO.CreateFromModel(model.PaymentMethodType),
        //            ReceiptDescription = model.ReceiptTempDetail.Description,
        //            Amount = model.ReceiptTempDetail.Amount,
        //            DepositNo = model.DepositHeader.DepositNo,
        //            RVNumber = model.PostGLHeader.DocumentNo,
        //            ReceiptStatus = !model.ReceiptTempHeader.IsDeleted
        //        };

        //        BankAccount BankAccount = null;
        //        if (model.PaymentMethodType.Key == PaymentMethodKeys.BankTransfer)
        //        {
        //            BankAccount = db.PaymentBankTransfers.Include(o => o.BankAccount)
        //                .Select(o => o.BankAccount).FirstOrDefault();
        //        }
        //        else if (model.PaymentMethodType.Key == PaymentMethodKeys.BillPayment)
        //        {
        //            BankAccount = db.PaymentBillPayments.Include(o => o.BillPaymentDetail).ThenInclude(o => o.BillPayment).ThenInclude(o => o.BankAccount)
        //                .Select(o => o.BillPaymentDetail.BillPayment.BankAccount).FirstOrDefault();
        //        }
        //        else if (model.PaymentMethodType.Key == PaymentMethodKeys.QRCode)
        //        {
        //            BankAccount = db.PaymentQRCodes.Include(o => o.BankAccount)
        //                .Where(o => o.PaymentMethodID == model.PaymentMethod.ID)
        //                .Select(o => o.BankAccount).FirstOrDefault();
        //        }
        //        else if (model.PaymentMethodType.Key == PaymentMethodKeys.UnknowPayment)
        //        {
        //            BankAccount = db.PaymentUnknownPayments.Include(o => o.UnknownPayment).ThenInclude(o => o.BankAccount)
        //                .Where(o => o.PaymentMethodID == model.PaymentMethod.ID)
        //                .Select(o => o.UnknownPayment.BankAccount).FirstOrDefault();
        //        }
        //        else if (model.PaymentMethodType.Key == PaymentMethodKeys.ForeignBankTransfer)
        //        {
        //            BankAccount = db.PaymentForeignBankTransfers.Include(o => o.BankAccount)
        //                .Where(o => o.PaymentMethodID == model.PaymentMethod.ID)
        //                .Select(o => o.BankAccount).FirstOrDefault();
        //        }

        //        result.BankAccount = BankAccountDropdownDTO.CreateFromModel(BankAccount);

        //        return result;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public static void SortBy(ReceiptInfoSortByParam sortByParam, ref List<ReceiptInfoDTO> model)
        {
            if (sortByParam.SortBy != null)
            {

                switch (sortByParam.SortBy.Value)
                {
                    case ReceiptInfoSortBy.ReceiptTempNo:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.ReceiptTempNo).ToList();
                        else model = model.OrderByDescending(o => o.ReceiptTempNo).ToList();
                        break;

                    case ReceiptInfoSortBy.ReceiveDate:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.ReceiveDate).ToList();
                        else model = model.OrderByDescending(o => o.ReceiveDate).ToList();
                        break;

                    case ReceiptInfoSortBy.ProjectNo:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.Project.ProjectNo).ToList();
                        else model = model.OrderByDescending(o => o.Project.ProjectNo).ToList();
                        break;

                    case ReceiptInfoSortBy.UnitNo:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.Unit.UnitNo).ToList();
                        else model = model.OrderByDescending(o => o.Unit.UnitNo).ToList();
                        break;

                    case ReceiptInfoSortBy.BankAccount:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.BankAccount.BankAccountNo).ToList();
                        else model = model.OrderByDescending(o => o.BankAccount.BankAccountNo).ToList();
                        break;

                    //case ReceiptInfoSortBy.PaymentType:
                    //    if (sortByParam.Ascending) model = model.OrderBy(o => o.PaymentType.Name).ToList();
                    //    else model = model.OrderByDescending(o => o.PaymentType.Name).ToList();
                    //    break;

                    case ReceiptInfoSortBy.PaymentType:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.PaymentType).ToList();
                        else model = model.OrderByDescending(o => o.PaymentType).ToList();
                        break;

                    case ReceiptInfoSortBy.ReceiptDescription:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.ReceiptDescription).ToList();
                        else model = model.OrderByDescending(o => o.ReceiptDescription).ToList();
                        break;

                    case ReceiptInfoSortBy.Amount:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.Amount).ToList();
                        else model = model.OrderByDescending(o => o.Amount).ToList();
                        break;

                    case ReceiptInfoSortBy.DepositNo:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.DepositNo).ToList();
                        else model = model.OrderByDescending(o => o.DepositNo).ToList();
                        break;

                    case ReceiptInfoSortBy.RVNumber:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.RVNumber).ToList();
                        else model = model.OrderByDescending(o => o.RVNumber).ToList();
                        break;

                    case ReceiptInfoSortBy.ReceiptStatus:
                        if (sortByParam.Ascending) model = model.OrderBy(o => o.ReceiptStatus).ToList();
                        else model = model.OrderByDescending(o => o.ReceiptStatus).ToList();
                        break;

                    default:
                        model = model.OrderBy(o => o.Project.ProjectNo).ThenBy(o => o.Unit.UnitNo).ToList();
                        break;
                }
            }
            else
            {
                model = model.OrderBy(o => o.ReceiptTempNo).ToList();
            }
        }

    }
}
