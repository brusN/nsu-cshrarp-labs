using lab2;
using NUnit.Framework;

namespace lab3_tests;

[TestFixture]
public class FriendTests
{
    private Friend _friend;
    
    [Test]
    public void CreateFriend()
    {
        _friend = new Friend();
    }

    [Test]
    public void RightContenderCompare_TwoContenders_Comparing1()
    {
        var contender1 = new Contender { Name = "Bob", Attraction = 2};
        var contender2 = new Contender { Name = "NeBob", Attraction = 4};
        var rejectedContendersList = new List<Contender> { contender1 };
        _friend.PrincessRejectedContenders = rejectedContendersList;
        
        // Contender with higher attraction lvl should be better
        var actualCompareResult = _friend.IsSecondContenderBetter(contender1, contender2);
        const bool expectedCompareResult = true;
        Assert.AreEqual(expectedCompareResult, actualCompareResult);
    }

    [Test]
    public void RightContenderCompare_TwoContenders_Comparing2()
    {
        var contender1 = new Contender { Name = "Bob", Attraction = 4};
        var contender2 = new Contender { Name = "NeBob", Attraction = 2};
        var rejectedContendersList = new List<Contender> { contender1 };
        _friend.PrincessRejectedContenders = rejectedContendersList;
        
        // Contender with higher attraction lvl should be better
        var actualCompareResult = _friend.IsSecondContenderBetter(contender1, contender2);
        const bool expectedCompareResult = false;
        Assert.AreEqual(expectedCompareResult, actualCompareResult);
    }
    
    [Test]
    public void RightContenderCompare_TwoContenders_SameAttractionLvl()
    {
        var contender1 = new Contender { Name = "Bob", Attraction = 4};
        var contender2 = new Contender { Name = "NeBob", Attraction = 4};
        var rejectedContendersList = new List<Contender> { contender1 };
        _friend.PrincessRejectedContenders = rejectedContendersList;
        
        // If attraction lvl same, then second not better, than first
        var actualCompareResult = _friend.IsSecondContenderBetter(contender1, contender2);
        const bool expectedCompareResult = false;
        Assert.AreEqual(expectedCompareResult, actualCompareResult);
    }
}