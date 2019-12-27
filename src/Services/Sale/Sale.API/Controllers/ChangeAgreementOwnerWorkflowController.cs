using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.SAL;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Sale.Params.Outputs;
using Sale.Services;
using Sale.Services.Service;
using Sale.Params.Inputs;

namespace Sale.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ChangeAgreementOwnerWorkflowController : Controller
    {
        private readonly IChangeAgreementOwnerWorkflowService ChangeAgreementOwnerWorkflowService;
        private readonly DatabaseContext DB;

        public ChangeAgreementOwnerWorkflowController(IChangeAgreementOwnerWorkflowService changeAgreementOwnerWorkflow, DatabaseContext db)
        {
            this.ChangeAgreementOwnerWorkflowService = changeAgreementOwnerWorkflow;
            this.DB = db;
        }

        /// <summary>
        /// ดึงข้อมูลผู้ทำสัญญาหลังตั้งเรื่องเพิ่มชื่อ
        /// </summary>
        /// <param name="agreementId"></param>
        /// <param name="changeAgreementOwnerWorkflowID"></param>
        /// <returns></returns>
        [HttpGet("{agreementId}/GetAgreementOwnersChangeAgreementOwnerWorkflow/{changeAgreementOwnerWorkflowID}")]
        [ProducesResponseType(200, Type = typeof(List<AgreementOwnerDTO>))]
        public async Task<IActionResult> GetAgreementOwnersChangeAgreementOwnerWorkflowAsync([FromRoute]Guid agreementId, [FromRoute]Guid? changeAgreementOwnerWorkflowID)
        {
            var result = await ChangeAgreementOwnerWorkflowService.GetAgreementOwnersChangeAgreementOwnerWorkflowAsync(agreementId, changeAgreementOwnerWorkflowID);
            return Ok(result);
        }
        /// <summary>
        /// ดึงข้อมูลผู้ทำสัญญาหลักหลังตั้งเรื่องเพิ่มชื่อ
        /// </summary>
        /// <param name="agreementId"></param>
        /// <param name="changeAgreementOwnerWorkflowID"></param>
        /// <returns></returns>
        [HttpGet("{agreementId}/GetMainAgreementOwnersChangeAgreementOwnerWorkflowAsync/{changeAgreementOwnerWorkflowID}")]
        [ProducesResponseType(200, Type = typeof(List<AgreementOwnerDTO>))]
        public async Task<IActionResult> GetMainAgreementOwnersChangeAgreementOwnerWorkflowAsync([FromRoute]Guid agreementId, [FromRoute]Guid? changeAgreementOwnerWorkflowID)
        {
            var result = await ChangeAgreementOwnerWorkflowService.GetMainAgreementOwnersChangeAgreementOwnerWorkflowAsync(agreementId, changeAgreementOwnerWorkflowID);
            return Ok(result);
        }

        /// <summary>
        /// สร้างข้อมูล ChangeAgreementOwnerWorkflowDTO จากการเพิ่มชื่อ 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/GetChangeAgreementOwnerWorkflow")]
        [ProducesResponseType(200, Type = typeof(ChangeAgreementOwnerWorkflowDTO))]
        public async Task<IActionResult> GetChangeAgreementOwnerWorkflowAsync([FromRoute]Guid id)
        {
            var result = await this.ChangeAgreementOwnerWorkflowService.GetChangeAgreementOwnerWorkflowAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// สร้างข้อมูล ChangeAgreementOwnerWorkflow
        /// </summary>
        /// <param name="input"></param>
        /// <returns>ChangeAgreementOwnerWorkflowDTO</returns>
        [HttpPost("CreateChangeAgreementOwnerWorkflow")]
        [ProducesResponseType(200, Type = typeof(ChangeAgreementOwnerWorkflowDTO))]
        public async Task<IActionResult> CreateChangeAgreementOwnerWorkflowAsync([FromBody]ChangeAgreementOwnerWorkflowInput input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await this.ChangeAgreementOwnerWorkflowService.CreateChangeAgreementOwnerWorkflowAsync(input.ChangeAgreementOwnerWorkflow, input.ListAgreementOwner);
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
        /// อนุมัติ ChangeAgreementOwnerWorkflow
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("ApproveChangeAgreementOwnerWorkflow")]
        [ProducesResponseType(200, Type = typeof(ChangeAgreementOwnerWorkflowDTO))]
        public async Task<IActionResult> ApproveChangeAgreementOwnerWorkflowAsync([FromBody]ChangeAgreementOwnerWorkflowDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    //Get user ID
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
                    {
                        userID = parsedUserID;
                    }
                    var result = await this.ChangeAgreementOwnerWorkflowService.ApproveChangeAgreementOwnerWorkflowAsync(input,userID);
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
        /// Cancel Appove ชื่อผู้ทำสัญญา
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("CancelApproveChangeAgreementOwnerWorkflow")]
        [ProducesResponseType(200, Type = typeof(ChangeAgreementOwnerWorkflowDTO))]
        public async Task<IActionResult> CancelApproveChangeAgreementOwnerWorkflowAsync([FromBody]ChangeAgreementOwnerWorkflowDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    //Get user ID
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
                    {
                        userID = parsedUserID;
                    }
                    var result = await this.ChangeAgreementOwnerWorkflowService.CancelApproveChangeAgreementOwnerWorkflowAsync(input,userID);
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
        /// ตรวจสอบเงื่อนไข เพิ่มชื่อ
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        [HttpGet("{agreementId}/ValidateAgreementOwnerWorkflow")]
        [ProducesResponseType(200, Type = typeof(ValidateAgreementOwnerWorkflowOutput))]
        public async Task<IActionResult> ValidateAgreementOwnerWorkflowAsync([FromRoute]Guid agreementId)
        {

            var result = await this.ChangeAgreementOwnerWorkflowService.ValidateAgreementOwnerWorkflowAsync(agreementId);

            return Ok(result);

        }

        /// <summary>
        /// อนุมัติ RequestChangeAgreementOwnerWorkflow
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("ApproveRequestChangeAgreementOwnerWorkflow")]
        [ProducesResponseType(200, Type = typeof(ChangeAgreementOwnerWorkflowDTO))]
        public async Task<IActionResult> ApproveRequestChangeAgreementOwnerWorkflowAsync([FromBody]ChangeAgreementOwnerWorkflowDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    //Get user ID
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
                    {
                        userID = parsedUserID;
                    }

                    var result = await this.ChangeAgreementOwnerWorkflowService.ApproveRequestChangeAgreementOwnerWorkflowAsync(input, userID);
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
        /// ไม่อนุมัติ RequestChangeAgreementOwnerWorkflow
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("CancelApproveRequestChangeAgreementOwnerWorkflow")]
        [ProducesResponseType(200, Type = typeof(ChangeAgreementOwnerWorkflowDTO))]
        public async Task<IActionResult> CancelApproveRequestChangeAgreementOwnerWorkflowAsync([FromBody]ChangeAgreementOwnerWorkflowDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    //Get user ID
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
                    {
                        userID = parsedUserID;
                    }

                    var result = await this.ChangeAgreementOwnerWorkflowService.CancelApproveRequestChangeAgreementOwnerWorkflowAsync(input, userID);
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
        /// อนุมัติ Print ChangeAgreementOwnerWorkflow
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("ApprovePrintChangeAgreementOwnerWorkflow")]
        [ProducesResponseType(200, Type = typeof(ChangeAgreementOwnerWorkflowDTO))]
        public async Task<IActionResult> ApprovePrintChangeAgreementOwnerWorkflowAsync([FromBody]ChangeAgreementOwnerWorkflowDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    //Get user ID
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
                    {
                        userID = parsedUserID;
                    }
                    var result = await this.ChangeAgreementOwnerWorkflowService.ApprovePrintChangeAgreementOwnerWorkflowAsync(input,userID);
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
        /// เพิ่มชื่อผู้ทำสัญญา
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("AddContactToAgreementOwnerAsync")]
        [ProducesResponseType(200, Type = typeof(List<AgreementOwnerDTO>))]
        public async Task<IActionResult> AddContactToAgreementOwnerAsync([FromBody]AddContactToAgreementOwnerInput input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await this.ChangeAgreementOwnerWorkflowService.AddContactToAgreementOwnerAsync(input.AgreementId, input.ListContractId);
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
        /// ลบ ชื่อที่เพิ่มเขามา
        /// </summary>
        /// <param name="agreementOwnerID"></param>
        /// <returns></returns>
        [HttpGet("{agreementOwnerID}/DeleteAddNewAgreementOwnerWorkflowAsync")]
        [ProducesResponseType(200, Type = typeof(ChangeAgreementOwnerWorkflowDTO))]
        public async Task<IActionResult> DeleteAddNewAgreementOwnerWorkflowAsync([FromRoute]Guid agreementOwnerID)
        {
            //using (var tran = DB.Database.BeginTransaction())
            //{
            //    try
            //    {
                    var result = await this.ChangeAgreementOwnerWorkflowService.DeleteAddNewAgreementOwnerWorkflowAsync(agreementOwnerID);
                    //tran.Commit();
                    return Ok(result);
            //    }
            //    catch (Exception ex)
            //    {
            //        tran.Rollback();
            //        throw ex;
            //    }
            //}
        }

    }
}
