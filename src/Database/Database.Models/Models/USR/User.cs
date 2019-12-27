using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.USR
{
    [Description("ผู้ใช้งาน")]
    [Table("User", Schema = Schema.USER)]
    public class User : BaseEntity
    {
        [Description("รหัสพนักงาน")]
        [MaxLength(50)]
        public string EmployeeNo { get; set; }
        [Description("คำนำหน้า")]
        [MaxLength(50)]
        public string Title { get; set; }
        [Description("ชื่อจริง")]
        [MaxLength(1000)]
        public string FirstName { get; set; }
        [Description("ชื่อกลาง")]
        [MaxLength(1000)]
        public string MiddleName { get; set; }
        [Description("นามสกุล")]
        [MaxLength(1000)]
        public string LastName { get; set; }
        [Description("Display Name")]
        [MaxLength(1000)]
        public string DisplayName { get; set; }
        [Description("รูปโปรไฟล์")]
        [MaxLength(1000)]
        public string ProfilePicture { get; set; }
        [Description("อีเมลล์")]
        [MaxLength(100)]
        public string Email { get; set; }
        [Description("เวลาที่ Login ล่าสุด")]
        public DateTime? LastLoginTime { get; set; }
        [Description("เวลาที่ใช้งานล่าสุด")]
        public DateTime? LastActivityTime { get; set; }
        [Description("เบอร์โทร")]
        [MaxLength(100)]
        public string PhoneNo { get; set; }
        [Description("ไลน์ไอดี")]
        [MaxLength(100)]
        public string LineId { get; set; }
        [Description("ไลน์ QR Code")]
        [MaxLength(1000)]
        public string LineQRCode { get; set; }
        [Description("รายงานต่อใคร")]
        public Guid? ReportToUserID { get; set; }
        [Description("เป็น 3rd Party Client")]
        public bool IsClient { get; set; }
        [Description("Client ID")]
        [MaxLength(100)]
        public string ClientID { get; set; }
        [Description("Client Secret")]
        [MaxLength(100)]
        public string ClientSecret { get; set; }

        public List<UserAuthorizeProject> UserAuthorizeProjects { get; set; }
        public List<UserRole> UserRoles { get; set; }

    }
}
