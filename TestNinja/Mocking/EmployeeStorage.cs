namespace TestNinja.Mocking;

public interface IEmployeeStorage
{
    void Delete(int id);
}

public class EmployeeStorage : IEmployeeStorage
{
    private EmployeeContext _db;

    public EmployeeStorage(EmployeeContext employeeContext)
    {
        _db = employeeContext;  
    }

    public void Delete(int id)
    {
        var employee = _db.Employees.Find(id);
        if (employee == null)
            return;

        _db.Employees.Remove(employee);
        _db.SaveChanges();
    }
}
