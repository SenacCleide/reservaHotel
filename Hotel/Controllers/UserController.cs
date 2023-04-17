using Hotel.Application.Dto;
using Hotel.Application.Helper;
using Hotel.Application.Repositories;
using Hotel.Application.UseCases.AuthUse;
using Hotel.Application.UseCases.Purse.AddPurse;
using Hotel.Application.UseCases.Users.AddUser;
using Hotel.Application.UseCases.Users.UserStay;
using Hotel.WebApi.Presenter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Hotel.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAddUserUseCase _addUserUseCase;
        private readonly IAddPurseUseCase _addPurseUseCase;
        private readonly IUserStayUseCase _userStayUseCase;
        private readonly IHttpContextAccessor _accessor;
        private readonly UserPresenter _presenter;

        public UserController(IAddUserUseCase addUserUseCase,
            IAddPurseUseCase addPurseUseCase,
            IUserStayUseCase userStayUseCase,
            IHttpContextAccessor accessor,
            UserPresenter presenter)
        {
            _addUserUseCase = addUserUseCase;
            _addPurseUseCase = addPurseUseCase;
            _userStayUseCase = userStayUseCase;
            _accessor = accessor;
            _presenter = presenter;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> User(UserDto user)
        {
            ResponseDto result = await _addUserUseCase.Execute(user);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("AddPurse")]
        public async Task<IActionResult> AddPurse(PurseRequestDto purse)
        {
            var IdUser = _accessor.HttpContext.User.Identity.Name;

            ResponseDto result = new ResponseDto();
            if (purse.Tennant == IdUser)
            {
                result = await _addPurseUseCase.Execute(purse, IdUser, true);
            }
            else
            {
                result = new ResponseDto
                {
                    Message = "Não foi possível adicionar crédito!",
                    Success = false
                };
            }

            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("AddUserStay")]
        public async Task<IActionResult> AddUserStay(StayHotelUserDto userStay)
        {
            var IdUser = _accessor.HttpContext.User.Identity.Name;

            ResponseDto result = new ResponseDto();
            if (userStay.Tennant == IdUser)
            {
                return await _userStayUseCase.Execute(userStay);
            }
            else
            {
                result = new ResponseDto
                {
                    Message = "USUÁRIO INVÁLIDO",
                    Success = false
                };
                _presenter.Populate(result);
                return _presenter.ContentResult;
            }
        }
    }
}