﻿using System.Net.Http.Json;
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
        var employees = Get<IEnumerable<Employee>>(Address);
        return employees!;
    }

    public Employee? GetById(int id)
    {
        var result=Get<Employee>($"{Address}/{id}");
        return result;
    }

    public int Add(Employee employee)
    {
        var response = Post(Address, employee);
        var added_employee = response.Content.ReadFromJsonAsync<Employee>().Result;
        if (added_employee is null)
            return -1;
        var id = added_employee.Id;
        employee.Id= id;
        return id;
    }

    public bool Edit(Employee employee)
    {
        var response = Put(Address, employee);
        var succes = response.EnsureSuccessStatusCode()
            .Content
            .ReadFromJsonAsync<bool>()
            .Result;
        return succes;
    }

    public bool Delete(int id)
    {
        var response = Delete($"{Address}/{id}");
        var succes = response.IsSuccessStatusCode;
        return succes;
    }
}
