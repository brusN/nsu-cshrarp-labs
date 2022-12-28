namespace lab2.hall;

public interface IHall
{
    public List<Contender>? RejectedContenders { get; set; }
    public List<Contender>? Contenders { get; set; }
    public int CountLeftContenders { get; }
    Contender? NextContender();
}