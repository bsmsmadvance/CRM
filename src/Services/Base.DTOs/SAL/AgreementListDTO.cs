using Base.DTOs.MST;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class AgreementListDTO : BaseDTO
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// เลขที่สัญญา
        /// </summary>
        public string AgreementNo { get; set; }
        /// <summary>
        /// แปลง
        ///  Project/api/Projects/{projectID}/Units/DropdownList
        /// </summary>
        public PRJ.UnitDropdownDTO Unit { get; set; }
        /// <summary>
        /// ผู้ทำสัญญา
        ///  Sale/api/Bookings/{bookingID}/AgreementOwners/DropdownList
        /// </summary>
        public AgreementOwnerDropdownDTO AgreementOwner { get; set; }
        /// <summary>
        /// ใบจอง
        /// </summary>
        public BookingDropdownDTO Booking { get; set; }
        /// <summary>
        /// วันที่ทำสัญญา
        /// </summary>
        public DateTime? ContractDate { get; set; }
        /// <summary>
        /// วันที่โอนกรรมสิทธิ์
        /// </summary>
        public DateTime? TransferOwnershipDate { get; set; }
        /// <summary>
        /// สถานะสัญญา
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=AgreementStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO AgreementStatus { get; set; }
        /// <summary>
        /// สถานะตั้งเรื่อง
        /// </summary>
        public ChangeAgreementOwnerWorkflowDTO ChangeAgreementOwnerWorkflow { get; set; }

        public static AgreementListDTO CreateFromQueryResult(AgreementListQueryResult model)
        {
            if (model != null)
            {
                var result = new AgreementListDTO()
                {
                    Id = model.Agreement.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Agreement.Project),
                    AgreementNo = model.Agreement.AgreementNo,
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Agreement.Unit),
                    AgreementOwner = AgreementOwnerDropdownDTO.CreateFromModel(model.AgreementOwner),
                    Booking = BookingDropdownDTO.CreateFromModel(model.Agreement.Booking),
                    ContractDate = model.Agreement.ContractDate,
                    TransferOwnershipDate = model.Agreement.TransferOwnershipDate,
                    AgreementStatus = MasterCenterDropdownDTO.CreateFromModel(model.Agreement.AgreementStatus),
                    ChangeAgreementOwnerWorkflow = ChangeAgreementOwnerWorkflowDTO.CreateFromModelAsync(model.ChangeAgreementOwnerWorkflow)
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(AgreementListSortByParam sortByParam, ref IQueryable<AgreementListQueryResult> query)
        {
            IOrderedQueryable<AgreementListQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case AgreementListSortBy.UnitNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenBy(o => o.Agreement.Unit.UnitNo);
                        else orderQuery = query.OrderByDescending(o => o.Agreement.Unit.UnitNo);
                        break;
                    case AgreementListSortBy.FullName:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenBy(o => o.AgreementOwner.FirstNameTH);
                        else orderQuery = query.OrderByDescending(o => o.AgreementOwner.FirstNameTH);
                        break;
                    case AgreementListSortBy.BookingNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenBy(o => o.Agreement.Booking.BookingNo);
                        else orderQuery = query.OrderByDescending(o => o.Agreement.Booking.BookingNo);
                        break;
                    case AgreementListSortBy.AgreementNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenBy(o => o.Agreement.AgreementNo);
                        else orderQuery = query.OrderByDescending(o => o.Agreement.AgreementNo);
                        break;
                    case AgreementListSortBy.AgreementStatus:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenBy(o => o.Agreement.AgreementStatus.Key);
                        else orderQuery = query.OrderByDescending(o => o.Agreement.AgreementStatus.Key);
                        break;
                    case AgreementListSortBy.ChangeAgreementOwnerType:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenBy(o => o.ChangeAgreementOwnerWorkflow.ChangeAgreementOwnerType.Key);
                        else orderQuery = query.OrderByDescending(o => o.ChangeAgreementOwnerWorkflow.ChangeAgreementOwnerType.Key);
                        break;
                    default:
                        orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenBy(o => o.Agreement.Unit.UnitNo);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.Agreement.ProjectID).ThenBy(o => o.Agreement.Unit.UnitNo);
            }

            orderQuery.ThenBy(o => o.Agreement.ID);
            query = orderQuery;
        }

        public class AgreementListQueryResult
        {
            public Agreement Agreement { get; set; }
            public AgreementOwner AgreementOwner { get; set; }
            public ChangeAgreementOwnerWorkflow ChangeAgreementOwnerWorkflow { get; set; }
        }
    }
}
