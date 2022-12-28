using lab2.friend;
using lab2.hall;
using lab4_db.model;
using Microsoft.EntityFrameworkCore;

namespace lab2;

public class PrincessSimulator: IPrincessSimulator
{
    public int LaunchSavedAttempt(string attemptName, IHall hall, IFriend friend, IPrincessStrategy strategy, DataContext dbContext)
    {
        // Getting attempt entity from db
        var princessAttemptEntity = dbContext.Attempts.FirstOrDefault(e => e.Name == attemptName);
        if (princessAttemptEntity == null)
        {
            return -1;
        }

        // Attempt logic
        dbContext.Entry(princessAttemptEntity).Collection(x => x.Сontenders).Load();
        hall.Contenders = princessAttemptEntity.Сontenders;
        var groom = strategy.ChooseGroom(hall, friend);
        return HappinessCalculator.CalcPrincessHappinessLvl(princessAttemptEntity.Сontenders, groom);
    }

    public void GenerateAttempts(int countAttempts, IContenderGenerator generator, DataContext dbContext)
    {
        dbContext.Database.EnsureCreated();

        // Removing all old entities from table if exists
        foreach (var entity in dbContext.Attempts)
        {
            dbContext.Attempts.Remove(entity);
        }
        dbContext.SaveChanges();

        // Generating attempts and add in db
        for (var i = 0; i < countAttempts; ++i)
        {
            var contenders = generator.GenerateContenders();
            var attemptNumber = i + 1;
            var attemptEntity = new PrincessAttemptEntity
            {
                Name = "attempt" + attemptNumber,
                Сontenders = contenders
            };
            dbContext.Attempts.Add(attemptEntity);
        }
        dbContext.SaveChanges();
    }
}