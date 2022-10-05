namespace CleanGame.Application.Common.Interfaces;

public interface ICacheTransaction 
{
    ICache Cache { get; }
    Task<bool> ExecuteAsync();
}