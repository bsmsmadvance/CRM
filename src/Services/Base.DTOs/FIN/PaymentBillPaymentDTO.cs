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
using Database.Models.CTM;
using System.Collections.Generic;
using Base.DTOs.USR;
using Base.DTOs.SAL;

namespace Base.DTOs.FIN
{
    public class PaymentBillPaymentDTO : BaseDTO
    {
        [Description("ผูกช่องทางการชำระเงิน")]
        public PaymentMethodDTO PaymentMethod { get; set; }

        //[Description("ผูกข้อมูลรายการผลการชำระเงิน")]
        //public BillPaymentDetailDTO BillPaymentDetail { get; set; }

        [Description("ผิดบัญชี")]
        public bool IsWrongAccount { get; set; }


        [Description("เลขที่สัญญา")]
        public AgreementDropdownDTO Agreement { get; set; }

        /// <summary>
        /// ใบจอง
        /// </summary>
        [Description("ใบจอง")]
        public BookingForBillPaymentDTO Booking { get; set; }

        /// <summary>
        /// เลขที่แปลง
        /// </summary>
        [Description("เลขที่แปลง")]
        public UnitDropdownDTO Unit { get; set; }

        [Description("โครงการ")]
        public ProjectDropdownDTO Project { get; set; }

        public static List<PaymentBillPaymentDTO> CreateFromModel(List<PaymentBillPayment> model, DatabaseContext DB)
        {
            if (model != null)
            {
                List<PaymentBillPaymentDTO> result = new List<PaymentBillPaymentDTO>();
                foreach (var item in model)
                {
                    var Agreement = DB.Agreements.Where(x => x.BookingID == item.PaymentMethod.Payment.Booking.ID).FirstOrDefault() ?? null;
                    PaymentBillPaymentDTO newModel = new PaymentBillPaymentDTO()
                    {
                        Id = item.ID,
                        Agreement = AgreementDropdownDTO.CreateFromModel(Agreement),
                        //Booking = BookingForBillPaymentDTO.CreateFromModel(item.PaymentMethod.Payment.Booking, item.PaymentMethod.Payment.TotalAmount),
                        Unit = UnitDropdownDTO.CreateFromModel(item.PaymentMethod.Payment.Booking.Unit),
                        Project = ProjectDropdownDTO.CreateFromModel(item.PaymentMethod.Payment.Booking.Unit.Project),
                        UpdatedBy = item.UpdatedBy.DisplayName,
                        IsWrongAccount = item.IsWrongAccount

                    };
                    result.Add(newModel);
                }
                return result;
            }
            else
            {
                return null;

            }

        }

    }
}
