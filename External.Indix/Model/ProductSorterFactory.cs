using Assurant.ElitaPlus.External.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assurant.ElitaPlus.External.Indix.Model
{
    internal class ProductSorterFactory
    {
        Dictionary<SortBy, BaseProductSorter> _sorters;
        internal ProductSorterFactory() {
            _sorters = new Dictionary<SortBy, Model.BaseProductSorter>();

            _sorters[SortBy.MNP] = new ProductMinPriceSorter();
            _sorters[SortBy.MXP] = new ProductMaxPriceSorter();
            _sorters[SortBy.MK] = new ProductBrandNameSorter();
            _sorters[SortBy.TTL] = new ProductTitleSorter();

        }

        internal BaseProductSorter GetProductSorter(SortBy sortBy) {
            return _sorters[sortBy];
        }
    }
}
