using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.SAL;

namespace Sale.Services
{
    public interface IAgreementOwnerService
    {
        /// <summary>
        /// ดึงผู้ทำสัญญาหลักลง dropdown (IsMain=true)
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        Task<List<AgreementOwnerDropdownDTO>> GetAgreementOwnerDropdownAsync(Guid agreementID);

        /// <summary>
        /// ลิสรายการผู้ทำสัญญา
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362366315/preview
        /// </summary>
        /// <param name="agreementID"></param>
        /// <returns></returns>
        Task<List<AgreementOwnerDTO>> GetAgreementOwnersAsync(Guid agreementID);

        /// <summary>
        /// Get ข้อมูลผู้ทำสัญญา Draft สำหรับการเพิ่มผู้ทำสัญญา
        /// </summary>
        /// <param name="contactID"></param>
        /// <returns></returns>
        Task<AgreementOwnerDTO> GetAgreementOwnersDraftAsync(Guid agreementID, Guid contactID);

        /// <summary>
        /// เพิ่มผู้ทำสัญญา
        /// โดยถ้าส่ง FromContactID เข้ามา ถือเป็นการเพิ่มผู้ทำสัญญาจากการเลือก Contact ที่มีอยู่
        /// แต่ถ้า ไม่ได้ส่ง ContactID มา จะต้องส่ง AgreementOwnerDTO มาเพื่อทำการสร้าง Contact ใหม่พร้อมกันเพิ่มผู้ทำสัญญา
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362366318/preview
        /// </summary>
        /// <param name="contactID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AgreementOwnerDTO> CreateAgreementOwnerAsync(Guid contactID, AgreementOwnerDTO input);

        /// <summary>
        /// แก้ไขชื่อผู้ทำสัญญา
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362366318/preview
        /// </summary>
        /// <param name="agreementOwnerID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AgreementOwnerDTO> EditAgreementOwnerAsync(Guid agreementOwnerID, AgreementOwnerDTO input);

        /// <summary>
        /// ตั้งผู้ทำสัญญาหลัก
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362366315/preview
        /// </summary>
        /// <param name="agreementOwnerID"></param>
        /// <returns></returns>
        Task<AgreementOwnerDTO> SetMainAgreementOwnerAsync(Guid agreementOwnerID);

        /// <summary>
        /// เรียงลำดับผู้ทำสัญญา
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362366315/preview
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AgreementOwnerDTO> ReOrderAgreementOwnerAsync(Guid agreementOwnerID, AgreementOwnerDTO input);

        /// <summary>
        /// ลบผู้ทำสัญญา
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362366316/preview
        /// </summary>
        /// <param name="agreementOwnerID"></param>
        /// <returns></returns>
        Task DeleteAgreementOwnerAsync(Guid agreementOwnerID, string reason);
    }
}
