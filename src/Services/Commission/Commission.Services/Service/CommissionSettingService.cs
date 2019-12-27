using Database.Models;
using Database.Models.CMS;
using Database.Models.PRJ;
using Commission.Params.Filters;
using Commission.Params.Inputs;
using Base.DTOs.CMS;
using Base.DTOs.USR;
using Base.DTOs.PRJ;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commission.Params.Outputs;
using PagingExtensions;
using Base.DTOs;

namespace Commission.Services
{
    public class CommissionSettingService : ICommissionSettingService
    {
        private readonly DatabaseContext DB;

        public CommissionSettingService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<CommissionSettingPaging> GetCommissionSettingListAsync(CommissionSettingFilter filter, PageParam pageParam, CommissionSettingSortByParam sortByParam)
        {
            IQueryable<CommissionSettingQueryResult> query = from pj in DB.Projects

                                                                 /*---New Luanch---------------------------------------------------------------------*/
                                                             join gsMin in DB.GeneralSettings on pj.ID equals gsMin.ProjectID into gMin1
                                                             from gssMin in gMin1.DefaultIfEmpty()
                                                             where (gssMin == null
                                                                    || (gssMin.IsActive && gssMin.ActiveDate == (DB.GeneralSettings.Where(n => n.ProjectID == pj.ID && n.IsActive).OrderBy(n => n.ActiveDate).Max(n => n.ActiveDate))))
                                                             join gsMax in DB.GeneralSettings on pj.ID equals gsMax.ProjectID into gMax1
                                                             from gssMax in gMax1.DefaultIfEmpty()
                                                             where (gssMax == null
                                                                    || (gssMax.IsActive && gssMax.ActiveDate == (DB.GeneralSettings.Where(n => n.ProjectID == pj.ID && n.IsActive).OrderByDescending(n => n.ActiveDate).Max(n => n.ActiveDate))))

                                                             /*---Fix Rating ตามแบบบ้าน ขาย------------------------------------------------------------*/
                                                             join fsMin in DB.RateSettingFixSaleModels on pj.ID equals fsMin.ProjectID into gMin2
                                                             from fssMin in gMin2.DefaultIfEmpty()
                                                             where (fssMin == null
                                                                    || (fssMin.IsActive && fssMin.ActiveDate == (DB.RateSettingFixSaleModels.Where(n => n.ProjectID == pj.ID && n.IsActive).OrderBy(n => n.ActiveDate).Max(n => n.ActiveDate))))
                                                             join fsMax in DB.RateSettingFixSaleModels on pj.ID equals fsMax.ProjectID into gMax2
                                                             from fssMax in gMax2.DefaultIfEmpty()
                                                             where (fssMax == null
                                                                     || (fssMax.IsActive && fssMax.ActiveDate == (DB.RateSettingFixSaleModels.Where(n => n.ProjectID == pj.ID && n.IsActive).OrderByDescending(n => n.ActiveDate).Max(n => n.ActiveDate))))

                                                             /*---Fix Rating ตามแบบบ้าน โอน------------------------------------------------------------*/
                                                             join ftMin in DB.RateSettingFixTransferModels on pj.ID equals ftMin.ProjectID into gMin3
                                                             from ftsMin in gMin3.DefaultIfEmpty()
                                                             where (ftsMin == null
                                                                    || (ftsMin.IsActive && ftsMin.ActiveDate == (DB.RateSettingFixTransferModels.Where(n => n.ProjectID == pj.ID && n.IsActive).OrderBy(n => n.ActiveDate).Max(n => n.ActiveDate))))
                                                             join ftMax in DB.RateSettingFixTransferModels on pj.ID equals ftMax.ProjectID into gMax3
                                                             from ftsMax in gMax3.DefaultIfEmpty()
                                                             where (ftsMax == null
                                                                     || (ftsMax.IsActive && ftsMax.ActiveDate == (DB.RateSettingFixTransferModels.Where(n => n.ProjectID == pj.ID && n.IsActive).OrderByDescending(n => n.ActiveDate).Max(n => n.ActiveDate))))

                                                             /*---Fix % Rating ขาย-----------------------------------------------------------------*/
                                                             join fpsMin in DB.RateSettingFixSales on pj.ID equals fpsMin.ProjectID into gMin4
                                                             from fpssMin in gMin4.DefaultIfEmpty()
                                                             where (fpssMin == null
                                                                    || (fpssMin.IsActive && fpssMin.ActiveDate == (DB.RateSettingFixSales.Where(n => n.ProjectID == pj.ID && n.IsActive).OrderBy(n => n.ActiveDate).Max(n => n.ActiveDate))))
                                                             join fpsMax in DB.RateSettingFixSales on pj.ID equals fpsMax.ProjectID into gMax4
                                                             from fpssMax in gMax4.DefaultIfEmpty()
                                                             where (fpssMax == null
                                                                     || (fpssMax.IsActive && fpssMax.ActiveDate == (DB.RateSettingFixSales.Where(n => n.ProjectID == pj.ID && n.IsActive).OrderByDescending(n => n.ActiveDate).Max(n => n.ActiveDate))))

                                                             /*---Fix % Rating โอน-----------------------------------------------------------------*/
                                                             join fptMin in DB.RateSettingFixTransfers on pj.ID equals fptMin.ProjectID into gMin5
                                                             from fptsMin in gMin5.DefaultIfEmpty()
                                                             where (fptsMin == null
                                                                    || (fptsMin.IsActive && fptsMin.ActiveDate == (DB.RateSettingFixTransfers.Where(n => n.ProjectID == pj.ID && n.IsActive).OrderBy(n => n.ActiveDate).Max(n => n.ActiveDate))))
                                                             join fptMax in DB.RateSettingFixTransfers on pj.ID equals fptMax.ProjectID into gMax5
                                                             from fptsMax in gMax5.DefaultIfEmpty()
                                                             where (fptsMax == null
                                                                     || (fptsMax.IsActive && fptsMax.ActiveDate == (DB.RateSettingFixSaleModels.Where(n => n.ProjectID == pj.ID && n.IsActive).OrderByDescending(n => n.ActiveDate).Max(n => n.ActiveDate))))

                                                             /*---% Rating ขาย-----------------------------------------------------------------*/
                                                             join rsMin in DB.RateSettingSales on pj.ID equals rsMin.ProjectID into gMin6
                                                             from rssMin in gMin6.DefaultIfEmpty()
                                                             where (rssMin == null
                                                                    || (rssMin.IsActive && rssMin.ActiveDate == (DB.RateSettingSales.Where(n => n.ProjectID == pj.ID && n.IsActive).OrderBy(n => n.ActiveDate).Max(n => n.ActiveDate))))
                                                             join rsMax in DB.RateSettingSales on pj.ID equals rsMax.ProjectID into gMax6
                                                             from rssMax in gMax6.DefaultIfEmpty()
                                                             where (rssMax == null
                                                                     || (rssMax.IsActive && rssMax.ActiveDate == (DB.RateSettingSales.Where(n => n.ProjectID == pj.ID && n.IsActive).OrderByDescending(n => n.ActiveDate).Max(n => n.ActiveDate))))

                                                             /*---% Rating โอน-----------------------------------------------------------------*/
                                                             join rtMin in DB.RateSettingTransfers on pj.ID equals rtMin.ProjectID into gMin7
                                                             from rtsMin in gMin7.DefaultIfEmpty()
                                                             where (rtsMin == null
                                                                    || (rtsMin.IsActive && rtsMin.ActiveDate == (DB.RateSettingTransfers.Where(n => n.ProjectID == pj.ID && n.IsActive).OrderBy(n => n.ActiveDate).Max(n => n.ActiveDate))))
                                                             join rtMax in DB.RateSettingTransfers on pj.ID equals rtMax.ProjectID into gMax7
                                                             from rtsMax in gMax7.DefaultIfEmpty()
                                                             where (rtsMax == null
                                                                     || (rtsMax.IsActive && rtsMax.ActiveDate == (DB.RateSettingTransfers.Where(n => n.ProjectID == pj.ID && n.IsActive).OrderByDescending(n => n.ActiveDate).Max(n => n.ActiveDate))))

                                                             /*---Agent Rate-----------------------------------------------------------------*/
                                                             join gMin in DB.RateSettingAgents on pj.ID equals gMin.ProjectID into gMin8
                                                             from gsMin in gMin8.DefaultIfEmpty()
                                                             where (gsMin == null
                                                                    || (gsMin.IsActive && gsMin.ActiveDate == (DB.RateSettingAgents.Where(n => n.ProjectID == pj.ID && n.IsActive).OrderBy(n => n.ActiveDate).Max(n => n.ActiveDate))))
                                                             join gMax in DB.RateSettingAgents on pj.ID equals gMax.ProjectID into gMax8
                                                             from gsMax in gMax8.DefaultIfEmpty()
                                                             where (gsMax == null
                                                                     || (gsMax.IsActive && gsMax.ActiveDate == (DB.RateSettingAgents.Where(n => n.ProjectID == pj.ID && n.IsActive).OrderByDescending(n => n.ActiveDate).Max(n => n.ActiveDate))))

                                                             select new CommissionSettingQueryResult()
                                                             {
                                                                 Project = pj ?? new Project(),
                                                                 GeneralSettingMin = gssMin ?? new GeneralSetting(),
                                                                 GeneralSettingMax = gssMax ?? new GeneralSetting(),
                                                                 RateSettingFixSaleModelMin = fssMin ?? new RateSettingFixSaleModel(),
                                                                 RateSettingFixSaleModelMax = fssMax ?? new RateSettingFixSaleModel(),
                                                                 RateSettingFixTransferModelMin = ftsMin ?? new RateSettingFixTransferModel(),
                                                                 RateSettingFixTransferModelMax = ftsMax ?? new RateSettingFixTransferModel(),
                                                                 RateSettingFixSaleMin = fpssMin ?? new RateSettingFixSale(),
                                                                 RateSettingFixSaleMax = fpssMax ?? new RateSettingFixSale(),
                                                                 RateSettingFixTransferMin = fptsMin ?? new RateSettingFixTransfer(),
                                                                 RateSettingFixTransferMax = fptsMax ?? new RateSettingFixTransfer(),
                                                                 RateSettingSaleMin = rssMin ?? new RateSettingSale(),
                                                                 RateSettingSaleMax = rssMax ?? new RateSettingSale(),
                                                                 RateSettingTransferMin = rtsMin ?? new RateSettingTransfer(),
                                                                 RateSettingTransferMax = rtsMax ?? new RateSettingTransfer(),
                                                                 RateSettingAgentMin = gsMin ?? new RateSettingAgent(),
                                                                 RateSettingAgentMax = gsMax ?? new RateSettingAgent()
                                                             };

            query = query.Where(x => x.Project.IsActive == true);

            #region Filter
            if (filter.BGID != null)
            {
                query = query.Where(x => x.Project.BGID == filter.BGID);
            }
            if (filter.ProjectID != null)
            {
                query = query.Where(x => x.Project.ID == filter.ProjectID);
            }
            #endregion

            CommissionSettingDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<CommissionSettingQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => CommissionSettingDTO.CreateFromQueryResult(o)).ToList();

