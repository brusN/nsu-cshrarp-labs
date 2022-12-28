namespace lab2;
public class ContenderGenerator: IContenderGenerator
{
    public List<Contender> GenerateContenders()
    {
        var contenders = new List<Contender>();
        try
        {
            var attractionValuesList = new List<int>(); 
            var streamReader = new StreamReader("files\\contenders.txt");
            var random = new Random();
            var line = streamReader.ReadLine();
            int randomNumber;
            while (line != null)
            {
                do
                {
                    randomNumber = random.Next(0, 1000);
                } while (attractionValuesList.Contains(randomNumber));

                var contender = new Contender
                {
                    Name = line,
                    Attraction = randomNumber
                };
                contenders.Add(contender);
                attractionValuesList.Add(randomNumber);
                line = streamReader.ReadLine();
            }
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine("FileNotFoundException: " + e.Message);
        }
        catch (IOException e)
        {
            Console.WriteLine("IOException: " + e.Message);
        }
        return contenders;
    }
}