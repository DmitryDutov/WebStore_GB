using System.Net.Http.Json;
using WebStore.Domain.Entities;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Employees;

public class EmployeesClient:BaseClient, IEmployeesData
{
    public EmployeesClient(HttpClient Client) : base(Client, WebAPIAdresses.Employees)
    {
    }

    public IEnumerable<Employee> GetAll()
    {
        var employees = Get<IEnumerable<Employee>>(Addres);
        return employees!;
    }

    public Employee? GetById(int id)
    {
        var result=Get<Employee>($"{Addres}/{id}");
        return result;
    }

    public int Add(Employee employee)
    {
        var response = Post(Addres, employee);
        var added_employee = response.Content.ReadFromJsonAsync<Employee>().Result;
        if (added_employee is null)
            return -1;
        var id = added_employee.Id;
        employee.Id= id;
        return id;
    }

    public bool Edit(Employee employee)
    {
        var response = Put(Addres, employee);
        var succes = response.EnsureSuccessStatusCode()
            .Content
            .ReadFromJsonAsync<bool>()
            .Result;
        return succes;
    }

    public bool Delete(int id)
    {
        var response = Delete($"{Addres}/{id}");
        var succes = response.IsSuccessStatusCode;
        return succes;
    }
}
