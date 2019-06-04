using Assurant.IndixService.Domain.ResultModels;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assurant.ElitaPlus.External.Indix.Extensions;
using Assurant.ElitaPlus.External.Indix.Model;
using System.Text;
using Assurant.IndixService.Domain.SearchModels;
using Assurant.ElitaPlus.External.Indix.Clients;
using Assurant.ElitaPlus.External.Interfaces;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Assurant.ElitaPlus.BusinessObjectsNew;
using System.Linq;
using Assurant.ElitaPlus.External.Indix.Extensions;

namespace Assurant.ElitaPlus.External.Indix.Helpers
{

    internal class IndixRequestHelper
    {
        #region Const
        private const int maxPerPage = 50;
        private const int maxRecordsFromIndix = 500;
        #endregion

        private IndixClient _client;
        private IndixRequestHelper() { }

        public IndixRequestHelper(RestClient client) {
            _client = client as IndixClient;
        }

        
        internal IEnumerable<ProductDetail> GetProducts(ProductSearchRequest request, out int totalNumberofObjetcs)
        {



            //Indix Parameter is vald if:
            // Required fields have values (CountryCode & SearchTerm)
            //SearchTerm is less than 200 characters
            if (!request.IsValid)
                throw new Exceptions.InvalidRequestFieldException();


            BaseProductSorter sorter = new Model.ProductSorterFactory().GetProductSorter(request.SortBy);

            int pageSize = request.DefaultPageSize < request.TotalNumberOfRecords ? request.DefaultPageSize
                                                                                  : request.TotalNumberOfRecords;


            int numberofRecords = 0;

            int numberOfPages = (request.TotalNumberOfRecords + pageSize - 1) / pageSize;



            SortedIndixProducts output = new SortedIndixProducts();

            output.SetSort(sorter);

            int pIndex = 0;
            int records = 0;
            int downloaded = 0;
            int actualNumberOfPages = numberOfPages;

            Action<int> loadOutput = i =>
            {
                numberofRecords = (request.TotalNumberOfRecords < pageSize) ? request.TotalNumberOfRecords
                                                                            : pageSize;

                //Getting products for current page
                ProductSearchResult response = GetProductResponse(request.ToProductSearchModel(), numberofRecords, (i + 1));

                if (response != null && response.Products != null && response.Products.Count() > 0)
                {
                    //Counting all records downloaded, Indix allows up to 500 only
                    downloaded += response.Products.Count();

                    IEnumerable<ProductDetail> products = response.Products.ToProductDetails();

                    if (records == 0)
                        records = response.Count;

                    products = products.DistinctBy(p => new { p.Title, p.MinSalePrice, p.MaxSalePrice });

                    if (output != null && output.Count > 0)
                        products = products.Where(p => output.FirstOrDefault(o => (o.Title.Equals(p.Title) && o.MinSalePrice.Equals(p.MinSalePrice) && o.MaxSalePrice.Equals(p.MaxSalePrice))) == null);
                        

                    if (products.Count() > 0 && !string.IsNullOrEmpty(request.StartPrice) && !string.IsNullOrEmpty(request.EndPrice))
                    {
                        products = products.Where(p =>
                                        (
                                            Decimal.Parse(p.MinSalePrice) >= Decimal.Parse(request.StartPrice) &&
                                            Decimal.Parse(p.MaxSalePrice) <= Decimal.Parse(request.EndPrice)
                                        ));

                    }

                    if (products.Count() > 0)
                    {
                        if (output.Count + products.Count() > request.TotalNumberOfRecords)
                            products = products.Take(request.TotalNumberOfRecords - output.Count);


                        output.AddRange(products);
                    }
                }
                else
                {
                    records = output.Count;
                }
            };


            ParallelOptions options = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };
            

            try
            {
                while ( 
                        output.Count < request.TotalNumberOfRecords && 
                        downloaded < maxRecordsFromIndix 
                         && ((records == 0) || (records > 0 && records != output.Count))
                       )
                {
                    Parallel.For(pIndex, numberOfPages, options, loadOutput);

                    pIndex = numberOfPages - 1;

                    //This section will trigger if number of records requested was not fulfilled due to filtering CategoryID or Price range)
                    if (output.Count < request.TotalNumberOfRecords && records > output.Count && downloaded < maxRecordsFromIndix)
                    {
                        //Sets Page Index in next Page
                        pIndex++;

                        //Recalculate number of pages based on the missing records
                        numberOfPages = numberOfPages + (((request.TotalNumberOfRecords - output.Count) + pageSize - 1) / pageSize);

                        numberOfPages = (numberOfPages == pIndex) ? numberOfPages + 1 : numberOfPages;

                    }
                    else
                    {
                        if (records == 0)
                            request.TotalNumberOfRecords = 0;
                        else if (output.Count < request.TotalNumberOfRecords)
                            records = output.Count;
                        else if (downloaded == maxRecordsFromIndix)
                            records = downloaded;
                    }
                }
            }
            catch (Exception ex)
            {
                StringBuilder error = new StringBuilder();
                error.AppendLine(ex.Message);

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                    error.AppendLine(ex.InnerException.Message);



                throw new ServiceException(error.ToString());
            }

            totalNumberofObjetcs = records;

            return output.Sort(request.SortType);
        }
        private ProductSearchResult GetProductResponse(ProductSearchModel searchParameter, int pageSize, int pageNumber)
        {

            ProductResponseModel response = new ProductResponseModel();
            IRestResponse resp = null;

            searchParameter.PageSize = pageSize.ToString();
            searchParameter.PageNumber = pageNumber.ToString();

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(AllwaysGoodCertificate);

                var request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;

                request.AddHeader("Accept", "application/json");

                request.AddBody(searchParameter);

                resp = _client.Execute<ProductDetailResponseModel>(request);
                
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                    message += (" / " + ex.InnerException.Message);

                throw new Exception(message);
            }

            if ((resp.ResponseStatus != ResponseStatus.Completed))
                throw new Exception(resp.ResponseStatus.ToString());

            try
            {
                JObject respObj = JObject.Parse(resp.Content);

                response.Result = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductSearchResult>(resp.Content);
            }
            catch
            {
                throw;
            }

            return (response == null || response.Result == null) ? null
                                                                 : response.Result;
        }


        private static bool AllwaysGoodCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            return true;
        }
        internal IEnumerable<Category> GetCategories()
        {



            CategoryResult response = new CategoryResult();

            try
            {
                var Request = new RestRequest(Method.GET);
                Request.RequestFormat = DataFormat.Json;

                IRestResponse resp = _client.Execute(Request);

                if ((resp.ResponseStatus != ResponseStatus.Completed))
                {
                    throw new Exception("Failed");
                }

                JObject respObj = JObject.Parse(resp.Content);

                response = Newtonsoft.Json.JsonConvert.DeserializeObject<CategoryResult>(resp.Content);


            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }

            return (response == null || response.Categories == null) ? null
                                                                     : response.Categories;
        }

    }
}