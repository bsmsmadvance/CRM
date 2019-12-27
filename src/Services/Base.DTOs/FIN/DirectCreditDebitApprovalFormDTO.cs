using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.SAL;
using Database.Models;
using Database.Models.FIN;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.FIN
{
    public class DirectCreditDebitApprovalFormDTO : BaseDTO
    {
        /// <summary>
        /// ผูกกับสัญญา
        /// </summary>
        [Description("ผูกกับสัญญา")]
        public SAL.AgreementDropdownDTO Agreement { get; set; }

        /// <summary>
        /// ผู้ทำสัญญาหลัก
        /// </summary>
        [Description("ผู้ทำสัญญาหลัก")]
        public SAL.AgreementOwnerDropdownDTO AgreementOwner { get; set; }

        /// <summary>
        /// งวดที่จะต้องชำระเงินดาวน์ของสัญญานี้
        /// </summary>
        [Description("งวดที่จะต้องชำระเงินดาวน์ของสัญญานี้")]
        // public List<PaymentUnitPriceItemDTO> PaymentUnitPriceItem { get; set; }
        public List<PaymentUnitPriceItemDTO> UnitPriceInstallment { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        [Description("โครงการ")]
        public PRJ.ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// Unit
        /// </summary>
        [Description("Unit")]
        public PRJ.UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// ชนิดของแบบฟอร์ม Credit,Debit
        /// </summary>
        [Description("ชนิดของแบบฟอร์ม Credit,Debit")]
        public MST.MasterCenterDropdownDTO DirectApprovalFormType { get; set; }

        /// <summary>
        /// สถานะขออนุมัติ Direct Credit/Debit
        /// </summary>
        [Description("สถานะขออนุมัติ Direct Credit/Debit")]
        public MST.MasterCenterDropdownDTO DirectApprovalFormStatus { get; set; }

        /// <summary>
        /// รอบการตัดเงิน วันที่ 1 หรือ 15 ยังขาดวิธีการคำนวนหาว่าจะใช้รอบไหน
        /// </summary>
        [Description("รอบการตัดเงิน วันที่ 1 หรือ 15")]
        public int? DirectPeriod { get; set; }

        /// <summary>
        /// ธนาคารของลูกค้าที่เลือกจะตัดเงิน
        /// </summary>
        [Description("ธนาคารของลูกค้าที่เลือกจะตัดเงิน")]
        public BankAccNameDTO Bank { get; set; }

        /// <summary>
        /// สาขาธนาคาร
        /// </summary>
        [Description("สาขาธนาคาร")]
        public MST.BankBranchDropdownDTO BankBranch { get; set; }

        /// <summary>
        /// จังหวัด
        /// </summary>
        [Description("จังหวัด")]
        public MST.ProvinceListDTO Province { get; set; }

        /// <summary>
        /// เลขที่บัญชี/เลขที่บัตรเครดิต ของลูกค้า
        /// </summary>
        [Description("เลขที่บัญชี/เลขที่บัตรเครดิต ของลูกค้า")]
        public string AccountNO { get; set; }

        /// <summary>
        /// ปีที่หมดอายุบัตรเครดิต
        /// </summary>
        [Description("ปีที่หมดอายุบัตรเครดิต")]
        public int? CreditCardExpireYear { get; set; }

        /// <summary>
        /// เดือนที่หมดอายุบัตรเครดิต
        /// </summary>
        [Description("เดือนที่หมดอายุบัตรเครดิต")]
        public int? CreditCardExpireMonth { get; set; }

        /// <summary>
        /// ชื่อลูกค้าเจ้าของ บัญชี/บัตรเครดิต
        /// </summary>
        [Description("ชื่อลูกค้าเจ้าของ บัญชี/บัตรเครดิต")]
        public string OwnerName { get; set; }

        /// <summary>
        /// เลขที่บัตรประชาชนลูกค้าเจ้าของ บัญชี/บัตรเครดิต
        /// </summary>
        [Description("เลขที่บัตรประชาชนลูกค้าเจ้าของ บัญชี/บัตรเครดิต")]
        public string CitizenIdentityNo { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [Description("หมายเหตุ")]
        public string Remark { get; set; }

        /// <summary>
        /// วันที่เริ่มตัดเงิน
        /// </summary>
        [Description("วันที่เริ่มตัดเงิน")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// วันที่ธนาคารอนุมัติ
        /// </summary>
        [Description("วันที่ธนาคารอนุมัติ")]
        public DateTime? ApproveDate { get; set; }

        /// <summary>
        /// วันที่ ธนาคาร Reject Form
        /// </summary>
        [Description("วันที่ ธนาคาร Reject Form")]
        public DateTime? RejectDate { get; set; }

        /// <summary>
        /// วันที่ยกเลิก
        /// </summary>
        [Description("วันที่ยกเลิก")]
        public DateTime? CancelDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Description("เลขที่บัญชี")]
        public BankAccountDropdownDTO BankAccount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Description("ใบจอง")]
        public BookingDropdownDTO Booking { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Description("วันที่บักทึก")]
        public DateTime? Created { get; set; }

        [Description("วันที่บักทึก")]
        public DateTime? CreditCardExpire { get; set; }

        public CompanyDTO Company { get; set; }


        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();

            var newGuid = Guid.NewGuid();
            if (!Guid.TryParse(this.Booking.Id.ToString(), out newGuid))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(Booking)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (string.IsNullOrEmpty(this.OwnerName))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DirectCreditDebitApprovalFormDTO.OwnerName)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.DirectPeriod != 1 && this.DirectPeriod != 15)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DirectCreditDebitApprovalFormDTO.DirectPeriod)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (string.IsNullOrEmpty(this.AccountNO))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DirectCreditDebitApprovalFormDTO.AccountNO)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (!Guid.TryParse(this.Bank.Id.ToString(), out newGuid))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DirectCreditDebitApprovalFormDTO.Bank)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (string.IsNullOrEmpty(this.CitizenIdentityNo))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DirectCreditDebitApprovalFormDTO.CitizenIdentityNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (!Guid.TryParse(this.DirectApprovalFormStatus.Id.ToString(), out newGuid))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DirectCreditDebitApprovalFormDTO.DirectApprovalFormStatus)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (!Guid.TryParse(this.DirectApprovalFormType.Id.ToString(), out newGuid))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DirectCreditDebitApprovalFormDTO.DirectApprovalFormType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            // DirectCredit
            var isDirectType = db.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.DirectApprovalFormType).ToList();
            var isDirectCredit = isDirectType.Where(x => x.Key == DirectApprovalFormTypeKeys.DirectCredit).Select(x => x.ID).FirstOrDefault();
            var isDirectDebit = isDirectType.Where(x => x.Key == DirectApprovalFormTypeKeys.DirectDebit).Select(x => x.ID).FirstOrDefault();
            if (DirectApprovalFormType.Id == isDirectCredit)
            {
                int ChkInt = 0;
                if (this.CreditCardExpireMonth != null)
                {
                    if (!int.TryParse(this.CreditCardExpireMonth.ToString(), out ChkInt))
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(DirectCreditDebitApprovalFormDTO.CreditCardExpireMonth)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }

                }
                if (this.CreditCardExpireYear != null)
                {
                    if (!int.TryParse(this.CreditCardExpireYear.ToString(), out ChkInt))
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(DirectCreditDebitApprovalFormDTO.CreditCardExpireYear)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }

                }
            }
            else if (DirectApprovalFormType.Id == isDirectDebit)
            {
                if (!Guid.TryParse(this.Province.Id.ToString(), out newGuid))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(DirectCreditDebitApprovalFormDTO.Province)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                if (string.IsNullOrEmpty(this.BankBranch.Name))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(DirectCreditDebitApprovalFormDTO.BankBranch)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                if (!Guid.TryParse(this.BankBranch.Id.ToString(), out newGuid))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(DirectCreditDebitApprovalFormDTO.BankBranch)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }

            }
            else
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DirectCreditDebitApprovalFormDTO.DirectApprovalFormType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (!Guid.TryParse(this.Bank.BankAccountID.ToString(), out newGuid))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DirectCreditDebitApprovalFormDTO.BankAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }


            var isType = db.MasterCenters
      .Where(x => x.Key == DirectApprovalFormStatusKey.Cancel && x.Key == DirectApprovalFormStatusKey.NotApproved && x.Key == DirectApprovalFormStatusKey.CancelTransferred && x.MasterCenterGroupKey == MasterCenterGroupKeys.DirectApprovalFormStatus).ToList();

            if (isType.Any(x => x.ID == this.DirectApprovalFormType.Id))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0088").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DirectCreditDebitApprovalFormDTO.BankAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", this.DirectApprovalFormType.Name);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }



        public static void SortBy(DirectCreditDebitApprovalFormSortByParam sortByParam, ref IQueryable<GetDirectCreditDebitApprovalFormListResult> query)
        {
            if (query != null)
            {
                if (sortByParam.SortBy != null)
                {
                    switch (sortByParam.SortBy.Value)
                    {
                        case DirectCreditDebitApprovalFormSortBy.SAPCompanyID:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.SAPCompanyID);
                            else query = query.OrderByDescending(o => o.Company.SAPCompanyID);
                            break;
                        case DirectCreditDebitApprovalFormSortBy.ProjectNo:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo);
                            else query = query.OrderByDescending(o => o.Project.ProjectNo);
                            break;
                        case DirectCreditDebitApprovalFormSortBy.UnitNo:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                            else query = query.OrderByDescending(o => o.Unit.UnitNo);
                            break;
                        case DirectCreditDebitApprovalFormSortBy.AgreementNo:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.Agreement.AgreementNo);
                            else query = query.OrderByDescending(o => o.Agreement.AgreementNo);
                            break;
                        case DirectCreditDebitApprovalFormSortBy.BankName:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.Alias);
                            else query = query.OrderByDescending(o => o.Bank.Alias);
                            break;
                        ///////////////////////////
                        case DirectCreditDebitApprovalFormSortBy.AccountNO:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitApprovalForm.AccountNO);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitApprovalForm.AccountNO);
                            break;
                        case DirectCreditDebitApprovalFormSortBy.CustomerName:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitApprovalForm.OwnerName);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitApprovalForm.OwnerName);
                            break;
                        case DirectCreditDebitApprovalFormSortBy.StartDate:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitApprovalForm.StartDate);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitApprovalForm.StartDate);
                            break;
                        case DirectCreditDebitApprovalFormSortBy.UpdatedBy:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitApprovalForm.UpdatedBy);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitApprovalForm.UpdatedBy);
                            break;
                        case DirectCreditDebitApprovalFormSortBy.ExpireDate:
                            if (sortByParam.Ascending) query = query.OrderBy(o => o.DirectCreditDebitApprovalForm.CreditCardExpireMonth).ThenByDescending(o => o.DirectCreditDebitApprovalForm.CreditCardExpireYear);
                            else query = query.OrderByDescending(o => o.DirectCreditDebitApprovalForm.CreditCardExpireYear).ThenByDescending(o => o.DirectCreditDebitApprovalForm.CreditCardExpireYear);
                            break;

                        default:
                            query = query.OrderBy(o => o.BankAccount.Company.Code);
                            break;
                    }

                    if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccount.Company.Code);
                    else query = query.OrderByDescending(o => o.BankAccount.Company);
                }
                else
                {
                    query = query.OrderBy(o => o.BankAccount.Company);
                }
            }
        }
        public static DirectCreditDebitApprovalFormDTO CreateFromQueryUnitPriceInstallmentResult(GetDirectCreditDebitApprovalFormListResult model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = CreateFromQueryResult(model, DB);
                var UnitPriceInstallment = DB.UnitPriceInstallments
                                                   .Include(o => o.InstallmentOfUnitPriceItem)
                                                        .ThenInclude(o => o.UnitPrice)
                 //.Where(o => o.InstallmentOfUnitPriceItem.UnitPrice.BookingID == model.Booking.ID).ToList();
                 .Where(o => o.InstallmentOfUnitPriceItem.UnitPrice.BookingID == model.Booking.ID && o.InstallmentOfUnitPriceItem.UnitPrice.IsActive == true).ToList();

                var UnitPriceInstallmentTolist = UnitPriceInstallment.OrderBy(x => x.Period);
                result.UnitPriceInstallment = new List<PaymentUnitPriceItemDTO>();
                result.DirectApprovalFormStatus = new MasterCenterDropdownDTO();

                if (model.DirectApprovalFormStatus == null)
                {

                    var MasterCenter = DB.MasterCenters
         .Where(x => x.Key == DirectApprovalFormStatusKey.New && x.MasterCenterGroupKey == MasterCenterGroupKeys.DirectApprovalFormStatus).FirstOrDefault();
                    result.DirectApprovalFormStatus = MasterCenterDropdownDTO.CreateFromModel(MasterCenter);
                }
                else
                {
                    result.DirectApprovalFormStatus = MasterCenterDropdownDTO.CreateFromModel(model.DirectApprovalFormStatus);
                }

                foreach (var item in UnitPriceInstallmentTolist)
                {
                    result.UnitPriceInstallment.Add(PaymentUnitPriceItemDTO.CreateFromUnitPriceInstallmentModel(item, item.Amount, item.Period));
                }
                foreach (var item in UnitPriceInstallmentTolist)
                {
                    if (item.DueDate != null)
                    {
                        result.DirectPeriod = item.DueDate.Value.Day;
                    }
                    break;
                }
                return result;
            }
            else
            {
                return null;

            }
        }

        public static DirectCreditDebitApprovalFormDTO CreateFromQueryResult(GetDirectCreditDebitApprovalFormListResult model, DatabaseContext DB)
        {
            if (model != null)
            {
                var AgreementOwners = DB.AgreementOwners
                            .Include(o => o.FromContact)
                            .Where(o => o.AgreementID == model.Agreement.ID && o.IsMainOwner == true).FirstOrDefault();

                string tDate = null;
                DateTime? dDate = null;
                DirectCreditDebitApprovalFormDTO result = null;
                if (model.DirectCreditDebitApprovalForm != null)
                {
                    if (model.DirectCreditDebitApprovalForm.CreditCardExpireMonth != null && model.DirectCreditDebitApprovalForm.CreditCardExpireYear != null)
                    {
                        if (model.DirectCreditDebitApprovalForm.CreditCardExpireMonth.ToString().Length == 1)
                        {
                            tDate = "01/0" + model.DirectCreditDebitApprovalForm.CreditCardExpireMonth.ToString() + "/" + model.DirectCreditDebitApprovalForm.CreditCardExpireYear.ToString();
                        }
                        else
                        {
                            tDate = "01/" + model.DirectCreditDebitApprovalForm.CreditCardExpireMonth.ToString() + "/" + model.DirectCreditDebitApprovalForm.CreditCardExpireYear.ToString();
                        }
                        dDate = DateTime.ParseExact(tDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-US"));
                    }

                    result = new DirectCreditDebitApprovalFormDTO()
                    {

                        Agreement = AgreementDropdownDTO.CreateFromModel(model.Agreement),

                        AgreementOwner = AgreementOwnerDropdownDTO.CreateFromModel(AgreementOwners),
                        Booking = BookingDropdownDTO.CreateFromModel(model.Booking),
                        Project = ProjectDropdownDTO.CreateFromModel(model.Project),
                        Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
                        DirectApprovalFormType = MasterCenterDropdownDTO.CreateFromModel(model.DirectApprovalFormType),
                        DirectApprovalFormStatus = MasterCenterDropdownDTO.CreateFromModel(model.DirectApprovalFormStatus),
                        DirectPeriod = model.DirectCreditDebitApprovalForm.DirectPeriod,
                        Bank = BankAccNameDTO.CreateFromModel(model.BankAccount),
                        Province = ProvinceListDTO.CreateFromModel(model.Province),
                        AccountNO = model.DirectCreditDebitApprovalForm.AccountNO,
                        CreditCardExpireYear = model.DirectCreditDebitApprovalForm.CreditCardExpireYear,
                        CreditCardExpireMonth = model.DirectCreditDebitApprovalForm.CreditCardExpireMonth,
                        OwnerName = model.DirectCreditDebitApprovalForm.OwnerName,
                        CitizenIdentityNo = model.DirectCreditDebitApprovalForm.CitizenIdentityNo,
                        BankAccount = BankAccountDropdownDTO.CreateFromModel(model.BankAccount),
                        UnitPriceInstallment = new List<PaymentUnitPriceItemDTO>(),

                        StartDate = model.DirectCreditDebitApprovalForm.StartDate,

                        Created = model.DirectCreditDebitApprovalForm.Created,
                        ApproveDate = model.DirectCreditDebitApprovalForm.ApproveDate,
                        CancelDate = model.DirectCreditDebitApprovalForm.CancelDate,
                        RejectDate = model.DirectCreditDebitApprovalForm.RejectDate,

                        Remark = model.DirectCreditDebitApprovalForm.Remark,
                        CreditCardExpire = dDate,
                        Company = CompanyDTO.CreateFromModel(model.Company),
                        Id = model.DirectCreditDebitApprovalForm.ID,
                        BankBranch = BankBranchDropdownDTO.CreateFromModel(model.BankBranch),

                        Updated = model.DirectCreditDebitApprovalForm.Updated
                    };
                }
                else
                {
                    result = new DirectCreditDebitApprovalFormDTO()
                    {

                        Agreement = AgreementDropdownDTO.CreateFromModel(model.Agreement),
                        AgreementOwner = AgreementOwnerDropdownDTO.CreateFromModel(AgreementOwners),
                        Booking = BookingDropdownDTO.CreateFromModel(model.Booking),
                        Project = ProjectDropdownDTO.CreateFromModel(model.Project),
                        Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
                        DirectApprovalFormType = new MasterCenterDropdownDTO(),
                        Company = CompanyDTO.CreateFromModel(model.Company),
                        //OwnerName = null,
                        //Id = null,
                        //AccountNO = null,
                        //ApproveDate = null,
                        //CancelDate = null,
                        //CitizenIdentityNo = null,
                        //Created = null,
                        Province = new ProvinceListDTO(),
                        Bank = new BankAccNameDTO(),
                        BankAccount = new BankAccountDropdownDTO(),
                        BankBranch = new BankBranchDropdownDTO(),

                        DirectApprovalFormStatus = new MasterCenterDropdownDTO(),




                    };
                }
                return result;
            }
            else
            {
                return null;
            }
        }
    }

    public class GetBankDebitCreditDebitDropdownResult
    {
        public string BankID { get; set; }
        public string NameTH { get; set; }
    }


    public class CreateDataResult
    {
        public Agreement Agreement { get; set; }
        public Unit Unit { get; set; }

        public Booking Booking { get; set; }
        public Project Project { get; set; }
        public Guid? CompanyID { get; set; }
        public Guid? BookingID { get; set; }
        public Transfer Transfer { get; set; }
        public AgreementOwner AgreementOwner { get; set; }

    }

    public class GetDirectCreditDebitApprovalFormListResult
    {
        public DirectCreditDebitApprovalForm DirectCreditDebitApprovalForm { get; set; }
        public Booking Booking { get; set; }
        public MasterCenter DirectApprovalFormType { get; set; }
        public MasterCenter DirectApprovalFormStatus { get; set; }
        public BankAccount BankAccount { get; set; }
        public BankBranch BankBranch { get; set; }
        public Bank Bank { get; set; }
        public Province Province { get; set; }
        public Company Company { get; set; }
        public Project Project { get; set; }
        public Unit Unit { get; set; }
        public Agreement Agreement { get; set; }
        public AgreementOwner AgreementOwner { get; set; }
        public List<UnitPriceItem> PaymentUnitPriceItem { get; set; }

    }

}
