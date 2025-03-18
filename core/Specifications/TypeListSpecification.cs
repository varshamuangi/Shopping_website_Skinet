using System;
using core.Entities;

namespace core.Specifications;

public class TypeListSpecification : BaseSpecifications<Product, string>
{
     public TypeListSpecification()
    {
        AddSelect(x => x.Brand);
        ApplyDistinct();

    }
  
}
