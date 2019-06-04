using Assurant.ElitaPlus.External.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assurant.ElitaPlus.BusinessObjectsNew;
using RestSharp;
using System.ServiceModel;
using Newtonsoft.Json.Linq;
using ElitaInternalWS;

namespace Assurant.ElitaPlus.External.AppleCare
{
    public class AppleCareServiceManager : IAppleCareServiceManager
    {
        private RestClient GetAppleCareClient(string methodName, string contentType = "application/json")
        {

            WebPasswd oWebPasswd = new WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__KDDI_APPLECARE_SERVICE), false);
            RestClient client = new RestClient(oWebPasswd.Url + methodName);

            //string iextUrl = "http://intra.servicefabric-mod.assurant.com/DiagnosticsManagement_KDDI/api/";
            //RestClient client = new RestClient(iextUrl + methodName);

            client.AddDefaultHeader("content-type", contentType);

            return client;
        }

        public ApplePartResponse GetApplePartFromIMEI(string IMEI)
        {
            ApplePartResponse response = new ApplePartResponse()
            {
                ErrorResponse = new ErrorResponse()
            };

            try
            {
                RestClient client = GetAppleCareClient("Parts");

                RestRequest Request = new RestRequest(Method.GET);
                Request.AddParameter("imei", IMEI);
                IRestResponse resp = client.Execute(Request);

                if ((resp.ResponseStatus != ResponseStatus.Completed))
                {
                    throw new FaultException<RequestWasNotSuccessFull>(new RequestWasNotSuccessFull(), "Invalid Request");
                }

                JObject respObj = JObject.Parse(resp.Content);

                string partNumber = (string)respObj.SelectToken("$.PartNumber");
                response.PartNumber = partNumber;

                if (respObj.SelectToken("$.ErrorResponse").HasValues)
                {
                    response.ErrorResponse.ErrorCode = (string)respObj.SelectToken("$.ErrorResponse")["ErrorCode"];
                    response.ErrorResponse.ErrorMessage = (string)respObj.SelectToken("$.ErrorResponse")["ErrorMessage"];
                }

            }
            catch (Exception ex)
            {
                response.ErrorResponse.ErrorCode = ex.GetType().Name;
                response.ErrorResponse.ErrorMessage = ex.Message;
            }

            return response;
        }

    }
}
