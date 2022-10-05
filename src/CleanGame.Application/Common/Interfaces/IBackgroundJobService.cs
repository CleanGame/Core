using System.Linq.Expressions;

namespace CleanGame.Application.Common.Interfaces;

public interface IBackgroundJobService
{
    void Enqueue(Expression<Func<Task>> methodCall);
    void Schedule(Expression<Func<Task>> methodCall, TimeSpan delay);
    void Schedule(Expression<Action> methodCall, TimeSpan delay);
    void RecurringJob(string jobName, Expression<Action> methodCall, string cronFormat);
    void RemoveRecurringJob(string jobName);
}