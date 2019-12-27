using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.PRJ;
using Microsoft.EntityFrameworkCore;
using Project.Params.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public class UnitOtherUnitInfoTagService : IUnitOtherUnitInfoTagService
    {
        private readonly DatabaseContext DB;

        public UnitOtherUnitInfoTagService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<List<UnitOtherUnitInfoTagDTO>> GetUnitTagListAsync(UnitOtherUnitInfoTagFilter filter)
        {
            IQueryable<UnitOtherUnitInfoTag> query = DB.UnitOtherUnitInfoTags;
            if (!string.IsNullOrEmpty(filter.name))
            {
                query = query.Where(x => x.Name == filter.name);
            }
            var results = await query.Select(o => UnitOtherUnitInfoTagDTO.CreateFromModel(o)).ToListAsync();
            return results;
        }

        public async Task<UnitOtherUnitInfoTagDTO> GetUnitTagAsync(Guid id)
        {
            var model = await DB.UnitOtherUnitInfoTags.Where(x => x.ID == id).FirstAsync();
            var result = UnitOtherUnitInfoTagDTO.CreateFromModel(model);
            return result;
        }

        public async Task<UnitOtherUnitInfoTagDTO> CreateUnitTagAsync(UnitOtherUnitInfoTagDTO input)
        {
            UnitOtherUnitInfoTag model = new UnitOtherUnitInfoTag();
            input.ToModel(ref model);

            await DB.UnitOtherUnitInfoTags.AddAsync(model);
            await DB.SaveChangesAsync();

            var result = UnitOtherUnitInfoTagDTO.CreateFromModel(model);
            return result;

        }

        public async Task<UnitOtherUnitInfoTagDTO> UpdateUnitTagAsync(Guid id, UnitOtherUnitInfoTagDTO input)
        {
            var model = await DB.UnitOtherUnitInfoTags.Where(x => x.ID == id).FirstAsync();
            input.ToModel(ref model);


            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var result = UnitOtherUnitInfoTagDTO.CreateFromModel(model);
            return result;

        }

        public async Task<UnitOtherUnitInfoTag> DeleteUnitTagAsync(Guid id)
        {
            var model = await DB.UnitOtherUnitInfoTags.Where(x => x.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}



