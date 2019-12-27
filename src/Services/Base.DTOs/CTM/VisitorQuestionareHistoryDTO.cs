using System;
namespace Base.DTOs.CTM
{
    public class VisitorQuestionnaireHistoryDTO
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// สถานะทำแบบสอบถาม
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=StatusQuestionaire
        /// </summary>
        public MST.MasterCenterDropdownDTO StatusQuestionaire { get; set; }
        /// <summary>
        /// จำนวนคำถามที่ตอบคำถามแล้ว
        /// </summary>
        public int? AnsweredQuestionaire { get; set; }
        /// <summary>
        /// จำนวนคำถามทั้งหมด
        /// </summary>
        public int? TotalQuestionaire { get; set; }
        /// <summary>
        /// วันที่ทำรายการ
        /// </summary>
        public DateTime? QuestionaireDate { get; set; }
    }
}
