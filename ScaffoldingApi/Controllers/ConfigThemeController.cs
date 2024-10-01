using Application.Interfaces.Scaffolding;
using Common.Resources;
using Core.DTOs;
using Core.DTOs.Scaffolding.Theme;
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
    public class ConfigThemeController : ControllerBase
    {
        #region Attributes
        private readonly IConfigThemeServices _configThemeServices;
        #endregion

        #region Builder
        public ConfigThemeController(IConfigThemeServices configThemeServices)
        {
            _configThemeServices = configThemeServices;
        }
        #endregion

        #region Services
        [HttpGet]
        [Route("GetConfigTheme")]
        [AllowAnonymous]
        public IActionResult GetConfigTheme()
        {
            ConsultConfigThemeDto result = _configThemeServices.GetConfigTheme();
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = result
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("SaveTheme")]        
        public async Task<IActionResult> SaveTheme([FromForm] AddConfigThmeDto config)
        {
            IActionResult action;
            bool result = await _configThemeServices.SaveTheme(config);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = result,
                Result = result,
                Message = result ? GeneralMessages.ThemeSaveSuccess : GeneralMessages.ThmeSaveFaile
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
