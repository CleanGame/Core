namespace CleanGame.Domain.Entities.Players.Enums;

/// <summary>
/// Player status for <see cref="Player"/>
/// </summary>
public enum PlayerStatusType
{
    /// <summary>
    /// Is new user and not active
    /// </summary>
    NotActive = 0,

    /// <summary>
    /// normal and active user
    /// </summary>
    Active = 1,

    /// <summary>
    /// Player is deactivated by admin
    /// </summary>
    Disable = 2,

    /// <summary>
    /// Deleted account
    /// </summary>
    Delete = 3
}