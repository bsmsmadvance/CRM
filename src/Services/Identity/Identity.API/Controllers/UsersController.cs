using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.USR;
using Database.Models;
using Identity.Params.Filters;
using Identity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IUserService UserService;

        public UsersController(DatabaseContext db, IUserService userService)
        {
            this.DB = db;
            this.UserService = userService;
        }

        /// <summary>
        /// Get ข้อมูล List User
        /// </summary>
        /// <returns>UserListDTO</returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<UserListDTO>))]
        public async Task<IActionResult> GetUserList([FromQuery]UserFilter filter, [FromQuery]PageParam pageParam, [FromQuery]UserListSortByParam sortByParam)
        {
            try
            {
                var result = await UserService.GetUserListAsync(filter, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);

                return Ok(result.Users);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get ข้อมูล Dropdown List User
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<UserListDTO>))]
        public async Task<IActionResult> GetUserDropdownList([FromQuery]string name)
        {
            try
            {
                var results = await UserService.GetUserDropdownListAsync(name);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}