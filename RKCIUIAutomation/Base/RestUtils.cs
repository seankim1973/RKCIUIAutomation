using System;
using RestSharp;

namespace RKCIUIAutomation.Base
{
    public class RestUtils
    {        
        public string GetJsonResponse(string baseUrl, string endpoint)
        {
            BaseUtils baseUtils = new BaseUtils();

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
                baseUtils.LogError(e.Message);
            }

            return json;
        }

    }
}
