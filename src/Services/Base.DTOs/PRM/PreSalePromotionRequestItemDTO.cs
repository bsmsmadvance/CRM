using Database.Models;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRM
{
    /// <summary>
    /// รายการสิ่งของเบิกโปรก่อนขาย
    /// Model: PreSalePromotionRequestItem
    /// </summary>
    public class PreSalePromotionRequestItemDTO : BaseDTO
    {
        /// <summary>
        /// ข้อมูลมาจาก Master
        /// </summary>
        public bool IsMaster { get; set; }
        /// <summary>
        /// ชื่อผลิตภัณฑ์ (TH)
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// จำนวน
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// ราคาต่อหน่วย (บาท)
        /// </summary>
        public decimal PricePerUnit { get; set; }
        /// <summary>
        /// ราคารวม (บาท)
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// วันที่คาดว่าจะได้รับ (default = วันที่ปัจจุบัน)
        /// </summary>
        public DateTime? ReceiveDate { get; set; }
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// เลขที่ PR
        /// </summary>
        public string PRNo { get; set; }
        /// <summary>
        /// MasterPreSalePromotionItemID
        /// </summary>
        public Guid? MasterPreSalePromotionItemID { get; set; }

        public static async Task<PreSalePromotionRequestItemDTO> CreateFromModelAsync(PreSalePromotionRequestItem model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new PreSalePromotionRequestItemDTO
                {
                    Id = model.ID,
                    NameTH = model.NameTH,
                    Quantity = model.Quantity,
                    PricePerUnit = model.PricePerUnit,
                    TotalPrice = model.TotalPrice,
                    ReceiveDate = model.ReceiveDate,
                    Remark = model.Remark,
                    PRNo = model.PRNo,
                    MasterPreSalePromotionItemID = model.MasterPreSalePromotionItemID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static PreSalePromotionRequestItemDTO CreateFromMasterModel(MasterPreSalePromotionItem model)
        {
            if (model != null)
            {
                var result = new PreSalePromotionRequestItemDTO
                {
                    NameTH = model.NameTH,
                    Quantity = model.Quantity,
                    PricePerUnit = model.PricePerUnit,
                    TotalPrice = model.TotalPrice,
                    ReceiveDate = DateTime.Now,
                    Remark = model.Remark,
                    MasterPreSalePromotionItemID = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    IsMaster = true
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
