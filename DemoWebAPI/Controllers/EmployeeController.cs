using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPI.Controllers;

[ApiController]
public class EmployeeController : ControllerBase
{
    [HttpGet, Route("/GetEmployee/{id}/v2")]
    public IActionResult GetById(int id)
    {
        return Ok($"Get Employee by {id}");
    }

    [HttpGet, Route("/GetEmployees/v2")]
    public IActionResult GetAl()
    {
        return Ok($"Get All Employees ");
    }

    [HttpPost, Route("/CreateEmployee/v2")]
    public ActionResult<Employee> Create(Employee employee)
    {
        return Ok(employee);
    }

    [HttpPut, Route("/UpdateEmployee/v2")]
    public IActionResult Update(Employee employee)
    {
        return Ok($" update a Employee by {employee.Id} ");
    }

    [HttpDelete, Route("/DeleteEmployee/{id}/v2")]
    public IActionResult Delete(int id)
    {
        return Ok($" Delete a Employee by {id} ");
    }
}

public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }

    public DateTime CreatedDate { get; set; }
}