using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams specParams)
            :base(x =>
                (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
                (!specParams.BrandId.HasValue || x.ProductBrandId == specParams.BrandId) &&
                (!specParams.TypeId.HasValue || x.ProductTypeId == specParams.TypeId)
            )
        {
            AddInclude(p=>p.ProductBrand);
            AddInclude(p=>p.ProductType);
            AddOrderBy(p=>p.Name);
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            if(!string.IsNullOrEmpty(specParams.Sort)) 
            {
                switch(specParams.Sort) 
                {
                    case "priceAsc":
                        AddOrderBy(p=>p.Price);
                        break;
                    case "priceDesc":
                        AddOrderDescendingBy(p=>p.Price);
                        break;  
                    default:
                        AddOrderBy(p=>p.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id)
            : base(p => p.Id == id)
        {
            AddInclude(p=>p.ProductBrand);
            AddInclude(p=>p.ProductType);
        }
    }
}