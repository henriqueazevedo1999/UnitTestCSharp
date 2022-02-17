using NUnit.Framework;
using Fundamentals.Classes;

namespace Fundamentals.UnitTests;

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
    public void Max_WhenCalled_ShouldReturnTheGreaterArgument(int a, int b, int expectedResult)
    {
        var result = math.Max(a, b);

        Assert.AreEqual(expectedResult, result);
    }
}
