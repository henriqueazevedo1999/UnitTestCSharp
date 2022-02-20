using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals;

[TestFixture]
public class DemeritPointsCalculatorTests
{
    private DemeritPointsCalculator calculator;

    [SetUp]
    public void SetUp()
    {
        calculator = new DemeritPointsCalculator();
    }

    [Test]
    [TestCase(-1)]
    [TestCase(301)]
    public void CalculateDemeritPoints_WhenOutOfRange_ThrowsArgumentOutOfRangeException(int speed)
    {
        // Assert 
        Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateDemeritPoints(speed));
    }

    [Test]
    [TestCase(0, 0)]
    [TestCase(10, 0)]
    [TestCase(65, 0)]
    [TestCase(66, 0)]
    [TestCase(70, 1)]
    [TestCase(75, 2)]
    public void CalculateDemeritPoints_WhenCalled_ReturnDemeritPoints(int speed, int expectedResult)
    {
        // Act
        var result = calculator.CalculateDemeritPoints(speed);

        // Assert 
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}
