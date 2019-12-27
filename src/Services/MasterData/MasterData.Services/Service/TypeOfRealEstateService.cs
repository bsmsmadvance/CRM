using Base.DTOs;
using Base.DTOs.MST;
using Database.Models;
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
    public class TypeOfRealEstateService : ITypeOfRealEstateService
    {
        private readonly DatabaseContext DB;

        public TypeOfRealEstateService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<List<TypeOfRealEstateDropdownDTO>> GetTypeOfRealEstateDropdownListAsync(string name)
        {
            IQueryable<TypeOfRealEstate> query = DB.TypeOfRealEstates;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.Name.Contains(name));
            }


            var queryResults = await query.OrderBy(o => o.Name).Take(100).ToListAsync();

            var results = queryResults.Select(o => TypeOfRealEstateDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<TypeOfRealEstatePaging> GetTypeOfRealEstateListAsync(TypeOfRealEstateFilter filter, PageParam pageParam, TypeOfRealEstateSortByParam sortByParam)
        {
            IQueryable<TypeOfRealEstateQueryResult> query = DB.TypeOfRealEstates.Select(o => new TypeOfRealEstateQueryResult
            {
                TypeOfRealEstate = o,
                RealEstateCategory = o.RealEstateCategory,
                UpdatedBy = o.UpdatedBy
            });

            #region Filter
            if (!string.IsNullOrEmpty(filter.Code))
            {
                query = query.Where(x => x.TypeOfRealEstate.Code.Contains(filter.Code));
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => x.TypeOfRealEstate.Name.Contains(filter.Name));
            }
            if (filter.StandardCostFrom != null)
            {
                query = query.Where(x => x.TypeOfRealEstate.StandardCost >= filter.StandardCostFrom);
            }
            if (filter.StandardCostTo != null)
            {
                query = query.Where(x => x.TypeOfRealEstate.StandardCost <= filter.StandardCostTo);
            }
            if (filter.StandardCostFrom != null && filter.StandardPriceTo != null)
            {
                query = query.Where(x => x.TypeOfRealEstate.StandardCost >= filter.StandardCostFrom && x.TypeOfRealEstate.StandardCost <= filter.StandardCostTo);
            }
            if (filter.StandardPriceFrom != null)
            {
                query = query.Where(x => x.TypeOfRealEstate.StandardPrice >= filter.StandardPriceFrom);
            }
            if (filter.StandardPriceTo != null)
            {
                query = query.Where(x => x.TypeOfRealEstate.StandardPrice <= filter.StandardPriceTo);
            }
            if (filter.StandardPriceFrom != null && filter.StandardPriceTo != null)
            {
                query = query.Where(x => x.TypeOfRealEstate.StandardPrice >= filter.StandardPriceFrom && x.TypeOfRealEstate.StandardCost <= filter.StandardPriceTo);
            }
            if (!string.IsNullOrEmpty(filter.RealEstateCategoryKey))
            {
                var realEstateCategoryMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.RealEstateCategoryKey
                                                                       && x.MasterCenterGroupKey == "RealEstateCategory")
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.RealEstateCategory.ID == realEstateCategoryMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.TypeOfRealEstate.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.TypeOfRealEstate.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.TypeOfRealEstate.Updated >= filter.UpdatedFrom && x.TypeOfRealEstate.Updated <= filter.UpdatedTo);
            }
            #endregion

            TypeOfRealEstateDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<TypeOfRealEstateQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => TypeOfRealEstateDTO.CreateFromQueryResult(o)).ToList();

            return new TypeOfRealEstatePaging()
            {
                PageOutput = pageOutput,
                TypeOfRealEstates = results
            };
        }

        public async Task<TypeOfRealEstateDTO> GetTypeOfRealEstateAsync(Guid id)
        {
            var model = await DB.TypeOfRealEstates.Where(o => o.ID == id)
                                                  .Include(o => o.RealEstateCategory)
                                                  .FirstAsync();

            var result = TypeOfRealEstateDTO.CreateFromModel(model);
            return result;
        }

        public async Task<TypeOfRealEstateDTO> CreateTypeOfRealEstateAsync(TypeOfRealEstateDTO input)
        {
            await input.ValidateAsync(DB);
            TypeOfRealEstate model = new TypeOfRealEstate();
            input.ToModel(ref model);

            await DB.TypeOfRealEstates.AddAsync(model);
            await DB.SaveChangesAsync();

            var result = this.GetTypeOfRealEstateAsync(model.ID).Result;
            return result;
        }

        public async Task<TypeOfRealEstateDTO> UpdateTypeOfRealEstateAsync(Guid id, TypeOfRealEstateDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.TypeOfRealEstates.Where(o => o.ID == id).FirstAsync();

            input.ToModel(ref model);
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = this.GetTypeOfRealEstateAsync(model.ID).Result;
            return result;
        }

        public async Task<TypeOfRealEstate> DeleteTypeOfRealEstateAsync(Guid id)
        {
            var model = await DB.TypeOfRealEstates.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
