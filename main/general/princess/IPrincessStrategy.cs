using lab2.friend;
using lab2.hall;

namespace lab2;

public interface IPrincessStrategy
{
    public Contender? ChooseGroom(IHall hall, IFriend friend);
}