using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using AutoMapper;
using Identity_JWT_API.Controllers;
using Identity_JWT_API.Extensions;
using Identity_JWT_API.Helpers;
using Identity_JWT_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;        
        public UsersController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromQuery] UserParams userParams)
        {
            var users = await _unitOfWork.UserRepository.GetUsersAsync(userParams);

            Response.AddPaginationHeader(users.CurrentPage, users.PageSize,
                users.TotalCount, users.TotalPages);

            return Ok(users);
        }

        [HttpGet("{username}", Name = "GetUser")]
        public async Task<ActionResult<UserDto>> GetUser(string username)
        {
            return await _unitOfWork.UserRepository.GetUserAsync(username);
        }
    }
}