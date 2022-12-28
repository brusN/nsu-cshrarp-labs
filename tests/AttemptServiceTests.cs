using lab2;
using lab2.friend;
using lab2.hall;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace lab3_tests;

[TestFixture]
internal class InMemoryWorldBehaviourServiceTests
{
    private readonly DbContextOptionsBuilder<DataContext> _optionsBuilder;
    private DataContext _context;
    private IPrincessSimulator _princessSimulator;
    private IHall _hall;
    private IFriend _friend;
    private readonly IPrincessStrategy _princessStrategy;
    private readonly IContenderGenerator _contenderGenerator;

    public InMemoryWorldBehaviourServiceTests()
    {
        _optionsBuilder = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "lab4");
        _princessSimulator = new PrincessSimulator();
        _contenderGenerator = new ContenderGenerator();
        _princessStrategy = new ClassicalPrincessStrategy();
    }

    [SetUp]
    public void InitDatabase()
    {
        _context = new DataContext(_optionsBuilder.Options);
        _hall = new Hall();
        _friend = new Friend();
        _friend.PrincessRejectedContenders = _hall.RejectedContenders;
    }

    [Test]
    public void GeneratingAttempts_100_AttemptsSavedInDb()
    {
        Assert.AreEqual(0, _context.Attempts.Count());
        
        const int requiredCountAttempts = 100;
        _princessSimulator.GenerateAttempts(requiredCountAttempts, _contenderGenerator, _context);
        _context.SaveChanges();
        Assert.AreEqual(100, _context.Attempts.Count());
    }

    [Test]
    public void GeneratingAttempts_100_SameCountContenders()
    {
        const int requiredCountAttempts = 100;
        _princessSimulator.GenerateAttempts(requiredCountAttempts, _contenderGenerator, _context);
        _context.SaveChanges();
        
        const int requiredCountContendersPerAttempt = 100;
        foreach (var entity in _context.Attempts)
        {
            Assert.AreEqual(requiredCountContendersPerAttempt, entity.Сontenders.Count);
        }
    }

    [Test]
    public void LaunchingAttemptByName_1_SuccessLaunch()
    {
        const int requiredCountAttempts = 100;
        _princessSimulator.GenerateAttempts(requiredCountAttempts, _contenderGenerator, _context);
        _context.SaveChanges();
        
        var princessAttemptEntity = _context.Attempts.FirstOrDefault(e => e.Name == "attempt1");
        Assert.NotNull(princessAttemptEntity);
        var attemptHappinessLvl = _princessSimulator.LaunchSavedAttempt("attempt1", _hall, _friend, _princessStrategy, _context);
        Assert.AreNotEqual(-1, attemptHappinessLvl);
    }
    
    [Test]
    public void LaunchingAttemptByName_1_NotFound()
    {
        const int requiredCountAttempts = 100;
        _princessSimulator.GenerateAttempts(requiredCountAttempts, _contenderGenerator, _context);
        _context.SaveChanges();
        
        var princessAttemptEntity = _context.Attempts.FirstOrDefault(e => e.Name == "attempt101");
        Assert.Null(princessAttemptEntity);
        var attemptHappinessLvl = _princessSimulator.LaunchSavedAttempt("attempt101", _hall, _friend, _princessStrategy, _context);
        Assert.AreEqual(-1, attemptHappinessLvl);
    }

    [TearDown]
    public void CleanContext()
    {
        _context.Database.EnsureDeleted();
    }
}