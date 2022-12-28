using lab2;
using NUnit.Framework;

namespace lab3_tests;

[TestFixture]
public class HallTests
{
    private int _curContendersListSize;
    private Hall _hall;

    [SetUp]
    public void CreateHall()
    {
        _hall = new Hall(new ContenderGenerator().GenerateContenders());

        // Should be 100
        _curContendersListSize = _hall.CountLeftContenders;
        Assert.AreEqual(100, _curContendersListSize);
    }

    [Test]
    public void CallNext_Contender_ListDecreases()
    {
        // Check on decreases list size on call NextContender()
        _hall.NextContender();
        var exceptedCountContenders = _curContendersListSize - 1;
        var actualCountContenders = _hall.CountLeftContenders;
        Assert.AreEqual(exceptedCountContenders, actualCountContenders);
        _curContendersListSize -= 1;

        // Skip 5 contenders
        for (int i = 0; i < 5; ++i)
        {
            _hall.NextContender();
        }

        exceptedCountContenders = _curContendersListSize - 5;
        actualCountContenders = _hall.CountLeftContenders;
        Assert.AreEqual(exceptedCountContenders, actualCountContenders);
        _curContendersListSize -= 5;
    }

    [Test]
    public void CallNext_Contender_NoContenders()
    {
        // Skip 100 contenders
        for (int i = 0; i < _curContendersListSize; ++i)
        {
            Assert.IsNotNull(_hall.NextContender());
        }

        // Try to get 101th
        Assert.IsNull(_hall.NextContender());
    }
}