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
        var sections = Get<IEnumerable<SectionDTO>>($"{Addres}/sections");
        return sections!.FromDTO()!;
    }

    public Section? GetSectionById(int Id)
    {
        var section = Get<SectionDTO>($"{Addres}/sections/{Id}");
        return section!.FromDTO()!;
    }

    public IEnumerable<Brand> GetBrands()
    {
        var brands = Get<IEnumerable<BrandDTO>>($"{Addres}/brands");
        return brands!.FromDTO()!;
    }

    public Brand? GetBrandById(int Id)
    {
        var brand = Get<BrandDTO>($"{Addres}/brands/{Id}");
        return brand!.FromDTO()!;
    }

    public IEnumerable<Product> GetProducts(ProductFilter? Filter = null)
    {
        var responce = Post(Addres, Filter ?? new()); //Если фильтр пустой, то создаём новый
        var products = responce.Content.ReadFromJsonAsync<IEnumerable<ProductDTO>>().Result;
        return products!.FromDTO()!;
    }

    public Product? GetProductById(int Id)
    {
        var product = Get<ProductDTO>($"{Addres}/{Id}");
        return product!.FromDTO()!;
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

        var product = response.Content.ReadFromJsonAsync<ProductDTO>().Result;
        return product!.FromDTO()!;
    }
}