using System;
using RestSharp;
using static RKCIUIAutomation.Base.BaseUtils;

namespace RKCIUIAutomation.Base
{
    public static class RestUtils
    {        
        public static string GetJsonResponse(string baseUrl, string endpoint)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest();
            string json = string.Empty;

            try
            {
                client.BaseUrl = new Uri(baseUrl);
                request.Resource = endpoint;
                IRestResponse response = client.Execute(request);
                if (response != null)
                {
                    json = response.Content;
                }
            }
            catch (Exception e)
            {
                LogError(e.Message);
            }

            return json;
        }

    }
}
