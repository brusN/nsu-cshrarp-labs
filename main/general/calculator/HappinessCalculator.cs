namespace lab2;

public class HappinessCalculator
{
    private static List<Contender> GetSortedContendersList(in List<Contender> contenders)
    {
        return contenders.OrderByDescending(x => x.Attraction).ToList();
    }

    public static int CalcPrincessHappinessLvl(in List<Contender> contenders, in Contender? princessGroom)
    {
        
        if (princessGroom == null)
        {
            return 10;
        }
        var index = GetSortedContendersList(contenders).IndexOf(princessGroom);
        if (index >= 50)
        {
            return 0;
        }
        return contenders.Count - index;
    }
}