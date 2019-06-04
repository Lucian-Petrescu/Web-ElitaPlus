using Assurant.ElitaPlus.BusinessObjectsNew;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RestSharp.Extensions;

namespace Assurant.ElitaPlus.External.Indix.Clients
{
    public class IndixClient : RestClient
    {
        private const string serviceUri = "https://qa-indixapi.assurantprotectionplans.com/api/v1/indix/summary/products";
        private const string thumbPrint = "25 36 78 6d 8f 6e 7b 3e ac f5 45 a8 3c 03 e6 a9 fe 34 cb 4c";
        private static object syncRoot = new Object();

        private static string _webPasswdUrl;
        private static string WebPasswdUrl
        {
            get
            {
                WebPasswd oWebPasswd = new WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIMS_INDIX_SERVICE_PRODUCT_SEARCH), true);
                return oWebPasswd.Url;
            }
        }
        public IndixClient() : this(WebPasswdUrl)
        {

        }

        public IndixClient(string url) : base(url)
        {
            X509Certificate2 cert = GetCertificateByThumbPrint(Properties.Settings.Default.INDIX_SERVICE_THUMBPRINT);

            ClientCertificates = new X509CertificateCollection { cert };

            this.AddDefaultHeader("content-type", "application/json");
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
    }
}
