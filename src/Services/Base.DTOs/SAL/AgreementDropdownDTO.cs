using Database.Models.SAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL
{
    public class AgreementDropdownDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่สัญญา
        /// </summary>
        public string AgreementNo { get; set; }
        /// <summary>
        /// วันที่ทำสัญญา
        /// </summary>
        public DateTime? ContractDate { get; set; }
        /// <summary>
        /// ใบจอง
        /// </summary>
        public BookingDropdownDTO Booking { get; set; }

        public static AgreementDropdownDTO CreateFromModel(Agreement model)
        {
            if (model != null)
            {
                var result = new AgreementDropdownDTO()
                {
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    AgreementNo = model.AgreementNo,
                    ContractDate = model.ContractDate,
                    Booking = BookingDropdownDTO.CreateFromModel(model.Booking)
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
