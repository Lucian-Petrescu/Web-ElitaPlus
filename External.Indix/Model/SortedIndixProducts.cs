using Assurant.IndixService.Domain.ResultModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Assurant.ElitaPlus.External.Interfaces;

namespace Assurant.ElitaPlus.External.Indix.Model
{
    internal class SortedIndixProducts: ConcurrentBag<ProductDetail>
    {
        private BaseProductSorter _sorter;

        internal void SetSort(BaseProductSorter sorter)
        {
            this._sorter = sorter;
        }

        internal IEnumerable<ProductDetail> Sort(SortType sortType) {
            
            return (sortType == SortType.Ascending) ? _sorter.SortAscending(this) : _sorter.SortDescending(this);
        }

    }
}
