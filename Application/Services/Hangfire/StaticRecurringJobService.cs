using Application;
using Hangfire;
using System;

public class StaticRecurringJobService : IStaticRecurringJobService
{
    public void ScheduleStaticRecurringJobs()
    {
        RecurringJob.AddOrUpdate<JobMethodsService>("mi-trabajo-estatico", x => x.MiMetodoEstatico(), Cron.Daily);

        RecurringJob.AddOrUpdate<JobMethodsService>("updateTemplateStatusFromCampaign", x => x.UpdateTemplateStatus(), "*/5 * * * *");

        //RecurringJob.AddOrUpdate<JobMethodsService>("sendingMessages", x => x.SendingMessages(), Cron.Minutely);

        // Add more static jobs here
    }
}