            return new CommissionSettingPaging()
            {
                PageOutput = pageOutput,
                CommissionSettings = results
            };
        }

        public async Task<List<UserListDTO>> GetSaleUserProjectAsync(Guid ProjectId, string SaleUserFullName)
        {
            var lcRoleID = await DB.Roles.Where(o => o.Code == "LC").Select(o => o.ID).FirstAsync();
            var lcUsers = from r in DB.Users
                .Where(o => o.UserAuthorizeProjects.Where(m => m.ProjectID == ProjectId).Any() &&
                         o.UserRoles.Where(n => n.RoleID == lcRoleID).Any())
                          select r;

            #region Filter
            if (!string.IsNullOrEmpty(SaleUserFullName))
            {
                lcUsers = lcUsers.Where(x => x.DisplayName.Contains(SaleUserFullName));
            }
            #endregion

            var results = await lcUsers.Select(o => UserListDTO.CreateFromModel(o)).OrderBy(o => o.FirstName).ToListAsync();

            return results;
        }

        public async Task<List<UserListDTO>> GetSaleUserAllAsync()
        {
            var lcRoleID = await DB.Roles.Where(o => o.Code == "LC").Select(o => o.ID).FirstAsync();
            var lcUsers = from r in DB.Users
                          .Where(o => o.UserRoles.Where(n => n.RoleID == lcRoleID).Any())
                          select r;


            var results = await lcUsers.Select(o => UserListDTO.CreateFromModel(o)).OrderBy(o => o.FirstName).ToListAsync();

            return results;
        }

