using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Customer.Services.JsonModels
{
    [XmlRoot(ElementName = "Transaction")]
    public class Transaction
    {
        [XmlElement(ElementName = "Firstname")]
        public string Firstname { get; set; }
        [XmlElement(ElementName = "Lastname")]
        public string Lastname { get; set; }
        [XmlElement(ElementName = "Email")]
        public string Email { get; set; }
        [XmlElement(ElementName = "Tel")]
        public string Tel { get; set; }
        [XmlElement(ElementName = "PersonalID")]
        public string PersonalID { get; set; }
        [XmlElement(ElementName = "Gender")]
        public string Gender { get; set; }
        [XmlElement(ElementName = "House_id")]
        public string House_id { get; set; }
        [XmlElement(ElementName = "House_Moo")]
        public string House_Moo { get; set; }
        [XmlElement(ElementName = "House_Soi")]
        public string House_Soi { get; set; }
        [XmlElement(ElementName = "House_Village")]
        public string House_Village { get; set; }
        [XmlElement(ElementName = "House_Road")]
        public string House_Road { get; set; }
        [XmlElement(ElementName = "House_Subdistrict")]
        public string House_Subdistrict { get; set; }
        [XmlElement(ElementName = "House_District")]
        public string House_District { get; set; }
        [XmlElement(ElementName = "House_Province")]
        public string House_Province { get; set; }
        [XmlElement(ElementName = "House_Country")]
        public string House_Country { get; set; }
        [XmlElement(ElementName = "House_Postalcode")]
        public string House_Postalcode { get; set; }
        [XmlElement(ElementName = "Work_Ampher")]
        public string Work_Ampher { get; set; }
        [XmlElement(ElementName = "Work_Province")]
        public string Work_Province { get; set; }
        [XmlElement(ElementName = "Work_Postalcode")]
        public string Work_Postalcode { get; set; }
        [XmlElement(ElementName = "Plan_Type")]
        public string Plan_Type { get; set; }
        [XmlElement(ElementName = "Budget")]
        public string Budget { get; set; }
        [XmlElement(ElementName = "Income")]
        public string Income { get; set; }
        [XmlElement(ElementName = "Family")]
        public string Family { get; set; }
        [XmlElement(ElementName = "Car_Park")]
        public string Car_Park { get; set; }
        [XmlElement(ElementName = "Hobbies")]
        public string Hobbies { get; set; }
        [XmlElement(ElementName = "Message")]
        public string Message { get; set; }
        [XmlElement(ElementName = "Purpose_of_buying")]
        public string Purpose_of_buying { get; set; }
        [XmlElement(ElementName = "Visit_Date")]
        public string Visit_Date { get; set; }
        [XmlElement(ElementName = "Visit_Time")]
        public string Visit_Time { get; set; }
        [XmlElement(ElementName = "Education")]
        public string Education { get; set; }
        [XmlElement(ElementName = "Birthday")]
        public string Birthday { get; set; }
        [XmlElement(ElementName = "Relation")]
        public string Relation { get; set; }
        [XmlElement(ElementName = "LeadsID")]
        public string LeadsID { get; set; }
        [XmlElement(ElementName = "Ptype")]
        public string Ptype { get; set; }
        [XmlElement(ElementName = "Brand_Name")]
        public string Brand_Name { get; set; }
        [XmlElement(ElementName = "Campaign_ID")]
        public string Campaign_ID { get; set; }
        [XmlElement(ElementName = "Campaign_Name")]
        public string Campaign_Name { get; set; }
        [XmlElement(ElementName = "ProductID")]
        public string ProductID { get; set; }
        [XmlElement(ElementName = "Project_name_crm")]
        public string Project_name_crm { get; set; }
        [XmlElement(ElementName = "Project_name_web")]
        public string Project_name_web { get; set; }
        [XmlElement(ElementName = "Data_Type")]
        public string Data_Type { get; set; }
        [XmlElement(ElementName = "Utm_main_url")]
        public string Utm_main_url { get; set; }
        [XmlElement(ElementName = "Utm_Source")]
        public string Utm_Source { get; set; }
        [XmlElement(ElementName = "Utm_Medium")]
        public string Utm_Medium { get; set; }
        [XmlElement(ElementName = "Utm_Campaign")]
        public string Utm_Campaign { get; set; }
        [XmlElement(ElementName = "Create_Date")]
        public string Create_Date { get; set; }
        [XmlElement(ElementName = "Create_Time")]
        public string Create_Time { get; set; }
        [XmlElement(ElementName = "Create_IP")]
        public string Create_IP { get; set; }
        [XmlElement(ElementName = "Email_Status")]
        public string Email_Status { get; set; }
        [XmlElement(ElementName = "Email_Detail")]
        public string Email_Detail { get; set; }
        [XmlElement(ElementName = "SMS_Status")]
        public string SMS_Status { get; set; }
        [XmlElement(ElementName = "SMS_Detail")]
        public string SMS_Detail { get; set; }
        [XmlElement(ElementName = "Format_Code")]
        public string Format_Code { get; set; }
        [XmlElement(ElementName = "Other_Field")]
        public string Other_Field { get; set; }
        [XmlElement(ElementName = "PtypeID")]
        public string PtypeID { get; set; }
        [XmlElement(ElementName = "Brand_ID")]
        public string Brand_ID { get; set; }
        [XmlElement(ElementName = "Campaign_Month")]
        public string Campaign_Month { get; set; }
        [XmlElement(ElementName = "Utm_Content")]
        public string Utm_Content { get; set; }
        [XmlElement(ElementName = "Title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "Send_By_Card")]
        public string Send_By_Card { get; set; }
        [XmlElement(ElementName = "Email2")]
        public string Email2 { get; set; }
        [XmlElement(ElementName = "Firstname2")]
        public string Firstname2 { get; set; }
        [XmlElement(ElementName = "Lastname2")]
        public string Lastname2 { get; set; }
        [XmlElement(ElementName = "Phone2")]
        public string Phone2 { get; set; }
        [XmlElement(ElementName = "Age")]
        public string Age { get; set; }
        [XmlElement(ElementName = "Room_Type")]
        public string Room_Type { get; set; }
        [XmlElement(ElementName = "Unit_Size")]
        public string Unit_Size { get; set; }
        [XmlElement(ElementName = "Room_A")]
        public string Room_A { get; set; }
        [XmlElement(ElementName = "Location")]
        public string Location { get; set; }
        [XmlElement(ElementName = "Unit")]
        public string Unit { get; set; }
        [XmlElement(ElementName = "Member")]
        public string Member { get; set; }
        [XmlElement(ElementName = "SMS")]
        public string SMS { get; set; }
        [XmlElement(ElementName = "PLAN")]
        public string PLAN { get; set; }
        [XmlElement(ElementName = "Occupation")]
        public string Occupation { get; set; }
        [XmlElement(ElementName = "Activity_Num")]
        public string Activity_Num { get; set; }
        [XmlElement(ElementName = "Return_Contact")]
        public string Return_Contact { get; set; }
        [XmlElement(ElementName = "Email_ID")]
        public string Email_ID { get; set; }
        [XmlElement(ElementName = "Unit_Name")]
        public string Unit_Name { get; set; }
        [XmlElement(ElementName = "Remark")]
        public string Remark { get; set; }
    }

    [XmlRoot(ElementName = "Response")]
    public class Response
    {
        [XmlElement(ElementName = "Transaction")]
        public List<Transaction> Transaction { get; set; }
    }
}
