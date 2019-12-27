using Base.DTOs.PRJ;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public class ProjectInfoService : IProjectInfoService
    {
        private readonly DatabaseContext DB;

        public ProjectInfoService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<ProjectInfoDTO> GetProjectInfoAsync(Guid id)
        {
            var model = await DB.Projects.Where(o => o.ID == id)
                                        .Include(o => o.ProjectStatus)
                                      .Include(o => o.Brand)
                                      .Include(o => o.Company)
                                      .Include(o => o.ProductType)
                                      .Include(o => o.ProjectType)
                                      .Include(o => o.BG)
                                      .Include(o => o.SubBG)
                                      .Include(o => o.MortgageBank)
                                      .Include(o => o.UpdatedBy)
                                      .FirstAsync();
            var result = await ProjectInfoDTO.CreateFromModelAsync(model, DB);
            return result;
        }
        public async Task<ProjectInfoDTO> UpdateProjectInfoAsync(Guid id, ProjectInfoDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.Projects.Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);

            var generalDataStatus = await this.GeneralDataStatus(model.ID);

            model.GeneralDataStatusMasterCenterID = generalDataStatus;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var result = await this.GetProjectInfoAsync(model.ID);
            return result;
        }

        private async Task<Guid> GeneralDataStatus(Guid projectID)
        {
            var model = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var projectAddressTypeKeySale = new List<string> { "1", "2" };
            var projectAddressTypeKeyTransfer = "3";
            var allProjectAdressSale = await DB.Addresses.Include(o => o.ProjectAddressType).Where(o => o.ProjectID == projectID && projectAddressTypeKeySale.Contains(o.ProjectAddressType.Key)).ToListAsync();
            var allProjectAdrresTransfer = await DB.Addresses.Include(o => o.ProjectAddressType).Where(o => o.ProjectID == projectID && o.ProjectAddressType.Key == projectAddressTypeKeyTransfer).ToListAsync();
            var generalDataStatusSaleMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Sale).Select(o => o.ID).FirstAsync();
            var generalDataStatusPrepareMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Draft).Select(o => o.ID).FirstAsync();
            var generalDataStatusTransferMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Transfer).Select(o => o.ID).FirstAsync();
            var checkGeneralDataStatus = generalDataStatusPrepareMasterCenterID;

            if (allProjectAdressSale.Count() + allProjectAdrresTransfer.Count() == 0)
            {
                return generalDataStatusPrepareMasterCenterID;
            }

            if (!string.IsNullOrEmpty(model.ProjectNo)
                && !string.IsNullOrEmpty(model.SapCode)
                && !string.IsNullOrEmpty(model.ProjectNameTH)
                && !string.IsNullOrEmpty(model.ProjectNameEN)
                && model.ProductTypeMasterCenterID != null
                && model.ProjectStatusMasterCenterID != null
                && model.BGID != null
                && model.SubBGID != null
                && model.BrandID != null
                && model.CompanyID != null
                && !string.IsNullOrEmpty(model.CostCenterCode)
                && !string.IsNullOrEmpty(model.ProfitCenterCode)
                && allProjectAdressSale.Count() > 0
                && allProjectAdressSale.TrueForAll(o => o.ProjectAddressTypeMasterCenterID != null
                    && !string.IsNullOrEmpty(o.TitleDeedNo)
                    && !string.IsNullOrEmpty(o.LandNo)
                    && !string.IsNullOrEmpty(o.InspectionNo)
                    && !string.IsNullOrEmpty(o.PostalCode)
                    && o.ProvinceID != null
                    && o.DistrictID != null
                    && o.SubDistrictID != null)
                )
            {
                checkGeneralDataStatus = generalDataStatusSaleMasterCenterID;
            }

            if (!string.IsNullOrEmpty(model.ProjectNo)
                && !string.IsNullOrEmpty(model.SapCode)
                && !string.IsNullOrEmpty(model.ProjectNameTH)
                && !string.IsNullOrEmpty(model.ProjectNameEN)
                && model.ProductTypeMasterCenterID != null
                && model.ProjectStatusMasterCenterID != null
                && model.BGID != null
                && model.SubBGID != null
                && model.BrandID != null
                && model.CompanyID != null
                && !string.IsNullOrEmpty(model.CostCenterCode)
                && !string.IsNullOrEmpty(model.ProfitCenterCode)
                && allProjectAdrresTransfer.Count() > 0
                && allProjectAdressSale.TrueForAll(o => o.ProjectAddressTypeMasterCenterID != null
                    && !string.IsNullOrEmpty(o.TitleDeedNo)
                    && !string.IsNullOrEmpty(o.LandNo)
                    && !string.IsNullOrEmpty(o.InspectionNo)
                    && !string.IsNullOrEmpty(o.PostalCode)
                    && o.ProvinceID != null
                    && o.DistrictID != null
                    && o.SubDistrictID != null)
                && allProjectAdrresTransfer.TrueForAll(o => o.ProjectAddressType != null
                        && !string.IsNullOrEmpty(o.TitleDeedNo)
                        && !string.IsNullOrEmpty(o.LandNo)
                        && !string.IsNullOrEmpty(o.InspectionNo)
                        && !string.IsNullOrEmpty(o.PostalCode)
                        && o.ProjectID != null
                        && o.DistrictID != null
                        && o.HouseSubDistrictID != null
                        && o.TitledeedSubDistrictID != null
                ))
            {
                checkGeneralDataStatus = generalDataStatusTransferMasterCenterID;
            }

            return checkGeneralDataStatus;
        }
    }
}
