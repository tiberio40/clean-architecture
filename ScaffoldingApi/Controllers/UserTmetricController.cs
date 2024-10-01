using Application.Interfaces;
using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScaffoldingApi.Handlers;

namespace ScaffoldingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [TypeFilter(typeof(CustomExceptionHandler))]
    [ServiceFilter(typeof(CustomValidationFilterAttribute))]
    public class UserTmetricController : ControllerBase
    {
        #region Attributes
        private readonly ITmetricRequestService _userTmetricServices;
        #endregion

        public UserTmetricController(ITmetricRequestService tmetricRequestService)
        {
            _userTmetricServices = tmetricRequestService;
        }
        [HttpGet]
        [Route("ExternalGetUserTmetric")]
        public async Task<IActionResult> GetUserTmetric()
        {
            var userTmetric = await _userTmetricServices.GetUserAdmin();
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = userTmetric
            };

            return Ok(response);
        }
    }
}
