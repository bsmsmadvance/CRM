using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Services.JsonModels
{
    public class UserRoleJsonTmp
    {
        [JsonProperty("sysUserRoles")]
        public string SysUserRoles { get; set; }
    }

    public class UserProjectJsonTmp
    {
        [JsonProperty("userProject")]
        public string UserProject { get; set; }
    }


    public class UserRoleJsonModel
    {
        [JsonProperty("sysUserRoles")]
        public SysUserRoles SysUserRoles { get; set; }
    }

    public class UserProjectJsonModel
    {
        [JsonProperty("userProject")]
        public UserProject[] UserProject { get; set; }
    }

    public partial class SysUserRoles
    {
        [JsonProperty("UserId")]
        public string UserId { get; set; }

        [JsonProperty("Roles")]
        public Role[] Roles { get; set; }
    }

    public partial class Role
    {
        [JsonProperty("ID")]
        public string Id { get; set; }

        [JsonProperty("UserID")]
        public string UserId { get; set; }

        [JsonProperty("UserGUID")]
        public string UserGuid { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("EmpCode")]
        public string EmpCode { get; set; }

        [JsonProperty("TitleName")]
        public string TitleName { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("PositionName")]
        public string PositionName { get; set; }

        [JsonProperty("RoleID")]
        public int RoleId { get; set; }

        [JsonProperty("RoleCode")]
        public string RoleCode { get; set; }

        [JsonProperty("RoleName")]
        public string RoleName { get; set; }

        [JsonProperty("AssignType")]
        public string AssignType { get; set; }

        [JsonProperty("SourceType")]
        public string SourceType { get; set; }

        [JsonProperty("Remark")]
        public string Remark { get; set; }

        [JsonProperty("StartDate")]
        public DateTimeOffset? StartDate { get; set; }

        [JsonProperty("EndDate")]
        public DateTimeOffset? EndDate { get; set; }
    }

    public partial class UserProject
    {
        [JsonProperty("ProjectCode")]
        public string ProjectCode { get; set; }
        [JsonProperty("ProjectWBS")]
        public string ProjectWBS { get; set; }
        [JsonProperty("ProjectName")]
        public string ProjectName { get; set; }
        [JsonProperty("ID")]
        public string ID { get; set; }
        [JsonProperty("UserID")]
        public string UserID { get; set; }
        [JsonProperty("ProjectID")]
        public string ProjectID { get; set; }
        [JsonProperty("AssignType")]
        public string AssignType { get; set; }
        [JsonProperty("SourceType")]
        public string SourceType { get; set; }
        [JsonProperty("CreatedBy")]
        public string CreatedBy { get; set; }
        [JsonProperty("CreatedDate")]
        public DateTimeOffset? CreatedDate { get; set; }
        [JsonProperty("ModifiedBy")]
        public string ModifiedBy { get; set; }
        [JsonProperty("ModifiedDate")]
        public DateTimeOffset? ModifiedDate { get; set; }
        [JsonProperty("Remark")]
        public string Remark { get; set; }
        [JsonProperty("StartDate")]
        public DateTimeOffset? StartDate { get; set; }
        [JsonProperty("EndDate")]
        public DateTimeOffset? EndDate { get; set; }
    }

}
