using Application.Interfaces.Security;
using Common.Resources;
using Core.DTOs;
using Core.DTOs.Security.Rol;
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
    public class RolController : ControllerBase
    {
        #region Attributes
        private readonly IRolServices _rolServices;
        #endregion

        #region Builder
        public RolController(IRolServices rolServices)
        {
            _rolServices = rolServices;
        }
        #endregion

        #region Services
        [HttpGet]
        [Route("GetAll")]
        //[CustomPermissionFilter(Enums.Permission.ConsultarRoles)]
        public IActionResult GetAll()
        {
            List<RolDto> result = _rolServices.GetAll();

            return Ok(new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = result
            });
        }

        [HttpPut]
        [Route("Update")]
        //[CustomPermissionFilter(Enums.Permission.ActualizarRoles)]
        public async Task<IActionResult> Update(RolDto rolDto)
        {
            IActionResult action;
            var result = await _rolServices.Update(rolDto);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = result ? GeneralMessages.ItemUpdated : GeneralMessages.ItemNoUpdated,
                Result = result,
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
