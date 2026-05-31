using System.Collections.Generic;
using hw1.Generics.Models;

namespace hw1.Generics.Core.Comparers
{
    // порівнювач за ціною (зростання)
    public class ProductPriceComparer : IComparer<Product>
    {
        public int Compare(Product? x, Product? y)
        {
            if (x is null || y is null) return 0; // спрощено: null рівні
            return x.Price.CompareTo(y.Price);
        }
    }
}
