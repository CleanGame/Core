namespace CleanGame.Domain.Shared.Interfaces;

public interface ICacheTransaction 
{
    ICache Cache { get; }
    Task<bool> ExecuteAsync();
}