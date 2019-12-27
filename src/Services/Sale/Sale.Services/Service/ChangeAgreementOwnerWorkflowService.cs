using Base.DTOs.SAL;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using Database.Models.USR;
using Microsoft.EntityFrameworkCore;
using Sale.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sale.Services.Service
{
    public class ChangeAgreementOwnerWorkflowService : IChangeAgreementOwnerWorkflowService
    {
        private readonly DatabaseContext DB;

        public ChangeAgreementOwnerWorkflowService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<ChangeAgreementOwnerWorkflowDTO> CreateChangeAgreementOwnerWorkflowAsync(ChangeAgreementOwnerWorkflowDTO input, List<AgreementOwnerDTO> ListAgreementOwner)
        {
            await input.ValidateAsync(DB);

            ChangeAgreementOwnerWorkflow model = new ChangeAgreementOwnerWorkflow();
            input.ToModel(ref model);
            model.ChangeAgreementOwnerStatusMasterCenterID = ChangeAgreementOwnerStatusKey.WaitForLCMApprove;

            await DB.ChangeAgreementOwnerWorkflows.AddAsync(model);
            await DB.SaveChangesAsync();

            foreach (var item in ListAgreementOwner)
            {
                AgreementOwner agreementOwner = await DB.AgreementOwners.Where(o => o.ID == item.Id).FirstOrDefaultAsync();
                var changeAgreementOwnerType = await DB.MasterCenters.Where(o => o.ID == model.ChangeAgreementOwnerTypeMasterCenterID).FirstOrDefaultAsync();

                if (agreementOwner == null)
                {
                    //insert agreementOwner
                    agreementOwner = new AgreementOwner();
                    item.ToModel(ref agreementOwner);

                    agreementOwner.IsActive = false;
                    agreementOwner.IsDeleted = false;
                    //agreementOwner.ContactNo = item.ContactNo/;
                    //agreementOwner.AgreementID = item.Agreement?.Id;
                    //agreementOwner.BirthDate = item.BirthDate;
                    //agreementOwner.CitizenExpireDate = item.CitizenExpireDate;
                    //agreementOwner.CitizenIdentityNo = item.CitizenIdentityNo;
                    //agreementOwner.ContactFirstName = item.ContactFirstName;
                    //agreementOwner.ContactLastname = item.ContactLastname;
                    //agreementOwner.ContactTitleENMasterCenterID = item.ContactTitleEN.Id;
                    //agreementOwner.ContactTitleTHMasterCenterID = item.ContactTitleTH.Id;
                    //agreementOwner.ContactTypeMasterCenterID = item.ContactType.Id;
                    //agreementOwner.FirstNameEN = item.FirstNameEN;
                    //agreementOwner.FirstNameTH = item.FirstNameTH;
                    //agreementOwner.FromContactID = item.FromContactID;
                    //agreementOwner.GenderMasterCenterID = item.Gender.Id;
                    //agreementOwner.IsMainOwner = item.IsMainOwner;
                    //agreementOwner.IsThaiNationality = item.IsThaiNationality;
                    //agreementOwner.IsVIP = item.IsVIP;
                    if (changeAgreementOwnerType.Key == "1")
                    {
                        agreementOwner.IsAddNewOwner = true;
                    }
                    else if (changeAgreementOwnerType.Key == "2")
                    {
                        agreementOwner.IsCancelledOwner = true;
                        agreementOwner.IsAddNewOwner = false;
                    }
                    else if (changeAgreementOwnerType.Key == "3")
                    {
                        if (item.IsSelected == true)
                        { 
                        agreementOwner.IsTransferOwner = true;
                        agreementOwner.IsCancelledOwner = true;
                        agreementOwner.IsAddNewOwner = false;
                        }
                        else
                        {
                            agreementOwner.IsTransferOwner = true;
                            agreementOwner.IsCancelledOwner = false;
                            agreementOwner.IsAddNewOwner = true;
                        }
                    }

                    await DB.AgreementOwners.AddAsync(agreementOwner);
                    await DB.SaveChangesAsync();

                    #region Phone
                    List<AgreementOwnerPhone> phones = new List<AgreementOwnerPhone>();
                    foreach (var phone in item.AgreementOwnerPhones)
                    {
                        var phoneModel = new AgreementOwnerPhone()
                        {
                            AgreementOwnerID = model.ID,
                            IsMain = phone.IsMain,
                            PhoneNumber = phone.PhoneNumber,
                            PhoneNumberExt = phone.PhoneNumberExt,
                            PhoneTypeMasterCenterID = phone.PhoneType?.Id,
                            CountryCode = phone.CountryCode,
                            FromContactPhoneID = phone.FromContactPhoneID
                        };
                        phones.Add(phoneModel);
                    }

                    if (phones.Count > 0)
                    {
                        var isMain = phones.Where(o => o.IsMain == true).Any();
                        if (isMain == false)
                        {
                            phones.First(o => o.IsMain = true);
                        }

                        await DB.AgreementOwnerPhones.AddRangeAsync(phones);
                    }

                    #endregion

                    #region Email
                    List<AgreementOwnerEmail> emails = new List<AgreementOwnerEmail>();
                    foreach (var email in item.AgreementOwnerEmails)
                    {
                        var emailModel = new AgreementOwnerEmail()
                        {
                            AgreementOwnerID = model.ID,
                            IsMain = email.IsMain,
                            Email = email.Email,
                            FromContactEmailID = email.FromContactEmailID
                        };
                        emails.Add(emailModel);
                    }

                    if (emails.Count > 0)
                    {
                        var isMain = emails.Where(o => o.IsMain == true).Any();
                        if (isMain == false)
                        {
                            emails.First(o => o.IsMain = true);
                        }

                        await DB.AgreementOwnerEmails.AddRangeAsync(emails);
                    }
                    #endregion

                    #region Address
                    List<AgreementOwnerAddress> addresses = new List<AgreementOwnerAddress>();
                    foreach (var address in item.AgreementOwnerAddresses)
                    {
                        // ต้องเพิ่ม validate require address
                        await address.ValidateAsync(DB);

                        var addressModel = new AgreementOwnerAddress();
                        address.ToModel(ref addressModel);
                        addressModel.AgreementOwnerID = model.ID;
                        addresses.Add(addressModel);
                    }

                    if (addresses.Count > 0)
                        await DB.AgreementOwnerAddresses.AddRangeAsync(addresses);
                    #endregion

                    //insert ChangeAgreementOwnerWorkflowDetail
                    ChangeAgreementOwnerWorkflowDetail detail = new ChangeAgreementOwnerWorkflowDetail();
                    detail.ChangeAgreementOwnerWorkflowID = model.ID;
                    detail.AgreementOwnerID = agreementOwner.ID;
                    detail.ChangeAgreementOwnerInType = item.ChangeAgreementOwnerInType.Value;

                    await DB.ChangeAgreementOwnerWorkflowDetails.AddAsync(detail);
                    await DB.SaveChangesAsync();
                }
                else
                {
                    if (model.ChangeAgreementOwnerType.Key == "2")
                    {
                        agreementOwner.IsCancelledOwner = true;
                        agreementOwner.IsAddNewOwner = false;
                    }

                    DB.AgreementOwners.Update(agreementOwner);
                    await DB.SaveChangesAsync();

                    //insert ChangeAgreementOwnerWorkflowDetail
                    ChangeAgreementOwnerWorkflowDetail detail = new ChangeAgreementOwnerWorkflowDetail();
                    detail.ChangeAgreementOwnerWorkflowID = model.ID;
                    detail.AgreementOwnerID = agreementOwner.ID;
                    detail.ChangeAgreementOwnerInType = item.ChangeAgreementOwnerInType.Value;

                    await DB.ChangeAgreementOwnerWorkflowDetails.AddAsync(detail);
                    await DB.SaveChangesAsync();
                }
            }

            //await DB.SaveChangesAsync();
            var result = ChangeAgreementOwnerWorkflowDTO.CreateFromModelAsync(model);
            return result;
        }

        public async Task<ChangeAgreementOwnerWorkflowDTO> ApproveRequestChangeAgreementOwnerWorkflowAsync(ChangeAgreementOwnerWorkflowDTO input, Guid? UserID)
        {
            var ChangeAgreementOwnerWorkflow = await DB.ChangeAgreementOwnerWorkflows.Where(o => o.ID == input.Id).FirstOrDefaultAsync();
            ChangeAgreementOwnerWorkflow.IsRequestApproved = true;
            ChangeAgreementOwnerWorkflow.RequestApprovedDate = input.RequestApprovedDate;

            var UserRole = await DB.UserRoles
                    .Include(e => e.Role)
                .Where(e => e.UserID == UserID && e.Role.Code == UserRoleCodeKeys.LCM)
                .FirstOrDefaultAsync() ?? new UserRole();

            ChangeAgreementOwnerWorkflow.RequestApproverUserID = UserRole?.UserID;
            ChangeAgreementOwnerWorkflow.RequestApproverRoleID = UserRole?.RoleID;

            ChangeAgreementOwnerWorkflow.ChangeAgreementOwnerStatusMasterCenterID = ChangeAgreementOwnerStatusKey.WaitForAGApprovePrint;

            DB.ChangeAgreementOwnerWorkflows.Update(ChangeAgreementOwnerWorkflow);
            await DB.SaveChangesAsync();

            #region Return
            var result = await this.GetChangeAgreementOwnerWorkflowAsync(input.Id);
            return result;
            #endregion
        }

        public async Task<ChangeAgreementOwnerWorkflowDTO> CancelApproveRequestChangeAgreementOwnerWorkflowAsync(ChangeAgreementOwnerWorkflowDTO input, Guid? UserID)
        {
            var ChangeAgreementOwnerWorkflow = await DB.ChangeAgreementOwnerWorkflows.Where(o => o.ID == input.Id).FirstOrDefaultAsync();
            ChangeAgreementOwnerWorkflow.IsRequestApproved = false;
            ChangeAgreementOwnerWorkflow.RequestApprovedDate = input.RequestApprovedDate;
            var UserRole = await DB.UserRoles
                  .Include(e => e.Role)
              .Where(e => e.UserID == UserID && e.Role.Code == UserRoleCodeKeys.LCM)
              .FirstOrDefaultAsync() ?? new UserRole();
            ChangeAgreementOwnerWorkflow.RequestApproverUserID = UserRole?.UserID;
            ChangeAgreementOwnerWorkflow.RequestApproverRoleID = UserRole.RoleID;
            ChangeAgreementOwnerWorkflow.RequestRejectComment = input.RequestRejectComment;

            DB.ChangeAgreementOwnerWorkflows.Update(ChangeAgreementOwnerWorkflow);
            await DB.SaveChangesAsync();

            #region Return
            var result = await this.GetChangeAgreementOwnerWorkflowAsync(input.Id);
            return result;
            #endregion

        }

        public async Task<ChangeAgreementOwnerWorkflowDTO> ApproveChangeAgreementOwnerWorkflowAsync(ChangeAgreementOwnerWorkflowDTO input, Guid? UserID)
        {

            var model = await DB.AgreementOwners.Where(o => o.AgreementID == input.Agreement.Id).FirstOrDefaultAsync();
            model.IsActive = true;
            await DB.SaveChangesAsync();

            var ChangeAgreementOwnerWorkflow = await DB.ChangeAgreementOwnerWorkflows.Where(o => o.ID == input.Id).FirstOrDefaultAsync();
            ChangeAgreementOwnerWorkflow.IsApproved = true;
            ChangeAgreementOwnerWorkflow.ApprovedDate = input.ApprovedDate;
            var UserRole = await DB.UserRoles
                  .Include(e => e.Role)
              .Where(e => e.UserID == UserID && e.Role.Code == UserRoleCodeKeys.LCM)
              .FirstOrDefaultAsync() ?? new UserRole();
            ChangeAgreementOwnerWorkflow.ApproverUserID = UserRole?.UserID;
            ChangeAgreementOwnerWorkflow.ApproverRoleID = UserRole?.RoleID;
            ChangeAgreementOwnerWorkflow.ChangeAgreementOwnerStatusMasterCenterID = ChangeAgreementOwnerStatusKey.Success;

            DB.ChangeAgreementOwnerWorkflows.Update(ChangeAgreementOwnerWorkflow);
            await DB.SaveChangesAsync();

            #region Return
            var result = await this.GetChangeAgreementOwnerWorkflowAsync(input.Id);
            return result;
            #endregion
        }

        public async Task<ChangeAgreementOwnerWorkflowDTO> CancelApproveChangeAgreementOwnerWorkflowAsync(ChangeAgreementOwnerWorkflowDTO input, Guid? UserID)
        {
            var model = await DB.AgreementOwners.Where(o => o.AgreementID == input.Agreement.Id).FirstOrDefaultAsync();
            model.IsDeleted = true;

            DB.AgreementOwners.Update(model);
            await DB.SaveChangesAsync();

            var ChangeAgreementOwnerWorkflow = await DB.ChangeAgreementOwnerWorkflows.Where(o => o.ID == input.Id).FirstOrDefaultAsync();
            ChangeAgreementOwnerWorkflow.IsApproved = false;
            ChangeAgreementOwnerWorkflow.ApprovedDate = input.ApprovedDate;
            var UserRole = await DB.UserRoles
                  .Include(e => e.Role)
              .Where(e => e.UserID == UserID && e.Role.Code == UserRoleCodeKeys.LCM)
              .FirstOrDefaultAsync() ?? new UserRole();
            ChangeAgreementOwnerWorkflow.ApproverUserID = UserRole.UserID;
            ChangeAgreementOwnerWorkflow.ApproverRoleID = UserRole.RoleID;
            ChangeAgreementOwnerWorkflow.RejectComment = input.RejectComment;

            DB.ChangeAgreementOwnerWorkflows.Update(ChangeAgreementOwnerWorkflow);
            await DB.SaveChangesAsync();

            #region Return
            var result = await this.GetChangeAgreementOwnerWorkflowAsync(input.Id);
            return result;
            #endregion
        }

        public async Task<ChangeAgreementOwnerWorkflowDTO> GetChangeAgreementOwnerWorkflowAsync(Guid? id)
        {
            var model = await DB.ChangeAgreementOwnerWorkflows
                        .Include(o => o.ChangeAgreementOwnerType)
                        .Include(o => o.ChangeAgreementOwnerStatus)
                        .Include(o => o.RequestApproverUser)
                        .Include(o => o.ApproverUser)
                        .Include(o => o.SaleUser)
                        .Where(o => o.AgreementID == id).FirstOrDefaultAsync() ?? new ChangeAgreementOwnerWorkflow();



            var result = ChangeAgreementOwnerWorkflowDTO.CreateFromModelAsync(model);
            var Agreement = new Base.DTOs.SAL.AgreementDTO();
            Agreement.Id = id.Value;
            result.Agreement = Agreement;
            return result;
        }

        //public async Task<ChangeAgreementOwnerWorkflow> DeleteChangeAgreementOwnerWorkflowAsync(Guid id)
        //{
        //    var model = await DB.ChangeAgreementOwnerWorkflows.Where(o => o.ID == id).FirstOrDefaultAsync();
        //    model.IsDeleted = true;

        //    await DB.SaveChangesAsync();
        //    return model;
        //}

        public async Task<ValidateAgreementOwnerWorkflowOutput> ValidateAgreementOwnerWorkflowAsync(Guid agreementId)
        {
            var Agreements = await DB.Agreements.Where(o => o.ID == agreementId).FirstOrDefaultAsync();
            var BookingOwners = await DB.BookingOwners.Include(o => o.Booking)
                                                      .Where(o => o.Booking.ProjectID == Agreements.ProjectID
                                                                && o.IsDeleted == false).ToListAsync();

            var CountProjectBooking = BookingOwners.Count();

            var ThaiNationality = await DB.BookingOwners.Include(o => o.Booking)
                                                    .Where(o => o.Booking.ProjectID == Agreements.ProjectID
                                                                && o.IsDeleted == false
                                                                && o.IsThaiNationality == false).ToListAsync();

            var CountThaiNationality = ThaiNationality.Count();

            decimal PercentForeignerNationality = (CountThaiNationality / CountProjectBooking) * 100;

            var Paid = await DB.UnitPriceInstallments
                                    .Include(o => o.InstallmentOfUnitPriceItem)
                                    .Include(o => o.InstallmentOfUnitPriceItem.UnitPrice)
                                    .Where(o => o.InstallmentOfUnitPriceItem.UnitPrice.BookingID == Agreements.BookingID
                                        && o.InstallmentOfUnitPriceItem.UnitPrice.UnitPriceStage.Key == UnitPriceStageKeys.Agreement
                                        && o.InstallmentOfUnitPriceItem.UnitPrice.IsActive == true
                                        && o.IsPaid == false
                                        && o.DueDate <= DateTime.Now).SumAsync(x => x.Amount);

            var result = new ValidateAgreementOwnerWorkflowOutput();


            //validate unique
            if (Agreements.SignContractApprovedDate == null)
            {
                result.IsSignContractApprovedDate = false;
            }

            if (Paid > 0)
            {
                result.IsInstallmentPeriod = false;
            }
            if (PercentForeignerNationality <= 49)
            {
                result.ISPercentForeignerNationality = true;
            }
            else
            {
                result.ISPercentForeignerNationality = false;
            }
            return result;
        }

        public async Task<List<AgreementOwnerDTO>> GetAgreementOwnersChangeAgreementOwnerWorkflowAsync(Guid agreementId, Guid? changeAgreementOwnerWorkflowId)
        {
            var model = await DB.AgreementOwners
                               .Include(o => o.ContactType)
                               .Include(o => o.ContactTitleTH)
                               .Include(o => o.ContactTitleEN)
                               .Include(o => o.National)
                               .Include(o => o.Gender)
                               .Include(o => o.Agreement)
                               .Where(o => o.AgreementID == agreementId && o.IsMainOwner == false)
                               .OrderBy(o => o.Order).ToListAsync();


            var result = model.Select(async o => await AgreementOwnerDTO.CreateFromModelChangeWorkflowAsync(o, DB, changeAgreementOwnerWorkflowId)).Select(o => o.Result).ToList();
            return result;
        }
        public async Task<List<AgreementOwnerDTO>> GetMainAgreementOwnersChangeAgreementOwnerWorkflowAsync(Guid agreementId, Guid? changeAgreementOwnerWorkflowId)
        {
            var model = await DB.AgreementOwners
                               .Include(o => o.ContactType)
                               .Include(o => o.ContactTitleTH)
                               .Include(o => o.ContactTitleEN)
                               .Include(o => o.National)
                               .Include(o => o.Gender)
                               .Include(o => o.Agreement)
                               .Where(o => o.AgreementID == agreementId && o.IsMainOwner == true)
                               .OrderBy(o => o.Order).ToListAsync();

            var result = model.Select(async o => await AgreementOwnerDTO.CreateFromModelChangeWorkflowAsync(o, DB, changeAgreementOwnerWorkflowId)).Select(o => o.Result).ToList();
            return result;
        }
        //public async Task<AgreementOwnerDropdownDTO> CreateAgreementOwnerAsync(Guid agreementId, Guid contactId)
        //{
        //    var contact = await DB.Contacts.Where(o => o.ID == contactId).FirstAsync();
        //    var agreement = AgreementOwner.CreateFromContact(contact);
        //    agreement.AgreementID = agreementId;
        //    var result = AgreementOwnerDropdownDTO.CreateFromModel(agreement);
        //    return result;
        //}

        public async Task<ChangeAgreementOwnerWorkflowDTO> ApprovePrintChangeAgreementOwnerWorkflowAsync(ChangeAgreementOwnerWorkflowDTO input, Guid? UserID)
        {
            var ChangeAgreementOwnerWorkflow = await DB.ChangeAgreementOwnerWorkflows.Where(o => o.ID == input.Id).FirstOrDefaultAsync();
            ChangeAgreementOwnerWorkflow.IsPrintApproved = true;
            ChangeAgreementOwnerWorkflow.PrintApprovedDate = input.PrintApprovedDate;
            var UserRole = await DB.UserRoles
                .Include(e => e.Role)
            .Where(e => e.UserID == UserID && e.Role.Code == UserRoleCodeKeys.LCM)
            .FirstOrDefaultAsync() ?? new UserRole();
            ChangeAgreementOwnerWorkflow.PrintApprovedByUserID = UserRole?.UserID;
            ChangeAgreementOwnerWorkflow.ChangeAgreementOwnerStatusMasterCenterID = ChangeAgreementOwnerStatusKey.WaitForAGApprove;


            DB.ChangeAgreementOwnerWorkflows.Update(ChangeAgreementOwnerWorkflow);
            await DB.SaveChangesAsync();

            #region Return
            var result = await this.GetChangeAgreementOwnerWorkflowAsync(input.Id);
            return result;
            #endregion
        }

        public async Task<AgreementOwnerDTO> DeleteAddNewAgreementOwnerWorkflowAsync(Guid? agreementOwnerID)
        {
            var model = await DB.AgreementOwners.Where(o => o.ID == agreementOwnerID).FirstOrDefaultAsync();
            model.IsDeleted = true;
            model.IsActive = false;

            DB.AgreementOwners.Update(model);
            await DB.SaveChangesAsync();

            #region Return
            var result = await AgreementOwnerDTO.CreateFromModelAsync(model, DB);
            return result;
            #endregion
        }

        public async Task<List<AgreementOwnerDTO>> AddContactToAgreementOwnerAsync(Guid agreementId, List<Guid> listContactId)
        {
            var lstId = listContactId.Select(o => o).ToList();

            var model = await DB.Contacts.Where(o => lstId.Contains(o.ID)).ToListAsync();

            var result = model.Select(async o => await AgreementOwnerDTO.CreateFromModelContactAsync(agreementId, o, DB, true)).Select(o => o.Result).ToList();

            return result;
        }
    }
}