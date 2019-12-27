using System;
using System.ComponentModel;
using System.Linq;
using Base.DTOs.FIN;
using Base.DTOs.MST;
using Database.Models;
using Database.Models.FIN;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using static Base.DTOs.FIN.BillPaymentDetailDTO;

namespace Base.DTOs.FIN
{
    public class SplitForBillPaymentDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ใบจอง
        /// </summary>
        public Guid BookingID { get; set; }

        /// <summary>
        /// ลบ
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// เลขที่ Agreement
        /// </summary>
        public string AgreementNo { get; set; }


        /// <summary>
        /// Project
        /// </summary>
        public string Project { get; set; }

        /// <summary>
        /// Project
        /// </summary>
        public string Unit { get; set; }


        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal Amount { get; set; }

        //public MasterCenterDropdownDTO DeleteReason { get; set; }

        ///// <summary>
        ///// Remark Delete
        ///// </summary>
        //public string RemarkDelete { get; set; }

        public static SplitForBillPaymentDTO CreateFromSplitModel(QueryResult model)
        {
            if (model != null)
            {
                SplitForBillPaymentDTO result = new SplitForBillPaymentDTO()
                {
                    Id = model.PaymentBillPayment.ID,
                    AgreementNo = model.Agreement.AgreementNo ?? model.PaymentBillPayment.PaymentMethod.Payment.Booking.BookingNo,
                    BookingID = model.PaymentBillPayment.PaymentMethod.Payment.Booking.ID,
                    Unit = model.PaymentBillPayment.PaymentMethod.Payment.Booking.Unit.UnitNo,
                    Project   = model.PaymentBillPayment.PaymentMethod.Payment.Booking.Unit.Project.ProjectNo + "-" + model.PaymentBillPayment.PaymentMethod.Payment.Booking.Unit.Project.ProjectNameTH,
                    Amount = model.PaymentBillPayment.PaymentMethod.PayAmount,
                    //DeleteReason = new MasterCenterDropdownDTO(),
                    IsDelete = false,
                    //RemarkDelete = null
                };
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
