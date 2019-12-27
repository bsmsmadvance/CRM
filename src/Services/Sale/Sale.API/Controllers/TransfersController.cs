using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs.CTM;
using Base.DTOs.FIN;
using Base.DTOs.PRJ;
using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Database.Models.SAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Sale.Params.Filters;
using Sale.Params.Inputs;
using Sale.Params.Outputs;
using Sale.Services;
using TransferDTO = Base.DTOs.SAL.TransferDTO;

namespace Sale.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class TransfersController : BaseController
    {
        private readonly ITransferService TransferService;
        private readonly DatabaseContext DB;

        public TransfersController(ITransferService transferService, DatabaseContext db)
        {
            this.TransferService = transferService;
            this.DB = db;
        }

        /// <summary>
        /// ดึงข้อมูลโอนกรรมสิทธิ์ (โครงการ/ค่าธรรมเนียม)   
        /// </summary>
        /// <param name="id">transferId</param>
        /// <returns></returns>
        [HttpGet("GetTransfer/{id}")]
        [ProducesResponseType(200, Type = typeof(TransferDTO))]
        public async Task<IActionResult> GetTransferAsync([FromRoute]Guid id)
        {
            var result = await TransferService.GetTransferAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลเพื่อโอนกรรมสิทธิ์ของสัญญา
        /// </summary>
        /// <param name="id">agreementId</param>
        /// <returns></returns>
        [HttpGet("GetTransferDraf/{id}")]
        [ProducesResponseType(200, Type = typeof(TransferDTO))]
        public async Task<IActionResult> GetTransferDrafAsync([FromRoute]Guid id)
        {
            var result = await TransferService.GetTransferDrafAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลโอนกรรมสิทธิ์ (ราคา)   
        /// </summary>
        /// <param name="id">transferId</param>
        /// <returns></returns>
        [HttpGet("GetTransferPrice/{id}")]
        [ProducesResponseType(200, Type = typeof(TransferPriceListDTO))]
        public async Task<IActionResult> GetTransferPriceAsync([FromRoute]Guid id)
        {
            var result = await TransferService.GetTransferPriceAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลรายการค่าใช้จ่าย   
        /// </summary>
        /// <param name="id">transferId</param>
        /// <returns></returns>
        [HttpGet("GetTransferFee/{id}")]
        [ProducesResponseType(200, Type = typeof(List<TransferExpenseListDTO>))]
        public async Task<IActionResult> GetTransferFeeAsync([FromRoute]Guid id)
        {
            var result = await TransferService.GetTransferFeeAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลรายละเอียดยอดเงินคงเหลือ 
        /// </summary>
        /// <param name="id">transferId</param>
        /// <returns></returns>
        [HttpGet("GetTransferMoney/{id}")]
        [ProducesResponseType(200, Type = typeof(TransferDTO))]
        public async Task<IActionResult> GetTransferMoneyAsync([FromRoute]Guid id)
        {
            var result = await TransferService.GetTransferMoneyAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// เงื่อนไขการตั้งเรื่องโอนกรรมสิทธิ์    
        /// </summary>
        /// <param name="id">agreementId</param>
        /// <returns>
        /// IsTitledeedNo = มีข้อมูลโฉนดแล้ว, 
        /// IsCreditBanking = มีข้อมูลขอสินเชื่อ, 
        /// IsWaiveQC = มีข้อมูล QC Pass, 
        /// IsWaiveSign = มีข้อมูลตรวจรับบ้านแล้ว, 
        /// IsNotTransfer = ยังไม่มีตั้งเรื่องโอน
        /// </returns>
        [HttpGet("ValidateCreateTransfer/{id}")]
        [ProducesResponseType(200, Type = typeof(TransferValidate))]
        public async Task<IActionResult> ValidateCreateTransferAsync([FromRoute]Guid id)
        {
            var result = await TransferService.ValidateCreateTransferAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลผู้โอนกรรมสิทธิ์ จากผู้ทำสัญญา  
        /// </summary>
        /// <param name="id">agreementId</param>
        /// <returns></returns>
        [HttpGet("GetTransferOwnerDraf/{id}")]
        [ProducesResponseType(200, Type = typeof(AgreementOwnerDTO))]
        public async Task<IActionResult> GetTransferOwnerDrafAsync([FromRoute]Guid id)
        {
            var result = await TransferService.GetTransferOwnerDrafAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลผู้โอนกรรมสิทธิ์ 
        /// </summary>
        /// <param name="id">transferOwnerId</param>
        /// <returns></returns>
        [HttpGet("GetTransferOwner/{id}")]
        [ProducesResponseType(200, Type = typeof(TransferOwnerDTO))]
        public async Task<IActionResult> GetTransferOwnerAsync([FromRoute]Guid id)
        {
            var result = await TransferService.GetTransferOwnerAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// แก้ไขข้อมูลผู้โอนกรรมสิทธิ์
        /// </summary>
        /// <param name="id">transferOwnerId</param>
        /// <param name="input">TransferOwnerDTO</param>
        /// <returns></returns>
        [HttpPost("UpdateTransferOwner/{id}")]
        [ProducesResponseType(200, Type = typeof(TransferOwnerDTO))]
        public async Task<IActionResult> UpdateTransferOwnerAsync([FromRoute]Guid id, [FromBody]TransferOwnerDTO input)
        {
            var result = await TransferService.UpdateTransferOwnerAsync(id, input);
            return Ok(result);
        }

        /// <summary>
        /// ดึงรายการข้อมูลผู้โอนกรรมสิทธิ์
        /// </summary>
        /// <param name="id">transferId</param>
        /// <returns></returns>
        [HttpGet("GetTransferOwnerList/{id}")]
        [ProducesResponseType(200, Type = typeof(List<TransferOwnerDTO>))]
        public async Task<IActionResult> GetTransferOwnerListAsync([FromRoute]Guid id)
        {
            var result = await TransferService.GetTransferOwnerListAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// กอปปี้ที่อยู่ตามบัตรประชาชน
        /// </summary>
        /// <param name="contactId">contactId</param>
        /// <returns></returns>
        [HttpGet("CopyContactAddress/{contactId}")]
        [ProducesResponseType(200, Type = typeof(ContactAddressDTO))]
        public async Task<IActionResult> CopyContactAddressAsync([FromRoute]Guid contactId)
        {
            var result = await TransferService.CopyContactAddressAsync(contactId);
            return Ok(result);
        }

        /// <summary>
        /// กอปปี้ที่อยู่โครงการ
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// <returns></returns>
        [HttpGet("CopyProjectAddress/{projectId}")]
        [ProducesResponseType(200, Type = typeof(ProjectAddressDTO))]
        public async Task<IActionResult> CopyProjectAddressAsync([FromRoute]Guid projectId)
        {
            var result = await TransferService.CopyProjectAddressAsync(projectId);
            return Ok(result);
        }


        /// <summary>
        /// บันทึกข้อมูลการตั้งเรื่องโอนกรรมสิทธิ์
        /// </summary>
        /// <param name="input">TransferDTO</param>
        /// <returns></returns>
        [HttpPost("CreateTransferData/{input}")]
        [ProducesResponseType(200, Type = typeof(TransferDTO))]
        public async Task<IActionResult> CreateTransferDataAsync([FromBody]TransferDTO input)
        {
            var result = await TransferService.CreateTransferDataAsync(input);
            return Ok(result);
        }

        /// <summary>
        /// แก้ไขข้อมูลการตั้งเรื่องโอนกรรมสิทธิ์
        /// </summary>
        /// <param name="id">transferId</param>
        /// <param name="input">TransferDTO</param>
        /// <returns></returns>
        [HttpPost("UpdateTransferData/{id}")]
        [ProducesResponseType(200, Type = typeof(TransferDTO))]
        public async Task<IActionResult> UpdateTransferDataAsync([FromRoute]Guid id, [FromBody]TransferDTO input)
        {
            var result = await TransferService.UpdateTransferDataAsync(id, input);
            return Ok(result);
        }

        /// <summary>
        /// ยกเลิกการตั้งเรื่องโอนกรรมสิทธิ์
        /// </summary>
        /// <param name="id">transferId</param>
        /// <returns></returns>
        [HttpGet("DeleteTransfer/{id}")]
        [ProducesResponseType(200, Type = typeof(Transfer))]
        public async Task<IActionResult> DeleteTransferAsync([FromRoute]Guid id)
        {
            var result = await TransferService.DeleteTransferAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// การแสดงข้อมูลรายการชำระ
        /// </summary>
        /// <param name="id">transferId</param>
        /// <returns></returns>
        [HttpGet("GetPaymentDetail/{id}")]
        [ProducesResponseType(200, Type = typeof(List<TransferPaymentDTO>))]
        public async Task<IActionResult> GetPaymentDetailAsync([FromRoute]Guid id)
        {
            var result = await TransferService.GetPaymentDetailAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// การแสดงข้อมูลสรุปการรับเงิน
        /// </summary>
        /// <param name="id">transferId</param>
        /// <returns></returns>
        [HttpGet("GetReceiptDetail/{id}")]
        [ProducesResponseType(200, Type = typeof(List<TransferPaymentDTO>))]
        public async Task<IActionResult> GetReceiptDetailAsync([FromRoute]Guid id)
        {
            var result = await TransferService.GetReceiptDetailAsync(id);
            return Ok(result);
        }




    }
}
