using Database.Models;
using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class UnitDropdownSellPriceDTO
    {
        /// <summary>
        /// Identity UnitID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// เลขที่แปลง
        /// </summary>
        public string UnitNo { get; set; }
        /// <summary>
        /// สถานะแปลง
        /// Master/api/MasterCenters?masterCenterGroupKey=UnitStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO UnitStatus { get; set; }
        /// <summary>
        /// ราคาขาย
        /// </summary>
        public decimal? SellPrice { get; set; }


        public async static Task<UnitDropdownSellPriceDTO> CreateFromModelAsync(Unit model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new UnitDropdownSellPriceDTO
                {
                    Id = model.ID,
                    UnitNo = model.UnitNo
                };
                result.UnitStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.UnitStatus);
                var activePriceList = await db.PriceLists.GetActivePriceListAsync(model.ID);
                result.SellPrice = activePriceList?.PriceListItems
                    .Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Amount)
                    .FirstOrDefault();

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
