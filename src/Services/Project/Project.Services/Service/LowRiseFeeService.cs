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
    public class LowRiseFeeService : ILowRiseFeeService
    {
        private readonly DatabaseContext DB;

        public LowRiseFeeService(DatabaseContext db)
        {
            this.DB = db;
        }
        public async Task<LowRiseFeePaging> GetLowRiseFeeListAsync(Guid projectID, LowRiseFeeFilter filter, PageParam pageParam, LowRiseFeeSortByParam sortByParam)
        {
            IQueryable<LowRiseFeeQueryResult> query = DB.LowRiseFees.Where(o => o.ProjectID == projectID)
                                                                    .Select(o => new LowRiseFeeQueryResult
                                                                    {
                                                                        LowRiseFee = o,
                                                                        Unit = o.Unit,
                                                                        UpdatedBy = o.UpdatedBy
                                                                    });
            #region Fitler
            if (filter.UnitID != null && filter.UnitID != Guid.Empty)
            {
                query = query.Where(x => x.LowRiseFee.UnitID == filter.UnitID);
            }
            if (filter.EstimatePriceAreaFrom != null)
            {
                query = query.Where(x => x.LowRiseFee.EstimatePriceArea >= filter.EstimatePriceAreaFrom);
            }
            if (filter.EstimatePriceAreaTo != null)
            {
                query = query.Where(x => x.LowRiseFee.EstimatePriceArea <= filter.EstimatePriceAreaTo);
            }
            if (filter.EstimatePriceAreaFrom != null && filter.EstimatePriceAreaTo != null)
            {
                query = query.Where(x => x.LowRiseFee.EstimatePriceArea >= filter.EstimatePriceAreaFrom &&
                                         x.LowRiseFee.EstimatePriceArea <= filter.EstimatePriceAreaTo);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.LowRiseFee.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.LowRiseFee.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.LowRiseFee.Updated >= filter.UpdatedFrom && x.LowRiseFee.Updated <= filter.UpdatedTo);
            }
            if (filter.UnitID == Guid.Empty)
            {
                query = query.Where(o => o.LowRiseFee.UnitID == null);
            }
            #endregion

            LowRiseFeeDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<LowRiseFeeQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => LowRiseFeeDTO.CreateFromQueryResult(o)).ToList();

            return new LowRiseFeePaging()
            {
                PageOutput = pageOutput,
                LowRiseFees = results
            };
        }

        public async Task<LowRiseFeeDTO> GetLowRiseFeeAsync(Guid projectID, Guid id)
        {
            var model = await DB.LowRiseFees.Where(o => o.ProjectID == projectID && o.ID == id)
                                             .Include(o => o.Unit)
                                             .Include(o => o.UpdatedBy)
                                             .FirstAsync();
            var result = LowRiseFeeDTO.CreateFromModel(model);
            return result;
        }

        public async Task<LowRiseFeeDTO> CreateLowRiseFeeAsync(Guid projectID, LowRiseFeeDTO input)
        {
            await this.ValidateLowRiseFee(projectID, input);
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();

            LowRiseFee model = new LowRiseFee();
            input.ToModel(ref model);
            model.ProjectID = projectID;
            await DB.LowRiseFees.AddAsync(model);
            await DB.SaveChangesAsync();

            var transferFeeDataStatusMasterCenterID = await this.TransferFeeDataStatus(projectID);
            project.TransferFeeDataStatusMasterCenterID = transferFeeDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await this.GetLowRiseFeeAsync(projectID, model.ID);

            return result;
        }

        public async Task<LowRiseFeeDTO> UpdateLowRiseFeeAsync(Guid projectID, Guid id, LowRiseFeeDTO input)
        {
            await this.ValidateLowRiseFee(projectID, input);
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var model = await DB.LowRiseFees.Where(o => o.ProjectID == projectID && o.ID == id).FirstAsync();

            input.ToModel(ref model);
            model.ProjectID = projectID;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var transferFeeDataStatusMasterCenterID = await this.TransferFeeDataStatus(projectID);
            project.TransferFeeDataStatusMasterCenterID = transferFeeDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await this.GetLowRiseFeeAsync(projectID, id);
            return result;
        }

        public async Task<LowRiseFee> DeleteLowRiseFeeAsync(Guid projectID, Guid id)
        {
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var model = await DB.LowRiseFees.Where(o => o.ProjectID == projectID && o.ID == id).FirstAsync();

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

        private async Task ValidateLowRiseFee(Guid projectID, LowRiseFeeDTO input)
        {
            ValidateException ex = new ValidateException();

            if (input.Unit != null)
            {
                var checkUniqueUnit = input.Id != (Guid?)null
               ? await DB.LowRiseFees.Where(o => o.ProjectID == projectID && o.ID != input.Id && o.UnitID == input.Unit.Id).CountAsync() > 0
               : await DB.LowRiseFees.Where(o => o.ProjectID == projectID && o.UnitID == input.Unit.Id).CountAsync() > 0;
                if (checkUniqueUnit)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = input.GetType().GetProperty(nameof(LowRiseFeeDTO.Unit)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", input.Unit.UnitNo);
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
