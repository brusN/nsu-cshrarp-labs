using lab2.friend;

namespace lab2;

public class Friend: IFriend
{
    public List<Contender> PrincessRejectedContenders { get; set; }
    public Friend()
    {
        PrincessRejectedContenders = null!;
        int x = 3;
    }

    public bool IsSecondContenderBetter(Contender contender1, Contender contender2)
    {
        if (!PrincessRejectedContenders.Contains(contender1))
        {
            throw new ArgumentException("First contender didn't visit princess");
        }
        return contender2.Attraction > contender1.Attraction;
    }
}