using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.SAL;

namespace Sale.Services
{
    public interface IBookingOwnerService
    {
        /// <summary>
        /// ดึงผู้จองหลักลง dropdown (IsMain=true)
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        Task<List<BookingOwnerDropdownDTO>> GetBookingOwnerDropdownAsync(Guid bookingID);

        /// <summary>
        /// ลิสรายการผู้จอง
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362366310/preview
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        Task<List<BookingOwnerDTO>> GetBookingOwnersAsync(Guid bookingID);

        /// <summary>
        /// Get ข้อมูลผู้จอง Draft สำหรับการเพิ่มผู้จอง
        /// </summary>
        /// <param name="contactID"></param>
        /// <returns></returns>
        Task<BookingOwnerDTO> GetBookingOwnersDraftAsync(Guid bookingID, Guid contactID);

        /// <summary>
        /// เพิ่มผู้จอง
        /// โดยถ้าส่ง FromContactID เข้ามา ถือเป็นการเพิ่มผู้จองจากการเลือก Contact ที่มีอยู่
        /// แต่ถ้า ไม่ได้ส่ง ContactID มา จะต้องส่ง BookingOwnerDTO มาเพื่อทำการสร้าง Contact ใหม่พร้อมกันเพิ่มผู้จอง
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362366318/preview
        /// </summary>
        /// <param name="contactID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BookingOwnerDTO> CreateBookingOwnerAsync(Guid bookingID, BookingOwnerDTO input);

        /// <summary>
        /// แก้ไขชื่อผู้จอง
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362366318/preview
        /// </summary>
        /// <param name="bookingOwnerID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BookingOwnerDTO> EditBookingOwnerAsync(Guid bookingOwnerID, BookingOwnerDTO input);

        /// <summary>
        /// ตั้งผู้จองหลัก
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362366310/preview
        /// </summary>
        /// <param name="bookingOwnerID"></param>
        /// <returns></returns>
        Task<BookingOwnerDTO> SetMainBookingOwnerAsync(Guid bookingOwnerID);

        /// <summary>
        /// เรียงลำดับผู้จอง
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362366310/preview
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BookingOwnerDTO> ReOrderBookingOwnerAsync(Guid bookingOwnerID, BookingOwnerDTO input);

        /// <summary>
        /// ลบผู้จอง
        /// </summary>
        /// <param name="bookingOwnerID"></param>
        /// <returns></returns>
        Task DeleteBookingOwnerAsync(Guid bookingOwnerID, string reason);
    }
}
