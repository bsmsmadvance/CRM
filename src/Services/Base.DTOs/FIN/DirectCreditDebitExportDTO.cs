using Database.Models;
using Database.Models.MST;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.FIN
{
    public class DirectCreditDebitExportDTO : BaseDTO
    {
        /// <summary>
        /// ชนิดของแบบฟอร์ม Direct Debit/Credit
        /// </summary>
        [Description("ชนิดของแบบฟอร์ม Direct Debit/Credit")]
        public MST.MasterCenterDropdownDTO DirectFormType { get; set; }

        /// <summary>
        /// บริษัท
        /// </summary>
        [Description("บริษัท")]
        public MST.CompanyDropdownDTO Company { get; set; }

        /// <summary>
        /// ธนาคาร
        /// </summary>
       [Description("ธนาคาร")]
        public FIN.BankAccNameDTO BankAccName { get; set; }

        /// <summary>
        /// เลขที่บัญชี
        /// </summary>
        [Description("เลขที่บัญชี")]
        public MST.BankAccountDropdownDTO BankAccount { get; set; }

        /// <summary>
        /// รอบการตัดเงิน วันที่ 1 หรือ 15
        /// </summary>
        [Description("รอบการตัดเงิน วันที่ 1 หรือ 15")]
        public int? PeriodDay { get; set; }

        /// <summary>
        /// รอบการตัดเงิน เดือน
        /// </summary>
        [Description("รอบการตัดเงิน เดือน")]
        public int? PeriodMounth { get; set; }

        /// <summary>
        /// รอบการตัดเงิน เดือน
        /// </summary>
        [Description("รอบการตัดเงิน เดือน")]
        public int? PeriodYear { get; set; }

        /// <summary>
        /// วันที่ตัดเงิน
        /// </summary>
        [Description("วันที่ตัดเงิน")]
        public DateTime ReceiveDate { get; set; }


        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            var newGuid = Guid.NewGuid();

            var masterCenterModel = db.MasterCenters.Where(o => o.ID == this.DirectFormType.Id).ToList() ?? new List<MasterCenter>();
            if (!masterCenterModel.Any())
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DirectCreditDebitExportDTO.DirectFormType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            var CompanyModel = db.Companies.Where(o => o.ID == this.Company.Id).ToList() ?? new List<Company>();
            if (!CompanyModel.Any())
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DirectCreditDebitExportDTO.Company)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            var Bank = db.BankAccounts.Where(o => o.ID == this.BankAccount.Id).ToList() ?? new List<BankAccount>();
            if (!Bank.Any())
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DirectCreditDebitExportDTO.BankAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            var BankAccount = db.Banks.Where(o => o.ID == this.BankAccName.Id).ToList() ?? new List<Bank>();
            if (!BankAccount.Any())
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DirectCreditDebitExportDTO.BankAccName)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }


            if (this.PeriodDay == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DirectCreditDebitExportDTO.PeriodDay)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (this.PeriodMounth == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DirectCreditDebitExportDTO.PeriodMounth)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (this.PeriodYear == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DirectCreditDebitExportDTO.PeriodYear)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (this.ReceiveDate == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DirectCreditDebitExportDTO.ReceiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
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
