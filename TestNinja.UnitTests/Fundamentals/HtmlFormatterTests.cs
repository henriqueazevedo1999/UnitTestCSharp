using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals;

[TestFixture]
public class HtmlFormatterTests
{
    [Test]
    public void FormatAsBold_WhenCalled_EncloseStringWithStrongElement()
    {
        // Arrange
        var formatter = new HtmlFormatter();

        // Act
        var result = formatter.FormatAsBold("abc");

        // Assert 
        Assert.That(result, Is.EqualTo("<strong>abc</strong>"));
    }
}
