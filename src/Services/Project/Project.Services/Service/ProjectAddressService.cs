using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using Project.Params.Outputs;

namespace Project.Services
{
    public class ProjectAddressService : IProjectAddressService
    {
        private DatabaseContext DB;

        public ProjectAddressService(DatabaseContext db)
        {
            this.DB = db;
        }
        public async Task<List<ProjectAddressListDTO>> GetProjectAddressDropdownListAsync(Guid projectID, string name, string projectAddressTypeKey)
        {
            IQueryable<Database.Models.PRJ.Address> query = DB.Addresses.Where(o => o.ProjectID == projectID).Include(o => o.UpdatedBy);
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.AddressNameTH.Contains(name));
            }
            if (!string.IsNullOrEmpty(projectAddressTypeKey))
            {
                var projectAddressTypeID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectAddressType && o.Key == projectAddressTypeKey).Select(o => o.ID).FirstOrDefaultAsync();
                query = query.Where(o => o.ProjectAddressTypeMasterCenterID == projectAddressTypeID);
            }

            var queryResults = await query.OrderBy(o => o.AddressNameTH).ToListAsync();
            var results = queryResults.Select(o => ProjectAddressListDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<ProjectAddressDTO> CreateProjectAddressAsync(Guid projectID, ProjectAddressDTO input)
        {
            var model = new Address();
            input.ToModel(ref model);
            model.ProjectAddressTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectAddressType && o.Key == input.ProjectAddressType.Key).Select(o => o.ID).FirstOrDefaultAsync();
            model.ProjectID = projectID;

            await DB.AddAsync(model);
            await DB.SaveChangesAsync();

            var result = await this.GetProjectAddressAsync(model.ID);
            return result;
        }

        public async Task DeleteProjectAddressAsync(Guid id)
        {
            var model = await DB.Addresses.FirstAsync(o => o.ID == id);
            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;

            await DB.SaveChangesAsync();
        }

        public async Task<ProjectAddressDTO> GetProjectAddressAsync(Guid id)
        {
            var model = await DB.Addresses.Where(o => o.ID == id)
                                        .Include(o => o.ProjectAddressType)
                                        .Include(o => o.Province)
                                        .Include(o => o.District)
                                        .Include(o => o.SubDistrict)
                                        .Include(o => o.HouseSubDistrict)
                                        .Include(o => o.TitledeedSubDistrict)
                                        .Include(o => o.LandOffice)
                                        .Include(o => o.UpdatedBy)
                                        .FirstAsync();
            var result = ProjectAddressDTO.CreateFromModel(model);
            return result;
        }

        public async Task<AddressPaging> GetProjectAddressListAsync(Guid projectID, PageParam pageParam, SortByParam sortByParam)
        {
            IQueryable<ProjectAddressQueryResult> query = DB.Addresses.Where(o => o.ProjectID == projectID)
                                           .Select(x => new ProjectAddressQueryResult
                                           {
                                               Address = x,
                                               District = x.District,
                                               ProjectAddressType = x.ProjectAddressType,
                                               Province = x.Province,
                                               SubDistrict = x.SubDistrict,
                                               UpdatedBy = x.UpdatedBy
                                           });

            ProjectAddressListDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<ProjectAddressQueryResult>(pageParam, ref query);

            var results = await query.Select(o => ProjectAddressListDTO.CreateFromQueryResult(o)).ToListAsync();

            return new AddressPaging()
            {
                PageOutput = pageOutput,
                ProjectAddresses = results
            };
        }

        public async Task<ProjectAddressDTO> UpdateProjectAddressAsync(Guid projectID, Guid id, ProjectAddressDTO input)
        {
            var model = await DB.Addresses.FirstAsync(o => o.ID == id);
            input.ToModel(ref model);
            DB.Entry(model).State = EntityState.Modified;

            await DB.SaveChangesAsync();

            var result = await this.GetProjectAddressAsync(id);
            return result;
        }
    }
}
