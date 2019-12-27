using Base.DTOs;
using Base.DTOs.MST;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.MST;
using MasterData.Params.Filters;
using MasterData.Params.Outputs;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterData.Services
{
    public class LegalEntityService : ILegalEntityService
    {
        private readonly DatabaseContext DB;

        public LegalEntityService(DatabaseContext db)
        {
            this.DB = db;
        }
        public async Task<List<LegalEntityDropdownDTO>> GetLegalEntityDropdownListAsync(string name)
        {
            IQueryable<LegalEntity> query = DB.LegalEntities;

            #region Filter
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.NameTH.Contains(name));
            }
            #endregion

            var queryResults = await query.Take(100).ToListAsync();

            var results = queryResults.Select(o => LegalEntityDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<LegalEntityPaging> GetLegalEntityListAsync(LegalFilter filter, PageParam pageParam, LegalEntitySortByParam sortByParam)
        {
            IQueryable<LegalEntityQueryResult> query = DB.LegalEntities
                                                         .Select(o => new LegalEntityQueryResult
                                                         {
                                                             LegalEntity = o,
                                                             Bank = o.Bank,
                                                             BankAccountType = o.BankAccountType,
                                                             UpdatedBy = o.UpdatedBy
                                                         });

            #region filter
            if (!string.IsNullOrEmpty(filter.NameTH))
            {
                query = query.Where(x => x.LegalEntity.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                query = query.Where(x => x.LegalEntity.NameEN.Contains(filter.NameEN));
            }
            if (filter.BankID != null & filter.BankID != Guid.Empty)
            {
                query = query.Where(x => x.LegalEntity.BankID == filter.BankID);
            }
            if (!string.IsNullOrEmpty(filter.BankAccountTypeKey))
            {
                var bankAccountTypeID = await (DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BankAccountType && o.Key == filter.BankAccountTypeKey).Select(o => o.ID)).FirstOrDefaultAsync();
                query = query.Where(o => o.LegalEntity.BankAccountTypeMasterCenterID == bankAccountTypeID);
            }
            if (filter.IsActive != null)
            {
                query = query.Where(x => x.LegalEntity.IsActive == filter.IsActive);
            }
            if (!string.IsNullOrEmpty(filter.BankAccountNo))
            {
                query = query.Where(x => x.LegalEntity.BankAccountNo.Contains(filter.BankAccountNo.Replace("-", string.Empty)));
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.LegalEntity.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.LegalEntity.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.LegalEntity.Updated >= filter.UpdatedFrom && x.LegalEntity.Updated <= filter.UpdatedTo);
            }
            #endregion

            LegalEntityDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<LegalEntityQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => LegalEntityDTO.CreateFromQueryResult(o)).ToList();

            return new LegalEntityPaging()
            {
                LegalEntities = results,
                PageOutput = pageOutput
            };
        }

        public async Task<LegalEntityDTO> GetLegalEntityAsync(Guid id)
        {
            var model = await DB.LegalEntities.Where(o => o.ID == id)
                                              .Include(o => o.Bank)
                                              .Include(o => o.UpdatedBy)
                                              .Include(o => o.BankAccountType)
                                              .FirstAsync();
            var result = LegalEntityDTO.CreateFromModel(model);
            return result;
        }

        public async Task<LegalEntityDTO> CreateLegalEntityAsync(LegalEntityDTO input)
        {
            await input.ValidateAsync(DB);
            LegalEntity model = new LegalEntity();
            input.ToModel(ref model);

            await DB.LegalEntities.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await this.GetLegalEntityAsync(model.ID);
            return result;
        }

        public async Task<LegalEntityDTO> UpdateLegalEntityAsync(Guid id, LegalEntityDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.LegalEntities.Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await this.GetLegalEntityAsync(model.ID);
            return result;
        }

        public async Task<LegalEntity> DeleteLegalEntityAsync(Guid id)
        {
            var model = await DB.LegalEntities.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
