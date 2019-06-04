using Assurant.ElitaPlus.External.Interfaces;
using Assurant.IndixService.Domain.ResultModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assurant.ElitaPlus.External.Indix.Model
{
    internal abstract class BaseProductSorter
    {
        protected SortedIndixProducts _products;
        internal BaseProductSorter() { }

        internal BaseProductSorter(SortedIndixProducts products)
        {
            _products = products;
        }
        internal abstract IEnumerable<ProductDetail> SortAscending(SortedIndixProducts products);

        internal abstract IEnumerable<ProductDetail> SortDescending(SortedIndixProducts products);
    }

    internal class ProductMaxPriceSorter : BaseProductSorter
    {
        internal override IEnumerable<ProductDetail> SortAscending(SortedIndixProducts products)
        {
            return products.OrderBy(p => Decimal.Parse(p.MaxSalePrice)).ThenBy(p => p.Title);
        }

        internal override IEnumerable<ProductDetail> SortDescending(SortedIndixProducts products)
        {
            return products.OrderByDescending(p => Decimal.Parse(p.MaxSalePrice)).ThenBy(p => p.Title);
        }
    }


    internal class ProductMinPriceSorter : BaseProductSorter
    {
        internal override IEnumerable<ProductDetail> SortAscending(SortedIndixProducts products)
        {
            return products.OrderBy(p => Decimal.Parse(p.MinSalePrice)).ThenBy(p => p.Title);
        }

        internal override IEnumerable<ProductDetail> SortDescending(SortedIndixProducts products)
        {
            return products.OrderByDescending(p => Decimal.Parse(p.MinSalePrice)).ThenBy(p => p.Title);
        }
    }

    internal class ProductBrandNameSorter : BaseProductSorter
    {
        internal override IEnumerable<ProductDetail> SortAscending(SortedIndixProducts products)
        {
            return products.OrderBy(p => p.BrandName).ThenBy(p => p.Title);
        }

        internal override IEnumerable<ProductDetail> SortDescending(SortedIndixProducts products)
        {
            return products.OrderByDescending(p => p.BrandName).ThenBy(p => p.Title);
        }
    }

    internal class ProductTitleSorter : BaseProductSorter
    {
        internal override IEnumerable<ProductDetail> SortAscending(SortedIndixProducts products)
        {
            return products.OrderBy(p => p.Title);
        }

        internal override IEnumerable<ProductDetail> SortDescending(SortedIndixProducts products)
        {
            return products.OrderByDescending(p => p.Title);
        }
    }
}
