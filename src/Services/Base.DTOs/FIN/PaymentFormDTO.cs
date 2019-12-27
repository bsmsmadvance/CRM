using Database.Models;
using Database.Models.FIN;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using ErrorHandling;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.FIN
{
    public class PaymentFormDTO
    {
        /// <summary>
        /// โครงการ
        /// Project/api/Projects/DropdownList
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// แปลง
        /// Project/api/Projects/{projectID}/Units/DropdownList
        /// </summary>
        public PRJ.UnitDropdownDTO Unit { get; set; }
        /// <summary>
        /// รายการที่ต้องชำระ
        /// </summary>
        public List<PaymentUnitPriceItemDTO> PaymentItems { get; set; }
        /// <summary>
        /// วิธีการชำระ
        /// </summary>
        public List<PaymentMethodDTO> PaymentMethods { get; set; }
        /// <summary>
        /// ชนิดของช่องทางชำระที่สามารถชำระได้
        /// </summary>
        public List<MST.MasterCenterDropdownDTO> AuthorizedPaymentMethodTypes { get; set; }
        /// <summary>
        /// วันที่ชำระ
        /// </summary>
        [Description("วันที่ชำระ")]
        public DateTime ReceiveDate { get; set; }
        /// <summary>
        /// ไฟล์แนบ
        /// </summary>
        public FileDTO AttachFile { get; set; }
        /// <summary>
        /// หมายเหตุ (บันทึกข้อความ)
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// ชนิดของ PaymentForm
        /// </summary>
        public PaymentFormType PaymentFormType { get; set; }
        /// <summary>
        /// สามารถเพิ่ม Payment Method ใหม่ได้
        /// </summary>
        public bool CanAddNewPaymentMethod { get; set; }
        /// <summary>
        /// สามารถแก้ไขยอดชำระเงินได้
        /// </summary>
        public bool CanEditPayAmount { get; set; }
        /// <summary>
        /// ID ของ UnknownPayment หรืออื่นๆ
        /// </summary>
        public Guid? RefID { get; set; }

        public bool? IsWrongAccount { get; set; }


        //public static async Task<PaymentFormDTO> CreateFromModelAsync(Payment model, DatabaseContext db, FileHelper fileHelper, Guid? refID = null, PaymentFormType formType = PaymentFormType.Normal, decimal paidAmount = 0)
        //{
        //    if (model != null)
        //    {
        //        var result = new PaymentFormDTO();
        //        result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Booking.Project);
        //        result.Remark = model.Remark;
        //        result.ReceiveDate = model.ReceiveDate;
        //        result.Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Booking.Unit);
        //        result.AttachFile = await FileDTO.CreateFromFileNameAsync(model.AttachFile, fileHelper);
        //        result.RefID = refID;
        //        result.CanAddNewPaymentMethod = formType == PaymentFormType.Normal ? true : false;
        //        result.CanEditPayAmount = formType == PaymentFormType.Normal ? true : false;
        //        result.AuthorizedPaymentMethodTypes = new List<MST.MasterCenterDropdownDTO>();

        //        if (formType == PaymentFormType.Normal)
        //        {
        //            var allPaymentBooking = PaymentMethodKeys.NeedToDepositKeys;
        //            var masterCenterAuthorizeds = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
        //                                                                        && allPaymentBooking.Contains(o.Key))
        //                                                                .ToListAsync();
        //            result.AuthorizedPaymentMethodTypes.AddRange(masterCenterAuthorizeds.Select(o => MST.MasterCenterDropdownDTO.CreateFromModel(o)).ToList());
        //        }
        //        else if (formType == PaymentFormType.UnknownPayment)
        //        {
        //            var masterCenterAuthorized = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
        //                                                                        && o.Key == PaymentMethodKeys.UnknowPayment).FirstAsync();
        //            result.AuthorizedPaymentMethodTypes.Add(MST.MasterCenterDropdownDTO.CreateFromModel(masterCenterAuthorized));
        //            result.PaymentMethods = new List<PaymentMethodDTO> { new PaymentMethodDTO { PayAmount = paidAmount, PaymentMethodType = MST.MasterCenterDropdownDTO.CreateFromModel(masterCenterAuthorized) } };
        //        }

        //        return result;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public static async Task<PaymentFormDTO> CreateFromBookingAsync(Booking model, DatabaseContext db, FileHelper fileHelper, Guid? refID = null, PaymentFormType formType = PaymentFormType.Normal, decimal paidAmount = 0)
        {
            if (model != null)
            {
                var result = new PaymentFormDTO();
                result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project);
                result.Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit);
                result.PaymentItems = new List<PaymentUnitPriceItemDTO>();
                result.PaymentMethods = new List<PaymentMethodDTO>();
                result.RefID = refID;
                result.CanAddNewPaymentMethod = formType == PaymentFormType.Normal ? true : false;
                result.CanEditPayAmount = formType == PaymentFormType.Normal ? true : false;
                result.AuthorizedPaymentMethodTypes = new List<MST.MasterCenterDropdownDTO>();

                if (formType == PaymentFormType.Normal)
                {
                    var allPaymentBooking = PaymentMethodKeys.NeedToDepositKeys;
                    var masterCenterAuthorizeds = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod && allPaymentBooking.Contains(o.Key))
                                                             .ToListAsync();
                    result.AuthorizedPaymentMethodTypes.AddRange(masterCenterAuthorizeds.Select(o => MST.MasterCenterDropdownDTO.CreateFromModel(o)).ToList());
                }
                else if (formType == PaymentFormType.UnknownPayment)
                {
                    var masterCenterAuthorized = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                                && o.Key == PaymentMethodKeys.UnknowPayment).FirstAsync();
                    result.AuthorizedPaymentMethodTypes.Add(MST.MasterCenterDropdownDTO.CreateFromModel(masterCenterAuthorized));
                    result.PaymentMethods = new List<PaymentMethodDTO> { new PaymentMethodDTO { PayAmount = paidAmount, PaymentMethodType = MST.MasterCenterDropdownDTO.CreateFromModel(masterCenterAuthorized) } };
                }

                #region UnitPriceItem ที่เอาไปแสดง


                #region ค่าบ้าน

                var listMasterPriceItemKeys = new List<Guid>();
                listMasterPriceItemKeys.Add(MasterPriceItemKeys.BookingAmount);
                listMasterPriceItemKeys.Add(MasterPriceItemKeys.ContractAmount);
                listMasterPriceItemKeys.Add(MasterPriceItemKeys.DownAmount);

                var paymentIDs = await db.Payments.Where(o => o.BookingID == model.ID).Select(o => o.ID).ToListAsync();
                var sumPayAmount = await db.PaymentItems.Where(o => paymentIDs.Contains(o.PaymentID) && listMasterPriceItemKeys.Contains((Guid)o.MasterPriceItemID)).SumAsync(o => o.PayAmount);
                var payAmount = sumPayAmount;
                var unitPrice = await db.UnitPrices.Where(o => o.BookingID == model.ID && o.IsActive == true).FirstAsync();
                var unitPriceItems = await db.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID && listMasterPriceItemKeys.Contains((Guid)o.MasterPriceItemID)).Include(o => o.MasterPriceItem).OrderBy(o => o.Order).ToListAsync();

                int i = 0;
                int j = 0;
                var order = 1;
                foreach (var item in unitPriceItems)
                {
                    if (sumPayAmount > 0)
                    {
                        i++;
                        if (item.MasterPriceItemID != MasterPriceItemKeys.DownAmount)
                        {
                            if (sumPayAmount - item.Amount >= 0)
                            {
                                result.PaymentItems.Add(PaymentUnitPriceItemDTO.CreateFromUnitPriceItemModel(item, item.Amount, order));
                                order++;
                            }
                            else if (sumPayAmount - item.Amount < 0)
                            {
                                var amount = sumPayAmount;
                                result.PaymentItems.Add(PaymentUnitPriceItemDTO.CreateFromUnitPriceItemModel(item, sumPayAmount, order));
                                i++;
                                order++;
                                break;
                            }
                            sumPayAmount -= item.Amount;
                        }
                        if (item.MasterPriceItemID == MasterPriceItemKeys.DownAmount)
                        {
                            var unitPriceInstallments = await db.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == item.ID)
                                                                                      .Include(o => o.InstallmentOfUnitPriceItem)
                                                                                          .ThenInclude(o => o.MasterPriceItem)
                                                                                      .OrderBy(o => o.Period)
                                                                                      .ToListAsync();
                            foreach (var installment in unitPriceInstallments)
                            {
                                j++;
                                if (sumPayAmount - installment.Amount >= 0)
                                {
                                    result.PaymentItems.Add(PaymentUnitPriceItemDTO.CreateFromUnitPriceInstallmentModel(installment, installment.Amount, order));
                                    order++;
                                }
                                else if (sumPayAmount - installment.Amount < 0)
                                {
                                    var amount = sumPayAmount;
                                    result.PaymentItems.Add(PaymentUnitPriceItemDTO.CreateFromUnitPriceInstallmentModel(installment, sumPayAmount, order));
                                    j++;
                                    order++;
                                    break;
                                }
                                sumPayAmount -= installment.Amount;
                            }
                        }
                    }
                    else
                    {
                        i++;
                        if (item.MasterPriceItemID != MasterPriceItemKeys.DownAmount)
                        {
                            result.PaymentItems.Add(PaymentUnitPriceItemDTO.CreateFromUnitPriceItemModel(item, 0, order));
                            order++;
                        }
                        if (item.MasterPriceItemID == MasterPriceItemKeys.DownAmount)
                        {
                            var unitPriceInstallments = await db.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == item.ID)
                                                                                         .Include(o => o.InstallmentOfUnitPriceItem)
                                                                                             .ThenInclude(o => o.MasterPriceItem)
                                                                                         .OrderBy(o => o.Period)
                                                                                         .ToListAsync();
                            foreach (var installment in unitPriceInstallments)
                            {
                                result.PaymentItems.Add(PaymentUnitPriceItemDTO.CreateFromUnitPriceInstallmentModel(installment, 0, order));
                                order++;
                            }
                        }
                    }
                }
                if (i != 0)
                {
                    for (int a = i - 1; a < unitPriceItems.Count(); a++)
                    {
                        if (unitPriceItems[a].MasterPriceItemID != MasterPriceItemKeys.DownAmount)
                        {
                            result.PaymentItems.Add(PaymentUnitPriceItemDTO.CreateFromUnitPriceItemModel(unitPriceItems[a], 0, order));
                            order++;
                        }
                        if (unitPriceItems[a].MasterPriceItemID == MasterPriceItemKeys.DownAmount)
                        {
                            var unitPriceInstallments = await db.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == unitPriceItems[a].ID)
                                                                                         .Include(o => o.InstallmentOfUnitPriceItem)
                                                                                             .ThenInclude(o => o.MasterPriceItem)
                                                                                         .OrderBy(o => o.Period)
                                                                                         .ToListAsync();
                            if (j != 0)
                            {
                                for (int b = j - 1; b < unitPriceInstallments.Count(); b++)
                                {
                                    result.PaymentItems.Add(PaymentUnitPriceItemDTO.CreateFromUnitPriceInstallmentModel(unitPriceInstallments[b], 0, order));
                                    order++;
                                }
                            }
                            else
                            {
                                for (int b = j; b < unitPriceInstallments.Count(); b++)
                                {
                                    result.PaymentItems.Add(PaymentUnitPriceItemDTO.CreateFromUnitPriceInstallmentModel(unitPriceInstallments[b], 0, order));
                                    order++;
                                }
                            }
                        }

                    }
                }
                #endregion

                #region ค่าบ้านส่วนที่เหลือ
                result.PaymentItems.Add(new PaymentUnitPriceItemDTO
                {
                    CanSelect = false,
                    ShowSelect = false,
                    Order = order,
                    Name = "ค่าบ้านส่วนที่เหลือ",
                    RemainAmount = unitPriceItems.Sum(o => o.Amount) - payAmount,
                    PaidAmount = payAmount,
                    ItemAmount = unitPriceItems.Sum(o => o.Amount),
                });
                #endregion


                #endregion

                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (this.ReceiveDate > DateTime.Now.AddDays(1))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR9999").FirstAsync();
                string desc = "ไม่สามารถเลือก วันที่รับชำระ เกินวันปัจจุบันได้";
                var msg = errMsg.Message.Replace("[message]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

    }

    public enum PaymentFormType
    {
        Normal = 0,
        UnknownPayment = 1,
        ChangeUnit = 2,
        DirectCredit = 3,
        DirectDebit = 4, 
        BillPayment = 5
    }
}
