using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Project.Params.Filters;
using Project.Params.Inputs;
using Project.Params.Outputs;
using Project.Services;
using Report.Integration;

namespace Project.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : BaseController
    {
        private IProjectService ProjectService;
        private IProjectInfoService ProjectInfoService;
        private IAgreementService AgreementService;
        private IModelService ModelService;
        private ITowerService TowerService;
        private IFloorService FloorService;
        private IUnitService UnitService;
        private IHighRiseFeeService HighRiseFeeService;
        private ILowRiseFeeService LowRiseFeeService;
        private IUnitOtherUnitInfoTagService UnitOtherUnitInfoTagService;
        private IRoundFeeService RoundFeeService;
        private IProjectImageService ProjectImageService;
        private IConfiguration Configuration;
        private ILowRiseBuildingPriceFeeService LowRiseBuildingPriceFeeService;
        private ILowRiseFenceFeeService LowRiseFenceFeeService;
        private IProjectAddressService ProjectAddressService;
        private IWaiveQCService WaiveQCService;
        private IBudgetPromotionService BudgetPromotionService;
        private IWaterElectricMeterPriceService WaterElectricMeterPriceService;
        private IMinPriceService MinPriceService;
        private ITitleDeedService TitleDeedService;
        private IPriceListService PriceListService;
        private IWaiveCustomerSignService WaiveCustomerSignService;
        private readonly DatabaseContext DB;


        public ProjectsController(
            IProjectService projectService,
            IProjectInfoService projectInfoService,
            IAgreementService agreementService,
            IModelService modelService,
            ITowerService towerService,
            IFloorService floorService,
            IUnitService unitService,
            IHighRiseFeeService highRiseFeeService,
            ILowRiseFeeService lowRiseFeeService,
            IUnitOtherUnitInfoTagService unitOtherUnitInfoTagService,
            IRoundFeeService roundFeeService,
            IProjectImageService projectImageService,
            IConfiguration configuration,
            ILowRiseBuildingPriceFeeService lowRiseBuildingPriceFeeService,
            ILowRiseFenceFeeService lowRiseFenceFeeService,
            IProjectAddressService projectAddressService,
            IWaiveQCService waiveQCService,
            IBudgetPromotionService budgetPromotionService,
            IWaterElectricMeterPriceService waterElectricMeterPriceService,
            IMinPriceService minPriceService,
            ITitleDeedService titleDeedService,
            IPriceListService priceListService,
            IWaiveCustomerSignService waiveCustomerSignService,
            DatabaseContext db)
        {
            this.ProjectService = projectService;
            this.ProjectInfoService = projectInfoService;
            this.AgreementService = agreementService;
            this.ModelService = modelService;
            this.TowerService = towerService;
            this.FloorService = floorService;
            this.UnitService = unitService;
            this.HighRiseFeeService = highRiseFeeService;
            this.LowRiseFeeService = lowRiseFeeService;
            this.UnitOtherUnitInfoTagService = unitOtherUnitInfoTagService;
            this.RoundFeeService = roundFeeService;
            this.ProjectImageService = projectImageService;
            this.Configuration = configuration;
            this.LowRiseBuildingPriceFeeService = lowRiseBuildingPriceFeeService;
            this.LowRiseFenceFeeService = lowRiseFenceFeeService;
            this.ProjectAddressService = projectAddressService;
            this.WaiveQCService = waiveQCService;
            this.BudgetPromotionService = budgetPromotionService;
            this.WaterElectricMeterPriceService = waterElectricMeterPriceService;
            this.MinPriceService = minPriceService;
            this.TitleDeedService = titleDeedService;
            this.PriceListService = priceListService;
            this.WaiveCustomerSignService = waiveCustomerSignService;
            this.DB = db;

        }

        #region ValidateTab (Data Status)
        /// <summary>
        /// ดึงข้อมูลสถานะของแต่ละ Tab ในหน้าข้อมูลโครงการ
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/DataStatus")]
        [ProducesResponseType(200, Type = typeof(ProjectDataStatusDTO))]
        public async Task<IActionResult> GetProjectDataStatus([FromRoute] Guid projectID)
        {
            try
            {
                var result = await ProjectService.GetProjectDataStatusAsync(projectID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Project
        /// <summary>
        /// ลิสข้อมูลโครงการ
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<ProjectDTO>))]
        public async Task<IActionResult> GetProjectList([FromQuery]ProjectsFilter filter, [FromQuery]PageParam pageParam, [FromQuery]ProjectSortByParam sortByParam)
        {
            try
            {
                var result = await ProjectService.GetProjectListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.Projects);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ProjectDTO))]
        public async Task<IActionResult> GetProject([FromRoute]Guid id)
        {
            try
            {
                var result = await ProjectService.GetProjectAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ลิสข้อมูลโครงการ Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <param name="companyID"></param>
        /// <param name="isActive"></param>
        /// <param name="projectStatusKey"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<ProjectDropdownDTO>))]
        public async Task<IActionResult> GetProjectDropdown([FromQuery]string name, [FromQuery]Guid? companyID, [FromQuery]bool isActive = true, [FromQuery]string projectStatusKey = null)
        {
            try
            {
                var result = await ProjectService.GetProjectDropdownListAsync(name, companyID, isActive, projectStatusKey);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ดึงข้อมูลจำนวนโครงการในสถานะต่างๆ
        /// </summary>
        /// <returns></returns>
        [HttpGet("Count")]
        [ProducesResponseType(200, Type = typeof(ProjectCountDTO))]
        public async Task<IActionResult> GetProjectCount()
        {
            try
            {
                var result = await ProjectService.GetProjectCountAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างข้อมูลโครงการ
        /// </summary>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ProjectDTO))]
        public async Task<IActionResult> CreateProject([FromBody]ProjectDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ProjectService.CreateProjectAsync(input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบโครงการ
        /// </summary>
        [HttpDelete("{projectID}")]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> DeleteProject([FromRoute] Guid projectID, [FromQuery]string reason)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ProjectService.DeleteProjectAsync(projectID, reason);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ดึง url Template ใบจอง
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/ExportBookingTemplateUrl")]
        [ProducesResponseType(200, Type = typeof(StringResult))]
        public async Task<IActionResult> GetExportBookingTemplateUrl([FromRoute] Guid projectID)
        {
            var result = await ProjectService.GetExportBookingTemplateUrlAsync(projectID);

            return Ok(result);
        }

        /// <summary>
        /// ดึง url Template สัญญา
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/ExportAgreementTemplateUrl")]
        [ProducesResponseType(200, Type = typeof(StringResult))]
        public async Task<IActionResult> GetExportAgreementTemplateUrl([FromRoute] Guid projectID)
        {
            var result = await ProjectService.GetExportAgreementTemplateUrlAsync(projectID);

            return Ok(result);
        }

        /// <summary>
        /// ดึง url รายงานตารางโครงการ
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="downloadAs"></param>
        /// <returns></returns>
        [HttpGet("ExportProjectListUrl")]
        [ProducesResponseType(200, Type = typeof(StringResult))]
        public async Task<IActionResult> GetExportProjectListUrl([FromQuery]ProjectsFilter filter, [FromQuery]ShowAs downloadAs)
        {
            var result = await ProjectService.GetExportProjectListUrlAsync(filter, downloadAs);

            return Ok(result);
        }

        /// <summary>
        /// อัพเดทสถานะโครงการ projectStatus
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectStatus"></param>
        /// <returns></returns>
        [HttpPut("{id}/ProjectStatus")]
        public async Task<IActionResult> UpdateProjectStatus([FromRoute]Guid id, [FromBody]MasterCenterDropdownDTO projectStatus)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await ProjectService.UpdateProjectStatus(id, projectStatus);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        #endregion

        #region ProjectInfo
        /// <summary>
        /// ข้อมูลโครงการ
        /// </summary>
        /// <returns>The project info.</returns>
        /// <param name="projectID">Project identifier.</param>
        [HttpGet("{projectID}/Info")]
        [ProducesResponseType(200, Type = typeof(ProjectInfoDTO))]
        public async Task<IActionResult> GetProjectInfo([FromRoute] Guid projectID)
        {
            try
            {
                var result = await ProjectInfoService.GetProjectInfoAsync(projectID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// แก้ไขข้อมูลโครงการ
        /// </summary>
        [HttpPut("{projectID}/Info")]
        [ProducesResponseType(200, Type = typeof(ProjectInfoDTO))]
        public async Task<IActionResult> EditProjectInfo([FromRoute] Guid projectID, [FromBody]ProjectInfoDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ProjectInfoService.UpdateProjectInfoAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }


        #endregion

        #region ProjectAddress
        /// <summary>
        /// ลิสข้อมูลที่ตั้ง Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Addresses/DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<ProjectAddressListDTO>))]
        public async Task<IActionResult> GetProjectAddressDropdown([FromRoute] Guid projectID, [FromQuery]string name, [FromQuery]string projectAddressTypeKey)
        {
            try
            {
                var result = await ProjectAddressService.GetProjectAddressDropdownListAsync(projectID, name, projectAddressTypeKey);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิส ข้อมูลที่ตั้งโครงการ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Addresses")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<ProjectAddressListDTO>))]
        public async Task<IActionResult> GetProjectAddressList([FromRoute] Guid projectID, [FromQuery]PageParam pageParam, [FromQuery]SortByParam sortByParam)
        {
            try
            {
                var result = await ProjectAddressService.GetProjectAddressListAsync(projectID, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);

                return Ok(result.ProjectAddresses);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลที่ตั้งโครงการ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Addresses/{id}")]
        [ProducesResponseType(200, Type = typeof(ProjectAddressDTO))]
        public async Task<IActionResult> GetProjectAddress([FromRoute]Guid projectID, [FromRoute] Guid id)
        {
            try
            {
                var result = await ProjectAddressService.GetProjectAddressAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้าง ข้อมูลที่ตั้งโครงการ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/Addresses")]
        [ProducesResponseType(200, Type = typeof(ProjectAddressDTO))]
        public async Task<IActionResult> CreateProjectAddress([FromRoute]Guid projectID, [FromBody]  ProjectAddressDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ProjectAddressService.CreateProjectAddressAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไข ข้อมูลที่ตั้งโครงการ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/Addresses/{id}")]
        [ProducesResponseType(200, Type = typeof(ProjectAddressDTO))]
        public async Task<IActionResult> EditProjectAddress([FromRoute] Guid projectID, [FromRoute] Guid id, [FromBody]ProjectAddressDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ProjectAddressService.UpdateProjectAddressAsync(projectID, id, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบ ข้อมูลที่ตั้งโครงการ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/Addresses/{id}")]
        public async Task<IActionResult> DeleteProjectAddress([FromRoute] Guid projectID, [FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await ProjectAddressService.DeleteProjectAddressAsync(id);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        #endregion

        #region ProjectAgreement
        /// <summary>
        /// เอกสารสัญญาในโครงการ
        /// </summary>
        [HttpGet("{projectID}/Agreement")]
        [ProducesResponseType(200, Type = typeof(AgreementDTO))]
        public async Task<IActionResult> GetProjectAgreement([FromRoute]Guid projectID)
        {
            try
            {
                var result = await AgreementService.GetAgreementAsync(projectID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// แก้ไขเอกสารสัญญาในโครงการ
        /// </summary>
        [HttpPut("{projectID}/Agreement/{id}")]
        [ProducesResponseType(200, Type = typeof(AgreementDTO))]
        public async Task<IActionResult> EditProjectAgreement([FromRoute] Guid projectID, [FromRoute] Guid id, [FromBody]AgreementDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await AgreementService.UpdateAgreementAsync(projectID, id, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        #endregion

        #region ProjectModel
        /// <summary>
        /// ลิสข้อมูลแบบบ้าน dropdown
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Models/DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<ModelDropdownDTO>))]
        public async Task<IActionResult> GetModelDropdown([FromRoute]Guid projectID, [FromQuery]string name)
        {
            try
            {
                var result = await ModelService.GetModelDropdownListAsync(projectID, name);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิสข้อมูลแบบบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Models")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<ModelListDTO>))]
        public async Task<IActionResult> GetProjectModelList([FromRoute]Guid projectID, [FromQuery]ModelsFilter filter, [FromQuery]PageParam pageParam, [FromQuery]ModelListSortByParam sortByParam)
        {
            try
            {
                var result = await ModelService.GetModelListAsync(projectID, filter, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);

                return Ok(result.Models);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลแบบบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Models/{id}")]
        [ProducesResponseType(200, Type = typeof(ModelDTO))]
        public async Task<IActionResult> GetProjectModel([FromRoute]Guid projectID, [FromRoute] Guid id)
        {
            try
            {
                var result = await ModelService.GetModelAsync(projectID, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างข้อมูลแบบบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/Models")]
        [ProducesResponseType(200, Type = typeof(ModelDTO))]
        public async Task<IActionResult> CreateProjectModel([FromRoute]Guid projectID, [FromBody]  ModelDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ModelService.CreateModelAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไขข้อมูลแบบบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/Models/{id}")]
        [ProducesResponseType(200, Type = typeof(ModelDTO))]
        public async Task<IActionResult> EditProjectModel([FromRoute] Guid projectID, [FromRoute] Guid id, [FromBody]ModelDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ModelService.UpdateModelAsync(projectID, id, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบข้อมูลแบบบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/Models/{id}")]
        public async Task<IActionResult> DeleteProjectModel([FromRoute] Guid projectID, [FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ModelService.DeleteModelAsync(projectID, id);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        #endregion

        #region ProjectTower
        /// <summary>
        /// ลิสข้อมูลตึก Dropdown
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Towers/DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<TowerDTO>))]
        public async Task<IActionResult> GetTowerDropdown([FromRoute]Guid projectID, [FromQuery]string code)
        {
            try
            {
                var result = await TowerService.GetTowerDropdownListAsync(projectID, code);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิสข้อมูลตึก
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Towers")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<TowerDTO>))]
        public async Task<IActionResult> GetProjectTowerList([FromRoute]Guid projectID, [FromQuery]TowerFilter filter, [FromQuery]PageParam pageParam, [FromQuery]TowerSortByParam sortByParam)
        {
            try
            {
                var result = await TowerService.GetTowerListAsync(projectID, filter, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);

                return Ok(result.Towers);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลตึก
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Towers/{id}")]
        [ProducesResponseType(200, Type = typeof(TowerDTO))]
        public async Task<IActionResult> GetProjectTower([FromRoute]Guid projectID, [FromRoute]Guid id)
        {
            try
            {
                var result = await TowerService.GetTowerAsync(projectID, id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างข้อมูลตึก
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/Towers")]
        [ProducesResponseType(200, Type = typeof(TowerDTO))]
        public async Task<IActionResult> CreateProjectTower([FromRoute] Guid projectID, [FromBody]TowerDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await TowerService.CreateTowerAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไขข้อมูลตึก
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/Towers/{id}")]
        [ProducesResponseType(200, Type = typeof(TowerDTO))]
        public async Task<IActionResult> EditProjectTower([FromRoute] Guid projectID, [FromRoute] Guid id, [FromBody]TowerDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await TowerService.UpdateTowerAsync(projectID, id, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบข้อมูลตึก
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/Towers/{id}")]
        public async Task<IActionResult> DeleteProjectTower([FromRoute] Guid projectID, [FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await TowerService.DeleteTowerAsync(projectID, id);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        #endregion

        #region ProjectFloor
        /// <summary>
        /// ลิสของชั้น Dropdown
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="towerID"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Towers/Floors/DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<FloorDropdownDTO>))]
        public async Task<IActionResult> GetFloorDropdown([FromRoute]Guid projectID, [FromQuery]Guid? towerID = null, [FromQuery]string name = null)
        {
            try
            {
                var result = await FloorService.GetFloorDropdownListAsync(projectID, towerID, name);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิสของชั้น
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="towerID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Towers/{towerID}/Floors")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<FloorDTO>))]
        public async Task<IActionResult> GetFloorList([FromRoute]Guid projectID, [FromRoute]Guid towerID, [FromQuery]FloorsFilter filter, [FromQuery]PageParam pageParam, [FromQuery]FloorSortByParam sortByParam)
        {
            try
            {
                var result = await FloorService.GetFloorListAsync(projectID, towerID, filter, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);

                return Ok(result.Floors);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลชั้น
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="towerID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Towers/{towerID}/Floors/{id}")]
        [ProducesResponseType(200, Type = typeof(FloorDTO))]
        public async Task<IActionResult> GetFloor([FromRoute]Guid projectID, [FromRoute] Guid towerID, [FromRoute] Guid id)
        {
            try
            {
                var result = await FloorService.GetFloorAsync(projectID, towerID, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// เพิ่มชั้น
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="towerID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/Towers/{towerID}/Floors")]
        [ProducesResponseType(200, Type = typeof(FloorDTO))]
        public async Task<IActionResult> CreateFloor([FromRoute]Guid projectID, [FromRoute]Guid towerID, [FromBody]  FloorDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await FloorService.CreateFloorAsync(projectID, towerID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// เพิ่มชั้นทีละหลายชั้น
        /// </summary>
        /// <returns>The multiple floors.</returns>
        /// <param name="projectID">Project identifier.</param>
        /// <param name="towerID">Tower identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPost("{projectID}/Towers/{towerID}/Floors/Multiple")]
        [ProducesResponseType(200, Type = typeof(List<FloorDTO>))]
        public async Task<IActionResult> CreateMultipleFloors([FromRoute]Guid projectID, [FromRoute]Guid towerID, [FromBody]  CreateMultipleFloorInput input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await FloorService.CreateMultipleFloorAsync(projectID, towerID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไขชั้น
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="towerID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/Towers/{towerID}/Floors/{id}")]
        [ProducesResponseType(200, Type = typeof(FloorDTO))]
        public async Task<IActionResult> EditFloor([FromRoute]Guid projectID, [FromRoute]Guid towerID, [FromRoute]Guid id, [FromBody]  FloorDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await FloorService.UpdateFloorAsync(projectID, towerID, id, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบชั้น
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="towerID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/Towers/{towerID}/Floors/{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> DeleteFloor([FromRoute]Guid projectID, [FromRoute]Guid towerID, [FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await FloorService.DeleteFloorAsync(projectID, towerID, id);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        #endregion

        #region ProjectUnit
        /// <summary>
        /// ลิส ข้อมูลแปลง Dropdown
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Units/DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<UnitDropdownDTO>))]
        public async Task<IActionResult> GetProjectUnitDropdownList([FromRoute]Guid projectID, [FromQuery]Guid? towerID = null, [FromQuery]Guid? floorID = null, [FromQuery]string name = null, [FromQuery]string unitStatusKey = null)
        {
            try
            {
                var result = await UnitService.GetUnitDropdownListAsync(projectID, towerID, floorID, name, unitStatusKey);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิส ข้อมูลแปลง Dropdown
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Units/DropdownListSellPrice")]
        [ProducesResponseType(200, Type = typeof(List<UnitDropdownSellPriceDTO>))]
        public async Task<IActionResult> GetProjectUnitDropdownWithSellPriceList([FromRoute]Guid projectID, [FromQuery]string name, [FromQuery]string unitStatusKey = null)
        {
            try
            {
                var result = await UnitService.GetUnitDropdownWithSellPriceListAsync(projectID, name, unitStatusKey);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิส ข้อมูลแปลง
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="unitFilter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Units")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<UnitDTO>))]
        public async Task<IActionResult> GetProjectUnitList([FromRoute]Guid projectID, [FromQuery]UnitFilter unitFilter, [FromQuery]PageParam pageParam, [FromQuery]UnitListSortByParam sortByParam)
        {
            try
            {
                var result = await UnitService.GetUnitListAsync(projectID, unitFilter, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);

                return Ok(result.Units);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลรายละเอียดแปลง
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Units/{id}")]
        [ProducesResponseType(200, Type = typeof(UnitDTO))]
        public async Task<IActionResult> GetProjectUnit([FromRoute]Guid projectID, [FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await UnitService.GetUnitAsync(projectID, id);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ข้อมูลแปลงทั่วไป
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Units/{id}/Info")]
        [ProducesResponseType(200, Type = typeof(UnitInfoDTO))]
        public async Task<IActionResult> GetProjectUnitInfo([FromRoute]Guid projectID, [FromRoute] Guid id)
        {
            try
            {
                var result = await UnitService.GetUnitInfoAsync(projectID, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้าง ข้อมูลแปลง
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/Units")]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> CreateProjectUnit([FromRoute]Guid projectID, [FromBody] UnitDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await UnitService.CreateUnitAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไข ข้อมูลแปลง
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/Units/{id}")]
        [ProducesResponseType(200, Type = typeof(UnitDTO))]
        public async Task<IActionResult> EditProjectUnit([FromRoute]Guid projectID, [FromRoute]Guid id, [FromBody] UnitDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await UnitService.UpdateUnitAsync(projectID, id, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบ ข้อมูลแปลง
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/Units/{id}")]
        public async Task<IActionResult> DeleteProjectUnit([FromRoute]Guid projectID, [FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await UnitService.DeleteUnitAsync(projectID, id);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Import Unit Excel (ตั้งต้น)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/Units/InitialImport")]
        [ProducesResponseType(200, Type = typeof(UnitInitialExcelDTO))]
        public async Task<IActionResult> ImportProjectUnitInitial([FromRoute]Guid projectID, [FromBody]FileDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await UnitService.ImportUnitInitialAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Import Unit Excel (ทั่วไป)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/Units/GeneralImport")]
        [ProducesResponseType(200, Type = typeof(UnitGeneralExcelDTO))]
        public async Task<IActionResult> ImportProjectUnitGeneral([FromRoute]Guid projectID, [FromBody]FileDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await UnitService.ImportUnitGeneralAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Import Excel (พิ้นที่รั้ว)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/Units/FenceAreaImport")]
        [ProducesResponseType(200, Type = typeof(UnitFenceAreaExcelDTO))]
        public async Task<IActionResult> ImportProjectUnitFenceArea([FromRoute]Guid projectID, [FromBody]FileDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await UnitService.ImportUnitFenceAreaAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Export excel (ตั้งต้น)
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Units/InitialExport")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportProjectUnitInitial([FromRoute]Guid projectID)
        {
            try
            {
                var result = await UnitService.ExportExcelUnitInitialAsync(projectID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Export excel (ทั่วไป)
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Units/GeneralExport")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportProjectUnitGeneral([FromRoute]Guid projectID)
        {
            try
            {
                var result = await UnitService.ExportExcelUnitGeneralAsync(projectID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Export excel (พื้นที่รั้ว)
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Units/FenceAreaExport")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportProjectUnitFenceArea([FromRoute]Guid projectID)
        {
            try
            {
                var result = await UnitService.ExportExcelUnitFenceAreaAsync(projectID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ProjectTitleDeed
        /// <summary>
        /// ลิส ข้อมูลโฉนด
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="request"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/TitleDeeds")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<TitleDeedListDTO>))]
        public async Task<IActionResult> GetProjectTitleDeedList([FromRoute]Guid projectID, [FromQuery]TitleDeedFilter request, [FromQuery]PageParam pageParam, [FromQuery]TitleDeedListSortByParam sortByParam)
        {
            try
            {
                var result = await TitleDeedService.GetTitleDeedListAsync(projectID, request, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);

                return Ok(result.TitleDeeds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลโฉนด
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/TitleDeeds/{id}")]
        [ProducesResponseType(200, Type = typeof(TitleDeedDTO))]
        public async Task<IActionResult> GetProjectTitleDeed([FromRoute]Guid projectID, [FromRoute] Guid id)
        {
            try
            {
                var result = await TitleDeedService.GetTitleDeedAsync(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้าง ข้อมูลโฉนด
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/TitleDeeds")]
        [ProducesResponseType(200, Type = typeof(TitleDeedDTO))]
        public async Task<IActionResult> CreateProjectTitleDeed([FromRoute]Guid projectID, [FromBody] TitleDeedDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await TitleDeedService.CreateTitleDeedAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไข ข้อมูลโฉนด
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/TitleDeeds/{id}")]
        [ProducesResponseType(200, Type = typeof(TitleDeedDTO))]
        public async Task<IActionResult> EditProjectTitleDeed([FromRoute]Guid projectID, [FromRoute]Guid id, [FromBody] TitleDeedDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await TitleDeedService.UpdateTitleDeedAsync(projectID, id, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบ ข้อมูลโฉนด
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/TitleDeeds/{id}")]
        public async Task<IActionResult> DeleteProjectTitleDeed([FromRoute]Guid projectID, [FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await TitleDeedService.DeleteTitleDeedAsync(projectID, id);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Import Excel ข้อมูลโฉนด
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/TitleDeeds/Import")]
        [ProducesResponseType(200, Type = typeof(TitledeedExcelDTO))]
        public async Task<IActionResult> ImportProjectTitleDeed([FromRoute]Guid projectID, [FromBody]FileDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await TitleDeedService.ImportTitleDeedAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Export Excel ข้อมูลโฉนด
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/TitleDeeds/Export")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportProjectTitleDeed([FromRoute]Guid projectID)
        {
            try
            {
                var result = await TitleDeedService.ExportExcelTitleDeedAsync(projectID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// กำหนดบ้านเลขที่
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/TitleDeeds/UpdateMultipleHouseNos")]
        public async Task<IActionResult> UpdateMultipleHouseNos([FromRoute]Guid projectID, [FromBody]UpdateMultipleHouseNoParam input)
        {
            try
            {
                await TitleDeedService.UpdateMultipleHouseNosAsync(projectID, input);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// กำหนดสำนักงานที่ดิน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/TitleDeeds/UpdateMultipleLandOffices")]
        public async Task<IActionResult> UpdateMultipleLandOffices([FromRoute]Guid projectID, [FromBody]UpdateMultipleLandOfficeParam input)
        {
            try
            {
                await TitleDeedService.UpdateMultipleLandOfficesAsync(projectID, input);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ProjectImage
        [HttpGet("{projectID}/Logo")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> GetProjectLogo([FromRoute]Guid projectID)
        {
            var result = await ProjectImageService.GetProjectLogoAsync(projectID);

            return Ok(result);
        }

        [HttpPut("{projectID}/Logo")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> UpdateProjectLogo([FromRoute]Guid projectID, FileDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ProjectImageService.UpdateProjectLogoAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        [HttpGet("{projectID}/FloorPlanImages")]
        [ProducesResponseType(200, Type = typeof(List<FloorPlanImageDTO>))]
        public async Task<IActionResult> GetFloorPlanImages([FromRoute]Guid projectID, [FromQuery]string name = null)
        {
            var results = await ProjectImageService.GetFloorPlanImagesAsync(projectID, name);
            return Ok(results);
        }

        [HttpGet("{projectID}/RoomPlanImages")]
        [ProducesResponseType(200, Type = typeof(List<RoomPlanImageDTO>))]
        public async Task<IActionResult> GetRoomPlanImage([FromRoute]Guid projectID, [FromQuery]string name = null)
        {
            var results = await ProjectImageService.GetRoomPlanImagesAsync(projectID, name);
            return Ok(results);
        }

        [HttpPost("{projectID}/FloorPlanImages")]
        [ProducesResponseType(200, Type = typeof(List<FloorPlanImageDTO>))]
        public async Task<IActionResult> SaveFloorPlanImages([FromRoute]Guid projectID, List<FloorPlanImageDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ProjectImageService.SaveFloorPlanImagesAsync(projectID, inputs);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        [HttpPost("{projectID}/RoomPlanImages")]
        [ProducesResponseType(200, Type = typeof(List<RoomPlanImageDTO>))]
        public async Task<IActionResult> SaveRoomPlanImage([FromRoute]Guid projectID, List<RoomPlanImageDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ProjectImageService.SaveRoomPlanImagesAsync(projectID, inputs);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        #endregion

        #region ProjectMinPrice
        /// <summary>
        /// ลิส ข้อมูล Minprice
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/MinPrices")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<MinPriceDTO>))]
        public async Task<IActionResult> GetProjectMinPriceList([FromRoute]Guid projectID, [FromQuery]MinPriceFilter filter, [FromQuery]PageParam pageParam, [FromQuery]MinPriceSortByParam sortByParam)
        {
            try
            {
                var result = await MinPriceService.GetMinPriceListAsync(projectID, filter, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);

                return Ok(result.MinPrices);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูล MinPrice
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/MinPrices/{id}")]
        [ProducesResponseType(200, Type = typeof(MinPriceDTO))]
        public async Task<IActionResult> GetProjectMinPrice([FromRoute]Guid projectID, [FromRoute] Guid id)
        {
            try
            {
                var result = await MinPriceService.GetMinPriceAsync(projectID, id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้าง ข้อมูล Minprice
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/MinPrices")]
        [ProducesResponseType(200, Type = typeof(MinPriceDTO))]
        public async Task<IActionResult> CreateProjectMinPrice([FromRoute]Guid projectID, [FromBody] MinPriceDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await MinPriceService.CreateMinPriceAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไขข้อมูล Minprice
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/MinPrices/{id}")]
        [ProducesResponseType(200, Type = typeof(MinPriceDTO))]
        public async Task<IActionResult> EditProjectMinPrice([FromRoute]Guid projectID, [FromRoute]Guid id, [FromBody] MinPriceDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await MinPriceService.UpdateMinPriceAsync(projectID, id, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบข้อมูล Minprice
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/MinPrices/{id}")]
        public async Task<IActionResult> DeleteProjectMinPrice([FromRoute]Guid projectID, [FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await MinPriceService.DeleteMinPriceAsync(projectID, id);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Import MinPrice
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/MinPrices/Import")]
        [ProducesResponseType(200, Type = typeof(MinPriceExcelDTO))]
        public async Task<IActionResult> ImportProjectMinPrice([FromRoute]Guid projectID, [FromBody]FileDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await MinPriceService.ImportMinPriceAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Export MinPrice
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/MinPrices/Export")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportProjectMinPrice([FromRoute]Guid projectID, [FromQuery]MinPriceFilter filter, [FromQuery]MinPriceSortByParam sortByParam)
        {
            try
            {
                var result = await MinPriceService.ExportExcelMinPriceAsync(projectID, filter, sortByParam);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ProjectPriceList
        /// <summary>
        /// ดึงข้อมูล PriceList
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/PriceLists")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<PriceListDTO>))]
        public async Task<IActionResult> GetProjectPriceListLists([FromRoute]Guid projectID, [FromQuery]PriceListFilter filter, [FromQuery]PageParam pageParam, [FromQuery]PriceListSortByParam sortByParam)
        {
            try
            {
                var result = await PriceListService.GetPriceListsAsync(projectID, filter, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);

                return Ok(result.PriceLists);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{projectID}/PriceLists/{id}")]
        [ProducesResponseType(200, Type = typeof(PriceListDTO))]
        public async Task<IActionResult> GetProjectPriceList([FromRoute]Guid projectID, [FromRoute] Guid id)
        {
            try
            {
                var result = await PriceListService.GetPriceListAsync(projectID, id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างข้อมูล PriceList
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/PriceLists")]
        [ProducesResponseType(200, Type = typeof(PriceListDTO))]
        public async Task<IActionResult> CreateProjectPriceList([FromRoute]Guid projectID, [FromBody] PriceListDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await PriceListService.CreatePriceListAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        [HttpPut("{projectID}/PriceLists/{id}")]
        [ProducesResponseType(200, Type = typeof(PriceListDTO))]
        public async Task<IActionResult> EditProjectPriceList([FromRoute]Guid projectID, [FromRoute]Guid id, [FromBody] PriceListDTO input)
        {
            try
            {
                var result = await PriceListService.UpdatePriceListAsync(projectID, id, input);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลบข้อมูล PriceList
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/PriceLists/{id}")]
        public async Task<IActionResult> DeleteProjectPriceList([FromRoute]Guid projectID, [FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await PriceListService.DeletePriceListAsync(id);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Import Excel PriceList
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/PriceLists/Import")]
        [ProducesResponseType(200, Type = typeof(PriceListExcelDTO))]
        public async Task<IActionResult> ImportProjectPriceList([FromRoute]Guid projectID, [FromBody]FileDTO file)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await PriceListService.ImportProjectPriceListAsync(projectID, file);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Export PriceList Excel
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/PriceLists/Export")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportProjectPriceList([FromRoute]Guid projectID)
        {
            try
            {
                var result = await PriceListService.ExportExcelPriceListAsync(projectID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ProjectFee

        #region HighRiseFee
        /// <summary>
        /// ลิสข้อมูลค่าทำเนียมโอน-ราคาประเมินที่ดิน (แนวสูง)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/HighRiseFees")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<HighRiseFeeDTO>))]
        public async Task<IActionResult> GetProjectHighRiseFeeList([FromRoute]Guid projectID, [FromQuery]HighRiseFeeFilter filter, [FromQuery]PageParam pageParam, [FromQuery]HighRiseFeeSortByParam sortByParam)
        {
            try
            {
                var result = await HighRiseFeeService.GetHighRiseFeeListAsync(projectID, filter, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);

                return Ok(result.HighRiseFees);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลค่าทำเนียมโอน-ราคาประเมินที่ดินแนวสูง
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/HighRiseFees/{id}")]
        [ProducesResponseType(200, Type = typeof(HighRiseFeeDTO))]
        public async Task<IActionResult> GetProjectHighRiseFee([FromRoute]Guid projectID, [FromRoute] Guid id)
        {
            try
            {
                var result = await HighRiseFeeService.GetHighRiseFeeAsync(projectID, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างข้อมูลค่าทำเนียมโอน-ราคาประเมินที่ดินแนวสูง
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/HighRiseFees")]
        [ProducesResponseType(200, Type = typeof(HighRiseFeeDTO))]
        public async Task<IActionResult> CreateProjectHighRiseFee([FromRoute]Guid projectID, [FromBody] HighRiseFeeDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await HighRiseFeeService.CreateHighRiseFeeAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไขข้อมูลค่าทำเนียมโอน-ราคาประเมินที่ดินแนวสูง
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/HighRiseFees/{id}")]
        [ProducesResponseType(200, Type = typeof(HighRiseFeeDTO))]
        public async Task<IActionResult> EditProjectHighRiseFee([FromRoute]Guid projectID, [FromRoute]Guid id, [FromBody] HighRiseFeeDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await HighRiseFeeService.UpdateHighRiseFeeAsync(projectID, id, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบข้อมูลค่าทำเนียมโอน-ราคาประเมินที่ดินแนวสูง
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/HighRiseFees/{id}")]
        public async Task<IActionResult> DeleteProjectHighRiseFee([FromRoute]Guid projectID, [FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await HighRiseFeeService.DeleteHighRiseFeeAsync(projectID, id);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Import ราคาประเมิณที่ดิน (แนวสูง)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/HighRiseFees/Import")]
        [ProducesResponseType(200, Type = typeof(HighRiseFeeExcelDTO))]
        public async Task<IActionResult> ImportProjectHighRiseFee([FromRoute]Guid projectID, [FromBody]FileDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await HighRiseFeeService.ImportHighRiseFeeAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Export ราคาประเมินที่ดิน (แนวสูง)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/HighRiseFees/Export")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportProjectHighRiseFee([FromRoute]Guid projectID, [FromQuery]HighRiseFeeFilter filter, [FromQuery]HighRiseFeeSortByParam sortByParam)
        {
            try
            {
                var result = await HighRiseFeeService.ExportHighRiseFeeAsync(projectID, filter, sortByParam);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region LowRiseFee
        /// <summary>
        /// ลิสข้อมูล ค่าธรรมเนียม ราคาประเมินที่ดิน (แนวราบ)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/LowRiseFees")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<LowRiseFeeDTO>))]
        public async Task<IActionResult> GetProjectLowRiseFeeList([FromRoute]Guid projectID, [FromQuery]LowRiseFeeFilter filter, [FromQuery]PageParam pageParam, [FromQuery]LowRiseFeeSortByParam sortByParam)
        {
            try
            {
                var result = await LowRiseFeeService.GetLowRiseFeeListAsync(projectID, filter, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);

                return Ok(result.LowRiseFees);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูล ราคาประเมินที่ดินแนวราบ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/LowRiseFees/{id}")]
        [ProducesResponseType(200, Type = typeof(LowRiseFeeDTO))]
        public async Task<IActionResult> GetProjectLowRiseFee([FromRoute]Guid projectID, [FromRoute] Guid id)
        {
            try
            {
                var result = await LowRiseFeeService.GetLowRiseFeeAsync(projectID, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างข้อมูล ราคาประเมินที่ดินแนวราบ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/LowRiseFees")]
        [ProducesResponseType(200, Type = typeof(LowRiseFeeDTO))]
        public async Task<IActionResult> CreateProjectLowRiseFee([FromRoute]Guid projectID, [FromBody] LowRiseFeeDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await LowRiseFeeService.CreateLowRiseFeeAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไข ข้อมูล ราคาประเมินที่ดินแนวราบ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/LowRiseFees/{id}")]
        [ProducesResponseType(200, Type = typeof(LowRiseFeeDTO))]
        public async Task<IActionResult> EditProjectLowRiseFee([FromRoute]Guid projectID, [FromRoute]Guid id, [FromBody] LowRiseFeeDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await LowRiseFeeService.UpdateLowRiseFeeAsync(projectID, id, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบ ข้อมูล ราคาประเมินที่ดินแนวราบ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/LowRiseFees/{id}")]
        public async Task<IActionResult> DeleteProjectLowRiseFee([FromRoute]Guid projectID, [FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await LowRiseFeeService.DeleteLowRiseFeeAsync(projectID, id);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        #endregion

        #region RoundFee
        /// <summary>
        /// ลิสข้อมูล ค่าทำเนียมโอน-สูตรปัดเศษ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/RoundFees")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<RoundFeeDTO>))]
        public async Task<IActionResult> GetProjectRoundFeeList([FromRoute]Guid projectID, [FromQuery]RoundFeeFilter filter, [FromQuery]PageParam pageParam, [FromQuery]RoundFeeSortByParam sortByParam)
        {
            try
            {
                var result = await RoundFeeService.GetRoundFeeListAsync(projectID, filter, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);

                return Ok(result.RoundFees);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูล ค่าทำเนียมโอน-สูตรปัดเศษ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/RoundFees/{id}")]
        [ProducesResponseType(200, Type = typeof(RoundFeeDTO))]
        public async Task<IActionResult> GetProjectRoundFee([FromRoute]Guid projectID, [FromRoute] Guid id)
        {
            try
            {
                var result = await RoundFeeService.GetRoundFeeAsync(projectID, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้าง ข้อมูล ค่าทำเนียมโอน-สูตรปัดเศษ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/RoundFees")]
        [ProducesResponseType(200, Type = typeof(RoundFeeDTO))]
        public async Task<IActionResult> CreateProjectRoundFee([FromRoute]Guid projectID, [FromBody] RoundFeeDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RoundFeeService.CreateRoundFeeAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไขข้อมูล ค่าทำเนียมโอน-สูตรปัดเศษ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/RoundFees/{id}")]
        [ProducesResponseType(200, Type = typeof(RoundFeeDTO))]
        public async Task<IActionResult> EditProjectRoundFee([FromRoute]Guid projectID, [FromRoute]Guid id, [FromBody] RoundFeeDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RoundFeeService.UpdateRoundFeeAsync(projectID, id, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบ ข้อมูล ค่าทำเนียมโอน-สูตรปัดเศษ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/RoundFees/{id}")]
        public async Task<IActionResult> DeleteProjectRoundFee([FromRoute]Guid projectID, [FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RoundFeeService.DeleteRoundFeeAsync(projectID, id);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        #endregion

        #region LowRiseBuildingPriceFee
        /// <summary>
        /// ลิส ข้อมูลค่าธรรมเนียม ค่าพื้นที่สิ่งปลูกสร้างแนวราบ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/LowRiseBuildingPriceFees")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<LowRiseBuildingPriceFeeDTO>))]
        public async Task<IActionResult> GetProjectLowRiseBuildingPriceFeeList([FromRoute]Guid projectID, [FromQuery]LowRiseBuildingPriceFeeFilter filter, [FromQuery]PageParam pageParam, [FromQuery]LowRiseBuildingPriceFeeSortByParam sortByParam)
        {
            try
            {
                var result = await LowRiseBuildingPriceFeeService.GetLowRiseBuildingPriceFeeListAsync(projectID, filter, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);

                return Ok(result.LowRiseBuildingPriceFees);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลค่าธรรมเนียม ค่าพื้นที่สิ่งปลูกสร้างแนวราบ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/LowRiseBuildingPriceFees/{id}")]
        [ProducesResponseType(200, Type = typeof(LowRiseBuildingPriceFeeDTO))]
        public async Task<IActionResult> GetProjectLowRiseBuildingPriceFee([FromRoute]Guid projectID, [FromRoute] Guid id)
        {
            try
            {
                var result = await LowRiseBuildingPriceFeeService.GetLowRiseBuildingPriceFeeAsync(projectID, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้าง ข้อมูลค่าธรรมเนียม ค่าพื้นที่สิ่งปลูกสร้างแนวราบ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/LowRiseBuildingPriceFees")]
        [ProducesResponseType(200, Type = typeof(LowRiseBuildingPriceFeeDTO))]
        public async Task<IActionResult> CreateProjectLowRiseBuildingPriceFee([FromRoute]Guid projectID, [FromBody] LowRiseBuildingPriceFeeDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await LowRiseBuildingPriceFeeService.CreateLowRiseBuildingPriceFeeAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไข ข้อมูลค่าธรรมเนียม ค่าพื้นที่สิ่งปลูกสร้างแนวราบ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/LowRiseBuildingPriceFees/{id}")]
        [ProducesResponseType(200, Type = typeof(LowRiseBuildingPriceFeeDTO))]
        public async Task<IActionResult> EditProjectLowRiseBuildingPriceFee([FromRoute]Guid projectID, [FromRoute]Guid id, [FromBody] LowRiseBuildingPriceFeeDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await LowRiseBuildingPriceFeeService.UpdateLowRiseBuildingPriceFeesync(projectID, id, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบ ข้อมูลค่าธรรมเนียม ค่าพื้นที่สิ่งปลูกสร้างแนวราบ
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/LowRiseBuildingPriceFees/{id}")]
        public async Task<IActionResult> DeleteProjectLowRiseBuildingPriceFee([FromRoute]Guid projectID, [FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await LowRiseBuildingPriceFeeService.DeleteLowRiseBuildingPriceFeeAsync(projectID, id);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        #endregion

        #region LowRiseFenceFee
        /// <summary>
        /// ลิสข้อมูล ค่าธรรมเนียม สำนักงานที่ดินค่ารั้ว (แนวราบ)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/LowRiseFenceFees")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<LowRiseFenceFeeDTO>))]
        public async Task<IActionResult> GetProjectLowRiseFenceFeeList([FromRoute]Guid projectID, [FromQuery]LowRiseFenceFeeFilter filter, [FromQuery]PageParam pageParam, [FromQuery]LowRiseFenceFeeSortByParam sortByParam)
        {
            try
            {
                var result = await LowRiseFenceFeeService.GetLowRiseFenceFeeListAsync(projectID, filter, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);

                return Ok(result.LowRiseFenceFees);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูล ค่าธรรมเนียม สำนักงานที่ดินค่ารั้ว (แนวราบ)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/LowRiseFenceFees/{id}")]
        [ProducesResponseType(200, Type = typeof(LowRiseFenceFeeDTO))]
        public async Task<IActionResult> GetProjectLowRiseFenceFee([FromRoute]Guid projectID, [FromRoute] Guid id)
        {
            try
            {
                var result = await LowRiseFenceFeeService.GetLowRiseFenceFeeAsync(projectID, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้าง ข้อมูล ค่าธรรมเนียม สำนักงานที่ดินค่ารั้ว (แนวราบ)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/LowRiseFenceFees")]
        [ProducesResponseType(200, Type = typeof(LowRiseFenceFeeDTO))]
        public async Task<IActionResult> CreateProjectLowRiseFenceFee([FromRoute]Guid projectID, [FromBody] LowRiseFenceFeeDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await LowRiseFenceFeeService.CreateLowRiseFenceFeeAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไข ข้อมูล ค่าธรรมเนียม สำนักงานที่ดินค่ารั้ว (แนวราบ)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/LowRiseFenceFees/{id}")]
        [ProducesResponseType(200, Type = typeof(LowRiseFenceFeeDTO))]
        public async Task<IActionResult> EditProjectLowRiseFenceFee([FromRoute]Guid projectID, [FromRoute]Guid id, [FromBody] LowRiseFenceFeeDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await LowRiseFenceFeeService.UpdateLowRiseFenceFeeAsync(projectID, id, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบ ข้อมูล ค่าธรรมเนียม สำนักงานที่ดินค่ารั้ว (แนวราบ)
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/LowRiseFenceFees/{id}")]
        public async Task<IActionResult> DeleteProjectLowRiseFenceFee([FromRoute]Guid projectID, [FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await LowRiseFenceFeeService.DeleteLowRiseFenceFeeAsync(projectID, id);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        #endregion

        #endregion

        #region ProjectBudgetPromotion
        /// <summary>
        /// ลิส ข้อมูลBudgetPromotions
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/BudgetPromotions")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<BudgetPromotionDTO>))]
        public async Task<IActionResult> GetProjectBudgetPromotionList([FromRoute]Guid projectID, [FromQuery]BudgetPromotionFilter filter, [FromQuery]PageParam pageParam, [FromQuery]BudgetPromotionSortByParam sortByParam)
        {
            try
            {
                var result = await BudgetPromotionService.GetBudgetPromotionListAsync(projectID, filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.BudgetPromotions);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{projectID}/BudgetPromotions/{unitID}")]
        [ProducesResponseType(200, Type = typeof(BudgetPromotionDTO))]
        public async Task<IActionResult> GetProjectBudgetPromotion([FromRoute]Guid projectID, [FromRoute] Guid unitID)
        {
            try
            {
                var result = await BudgetPromotionService.GetBudgetPromotionAsync(projectID, unitID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("{projectID}/BudgetPromotions")]
        [ProducesResponseType(200, Type = typeof(BudgetPromotionDTO))]
        public async Task<IActionResult> CreateProjectBudgetPromotion([FromRoute]Guid projectID, [FromBody] BudgetPromotionDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BudgetPromotionService.CreateBudgetPromotionAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไข ข้อมูลBudgetPromotions
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="unitID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/BudgetPromotions/{unitID}")]
        [ProducesResponseType(200, Type = typeof(BudgetPromotionDTO))]
        public async Task<IActionResult> EditProjectBudgetPromotion([FromRoute]Guid projectID, [FromRoute]Guid unitID, [FromBody] BudgetPromotionDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BudgetPromotionService.UpdateBudgetPromotionAsync(projectID, unitID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบ ข้อมูลBudgetPromotions
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/BudgetPromotions/{unitID}")]
        public async Task<IActionResult> DeleteProjectBudgetPromotion([FromRoute]Guid projectID, [FromRoute]Guid unitID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await BudgetPromotionService.DeleteBudgetPromotionAsync(projectID, unitID);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Import BudgetPromotion
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/BudgetPromotions/Import")]
        [ProducesResponseType(200, Type = typeof(BudgetPromotionExcelDTO))]
        public async Task<IActionResult> ImportProjectBudgetPromotion([FromRoute]Guid projectID, [FromBody]FileDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BudgetPromotionService.ImportBudgetPromotionAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Export BudgetPromotion
        /// </summary>
        /// <param name="projectID"></param>s
        /// <returns></returns>
        [HttpGet("{projectID}/BudgetPromotions/Export")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportProjectBudgetPromotion([FromRoute]Guid projectID)
        {
            try
            {
                var result = await BudgetPromotionService.ExportExcelBudgetPromotionAsync(projectID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ProjectWaiveQC && WaiveContract

        #region ProjectWaiveQC


        /// <summary>
        /// ลิสข้อมูล WaiveQCs
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/WaiveQCs")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<WaiveQCDTO>))]
        public async Task<IActionResult> GetProjectWaiveQCList([FromRoute]Guid projectID, [FromQuery]WaiveQCFilter filter, [FromQuery]PageParam pageParam, [FromQuery]WaiveQCSortByParam sortByParam)
        {
            try
            {
                var result = await WaiveQCService.GetWaiveQCListAsync(projectID, filter, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);

                return Ok(result.WaiveQC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูล WaiveQCs
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/WaiveQCs/{id}")]
        [ProducesResponseType(200, Type = typeof(WaiveQCDTO))]
        public async Task<IActionResult> GetProjectWaiveQC([FromRoute]Guid projectID, [FromRoute] Guid id)
        {
            try
            {
                var result = await WaiveQCService.GetWaiveQCAsync(projectID, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้าง ข้อมูล WaiveQCs
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/WaiveQCs")]
        [ProducesResponseType(200, Type = typeof(WaiveQCDTO))]
        public async Task<IActionResult> CreateProjectWaiveQC([FromRoute]Guid projectID, [FromBody] WaiveQCDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await WaiveQCService.CreateWaiveQCAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไขข้อมูล WaiveQCs
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/WaiveQCs/{id}")]
        [ProducesResponseType(200, Type = typeof(WaiveQCDTO))]
        public async Task<IActionResult> EditProjectWaiveQC([FromRoute]Guid projectID, [FromRoute]Guid id, [FromBody] WaiveQCDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await WaiveQCService.UpdateWaiveQCAsync(projectID, id, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบ ข้อมูล WaiveQCs
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/WaiveQCs/{id}")]
        public async Task<IActionResult> DeleteProjectWaiveQC([FromRoute]Guid projectID, [FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await WaiveQCService.DeleteWaiveQCAsync(projectID, id);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Import WavieQC Excel
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(WaiveQCExcelDTO))]
        [HttpPost("{projectID}/WaiveQCs/Import")]
        public async Task<IActionResult> ImportProjectWaiveQC([FromRoute]Guid projectID, [FromBody]FileDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await WaiveQCService.ImportWaiveQCAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Export WaiveQC
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/WaiveQCs/Export")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportProjectWaiveQC([FromRoute]Guid projectID, [FromQuery]WaiveQCFilter filter, [FromQuery] WaiveQCSortByParam sortByParam)
        {
            try
            {
                var result = await WaiveQCService.ExportExcelWaiveQCAsync(projectID, filter, sortByParam);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region WaiveCustomerSign
        /// <summary>
        /// ลิสข้อมูล Waive รับบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/WaiveCustomerSigns")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<WaiveCustomerSignDTO>))]
        public async Task<IActionResult> GetProjectWaiveCustomerSignList([FromRoute]Guid projectID, [FromQuery]WaiveCustomerSignFilter filter, [FromQuery]PageParam pageParam, [FromQuery]WaiveCustomerSignSortByParam sortByParam)
        {
            try
            {
                var result = await WaiveCustomerSignService.GetWaiveCustomerSignListAsync(projectID, filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.WaiveCustomerSigns);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูล Waive รับบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/WaiveCustomerSigns/{id}")]
        [ProducesResponseType(200, Type = typeof(WaiveCustomerSignDTO))]
        public async Task<IActionResult> GetProjectWaiveCustomerSign([FromRoute]Guid projectID, [FromRoute] Guid id)
        {
            try
            {
                var result = await WaiveCustomerSignService.GetWaiveCustomerSignAsync(projectID, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้าง ข้อมูล Waive รับบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectID}/WaiveCustomerSigns")]
        [ProducesResponseType(200, Type = typeof(WaiveCustomerSignDTO))]
        public async Task<IActionResult> CreateProjectWaiveCustomerSign([FromRoute]Guid projectID, [FromBody] WaiveCustomerSignDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await WaiveCustomerSignService.CreateWaiveCustomerSignAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// แก้ไข ข้อมูล Waive รับบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectID}/WaiveCustomerSigns/{id}")]
        [ProducesResponseType(200, Type = typeof(WaiveCustomerSignDTO))]
        public async Task<IActionResult> EditProjectWaiveCustomerSign([FromRoute]Guid projectID, [FromRoute]Guid id, [FromBody] WaiveCustomerSignDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await WaiveCustomerSignService.UpdateWaiveCustomerSignAsync(projectID, id, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ลบ ข้อมูล Waive รับบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{projectID}/WaiveCustomerSigns/{id}")]
        public async Task<IActionResult> DeleteProjectWaiveCustomerSign([FromRoute]Guid projectID, [FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await WaiveQCService.DeleteWaiveQCAsync(projectID, id);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Import WaiveCustomerSign
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(WaiveCustomerSignExcelDTO))]
        [HttpPost("{projectID}/WaiveCustomerSigns/Import")]
        public async Task<IActionResult> ImportProjectWaiveCustomerSign([FromRoute]Guid projectID, [FromBody]FileDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await WaiveCustomerSignService.ImportWaiveCustomerSignAsync(projectID, input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Export WaiveCustomerSign
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="filter"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/WaiveCustomerSigns/Export")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportProjectWaiveCustomerSign([FromRoute]Guid projectID, [FromQuery]WaiveCustomerSignFilter filter, [FromQuery] WaiveCustomerSignSortByParam sortByParam)
        {
            try
            {
                var result = await WaiveCustomerSignService.ExportExcelWaiveCustomerSignAsync(projectID, filter, sortByParam);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region ProjectUnitMeter
        //[HttpGet("{projectID}/UnitsMeters")]
        //[PagingResponseHeaders]
        //[ProducesResponseType(200, Type = typeof(List<UnitMeterListDTO>))]
        //public async Task<IActionResult> GetProjectUnitMeterList([FromRoute]Guid projectID, [FromQuery]UnitMeterFilter unitMeterFilter)
        //{
        //    return Ok();
        //}

        //[HttpGet("{projectID}/UnitsMeters/{id}")]
        //[ProducesResponseType(200, Type = typeof(UnitMeterDTO))]
        //public async Task<IActionResult> GetProjectUnitMeter([FromRoute]Guid projectID, [FromRoute] Guid id)
        //{
        //    return Ok();
        //}

        //[HttpPost("{projectID}/UnitsMeters")]
        //[ProducesResponseType(200, Type = typeof(UnitMeterDTO))]
        //public async Task<IActionResult> CreateProjectUnitMeter([FromRoute]Guid projectID, [FromBody] UnitMeterDTO input)
        //{
        //    return Ok();
        //}

        //[HttpPut("{projectID}/UnitsMeters/{id}")]
        //[ProducesResponseType(200, Type = typeof(UnitMeterDTO))]
        //public async Task<IActionResult> EditProjectUnitMeter([FromRoute]Guid projectID, [FromRoute]Guid id, [FromBody] UnitMeterDTO input)
        //{
        //    return Ok();
        //}

        //[HttpDelete("{projectID}/UnitsMeters/{id}")]
        //public async Task<IActionResult> DeleteProjectUnitMeter([FromRoute]Guid projectID, [FromRoute]Guid id)
        //{
        //    return Ok();
        //}

        //[Consumes("multipart/form-data")]
        //[HttpPost("{projectID}/UnitsMeters/Import")]
        //public async Task<IActionResult> ImportProjectUnitMeter([FromRoute]Guid projectID, [FromForm]IFormFile input)
        //{
        //    return Ok();
        //}

        //[HttpGet("{projectID}/UnitsMeters/Export")]
        //[ProducesResponseType(200, Type = typeof(FileDTO))]
        //public async Task<IActionResult> ExportProjectUnitMeter([FromRoute]Guid projectID)
        //{
        //    return Ok();
        //}
        #endregion

        #region ProjectWaterElectricMeterPrice
        /// <summary>
        /// ลิส ข้อมูลมิเตอร์ไฟฟ้า-น้ำประปา ของแบบบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="modelID"></param>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Model/{modelID}/WaterElectricMeterPrice")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<WaterElectricMeterPriceDTO>))]
        public async Task<IActionResult> GetProjectWaterElectricMeterPriceList([FromRoute]Guid projectID, [FromRoute]Guid modelID, [FromQuery]WaterElectricMeterPriceFilter filter, [FromQuery]PageParam pageParam, [FromQuery]SortByParam sortByParam)
        {
            try
            {
                var result = await WaterElectricMeterPriceService.GetWaterElectricMeterPriceListAsync(modelID, filter, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);

                return Ok(result.WaterElectricMeterPrices);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลมิเตอร์ไฟฟ้า-น้ำประปา ของแบบบ้าน
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="modelID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{projectID}/Model/{modelID}/WaterElectricMeterPrice/{id}")]
        [ProducesResponseType(200, Type = typeof(WaterElectricMeterPriceDTO))]
        public async Task<IActionResult> GetProjectWaterElectricMeterPrice([FromRoute]Guid projectID, [FromRoute]Guid modelID, [FromRoute]Guid id)
        {
            try
            {
                var result = await WaterElectricMeterPriceService.GetWaterElectricMeterPriceAsync(modelID, id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///// <summary>
        ///// สร้าง ข้อมูลมิเตอร์ไฟฟ้า-น้ำประปา ของแบบบ้าน
        ///// </summary>
        ///// <param name="projectID"></param>
        ///// <param name="modelID"></param>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //[HttpPost("{projectID}/Model/{modelID}/WaterElectricMeterPrice")]
        //[ProducesResponseType(200, Type = typeof(WaterElectricMeterPriceDTO))]
        //public async Task<IActionResult> CreateProjectWaterElectricMeterPrice([FromRoute] Guid projectID,[FromRoute] Guid modelID, [FromBody]WaterElectricMeterPriceDTO input)
        //{
        //    using (var tran = DB.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            var result = await WaterElectricMeterPriceService.CreateWaterElectricMeterPriceAsync(modelID, input);
        //            tran.Commit();
        //            return Ok(result);
        //        }
        //        catch (Exception ex)
        //        {
        //            tran.Rollback();
        //            throw ex;
        //        }
        //    }
        //}
        ///// <summary>
        ///// แก้ไข ข้อมูลมิเตอร์ไฟฟ้า-น้ำประปา ของแบบบ้าน
        ///// </summary>
        ///// <param name="projectID"></param>
        ///// <param name="modelID"></param>
        ///// <param name="id"></param>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //[HttpPut("{projectID}/Model/{modelID}/WaterElectricMeterPrice/{id}")]
        //[ProducesResponseType(200, Type = typeof(WaterElectricMeterPriceDTO))]
        //public async Task<IActionResult> EditProjectWaterElectricMeterPrice([FromRoute] Guid projectID, [FromRoute] Guid modelID,[FromRoute]Guid id, [FromBody]WaterElectricMeterPriceDTO input)
        //{
        //    using (var tran = DB.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            var result = await WaterElectricMeterPriceService.UpdateWaterElectricMeterPriceAsync(modelID, id, input);
        //            tran.Commit();
        //            return Ok(result);
        //        }
        //        catch (Exception ex)
        //        {
        //            tran.Rollback();
        //            throw ex;
        //        }
        //    }
        //}
        ///// <summary>
        ///// ลบ ข้อมูลมิเตอร์ไฟฟ้า-น้ำประปา ของแบบบ้าน
        ///// </summary>
        ///// <param name="projectID"></param>
        ///// <param name="modelID"></param>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpDelete("{projectID}/Model/{modelID}/WaterElectricMeterPrice/{id}")]
        //public async Task<IActionResult> DeleteProjectWaterElectricMeterPrice([FromRoute] Guid projectID,[FromRoute]Guid modelID, [FromRoute] Guid id)
        //{
        //    using (var tran = DB.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            var result = await WaterElectricMeterPriceService.DeleteWaterElectricMeterPriceAsync(modelID, id);
        //            tran.Commit();
        //            return Ok();
        //        }
        //        catch (Exception ex)
        //        {
        //            tran.Rollback();
        //            throw ex;
        //        }
        //    }
        //}
        #endregion


        #region UnitOtherUnitInfoTag
        [HttpGet("UnitOtherUnitInfoTag")]
        [ProducesResponseType(200, Type = typeof(List<UnitOtherUnitInfoTagDTO>))]
        public async Task<IActionResult> GetUnitOtherUnitInfoTag([FromQuery]UnitOtherUnitInfoTagFilter request)
        {
            try
            {
                var result = await UnitOtherUnitInfoTagService.GetUnitTagListAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}