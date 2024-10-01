using Application.Interfaces.Security;
using Common.Enums;
using Common.Helpers;
using Common.Resources;
using Core.DTOs;
using Core.DTOs.Security.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScaffoldingApi.Handlers;
using static Common.Constant.Const;

namespace ScaffoldingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [TypeFilter(typeof(CustomExceptionHandler))]
    [ServiceFilter(typeof(CustomValidationFilterAttribute))]
    public class UserController : ControllerBase
    {
        #region Attributes
        private readonly IUserServices _userServices;
        #endregion

        #region Builder
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        #endregion

        #region Services

        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetUser(int idUser)
        {
            ConsultUserDto user = _userServices.GetUser(idUser);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = user
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllUser")]
        [CustomPermissionFilter(Enums.RolUser.Administrador)]
        public IActionResult GetAllUser()
        {
            List<ConsultUserDto> users = _userServices.GetAllUser();
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = users
            };

            return Ok(response);
        }


        [HttpGet]
        [Route("GetUserByEmail")]
        public IActionResult GetUserByEmail(string email)
        {
            ConsultUserDto user = _userServices.GetUserByEmail(email);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = user
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDto login)
        {
            TokenDto result = _userServices.Login(login);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = result,
                Message = string.Empty
            };

            return Ok(response);
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="register"></param>
        /// <returns>
        /// ReponseDto
        /// </returns>
        /// <response code="200">Returns 200 OK</response>
        /// <response code="400">Returns 400 if there are business errors</response>
        /// <response code="500">Returns 500 if there are internal server error</response>
        [HttpPost]
        [Route("RegisterUser")]
        //[CustomPermissionFilter(Enums.RolUser.Administrador)]
        public async Task<IActionResult> RegisterUser(AddUserDto register)
        {
            IActionResult action;
            bool result = await _userServices.RegisterUser(register);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = result,
                Result = result,
                Message = result ? GeneralMessages.ItemInserted : GeneralMessages.ItemNoInserted
            };

            if (result)
                action = Ok(response);
            else
                action = BadRequest(response);

            return action;
        }

        [HttpPut]
        [Route("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(UserPasswordDto password)
        {
            IActionResult action;
            string idUser = Helper.GetClaimValue(Request.Headers["Authorization"], TypeClaims.IdUser);
            bool result = await _userServices.UpdatePassword(password, Convert.ToInt32(idUser));
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = result,
                Result = string.Empty,
                Message = result ? GeneralMessages.PasswordChanged : GeneralMessages.PasswordChangeError
            };

            if (result)
                action = Ok(response);
            else
                action = BadRequest(response);

            return action;
        }

        [HttpPut]
        [Route("ResetPassword")]
        [CustomPermissionFilter(Enums.RolUser.Administrador)]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            IActionResult action;
            bool result = await _userServices.ResetPassword(resetPassword);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = result,
                Result = result,
                Message = result ? GeneralMessages.PasswordReset : GeneralMessages.PasswordChangeError
            };

            if (result)
                action = Ok(response);
            else
                action = BadRequest(response);

            return action;
        }

        [HttpPut]
        [Route("ActivateAndDeactivate")]
        [CustomPermissionFilter(Enums.RolUser.Administrador)]
        public async Task<IActionResult> ActivateAndDeactivate(ActiveUserDto activeUser)
        {
            IActionResult action;
            bool result = await _userServices.ActivateAndDeactivate(activeUser);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = result,
                Result = result,
                Message = result ? GeneralMessages.ItemUpdated : GeneralMessages.ItemNoUpdated
            };

            if (result)
                action = Ok(response);
            else
                action = BadRequest(response);

            return action;
        }



        #endregion
    }
}
