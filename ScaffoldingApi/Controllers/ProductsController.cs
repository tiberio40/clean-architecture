using Microsoft.AspNetCore.Mvc;

namespace ScaffoldingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //private readonly ProductService _productRepository;

        //public ProductsController(ProductService productRepository)
        //{
        //    _productRepository = productRepository;
        //}

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        //{
        //    var products = await _productRepository.GetProductsAsync();
        //    return Ok(products);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Product>> GetById(int id)
        //{
        //    var product = await _productRepository.GetProductByIdAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(product);
        //}

        //[HttpPost]
        //public async Task<ActionResult> Create(Product product)
        //{
        //    await _productRepository.CreateProductAsync(product);
        //    return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult> Update(int id, Product product)
        //{
        //    var existingProduct = await _productRepository.GetProductByIdAsync(id);
        //    if (existingProduct == null)
        //    {
        //        return NotFound();
        //    }

        //    await _productRepository.UpdateProductAsync(existingProduct.Id, product);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    var product = await _productRepository.GetProductByIdAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    await _productRepository.DeleteProductAsync(id);
        //    return NoContent();
        //}
    }
}
