using Base.DTOs.MST;
using Database.Models;
using Database.Models.MST;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MasterData.Services
{
    /// <summary>
    /// ตั้งค่าการยกเลิกคืนเงิน
    /// Model: CancelReturnSetting
    /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17484404/367792590/preview
    /// </summary>
    public class CancelReturnSettingService : ICancelReturnSettingService
    {
        private readonly DatabaseContext DB;

        public CancelReturnSettingService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<CancelReturnSettingDTO> GetCancelReturnSettingAsync()
        {
            var data = await DB.CancelReturnSettings.Include(o => o.UpdatedBy).FirstOrDefaultAsync();
            if (data == null)
            {
                CancelReturnSetting model = new CancelReturnSetting();
                model.ChiefReturnLessThanPercent = 70;
                model.HandlingFee = 2000;
                await DB.CancelReturnSettings.AddAsync(model);
                await DB.SaveChangesAsync();
                data = await DB.CancelReturnSettings.FirstOrDefaultAsync();
            }
            var result = CancelReturnSettingDTO.CreateFromModel(data);
            return result;
        }

        public async Task<CancelReturnSettingDTO> UpdateCancelReturnSettingAsync(Guid id, CancelReturnSettingDTO input)
        {
            var model = await DB.CancelReturnSettings.Where(o => o.ID == id).FirstOrDefaultAsync();

            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;

            await DB.SaveChangesAsync();
            var result = CancelReturnSettingDTO.CreateFromModel(model);
            return result;
        }
    }
}
