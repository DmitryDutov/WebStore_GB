using System.Net.Http.Json;
using WebStore.Domain;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Products;

public class ProductsClient : BaseClient, IProductData
{
    public ProductsClient(HttpClient Client) : base(Client, "api/products")
    {

    }

    public IEnumerable<Section> GetSections()
    {
        var sections = Get<IEnumerable<Section>>($"{Addres}/sections");
        return sections!;
    }

    public Section? GetSectionById(int Id)
    {
        var section = Get<Section>($"{Addres}/sections/{Id}");
        return section;
    }

    public IEnumerable<Brand> GetBrands()
    {
        var brands = Get<IEnumerable<Brand>>($"{Addres}/brands");
        return brands!;
    }

    public Brand? GetBrandById(int Id)
    {
        var brand = Get<Brand>($"{Addres}/brands/{Id}");
        return brand;
    }

    public IEnumerable<Product> GetProducts(ProductFilter? Filter = null)
    {
        var responce = Post(Addres, Filter ?? new()); //Если фильтр пустой, то создаём новый
        var products = responce.Content.ReadFromJsonAsync<IEnumerable<Product>>().Result;
        return products!;
    }

    public Product? GetProductById(int Id)
    {
        var product = Get<Product>($"{Addres}/{Id}");
        return product;
    }

    public Product CreateProduct(string Name, int Order, decimal Price, string ImageUrl, string Section, string? Brand = null)
    {
        var response = Post($"{Addres}/new/", new CreateProductDTO()
        {
            Name = Name
            , Order = Order
            , Price = Price
            , ImageUrl = ImageUrl
            , Section = Section
            , Brand = Brand
        });

        var product = response.Content.ReadFromJsonAsync<Product>().Result;
        return product!;
    }
}