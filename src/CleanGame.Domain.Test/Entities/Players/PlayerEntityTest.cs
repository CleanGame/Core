using CleanGame.Domain.Entities.Players;
using CleanGame.Domain.Entities.Players.Enums;
using FluentAssertions;
using FluentAssertions.Execution;

namespace CleanGame.Domain.Test.Entities.Players;

public class PlayerEntityTest
{
    [Fact]
    public void Create_Player_Success()
    {
        //arrange
        var nickName="TestName";
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
        player.WinGame.Should().Be(0);
        player.LoseGame.Should().Be(0);
    }
}