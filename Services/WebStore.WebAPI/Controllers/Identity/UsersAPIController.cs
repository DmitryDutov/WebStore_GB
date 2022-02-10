using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces;

namespace WebStore.WebAPI.Controllers.Identity;

[ApiController]
[Route(WebAPIAdresses.Identity.Users)]
public class UsersAPIController : ControllerBase
{
    ////Если ничего не определено, то можно имспользовать так:
    //private readonly UserStore _UserStore;
    //public UsersAPIController(WebStoreDB db)
    //{
    //    _UserStore = new UserStore(db);
    //}

    private readonly UserStore<User, Role, WebStoreDB> _UserStore;
    public UsersAPIController(WebStoreDB db)
    {
        _UserStore = new UserStore<User, Role, WebStoreDB>(db); //можно так
                                                                //_UserStore = new(db); //или так
        _UserStore.AutoSaveChanges = true; //можно не прописывать
    }

    [HttpGet("all")]
    public async Task<IEnumerable<User>> GetAll() => await _UserStore.Users.ToArrayAsync();
}

