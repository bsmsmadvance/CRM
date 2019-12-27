using Database.Models.SAL;
using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using models = Database.Models;
using Base.DTOs.MST;
using Database.Models;
using ErrorHandling;
using System.Reflection;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Base.DTOs.USR;

namespace Base.DTOs.SAL
{
    public class MortgageInfoDTO
    {
        [Description("ใบจอง")]
        public BookingDTO Booking { get; set; }

        [Description("สัญญา")]
        public AgreementDTO Agreement { get; set; }

        [Description("วันทีโอนกรรมสิทธิ์จริง")]
        public DateTime? ActualTransferDate { get; set; }

        [Description("ราคาในสัญญา")]
        public AgreementPriceListDTO AgreementPriceList { get; set; }

        [Description("ราคาในจอง")]
        public BookingPriceListDTO BookingPriceList { get; set; }

        [Description("ผู้ทำสัญญา")]
        public AgreementOwnerDTO AgreementOwner { get; set; }

        [Description("ผู้จอง")]
        public BookingOwnerDTO BookingOwner { get; set; }

        [Description("สถานะสัญญา")]
        public bool IsAgreementStage { get; set; }

        /// <summary>
        /// LCM
        /// GET Identity/api/Users?roleCodes=LCM&authorizeProjectIDs=7{projectID}
        /// </summary>
        public USR.UserListDTO LCMUser { get; set; }

        public async static Task<MortgageInfoDTO> CreateFromModelAsync(Guid unitId, DatabaseContext DB)
        {
            if (unitId != null)
            {
                var transfer = new Transfer();
                var agreement = new Agreement();
                var agreementOwner = new AgreementOwner();
                var booking = new Booking();
                var bookingOwner = new BookingOwner();
                var ProjectId = new Guid?();

                transfer = await DB.Transfers
                    .Include(o=>o.Agreement)
                    .ThenInclude(o=>o.Booking)
                    .Include(o => o.Project)
                    .Include(o => o.Unit)
                    .Where(o => o.Unit.ID == unitId && o.IsDeleted == false).FirstOrDefaultAsync() ?? new Transfer();

                if (transfer == null)
                { 
                    agreement = await DB.Agreements
                        .Include(o => o.Booking)
                        .Include(o => o.Project)
                        .Include(o => o.Unit)
                        .Where(o => o.Unit.ID == unitId && o.IsCancel == false).FirstOrDefaultAsync() ?? new models.SAL.Agreement();

                    if (agreement == null)
                    {
                        booking = await DB.Bookings
                            .Include(o => o.Project)
                            .Include(o => o.Unit)
                            .Where(o => o.Unit.ID == unitId && o.IsCancelled == false).FirstOrDefaultAsync() ?? new models.SAL.Booking();

                        if (booking != null)
                        {
                            ProjectId = booking.ProjectID;
                            bookingOwner = await DB.BookingOwners.Where(o => o.BookingID == booking.ID && o.IsMainOwner == true && o.IsDeleted == false).FirstOrDefaultAsync() ?? new models.SAL.BookingOwner();
                        }
                    }
                    else
                    {
                        ProjectId = agreement.ProjectID;
                        booking = agreement.Booking ?? new models.SAL.Booking();
                        agreementOwner = await DB.AgreementOwners.Where(o => o.AgreementID == agreement.ID && o.IsMainOwner == true && o.IsDeleted == false).FirstOrDefaultAsync() ?? new models.SAL.AgreementOwner();
                    }
                }
                else
                {
                    ProjectId = transfer.ProjectID;

                    agreement = transfer.Agreement ?? new models.SAL.Agreement();
                    agreementOwner = await DB.AgreementOwners.Where(o => o.AgreementID == agreement.ID && o.IsMainOwner == true && o.IsDeleted == false).FirstOrDefaultAsync() ?? new models.SAL.AgreementOwner();
                    booking = agreement.Booking ?? new models.SAL.Booking();
                }

                var result = new MortgageInfoDTO()
                {
                    Booking = await SAL.BookingDTO.CreateFromModelAsync(booking ?? new models.SAL.Booking(), DB),
                    Agreement = await SAL.AgreementDTO.CreateFromModelAsync(agreement ?? new models.SAL.Agreement(), null, DB),
                    ActualTransferDate = transfer?.ActualTransferDate
                };

                if (agreement != null)
                {
                    result.AgreementPriceList = await SAL.AgreementPriceListDTO.CreateFromModelAsync(agreement.ID, DB);   
                    result.AgreementOwner = await SAL.AgreementOwnerDTO.CreateFromModelAsync(agreementOwner, DB);
                    result.IsAgreementStage = true;
                }
                else
                {
                    result.BookingPriceList = await SAL.BookingPriceListDTO.CreateFromModelAsync(booking.ID, DB);
                    result.BookingOwner = await SAL.BookingOwnerDTO.CreateFromModelAsync(bookingOwner, DB);
                    result.IsAgreementStage = false;
                }

                var lcmRoleID = await DB.Roles.Where(o => o.Code == "LCM").Select(o => o.ID).FirstAsync();
                var lcmUsers = from r in DB.Users
                    .Where(o => o.UserAuthorizeProjects.Where(m => m.ProjectID == ProjectId).Any() &&
                             o.UserRoles.Where(n => n.RoleID == lcmRoleID).Any())
                              select r;

                result.LCMUser = await lcmUsers.Select(o => UserListDTO.CreateFromModel(o)).FirstOrDefaultAsync();

                return result ?? new MortgageInfoDTO();
            }
            else
            {
                return null;
            }
        }
    }
}
