using Application.Interfaces;
using Core.DTOs.TmetricDtos;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using MongoDB.Driver;
using Application.Services;

namespace ScaffoldingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController : ControllerBase 
    {
        private readonly ILogService _logService;

        public LogsController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateLog(LogDto logEntry) 
        {
            logEntry.Timestamp = DateTime.UtcNow;
            await _logService.AddLog(logEntry);
            return CreatedAtAction(nameof(CreateLog), logEntry);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogDto>>> GetLogs()
        {
            var logs = await _logService.GetAllLogs();
            if (logs == null)
            {
                return NotFound();
            }
            return Ok(logs);
        }

    }
}
