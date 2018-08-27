using RestSharp;
using RestSharp.Authenticators;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Tools
{
    public class HipTestApi
    {
        public enum HipTestKey
        {
            HipTest_UID,
            HipTest_PWD,
            HipTest_Token,
            HipTest_ClientID
        }

        const string ApiBase = "https://app.hiptest.com/api";

        static readonly string _accessToken;
        static readonly string _clientId;
        static readonly string _userId;
        static readonly string _userPw;


        static HipTestApi()
        {
            ConfigUtils config = new ConfigUtils();
            _accessToken = config.GetHipTestCreds(HipTestKey.HipTest_Token);
            _clientId = config.GetHipTestCreds(HipTestKey.HipTest_ClientID);
            _userId = config.GetHipTestCreds(HipTestKey.HipTest_UID);
            _userPw = config.GetHipTestCreds(HipTestKey.HipTest_PWD);
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri($"{ApiBase}/auth/sign_in");
            client.Authenticator = new HttpBasicAuthenticator(_userId, _userPw);
            request.AddParameter("access-token", _accessToken);
            request.AddParameter("client", _clientId);
            request.AddParameter("uid", _userId);
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.";
                var hipTestApiException = new ApplicationException(message, response.ErrorException);
                throw hipTestApiException;
            }
            return response.Data;
        }

        public static IRestResponse GetResponse()
        {
            var client = new RestClient("https://app.hiptest.com/api/projects/95332");
            var request = new RestRequest(Method.GET);
            request.Resource = "/scenarios/2156851";
            request.AddHeader("uid", _userId);
            request.AddHeader("client", _clientId);
            request.AddHeader("access-token", _accessToken);
            request.AddHeader("Accept", "application/vnd.api+json; version=1");
            IRestResponse response = client.Execute(request);
            return response;
        }
    }


}
