using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.PRJ;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using Project.Params.Filters;
using Project.Params.Outputs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public class RoundFeeService : IRoundFeeService
    {
        private readonly DatabaseContext DB;

        public RoundFeeService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<RoundFeePaging> GetRoundFeeListAsync(Guid projectID, RoundFeeFilter filter, PageParam pageParam, RoundFeeSortByParam sortByParam)
        {
            IQueryable<RoundFeeQueryResult> query = DB.RoundFees.Where(o => o.ProjectID == projectID)
                                                                .Select(o => new RoundFeeQueryResult
                                                                {
                                                                    LandOffice = o.LandOffice,
                                                                    BusinessTaxRoundFormula = o.BusinessTaxRoundFormula,
                                                                    IncomeTaxRoundFormula = o.IncomeTaxRoundFormula,
                                                                    LocalTaxRoundFormula = o.LocalTaxRoundFormula,
                                                                    RoundFee = o,
                                                                    TransferFeeRoundFormula = o.TransferFeeRoundFormula,
                                                                    UpdatedBy = o.UpdatedBy
                                                                });
            #region Filter
            if (!string.IsNullOrEmpty(filter.IncomeTaxRoundFormulaKey))
            {
                var incomeTaxRoundFormulaID = await DB.MasterCenters.Where(x => x.Key == filter.IncomeTaxRoundFormulaKey
                                                                 && x.MasterCenterGroupKey == "RoundFormulaType")
                                                                .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.RoundFee.IncomeTaxRoundFormulaMasterCenterID == incomeTaxRoundFormulaID);
            }
            if (!string.IsNullOrEmpty(filter.BusinessTaxRoundFormulaKey))
            {
                var businessTaxRoundFormulaID = await DB.MasterCenters.Where(x => x.Key == filter.BusinessTaxRoundFormulaKey
                                                                 && x.MasterCenterGroupKey == "RoundFormulaType")
                                                                .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.RoundFee.BusinessTaxRoundFormulaMasterCenterID == businessTaxRoundFormulaID);
            }
            if (!string.IsNullOrEmpty(filter.LocalTaxRoundFormulaKey))
            {
                var localTaxRoundFormulaID = await DB.MasterCenters.Where(x => x.Key == filter.LocalTaxRoundFormulaKey
                                                                 && x.MasterCenterGroupKey == "RoundFormulaType")
                                                                .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.RoundFee.LocalTaxRoundFormulaMasterCenterID == localTaxRoundFormulaID);
            }
            if (!string.IsNullOrEmpty(filter.TransferFeeRoundFormulaKey))
            {
                var transferFeeRoundFormulaID = await DB.MasterCenters.Where(x => x.Key == filter.TransferFeeRoundFormulaKey
                                                                 && x.MasterCenterGroupKey == "RoundFormulaType")
                                                                .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.RoundFee.IncomeTaxRoundFormulaMasterCenterID == transferFeeRoundFormulaID);
            }
            if (filter.LandOfficeID != null && filter.LandOfficeID != Guid.Empty)
            {
                query = query.Where(x => x.RoundFee.LandOfficeID == filter.LandOfficeID);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.RoundFee.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.RoundFee.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.RoundFee.Updated >= filter.UpdatedFrom && x.RoundFee.Updated <= filter.UpdatedTo);
            }
            #endregion

            RoundFeeDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<RoundFeeQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => RoundFeeDTO.CreateFromQueryResult(o)).ToList();

            return new RoundFeePaging()
            {
                PageOutput = pageOutput,
                RoundFees = results
            };
        }

        public async Task<RoundFeeDTO> GetRoundFeeAsync(Guid projectID, Guid id)
        {
            var model = await DB.RoundFees.Where(o => o.ProjectID == projectID && o.ID == id)
                                          .Include(o => o.LandOffice)
                                          .Include(o => o.TransferFeeRoundFormula)
                                          .Include(o => o.BusinessTaxRoundFormula)
                                          .Include(o => o.LocalTaxRoundFormula)
                                          .Include(o => o.IncomeTaxRoundFormula)
                                          .Include(o => o.UpdatedBy)
                                          .FirstAsync();
            var result = RoundFeeDTO.CreateFromModel(model);
            return result;
        }

        public async Task<RoundFeeDTO> CreateRoundFeeAsync(Guid projectID, RoundFeeDTO input)
        {
            await this.ValidateRoundFee(projectID, input);
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            RoundFee model = new RoundFee();
            input.ToModel(ref model);
            model.ProjectID = projectID;
            await DB.RoundFees.AddAsync(model);
            await DB.SaveChangesAsync();

            var transferFeeDataStatusMasterCenterID = await this.TransferFeeDataStatus(projectID);
            project.TransferFeeDataStatusMasterCenterID = transferFeeDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await this.GetRoundFeeAsync(projectID, model.ID);

            return result;
        }

        public async Task<RoundFeeDTO> UpdateRoundFeeAsync(Guid projectID, Guid id, RoundFeeDTO input)
        {
            await this.ValidateRoundFee(projectID, input);
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var model = await DB.RoundFees.Where(o => o.ProjectID == projectID && o.ID == id).FirstAsync();

            input.ToModel(ref model);
            model.ProjectID = projectID;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var transferFeeDataStatusMasterCenterID = await this.TransferFeeDataStatus(projectID);
            project.TransferFeeDataStatusMasterCenterID = transferFeeDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await this.GetRoundFeeAsync(projectID, model.ID);
            return result;
        }

        public async Task<RoundFee> DeleteRoundFeeAsync(Guid projectID, Guid id)
        {
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var model = await DB.RoundFees.Where(o => o.ProjectID == projectID && o.ID == id).FirstAsync();

            model.IsDeleted = true;
            await DB.SaveChangesAsync();

            var transferFeeDataStatusMasterCenterID = await this.TransferFeeDataStatus(projectID);
            project.TransferFeeDataStatusMasterCenterID = transferFeeDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            return model;
        }

        private async Task<Guid> TransferFeeDataStatus(Guid projectID)
        {
            var project = await DB.Projects.Where(o => o.ID == projectID).Include(o => o.ProductType).FirstAsync();
            var allHiseRiseFee = await DB.HighRiseFees.Where(o => o.ProjectID == projectID).ToListAsync();
            var allLowRiseFees = await DB.LowRiseFees.Where(o => o.ProjectID == projectID).ToListAsync();
            var allLowRiseFenceFees = await DB.LowRiseFenceFees.Where(o => o.ProjectID == projectID).ToListAsync();
            var allLowRiseBuildingPriceFees = await DB.LowRiseBuildingPriceFees.Where(o => o.ProjectID == projectID).ToListAsync();
            var allRoundFees = await DB.RoundFees.Where(o => o.ProjectID == projectID).ToListAsync();


            var transferFeeDataStatusPrepareMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Draft).Select(o => o.ID).FirstAsync();
            var transferFeeDataStatusTransferMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Transfer).Select(o => o.ID).FirstAsync();
            var transferFeeDataStatusMasterCenterID = transferFeeDataStatusPrepareMasterCenterID;

            if (project.ProductType.Key == ProductTypeKeys.LowRise)
            {
                if (allLowRiseFees.Count() == 0 || allRoundFees.Count() == 0 || allLowRiseBuildingPriceFees.Count() == 0)
                {
                    return transferFeeDataStatusMasterCenterID;
                }
                if (allLowRiseFees.TrueForAll(o => o.EstimatePriceArea != null)
                    && allRoundFees.TrueForAll(o => o.LandOfficeID != null && o.OtherFee != null && o.LocalTaxRoundFormulaMasterCenterID != null && o.TransferFeeRoundFormulaMasterCenterID != null && o.IncomeTaxRoundFormulaMasterCenterID != null && o.BusinessTaxRoundFormulaMasterCenterID != null)
                    && allLowRiseBuildingPriceFees.TrueForAll(o => o.ModelID != null && o.UnitID != null && o.Price != null)
                    )
                {
                    transferFeeDataStatusMasterCenterID = transferFeeDataStatusTransferMasterCenterID;
                }
            }
            else
            {
                if (allRoundFees.Count() == 0)
                {
                    return transferFeeDataStatusMasterCenterID;
                }
                if (allRoundFees.TrueForAll(o => o.LandOfficeID != null && o.OtherFee != null && o.LocalTaxRoundFormulaMasterCenterID != null && o.TransferFeeRoundFormulaMasterCenterID != null && o.IncomeTaxRoundFormulaMasterCenterID != null && o.BusinessTaxRoundFormulaMasterCenterID != null))
                {
                    transferFeeDataStatusMasterCenterID = transferFeeDataStatusTransferMasterCenterID;
                }
            }
            return transferFeeDataStatusMasterCenterID;
        }

        private async Task ValidateRoundFee(Guid projectID, RoundFeeDTO input)
        {
            ValidateException ex = new ValidateException();

            if (input.LandOffice != null)
            {
                var checkUniqueLandOffice = input.Id != (Guid?)null
               ? await DB.RoundFees.Where(o => o.ProjectID == projectID && o.ID != input.Id && o.LandOfficeID == input.LandOffice.Id).CountAsync() > 0
               : await DB.RoundFees.Where(o => o.ProjectID == projectID && o.LandOfficeID == input.LandOffice.Id).CountAsync() > 0;
                if (checkUniqueLandOffice)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = input.GetType().GetProperty(nameof(RoundFeeDTO.LandOffice)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", input.LandOffice.NameTH);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }
    }
}
