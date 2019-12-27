using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL

{
    public class AgreementDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่สัญญา
        /// </summary>
        public string AgreementNo { get; set; }
        /// <summary>
        /// แปลง
        /// </summary>
        public PRJ.UnitDTO Unit { get; set; }
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDTO Project { get; set; }
        /// <summary>
        /// ใบจอง
        /// </summary>
        public BookingDTO Booking { get; set; }
        /// <summary>
        /// สถานะสัญญา
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=AgreementStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO AgreementStatus { get; set; }
        /// <summary>
        /// วันที่ทำสัญญา
        /// </summary>
        public DateTime? ContractDate { get; set; }
        /// <summary>
        /// วันที่โอนกรรมสิทธิ์
        /// </summary>
        public DateTime? TransferOwnershipDate { get; set; }
        /// <summary>
        /// วันที่ลงนามสัญญา
        /// </summary>
        public DateTime? SignAgreementDate { get; set; }
        /// <summary>
        /// ผู้ขออนุมัติ
        /// </summary>
        public USR.UserListDTO SignContractRequestUser { get; set; }
        /// <summary>
        /// วันที่ขออนุมัติ Sign Contract ล่าสุด
        /// </summary>
        public DateTime? SignContractRequestDate { get; set; }
        /// <summary>
        /// วันที่อนุมัติ Sign Contract
        /// </summary>
        public DateTime? SignContractApprovedDate { get; set; }
        /// <summary>
        /// สถานะอนุมัติ Sign Contract (true = อนุมัติ/false = ไม่อนุมัติ)
        /// </summary>
        public bool? IsSignContractApproved { get; set; }
        /// <summary>
        /// สถานะอนุมัติพิมพ์สัญญา (true = อนุมัติ/false = ไม่อนุมัติ)
        /// </summary>
        public bool? IsPrintApproved { get; set; }
        /// <summary>
        /// วัน-เวลาที่อนุมัติพิมพ์สัญญา
        /// </summary>
        public DateTime? PrintApprovedDate { get; set; }
        /// <summary>
        /// ผู้อนุมัติพิมพ์สัญญา
        /// </summary>
        public USR.UserListDTO PrintApprovedBy { get; set; }
        /// <summary>
        /// ราคาพื้นที่ต่อหน่วย
        /// </summary>
        public decimal? AreaPricePerUnit { get; set; }
        /// <summary>
        /// พื้นที่เพิ่มลด ค่าบวก = พื้นที่โฉนด > พื้นที่ขาย
        /// </summary>
        public double? OffsetArea { get; set; }
        /// <summary>
        /// ราคาพื้นที่เพิ่มลด
        /// </summary>
        public decimal? OffsetAreaPrice { get; set; }
        /// <summary>
        /// สถานะการก่อสร้าง (สำหรับโครงการแนวสูง)
        /// </summary>
        public MST.MasterCenterDropdownDTO HighRiseConstructionStatus { get; set; }
        /// <summary>
        /// บริษัทจ่ายงวดสุดท้าย (true = จ่าย)
        /// </summary>
        public bool IsSellerPayLastDownInstallment { get; set; }
        /// <summary>
        /// ไฟล์แนบ
        /// </summary>
        public FileDTO Files { get; set; }
        /// <summary>
        /// ประเภทพนักงานปิดการขาย
        /// GET Master/api/MasterCenters?MasterCenterGroupKey=SaleOfficerType
        /// </summary>
        public MST.MasterCenterDropdownDTO SaleOfficerType { get; set; }
        /// <summary>
        /// รหัส Sale
        /// GET Identity/api/Users?roleCodes=LC&authorizeProjectIDs=7{projectID}
        /// </summary>
        public USR.UserListDTO SaleUser { get; set; }
        /// <summary>
        /// รหัส Agent
        /// GET Master/api/Agents/DropdownList
        /// </summary>
        public MST.AgentDropdownDTO Agent { get; set; }
        /// <summary>
        /// รหัสพนักงาน Agent
        /// GET Master/api/AgentEmployees/DropdownList?agentID={agentID}
        /// </summary>
        public MST.AgentEmployeeDropdownDTO AgentEmployee { get; set; }
        /// <summary>
        /// รหัส Sale ประจำโครงการ
        /// GET Identity/api/Users?roleCodes=LC&authorizeProjectIDs=7{projectID}
        /// </summary>
        public USR.UserListDTO ProjectSaleUser { get; set; }

        public static async Task<AgreementDTO> CreateFromModelAsync(Agreement model, FileHelper fileHelper, DatabaseContext DB)
        {
            if (model != null)
            {
                if(model.Unit == null)
                {
                    model.Unit = DB.Units.Where(e => e.ID == model.UnitID).FirstOrDefault() ?? new Database.Models.PRJ.Unit();
                }

                model.Unit.TitledeedDetails = await DB.TitledeedDetails.Where(o => o.UnitID == model.UnitID).ToListAsync() ?? new List<Database.Models.PRJ.TitledeedDetail>();     

                AgreementDTO result = new AgreementDTO()
                {
                    Id = model.ID,
                    AgreementNo = model.AgreementNo,
                    Unit = PRJ.UnitDTO.CreateFromModel(model.Unit),
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    Booking = await BookingDTO.CreateFromModelAsync(model.Booking,DB),
                    AgreementStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.AgreementStatus),
                    ContractDate = model.ContractDate,
                    TransferOwnershipDate = model.TransferOwnershipDate,
                    SignAgreementDate = model.SignAgreementDate,
                    SignContractRequestUser = USR.UserListDTO.CreateFromModel(model.SignContractRequestUser),
                    SignContractRequestDate = model.SignContractRequestDate,
                    SignContractApprovedDate = model.SignContractApprovedDate,
                    IsSignContractApproved = model.IsSignContractApproved,
                    IsPrintApproved = model.IsPrintApproved,
                    PrintApprovedDate = model.PrintApprovedDate,
                    PrintApprovedBy = USR.UserListDTO.CreateFromModel(model.PrintApprovedBy),
                    AreaPricePerUnit = model.AreaPricePerUnit,
                    OffsetArea = model.OffsetArea,
                    HighRiseConstructionStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.HighRiseConstructionStatus),
                    IsSellerPayLastDownInstallment = model.IsSellerPayLastDownInstallment,
                    SaleOfficerType = MST.MasterCenterDropdownDTO.CreateFromModel(model.Booking.SaleOfficerType),
                    SaleUser = USR.UserListDTO.CreateFromModel(model.Booking.SaleUser),
                    Agent = MST.AgentDropdownDTO.CreateFromModel(model.Booking.Agent),
                    AgentEmployee = MST.AgentEmployeeDropdownDTO.CreateFromModel(model.Booking.AgentEmployee),
                    ProjectSaleUser = USR.UserListDTO.CreateFromModel(model.Booking.ProjectSaleUser),
                };

            

                result.Unit.TitleDeed = PRJ.TitleDeedDTO.CreateFromModel(model.Unit.TitledeedDetails?.FirstOrDefault());
                result.Unit.AddOnArea = (result.Unit.TitleDeed?.TitledeedArea ?? 0.00) - (result.Unit.SaleArea ?? 0.00);

                //result.Files = await FileDTO.CreateFromFileNameAsync(model., fileHelper);

                return result;
            }
            else
            {
                return null;
            }
        }

        public static async Task<AgreementDTO> CreateFromModelByBookingAsync(Booking model, DatabaseContext DB)
        {
            if (model != null)
            {
                var agreementStatus = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "AgreementStatus" && o.Key == "1").FirstOrDefaultAsync();

                AgreementDTO result = new AgreementDTO()
                {
                    AgreementNo = "",
                    Unit = PRJ.UnitDTO.CreateFromModel(model.Unit),
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    Booking = await BookingDTO.CreateFromModelAsync(model,DB),
                    AgreementStatus = MST.MasterCenterDropdownDTO.CreateFromModel(agreementStatus)

                    //ContractDate = model.ContractDate,
                    //TransferOwnershipDate = model.TransferOwnershipDate,
                    //SignAgreementDate = model.SignAgreementDate,
                    //SignContractRequestUser = USR.UserListDTO.CreateFromModel(model.SignContractRequestUser),
                    //SignContractRequestDate = model.SignContractRequestDate,
                    //SignContractApprovedDate = model.SignContractApprovedDate,
                    //IsSignContractApproved = model.IsSignContractApproved,
                    //IsPrintApproved = model.IsPrintApproved,
                    //PrintApprovedDate = model.PrintApprovedDate,
                    //PrintApprovedBy = USR.UserListDTO.CreateFromModel(model.PrintApprovedBy),
                    //AreaPricePerUnit = model.AreaPricePerUnit,
                    //OffsetArea = model.OffsetArea,
                    //HighRiseConstructionStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.HighRiseConstructionStatus),
                    //IsSellerPayLastDownInstallment = model.IsSellerPayLastDownInstallment
                };                                     

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
