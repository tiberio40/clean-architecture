using Application.Interfaces;
using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ScaffoldingApi.Handlers;
using Microsoft.Identity.Client;
using Application.Services;
using Core.DTOs.TmetricDtos;

namespace ScaffoldingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    [TypeFilter(typeof(CustomExceptionHandler))]
    [TypeFilter(typeof(CustomValidationFilterAttribute))]
    public class TimeTmetricController : ControllerBase
    {
        private readonly ITmetricRequestService _tmetricRequestService;
        private readonly IMembersRequestService _membersRequestService;

        public TimeTmetricController(ITmetricRequestService tmetricRequestService, IMembersRequestService membersRequestService)
        {
            _tmetricRequestService = tmetricRequestService;
            _membersRequestService = membersRequestService;
        }

        [HttpGet]
        [Route("GetAllTimeEntry")]
        public async Task<ActionResult<ResponseDto>> GetAllTimeEntry([FromQuery] string account, [FromQuery] string startTime, [FromQuery] string endTime, [FromQuery] bool? useUtcTime)
        {

            if (!account.IsNullOrEmpty() && !startTime.IsNullOrEmpty() && !endTime.IsNullOrEmpty())
            {
                var timeEntries = await _tmetricRequestService.GetAllTimeEntry(account, startTime, endTime, useUtcTime ?? true);
                ResponseDto response = new ResponseDto()
                {
                    IsSuccess = true,
                    Message = string.Empty,
                    Result = timeEntries
                };

                return Ok(response);

            }

            return BadRequest();
        }

        [HttpGet("account/{accountId}/user/{email}")]
        public async Task<ActionResult<ResponseDto>> GetTimeEntriesByUser(int accountId, string email, [FromQuery] string startTime, [FromQuery] string endTime, [FromQuery] bool? useUtcTime, [FromQuery] bool? includeDeleted, [FromQuery] bool? truncate)
        {

            if (!startTime.IsNullOrEmpty() && !endTime.IsNullOrEmpty())
            {
                var timeEntries = await _tmetricRequestService.GetTimeEntriesByUser(accountId, email, startTime, endTime, useUtcTime ?? true, includeDeleted ?? false, truncate ?? true);
                ResponseDto response = new ResponseDto()
                {
                    IsSuccess = true,
                    Message = string.Empty,
                    Result = timeEntries
                };

                return Ok(response);
            }

            return BadRequest();
        }
    }
}
