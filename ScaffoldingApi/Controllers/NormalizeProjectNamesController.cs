using Application.Interfaces.Scaffolding;
using Common.Resources;
using Core.DTOs;
using Core.DTOs.Scaffolding.Projets;
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
    public class NormalizeProjectNamesController : ControllerBase
    {
        #region Attributes
        private readonly INormalizeProjectNamesServices _normalizeProjectNamesServices;
        #endregion

        #region Builder
        public NormalizeProjectNamesController(INormalizeProjectNamesServices normalizeProjectNamesServices)
        {
            _normalizeProjectNamesServices = normalizeProjectNamesServices;
        }
        #endregion

        #region Services
        [HttpGet]
        [Route("GetallProjectNames")]
        public IActionResult Getall()
        {
            List<NormalizeProjectNamesDto> result = _normalizeProjectNamesServices.Getall();
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = result
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("InsertProjectNames")]
        public async Task<IActionResult> Insert(AddNormalizeProjectNamesDto add)
        {
            IActionResult action;
            bool result = await _normalizeProjectNamesServices.Insert(add);
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
        [Route("UpdateProjectNames")]
        public async Task<IActionResult> Update(NormalizeProjectNamesDto update)
        {
            IActionResult action;
            bool result = await _normalizeProjectNamesServices.Update(update);
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

        [HttpDelete]
        [Route("DeleteProjectNames")]
        public async Task<IActionResult> Delete(int id)
        {
            IActionResult action;
            bool result = await _normalizeProjectNamesServices.Delete(id);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = result,
                Result = result,
                Message = result ? GeneralMessages.ItemDeleted : GeneralMessages.ItemNoDeleted
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
