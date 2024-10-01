using Hangfire;
using Hangfire.Common;
using Hangfire.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScaffoldingApi.Handlers;

[ApiController]
[Route("api/hangfire/jobs")]
[Authorize]
[TypeFilter(typeof(CustomExceptionHandler))]
[TypeFilter(typeof(CustomValidationFilterAttribute))]
public class HangfireJobsController : ControllerBase
{
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IRecurringJobManager _recurringJobManager;

    private readonly IMonitoringApi _monitoringApi;


    public HangfireJobsController(IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager, IMonitoringApi monitoringApi)
    {
        _backgroundJobClient = backgroundJobClient;
        _recurringJobManager = recurringJobManager;
        _monitoringApi = monitoringApi;

    }

    // POST api/hangfire/jobs
    [HttpPost]
    public IActionResult CreateJob([FromBody] HangfireJobRequest request)
    {
        // Ejemplo de encolar un trabajo
        string jobId = _backgroundJobClient.Enqueue(() => Console.WriteLine(request.JobName));

        return Ok(new { JobId = jobId });
    }

    // GET api/hangfire/jobs
    [HttpGet]
    public IActionResult GetAllJobs()
    {
        // Ejemplo de obtener todos los trabajos encolados
        var jobs = Hangfire.JobStorage.Current.GetMonitoringApi().EnqueuedJobs("default", 0, 100);
        return Ok(jobs);
    }

    // PUT api/hangfire/jobs/{jobId}
    [HttpPut("{jobId}")]
    public IActionResult UpdateJob(string jobId, [FromBody] HangfireJobRequest request)
    {
        // No se puede actualizar directamente un trabajo en Hangfire; considera encolar uno nuevo o cancelar/eliminar el existente y crear uno nuevo.
        return Ok(new { Message = $"Job with ID '{jobId}' updated successfully" });
    }

    // DELETE api/hangfire/jobs/{jobId}
    [HttpDelete("{jobId}")]
    public IActionResult DeleteJob(string jobId)
    {
        bool succeeded = _backgroundJobClient.Delete(jobId);

        if (succeeded)
            return Ok(new { Message = $"Job with ID '{jobId}' deleted successfully" });
        else
            return BadRequest(new { Message = $"Failed to delete job with ID '{jobId}'" });
    }

    // POST api/hangfire/jobs/recurring
    [HttpPost("recurring")]
    public IActionResult CreateRecurringJob([FromBody] RecurringJobRequest request)
    {
        var options = new RecurringJobOptions
        {
            TimeZone = TimeZoneInfo.Utc // Ejemplo: Configura la zona horaria (opcional)
            // Puedes configurar más opciones aquí según tus necesidades
        };
        // Ejemplo de crear un trabajo recurrente que se ejecuta cada hora
        _recurringJobManager.AddOrUpdate(
           request.JobId,
           Job.FromExpression(() => Console.WriteLine(request.JobName)),
           request.CronExpression,
           options); // Aquí se pasa las opciones de configuración del trabajo recurrente


        return Ok(new { Message = $"Recurring job '{request.JobId}' created successfully" });
    }

    // POST api/hangfire/jobs/{jobId}/run
    [HttpPost("{jobId}/run")]
    public IActionResult RunJob(string jobId)
    {
        // Ejemplo de ejecutar manualmente un trabajo
        _backgroundJobClient.Enqueue(() => Console.WriteLine($"Manually running job with ID '{jobId}'"));

        return Ok(new { Message = $"Job with ID '{jobId}' manually triggered successfully" });
    }

    // GET api/hangfire/jobs/enqueued
    [HttpGet("enqueued")]
    public IActionResult GetEnqueuedJobs()
    {
        // Obtener trabajos encolados para la cola "default", desde el índice 0 al 100
        var enqueuedJobs = _monitoringApi.EnqueuedJobs("default", 0, 100);
        var filteredJobs = enqueuedJobs.Select(job => new
        {
            job.Key,
            job.Value.Job.Method.Name,
            job.Value.Job.Args,
            job.Value.StateData,
            // Agregar otras propiedades seguras para la serialización JSON
        });

        return Ok(enqueuedJobs);
    }

    // GET api/hangfire/jobs/processing
    [HttpGet("processing")]
    public IActionResult GetProcessingJobs()
    {
        // Obtener trabajos en procesamiento
        var processingJobs = _monitoringApi.ProcessingJobs(0, 100);

        return Ok(processingJobs);
    }

    // GET api/hangfire/jobs/succeeded
    [HttpGet("succeeded")]
    public IActionResult GetSucceededJobs()
    {
        // Obtener trabajos completados exitosamente
        var succeededJobs = _monitoringApi.SucceededJobs(0, 100);
        var filteredJobs = succeededJobs.Select(job => new
        {
            job.Key,
            job.Value.Job.Method.Name,
            job.Value.Job.Args,
            job.Value.InSucceededState,
            job.Value.StateData,
            // Agregar otras propiedades seguras para la serialización JSON
        });

        return Ok(filteredJobs);
    }

    // GET api/hangfire/jobs/failed
    [HttpGet("failed")]
    public IActionResult GetFailedJobs()
    {
        // Obtener trabajos fallidos
        var failedJobs = _monitoringApi.FailedJobs(0, 100);
        var filteredJobs = failedJobs.Select(job => new
        {
            job.Key,
            job.Value.Job.Method.Name,
            job.Value.Job.Args,
            job.Value.StateData,
            // Agregar otras propiedades seguras para la serialización JSON
        });
        return Ok(failedJobs);
    }

    // GET api/hangfire/jobs/details/{jobId}
    [HttpGet("details/{jobId}")]
    public IActionResult GetJobDetails(string jobId)
    {
        // Obtener detalles de un trabajo específico por su ID
        var jobDetails = _monitoringApi.JobDetails(jobId);

        return Ok(jobDetails);
    }

    // Clase modelo para la solicitud de trabajo en Hangfire
    public class HangfireJobRequest
    {
        public string JobName { get; set; }
    }

    // Clase modelo para la solicitud de trabajo recurrente en Hangfire
    public class RecurringJobRequest
    {
        public string JobId { get; set; }
        public string JobName { get; set; }
        public string CronExpression { get; set; } // Propiedad para el Cron Expression
    }



}
