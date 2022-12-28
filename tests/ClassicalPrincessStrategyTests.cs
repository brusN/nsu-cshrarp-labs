using lab2;
using NUnit.Framework;

namespace lab3_tests;

[TestFixture]
public class PrincessTests
{
    private ClassicalPrincessStrategy _strategy;
    private Friend _friend;
    private Hall _hall;

    [SetUp]
    public void CreatePrincess()
    {
        _strategy = new ClassicalPrincessStrategy();
        _friend = new Friend();
        _hall = new Hall();
        _friend.PrincessRejectedContenders = _hall.RejectedContenders;
    }

    [Test]
    public void RightStrategy_TenContenders1_10()
    {
        _hall.Contenders = new List<Contender>
        {
            new Contender() { Name = "Bob1", Attraction = 100},
            new Contender() { Name = "Bob2", Attraction = 100},
            new Contender() { Name = "Bob3", Attraction = 100},
            new Contender() { Name = "Bob4", Attraction = 15},
            new Contender() { Name = "Bob5", Attraction = 13},
            new Contender() { Name = "Bob6", Attraction = 1},
            new Contender() { Name = "Bob7", Attraction = 1},
            new Contender() { Name = "Bob8", Attraction = 51},
            new Contender() { Name = "Bob9", Attraction = 91},
            new Contender() { Name = "Bob10", Attraction = 99}
        };

        var groom = _strategy.ChooseGroom(_hall, _friend);
        Assert.IsNull(groom);
    }
    
    [Test]
    public void RightStrategy_TenContenders2_70()
    {
        _hall.Contenders = new List<Contender>
        {
            new Contender() { Name = "Bob1", Attraction = 30},
            new Contender() { Name = "Bob2", Attraction = 30},
            new Contender() { Name = "Bob3", Attraction = 30},
            new Contender() { Name = "Bob4", Attraction = 30},
            new Contender() { Name = "Bob5", Attraction = 13},
            new Contender() { Name = "Bob6", Attraction = 70},
            new Contender() { Name = "Bob7", Attraction = 90},
            new Contender() { Name = "Bob8", Attraction = 90},
            new Contender() { Name = "Bob9", Attraction = 100},
            new Contender() { Name = "Bob10", Attraction = 99}
        };

        var groom = _strategy.ChooseGroom(_hall, _friend);
        Assert.IsNotNull(groom);
        Assert.AreEqual(70, groom.Attraction);
    }
    
    [Test]
    public void RightStrategy_TenContenders3_100()
    {
        _hall.Contenders = new List<Contender>
        {
            new Contender() { Name = "Bob1", Attraction = 90},
            new Contender() { Name = "Bob2", Attraction = 90},
            new Contender() { Name = "Bob3", Attraction = 90},
            new Contender() { Name = "Bob4", Attraction = 1},
            new Contender() { Name = "Bob5", Attraction = 100},
            new Contender() { Name = "Bob6", Attraction = 1},
            new Contender() { Name = "Bob7", Attraction = 1},
            new Contender() { Name = "Bob8", Attraction = 51},
            new Contender() { Name = "Bob9", Attraction = 91},
            new Contender() { Name = "Bob10", Attraction = 99}
        };

        var groom = _strategy.ChooseGroom(_hall, _friend);
        Assert.IsNotNull(groom);
        Assert.AreEqual(100, groom.Attraction);
    }
}