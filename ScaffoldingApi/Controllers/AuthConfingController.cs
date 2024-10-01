using Core.DTOs.OAuth;
using Core.Entities.Oauth;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace ScaffoldingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthConfigController : ControllerBase
    {
        private readonly IAuthConfigService _AuthConfigService;

        public AuthConfigController(IAuthConfigService AuthConfigService)
        {
            _AuthConfigService = AuthConfigService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthConfigEntity>>> GetAll()
        {
            var AuthConfigs = await _AuthConfigService.GetAllAuthConfigsAsync();
            return Ok(AuthConfigs);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<AuthConfigEntity>> GetById(string id)
        {
            var objectId = new ObjectId(id);
            var AuthConfig = await _AuthConfigService.GetAuthConfigByIdAsync(objectId);
            return AuthConfig == null ? NotFound() : Ok(AuthConfig);
        }

        [HttpGet("GetByConnectionName{ConnectionName}")]
        public async Task<ActionResult<AuthConfigEntity>> GetByConnectionName(string ConnectionName)
        {
            var AuthConfig = await _AuthConfigService.GetAuthConfigByConnectionNameAsync(ConnectionName);
            return AuthConfig == null ? NotFound() : Ok(AuthConfig);
        }

        [HttpPost("oauth1")]
        public async Task<ActionResult> CreateOAuth1(OAuthDto<OAuth1Dto> model)
        {
            if (ModelState.IsValid)
            {
                var result = await _AuthConfigService.CreateAuthConfigAsync(model);
                return CreatedAtAction(nameof(GetById), new { id = result.AuthConfigId.ToString() }, result);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("bearerToken")]
        public async Task<ActionResult> CreateBearerToken(OAuthDto<OAuthBearerDto> model)
        {
            if (ModelState.IsValid)
            {
                var result = await _AuthConfigService.CreateAuthConfigAsync(model);
                return CreatedAtAction(nameof(GetById), new { id = result.AuthConfigId.ToString() }, result);
            }

            return BadRequest(ModelState);

        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, AuthConfigEntity AuthConfig)
        {
            var objectId = new ObjectId(id);
            var existingAuthConfig = await _AuthConfigService.GetAuthConfigByIdAsync(objectId);
            if (existingAuthConfig == null)
            {
                return NotFound();
            }

            await _AuthConfigService.UpdateAuthConfigAsync(objectId, AuthConfig);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var objectId = new ObjectId(id);
            var AuthConfig = await _AuthConfigService.GetAuthConfigByIdAsync(objectId);
            if (AuthConfig == null)
            {
                return NotFound();
            }

            await _AuthConfigService.DeleteAuthConfigAsync(objectId);
            return NoContent();
        }
    }
}