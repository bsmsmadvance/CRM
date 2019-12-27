using System;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Database.Models.FIN;
using Microsoft.EntityFrameworkCore;

namespace Base.DTOs.FIN
{
    public class ReceiptTempListDTO
    {
        /// <summary>
        /// เลขที่ใบเสร็จชั่วคราว
        /// </summary>
        public string ReceiptTempNo { get; set; }
        /// <summary>
        /// เลขที่ใบเสร็จ
        /// </summary>
        public string ReceiptNo { get; set; }
        /// <summary>
        /// เลขที่โรงพิมพ์
        /// </summary>
        public string PrintingNo { get; set; }
        /// <summary>
        /// วันที่ส่งโรงพิมพ์
        /// </summary>
        public DateTime? SendDate { get; set; }

        public static async Task<ReceiptTempListDTO> CreateFromUnitPriceItemModelAsync(Payment model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new ReceiptTempListDTO();
                var receiptTempHeader = await db.ReceiptTempHeaders.Where(o => o.PaymentID == model.ID).FirstAsync();
                result.ReceiptTempNo = receiptTempHeader.ReceiptTempNo;
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
