using System;
using System.Threading.Tasks;
using Database.Models;
using models = Database.Models;

namespace Base.DTOs.SAL
{
    public class QuotationDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ใบเสนอราคา
        /// </summary>
        public string QuotationNo { get; set; }
        /// <summary>
        /// วันที่เสนอ
        /// </summary>
        public DateTime? IssueDate { get; set; }
        /// <summary>
        /// สถานะใบเสนอราคา
        /// </summary>
        public MST.MasterCenterDropdownDTO QuotationStatus { get; set; }
        /// <summary>
        /// โครงการ
        /// Project/api/Projects/DropdownList
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// แปลง
        /// </summary>
        public PRJ.UnitDTO Unit { get; set; }
        /// <summary>
        /// วันที่สร้าง
        /// </summary>
        public DateTime? Created { get; set; }
        /// <summary>
        /// ผู้สร้าง
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// สามารถบันทึกได้หรือไม่
        /// </summary>
        public bool? CanSave { get; set; }
        /// <summary>
        /// สามารถพิมพ์ได้หรือไม่
        /// </summary>
        public bool? CanPrint { get; set; }
        /// <summary>
        /// สามารถแปลงเป็นใบจองได้หรือไม่
        /// </summary>
        public bool? CanConvertToBooking { get; set; }
        /// <summary>
        /// สามารถลบได้หรือไม่
        /// </summary>
        public bool? CanDelete { get; set; }
        //TODO: [Palm] ทำให้รองรับการรับเหตุผลการขอ Min Price
        /// <summary>
        /// เหตุผลขออนุมัติ Min Price (กรณีติด Workflow)
        /// </summary>
        public MST.MasterCenterDropdownDTO MinPriceRequestReason { get; set; }
        /// <summary>
        /// เหตุผลขออนุมัติ Min Price อื่นๆ (กรณีติด Workflow)
        /// </summary>
        public string OtherMinPriceRequestReason { get; set; }

        public static QuotationDTO CreateFromModel(models.SAL.Quotation model)
        {
            if (model != null)
            {
                QuotationDTO result = new QuotationDTO()
                {
                    Id = model.ID,
                    QuotationNo = model.QuotationNo,
                    IssueDate = model.IssueDate,
                    QuotationStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.QuotationStatus),
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Created = model.Created,
                    CreatedBy = model.CreatedBy?.DisplayName,
                    Unit = PRJ.UnitDTO.CreateFromModel(model.Unit),
                    CanPrint = true,
                };

                if (model.Unit.UnitStatus.Key == UnitStatusKeys.Available)
                {
                    result.CanDelete = true;
                    result.CanSave = true;
                    result.CanConvertToBooking = true;
                }
                else
                {
                    result.CanDelete = false;
                    result.CanSave = false;
                    result.CanConvertToBooking = false;
                }
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
