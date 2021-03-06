using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            Product product = await _context.Products
                                                .Include(p => p.ProductBrand)
                                                .Include(p => p.ProductType)
                                                .FirstOrDefaultAsync(p => p.Id == id);

            return product;
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            IReadOnlyList<Product> products = await _context.Products
                                                                .Include(p => p.ProductBrand)
                                                                .Include(p => p.ProductType)
                                                                .ToListAsync();

            return products;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            IReadOnlyList<ProductBrand> productBrands = await _context.ProductBrands.ToListAsync();

            return productBrands;
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            IReadOnlyList<ProductType> productTypes = await _context.ProductTypes.ToListAsync();

            return productTypes;
        }
    }
}