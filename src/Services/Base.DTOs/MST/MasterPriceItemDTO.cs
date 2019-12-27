using System;
using Database.Models.MST;

namespace Base.DTOs.MST
{
    public class MasterPriceItemDTO : BaseDTO
    {
        public MasterCenterDropdownDTO PriceType { get; set; }
        public string Detail { get; set; }
        public string DetailEN { get; set; }
        public MasterCenterDropdownDTO PaymentReceiver { get; set; }

        public static MasterPriceItemDTO CreateFromModel(MasterPriceItem model)
        {
            if (model != null)
            {
                var result = new MasterPriceItemDTO()
                {
                    Id = model.ID,
                    PriceType = MasterCenterDropdownDTO.CreateFromModel(model.PriceType),
                    Detail = model.Detail,
                    DetailEN = model.DetailEN,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    PaymentReceiver = MasterCenterDropdownDTO.CreateFromModel(model.PaymentReceiver)
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
