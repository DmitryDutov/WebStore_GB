using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Interfaces.Services;

namespace WebStore.WebAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly IProductData _ProductData;

        public ProductAPIController(IProductData ProductData) => _ProductData = ProductData;


        #region Набор действий

        [HttpGet("sections")]
        public IActionResult GetSections()
        {
            var sections = _ProductData.GetSections();
            return Ok(sections);
        }

        [HttpGet("sections/{Id}")]
        public IActionResult GetSectionsById(int Id)
        {
            var sections = _ProductData.GetSectionById(Id);
            if (sections is null)
                return NotFound();
            return Ok(sections);
        }

        [HttpGet("brands")]
        public IActionResult GetBrands()
        {
            var brands = _ProductData.GetBrands();
            return Ok(brands);
        }


        [HttpGet("brands/{Id}")]
        public IActionResult GetBandById(int Id)
        {
            var brand = _ProductData.GetBrandById(Id);
            if (brand is null)
                return NotFound();
            return Ok(brand);
        }

        [HttpPost]
        public IActionResult GetProducts(ProductFilter? Filter = null)
        {
            var poducts = _ProductData.GetProducts(Filter);
            return Ok(poducts);
        }

        [HttpGet("{Id}")]
        public IActionResult GetProductById(int Id)
        {
            var product = _ProductData.GetProductById(Id);
            if (product is null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct(string Name, int Order, decimal Price, string ImageUrl, string Section, string? Brand = null)
        {
            var product = _ProductData.CreateProduct(Name, Order, Price, ImageUrl, Section, Brand);
            return CreatedAtAction(nameof(GetProductById), new {product.Id}, product); 
        }

        #endregion
    }
}
