using lab2.friend;
using lab2.hall;

namespace lab2;

public class ClassicalPrincessStrategy : IPrincessStrategy
{
    public Contender? ChooseGroom(IHall hall, IFriend friend)
    {
        // Skip first n / e % contenders
        int countContenders = hall.CountLeftContenders;
        Contender? bestContender = hall.NextContender();
        hall.RejectedContenders.Add(bestContender); // Reject first
        Contender? curContender;
        for (var i = 1; i < Math.Floor(countContenders / Math.E); ++i)
        {
            curContender = hall.NextContender();
            if (friend.IsSecondContenderBetter(bestContender, curContender))
            {
                bestContender = curContender;
            }
            hall.RejectedContenders.Add(curContender); // Reject first n/e %
        }                                                        
        
        // Need to find better, than the best of first n/e %
        curContender = hall.NextContender();
        while (curContender != null)
        {
            if (friend.IsSecondContenderBetter(bestContender, curContender)) 
                return curContender;
            
            hall.RejectedContenders.Add(curContender);
            curContender = hall.NextContender();
        }
        
        return null;
    }
}