        public async Task<List<ProjectDropdownDTO>> GetProjectDropdownListByBGAsync(Guid BgId)
        {
            IQueryable<Database.Models.PRJ.Project> query = DB.Projects
                 .Include(o => o.ProjectStatus)
                 .Include(o => o.ProductType)
                 .Include(o => o.BG)
                 .Where(o => o.IsActive == true);

            if (BgId != null && BgId != Guid.Empty)
            {
                query = query.Where(o => o.BGID == BgId);
            }

            var queryResults = await query.OrderBy(o => o.ProjectNo).ThenBy(o => o.ProjectNameTH).OrderBy(o => o.ProjectNo).Take(100).ToListAsync();

            var results = queryResults.Select(o => ProjectDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<List<ProjectDropdownDTO>> GetProjectDropdownListByProjectAsync(ListProjectInput ListProject)
        {
            IQueryable<Database.Models.PRJ.Project> query = DB.Projects
                 .Include(o => o.ProjectStatus)
                 .Include(o => o.ProductType)
                 .Include(o => o.BG)
                 .Where(o => o.IsActive == true);

            if (ListProject != null && ListProject.Projects.Count > 0)
            {
                var lstId = ListProject.Projects.Select(o => o.ProjectID).ToList();

                query = query.Where(o => lstId.Contains(o.ID));
                //query = query.Where(p => ListProject.Any(p2 => p2.ProjectID == p.ID));
            }

            var queryResults = await query.OrderBy(o => o.ProjectNo).ThenBy(o => o.ProjectNameTH).OrderBy(o => o.ProjectNo).Take(100).ToListAsync();

            var results = queryResults.Select(o => ProjectDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<List<ProjectDropdownDTO>> GetProjectDropdownListByProductTypeAsync(string ProductType)
        {
            IQueryable<Database.Models.PRJ.Project> query = DB.Projects
                 .Include(o => o.ProjectStatus)
                 .Include(o => o.ProductType)
                 .Include(o => o.BG)
                 .Where(o => o.IsActive == true);

            if (!string.IsNullOrEmpty(ProductType))
            {
                query = query.Where(o => o.ProductType.Key == ProductType);
            }

            var queryResults = await query.OrderBy(o => o.ProjectNo).ThenBy(o => o.ProjectNameTH).OrderBy(o => o.ProjectNo).Take(100).ToListAsync();

            var results = queryResults.Select(o => ProjectDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }
    }
}
