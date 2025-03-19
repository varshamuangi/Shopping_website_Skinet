using System;
using core.Entities;

namespace core.Specifications;

public class ProductSpecification : BaseSpecifications<Product>
{
   public ProductSpecification(ProductSpecPrams specPrams) : base(x => 
       (string.IsNullOrEmpty(specPrams.Search) || x.Name.ToLower().Contains(specPrams.Search)) &&
       (specPrams.Brands.Count == 0 || specPrams.Brands.Contains(x.Brand)) &&
       (specPrams.Types.Count == 0 || specPrams.Types.Contains(x.Type))
       )
   {
      ApplyPaging(specPrams.PageSize * (specPrams.PageIndex -1), specPrams.PageSize);
      switch(specPrams.Sort)
      {
        case "priceAsc":
            AddOrderBy(x => x.Price);
            break;
        case "priceDesc":
            AddOrderByDescending(x =>x.Price);
            break;
        default: 
            AddOrderBy(x => x.Name);
            break;
      }
   }
}
