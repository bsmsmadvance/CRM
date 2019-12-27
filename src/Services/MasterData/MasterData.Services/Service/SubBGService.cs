using Database.Models;
using Database.Models.MST;
using MasterData.Params.Filters;
using Base.DTOs.MST;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagingExtensions;
using MasterData.Params.Outputs;
using Base.DTOs;

namespace MasterData.Services
{
    public class SubBGService : ISubBGService
    {
        private readonly DatabaseContext DB;

        public SubBGService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<List<SubBGDropdownDTO>> GetSubBGDropdownListAsync(string name, Guid? bGID)
        {
            IQueryable<SubBG> query = DB.SubBGs;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.Name.Contains(name));
            }
            if (bGID != null)
            {
                query = query.Where(o => o.BGID == bGID);
            }

            var queryResults = await query.OrderBy(o => o.Name).Take(100).ToListAsync();

            var results = queryResults.Select(o => SubBGDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<SubBGPaging> GetSubBGListAsync(SubBGFilter request, PageParam pageParam, SubBGSortByParam sortByParam)
        {
            IQueryable<SubBGQueryResult> query = DB.SubBGs.Select(o => new SubBGQueryResult
            {
                SubBG = o,
                BG = o.BG,
                UpdatedBy = o.UpdatedBy
            });

            #region filter
            if (!string.IsNullOrEmpty(request.SubBGNo))
            {
                query = query.Where(x => x.SubBG.SubBGNo.Contains(request.SubBGNo));
            }
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.SubBG.Name.Contains(request.Name));
            }
            if (request.BgID != null && request.BgID != Guid.Empty)
            {
                query = query.Where(x => x.SubBG.BGID == request.BgID);
            }
            if (!string.IsNullOrEmpty(request.UpdatedBy))
            {
                query = query.Where(x => x.SubBG.UpdatedBy.DisplayName.Contains(request.UpdatedBy));
            }
            if (request.UpdatedFrom != null)
            {
                query = query.Where(x => x.SubBG.Updated >= request.UpdatedFrom);
            }
            if (request.UpdatedTo != null)
            {
                query = query.Where(x => x.SubBG.Updated <= request.UpdatedTo);
            }
            if (request.UpdatedFrom != null && request.UpdatedTo != null)
            {
                query = query.Where(x => x.SubBG.Updated >= request.UpdatedFrom && x.SubBG.Updated <= request.UpdatedTo);
            }
            #endregion

            SubBGDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<SubBGQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => SubBGDTO.CreateFromQueryResult(o)).ToList();

            return new SubBGPaging()
            {
                PageOutput = pageOutput,
                SubBGs = results
            };
        }

        public async Task<SubBGDTO> GetSubBGAsync(Guid id)
        {
            var model = await DB.SubBGs.Where(o => o.ID == id)
                                       .Include(o => o.BG)
                                       .Include(o => o.UpdatedBy)
                                       .FirstAsync();
            var result = SubBGDTO.CreateFromModel(model);
            return result;
        }

        public async Task<SubBGDTO> CreateSubBGAsync(SubBGDTO input)
        {
            await input.ValidateAsync(DB);
            SubBG model = new SubBG();
            input.ToModel(ref model);

            await DB.SubBGs.AddAsync(model);
            await DB.SaveChangesAsync();


            var result = await this.GetSubBGAsync(model.ID);
            return result;
        }

        public async Task<SubBGDTO> UpdateSubBGAsync(Guid id, SubBGDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.SubBGs.Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var result = await this.GetSubBGAsync(model.ID);
            return result;
        }

        public async Task<SubBG> DeleteSubBGAsync(Guid id)
        {
            var model = await DB.SubBGs.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
