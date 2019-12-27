using Database.Models;
using Database.Models.SAL;
using Database.Models.USR;
using Database.Models.MST;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using models = Database.Models;
using Base.DTOs.MST;
using Base.DTOs.USR;

namespace Base.DTOs.SAL
{
    public class ChangeAgreementOwnerWorkflowDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่สํญญา
        /// </summary>
        public AgreementDTO Agreement { get; set; }

        /// <summary>
        /// วันที่นัดทำรายการ
        /// </summary>
        public DateTime? AppointmentDate { get; set; }

        /// <summary>
        /// วันที่นัดโอนใหม่
        /// </summary>
        public DateTime? NewTransferOwnershipDate { get; set; }
        
        /// <summary>
        /// เบี้ยปรับ/ค่าธรรมเนียม
        /// </summary>
        public decimal Fee { get; set; }
        
        /// <summary>
        /// ชนิดของการเปลี่ยนแปลงชื่อผู้ทำสัญญา
        /// </summary>
        public MasterCenterDropdownDTO ChangeAgreementOwnerType { get; set; }
        
        /// <summary>
        /// ตำแหน่งของผู้อนุมัติตั้งเรื่อง
        /// </summary>
        public Role RequestApproverRole { get; set; }
        /// <summary>
        /// ผู้อนุมัติตั้งเรื่อง
        /// </summary>
        public UserListDTO RequestApproverUser { get; set; }
        /// <summary>
        /// วันที่อนุมัติตั้งเรื่อง
        /// </summary>
        public DateTime? RequestApprovedDate { get; set; }
        /// <summary>
        /// สถานะอนุมัติตั้งเรื่อง
        /// </summary>
        public bool? IsRequestApproved { get; set; }
        /// <summary>
        /// เหตุผลที่ไม่อนุมัติตั้งเรื่อง
        /// </summary>
        public string RequestRejectComment { get; set; }

        /// <summary>
        /// ตำแหน่งของผู้อนุมัติ
        /// </summary>
        public Role ApproverRole { get; set; }
        /// <summary>
        /// ผู้อนุมัติ
        /// </summary>
        public UserListDTO ApproverUser { get; set; }
        /// <summary>
        /// วันที่อนุมัติ
        /// </summary>
        public DateTime? ApprovedDate { get; set; }
        /// <summary>
        /// สถานะอนุมัติ
        /// </summary>
        public bool? IsApproved { get; set; }
        /// <summary>
        /// เหตุผลที่ไม่อนุมัติ
        /// </summary>
        public string RejectComment { get; set; }
        /// <summary>
        /// สถานะอนุมัติพิมพ์เอกสาร
        /// </summary>
        public bool? IsPrintApproved { get; set; }
        /// <summary>
        /// วันที่อนุมัติพิมพ์เอกสาร
        /// </summary>
        public DateTime? PrintApprovedDate { get; set; }
        /// <summary>
        /// ผู้อนุมัติพิมพ์เอกสาร
        /// </summary>
        public UserListDTO PrintApprovedBy { get; set; }

        /// <summary>
        /// เหตุผลที่ไม่เก็บเบี้ยปรับ/ค่าธรรมเนียม
        /// </summary>
        public string NoFeeComment { get; set; }

        /// <summary>
        /// รหัสพนักงานตั้งเรื่อง
        /// </summary>
        public USR.UserListDTO SaleUser { get; set; }

        /// <summary>
        /// สถานะของการเปลี่ยนแปลงชื่อผู้ทำสัญญา
        /// </summary>
        public MasterCenterDropdownDTO ChangeAgreementOwnerStatus { get; set; }

        public static ChangeAgreementOwnerWorkflowDTO CreateFromModelAsync(ChangeAgreementOwnerWorkflow model)
        {
            if (model != null)
            {
                var result = new ChangeAgreementOwnerWorkflowDTO()
                {
                    Id = model.ID,
                    AppointmentDate = model.AppointmentDate,
               
                    NewTransferOwnershipDate = model.NewTransferOwnershipDate,
                    Fee = model.Fee,
                    ChangeAgreementOwnerType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ChangeAgreementOwnerType),
                    RequestApproverRole = model.RequestApproverRole,
                    RequestApproverUser = USR.UserListDTO.CreateFromModel(model.RequestApproverUser),
                    RequestApprovedDate = model.RequestApprovedDate,
                    IsRequestApproved = model.IsRequestApproved,
                    RequestRejectComment = model.RequestRejectComment,
                    ApproverRole = model.ApproverRole,
                    ApproverUser = USR.UserListDTO.CreateFromModel(model.ApproverUser),
                    ApprovedDate = model.ApprovedDate,
                    IsApproved = model.IsApproved,
                    RejectComment = model.RejectComment,
                    NoFeeComment = model.NoFeeComment,
                    SaleUser = USR.UserListDTO.CreateFromModel(model.SaleUser),
                
                    ChangeAgreementOwnerStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.ChangeAgreementOwnerStatus)

                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (this.ChangeAgreementOwnerType == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(ChangeAgreementOwnerWorkflowDTO.ChangeAgreementOwnerType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref ChangeAgreementOwnerWorkflow model)
        {
            model.AppointmentDate = this.AppointmentDate;
            model.NewTransferOwnershipDate = this.NewTransferOwnershipDate;
            model.Fee = this.Fee;
            //model.ChangeAgreementOwnerType = this.ChangeAgreementOwnerType;
            model.ChangeAgreementOwnerTypeMasterCenterID = this.ChangeAgreementOwnerType?.Id;
            model.RequestApproverRoleID = this.RequestApproverRole?.ID;
            model.RequestApproverUserID = this.RequestApproverUser?.Id;
            model.RequestApprovedDate = this.RequestApprovedDate;
            model.IsRequestApproved = this.IsRequestApproved;
            model.RequestRejectComment = this.RequestRejectComment;
            model.ApproverRoleID = this.ApproverRole?.ID;
            model.ApproverUserID = this.ApproverUser?.Id;
            model.ApprovedDate = this.ApprovedDate;
            model.IsApproved = this.IsApproved;
            model.RejectComment = this.RejectComment;
            model.AgreementID = this.Agreement.Id;
            model.IsPrintApproved = this.IsPrintApproved;
            model.NoFeeComment = this.NoFeeComment;
            model.SaleUserID = this.SaleUser.Id;
            //model.ChangeAgreementOwnerStatusMasterCenterID = this.ChangeAgreementOwnerStatus?.Id;
        }
    }
}
