using Application.Interfaces.Campaign;
using Application.Services.Campaign;
using Common.Resources;
using Core.DTOs;
using Core.DTOs.Campaign.Marketing;
using Core.DTOs.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ScaffoldingApi.Handlers;
using System;
using static System.Collections.Specialized.BitVector32;

namespace ScaffoldingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionHandler))]
    [TypeFilter(typeof(CustomValidationFilterAttribute))]
    public class MarketingController : ControllerBase
    {
        private readonly IMarketingService _marketingService;

        public MarketingController(IMarketingService marketingService) {
            _marketingService = marketingService;
        }

        [HttpGet("")]
        public IActionResult Index() {
            return Ok(_marketingService.GetAllMarketings());
        }


        [HttpGet("{id}")]
        public IActionResult GetMarketingById(int id)
        {
            return Ok(_marketingService.GetMarketingById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMarketingDto model) {
            IActionResult action;

            if (ModelState.IsValid)
            {
                var result = await _marketingService.CreateMarketing(model);
                action = Ok(new ResponseDto()
                {
                    IsSuccess = true,
                    Result = result,
                    Message = GeneralMessages.ItemInserted
                });
            }
            else {
                action = BadRequest(new ResponseDto()
                {
                    IsSuccess = false,
                    Result = ModelState,
                    Message = GeneralMessages.ItemNoInserted
                });
            }

            return action;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, CreateMarketingDto model) {
            IActionResult action;

            if (ModelState.IsValid)
            {
                var result = await _marketingService.EditMarketing(id, model);
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

        [HttpPost("{id}/SetOAuthSource")]
        public async Task<IActionResult> SetOAuthSource(int id, OAuth1Dto model) {
            IActionResult action;

            if (ModelState.IsValid)
            {
                var result = await _marketingService.SetOAuthSource(id, model);
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

        [HttpGet("{id}/GetOAuthSource")]
        public async Task<IActionResult> GetOAuthSource(int id)
        {
            var result = await _marketingService.GetOAuthSource(id);

            return Ok(result);
        }

        [HttpGet("{id}/SetMetaCredentials")]
        public async Task<IActionResult> SetMetaCredentials(int id, MetaCretentialDto model) {
            IActionResult action;


            if (ModelState.IsValid)
            {
                var result = await _marketingService.SetMetaCredentials(id, model);
                action = Ok(new ResponseDto()
                {
                    IsSuccess = true,
                    Result = result,
                    Message = GeneralMessages.ItemUpdated
                });
            }
            else
            {
                action = BadRequest(new ResponseDto()
                {
                    IsSuccess = false,
                    Result = ModelState,
                    Message = GeneralMessages.ItemNoUpdated
                });
            }

            return action;
        }

        [HttpGet("Campaign/GetMarketingCampaigns/{marketingId}")]
        public IActionResult GetMarketingCampaigns(int marketingId)
        {
            var result = _marketingService.GetMarketingCampaigns(marketingId);

            IActionResult action = Ok(new ResponseDto()
            {
                IsSuccess = true,
                Result = result,
                Message = GeneralMessages.ItemInserted
            });

            return action;
        }

        [HttpGet("Campaign/GetMarketingCampaign/{id}")]
        public IActionResult GetMarketingCampaign(int id)
        {
            return Ok(_marketingService.GetMarketingCampaign(id));
        }

        [HttpGet("Campaign/GetMarketingUsers/{marketingCampaignId}")]
        public IActionResult GetMarketingUsers(int marketingCampaignId)
        {
            var result = _marketingService.GetMarketingUsers(marketingCampaignId);

            IActionResult action = Ok(new ResponseDto()
            {
                IsSuccess = true,
                Result = result,
                Message = GeneralMessages.ItemInserted
            });

            return action;
        }

        [HttpGet("Campaign/GetMarketingTemplate/{marketingCampaignId}")]
        public IActionResult GetMarketingTemplate(int marketingCampaignId) {
            return Ok(_marketingService.GetMarketingTemplate(marketingCampaignId));
        }


        [HttpPost("Campaign/{marketingCampaignId}/UploadFileForTemplate")]
        public async Task<IActionResult> UploadFileForTemplate(IFormFile file, int marketingCampaignId)
        {
            IActionResult action;
            

            if (ModelState.IsValid)
            {
                var result = await _marketingService.UploadFileForTemplate(file, marketingCampaignId);
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

        [HttpPost("Campaign/EditTemplate")]
        public async Task<IActionResult> EditTemplate(CreateTemplateDto model) {
            IActionResult action;
            
            if (ModelState.IsValid)
            {
                await _marketingService.EditTemplate(model);
                var result = 1;
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

        [HttpPost("Campaign/SetRecurring")]
        public async Task<IActionResult> SetRecurring(RecurringDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDto
                {
                    IsSuccess = false,
                    Result = ModelState,
                    Message = GeneralMessages.ItemNoUpdated
                });
            }

            var result = await _marketingService.SetRecurring(model);
            var isSuccess = result == 1;

            return Ok(new ResponseDto
            {
                IsSuccess = isSuccess,
                Result = result,
                Message = isSuccess ? GeneralMessages.ItemUpdated : GeneralMessages.ItemNoUpdated
            });
        }

        [HttpGet("{id}/SyncCampaigns")]
        public async Task<IActionResult> SyncCampaigns(int id) {
            var result = await _marketingService.LoadMarketingCampaign(id);
            return Ok(result);
        }





        [HttpGet("test")]
        public async Task<IActionResult> Test() {
            //await _marketingService.LoadMarketingCampaign();
            await _marketingService.SendingMessages();
            return Ok("d");
        }
    }
}
