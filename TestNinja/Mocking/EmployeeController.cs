﻿using System.Data.Entity;

namespace TestNinja.Mocking;

public class EmployeeController
{
    private readonly IEmployeeStorage _storage;

    public EmployeeController(IEmployeeStorage employeeStorage)
    {
        _storage = employeeStorage;
    }

    public ActionResult DeleteEmployee(int id)
    {
        _storage.Delete(id);
        return RedirectToAction("Employees");
    }

    private ActionResult RedirectToAction(string employees)
    {
        return new RedirectResult();
    }
}

public class ActionResult { }

public class RedirectResult : ActionResult { }

public class EmployeeContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
}

public class Employee
{
}