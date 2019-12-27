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
using Database.Models.USR;
using Base.DTOs.USR;
using Base.DTOs.SAL;

namespace Base.DTOs.FIN
{
    public class BillPaymentHeaderDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ BatchID
        /// </summary>
        [Description("เลขที่ BatchID")]
        public string BatchID { get; set; }

        /// <summary>
        /// วันที่เงินเข้า/วันที่ชำระเงิน
        /// </summary>
        [Description("วันที่เงินเข้า/วันที่ชำระเงิน")]
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// วันที่โหลด
        /// </summary>
        [Description("วันที่โหลด")]
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// ชื่อผู้โหลด
        /// </summary>
        [Description("ชื่อผู้โหลด")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// บริษัท
        /// </summary>
        [Description("บริษัท")]
        public CompanyDropdownDTO Company { get; set; }

        /// <summary>
        /// ธนาคาร 
        /// </summary>
        [Description("ธนาคาร")]
        public BankDropdownDTO Bank { get; set; }

        /// <summary>
        /// บัญชีธนาคาร
        /// </summary>
        [Description("บัญชีธนาคาร")]
        public BankAccountDropdownDTO BankAccount { get; set; }

        /// <summary>
        /// ช่วงเลขที่ BatchID ของ Detail
        /// </summary>
        [Description("เลขที่ เริ่มต้น-สิ้นสุด")]
        public string BatchRange { get; set; }

        /// <summary>
        /// จำนวนเงินรวม
        /// </summary>
        [Description("จำนวนเงินรวม")]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// จำนวนรายการ
        /// </summary>
        [Description("จำนวนรายการ")]
        public int TotalRecord { get; set; }

        /// <summary>
        /// จำนวนรายการที่สำเร็จ
        /// </summary>
        [Description("จำนวนรายการที่สำเร็จ")]
        public int TotalRecordDone { get; set; }

        /// <summary>
        /// จำนวนรายการที่ไม่พบ
        /// </summary>
        [Description("จำนวนรายการที่ไม่พบ")]
        public int TotalRecordWiat { get; set; }

        /// <summary>
        /// ข้อมูลรายละเอียด
        /// </summary>
        [Description("จำนวนรายการที่ไม่พบ")]
        public BillPaymentDetailDTO BillPaymentDetails { get; set; }

        [Description("ชื่อ Text file ที่ Import")]

        public string ImportFileName { get; set; }

        public List<PaymentBillPaymentDTO> PaymentBillPayments { get; set; }

        public class BillPaymentQueryResult
        {
            public BillPaymentHeader BillPaymentHeader { get; set; }
            public BankAccount BankAccount { get; set; }
            public Bank Bank { get; set; }
            public int countDone { get; set; }
            public int countWiat { get; set; }
            public BillPaymentDetail BillPaymentDetail { get; set; }
            public Agreement Agreement { get; set; }
            public PaymentMethod PaymentMethods { get; set; }
            //public string strAgreementNo { get; set; }
            //public string strUnitNo { get; set; }
            //public string strProject { get; set; }
            //public string strProjectID { get; set; }
        }

        public class BillPaymentTampQueryResult
        {
            public BillPaymentHeaderTemp BillPaymentHeader { get; set; }
            public BankAccount BankAccount { get; set; }
            public Bank Bank { get; set; }
            public int countDone { get; set; }
            public int countWiat { get; set; }
            public BillPaymentDetailTemp BillPaymentDetail { get; set; }

            public Agreement Agreement { get; set; }
            public AgreementOwner AgreementOwner { get; set; }
        }


        public class importBillPaymentQueryResult
        {
            public Booking Booking { get; set; }
            public Agreement Agreement { get; set; }
            public AgreementOwner AgreementOwner { get; set; }
            public Transfer Transfer { get; set; }
        }



        public static void SortBy(BillPaymentHeaderSortByParam sortByParam, ref IQueryable<BillPaymentQueryResult> query)
        {
            if (query != null)
            {
                if (sortByParam.SortBy != null)
                {
                    switch (sortByParam.SortBy.Value)
                    {

                        case BillPaymentSortBy.BatchID:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentHeader.BatchID);
                            else query = query.OrderByDescending(o => o.BillPaymentHeader.BatchID);
                            break;
                        case BillPaymentSortBy.Bank:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.Alias);
                            else query = query.OrderByDescending(o => o.Bank.Alias);
                            break;

                        //case BillPaymentSortBy.ReceiveDate:
                        //    if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentHeader.ReceiveDate);
                        //    else query = query.OrderByDescending(o => o.BillPaymentHeader.ReceiveDate);
                        //    break;
                        case BillPaymentSortBy.CreateDate:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentHeader.Created);
                            else query = query.OrderByDescending(o => o.BillPaymentHeader.Created);
                            break;
                        case BillPaymentSortBy.TotalRecord:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentHeader.TotalRecord);
                            else query = query.OrderByDescending(o => o.BillPaymentHeader.TotalRecord);
                            break;
                        case BillPaymentSortBy.TotalSuccessRecord:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.countDone);
                            else query = query.OrderByDescending(o => o.countDone);
                            break;
                        case BillPaymentSortBy.TotalWatingRecord:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.countWiat);
                            else query = query.OrderByDescending(o => o.countWiat);
                            break;
                        case BillPaymentSortBy.TotalAmount:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentHeader.TotalAmount);
                            else query = query.OrderByDescending(o => o.BillPaymentHeader.TotalAmount);
                            break;
                        case BillPaymentSortBy.ImportBy:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentHeader.CreatedBy);
                            else query = query.OrderByDescending(o => o.BillPaymentHeader.CreatedBy);
                            break;
                        default:
                            query = query.OrderBy(o => o.BillPaymentHeader.Created);
                            break;
                    }
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentHeader.Created);
                    else query = query.OrderByDescending(o => o.BillPaymentHeader.Created);
                }
                else
                {
                    query = query.OrderBy(o => o.BillPaymentHeader.Created);
                }
            }
        }
        public static void SortByTamp(BillPaymentHeaderSortByParam sortByParam, ref IQueryable<BillPaymentTampQueryResult> query)
        {
            if (query != null)
            {
                if (sortByParam.SortBy != null)
                {
                    switch (sortByParam.SortBy.Value)
                    {

                        case BillPaymentSortBy.BatchID:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentHeader.BatchID);
                            else query = query.OrderByDescending(o => o.BillPaymentHeader.BatchID);
                            break;
                        case BillPaymentSortBy.Bank:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.Alias);
                            else query = query.OrderByDescending(o => o.Bank.Alias);
                            break;

                        //case BillPaymentSortBy.ReceiveDate:
                        //    if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentHeader.ReceiveDate);
                        //    else query = query.OrderByDescending(o => o.BillPaymentHeader.ReceiveDate);
                        //    break;
                        case BillPaymentSortBy.CreateDate:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentHeader.Created);
                            else query = query.OrderByDescending(o => o.BillPaymentHeader.Created);
                            break;
                        case BillPaymentSortBy.TotalRecord:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentHeader.TotalRecord);
                            else query = query.OrderByDescending(o => o.BillPaymentHeader.TotalRecord);
                            break;
                        case BillPaymentSortBy.TotalSuccessRecord:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.countDone);
                            else query = query.OrderByDescending(o => o.countDone);
                            break;
                        case BillPaymentSortBy.TotalWatingRecord:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.countWiat);
                            else query = query.OrderByDescending(o => o.countWiat);
                            break;
                        case BillPaymentSortBy.TotalAmount:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentHeader.TotalAmount);
                            else query = query.OrderByDescending(o => o.BillPaymentHeader.TotalAmount);
                            break;
                        case BillPaymentSortBy.ImportBy:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentHeader.CreatedBy);
                            else query = query.OrderByDescending(o => o.BillPaymentHeader.CreatedBy);
                            break;
                        default:
                            query = query.OrderBy(o => o.BillPaymentHeader.Created);
                            break;
                    }
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.BillPaymentHeader.Created);
                    else query = query.OrderByDescending(o => o.BillPaymentHeader.Created);
                }
                else
                {
                    query = query.OrderBy(o => o.BillPaymentHeader.Created);
                }
            }
        }




        public static BillPaymentHeaderDTO CreateFromModel(BillPaymentQueryResult model, DatabaseContext DB, bool chkPaymentBillPayments = false)
        {
            if (model != null)
            {
                BillPaymentHeaderDTO result = new BillPaymentHeaderDTO()
                {
                    Id = model.BillPaymentHeader.ID,
                    BatchID = model.BillPaymentHeader.BatchID,
                    ReceiveDate = model.BillPaymentHeader.ReceiveDate,
                    CreateDate = model.BillPaymentHeader.Created,
                    CreatedBy = model.BillPaymentHeader.CreatedBy != null ? model.BillPaymentHeader.CreatedBy.DisplayName : null,
                    Updated = model.BillPaymentHeader.Updated,
                    //UpdatedBy = UserDTO.CreateFromModel(model.BillPaymentHeader.UpdatedBy).,
                    Company = CompanyDropdownDTO.CreateFromModel(model.BillPaymentHeader.BankAccount.Company),
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    BankAccount = BankAccountDropdownDTO.CreateFromModel(model.BankAccount),
                    //BatchRange = 
                    TotalAmount = model.BillPaymentHeader.TotalAmount,
                    TotalRecord = model.BillPaymentHeader.TotalRecord,
                    TotalRecordDone = model.countDone,
                    TotalRecordWiat = model.countWiat,
                    BillPaymentDetails = new BillPaymentDetailDTO(),
                    PaymentBillPayments = new List<PaymentBillPaymentDTO>(),
                    ImportFileName = model.BillPaymentHeader.ImportFileName
                };
                if (model.BillPaymentDetail != null)
                {
                    result.BillPaymentDetails = BillPaymentDetailDTO.CreateFromModel(model.BillPaymentDetail, DB);
                    if (chkPaymentBillPayments)
                    {
                        var PaymentBillPaymentModel = DB.PaymentBillPayments.Where(x => x.BillPaymentDetailID == model.BillPaymentDetail.ID)
                              .Include(x => x.PaymentMethod)
                                  .ThenInclude(x => x.Payment)
                                      .ThenInclude(x => x.Booking)
                                          .ThenInclude(x => x.Unit)
                                              .ThenInclude(x => x.Project)
                                                    .ThenInclude(x => x.Company).ToList() ?? null;

                        if (PaymentBillPaymentModel.Select(x => x.PaymentMethod.Payment.Booking) != null)
                        {
                            result.PaymentBillPayments = PaymentBillPaymentDTO.CreateFromModel(PaymentBillPaymentModel, DB);
                        }
                    }


                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public static BillPaymentHeaderDTO CreateFromModelTamp(BillPaymentTampQueryResult model, DatabaseContext DB)
        {
            if (model != null)
            {
                BillPaymentHeaderDTO result = new BillPaymentHeaderDTO()
                {
                    Id = model.BillPaymentHeader.ID,
                    BatchID = model.BillPaymentHeader.BatchID,
                    //ReceiveDate = model.BillPaymentHeader.ReceiveDate,
                    CreateDate = model.BillPaymentHeader.Created,
                    CreatedBy = model.BillPaymentHeader.CreatedBy.DisplayName,

                    //Company
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    BankAccount = BankAccountDropdownDTO.CreateFromModel(model.BankAccount),
                    Company = CompanyDropdownDTO.CreateFromModel(model.BankAccount.Company),
                    //BatchRange = 
                    TotalAmount = model.BillPaymentHeader.TotalAmount,
                    TotalRecord = model.BillPaymentHeader.TotalRecord,
                    TotalRecordDone = model.countDone,
                    TotalRecordWiat = model.countWiat,
                    BillPaymentDetails = new BillPaymentDetailDTO()
                };
                if (model.BillPaymentDetail != null)
                {
                    result.BillPaymentDetails = BillPaymentDetailDTO.CreateFromModelTamp(model.BillPaymentDetail, model.Agreement, DB);
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        //public static BillPaymentHeaderDTO CreateFromModelSplit(BillPaymentTampQueryResult model)
        //{
        //    if (model != null)
        //    {
        //        BillPaymentHeaderDTO result = new BillPaymentHeaderDTO()
        //        {
        //            Id = model.BillPaymentHeader.ID,
        //            BatchID = model.BillPaymentHeader.BatchID,
        //            //ReceiveDate = model.BillPaymentHeader.ReceiveDate,
        //            CreateDate = model.BillPaymentHeader.Created,
        //            CreatedBy = UserDTO.CreateFromModel(model.BillPaymentHeader.CreatedBy),

        //            //Company
        //            Bank = BankDropdownDTO.CreateFromModel(model.Bank),
        //            BankAccount = BankAccountDropdownDTO.CreateFromModel(model.BankAccount),
        //            Company = CompanyDropdownDTO.CreateFromModel(model.BankAccount.Company),
        //            //BatchRange = 
        //            TotalAmount = model.BillPaymentHeader.TotalAmount,
        //            TotalRecord = model.BillPaymentHeader.TotalRecord,
        //            TotalRecordDone = model.countDone,
        //            TotalRecordWiat = model.countWiat,
        //            BillPaymentDetails = new BillPaymentDetailDTO()
        //        };
        //        if (model.BillPaymentDetail != null)
        //        {
        //            result.BillPaymentDetails = BillPaymentDetailDTO.CreateFromModelTamp(model.BillPaymentDetail, model.Agreement);
        //        }
        //        return result;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public static BillPaymentHeader ToModel(BillPaymentHeaderTemp model)
        {
            if (model != null)
            {
                BillPaymentHeader result = new BillPaymentHeader()
                {
                    ID = model.ID,
                    BatchID = model.BatchID,
                    BankAccountID = model.BankAccountID,
                    ImportFileName = model.ImportFileName,
                    BillPaymentImportTypeMasterCenterID = model.BillPaymentImportTypeMasterCenterID,
                    TotalAmount = model.TotalAmount,
                    TotalRecord = model.TotalRecord,
                    ReceiveDate = model.ReceiveDate,
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public class listRef
        {
            public string Ref1 { get; set; }
            public string Ref2 { get; set; }
            public string Ref3 { get; set; }
            public DateTime Date { get; set; }
            public string CustomerName { get; set; }
            public decimal Amount { get; set; }
        }


        public async Task ValidateAsync(DatabaseContext db)
        {
            var BillPaymentDetail = new BillPaymentDetailDTO();
            ValidateException ex = new ValidateException();
            var newGuid = Guid.NewGuid();

            var masterCenterModel = db.MasterCenters.Where(o => o.ID == this.BillPaymentDetails.BillPaymentStatus.Id).ToList() ?? new List<MasterCenter>();
            if (!masterCenterModel.Any())
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BillPaymentHeaderDTO.BillPaymentDetails.BillPaymentStatus)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            var Bank = db.BankAccounts.Where(o => o.ID == this.BankAccount.Id).ToList() ?? new List<BankAccount>();
            if (!Bank.Any())
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BillPaymentHeaderDTO.BankAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            //var BillPaymentsID = db.BillPaymentTemps.Where(o => o.ID == this.Id).ToList() ?? new List<BillPaymentHeaderTemp>();
            //if (!BillPaymentsID.Any())
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(BillPaymentHeaderDTO.BankAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}

            var ChkBooking = db.Bookings.Where(o => this.BillPaymentDetails.Booking.Id == o.ID).FirstOrDefault() ?? null;
            if (ChkBooking == null)

            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = BillPaymentDetail.GetType().GetProperty(nameof(BillPaymentDetail.Booking)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (this.BillPaymentDetails.ReceiveDate == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = BillPaymentDetail.GetType().GetProperty(nameof(BillPaymentDetail.ReceiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.BillPaymentDetails.DetailBatchID == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = BillPaymentDetail.GetType().GetProperty(nameof(BillPaymentDetail.DetailBatchID)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            //if (this.BillPaymentDetails.PayType == null)
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(BillPaymentHeaderDTO.BillPaymentDetails.DetailBatchID)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}

            if (this.BillPaymentDetails.PayAmount <= 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = BillPaymentDetail.GetType().GetProperty(nameof(BillPaymentDetail.PayAmount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (this.BillPaymentDetails.CustomerName == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = BillPaymentDetail.GetType().GetProperty(nameof(BillPaymentDetail.CustomerName)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public async Task ValidateTempAsync(DatabaseContext db)
        {
            var BillPaymentDetail = new BillPaymentDetailDTO();
            ValidateException ex = new ValidateException();
            var newGuid = Guid.NewGuid();

            var masterCenterModel = db.MasterCenters.Where(o => o.ID == this.BillPaymentDetails.BillPaymentStatus.Id).ToList() ?? new List<MasterCenter>();
            if (!masterCenterModel.Any())
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BillPaymentHeaderDTO.BillPaymentDetails.BillPaymentStatus)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            var Bank = db.BankAccounts.Where(o => o.ID == this.BankAccount.Id).ToList() ?? new List<BankAccount>();
            if (!Bank.Any())
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BillPaymentHeaderDTO.BankAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            //var BillPaymentsID = db.BillPaymentTemps.Where(o => o.ID == this.Id).ToList() ?? new List<BillPaymentHeaderTemp>();
            //if (!BillPaymentsID.Any())
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(BillPaymentHeaderDTO.BankAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}

            //var ChkBooking = db.Bookings.Where(o => this.BillPaymentDetails.Booking.Id == o.ID).FirstOrDefault() ?? null;
            //if (ChkBooking == null)
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = BillPaymentDetail.GetType().GetProperty(nameof(BillPaymentDetail.Booking)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}

            if (this.BillPaymentDetails.ReceiveDate == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = BillPaymentDetail.GetType().GetProperty(nameof(BillPaymentDetail.ReceiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.BillPaymentDetails.DetailBatchID == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = BillPaymentDetail.GetType().GetProperty(nameof(BillPaymentDetail.DetailBatchID)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            //if (this.BillPaymentDetails.PayType == null)
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(BillPaymentHeaderDTO.BillPaymentDetails.DetailBatchID)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}

            if (this.BillPaymentDetails.PayAmount <= 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = BillPaymentDetail.GetType().GetProperty(nameof(BillPaymentDetail.PayAmount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (this.BillPaymentDetails.CustomerName == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = BillPaymentDetail.GetType().GetProperty(nameof(BillPaymentDetail.CustomerName)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }
    }
}