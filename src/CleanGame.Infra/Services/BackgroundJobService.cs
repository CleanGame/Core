using System.Linq.Expressions;
using CleanGame.Application.Shared.Interfaces;
using Hangfire;

namespace CleanGame.Infra.Services;

public class BackgroundJobService : IBackgroundJobService
{
    public void Enqueue(Expression<Func<Task>> methodCall)
    {
        BackgroundJob.Enqueue(methodCall);
    }

    public void Schedule(Expression<Func<Task>> methodCall, TimeSpan delay)
    {
        BackgroundJob.Schedule(methodCall, delay);
    }

    public void Schedule(Expression<Action> methodCall, TimeSpan delay)
    {
        BackgroundJob.Schedule(methodCall, delay);
    }

    public void RecurringJob(string jobName, Expression<Action> methodCall, string cronFormat)
    { 
        Hangfire.RecurringJob.AddOrUpdate(jobName,methodCall,cronFormat);
    }
    
    public void RemoveRecurringJob(string jobName)
    { 
        Hangfire.RecurringJob.RemoveIfExists(jobName);
    }
}