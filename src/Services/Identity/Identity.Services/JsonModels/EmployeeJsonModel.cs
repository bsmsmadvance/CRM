using System;
using Database.Models.USR;

namespace Identity.Services.JsonModels
{
    public class EmployeeJsonModel
    {
        public string TitleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string EmployeeID { get; set; }
        public string Email { get; set; }
        public string PositionCode { get; set; }
        public string PositionName { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string LeaderCode { get; set; }
        public string LeaderName { get; set; }
        public string CostCenter { get; set; }
        public string Status { get; set; }

        public void ToUserModel(ref User model)
        {
            model.EmployeeNo = this.EmployeeID;
            model.FirstName = this.FirstName;
            model.LastName = this.LastName;
            model.DisplayName = this.DisplayName;
            model.Email = this.Email;
        }
    }
}
