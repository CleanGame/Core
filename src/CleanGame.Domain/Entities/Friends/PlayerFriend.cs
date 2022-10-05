using CleanGame.Domain.Base;
using CleanGame.Domain.Entities.Players;

namespace CleanGame.Domain.Entities.Friends;

public class PlayerFriend : AuditableEntity<PlayerFriend>
{
    public Player Player { get; private set; }
    public Player Friend { get; private set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerFriend"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private PlayerFriend()
    {
    }

    internal PlayerFriend(Player player, Player friend)
    {
        Player = player;
        Friend = friend;
    }
}