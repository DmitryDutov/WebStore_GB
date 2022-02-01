using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.DTO;
using WebStore.Interfaces.Services;

namespace WebStore.WebAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductAPIController : ControllerBase
    {
        private readonly IProductData _ProductData;

        public ProductAPIController(IProductData ProductData) => _ProductData = ProductData;


        #region Набор действий

        [HttpGet("sections")]
        public IActionResult GetSections()
        {
            var sections = _ProductData.GetSections();
            return Ok(sections.ToDTO());
        }

        [HttpGet("sections/{Id}")]
        public IActionResult GetSectionsById(int Id)
        {
            var section = _ProductData.GetSectionById(Id);
            if (section is null)
                return NotFound();
            return Ok(section.ToDTO());
        }

        [HttpGet("brands")]
        public IActionResult GetBrands()
        {
            var brands = _ProductData.GetBrands();
            return Ok(brands.ToDTO());
        }


        [HttpGet("brands/{Id}")]
        public IActionResult GetBandById(int Id)
        {
            var brand = _ProductData.GetBrandById(Id);
            if (brand is null)
                return NotFound();
            return Ok(brand.ToDTO());
        }

        [HttpPost]
        public IActionResult GetProducts(ProductFilter? Filter = null)
        {
            var poducts = _ProductData.GetProducts(Filter);
            return Ok(poducts.ToDTO());
        }

        [HttpGet("{Id}")]
        public IActionResult GetProductById(int Id)
        {
            var product = _ProductData.GetProductById(Id);
            if (product is null)
                return NotFound();
            return Ok(product.ToDTO());
        }

        [HttpPost]
        //public IActionResult CreateProduct(string Name, int Order, decimal Price, string ImageUrl, string Section, string? Brand = null)
        //{
        //    var product = _ProductData.CreateProduct(Name, Order, Price, ImageUrl, Section, Brand);
        //    return CreatedAtAction(nameof(GetProductById), new {product.Id}, product); 
        //}
        public IActionResult CreateProduct(CreateProductDTO Model)
        {
            var product = _ProductData.CreateProduct(
                          Model.Name
                        , Model.Order
                        , Model.Price
                        , Model.ImageUrl
                        , Model.Section
                        , Model.Brand
                       );
            return CreatedAtAction(nameof(GetProductById), new {product.Id}, product.ToDTO());
        }

        #endregion
    }
}

