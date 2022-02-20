using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals;

[TestFixture]
public class MathTests
{
    private Math math;

    [SetUp]
    public void SetUp()
    {
        math = new Math();
    }

    [Test]
    [TestCase(1, 2, 3)]
    public void Add_WhenCalled_ReturnTheSumOfArguments(int a, int b, int expectedResult)
    {
        var result = math.Add(a, b);

        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    [TestCase(2, 1, 2)]
    [TestCase(1, 2, 2)]
    [TestCase(1, 1, 1)]
    public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
    {
        var result = math.Max(a, b);

        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    [TestCase(3, 1, 3)]
    [TestCase(1, 1)]
    [TestCase(0)]
    [TestCase(-1)]
    public void GetOddNumbers_WhenCalled_ReturnAListOfOddNumbersFromZeroToLimit(int limit, params int[] expectedResult)
    {
        // Act
        var result = math.GetOddNumbers(limit);

        // Assert 
        Assert.That(result, Is.EquivalentTo(expectedResult));
        Assert.That(result, Is.Ordered);
        Assert.That(result, Is.Unique);
    }
}
