using Database.Models;
using Database.Models.MST;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.MST
{
    /// <summary>
    /// ข้อมูลบัญชีธนาคาร
    /// Model = BankAccount
    /// </summary>
    public class BankAccountDTO : BaseDTO
    {
        /// <summary>
        /// บริษัท
        ///  Master/api/Companies/DropdownList
        /// </summary>
        [Description("บริษัท")]
        public CompanyDropdownDTO Company { get; set; }
        /// <summary>
        /// ธนาคาร
        /// Master/api/Banks/DropdownList
        /// </summary>
        [Description("ธนาคาร")]
        public BankDropdownDTO Bank { get; set; }
        /// <summary>
        /// สาขาธนาคาร
        /// Master/api/BankBranchs/DropdownList
        /// </summary>
        [Description("สาขาธนาคาร")]
        public BankBranchDropdownDTO BankBranch { get; set; }
        /// <summary>
        /// จังหวัด
        /// Master/Provinces/DropdownList
        /// </summary>
        [Description("จังหวัด")]
        public ProvinceListDTO Province { get; set; }
        /// <summary>
        /// ประเภทบัญชี
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=BankAccountType
        /// </summary>
        [Description("ประเภทบัญชี")]
        public MST.MasterCenterDropdownDTO BankAccountType { get; set; }
        /// <summary>
        /// เลขที่บัญชี GL
        /// </summary>
        [Description("เลขที่บัญชี GL")]
        public string GLAccountNo { get; set; }
        /// <summary>
        /// เลขที่บัญชี
        /// </summary>
        [Description("เลขที่บัญชี")]
        public string BankAccountNo { get; set; }
        /// <summary>
        /// บัญชีเงินโอนผ่านธนาคาร
        /// </summary>
        public bool IsTransferAccount { get; set; }

        /// <summary>
        /// บัญชี Direct Debit
        /// </summary>
        public bool IsDirectDebit { get; set; }
        /// <summary>
        /// บัญชี Direct Credit
        /// </summary>
        public bool IsDirectCredit { get; set; }
        /// <summary>
        /// บัญชีนำฝาก
        /// </summary>
        public bool IsDepositAccount { get; set; }
        /// <summary>
        /// P.Card กระทรวงการคลัง
        /// </summary>
        public bool IsPCard { get; set; }
        /// <summary>
        /// Service Code
        /// </summary>
        [Description("Service Code")]
        public string ServiceCode { get; set; }
        /// <summary>
        /// Merchant ID
        /// </summary>
        [Description("Merchant ID")]
        public string MerchantID { get; set; }
        /// <summary>
        /// สถานะ Active
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// ประเภทของคู่บัญชี
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=GLAccountType
        /// </summary>
        [Description("ประเภทของคู่บัญชี")]
        public MST.MasterCenterDropdownDTO GLAccountType { get; set; }
        /// <summary>
        /// ภาษีมูลค่าเพิ่ม
        /// </summary>
        public bool HasVat { get; set; }
        /// <summary>
        /// GLRefCode
        /// </summary>
        public string GLRefCode { get; set; }
        /// <summary>
        /// ชื่อบัญชี
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }

        public static BankAccountDTO CreateFromModel(BankAccount model)
        {
            if (model != null)
            {
                var result = new BankAccountDTO()
                {
                    Id = model.ID,
                    Company = CompanyDropdownDTO.CreateFromModel(model.Company),
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    BankBranch = BankBranchDropdownDTO.CreateFromModel(model.BankBranch),
                    Province = ProvinceListDTO.CreateFromModel(model.Province),
                    BankAccountType = MasterCenterDropdownDTO.CreateFromModel(model.BankAccountType),
                    GLAccountNo = model.GLAccountNo,
                    BankAccountNo = model.BankAccountNo,
                    IsTransferAccount = model.IsTransferAccount,
                    IsDirectDebit = model.IsDirectDebit,
                    IsDirectCredit = model.IsDirectCredit,
                    IsDepositAccount = model.IsDepositAccount,
                    IsPCard = model.IsPCard,
                    ServiceCode = model.ServiceCode,
                    MerchantID = model.MerchantID,
                    HasVat = model.HasVat,
                    GLAccountType = MasterCenterDropdownDTO.CreateFromModel(model.GLAccountType),
                    IsActive = model.IsActive,
                    Name = model.Name,
                    GLRefCode = model.GLRefCode,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    Remark = model.Remark
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        public static BankAccountDTO CreateFromQueryResult(BankAccountQueryResult model)
        {
            if (model != null)
            {
                var result = new BankAccountDTO()
                {
                    Id = model.BankAccount.ID,
                    Company = CompanyDropdownDTO.CreateFromModel(model.Company),
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    BankBranch = BankBranchDropdownDTO.CreateFromModel(model.BankBranch),
                    Province = ProvinceListDTO.CreateFromModel(model.Province),
                    BankAccountType = MasterCenterDropdownDTO.CreateFromModel(model.BankAccountType),
                    GLAccountNo = model.BankAccount.GLAccountNo,
                    BankAccountNo = model.BankAccount.BankAccountNo,
                    IsTransferAccount = model.BankAccount.IsTransferAccount,
                    IsDirectDebit = model.BankAccount.IsDirectDebit,
                    IsDirectCredit = model.BankAccount.IsDirectCredit,
                    IsDepositAccount = model.BankAccount.IsDepositAccount,
                    IsPCard = model.BankAccount.IsPCard,
                    ServiceCode = model.BankAccount.ServiceCode,
                    MerchantID = model.BankAccount.MerchantID,
                    IsActive = model.BankAccount.IsActive,
                    HasVat = model.BankAccount.HasVat,
                    GLAccountType = MasterCenterDropdownDTO.CreateFromModel(model.GLAccountType),
                    GLRefCode = model.BankAccount.GLRefCode,
                    Name = model.BankAccount.Name,
                    Updated = model.BankAccount.Updated,
                    UpdatedBy = model.BankAccount.UpdatedBy?.DisplayName,
                    Remark = model.BankAccount.Remark
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(BankAccountSortByParam sortByParam, ref IQueryable<BankAccountQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case BankAccountSortBy.Company:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.NameTH);
                        else query = query.OrderByDescending(o => o.Company.NameTH);
                        break;
                    case BankAccountSortBy.Bank:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.NameTH);
                        else query = query.OrderByDescending(o => o.Bank.NameTH);
                        break;
                    case BankAccountSortBy.BankBranch:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankBranch.Name);
                        else query = query.OrderByDescending(o => o.BankBranch.Name);
                        break;
                    case BankAccountSortBy.Province:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Province.NameTH);
                        else query = query.OrderByDescending(o => o.Province.NameTH);
                        break;
                    case BankAccountSortBy.BankAccountType:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccountType.Name);
                        else query = query.OrderByDescending(o => o.BankAccountType.Name);
                        break;
                    case BankAccountSortBy.GLAccountNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccount.GLAccountNo);
                        else query = query.OrderByDescending(o => o.BankAccount.GLAccountNo);
                        break;
                    case BankAccountSortBy.BankAccountNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccount.BankAccountNo);
                        else query = query.OrderByDescending(o => o.BankAccount.BankAccountNo);
                        break;
                    case BankAccountSortBy.IsTransferAccount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccount.IsTransferAccount);
                        else query = query.OrderByDescending(o => o.BankAccount.IsTransferAccount);
                        break;
                    case BankAccountSortBy.IsDirectDebit:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccount.IsDirectDebit);
                        else query = query.OrderByDescending(o => o.BankAccount.IsDirectDebit);
                        break;
                    case BankAccountSortBy.IsDirectCredit:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccount.IsDirectCredit);
                        else query = query.OrderByDescending(o => o.BankAccount.IsDirectCredit);
                        break;
                    case BankAccountSortBy.IsDepositAccount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccount.IsDepositAccount);
                        else query = query.OrderByDescending(o => o.BankAccount.IsDepositAccount);
                        break;
                    case BankAccountSortBy.IsPCard:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccount.IsPCard);
                        else query = query.OrderByDescending(o => o.BankAccount.IsPCard);
                        break;
                    case BankAccountSortBy.ServiceCode:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccount.ServiceCode);
                        else query = query.OrderByDescending(o => o.BankAccount.ServiceCode);
                        break;
                    case BankAccountSortBy.MerchantID:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccount.MerchantID);
                        else query = query.OrderByDescending(o => o.BankAccount.MerchantID);
                        break;
                    case BankAccountSortBy.IsActive:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccount.IsActive);
                        else query = query.OrderByDescending(o => o.BankAccount.IsActive);
                        break;
                    case BankAccountSortBy.HasVat:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccount.HasVat);
                        else query = query.OrderByDescending(o => o.BankAccount.HasVat);
                        break;
                    case BankAccountSortBy.GLAccountType:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.GLAccountType.Name);
                        else query = query.OrderByDescending(o => o.GLAccountType.Name);
                        break;
                    case BankAccountSortBy.GLRefCode:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccount.GLRefCode);
                        else query = query.OrderByDescending(o => o.BankAccount.GLRefCode);
                        break;
                    case BankAccountSortBy.Name:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccount.Name);
                        else query = query.OrderByDescending(o => o.BankAccount.Name);
                        break;
                    case BankAccountSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccount.Updated);
                        else query = query.OrderByDescending(o => o.BankAccount.Updated);
                        break;
                    case BankAccountSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.Bank.NameTH);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Bank.NameTH);
            }

        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            // บริษัท
            if (this.Company == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BankAccountDTO.Company)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            // ธนาคาร
            if (this.Bank == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BankAccountDTO.Bank)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            // สาขาธนาคาร
            if (this.BankBranch == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BankAccountDTO.BankBranch)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            // จังหวัด
            if (this.Province == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BankAccountDTO.Province)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            // ประเภทบัญชี
            if (this.BankAccountType == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BankAccountDTO.BankAccountType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            // เลขที่บัญชี
            if (string.IsNullOrEmpty(this.BankAccountNo))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BankAccountDTO.BankAccountNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.BankAccountNo.IsOnlyNumberWithMaxLength(10))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0023").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BankAccountDTO.BankAccountNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                var checkUniqueBankAccountNo = this.Id != (Guid?)null ? await db.BankAccounts.Where(o => o.BankAccountNo == this.BankAccountNo && o.ID != this.Id).CountAsync() > 0 : await db.BankAccounts.Where(o => o.BankAccountNo == this.BankAccountNo).CountAsync() > 0;
                if (checkUniqueBankAccountNo)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BankAccountDTO.BankAccountNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.BankAccountNo);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            // เลขที่บัญชี GL
            if (string.IsNullOrEmpty(this.GLAccountNo))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BankAccountDTO.GLAccountNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.GLAccountNo.IsOnlyNumberWithMaxLength(10))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0023").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BankAccountDTO.GLAccountNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                var checkUniqueBankGLAccountNo = this.Id != (Guid?)null ? await db.BankAccounts.Where(o => o.GLAccountNo == this.GLAccountNo && o.ID != this.Id).CountAsync() > 0 : await db.BankAccounts.Where(o => o.GLAccountNo == this.GLAccountNo).CountAsync() > 0;
                if (checkUniqueBankGLAccountNo)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BankAccountDTO.GLAccountNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.GLAccountNo);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            // Service Code
            if (string.IsNullOrEmpty(this.ServiceCode))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BankAccountDTO.ServiceCode)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.ServiceCode.IsOnlyNumber())
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BankAccountDTO.ServiceCode)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                var checkUniqueServiceCode = this.Id != (Guid?)null ? await db.BankAccounts.Where(o => o.ServiceCode == this.ServiceCode && o.ID != this.Id).CountAsync() > 0 : await db.BankAccounts.Where(o => o.ServiceCode == this.ServiceCode).CountAsync() > 0;
                if (checkUniqueServiceCode)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BankAccountDTO.ServiceCode)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.ServiceCode);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            // Merchance ID
            if (string.IsNullOrEmpty(this.MerchantID))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BankAccountDTO.MerchantID)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.MerchantID.IsOnlyNumber())
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BankAccountDTO.MerchantID)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                var checkUniqueMerchantID = this.Id != (Guid?)null ? await db.BankAccounts.Where(o => o.MerchantID == this.MerchantID && o.ID != this.Id).CountAsync() > 0 : await db.BankAccounts.Where(o => o.MerchantID == this.MerchantID).CountAsync() > 0;
                if (checkUniqueMerchantID)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BankAccountDTO.MerchantID)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.MerchantID);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            //P.Card กระทรวงการคลัง
            if (this.IsPCard)
            {
                var pCardExisted = await db.BankAccounts.Where(o => o.CompanyID == this.Company.Id && o.IsPCard).AnyAsync();
                if (pCardExisted)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0056").FirstAsync();
                    var msg = errMsg.Message.Replace("[value]", this.Company?.NameTH);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public async Task ValidateChartOfAccountAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (this.GLAccountType == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BankAccountDTO.GLAccountType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (this.IsActive == true)
                {
                    var checkUniqueIsActive = this.Id != (Guid?)null ? await db.BankAccounts.Include(o => o.GLAccountType).Where(o => o.GLAccountType.Key != "1" && o.GLAccountType.Key == this.GLAccountType.Key && o.IsActive == true && o.ID != this.Id).CountAsync() > 0 : await db.BankAccounts.Include(o => o.GLAccountType).Where(o => o.GLAccountType.Key != "1" && o.GLAccountType.Key == this.GLAccountType.Key && o.IsActive == true).CountAsync() > 0;
                    if (checkUniqueIsActive)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0057").FirstAsync();
                        var msg = errMsg.Message;
                        msg = msg.Replace("[value]", this.GLAccountType?.Name);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
            }
            // เลขที่บัญชี GL
            if (string.IsNullOrEmpty(this.GLAccountNo))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BankAccountDTO.GLAccountNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.GLAccountNo.IsOnlyNumberWithMaxLength(10))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0023").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BankAccountDTO.GLAccountNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                var checkUniqueBankGLAccountNo = this.Id != (Guid?)null ? await db.BankAccounts.Where(o => o.GLAccountNo == this.GLAccountNo && o.ID != this.Id).CountAsync() > 0 : await db.BankAccounts.Where(o => o.GLAccountNo == this.GLAccountNo).CountAsync() > 0;
                if (checkUniqueBankGLAccountNo)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BankAccountDTO.GLAccountNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.GLAccountNo);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (this.GLAccountType?.Key == "1")
            {
                // Service Code
                if (string.IsNullOrEmpty(this.ServiceCode))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BankAccountDTO.ServiceCode)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                else
                {
                    if (!this.ServiceCode.IsOnlyNumber())
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(BankAccountDTO.ServiceCode)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    var checkUniqueServiceCode = this.Id != (Guid?)null ? await db.BankAccounts.Where(o => o.ServiceCode == this.ServiceCode && o.ID != this.Id).CountAsync() > 0 : await db.BankAccounts.Where(o => o.ServiceCode == this.ServiceCode).CountAsync() > 0;
                    if (checkUniqueServiceCode)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(BankAccountDTO.ServiceCode)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        msg = msg.Replace("[value]", this.ServiceCode);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
                // Merchance ID
                if (string.IsNullOrEmpty(this.MerchantID))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BankAccountDTO.MerchantID)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                else
                {
                    if (!this.MerchantID.IsOnlyNumber())
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(BankAccountDTO.MerchantID)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    var checkUniqueMerchantID = this.Id != (Guid?)null ? await db.BankAccounts.Where(o => o.MerchantID == this.MerchantID && o.ID != this.Id).CountAsync() > 0 : await db.BankAccounts.Where(o => o.MerchantID == this.MerchantID).CountAsync() > 0;
                    if (checkUniqueMerchantID)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(BankAccountDTO.MerchantID)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        msg = msg.Replace("[value]", this.MerchantID);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref BankAccount model)
        {
            model.CompanyID = this.Company?.Id;
            model.BankID = this.Bank?.Id;
            model.BankBranchID = this.BankBranch?.Id;
            model.ProvinceID = this.Province?.Id;
            model.BankAccountTypeMasterCenterID = this.BankAccountType?.Id;
            model.GLAccountNo = this.GLAccountNo;
            model.BankAccountNo = this.BankAccountNo;
            model.IsTransferAccount = this.IsTransferAccount;
            model.IsDirectDebit = this.IsDirectDebit;
            model.IsDirectCredit = this.IsDirectCredit;
            model.IsDepositAccount = this.IsDepositAccount;
            model.IsPCard = this.IsPCard;
            model.ServiceCode = this.ServiceCode;
            model.MerchantID = this.MerchantID;
            model.IsActive = this.IsActive;
            if (this.GLAccountType?.Key == "7")
            {
                model.HasVat = this.HasVat;
            }
            model.GLAccountTypeMasterCenterID = this.GLAccountType?.Id;
            model.Name = this.Name;
            if (this.GLAccountType?.Key != "1")
            {
                model.Remark = this.Remark;
            }
        }
    }

    public class BankAccountQueryResult
    {
        public BankAccount BankAccount { get; set; }
        public Company Company { get; set; }
        public Bank Bank { get; set; }
        public BankBranch BankBranch { get; set; }
        public Province Province { get; set; }
        public MasterCenter BankAccountType { get; set; }
        public MasterCenter GLAccountType { get; set; }
        public User UpdatedBy { get; set; }
    }
}
