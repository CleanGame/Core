using CleanGame.Domain.Base;
using CleanGame.Domain.Entities.Players;
using CleanGame.Domain.Shared.Assets;

namespace CleanGame.Domain.Entities.Assets;

public class PlayerAssets : AuditableEntity<PlayerAssets>
{
    public Player Player { get; private set; }
    public AssetType Type { get; private set; }
    public decimal Amount { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerAssets"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private PlayerAssets()
    {
    }

    public PlayerAssets(Player player, AssetType type)
    {
        Player = player;
        Type = type;
        Amount = 0;
    }

    public void AddAmount(decimal amount)
    {
        Amount += amount;
    }
}