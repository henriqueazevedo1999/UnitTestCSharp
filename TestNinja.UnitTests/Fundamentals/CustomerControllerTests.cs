using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals;

[TestFixture]
public class CustomerControllerTests
{
    [Test]
    [TestCase(0, typeof(NotFound))]
    [TestCase(1, typeof(Ok))]
    public void GetCustomer_WhenCalled_ReturnActionResult(int id, Type returnType)
    {
        // Arrange
        var customerController = new CustomerController();

        // Act
        var result = customerController.GetCustomer(id);

        // Assert 
        Assert.That(result, Is.TypeOf(returnType));
    }
}
