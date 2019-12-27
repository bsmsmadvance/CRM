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
using MasterData.Params.Outputs;
using PagingExtensions;
using Base.DTOs;

namespace MasterData.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly DatabaseContext DB;

        public CompanyService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<List<CompanyDropdownDTO>> GetCompanyDropdownListAsync(CompanyDropdownFilter filter)
        {
            IQueryable<Company> query = DB.Companies;
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(o => o.NameTH.Contains(filter.Name) || o.Code.Contains(filter.Name));
            }

            var queryResults = await query.OrderBy(o => o.Code).Take(100).ToListAsync();

            var results = queryResults.Select(o => CompanyDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<CompanyPaging> GetCompanyListAsync(CompanyFilter filter, PageParam pageParam, CompanySortByParam sortByParam)
        {
            IQueryable<CompanyQueryResult> query = DB.Companies.IgnoreQueryFilters().Where(o => !o.IsDeleted)
                                          .Select(x => new CompanyQueryResult
                                          {
                                              Company = x,
                                              District = x.District,
                                              SubDistrict = x.SubDistrict,
                                              Province = x.Province,
                                              UpdatedBy = x.UpdatedBy
                                          });

            if (!string.IsNullOrEmpty(filter.APAuthorizeRefID))
            {
                query = query.Where(x => x.Company.APAuthorizeRefID.Contains(filter.APAuthorizeRefID));
            }
            if (!string.IsNullOrEmpty(filter.Code))
            {
                query = query.Where(x => x.Company.Code.Contains(filter.Code));
            }
            if (!string.IsNullOrEmpty(filter.NameTH))
            {
                query = query.Where(x => x.Company.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                query = query.Where(x => x.Company.NameEN.Contains(filter.NameEN));
            }
            if (!string.IsNullOrEmpty(filter.TaxID))
            {
                query = query.Where(x => x.Company.TaxID.Contains(filter.TaxID));
            }
            if (!string.IsNullOrEmpty(filter.AddressTH))
            {
                query = query.Where(x => x.Company.AddressTH.Contains(filter.AddressTH));
            }
            if (!string.IsNullOrEmpty(filter.AddressEN))
            {
                query = query.Where(x => x.Company.AddressEN.Contains(filter.AddressEN));
            }
            if (!string.IsNullOrEmpty(filter.BuildingTH))
            {
                query = query.Where(x => x.Company.BuildingTH.Contains(filter.BuildingTH));
            }
            if (!string.IsNullOrEmpty(filter.BuildingEN))
            {
                query = query.Where(x => x.Company.BuildingEN.Contains(filter.BuildingEN));
            }
            if (!string.IsNullOrEmpty(filter.SoiTH))
            {
                query = query.Where(x => x.Company.SoiTH.Contains(filter.SoiTH));
            }
            if (!string.IsNullOrEmpty(filter.SoiEN))
            {
                query = query.Where(x => x.Company.SoiEN.Contains(filter.SoiEN));
            }
            if (!string.IsNullOrEmpty(filter.RoadTH))
            {
                query = query.Where(x => x.Company.RoadTH.Contains(filter.RoadTH));
            }
            if (!string.IsNullOrEmpty(filter.RoadEN))
            {
                query = query.Where(x => x.Company.RoadEN.Contains(filter.RoadEN));
            }
            if (!string.IsNullOrEmpty(filter.PostalCode))
            {
                query = query.Where(x => x.Company.PostalCode.Contains(filter.PostalCode));
            }
            if (!string.IsNullOrEmpty(filter.Telephone))
            {
                query = query.Where(x => x.Company.Telephone.Replace("-", "").Contains(filter.Telephone.Replace("-", "")));
            }
            if (!string.IsNullOrEmpty(filter.Fax))
            {
                query = query.Where(x => x.Company.Fax.Replace("-", "").Contains(filter.Fax.Replace("-", "")));
            }
            if (!string.IsNullOrEmpty(filter.Website))
            {
                query = query.Where(x => x.Company.Website.Contains(filter.Website));
            }
            if (!string.IsNullOrEmpty(filter.SapCompanyID))
            {
                query = query.Where(x => x.Company.SAPCompanyID.Contains(filter.SapCompanyID));
            }
            if (!string.IsNullOrEmpty(filter.NameTHOld))
            {
                query = query.Where(x => x.Company.NameTHOld.Contains(filter.NameTHOld));
            }
            if (!string.IsNullOrEmpty(filter.NameENOld))
            {
                query = query.Where(x => x.Company.NameENOld.Contains(filter.NameENOld));
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.Company.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Company.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Company.Updated >= filter.UpdatedFrom && x.Company.Updated <= filter.UpdatedTo);
            }
            if (filter.ProvinceID != null && filter.ProvinceID != Guid.Empty)
            {
                query = query.Where(x => x.Company.ProvinceID == filter.ProvinceID);
            }
            if (filter.DistrictID != null && filter.DistrictID != Guid.Empty)
            {
                query = query.Where(x => x.Company.DistrictID == filter.DistrictID);
            }
            if (filter.SubDistrictID != null && filter.SubDistrictID != Guid.Empty)
            {
                query = query.Where(x => x.Company.SubDistrictID == filter.SubDistrictID);
            }
            if (filter.IsUseInCRM != null)
            {
                query = query.Where(o => o.Company.IsUseInCRM == filter.IsUseInCRM);
            }

            CompanyDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<CompanyQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var resultsDTO = queryResults.Select(o => CompanyDTO.CreateFromQueryResult(o)).ToList();

            return new CompanyPaging()
            {
                Companies = resultsDTO,
                PageOutput = pageOutput
            };
        }

        public async Task<CompanyDTO> GetCompanyAsync(Guid id)
        {
            var model = await DB.Companies.IgnoreQueryFilters().Where(o => !o.IsDeleted)
                                .Include(o => o.Province)
                                .Include(o => o.District)
                                .Include(o => o.SubDistrict)
                                .Include(o => o.UpdatedBy)
                                .Where(o => o.ID == id)
                                .FirstAsync();
            var result = CompanyDTO.CreateFromModel(model);
            return result;
        }

        public async Task<CompanyDTO> CreateCompanyAsync(CompanyDTO input)
        {
            Company model = new Company();
            input.ToModel(ref model);

            await DB.Companies.AddAsync(model);
            await DB.SaveChangesAsync();

            var result = await this.GetCompanyAsync(model.ID);
            return result;
        }

        public async Task<CompanyDTO> UpdateCompanyAsync(Guid id, CompanyDTO input)
        {
            var model = await DB.Companies.IgnoreQueryFilters().Where(o => !o.IsDeleted).Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);


            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var result = await this.GetCompanyAsync(model.ID);
            return result;
        }

        public async Task<Company> DeleteCompanyAsync(Guid id)
        {
            var model = await DB.Companies.IgnoreQueryFilters().Where(o => !o.IsDeleted).Where(o => o.ID == id).FirstOrDefaultAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();

            return model;
        }
    }
}
