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
    public class BankBranchService : IBankBranchService
    {
        private readonly DatabaseContext DB;

        public BankBranchService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<List<BankBranchDropdownDTO>> GetBankBrachDropdownListAsync(Guid bankID, string name, Guid? provinceID = null)
        {
            IQueryable<BankBranch> query = DB.BankBranches.Where(o => o.BankID == bankID);
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.Name.Contains(name));
            }
            if (provinceID != null)
            {
                query = query.Where(o => o.ProvinceID == provinceID);
            }
            var queryResults = await query.OrderBy(o => o.Name).Take(100).ToListAsync();

            var results = queryResults.Select(o => BankBranchDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<BankBranchPaging> GetBankBranchListAsync(BankBranchFilter filter, PageParam pageParam, BankBranchSortByParam sortByParam)
        {
            IQueryable<BankBranchQueryResult> query = DB.BankBranches
                                             .Select(o => new BankBranchQueryResult()
                                             {
                                                 BankBranch = o,
                                                 Province = o.Province,
                                                 District = o.District,
                                                 SubDistrict = o.SubDistrict,
                                                 Bank = o.Bank,
                                                 UpdatedBy = o.UpdatedBy
                                             });

            #region Filter
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => x.BankBranch.Name.Contains(filter.Name));
            }
            if (!string.IsNullOrEmpty(filter.Address))
            {
                query = query.Where(x => x.BankBranch.Address.Contains(filter.Address));
            }
            if (!string.IsNullOrEmpty(filter.Building))
            {
                query = query.Where(x => x.BankBranch.Building.Contains(filter.Building));
            }
            if (!string.IsNullOrEmpty(filter.Soi))
            {
                query = query.Where(x => x.BankBranch.Soi.Contains(filter.Soi));
            }
            if (!string.IsNullOrEmpty(filter.Road))
            {
                query = query.Where(x => x.BankBranch.Road.Contains(filter.Road));
            }
            if (!string.IsNullOrEmpty(filter.PostalCode))
            {
                query = query.Where(x => x.BankBranch.PostalCode.Contains(filter.PostalCode));
            }
            if (!string.IsNullOrEmpty(filter.Telephone))
            {
                query = query.Where(x => x.BankBranch.Telephone.Contains(filter.Telephone));
            }
            if (!string.IsNullOrEmpty(filter.Fax))
            {
                query = query.Where(x => x.BankBranch.Fax.Contains(filter.Fax));
            }
            if (!string.IsNullOrEmpty(filter.AreaCode))
            {
                query = query.Where(x => x.BankBranch.AreaCode.Contains(filter.AreaCode));
            }
            if (!string.IsNullOrEmpty(filter.OldBankID))
            {
                query = query.Where(x => x.BankBranch.OldBankID.Contains(filter.OldBankID));
            }
            if (!string.IsNullOrEmpty(filter.OldBranchID))
            {
                query = query.Where(x => x.BankBranch.OldBranchID.Contains(filter.OldBranchID));
            }
            if (filter.BankID != Guid.Empty && filter.BankID != null)
            {
                query = query.Where(x => x.BankBranch.BankID == filter.BankID);
            }
            if (filter.DistrictID != Guid.Empty && filter.DistrictID != null)
            {
                query = query.Where(x => x.BankBranch.DistrictID == filter.DistrictID);
            }
            if (filter.SubDistrictID != Guid.Empty && filter.SubDistrictID != null)
            {
                query = query.Where(x => x.BankBranch.SubDistrictID == filter.SubDistrictID);
            }
            if (filter.ProvinceID != Guid.Empty && filter.ProvinceID != null)
            {
                query = query.Where(x => x.BankBranch.ProvinceID == filter.ProvinceID);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.BankBranch.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.BankBranch.Updated <= filter.UpdatedTo);
            }
            if (filter.IsActive != null)
            {
                query = query.Where(o => o.BankBranch.IsActive == filter.IsActive);
            }
            #endregion

            BankBranchDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<BankBranchQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => BankBranchDTO.CreateFromQueryResult(o))
                                     .ToList();

            return new BankBranchPaging()
            {
                PageOutput = pageOutput,
                BankBranches = results
            };
        }

        public async Task<BankBranchDTO> CreateBankBranchAsync(BankBranchDTO input)
        {
            await input.ValidateAsync(DB);

            BankBranch model = new BankBranch();
            input.ToModel(ref model);

            await DB.BankBranches.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await this.GetBankBranchAsync(model.ID);
            return result;
        }
        public async Task<BankBranchDTO> UpdateBankBranchAsync(Guid id, BankBranchDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.BankBranches.Where(o => o.ID == id)
                                            .FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await this.GetBankBranchAsync(id);
            return result;
        }
        public async Task<BankBranchDTO> GetBankBranchAsync(Guid id)
        {
            var model = await DB.BankBranches.Where(o => o.ID == id)
                                            .Include(o => o.Province)
                                            .Include(o => o.District)
                                            .Include(o => o.SubDistrict)
                                            .Include(o => o.Bank)
                                            .Include(o => o.UpdatedBy)
                                            .FirstAsync();

            var result = BankBranchDTO.CreateFromModel(model);
            return result;
        }
        public async Task<BankBranch> DeleteBankBranchAsync(Guid id)
        {

            var model = await DB.BankBranches.Where(o => o.ID == id).FirstOrDefaultAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
