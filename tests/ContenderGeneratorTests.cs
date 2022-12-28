using lab2;
using NUnit.Framework;

namespace lab3_tests;

[TestFixture]
public class ContenderGeneratorTests
{
    private ContenderGenerator _contenderGenerator;
    
    [SetUp]
    public void CreateContenderGenerator()
    {
        _contenderGenerator = new ContenderGenerator();
    }
    
    [Test]
    public void GenerateContenders_100_UniqueName()
    {
        var contenders = _contenderGenerator.GenerateContenders();
        
        // List size assert
        const int expectedCountContenders = 100;
        var actualCountContenders = contenders.Count;
        Assert.AreEqual(expectedCountContenders, actualCountContenders);
        
        // Checking unique name
        var names = new HashSet<string>();
        foreach (var contender in contenders)
        {
            names.Add(contender.Name);
        }
        const int expectedCountUniqueNames = 100;
        var actualCountUniqueNames = names.Count;
        Assert.AreEqual(expectedCountUniqueNames, actualCountUniqueNames);
    }
}