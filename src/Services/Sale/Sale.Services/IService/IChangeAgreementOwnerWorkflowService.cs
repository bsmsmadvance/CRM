using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.SAL;
using Database.Models.SAL;
using Sale.Params.Inputs;
using Sale.Params.Outputs;

namespace Sale.Services
{
    public interface IChangeAgreementOwnerWorkflowService
    {
        //เพิ่มชื่อ
        //ลดชื่อ
        //โอนสิทธิ์เปลี่ยนมือ

        /// <summary>
        /// สร้างข้อมูล ChangeAgreementOwnerWorkflow
        /// </summary>
        /// <param name="input"></param>
        /// <param name="ListAgreementOwner"></param>
        /// <returns></returns>
        Task<ChangeAgreementOwnerWorkflowDTO> CreateChangeAgreementOwnerWorkflowAsync(ChangeAgreementOwnerWorkflowDTO input, List<AgreementOwnerDTO> ListAgreementOwner);

        /// <summary>
        /// อนุมัติ RequestChangeAgreementOwnerWorkflow
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ChangeAgreementOwnerWorkflowDTO> ApproveRequestChangeAgreementOwnerWorkflowAsync(ChangeAgreementOwnerWorkflowDTO input, Guid? UserID);

        /// <summary>
        /// ไม่อนุมัติ RequestChangeAgreementOwnerWorkflow
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ChangeAgreementOwnerWorkflowDTO> CancelApproveRequestChangeAgreementOwnerWorkflowAsync(ChangeAgreementOwnerWorkflowDTO input, Guid? UserID);

        /// <summary>
        /// อนุมัติ ChangeAgreementOwnerWorkflow
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ChangeAgreementOwnerWorkflowDTO> ApproveChangeAgreementOwnerWorkflowAsync(ChangeAgreementOwnerWorkflowDTO input, Guid? UserID);

        /// <summary>
        /// ไม่อนุมัติ ChangeAgreementOwnerWorkflow
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ChangeAgreementOwnerWorkflowDTO> CancelApproveChangeAgreementOwnerWorkflowAsync(ChangeAgreementOwnerWorkflowDTO input, Guid? UserID);

        /// <summary>
        /// ลบ ChangeAgreementOwnerWorkflow
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //Task<ChangeAgreementOwnerWorkflow> DeleteChangeAgreementOwnerWorkflowAsync(Guid id);

        /// <summary>
        /// AddContact To AgreementOwner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<AgreementOwnerDTO>> AddContactToAgreementOwnerAsync(Guid agreementId, List<Guid> listContactId);

        /// <summary>
        /// ตรวจสอบเงื่อนไข เพิ่มชื่อ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ValidateAgreementOwnerWorkflowOutput> ValidateAgreementOwnerWorkflowAsync(Guid agreementId);

        /// <summary>
        /// ดึงข้อมูลผู้ทำสัญญาหลังตั้งเรื่องเพิ่มชื่อ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<AgreementOwnerDTO>> GetAgreementOwnersChangeAgreementOwnerWorkflowAsync(Guid agreementId, Guid? changeAgreementOwnerWorkflowI);

        /// <summary>
        /// ดึงข้อมูลผู้ทำสัญญาหลักหลังตั้งเรื่องเพิ่มชื่อ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<AgreementOwnerDTO>> GetMainAgreementOwnersChangeAgreementOwnerWorkflowAsync(Guid agreementId, Guid? changeAgreementOwnerWorkflowId);

        /// <summary>
        /// สร้างข้อมูล AgreementOwner จากการเพิ่มชื่อ 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task<AgreementOwnerDropdownDTO> CreateAgreementOwnerAsync(Guid agreementId, Guid contactId);

        /// <summary>
        /// สร้างข้อมูล ChangeAgreementOwnerWorkflowDTO จากการเพิ่มชื่อ 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ChangeAgreementOwnerWorkflowDTO> GetChangeAgreementOwnerWorkflowAsync(Guid? id);


        /// <summary>
        /// อนุมัติ Print ChangeAgreementOwnerWorkflow
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ChangeAgreementOwnerWorkflowDTO> ApprovePrintChangeAgreementOwnerWorkflowAsync(ChangeAgreementOwnerWorkflowDTO input, Guid? UserID);

  
        /// <summary>
        /// การลบผู้ที่เพิ่มชื่อ
        /// </summary>
        /// <param name="AgreementOwnerID"></param>
        /// <returns></returns>
        Task<AgreementOwnerDTO> DeleteAddNewAgreementOwnerWorkflowAsync(Guid? agreementOwnerID);
    }
}
