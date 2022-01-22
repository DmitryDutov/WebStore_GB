using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.TestAPI;

namespace WebStore.Controllers;
public class WebAPIController:Controller
{
    private readonly IValuesService _ValueService;

    public WebAPIController(IValuesService ValueService) => _ValueService = ValueService;

    public IActionResult Index()
    {
        var values = _ValueService.GetValues();
        return View(values);
    }
}
