namespace lab2;

public class LaunchServiceConfig
{
    public List<string> AttemptNames { get; set; }

    public bool IsDefaultConfig()
    {
        return AttemptNames.Count == 0;
    } 
}