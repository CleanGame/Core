using CleanGame.Domain.Entities.Players;
using CleanGame.Domain.Entities.Players.Enums;
using CleanGame.Domain.Shared.Interfaces;
using FluentAssertions;
using Moq;

namespace CleanGame.Domain.Test.Entities.Players;

public class PlayerEntityTest
{
    [Fact]
    public void Player_Create_Success()
    {
        //arrange
        var nickName = "TestName";
        Player parent = null;

        //act
        var player = new Player(nickName, parent);

        //asset
        player.NickName.Should().Be(nickName);
        player.Parent.Should().Be(parent);
        player.Id.Should().NotBe(Guid.Empty);
        player.Status.Should().Be(PlayerStatusType.NotActive);
        player.Level.Should().Be(0);
        player.Point.Should().Be(0);
        player.TotalWinGame.Should().Be(0);
        player.TotalLoseGame.Should().Be(0);
        player.DomainEvents.Should()
            .NotBeEmpty()
            .And.HaveCount(1);
    }

    [Fact]
    public void Player_SetActive_Success()
    {
        //arrange
        var nickName = "TestName";
        Player parent = null;

        //act
        var player = new Player(nickName, parent);
        player.ClearDomainEvents();
        player.SetActive();

        //assert
        player.Status.Should().Be(PlayerStatusType.Active);
        player.DomainEvents.Should()
            .NotBeEmpty()
            .And.HaveCount(1);
    }

    [Fact]
    public void Player_SetDisable_Success()
    {
        //arrange
        var nickName = "TestName";
        Player parent = null;

        //act
        var player = new Player(nickName, parent);
        player.ClearDomainEvents();
        player.SetDisable();

        //assert
        player.Status.Should().Be(PlayerStatusType.Disable);
        player.DomainEvents.Should()
            .NotBeEmpty()
            .And.HaveCount(1);
    }

    [Fact]
    public void Player_Inc_TotalWinGames_Success()
    {
        //arrange
        var nickName = "TestName";
        Player parent = null;

        //act
        var player = new Player(nickName, parent);
        player.ClearDomainEvents();
        player.IncWinGames(Guid.NewGuid());

        //assert
        player.TotalWinGame.Should().Be(1);
        player.DomainEvents.Should()
            .NotBeEmpty()
            .And.HaveCount(1);
    }

    [Fact]
    public void Player_Inc_LoseWinGames_Success()
    {
        //arrange
        var nickName = "TestName";
        Player parent = null;

        //act
        var player = new Player(nickName, parent);
        player.ClearDomainEvents();
        player.IncLoseGames(Guid.NewGuid());

        //assert
        player.TotalLoseGame.Should().Be(1);
        player.DomainEvents.Should()
            .NotBeEmpty()
            .And.HaveCount(1);
    }

    [Fact]
    public void Player_Set_LastLogin_Success()
    {
        //arrange
        var nickName = "TestName";
        Player parent = null;
        var dateTime = new Mock<IDateTime>();
        var currentTime = DateTime.UtcNow;
        dateTime
            .Setup(time => time.Now)
            .Returns(currentTime);

        //act
        var player = new Player(nickName, parent);
        player.ClearDomainEvents();
        player.SetPlayerLogin(dateTime.Object);

        //assert
        player.LastLogin.Should().Be(currentTime);
        player.DomainEvents.Should()
            .NotBeEmpty()
            .And.HaveCount(1);
    }

    [Fact]
    public void Player_Set_LastLogout_Success()
    {
        //arrange
        var nickName = "TestName";
        Player parent = null;
        var dateTime = new Mock<IDateTime>();
        var currentTime = DateTime.UtcNow;
        dateTime
            .Setup(time => time.Now)
            .Returns(currentTime);

        //act
        var player = new Player(nickName, parent);
        player.ClearDomainEvents();
        player.SetPlayerLogout(dateTime.Object);

        //assert
        player.LastLogout.Should().Be(currentTime);
        player.DomainEvents.Should()
            .NotBeEmpty()
            .And.HaveCount(1);
    }
}