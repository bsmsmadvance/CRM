using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using ErrorHandling;
using ExcelExtensions;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Project.Params.Filters;
using Project.Params.Outputs;
using Project.Services.Excels;
using Report.Integration;
using Report.Integration.PrintForms.MD;
using Report.Integration.Reports.MD;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public class ProjectService : IProjectService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;

        public ProjectService(IConfiguration configuration, DatabaseContext db)
        {
            this.DB = db;
            this.Configuration = configuration;

            var minioEndpoint = Configuration["Minio:Endpoint"];
            var minioAccessKey = Configuration["Minio:AccessKey"];
            var minioSecretKey = Configuration["Minio:SecretKey"];
            var minioBucketName = Configuration["Minio:DefaultBucket"];
            var minioTempBucketName = Configuration["Minio:TempBucket"];

            this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName);
        }

        public async Task<List<ProjectDropdownDTO>> GetProjectDropdownListAsync(string name, Guid? companyID, bool isActive, string projectStatusKey)
        {
            IQueryable<Database.Models.PRJ.Project> query = DB.Projects
                    .Include(o => o.ProjectStatus)
                    .Include(o => o.ProductType)
                    .Include(o => o.BG)
                    .Where(o => o.IsActive == isActive);
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.ProjectNameTH.Contains(name) || o.ProjectNo.Contains(name));
            }
            if (companyID != null && companyID != Guid.Empty)
            {
                query = query.Where(o => o.CompanyID == companyID);
            }
            if (!string.IsNullOrEmpty(projectStatusKey))
            {
                var projectStatusMasterCenterID = await DB.MasterCenters.Where(o => o.Key == projectStatusKey
                                                                      && o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectStatus)
                                                                     .Select(o => o.ID).FirstAsync();
                query = query.Where(o => o.ProjectStatusMasterCenterID == projectStatusMasterCenterID);
            }

            var queryResults = await query.OrderBy(o => o.ProjectNo).ThenBy(o => o.ProjectNameTH).OrderBy(o => o.ProjectNo).Take(100).ToListAsync();

            var results = queryResults.Select(o => ProjectDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<ProjectPaging> GetProjectListAsync(ProjectsFilter filter, PageParam pageParam, ProjectSortByParam sortByParam)
        {
            IQueryable<ProjectQueryResult> query = DB.Projects
                                                     .Select(o => new ProjectQueryResult
                                                     {
                                                         Project = o,
                                                         Brand = o.Brand,
                                                         Company = o.Company,
                                                         ProductType = o.ProductType,
                                                         ProjectStatus = o.ProjectStatus,
                                                         UpdatedBy = o.UpdatedBy
                                                     });

            #region Filter
            if (!string.IsNullOrEmpty(filter.ProjectNo))
            {
                query = query.Where(x => x.Project.ProjectNo.Contains(filter.ProjectNo));
            }
            if (!string.IsNullOrEmpty(filter.ProjectNameTH))
            {
                query = query.Where(x => x.Project.ProjectNameTH.Contains(filter.ProjectNameTH));
            }
            if (!string.IsNullOrEmpty(filter.ProjectNameEN))
            {
                query = query.Where(x => x.Project.ProjectNameEN.Contains(filter.ProjectNameEN));
            }
            if (filter.BrandID != null && filter.BrandID != Guid.Empty)
            {
                query = query.Where(x => x.Project.BrandID == filter.BrandID);
            }
            if (filter.CompanyID != null && filter.CompanyID != Guid.Empty)
            {
                query = query.Where(x => x.Project.CompanyID == filter.CompanyID);
            }
            if (!string.IsNullOrEmpty(filter.ProductTypeKey))
            {
                var productTypeMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.ProductTypeKey
                                                                       && x.MasterCenterGroupKey == "ProductType")
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Project.ProductTypeMasterCenterID == productTypeMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.ProjectStatusKeys))
            {
                var projectStatusKeys = filter.ProjectStatusKeys.Split(',');
                var projectStatusMasterCenterIDs = await DB.MasterCenters.Where(x => projectStatusKeys.Contains(x.Key)
                                                                          && x.MasterCenterGroupKey == "ProjectStatus")
                                                                         .Select(x => x.ID).ToListAsync();
                query = query.Where(x => projectStatusMasterCenterIDs.Contains(x.Project.ProjectStatusMasterCenterID.Value));
            }
            if (filter.IsActive != null)
            {
                query = query.Where(x => x.Project.IsActive == filter.IsActive);

            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.Project.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Project.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Project.Updated >= filter.UpdatedFrom && x.Project.Updated <= filter.UpdatedTo);
            }
            #endregion

            ProjectDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<ProjectQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => ProjectDTO.CreateFromQueryResult(o)).ToList();

            //นับจำนวน Unit
            var availableStatusIDs = await DB.MasterCenters
                .Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus && o.Key == UnitStatusKeys.Available)
                .Select(o => o.ID).ToListAsync();
            var bookingStatusIDs = await DB.MasterCenters
                .Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus && (o.Key == UnitStatusKeys.WaitingForConfirmBooking || o.Key == UnitStatusKeys.WaitingForAgreement || o.Key == UnitStatusKeys.WaitingForTransfer))
                .Select(o => o.ID).ToListAsync();
            var transferStatusIDs = await DB.MasterCenters
                .Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus && (o.Key == UnitStatusKeys.Transfer || o.Key == UnitStatusKeys.PreTransfer))
                .Select(o => o.ID).ToListAsync();
            foreach (var item in results)
            {
                item.UnitCount = new ProjectUnitCountDTO();
                item.UnitCount.Available = await DB.Units.CountAsync(o => o.ProjectID == item.Id && availableStatusIDs.Contains(o.UnitStatusMasterCenterID ?? Guid.Empty));
                item.UnitCount.Booking = await DB.Units.CountAsync(o => o.ProjectID == item.Id && bookingStatusIDs.Contains(o.UnitStatusMasterCenterID ?? Guid.Empty));
                item.UnitCount.Transfer = await DB.Units.CountAsync(o => o.ProjectID == item.Id && transferStatusIDs.Contains(o.UnitStatusMasterCenterID ?? Guid.Empty));
            }

            return new ProjectPaging()
            {
                PageOutput = pageOutput,
                Projects = results
            };
        }

        public async Task<ProjectDTO> GetProjectAsync(Guid id)
        {
            var model = await DB.Projects
                .Include(o => o.Brand)
                .Include(o => o.Company)
                .Include(o => o.ProductType)
                .Include(o => o.ProjectStatus)
                .Include(o => o.UpdatedBy)
                .FirstAsync(o => o.ID == id);
            var result = ProjectDTO.CreateFromModel(model);

            return result;
        }

        public async Task<ProjectDTO> CreateProjectAsync(ProjectDTO input)
        {
            await input.ValidateAsync(DB);
            Database.Models.PRJ.Project model = new Database.Models.PRJ.Project();
            input.ToModel(ref model);

            var projectStatusPrepareID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectStatus" && o.Key == "0").Select(o => o.ID).FirstAsync();
            var projectDataStatusPrepareID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == "0").Select(o => o.ID).FirstAsync();
            model.ProjectStatusMasterCenterID = projectStatusPrepareID;

            #region Tab
            model.GeneralDataStatusMasterCenterID = projectDataStatusPrepareID;
            model.AgreementDataStatusMasterCenterID = projectDataStatusPrepareID;
            model.ModelDataStatusMasterCenterID = projectDataStatusPrepareID;
            model.TowerDataStatusMasterCenterID = projectDataStatusPrepareID;
            model.UnitDataStatusMasterCenterID = projectDataStatusPrepareID;
            model.TitleDeedDataStatusMasterCenterID = projectDataStatusPrepareID;
            model.PictureDataStatusMasterCenterID = projectDataStatusPrepareID;
            model.MinPriceDataStatusMasterCenterID = projectDataStatusPrepareID;
            model.PriceListDataStatusMasterCenterID = projectDataStatusPrepareID;
            model.TransferFeeDataStatusMasterCenterID = projectDataStatusPrepareID;
            model.BudgetProDataStatusMasterCenterID = projectDataStatusPrepareID;
            model.WaiveDataStatusMasterCenterID = projectDataStatusPrepareID;
            #endregion

            Database.Models.PRJ.AgreementConfig agreement = new Database.Models.PRJ.AgreementConfig();
            agreement.ProjectID = model.ID;
            agreement.IsPrintAgreementForBuyer = true;
            agreement.IsPrintAgreementForSeller = input.ProductType?.Key == ProductTypeKeys.HighRise ? true : false;
            agreement.IsPrintAgreementForRevenue = true;
            agreement.IsPrintAgreementEmpty = true;

            await DB.Projects.AddAsync(model);
            await DB.AgreementConfigs.AddAsync(agreement);
            await DB.SaveChangesAsync();

            var data = await DB.Projects.Where(o => o.ID == model.ID)
                                      .Include(o => o.Brand)
                                      .Include(o => o.Company)
                                      .Include(o => o.ProductType)
                                      .Include(o => o.ProjectStatus)
                                      .FirstAsync();

            var result = ProjectDTO.CreateFromModel(data);
            return result;
        }

        public async Task<Database.Models.PRJ.Project> DeleteProjectAsync(Guid id, string reason)
        {
            var model = await DB.Projects.Where(o => o.ID == id).FirstOrDefaultAsync();
            model.DeleteReason = reason;
            model.IsDeleted = true;
            await DB.SaveChangesAsync();

            return model;
        }

        /// <summary>
        /// ดึงข้อมูลสถานะของโครงการแต่ละ tab
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProjectDataStatusDTO> GetProjectDataStatusAsync(Guid id)
        {
            var model = await DB.Projects.Where(o => o.ID == id)
                                         .Include(o => o.GeneralDataStatus)
                                         .Include(o => o.AgreementDataStatus)
                                         .Include(o => o.ModelDataStatus)
                                         .Include(o => o.TowerDataStatus)
                                         .Include(o => o.UnitDataStatus)
                                         .Include(o => o.TitleDeedDataStatus)
                                         .Include(o => o.PictureDataStatus)
                                         .Include(o => o.MinPriceDataStatus)
                                         .Include(o => o.PriceListDataStatus)
                                         .Include(o => o.TransferFeeDataStatus)
                                         .Include(o => o.BudgetProDataStatus)
                                         .Include(o => o.WaiveDataStatus)
                                         .Include(o => o.ProjectStatus)
                                         .FirstAsync();
            var result = ProjectDataStatusDTO.CreateFromModel(model);
            return result;
        }

        /// <summary>
        /// ดึงข้อมูลจำนวนโปรเจคแบ่งตามสถานะ Active
        /// </summary>
        /// <returns></returns>
        public async Task<ProjectCountDTO> GetProjectCountAsync()
        {
            var result = new ProjectCountDTO
            {
                All = await DB.Projects.Where(o => o.IsActive == true || o.IsActive == false).CountAsync(),
                Inactive = await DB.Projects.Where(o => o.IsActive == false).CountAsync(),
                Active = await DB.Projects.Where(o => o.IsActive == true).CountAsync()
            };
            return result;
        }

        public async Task UpdateProjectStatus(Guid projectID, MasterCenterDropdownDTO projectStatus)
        {
            var project = await DB.Projects.Where(o => o.ID == projectID)
                                           .Include(o => o.ProjectStatus)
                                           .FirstAsync();
            ValidateException ex = new ValidateException();
            if (project.ProjectStatus.Key == ProjectStatusKeys.Active && projectStatus.Key == ProjectStatusKeys.InActive)
            {
                project.ProjectStatusMasterCenterID = projectStatus?.Id;
                project.IsActive = false;

                DB.Update(project);
                await DB.SaveChangesAsync();
            }
            else
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR9999").FirstAsync();
                string desc = "สามารถแก้ไขสถานะโครงการได้เฉพาะ อยู่ระหว่างขาย ไปเป็นปิดการขาย เท่านั้น";
                var msg = errMsg.Message.Replace("[message]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public async Task<ReportResult> GetExportBookingTemplateUrlAsync(Guid projectID)
        {
            var projectNo = await DB.Projects.Where(o => o.ID == projectID).Select(o => o.ProjectNo).FirstAsync();
            ReportFactory reportFactory = new ReportFactory(Configuration,  ReportFolder.MD, "PF_MD_001");
            reportFactory.AddParameter("projectNo", projectNo);
            return reportFactory.CreateUrl();

            //var projectNo = await DB.Projects.Where(o => o.ID == projectID).Select(o => o.ProjectNo).FirstAsync();
            //ReportFactory reportFactory = new ReportFactory(Configuration["Report:Url"], Configuration["Report:SecretKey"]);
            //var reportUrl = reportFactory.CreateUrl<PF_MD_001>(new PF_MD_001()
            //{
            //    AgreementNo = projectNo
            //});

            //return new StringResult()
            //{
            //    Result = reportUrl
            //};
        }

        public async Task<ReportResult> GetExportAgreementTemplateUrlAsync(Guid projectID)
        {
            var projectNo = await DB.Projects.Where(o => o.ID == projectID).Select(o => o.ProjectNo).FirstAsync();
            ReportFactory reportFactory = new ReportFactory(Configuration, ReportFolder.MD, "PF_MD_002");
            reportFactory.AddParameter("projectNo", projectNo);
            return reportFactory.CreateUrl();

            //var projectNo = await DB.Projects.Where(o => o.ID == projectID).Select(o => o.ProjectNo).FirstAsync();
            //ReportFactory reportFactory = new ReportFactory(Configuration["Report:Url"], Configuration["Report:SecretKey"]);
            //var reportUrl = reportFactory.CreateUrl<PF_MD_002>(new PF_MD_002()
            //{
            //    ProjectNo = projectNo
            //});

            //return new StringResult()
            //{
            //    Result = reportUrl
            //};
        }

        public async Task<ReportResult> GetExportProjectListUrlAsync(ProjectsFilter filter, ShowAs downloadAs)
        {
            ReportFactory reportFactory = new ReportFactory(Configuration, ReportFolder.MD, "RP_Master_001");
            reportFactory.AddParameter("BrandID", filter.BrandID);
            reportFactory.AddParameter("CompanyID", filter.CompanyID);
            reportFactory.AddParameter("IsActive", filter.IsActive);
            reportFactory.AddParameter("ProductTypeKey", filter.ProductTypeKey);
            reportFactory.AddParameter("ProjectNameTH", filter.ProjectNameTH);
            reportFactory.AddParameter("ProjectNo", filter.ProjectNo);
            reportFactory.AddParameter("ProjectStatusKeys", filter.ProjectStatusKeys);
            return reportFactory.CreateUrl();

            //ReportFactory reportFactory = new ReportFactory(Configuration["Report:Url"], Configuration["Report:SecretKey"]);
            //var reportUrl = reportFactory.CreateUrl<RP_Master_001>(new RP_Master_001()
            //{
            //    BrandID = filter.BrandID,
            //    CompanyID = filter.CompanyID,
            //    IsActive = filter.IsActive,
            //    ProductTypeKey = filter.ProductTypeKey,
            //    ProjectNameTH = filter.ProjectNameTH,
            //    ProjectNo = filter.ProjectNo,
            //    ProjectStatusKeys = filter.ProjectStatusKeys
            //}, downloadAs);

            //return new StringResult()
            //{
            //    Result = reportUrl
            //};
        }

    }
}
