using Application.Interfaces;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        //private readonly IProductRepository _productRepository;

        //public ProductService(IProductRepository productRepository)
        //{
        //    _productRepository = productRepository;
        //}

        //public async Task<IEnumerable<Product>> GetProductsAsync()
        //{
        //    return await _productRepository.GetAllAsync();
        //}

        //public async Task<Product> GetProductByIdAsync(int id)
        //{
        //    return await _productRepository.GetByIdAsync(id);
        //}

        //public async Task<Product> CreateProductAsync(Product product)
        //{
        //    //return await _productRepository.AddAsync(product);
        //    return new Product();
        //}

        //public async Task<Product> UpdateProductAsync(int id, Product product)
        //{
        //    var existingProduct = await _productRepository.GetByIdAsync(id);
        //    if (existingProduct == null)
        //    {
        //        return null;
        //    }

        //    existingProduct.Name = product.Name;
        //    existingProduct.Price = product.Price;

        //    //return await _productRepository.UpdateAsync(existingProduct);
        //    return new Product();
        //}

        //public async Task<bool> DeleteProductAsync(int id)
        //{
        //    var existingProduct = await _productRepository.GetByIdAsync(id);
        //    if (existingProduct == null)
        //    {
        //        return false;
        //    }

        //    //return await _productRepository.DeleteAsync(existingProduct);
        //    return true;
        //}
    }
}
