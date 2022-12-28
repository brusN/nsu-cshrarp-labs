using lab2.hall;

namespace lab2;

public class Hall: IHall
{
    public List<Contender>? RejectedContenders { get; set; }
    public List<Contender>? Contenders { get; set; }
    private int _curContenderIndex;

    public Hall()
    {
        RejectedContenders = new List<Contender>();

    }
    public Hall(List<Contender> contenders)
    {
        Contenders = contenders;
        RejectedContenders = new List<Contender>();
    }
    
    public int CountLeftContenders => _curContenderIndex >= Contenders.Count ? 0 : Contenders.Count - _curContenderIndex;

    public Contender? NextContender()
    {
        return _curContenderIndex >= Contenders.Count ? null : Contenders[_curContenderIndex++];
    }
}