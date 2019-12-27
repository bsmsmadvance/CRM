using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.FIN;
using Database.Models.MST;
using Database.Models.SAL;
using System;
using System.ComponentModel;
using System.Linq;

namespace Base.DTOs.FIN
{
    public class DirectCreditDebitExportDetailDTO : BaseDTO
    {
        /// <summary>
        /// Code ที่ใช้ Referent ใน Textfile
        /// </summary>
        public string BatchID { get; set; }

        /// <summary>
        /// ลำดับรายการ
        /// </summary>
        public int Seq { get; set; }

        ///// <summary>
        ///// Booking
        ///// </summary>
        //public SAL.BookingDropdownDTO Booking { get; set; }

        /// <summary>
        /// UnitPriceInstallment งวดที่จะตัดเงินลูกค้า
        /// </summary>
        public PaymentUnitPriceItemDTO UnitPriceInstallment { get; set; }

        /// <summary>
        /// จำนวนเงินที่จะตัดเงินลูกค้า
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Code ผลลัพธ์จากธนาคาร
        /// </summary>
        public string TransCode { get; set; }

        /// <summary>
        /// สถานะตัดเงิน รอ Import Text File,ข้อมูลถูกต้อง,ไม่สามารถตัดเงินได้,ห้องโอนกรรมสิทธิ์แล้ว
        /// </summary>
        public MST.MasterCenterDropdownDTO DirectCreditDebitExportDetailStatus { get; set; }

        public ProjectDropdownDTO Project { get; set; }
        public string Unit { get; set; }
        public string AccountNO { get; set; }

        public DateTime? dueDate { get; set; }
        public string Agreement { get; set; }

        public string OwnerName { get; set; }
        public int? Period { get; set; }


        public static DirectCreditDebitExportDetailDTO CreateFromModel(DirectCreditDebitExportDetail model, Agreement Agreement)
        {
            if (model != null)
            {
               // var tAgreement = DB.Agreements.Where(x => x.BookingID == model.DirectCreditDebitApprovalForm.BookingID).Select(x => x.AgreementNo).FirstOrDefault();
                var result = new DirectCreditDebitExportDetailDTO()
                {
                    BatchID = model.BatchID,
                    Project = ProjectDropdownDTO.CreateFromModel(model.DirectCreditDebitApprovalForm.Booking.Unit.Project),
                    Unit = model.DirectCreditDebitApprovalForm.Booking.Unit.UnitNo,
                    AccountNO = model.DirectCreditDebitApprovalForm.AccountNO,
                    Period = model.DirectCreditDebitApprovalForm.DirectPeriod,
                    dueDate = model.UnitPriceInstallment.DueDate,
                    Agreement = Agreement.AgreementNo,
                    OwnerName = model.DirectCreditDebitApprovalForm.OwnerName,
                    Amount = model.UnitPriceInstallment.Amount - model.UnitPriceInstallment.PaidAmount,
                    DirectCreditDebitExportDetailStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.DirectCreditDebitExportDetailStatus),
                    //UnitPriceInstallment = PaymentUnitPriceItemDTO.CreateFromUnitPriceInstallmentModel(model.UnitPriceInstallment, model.UnitPriceInstallment.Amount, model.UnitPriceInstallment.Period),
                    Seq = model.Seq,
                    TransCode = model.TransCode,
                   
                };
                switch (result.DirectCreditDebitExportDetailStatus.Key.ToString())
                {
                    case "Fail":         //Status : Fail
                        result.DirectCreditDebitExportDetailStatus.Key = "0" + result.DirectCreditDebitExportDetailStatus.Key;
                        break;
                    case "TransferUnit": // Status : FaTransferUnitil
                        result.DirectCreditDebitExportDetailStatus.Key = "1" + result.DirectCreditDebitExportDetailStatus.Key;
                        break;
                    case "Complete":     // Status : Complete
                        result.DirectCreditDebitExportDetailStatus.Key = "2" + result.DirectCreditDebitExportDetailStatus.Key;
                        break;
                    case "Wait":         // Status : Wait
                        result.DirectCreditDebitExportDetailStatus.Key = "3" + result.DirectCreditDebitExportDetailStatus.Key;
                        break;
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
