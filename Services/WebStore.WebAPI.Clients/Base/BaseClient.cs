namespace WebStore.WebAPI.Clients.Base;

public abstract class BaseClient
{
    protected HttpClient Http { get; }
    protected string Addres { get; }

    protected BaseClient(HttpClient Client, string Addres)
    {
        Http=Client;

        this.Addres=Addres;
    }
}

