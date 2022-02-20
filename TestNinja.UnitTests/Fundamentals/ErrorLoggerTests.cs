using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals;

[TestFixture]
public class ErrorLoggerTests
{
    private ErrorLogger logger;

    [SetUp]
    public void SetUp()
    {
        logger = new ErrorLogger();
    }

    [Test]
    [TestCase("my error message")]
    public void Log_WhenCalled_SetTheLastErrorProperty(string errorMessage)
    {
        // Act
        logger.Log(errorMessage);

        // Assert 
        Assert.AreEqual(errorMessage, logger.LastError);
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void Log_InvalidError_ThrowArgumentNullException(string errorMessage)
    {
        // Assert 
        Assert.Throws<ArgumentNullException>(() => logger.Log(errorMessage));
    }

    [Test]
    public void Log_ValidError_RaiseErrorLoggedEvent()
    {
        // Arrange
        var id = Guid.Empty;
        logger.ErrorLogged += (sender, args) => { id = args; };

        // Act
        logger.Log("my error");

        // Assert 
        Assert.That(id, Is.Not.EqualTo(Guid.Empty));
    }
}
