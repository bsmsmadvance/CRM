using Base.DTOs.USR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.SAL
{
    public class UnitInfoListDTO : BaseDTO
    {
        /// <summary>
        /// แปลง
        /// </summary>
        public PRJ.UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// ชื่อจริง (ผู้จอง/ผู้ทำสัญญา)
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// นามสกุล (ผู้จอง/ผู้ทำสัญญา)
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// ใบจอง
        /// </summary>
        public SAL.BookingListDTO Booking { get; set; }

        /// <summary>
        /// ใบสัญญา
        /// </summary>
        public SAL.AgreementListDTO Agreement { get; set; }

        /// <summary>
        /// โปรโอน
        /// </summary>
        public SAL.TransferPromotionDTO TransferPromotion { get; set; }

        /// <summary>
        /// ธนาคารที่ขอสินเชื่อ
        /// </summary>
        public FIN.BankAccNameDTO Bank { get; set; }

        /// <summary>
        /// โอนกรรมสิทธิ์
        /// </summary>
        public TransferDTO Transfer { get; set; }

        /// <summary>
        /// LC ผู้รับผิดชอบ
        /// </summary>
        public UserDTO LCOwner { get; set; }


        public static async Task<UnitInfoListDTO> CreateFromQueryResultAsync(UnitInfoListQueryResult model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new UnitInfoListDTO()
                {
                    Id = model.Unit.ID,
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit),
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Unit.Project),
                    FirstName = model.BookingOwner?.FirstNameTH,
                    LastName = model.BookingOwner?.LastNameTH,
                    Booking = SAL.BookingListDTO.CreateFromModel(model.Booking, DB),
                    Agreement = new AgreementListDTO { AgreementNo = model.Agreement?.AgreementNo },
                    Transfer = new TransferDTO { TransferNo = model.Transfer?.TransferNo },
                    Bank = new FIN.BankAccNameDTO(), //TODO : Kim ธนาคารที่ขอโฉนด
                    LCOwner = UserDTO.CreateFromModel(model.LCOwner)
                };

                var TransferPromotion = await DB.TransferPromotions.Where(o => o.BookingID == model.Unit.ID).FirstOrDefaultAsync();
                result.TransferPromotion = await TransferPromotionDTO.CreateFromModelAsync(TransferPromotion, DB);

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(UnitInfoListSortByParam sortByParam, ref IQueryable<UnitInfoListQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case UnitInfoListSortBy.UnitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case UnitInfoListSortBy.HouseNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.HouseNo);
                        else query = query.OrderByDescending(o => o.Unit.HouseNo);
                        break;
                    case UnitInfoListSortBy.FullName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BookingOwner.FirstNameTH).ThenBy(o => o.BookingOwner.LastNameTH);
                        else query = query.OrderByDescending(o => o.BookingOwner.FirstNameTH).ThenByDescending(o => o.BookingOwner.LastNameTH);
                        break;
                    case UnitInfoListSortBy.BookingNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Booking.BookingNo);
                        else query = query.OrderByDescending(o => o.Booking.BookingNo);
                        break;
                    case UnitInfoListSortBy.ProjectNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo);
                        else query = query.OrderByDescending(o => o.Project.ProjectNo);
                        break;
                    case UnitInfoListSortBy.UnitStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UnitStatus.Name);
                        else query = query.OrderByDescending(o => o.UnitStatus.Name);
                        break;
                    default:
                        query = query.OrderBy(o => o.Unit.UnitNo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Unit.UnitNo);
            }
        }
    }

    public class UnitInfoListQueryResult
    {
        public models.PRJ.Unit Unit { get; set; }
        public models.PRJ.Project Project { get; set; }
        public models.MST.MasterCenter UnitStatus { get; set; }
        public models.SAL.Booking Booking { get; set; }
        public models.SAL.Agreement Agreement { get; set; }
        public models.SAL.Transfer Transfer { get; set; }

        public models.SAL.BookingOwner BookingOwner { get; set; }

        public models.USR.User LCOwner { get; set; }
    }
}
