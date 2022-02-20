using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals;

[TestFixture]
public class FizzBuzzTests
{
    [Test]
    public void GetOutput_WhenInputIsDivisibleBy3And5_ReturnsFizzBuzz()
    {
        // Act
        var result = FizzBuzz.GetOutput(15);

        // Assert 
        Assert.That(result, Is.EqualTo("FizzBuzz"));
    }

    [Test]
    public void GetOutput_WhenInputIsDivisibleBy3_ReturnsFizz()
    {
        // Act
        var result = FizzBuzz.GetOutput(9);

        // Assert 
        Assert.That(result, Is.EqualTo("Fizz"));
    }

    [Test]
    public void GetOutput_WhenInputIsDivisibleBy5_ReturnsBuzz()
    {
        // Act
        var result = FizzBuzz.GetOutput(10);

        // Assert 
        Assert.That(result, Is.EqualTo("Buzz"));
    }

    [Test]
    public void GetOutput_WhenInputIsNotDivisibleBy3Neither5_ReturnsTheStringOfNumber()
    {
        // Act
        var result = FizzBuzz.GetOutput(11);

        // Assert 
        Assert.That(result, Is.EqualTo("11"));
    }
}
