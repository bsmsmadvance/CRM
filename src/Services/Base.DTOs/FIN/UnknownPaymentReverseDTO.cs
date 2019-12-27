using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.FIN;
using Database.Models.PRJ;
using Database.Models.SAL;
using Database.Models.USR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Base.DTOs.FIN
{

    public class UnknownPaymentDetailDTO : BaseDTO
    {
        [Description("ID เงินโอนไม่ทราบผู้โอน")]
        public Guid? UnknownPaymentID { get; set; }

        /// <summary>
        /// เลขที่ PI
        /// </summary>
        [Description("เลขที่ PI")]
        public string UnknownPaymentCode { get; set; }

        /// <summary>
        /// วันที่เงินเข้า
        /// </summary>
        [Description("วันที่เงินเข้า")]
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// บริษัท
        /// </summary>
        [Description("บริษัท")]
        public CompanyDropdownDTO Company { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        public ProjectDTO Project { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        public UnitDTO Unit { get; set; }

        /// <summary>
        /// บัญชีธนาคาร filter ตามข้อมูลบริษัท
        /// </summary>
        [Description("บัญชีธนาคาร")]
        public BankAccountDropdownDTO BankAccount { get; set; }

        /// <summary>
        /// เงินตั้งพัก
        /// </summary>
        [Description("เงินตั้งพัก")]
        public decimal Amount { get; set; }

        /// <summary>
        /// เงินตั้งพักคงเหลือ
        /// </summary>
        [Description("เงินตั้งพักคงเหลือ")]
        public decimal AmountBalance { get; set; }

        public List<UnknownPaymentReverseDTO> UnknownPaymentReverseList { get; set; }

        public static UnknownPaymentDetailDTO CreateFromQueryResultAsync(UnknownPaymentQueryResult model, DatabaseContext DB)
        {
            if (model != null)
            {
                UnknownPaymentDetailDTO result = new UnknownPaymentDetailDTO()
                {
                    UnknownPaymentID = model.UnknownPayment.ID,
                    UnknownPaymentCode = model.UnknownPayment.UnknownPaymentCode,
                    ReceiveDate = model.UnknownPayment.ReceiveDate,
                    Company = CompanyDropdownDTO.CreateFromModel(model.Company),

                    Project = ProjectDTO.CreateFromModel(model.Project),
                    Unit = UnitDTO.CreateFromModel(model.Unit),

                    BankAccount = BankAccountDropdownDTO.CreateFromModel(model.BankAccount),
                    Amount = model.UnknownPayment.Amount
                };

                var PaymentUnknownPaymentList = DB.PaymentUnknownPayments
                    .Include(o => o.PaymentMethod)
                        .ThenInclude(o => o.Payment)
                    .Where(o => o.UnknownPaymentID == model.UnknownPayment.ID && o.IsDeleted == false).ToList() ?? new List<PaymentUnknownPayment>();

                result.AmountBalance = result.Amount - PaymentUnknownPaymentList.Sum(o => o.PaymentMethod.Payment.TotalAmount);

                var detailQuery = from o in DB.PaymentUnknownPayments
                                     .Include(o => o.PaymentMethod)
                                         .ThenInclude(o => o.Payment)
                                             .ThenInclude(o => o.Booking)
                                                 .ThenInclude(o => o.Unit)
                                                     .ThenInclude(o => o.Project)
                                     .Include(o => o.CreatedBy)

                                  join ag in DB.Agreements on o.PaymentMethod.Payment.Booking.ID equals ag.BookingID into agData
                                  from agModel in agData.DefaultIfEmpty()

                                  join rth in DB.ReceiptTempHeaders on o.PaymentMethod.Payment.ID equals rth.PaymentID into rthData
                                  from rthModel in rthData.DefaultIfEmpty()

                                  select new UnknownPaymentReverseQueryResult
                                  {
                                      PaymentUnknownPayment = o,

                                      PaymentMethod = o.PaymentMethod,
                                      Payment = o.PaymentMethod.Payment,
                                      Booking = o.PaymentMethod.Payment.Booking,
                                      Project = o.PaymentMethod.Payment.Booking.Project,
                                      Unit = o.PaymentMethod.Payment.Booking.Unit,
                                      Agreement = agModel ?? new Agreement(),

                                      ReceiptTempHeader = rthModel ?? new ReceiptTempHeader(),

                                      //UserReverse = o.CreatedBy
                                  };

                detailQuery = detailQuery.Where(o => o.PaymentUnknownPayment.UnknownPaymentID == result.UnknownPaymentID);

                var detailData = detailQuery.ToList();

                result.UnknownPaymentReverseList = new List<UnknownPaymentReverseDTO>();

                foreach (var item in detailData)
                {
                    var detailModel = new UnknownPaymentReverseDTO();

                    detailModel.Id = item.PaymentUnknownPayment.ID;

                    detailModel.ReferentNo = (item.Agreement.AgreementNo ?? "") == "" ? item.Booking.BookingNo : item.Agreement.AgreementNo;
                    detailModel.ReverseProject = ProjectDTO.CreateFromModel(item.Project);
                    detailModel.ReverseUnit = UnitDTO.CreateFromModel(item.Unit);

                    detailModel.ReverseDate = item.PaymentUnknownPayment.Created;
                    detailModel.ReverseAmount = item.PaymentMethod.PayAmount;
                    detailModel.ReceiptTempNo = item.ReceiptTempHeader.ReceiptTempNo;
                    //CancelRemark =  ???? ,

                    result.UnknownPaymentReverseList.Add(detailModel);
                }

                return result;
            }
            else
            {
                return null;
            }
        }

    }

    public class UnknownPaymentReverseDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่จอง/สัญญา
        /// </summary>
        [Description("เลขที่จอง/สัญญา")]
        public string ReferentNo { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        public ProjectDTO ReverseProject { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        public UnitDTO ReverseUnit { get; set; }

        /// <summary>
        /// วันที่กลับรายการ
        /// </summary>
        [Description("วันที่กลับรายการ")]
        public DateTime? ReverseDate { get; set; }

        /// <summary>
        /// จำนวนเงินที่กลับรายการ
        /// </summary>
        [Description("จำนวนเงินที่กลับรายการ")]
        public decimal ReverseAmount { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จ
        /// </summary>
        [Description("เลขที่ใบเสร็จ")]
        public string ReceiptTempNo { get; set; }

        /// <summary>
        /// เลขที่ RVNumber
        /// </summary>
        [Description("เลขที่ RVNumber")]
        public string RVNumber { get; set; }

        /// <summary>
        /// หมายเหตุยกเลิก
        /// </summary>
        [Description("หมายเหตุยกเลิก")]
        public string CancelRemark { get; set; }

    }

    public class UnknownPaymentReverseQueryResult
    {
        public PaymentUnknownPayment PaymentUnknownPayment { get; set; }
        public User UserReverse { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        public Payment Payment { get; set; }

        public Booking Booking { get; set; }
        public Project Project { get; set; }
        public Unit Unit { get; set; }
        public Agreement Agreement { get; set; }

        public ReceiptTempHeader ReceiptTempHeader { get; set; }
    }

}
