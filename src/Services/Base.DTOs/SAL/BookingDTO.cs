using Base.DTOs.PRJ;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// ใบจอง
    /// Model: Booking
    /// </summary>
    public class BookingDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ใบเสนอราคา
        /// </summary>
        public string QuotationNo { get; set; }
        /// <summary>
        /// เลขที่ใบจอง
        /// </summary>
        public string BookingNo { get; set; }
        /// <summary>
        /// สถานะใบจอง
        /// </summary>
        public MST.MasterCenterDropdownDTO BookingStatus { get; set; }
        /// <summary>
        /// แปลง
        /// </summary>
        public PRJ.UnitDropdownDTO Unit { get; set; }
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// แบบบ้าน
        /// </summary>
        public PRJ.ModelDropdownDTO Model { get; set; }
        /// <summary>
        /// พื้นที่ขาย
        /// </summary>
        public double? SaleArea { get; set; }
        /// <summary>
        /// วันที่จอง
        /// </summary>
        public DateTime? BookingDate { get; set; }
        /// <summary>
        /// วันที่นัดทำสัญญา
        /// </summary>
        public DateTime? ContractDueDate { get; set; }
        /// <summary>
        /// วันที่อนุมัติ
        /// </summary>
        public DateTime? ApproveDate { get; set; }
        /// <summary>
        /// วันที่ทำสัญญา
        /// </summary>
        public DateTime? ContractDate { get; set; }
        /// <summary>
        /// วันที่โอนกรรมสิทธิ์
        /// </summary>
        public DateTime? TransferOwnershipDate { get; set; }
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
        /// <summary>
        /// สร้างใบจองจากระบบไหน
        /// </summary>
        public MST.MasterCenterDropdownDTO CreateBookingFrom { get; set; }
        /// <summary>
        /// สถานะการชำระเงิน (null = ยังไม่ได้ชำระ/ false = ชำระเงินจองไม่ครบ / true = ชำระเงินจองครบแล้ว)
        /// </summary>
        public bool? IsPaid { get; set; }
        /// <summary>
        /// Min Price Workflow
        /// </summary>
        public MinPriceBudgetWorkflowDTO MinPriceBudgetWorkflow { get; set; }
        /// <summary>
        /// บันทึก
        /// </summary>
        public bool? CanSave { get; set; }
        /// <summary>
        /// ชำระเงิน
        /// </summary>
        public bool? CanPay { get; set; }
        /// <summary>
        /// พิมพ์
        /// </summary>
        public bool? CanPrint { get; set; }
        /// <summary>
        /// เลือกผู้ทำสัญญา
        /// </summary>
        public bool? CanManageAgreementOwner { get; set; }
        /// <summary>
        /// ทำแบบสอบถาม
        /// </summary>
        public bool? CanQuestionnaire { get; set; }
        /// <summary>
        /// ย้ายแปลง
        /// </summary>
        public bool? CanChangeUnit { get; set; }
        /// <summary>
        /// ยกเลิกการจอง
        /// </summary>
        public bool? CanCancel { get; set; }
        /// <summary>
        /// เหตุผลขออนุมัติ Min Price (กรณีติด Workflow)
        /// </summary>
        public MST.MasterCenterDropdownDTO MinPriceRequestReason { get; set; }
        /// <summary>
        /// เหตุผลขออนุมัติ Min Price อื่นๆ (กรณีติด Workflow)
        /// </summary>
        public string OtherMinPriceRequestReason { get; set; }
        /// <summary>
        /// เงินที่ชำระแล้ว
        /// </summary>
        public decimal? TotalPayAmount { get; set; }


        /// <summary>
        /// สถานะการขอสินเชื่อ
        /// </summary>
        public MST.MasterCenterDropdownDTO CreditBankingType { get; set; }

        public async static Task<BookingDTO> CreateFromModelAsync(models.SAL.Booking model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new BookingDTO()
                {
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    QuotationNo = model.Quotation?.QuotationNo,
                    BookingNo = model.BookingNo,
                    BookingStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.BookingStatus),
                    Unit = await PRJ.UnitDropdownDTO.CreateFromModelAsync(model.Unit, DB),
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Model = PRJ.ModelDropdownDTO.CreateFromModel(model.Model),
                    SaleArea = model.SaleArea,
                    BookingDate = model.BookingDate,
                    ContractDueDate = model.ContractDueDate,
                    ApproveDate = model.ApproveDate,
                    ContractDate = model.ContractDueDate,
                    SaleOfficerType = MST.MasterCenterDropdownDTO.CreateFromModel(model.SaleOfficerType),
                    SaleUser = USR.UserListDTO.CreateFromModel(model.SaleUser),
                    Agent = MST.AgentDropdownDTO.CreateFromModel(model.Agent),
                    AgentEmployee = MST.AgentEmployeeDropdownDTO.CreateFromModel(model.AgentEmployee),
                    ProjectSaleUser = USR.UserListDTO.CreateFromModel(model.ProjectSaleUser),
                    CreateBookingFrom = MST.MasterCenterDropdownDTO.CreateFromModel(model.CreateBookingFrom),
                    IsPaid = model.IsPaid,
                    TransferOwnershipDate = model.TransferOwnershipDate,
                    CanSave = true,
                    CanQuestionnaire = false,
                    CreditBankingType = MST.MasterCenterDropdownDTO.CreateFromModel(model.CreditBankingType)
                };

                #region CanPay//CanPrint//CanManageAgreementOwner
                var waitingForApprovePriceListMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus
                                                                                   && o.Key == BookingStatusKeys.WaitingForApprovePriceList)
                                                                         .Select(o => o.ID)
                                                                         .FirstAsync();
                var waitingForApproveMinPriceMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus
                                                                    && o.Key == BookingStatusKeys.WaitingForApproveMinPrice)
                                                          .Select(o => o.ID)
                                                          .FirstAsync();

                if (!string.IsNullOrEmpty(model.BookingNo)
                    && (model.BookingStatusMasterCenterID != waitingForApprovePriceListMasterCenterID
                            && model.BookingStatusMasterCenterID != waitingForApproveMinPriceMasterCenterID)
                    )
                {
                    result.CanPay = true;
                    result.CanPrint = true;
                    result.CanManageAgreementOwner = true;
                }
                else
                {
                    result.CanPay = false;
                    result.CanPrint = false;
                    result.CanManageAgreementOwner = true;
                }
                #endregion

                #region CanCancel
                var payments = await DB.Payments.Where(o => o.BookingID == model.ID).ToListAsync();
                if (!string.IsNullOrEmpty(model.BookingNo)
                   && payments.Count() > 0
                   && (model.BookingStatusMasterCenterID != waitingForApprovePriceListMasterCenterID
                           && model.BookingStatusMasterCenterID != waitingForApproveMinPriceMasterCenterID)
                   )
                {
                    result.CanCancel = true;
                }
                else
                {
                    result.CanCancel = false;
                }
                #endregion

                #region CanChangeUnit
                if (!string.IsNullOrEmpty(model.BookingNo))
                {
                    result.CanChangeUnit = true;
                }
                else
                {
                    result.CanChangeUnit = false;
                }
                #endregion

                var workflow = await DB.MinPriceBudgetWorkflows
                .Include(o => o.Project)
                .Include(o => o.MinPriceBudgetWorkflowStage)
                .Include(o => o.MinPriceWorkflowType)
                .Include(o => o.BudgetPromotionType)
                .Include(o => o.Booking)
                .ThenInclude(o => o.Unit)
                .Where(o => o.BookingID == model.ID)
                .OrderByDescending(o => o.Created).FirstOrDefaultAsync();
                if (workflow != null)
                {
                    result.MinPriceBudgetWorkflow = MinPriceBudgetWorkflowDTO.CreateFromModel(workflow);
                }

                result.TotalPayAmount =await (from o in DB.PaymentMethods where o.Payment.BookingID == model.ID select o).SumAsync(o => o.PayAmount);
                
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
