using Database.Models;
using Database.Models.PRJ;
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
    public class UnitInfoDTO
    {
        /// <summary>
        /// ข้อมูลโครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// ข้อมูลแปลง
        /// </summary>
        public PRJ.UnitDTO Unit { get; set; }

        /// <summary>
        /// ข้อมูลใบจอง
        /// </summary>
        public BookingDTO Booking { get; set; }

        /// <summary>
        /// ข้อมูลสัญญา
        /// </summary>
        public AgreementDTO Agreement { get; set; }

        /// <summary>
        /// เจ้าของห้อง
        /// </summary>
        public List<UnitOwner> UnitOwnerList { get; set; }


        public async static Task<UnitInfoDTO> CreateFromQueryResultAsync(UnitInfoQueryResult model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new UnitInfoDTO()
                {
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Unit = PRJ.UnitDTO.CreateFromModel(model.Unit),
                    Booking = await BookingDTO.CreateFromModelAsync(model.Booking, DB),
                };

                result.UnitOwnerList = new List<UnitOwner>();

                if (model.Booking.BookingNo != null)
                {
                    var AgreementModel = await DB.AgreementOwners
                          .Include(o => o.ContactType)
                          .Include(o => o.ContactTitleTH)
                          .Include(o => o.ContactTitleEN)
                          .Include(o => o.National)
                          .Include(o => o.Gender)
                          .Include(o => o.Agreement)
                          .Where(o => o.Agreement.BookingID == model.Booking.ID)
                          .OrderBy(o => o.Order)
                          .ToListAsync();

                    if (AgreementModel.Any())
                    {
                        var AgreementOwner = AgreementModel.Select(o => AgreementOwnerDTO.CreateFromModelAsync(o, DB)).Select(o => o.Result).ToList();

                        if (AgreementOwner.Any())
                        {
                            var UnitOwner = new UnitOwner();
                            foreach (var item in AgreementOwner)
                            {
                                UnitOwner.FirstNameTH = item.FirstNameTH;
                                UnitOwner.LastNameTH = item.LastNameTH;

                                UnitOwner.FirstNameEN = item.FirstNameEN;
                                UnitOwner.LastNameEN = item.LastNameEN;

                                UnitOwner.National = item.National?.Name;
                                UnitOwner.ContactNo = item.ContactNo;

                                UnitOwner.ActiveDate = item.UpdateDate;
                                UnitOwner.PhoneNumber = (item.AgreementOwnerPhones.Where(o => o.IsMain == true).FirstOrDefault() ?? new AgreementOwnerPhoneDTO())?.PhoneNumber;
                                UnitOwner.Email = (item.AgreementOwnerEmails.Where(o => o.IsMain == true).FirstOrDefault() ?? new AgreementOwnerEmailDTO())?.Email;

                                result.UnitOwnerList.Add(UnitOwner);
                            }
                        }
                        else
                        {
                            var BookingModel = await DB.BookingOwners
                                                .Include(o => o.ContactType)
                                                .Include(o => o.ContactTitleTH)
                                                .Include(o => o.ContactTitleEN)
                                                .Include(o => o.National)
                                                .Include(o => o.Gender)
                                                .Include(o => o.Booking)
                                                .Where(o => o.BookingID == model.Booking.ID)
                                                .OrderBy(o => o.Order).ToListAsync();

                            var BookingOwner = BookingModel.Select(o => BookingOwnerDTO.CreateFromModelAsync(o, DB)).Select(o => o.Result).ToList();

                            if (BookingOwner.Any())
                            {
                                var UnitOwner = new UnitOwner();
                                foreach (var item in BookingOwner)
                                {
                                    UnitOwner.FirstNameTH = item.FirstNameTH;
                                    UnitOwner.LastNameTH = item.LastNameTH;

                                    UnitOwner.FirstNameEN = item.FirstNameEN;
                                    UnitOwner.LastNameEN = item.LastNameEN;

                                    UnitOwner.National = item.National?.Name;
                                    UnitOwner.ContactNo = item.ContactNo;

                                    UnitOwner.ActiveDate = item.UpdateDate;
                                    UnitOwner.PhoneNumber = (item.ContactPhones.Where(o => o.IsMain == true).FirstOrDefault() ?? new BookingOwnerPhoneDTO())?.PhoneNumber;
                                    UnitOwner.Email = (item.ContactEmails.Where(o => o.IsMain == true).FirstOrDefault() ?? new BookingOwnerEmailDTO())?.Email;

                                    result.UnitOwnerList.Add(UnitOwner);
                                }
                            }
                        }
                    }
                }

                return result;
            }
            else
            {
                return null;
            }
        }
    }

    public class UnitInfoQueryResult
    {
        public Unit Unit { get; set; }
        public Project Project { get; set; }
        public Booking Booking { get; set; }
    }

    public class UnitOwner
    {
        public string FirstNameTH { get; set; }
        public string LastNameTH { get; set; }

        public string FirstNameEN { get; set; }
        public string LastNameEN { get; set; }

        public string National { get; set; }

        public string ContactNo { get; set; }

        public DateTime? ActiveDate { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
