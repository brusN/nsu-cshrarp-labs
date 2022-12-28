using lab2.friend;
using lab2.hall;

namespace lab2;

public interface IPrincessSimulator
{
    int LaunchSavedAttempt(string attemptName, IHall hall, IFriend friend, IPrincessStrategy strategy, DataContext dbContext);
    void GenerateAttempts(int countAttempts, IContenderGenerator generator, DataContext dbContext);
}