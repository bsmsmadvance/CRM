using System;
namespace Base.DTOs.SAL
{
    /// <summary>
    /// ไฟล์แนบการตั้งเรื่องย้ายแปลง
    /// </summary>
    public class ChangeUnitFileDTO : BaseDTO
    {
        public string Name { get; set; }
        public FileDTO File { get; set; }
    }
}
