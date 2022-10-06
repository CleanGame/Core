using CleanGame.Domain.Base;
using CleanGame.Domain.Entities.Players.Enums;
using CleanGame.Domain.Entities.Players.Events;

namespace CleanGame.Domain.Entities.Players;

[CacheEntity(nameof(Player))]
public class Player : AuditableEntity<Player>
{
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
    public int TotalWinGame { get; private set; }

    /// <summary>
    /// Total lose game
    /// </summary>
    public int TotalLoseGame { get; private set; }

    /// <summary>
    /// Last player login
    /// </summary>
    public DateTime LastLogin { get; private set; }

    /// <summary>
    /// Last player logout
    /// </summary>
    public DateTime LastLogout { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Player"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private Player()
    {
    }

    public Player(string nickName, Player parent)
    {
        Id = Guid.NewGuid();
        Parent = parent;
        NickName = nickName;
        Status = PlayerStatusType.NotActive;
        Level = 0;
        Point = 0;
        TotalWinGame = 0;
        TotalLoseGame = 0;

        AddDomainEvent(new PlayerCreatedEvent(Id));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Player"/> class.
    /// </summary>
    /// <remarks>
    /// Required by json converter
    /// </remarks>    
    private Player(Guid id, string nickName, Player parent, PlayerStatusType status, int level,
        int point, int winGame, int loseGame, DateTime lastLogin, DateTime lastLogout)
    {
        Id = id;
        NickName = nickName;
        Parent = parent;
        Status = status;
        Level = level;
        Point = point;
        TotalWinGame = winGame;
        TotalLoseGame = loseGame;
        LastLogin = lastLogin;
        LastLogout = lastLogout;
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
        TotalWinGame++;
        AddDomainEvent(new PlayerWinGameEvent(Id, gameId));
    }

    public void IncLoseGames(Guid gameId)
    {
        TotalLoseGame++;
        AddDomainEvent(new PlayerLoseGameEvent(Id, gameId));
    }

    #endregion

    #region Login/logout

    public void SetPlayerLogin(IDateTime dateTime)
    {
        LastLogin = dateTime.Now;

        AddDomainEvent(new PlayerLoggedInEvent(Id));
    }

    public void SetPlayerLogout(IDateTime dateTime)
    {
        LastLogout = dateTime.Now; 

        AddDomainEvent(new PlayerLoggedOutEvent(Id));
    }

    #endregion
}