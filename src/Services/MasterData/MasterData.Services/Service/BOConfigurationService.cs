using Database.Models;
using Database.Models.MST;
using Base.DTOs.MST;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterData.Services
{
    public class BOConfigurationService : IBOConfigurationService
    {
        private readonly DatabaseContext DB;

        public BOConfigurationService(DatabaseContext db)
        {
            this.DB = db;
        }
        public async Task<BOConfigurationDTO> GetBOConfigurationAsync()
        {
            var data = await DB.BOConfigurations.Include(o => o.UpdatedBy).FirstOrDefaultAsync();
            if (data == null)
            {
                BOConfiguration model = new BOConfiguration();
                await DB.BOConfigurations.AddAsync(model);
                await DB.SaveChangesAsync();
                data = await DB.BOConfigurations.FirstOrDefaultAsync();
            }
            var result = BOConfigurationDTO.CreateFromModel(data);
            return result;
        }

        public async Task<BOConfigurationDTO> UpdateBOConfigurationAsync(Guid id, BOConfigurationDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.BOConfigurations.Where(o => o.ID == id).FirstOrDefaultAsync();
            
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = BOConfigurationDTO.CreateFromModel(model);
            return result;
        }
    }

}
