using Database.Models.CTM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Services.JsonModels
{
    public class LeadJsonModel
    {
        public string LeadDate { get; set; }
        public string ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string Remark { get; set; }
        public string refID { get; set; }
        public string LeadsTypeID { get; set; }
        public string Advertisement { get; set; }
        public string AdvertisementID { get; set; }
        public bool? CallBack { get; set; }
        public string LCUserName { get; set; }
        public string ISlead { get; set; }

        public void ToLeadModel(ref Lead model)
        {
            model.FirstName = this.CustomerName;
            model.PhoneNumber = this.Mobile;
            model.IsPhoneNumberConfirmed = (!string.IsNullOrEmpty(this.Mobile)) ? true : false;
            model.RefID = this.refID;
            model.Remark = this.Remark;
            model.CallBack = this.CallBack;
        }
    }
}
