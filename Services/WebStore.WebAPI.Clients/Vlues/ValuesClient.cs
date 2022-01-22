namespace WebStore.Interfaces.TestAPI;

public class ValuesClient : IValuesService
{
    private HttpClient _Client;



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