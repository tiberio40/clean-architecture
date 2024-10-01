using Application.Interfaces.Campaign;
using Application.Services.Campaign;
using Common.Resources;
using Core.DTOs;
using Core.DTOs.Campaign.Marketing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ScaffoldingApi.Handlers;

namespace ScaffoldingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionHandler))]
    [TypeFilter(typeof(CustomValidationFilterAttribute))]
    public class MetaController : ControllerBase
    {
        private readonly IMetaTypeService _metaTypeService;
        public MetaController(IMetaTypeService metaTypeService) {
            _metaTypeService = metaTypeService;
        }


        [HttpGet("")]
        public ActionResult Index() {
            return Ok(_metaTypeService.GetAll());        
        }

        [HttpGet("Credentials")]
        public IActionResult GetCredentials()
        {
            return Ok(new ResponseDto()
            {
                IsSuccess = true,
                Result = _metaTypeService.GetCredentials()
            });
        }

        [HttpPost("Credentials")]
        public async Task<IActionResult> SetCredentials(MetasCredentialsDto model)
        {
            IActionResult action;


            if (ModelState.IsValid)
            {
                var result = await _metaTypeService.SetCredentials(model);
                action = Ok(new ResponseDto()
                {
                    IsSuccess = true,
                    Result = result,
                    Message = GeneralMessages.ItemInserted
                });
            }
            else
            {
                action = BadRequest(new ResponseDto()
                {
                    IsSuccess = false,
                    Result = ModelState,
                    Message = GeneralMessages.ItemNoInserted
                });
            }

            return action;
        }

        [HttpGet("Credentials/{id}")]
        public async Task<IActionResult> GetCredentialsById(int id) { 
            return Ok(new ResponseDto()
            {
                IsSuccess = true,
                Result = await _metaTypeService.GetCredentialsById(id)
            });
        }
    }
}
