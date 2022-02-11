using System.Globalization;
using System.Net.Http.Json;

namespace WebStore.WebAPI.Clients.Base;

public abstract class BaseClient : IDisposable
{
    protected HttpClient Http { get; }
    protected string Addres { get; }

    protected BaseClient(HttpClient Client, string Addres)
    {
        Http=Client;

        this.Addres=Addres;
    }

    #region Набор действий

    //Синхронный метод
    protected T? Get<T>(string url) => GetAsync<T>(url).Result;
    //Асинхронный метод
    protected async Task<T?> GetAsync<T>(string url)
    {
        var response = await Http.GetAsync(url).ConfigureAwait(false);
        return await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>()
                .ConfigureAwait(false)
            ;
    }

    //Синхронный метод
    protected HttpResponseMessage Post<T>(string url, T value) => PostAsync<T>(url, value).Result;
    //Асинхронный метод
    protected async Task<HttpResponseMessage?> PostAsync<T>(string url, T value)
    {
        var response = await Http.PostAsJsonAsync(url, value).ConfigureAwait(false);
        return response.EnsureSuccessStatusCode();
    }

    //Синхронный метод
    protected HttpResponseMessage Put<T>(string url, T value) => PutAsync<T>(url, value).Result;
    //Асинхронный метод
    protected async Task<HttpResponseMessage> PutAsync<T>(string url, T value)
    {
        var response = await Http.PutAsJsonAsync(url, value).ConfigureAwait(false);
        return response.EnsureSuccessStatusCode();
    }

    //Синхронный метод
    protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;
    //Асинхронный метод
    protected async Task<HttpResponseMessage> DeleteAsync(string url)
    {
        var response = await Http.DeleteAsync(url).ConfigureAwait(false);
        return response.EnsureSuccessStatusCode();
    }

    #endregion

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this); //если есть финализатор
    }
    //~BaseClient() => Dispose(false); //финализотор?

    protected bool _Disposed;
    protected virtual void Dispose(bool disposing)
    {
        if (_Disposed ) return;
        _Disposed = true;

        if (disposing)
        {
            //освобождаем управляемые ресурсы - обычные объекты с интерфейсом IDispisible
            //Http.Dispose();//не должны вызывать Disposr(), так как не мы его создавали
        }

        //освобождаем неуправляемые ресурсы (COM-объекты и т.п.)
    }
}

