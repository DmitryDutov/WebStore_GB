using System.Net.Http.Json;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.Interfaces.TestAPI;

public class ValuesClient : BaseClient, IValuesService
{

    //Получаем HttpClient снаружи и передаём базовому классу
    public ValuesClient(HttpClient Client):base(Client, WebAPIAdresses.Values)
    {

    }

    //Для упрощения работы с заполнением методов ставим из NuGet: Microsoft.Extensions.Http + System.Net.Http.Json (Сериализация\Десериализация)
    //Получеам перечисление строк со стороны WebAPI
    public IEnumerable<string> GetValues()
    {
        var responce = Http.GetAsync(Address).Result; //Result для приведения асинхронного метода к синхронному
        if (responce.IsSuccessStatusCode)
            return responce.Content.ReadFromJsonAsync<IEnumerable<string>>().Result!;
        //Если адресс неверный возвращаем пустое перечисление
        return Enumerable.Empty<string>();
    }

    public int Count()
    {
        var responce = Http.GetAsync($"{Address}/count").Result;
        if (responce.IsSuccessStatusCode)
            return responce.Content.ReadFromJsonAsync<int>().Result!;
        //Если адресс неверный возвращаем -1
        return -1;
    }

    public string? GetById(int Id)
    {
        var responce = Http.GetAsync($"{Address}/{Id}").Result;
        if (responce.IsSuccessStatusCode)
            return responce.Content.ReadFromJsonAsync<string>().Result!;
        //Если адресс неверный возвращаем null
        return null;
    }

    public void Add(string Value)
    {
        var responce = Http.PostAsJsonAsync(Address, Value).Result;
        //если статусный код некорректен генрируем исключение
        responce.EnsureSuccessStatusCode();
    }

    public void Edit(int Id, string Value)
    {
        var responce = Http.PutAsJsonAsync($"{Address}/count", Value).Result;
        responce.EnsureSuccessStatusCode();
    }

    public bool Delete(int Id)
    {
        var responce = Http.DeleteAsync($"{Address}/{Id}").Result;
        //возвращаем ответ если статусный код успешный
        return responce.IsSuccessStatusCode;
    }
}

