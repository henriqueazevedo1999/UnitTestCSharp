using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class EmployeeControllertests
{
    private Mock<IEmployeeStorage> storage;
    private EmployeeController employeeController;

    [SetUp]
    public void SetUp()
    {
        storage = new Mock<IEmployeeStorage>();
        employeeController = new EmployeeController(storage.Object);
    }

    [Test]
    public void DeleteEmployee_WhenCalled_DeleteEmployeeFromDatabase()
    {
        // Act
        employeeController.DeleteEmployee(1);

        // Assert 
        storage.Verify(s => s.Delete(1));
    }
}
