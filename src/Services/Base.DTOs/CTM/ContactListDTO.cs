﻿using Database.Models.MasterKeys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class ContactListDTO
    {
        /// <summary>
        /// ID ของ Contact
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// รหัสของ Contact
        /// </summary>
        public string ContactNo { get; set; }
        /// <summary>
        /// ชื่อจริง/ชื่อบริษัท (ภาษาไทย)
        /// </summary>
        public string FirstNameTH { get; set; }
        /// <summary>
        /// นามสกุล (ภาษาไทย)
        /// </summary>
        public string LastNameTH { get; set; }
        /// <summary>
        /// เบอร์โทรศัพท์
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// เบอร์โทรศัพท์บ้าน
        /// </summary>
        public string HomeNumber { get; set; }
        /// <summary>
        /// จำนวน Opportunity
        /// </summary>
        public int OpportunityCount { get; set; }
        /// <summary>
        /// Last Opportunity
        /// </summary>
        public DateTime? LastOpportunityDate { get; set; }
        /// <summary>
        /// วันที่สร้าง
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// วันที่แก้ไข
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
        /// <summary>
        /// เลขที่บัตรประชาชน
        /// </summary>
        public string CitizenIdentityNo { get; set; }

        public static ContactListDTO CreateFromQueryResult(ContactQueryResult model)
        {
            if (model != null)
            {
                var result = new ContactListDTO();

                result.Id = model.Contact.ID;
                result.ContactNo = model.Contact.ContactNo;
                result.FirstNameTH = model.Contact.FirstNameTH;
                result.LastNameTH = model.Contact.LastNameTH;
                result.PhoneNumber = model.ContactPhone?.PhoneNumber;
                result.CreatedDate = model.Contact.Created;
                result.UpdatedDate = model.Contact.Updated;
                result.CitizenIdentityNo = model.Contact.CitizenIdentityNo;
                result.LastOpportunityDate = model.Contact.LastOpportunity?.Created;
                result.OpportunityCount = model.Contact.OpportunityCount;

                return result;
            }
            else
            {
                return null;
            }
        }

        public static ContactListDTO CreateFromModel(models.CTM.Contact model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new ContactListDTO()
                {
                    Id = model.ID,
                    ContactNo = model.ContactNo,
                    FirstNameTH = model.FirstNameTH,
                    LastNameTH = model.LastNameTH,
                    CreatedDate = model.Created,
                    UpdatedDate = model.Updated,
                    CitizenIdentityNo = model.CitizenIdentityNo,
                    OpportunityCount = DB.Opportunities.Where(o => o.ContactID == model.ID).Count()
                };

                var phoneMasterID = DB.MasterCenters.Where(o => o.Key == PhoneTypeKeys.Mobile && o.MasterCenterGroupKey == MasterCenterGroupKeys.PhoneType).Select(o => o.ID).First();
                var homeMasterID = DB.MasterCenters.Where(o => o.Key == PhoneTypeKeys.Home && o.MasterCenterGroupKey == MasterCenterGroupKeys.PhoneType).Select(o => o.ID).First();
                result.PhoneNumber = DB.ContactPhones?.Where(o => o.ContactID == model.ID && o.PhoneTypeMasterCenterID == phoneMasterID).Select(o => o.PhoneNumber).FirstOrDefault();
                result.HomeNumber = DB.ContactPhones?.Where(o => o.ContactID == model.ID && o.PhoneTypeMasterCenterID == homeMasterID).Select(o => o.PhoneNumber).FirstOrDefault();



                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(ContactListSortByParam sortByParam, ref IQueryable<ContactQueryResult> query)
        {
            IOrderedQueryable<ContactQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case ContactListSortBy.ContactNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Contact.ContactNo);
                        else orderQuery = query.OrderByDescending(o => o.Contact.ContactNo);
                        break;
                    case ContactListSortBy.CitizenIdentityNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Contact.CitizenIdentityNo);
                        else orderQuery = query.OrderByDescending(o => o.Contact.CitizenIdentityNo);
                        break;
                    case ContactListSortBy.FirstNameTH:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Contact.FirstNameTH);
                        else orderQuery = query.OrderByDescending(o => o.Contact.FirstNameTH);
                        break;
                    case ContactListSortBy.LastNameTH:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Contact.LastNameTH);
                        else orderQuery = query.OrderByDescending(o => o.Contact.LastNameTH);
                        break;
                    case ContactListSortBy.PhoneNumber:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ContactPhone.PhoneNumber);
                        else orderQuery = query.OrderByDescending(o => o.ContactPhone.PhoneNumber);
                        break;
                    case ContactListSortBy.CreatedDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Contact.Created);
                        else orderQuery = query.OrderByDescending(o => o.Contact.Created);
                        break;
                    case ContactListSortBy.UpdatedDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Contact.Updated);
                        else orderQuery = query.OrderByDescending(o => o.Contact.Updated);
                        break;
                    case ContactListSortBy.LastOpportunityDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Opportunity.Created);
                        else orderQuery = query.OrderByDescending(o => o.Opportunity.Created);
                        break;
                    case ContactListSortBy.OpportunityCount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Contact.OpportunityCount);
                        else orderQuery = query.OrderByDescending(o => o.Contact.OpportunityCount);
                        break;
                    default:
                        orderQuery = query.OrderBy(o => o.Contact.ContactNo);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.Contact.ContactNo);
            }

            orderQuery.ThenBy(o => o.Contact.ID);
            query = orderQuery;
        }
    }

    public class ContactQueryResult
    {
        public models.CTM.Contact Contact { get; set; }
        public models.CTM.ContactPhone ContactPhone { get; set; }
        public models.CTM.Opportunity Opportunity { get; set; }
    }
}