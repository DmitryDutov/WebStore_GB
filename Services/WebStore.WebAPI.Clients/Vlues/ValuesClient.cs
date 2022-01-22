using WebStore.WebAPI.Clients.Base;

namespace WebStore.Interfaces.TestAPI;

public class ValuesClient : BaseClient, IValuesService
{

    //Получаем HttpClient снаружи и передаём базовому классу
    public ValuesClient(HttpClient Client):base(Client, "api/values")
    {

    }



    public IEnumerable<string> GetValues()
    {
        throw new NotImplementedException();
    }

    public int Count()
    {
        throw new NotImplementedException();
    }

    public string GetById(int Id)
    {
        throw new NotImplementedException();
    }

    public void Add(string Value)
    {
        throw new NotImplementedException();
    }

    public void Edit(int Id, string Value)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int Id)
    {
        throw new NotImplementedException();
    }
}