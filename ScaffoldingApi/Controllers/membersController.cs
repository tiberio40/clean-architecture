using Application.Interfaces;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using ScaffoldingApi.Handlers;

namespace ScaffoldingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    [TypeFilter(typeof(CustomExceptionHandler))]
    [TypeFilter(typeof(CustomValidationFilterAttribute))]
    public class MembersController : ControllerBase
    {
        private readonly IMembersRequestService _membersRequestService;

        public MembersController(IMembersRequestService membersRequestService)
        {
            _membersRequestService = membersRequestService;
        }

        [HttpGet]
        [Route("GetAllMembers")]
        public async Task<ActionResult<ResponseDto>> GetAllMembers([FromQuery]string account)
        {
            var members = await _membersRequestService.GetAllMembers(account);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Message = string.Empty,
                Result = members
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto>> GetMemberById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var member = await _membersRequestService.GetMemberById(id);
                if (member != null)
                {
                    ResponseDto response = new ResponseDto()
                    {
                        IsSuccess = true,
                        Message = string.Empty,
                        Result = member
                    };

                    return Ok(response);
                }
                return NotFound();
            }
            return BadRequest();
        }
    }
}
