using RestSharp;
using System;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Base
{
    public class RestUtils : BaseUtils
    {
        public string GetJsonResponse(string baseUrl, string endpoint)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest();
            IRestResponse response = new RestResponse();
            string json = string.Empty;

            try
            {
                client.BaseUrl = new Uri(baseUrl);
                request.Resource = endpoint;
                response = client.Execute(request);

                if (response != null)
                {
                    json = response.Content;
                }
            }
            catch (Exception e)
            {
                Report.Error(e.Message);
            }

            return json;
        }
    }
}