using CleanGame.Domain.Base;
using CleanGame.Domain.Entities.Players.Enums;
using CleanGame.Domain.Entities.Players.Events;

namespace CleanGame.Domain.Entities.Players;

[CacheEntity("Player")]
public class Player : AuditableEntity<Player>
{
    /// <summary>
    /// Player mobile
    /// </summary>
    public string Mobile { get; private set; }

    /// <summary>
    /// Parent player
    /// </summary>
    [JsonIgnore]
    public Player Parent { get; private set; }

    /// <summary>
    /// Player accounting status
    /// </summary>
    public PlayerStatusType Status { get; private set; }

    /// <summary>
    /// Player nick name
    /// </summary>
    public string NickName { get; private set; }

    /// <summary>
    /// Level
    /// </summary>
    public int Level { get; private set; }

    /// <summary>
    /// Current level point
    /// </summary>
    public int Point { get; private set; }

    /// <summary>
    /// Total win games
    /// </summary>
    public int WinGame { get; private set; }

    /// <summary>
    /// Total lost game
    /// </summary>
    public int LoseGame { get; private set; }

    /// <summary>
    /// Last active time
    /// </summary>
    public DateTime LastActive { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Player"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private Player()
    {
    }

    public Player(string mobile, string nickName, Player parent)
    {
        Id = Guid.NewGuid();
        Mobile = mobile;
        Parent = parent;
        NickName = nickName;
        Status = PlayerStatusType.NotActive;
        Level = 0;
        Point = 0;
        WinGame = 0;
        LoseGame = 0;
        LastActive = DateTime.UtcNow;

        AddDomainEvent(new PlayerCreatedEvent(Id));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Player"/> class.
    /// </summary>
    /// <remarks>
    /// Required by json converter
    /// </remarks>    
    private Player(Guid id, string mobile, string nickName, Player parent, PlayerStatusType status, int level,
        int point, int winGame, int loseGame, DateTime lastActive)
    {
        Id = id;
        Mobile = mobile;
        NickName = nickName;
        Parent = parent;
        Status = status;
        Level = level;
        Point = point;
        WinGame = winGame;
        LoseGame = loseGame;
        LastActive = lastActive;
    }

    #region Status

    public void SetActive()
    {
        Status = PlayerStatusType.Active;

        AddDomainEvent(new PlayerStatusChangedEvent(Id, Status));
    }

    public void SetDisable()
    {
        Status = PlayerStatusType.Disable;

        AddDomainEvent(new PlayerStatusChangedEvent(Id, Status));
    }

    #endregion

    #region Play game status

    public void IncWinGames(Guid gameId)
    {
        WinGame++;
        AddDomainEvent(new PlayerWinGameEvent(Id, gameId));
    }

    public void IncLoseGames(Guid gameId)
    {
        LoseGame++;
        AddDomainEvent(new PlayerLoseGameEvent(Id, gameId));
    }

    #endregion

    #region Login/logout

    public void SetPlayerLogin()
    {
        LastActive = DateTime.UtcNow;

        AddDomainEvent(new PlayerLoggedInEvent(Id));
    }

    public void SetPlayerLogout()
    {
        LastActive = DateTime.UtcNow;

        AddDomainEvent(new PlayerLoggedOutEvent(Id));
    }

    #endregion

}