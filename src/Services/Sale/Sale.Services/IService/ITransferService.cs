using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.CTM;
using Base.DTOs.PRJ;
using Base.DTOs.SAL;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using Sale.Params.Inputs;
using Sale.Params.Outputs;

namespace Sale.Services
{
    public interface ITransferService
    {
        /// <summary>
        /// ดึงข้อมูลโอนกรรมสิทธิ์ (โครงการ/ค่าธรรมเนียม)   
        /// </summary>
        /// <param name="transferId"></param>
        /// <returns></returns>
        Task<TransferDTO> GetTransferAsync(Guid transferId);

        /// <summary>
        /// ดึงข้อมูลเพื่อโอนกรรมสิทธิ์ของสัญญา ก่อนตั้งเรื่องโอน     
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        Task<TransferDTO> GetTransferDrafAsync(Guid agreementId);

        /// <summary>
        /// ดึงข้อมูลโอนกรรมสิทธิ์ (ราคา)   
        /// </summary>
        /// <param name="transferId"></param>
        /// <returns></returns>
        Task<TransferPriceListDTO> GetTransferPriceAsync(Guid transferId);

        /// <summary>
        /// ดึงข้อมูลโอนกรรมสิทธิ์ (ราคา) ก่อนตั้งเรื่องโอน   
        /// </summary>
        /// <param name="transferId"></param>
        /// <returns></returns>
        Task<TransferPriceListDTO> GetTransferPriceDrafAsync(Guid agreementId);

        /// <summary>
        /// ดึงข้อมูลรายการค่าใช้จ่าย   
        /// </summary>
        /// <param name="transferId"></param>
        /// <returns></returns>
        Task<List<TransferExpenseListDTO>> GetTransferFeeAsync(Guid transferId);

        /// <summary>
        /// ดึงข้อมูลรายการค่าใช้จ่ายของสัญญา ก่อนตั้งเรื่องโอน  
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        Task<List<TransferExpenseListDTO>> GetTransferFeeDrafAsync(Guid agreementId);

        /// <summary>
        /// ดึงข้อมูลรายละเอียดยอดเงินคงเหลือ 
        /// </summary>
        /// <param name="transferId"></param>
        /// <returns></returns>
        Task<TransferDTO> GetTransferMoneyAsync(Guid transferId);

        /// <summary>
        /// เงื่อนไขการตั้งเรื่องโอนกรรมสิทธิ์    
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns>
        /// IsTitledeedNo = มีข้อมูลโฉนดแล้ว, 
        /// IsCreditBanking = มีข้อมูลขอสินเชื่อ, 
        /// IsWaiveQC = มีข้อมูล QC Pass, 
        /// IsWaiveSign = มีข้อมูลตรวจรับบ้านแล้ว, 
        /// IsNotTransfer = ยังไม่มีตั้งเรื่องโอน
        /// </returns>
        Task<TransferValidate> ValidateCreateTransferAsync(Guid agreementId);

        /// <summary>
        /// ดึงข้อมูลผู้โอนกรรมสิทธิ์ จากผู้ทำสัญญา  
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        Task<AgreementOwnerDTO> GetTransferOwnerDrafAsync(Guid agreementId);

        /// <summary>
        /// ดึงข้อมูลผู้โอนกรรมสิทธิ์ 
        /// </summary>
        /// <param name="transferOwnerId"></param>
        /// <returns></returns>
        Task<TransferOwnerDTO> GetTransferOwnerAsync(Guid transferOwnerId);

        /// <summary>
        /// แก้ไขข้อมูลผู้โอนกรรมสิทธิ์
        /// </summary>
        /// <param name="transferOwnerId"></param>
        /// <param name="transfer"></param>
        /// <returns></returns>
        Task<TransferOwnerDTO> UpdateTransferOwnerAsync(Guid transferOwnerId, TransferOwnerDTO transferOwner);

        /// <summary>
        /// ดึงรายการข้อมูลผู้โอนกรรมสิทธิ์
        /// </summary>
        /// <param name="transferId"></param>
        /// <returns></returns>
        Task<List<TransferOwnerDTO>> GetTransferOwnerListAsync(Guid transferId);

        /// <summary>
        /// ดึงรายการข้อมูลผู้โอนกรรมสิทธิ์ของสัญญา ก่อนตั้งเรื่องโอน  
        /// </summary>
        /// <param name="transferId"></param>
        /// <returns></returns>
        Task<List<TransferOwnerDTO>> GetTransferOwnerDrafListAsync(Guid agreementId);

        /// <summary>
        /// การแสดงข้อมูลรายการชำระ (ตามงวดชำระ)
        /// </summary>
        /// <param name="transferId"></param>
        /// <returns></returns>
        Task<List<Base.DTOs.FIN.TransferPaymentDTO>> GetPaymentDetailAsync(Guid transferId);

        /// <summary>
        /// การแสดงข้อมูลสรุปการรับเงิน (ตามการชำระ)
        /// </summary>
        /// <param name="transferId"></param>
        /// <returns></returns>
        Task<List<Base.DTOs.FIN.TransferPaymentDTO>> GetReceiptDetailAsync(Guid transferId);

        /// <summary>
        /// บันทึกข้อมูลการตั้งเรื่องโอนกรรมสิทธิ์
        /// </summary>
        /// <param name="transferId"></param>
        /// <returns></returns>
        Task<TransferDTO> CreateTransferDataAsync(TransferDTO input);

        /// <summary>
        /// แก้ไขข้อมูลการตั้งเรื่องโอนกรรมสิทธิ์
        /// </summary>
        /// <param name="transferId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<TransferDTO> UpdateTransferDataAsync(Guid transferId, TransferDTO input);

        /// <summary>
        /// ยกเลิกการตั้งเรื่องโอนกรรมสิทธิ์
        /// </summary>
        /// <param name="transferId"></param>
        /// <returns></returns>
        Task<Transfer> DeleteTransferAsync(Guid transferId);

        /// <summary>
        /// กอปปี้ที่อยู่โครงการ
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<ProjectAddressDTO> CopyProjectAddressAsync(Guid projectId);

        /// <summary>
        /// กอปปี้ที่อยู่ลูกค้า
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<ContactAddressDTO> CopyContactAddressAsync(Guid contactId);
    }
}
