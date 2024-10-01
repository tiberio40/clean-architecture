using Application.Interfaces.Security;
using Common.Resources;
using Core.DTOs;
using Core.DTOs.Security.Menus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScaffoldingApi.Handlers;

namespace ScaffoldingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [TypeFilter(typeof(CustomExceptionHandler))]
    [TypeFilter(typeof(CustomValidationFilterAttribute))]
    public class MenuController : ControllerBase
    {
        #region Attributes
        private readonly IMenuServices _menuServices;
        #endregion

        #region Buildes
        public MenuController(IMenuServices menuServices)
        {
            _menuServices = menuServices;
        }
        #endregion

        #region Services
        [HttpGet]
        [Route("GetAllMenuByRol")]
        public IActionResult GetAllMenuByRol(int idRol)
        {
            var menus = _menuServices.GetAllMenuByRol(idRol);
            ResponseDto response = new()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = menus
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateMenuRol")]
        public async Task<IActionResult> UpdateMenuRol(RolMenuDto update)
        {
            IActionResult action;
            var result = await _menuServices.UpdateMenuRol(update);
            ResponseDto response = new()
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
