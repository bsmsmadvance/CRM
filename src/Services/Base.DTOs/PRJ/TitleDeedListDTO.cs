using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class TitleDeedListDTO : BaseDTO
    {
        /// <summary>
        /// ข้อมูลแปลง
        ///  Project/api/Projects/{projectID}/Units/DropdownList
        /// </summary>
        public UnitDropdownDTO Unit { get; set; }
        /// <summary>
        /// โครงการ
        /// Project/api/Projects/DropdownList
        /// </summary>
        public ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// เลขที่โฉนด
        /// </summary>
        public string TitledeedNo { get; set; }
        /// <summary>
        /// ชื่อแบบบ้าน
        ///  Project/api/Projects/{projectID}/Models/DropdownList
        /// </summary>
        public ModelDropdownDTO Model { get; set; }
        /// <summary>
        /// สำนักงานที่ดิน
        /// Master/api/LandOffices/DropdownList
        /// </summary>
        public MST.LandOfficeListDTO LandOffice { get; set; }
        /// <summary>
        /// พื้นที่โฉนด
        /// </summary>
        public double? TitledeedArea { get; set; }
        /// <summary>
        /// พื้นที่ใช้สอย
        /// </summary>
        public double? UsedArea { get; set; }
        /// <summary>
        /// สถานะโฉนด
        /// Master/api/MasterCenters?masterCenterGroupKey=LandStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO LandStatus { get; set; }
        /// <summary>
        /// วันที่เปลี่ยนสถานะโฉนด
        /// </summary>
        public DateTime? LandStatusDate { get; set; }
        /// <summary>
        /// เลขระวาง
        /// </summary>
        public string LandPortionNo { get; set; }
        /// <summary>
        /// เลขที่ดิน
        /// </summary>
        public string LandNo { get; set; }
        /// <summary>
        /// หน้าสำรวจ
        /// </summary>
        public string LandSurveyArea { get; set; }
        /// <summary>
        /// บ้านเลขที่
        /// </summary>
        public string HouseNo { get; set; }
        /// <summary>
        /// สถานะขอปลอด
        /// Master/api/MasterCenters?masterCenterGroupKey=PreferStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO PreferStatus { get; set; }

        public static TitleDeedListDTO CreateFromModel(TitledeedDetail model)
        {
            if (model != null)
            {
                var result = new TitleDeedListDTO();

                result.Id = model.ID;
                result.TitledeedNo = model.TitledeedNo;
                result.Project = ProjectDropdownDTO.CreateFromModel(model.Project);
                result.Unit = UnitDropdownDTO.CreateFromModel(model.Unit);
                result.Model = ModelDropdownDTO.CreateFromModel(model.Unit?.Model);
                result.TitledeedArea = model.TitledeedArea;
                result.LandOffice = MST.LandOfficeListDTO.CreateFromModel(model.Unit?.LandOffice);
                result.UsedArea = model.Unit?.UsedArea;
                result.LandPortionNo = model.LandPortionNo;
                result.LandStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.LandStatus);
                result.LandNo = model.LandNo;
                result.LandStatusDate = model.LandStatusDate;
                result.PreferStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PreferStatus);
                result.HouseNo = model.Unit?.HouseNo;
                result.Updated = model.Updated;
                result.UpdatedBy = model.UpdatedBy?.DisplayName;
                result.LandSurveyArea = model.LandSurveyArea;

                return result;
            }
            else
            {
                return null;
            }
        }
        public static TitleDeedListDTO CreateFromQueryResult(TitleDeedQueryResult model)
        {
            if (model != null)
            {
                var result = new TitleDeedListDTO();

                result.Id = model.Titledeed.ID;
                result.TitledeedNo = model.Titledeed.TitledeedNo;
                result.Project = ProjectDropdownDTO.CreateFromModel(model.Project);
                result.Unit = UnitDropdownDTO.CreateFromModel(model.Unit);
                result.Model = ModelDropdownDTO.CreateFromModel(model.Model);
                result.TitledeedArea = model.Titledeed.TitledeedArea;
                result.LandOffice = MST.LandOfficeListDTO.CreateFromModel(model.LandOffice);         
                result.UsedArea = model.Titledeed?.Unit?.UsedArea;
                result.LandPortionNo = model.Titledeed.LandPortionNo;
                result.LandStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.LandStatus);
                result.LandNo = model.Titledeed.LandNo;
                result.LandStatusDate = model.Titledeed.LandStatusDate;
                result.PreferStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.Titledeed.PreferStatus);
                result.HouseNo = model.Titledeed?.Unit?.HouseNo;
                result.Updated = model.Titledeed.Updated;
                result.UpdatedBy = model.Titledeed.UpdatedBy?.DisplayName;
                result.LandSurveyArea = model.Titledeed.LandSurveyArea;

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(TitleDeedListSortByParam sortByParam, ref IQueryable<TitleDeedQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case TitleDeedListSortBy.Unit:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case TitleDeedListSortBy.Project:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo);
                        else query = query.OrderByDescending(o => o.Project.ProjectNo);
                        break;
                    case TitleDeedListSortBy.TitledeedNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Titledeed.TitledeedNo);
                        else query = query.OrderByDescending(o => o.Titledeed.TitledeedNo);
                        break;
                    case TitleDeedListSortBy.Model:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Model.NameTH);
                        else query = query.OrderByDescending(o => o.Model.NameTH);
                        break;
                    case TitleDeedListSortBy.LandOffice:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LandOffice.NameTH);
                        else query = query.OrderByDescending(o => o.LandOffice.NameTH);
                        break;
                    case TitleDeedListSortBy.TitledeedArea:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Titledeed.TitledeedArea);
                        else query = query.OrderByDescending(o => o.Titledeed.TitledeedArea);
                        break;
                    case TitleDeedListSortBy.UsedArea:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Titledeed.Unit.UsedArea);
                        else query = query.OrderByDescending(o => o.Titledeed.Unit.UsedArea);
                        break;
                    case TitleDeedListSortBy.LandStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LandStatus.Name);
                        else query = query.OrderByDescending(o => o.LandStatus.Name);
                        break;
                    case TitleDeedListSortBy.LandStatusDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Titledeed.LandStatusDate);
                        else query = query.OrderByDescending(o => o.Titledeed.LandStatusDate);
                        break;
                    case TitleDeedListSortBy.LandPortionNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Titledeed.LandPortionNo);
                        else query = query.OrderByDescending(o => o.Titledeed.LandPortionNo);
                        break;
                    case TitleDeedListSortBy.LandNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Titledeed.LandNo);
                        else query = query.OrderByDescending(o => o.Titledeed.LandNo);
                        break;
                    case TitleDeedListSortBy.HouseNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Titledeed.Unit.HouseNo);
                        else query = query.OrderByDescending(o => o.Titledeed.Unit.HouseNo);
                        break;
                    case TitleDeedListSortBy.LandSurveyArea:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Titledeed.LandSurveyArea);
                        else query = query.OrderByDescending(o => o.Titledeed.LandSurveyArea);
                        break;
                    case TitleDeedListSortBy.PreferStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PreferStatus.Name);
                        else query = query.OrderByDescending(o => o.PreferStatus.Name);
                        break;
                    case TitleDeedListSortBy.UnitStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitStatus.Name);
                        else query = query.OrderByDescending(o => o.Unit.UnitStatus.Name);
                        break;
                    default:
                        query = query.OrderBy(o => o.Titledeed.TitledeedNo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Titledeed.TitledeedNo);
            }
        }
    }
    public class TitleDeedQueryResult
    {
        public Model Model { get; set; }
        public TitledeedDetail Titledeed { get; set; }
        public Project Project { get; set; }
        public Unit Unit { get; set; }
        public LandOffice LandOffice { get; set; }
        public MasterCenter LandStatus { get; set; }
        public MasterCenter PreferStatus { get; set; }
        public User UpdatedBy { get; set; }
    }

}
