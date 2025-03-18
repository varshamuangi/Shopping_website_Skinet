using System;
using core.Entities;

namespace core.Specifications;

public class BrandListSpecification : BaseSpecifications<Product, string>
{
    public BrandListSpecification()
    {
        AddSelect(x => x.Brand);
        ApplyDistinct();

    }

}
