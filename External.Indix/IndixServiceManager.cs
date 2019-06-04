using Assurant.ElitaPlus.External.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Assurant.ElitaPlus.BusinessObjectsNew;
using RestSharp;
using ElitaInternalWS;
using System.ServiceModel;
using Assurant.IndixService.Domain.ResultModels;

namespace Assurant.ElitaPlus.External.Indix
{
    public class IndixServiceManager : IIndixServiceManager
    {
        private RestClient _client;

        public IndixServiceManager()
        {
            _client = new Clients.IndixClient();
        }

        public IndixServiceManager(RestClient client) {
            _client = client;
        }
        public ProductDetailsResponse GetProductDetails(ProductDetailsRequest request)
        {
            ProductDetailsResponse response = new ProductDetailsResponse() {
                Product = new ProductDetail() {
                    MaxSalePrice = 0.ToString()
                }
            };

            try
            {
                WebPasswd oWebPasswd = new WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIMS_INDIX_SERVICE), true);
                //string indixUrl = "https://qa-indixapi.assurantprotectionplans.com/api/v1/indix/summary/product/{mpId}/{countryCode}/";
                //oWebPasswd.Url
                RestClient client = new RestClient(oWebPasswd.Url);
                client.AddDefaultHeader("content-type", "application/json");

                X509Certificate2 cert = GetCertificateByThumbPrint(Properties.Settings.Default.INDIX_SERVICE_THUMBPRINT);

                client.ClientCertificates = new X509CertificateCollection { cert };

                dynamic Request = new RestRequest(Method.GET);
                Request.AddParameter("mpId", request.mpId);
                Request.AddParameter("countryCode", request.countryCode);
                IRestResponse resp = client.Execute(Request);

                if ((resp.ResponseStatus != ResponseStatus.Completed))
                {
                    throw new FaultException<RequestWasNotSuccessFull>(new RequestWasNotSuccessFull(), "Invalid Request");
                }

                JObject respObj = JObject.Parse(resp.Content);
                //Dim tokenErrMsg As JToken = respObj.SelectToken("$.ErrorMessage")
                string salesPrice = (string)respObj.SelectToken("$.Product")["MaxSalePrice"];

                //REQ-6230
                string minSalePrice = (string)respObj.SelectToken("$.Product")["MinSalePrice"];
                string categoryID = (string)respObj.SelectToken("$.Product")["CategoryId"];
                string title = (string)respObj.SelectToken("$.Product")["Title"];
                string brandName = (string)respObj.SelectToken("$.Product")["BrandName"];

                //response.Product.CategoryNamePath
                //response.Product.CategoryId
                //response.Product.Mpid
                //response.Product.CategoryName
                //response.Product.BrandName
                //response.Product.MinSalePrice                   
                //response.Product.BrandId
                //response.Product.CategoryIdPath
                //response.Product.CountryCode

                //response.Product.Currency
                //response.Product.Title
                //response.Product.LastRecordedAt
                //response.Product.ImageUrl

                response.Product.MaxSalePrice = salesPrice;

                //REQ-6230
                response.Product.CategoryId = categoryID;
                response.Product.BrandName = brandName;
                response.Product.Title = title;
                response.Product.MinSalePrice = minSalePrice;

                //response.Product.OffersCount
                //response.Product.StoresCount



            }
            catch (Exception ex) {
                response.Status = "Fail";
                response.ErrorMessage = ex.Message;
            }            

            return response;
        }

        private static X509Certificate2 GetCertificateByThumbPrint(string thumbPrint, StoreLocation store = StoreLocation.LocalMachine)
        {
            X509Certificate2 serverCertificate = new X509Certificate2();
            X509Store certStore = new X509Store(store);
            certStore.Open(OpenFlags.ReadOnly);
            try
            {
                string thumbprint = thumbPrint;
                var certCollection = certStore.Certificates.Find(X509FindType.FindByThumbprint, Regex.Replace(thumbprint, @"\s+", "").ToUpper(), false);
                if (certCollection.Count > 0)
                {
                    serverCertificate = certCollection[0];
                    return serverCertificate;
                }
            }
            finally
            {
                certStore.Close();
            }
            return serverCertificate;
        }

        //REQ-6230
        public IEnumerable<ProductDetail> GetProducts(ProductSearchRequest request, ref int totalNumberOfRecords)
        {
            return (new Helpers.IndixRequestHelper(_client).GetProducts(request, out totalNumberOfRecords));
        }
    }
}
