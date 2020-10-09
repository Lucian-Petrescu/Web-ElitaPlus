using Assurant.ElitaPlus.BusinessObjectsNew;
using RestSharp;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Assurant.ElitaPlus.External.Indix.Clients
{
    /// <summary>
    /// IndixClient
    /// </summary>
    public class IndixClient : RestClient
    {
        private static string webPasswdUrl
        {
            get
            {
                WebPasswd oWebPasswd = new WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIMS_INDIX_SERVICE_PRODUCT_SEARCH), true);
                return oWebPasswd.Url;
            }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public IndixClient() : this(webPasswdUrl)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="url"></param>
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
                var certCollection = certStore.Certificates.Find(X509FindType.FindByThumbprint, Regex.Replace(thumbPrint, @"\s+", "").ToUpper(), false);
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
