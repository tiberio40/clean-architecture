using Application.Interfaces.Security;
using Common.Resources;
using Core.DTOs;
using Core.DTOs.Security.Permission;
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
    public class PermissionController : ControllerBase
    {
        #region Attributes
        private readonly IPermissionServices _permissionServices;
        #endregion

        #region Builder
        public PermissionController(IPermissionServices permissionServices)
        {
            _permissionServices = permissionServices;
        }
        #endregion

        #region Services
        [HttpGet]
        [Route("GetAllPermissionByRol")]
        //[CustomPermissionFilter(Enums.Permission.ConsultarPermisos)]
        public IActionResult GetAllPermissionByRol(int idRol)
        {
            var result = _permissionServices.GetAllPermissionsByRol(idRol);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = result,
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllPermissions")]
        //[CustomPermissionFilter(Enums.Permission.ConsultarPermisos)]
        public IActionResult GetAll()
        {
            var result = _permissionServices.GetAllPermission();
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = result,
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("UpdatePermission")]
        //[CustomPermissionFilter(Enums.Permission.ActualizarPermisos)]
        public async Task<IActionResult> UpdatePermission(UpdatePermissionDto update)
        {
            IActionResult action;
            bool result = await _permissionServices.UpdatePermission(update);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = result,
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
