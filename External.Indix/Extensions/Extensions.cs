using Assurant.ElitaPlus.External.Interfaces;
using Assurant.IndixService.Domain.ResultModels;
using Assurant.IndixService.Domain.SearchModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assurant.ElitaPlus.External.Indix.Extensions
{

    internal static class Extensions
    {

        internal static void AddRange<T>(this ConcurrentBag<T> bag, IEnumerable<T> toAdd)
        {
            if (toAdd != null)
            {
                foreach (T element in toAdd)
                    bag.Add(element);

            }
        }

        internal static IEnumerable<ProductDetail> ToProductDetails(this IEnumerable<Product> products)
        {
            var output = products.Select(p => new ProductDetail { BrandName = p.BrandName,
                 BrandId = p.BrandId,
                 CategoryId = p.CategoryId,
                 CategoryIdPath = p.CategoryIdPath,
                 CategoryName = p.CategoryName,
                 CategoryNamePath = p.CategoryNamePath,
                 CountryCode = p.CountryCode,
                 Currency = p.Currency,
                 ImageUrl = p.ImageUrl,
                 LastRecordedAt = p.LastRecordedAt,
                 MaxSalePrice = p.MaxSalePrice,
                 MinSalePrice = p.MinSalePrice,
                 Mpid = p.Mpid,
                 Mpns = p.Mpns,
                 OffersCount = p.OffersCount,
                 StoresCount = p.StoresCount,
                 Title = p.Title,
                 Upcs = p.Upcs
            });

            return output;
        }

        internal static ProductSearchModel ToProductSearchModel(this ProductSearchRequest request)
        {
            ProductSearchModel psm = new ProductSearchModel
            {
                CountryCode = request.CountryCode,
                SearchTerm = request.SearchTerm,
                CategoryId = request.CategoryId,
            };

            if (!string.IsNullOrEmpty(request.StartPrice))
                psm.StartPrice = request.StartPrice;

            if (!string.IsNullOrEmpty(request.EndPrice))
                psm.EndPrice = request.EndPrice;

            return psm;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

    }
}
