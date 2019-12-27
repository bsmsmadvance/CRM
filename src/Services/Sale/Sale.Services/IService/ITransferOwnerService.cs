using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.SAL;
using Sale.Params.Inputs;

namespace Sale.Services
{
    public interface ITransferOwnerService
    {
        /// <summary>
        /// ดึงรายการข้อมูลผู้โอนกรรมสิทธิ์     
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<TransferOwnerDTO>> GetTransferOwnerListAsync(Guid id);

        /// <summary>
        /// ดึงข้อมูลผู้โอนกรรมสิทธิ์ จากผู้ทำสัญญา 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TransferOwnerDTO> GetTransferOwnerDrafAsync(Guid id);

        /// <summary>
        /// ดึงข้อมูลผู้โอนกรรมสิทธิ์     
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TransferOwnerDTO> GetTransferOwnerAsync(Guid id);

        /// <summary>
        /// แก้ไขข้อมูลผู้โอนกรรมสิทธิ์ 
        /// </summary>
        /// <param name="id,input"></param>
        /// <returns></returns>
        Task<TransferOwnerDTO> UpdateTransferOwnerAsync(Guid id, TransferOwnerDTO input);
    }
}
