using System;
using System.ComponentModel;
using Database.Models.MST;

namespace Base.DTOs.MST
{
    public class BankAccountDropdownDTO : BaseDTO
    {
        public string DisplayName { get; set; }
        /// <summary>
        /// ธนาคาร
        /// Master/api/Banks/DropdownList
        /// </summary>
        [Description("ธนาคาร")]
        public BankDropdownDTO Bank { get; set; }

        /// <summary>
        /// เลขที่บัญชี
        /// </summary>
        [Description("เลขที่บัญชี")]
        public string BankAccountNo { get; set; }

        /// <summary>
        /// ประเภทบัญชี
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=BankAccountType
        /// </summary>
        [Description("ประเภทบัญชี")]
        public MST.MasterCenterDropdownDTO BankAccountType { get; set; }

        public static BankAccountDropdownDTO CreateFromModel(BankAccount model)
        {
            if (model != null)
            {
                BankAccountDropdownDTO result = new BankAccountDropdownDTO();
                result.Id = model.ID;
                result.Bank = BankDropdownDTO.CreateFromModel(model.Bank);
                result.BankAccountNo = model.BankAccountNo;
                result.BankAccountType = MasterCenterDropdownDTO.CreateFromModel(model.BankAccountType);
                result.DisplayName = result.Bank?.Alias + " " + result.BankAccountType?.Name + " " + result.BankAccountNo;

                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
