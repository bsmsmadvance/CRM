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
    public class AgreementService : IAgreementService
    {
        private readonly DatabaseContext DB;
        public AgreementService(DatabaseContext db)
        {
            this.DB = db;
        }
        public async Task<AgreementDTO> GetAgreementAsync(Guid projectID)
        {
            var model = await DB.AgreementConfigs.Where(o => o.ProjectID == projectID)
                                                 .Include(o => o.LegalEntity)
                                                 .Include(o => o.UpdatedBy)
                                                 .FirstAsync();
            var result = AgreementDTO.CreateFromModel(model);
            return result;
        }
        public async Task<AgreementDTO> UpdateAgreementAsync(Guid projectID, Guid id, AgreementDTO input)
        {
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var model = await DB.AgreementConfigs.Where(x => x.ID == id && x.ProjectID == projectID)
                                      .Include(o => o.LegalEntity)
                                      .FirstAsync();
            input.ToModel(ref model);

            var agreementDataStatusMasterCenterID =await this.AgreementDataStatus(projectID);

            project.AgreementDataStatusMasterCenterID = agreementDataStatusMasterCenterID;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = AgreementDTO.CreateFromModel(model);
            return result;
        }
        private async Task<Guid> AgreementDataStatus(Guid projectID)
        {
            var model = await DB.AgreementConfigs.Where(o => o.ProjectID == projectID).FirstAsync();
            var agreementDataStatusReadyToContractMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Contract).Select(o => o.ID).FirstAsync();
            var agreementDataStatusTransferMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Transfer).Select(o => o.ID).FirstAsync();
            var agreementDataStatusPrepareMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Draft).Select(o => o.ID).FirstAsync();
            var agreementDataStatusMasterCenterID = agreementDataStatusPrepareMasterCenterID;

            if (!string.IsNullOrEmpty(model.AttorneyNameTH1) 
                && !string.IsNullOrEmpty(model.AttorneyNameEN1)
                && !string.IsNullOrEmpty(model.PreferApproveName)
                )
            {
                agreementDataStatusMasterCenterID = agreementDataStatusReadyToContractMasterCenterID; 
            }
            if (!string.IsNullOrEmpty(model.AttorneyNameTH1) 
                && !string.IsNullOrEmpty(model.AttorneyNameEN1) 
                && !string.IsNullOrEmpty(model.PreferApproveName) 
                && !string.IsNullOrEmpty(model.AttorneyNameTransfer)
                )
            {
                agreementDataStatusMasterCenterID = agreementDataStatusTransferMasterCenterID;
            }

            return agreementDataStatusMasterCenterID;
        }
    }
}
