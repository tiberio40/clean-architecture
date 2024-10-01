using Application;
using Core;
using Hangfire;
// using MongoDB.Driver;
using System;
using System.Collections.Generic;

public class RecurringJobService : IRecurringJobService
{
    // private readonly IMongoCollection<RecurringJobInfo> _recurringJobsCollection;

    public RecurringJobService()
    {
        // var database = mongoClient.GetDatabase("YourDatabaseName");
        // _recurringJobsCollection = database.GetCollection<RecurringJobInfo>("RecurringJobs");
    }

    public void ScheduleRecurringJobs()
    {
        // var recurringJobs = _recurringJobsCollection.Find(job => true).ToList();

        // foreach (var job in recurringJobs)
        // {
        //     Action jobAction = job.MethodName switch
        //     {
        //         "MiMetodoRecurrente" => Job.FromExpression<JobMethodsService>(x => x.MiMetodoRecurrente()),
        //         // Add other method mappings here
        //         _ => throw new ArgumentException($"No method found for {job.MethodName}")
        //     };

        //     RecurringJob.AddOrUpdate(job.Id, () => jobAction(), job.CronExpression);
        // }
    }
}
