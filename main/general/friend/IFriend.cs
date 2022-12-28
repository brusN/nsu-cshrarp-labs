namespace lab2.friend;

public interface IFriend
{
    public List<Contender> PrincessRejectedContenders { get; set; }
    bool IsSecondContenderBetter(Contender contender1, Contender contender2);
}