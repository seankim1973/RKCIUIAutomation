﻿using Newtonsoft.Json;
using OpenQA.Selenium.Remote;
using RestSharp;
using System;

namespace RKCIUIAutomation.Tools
{
    extern alias newtJson;

    public class GridUtility
    {
        private string gridHostName;
        private int gridPort;

        public GridUtility(string gridHostName, int gridPort)
        {
            this.gridHostName = gridHostName;
            this.gridPort = gridPort;
        }

        public GridNode GetNodeInfoForSession(SessionId sessionId)
        {
            GridNode node = null;
            RestClient client = new RestClient();
            IRestResponse response = null;
            try
            {
                Uri uri = new Uri($"http://{gridHostName}:{gridPort}/grid/api/testsession?session={sessionId}");
                RestRequest request = new RestRequest(uri, Method.POST);
                response = client.Execute(request);
                string json = newtJson.Newtonsoft.Json.JsonConvert.SerializeObject(response);
                Object obj = newtJson.Newtonsoft.Json.JsonConvert.DeserializeObject<Object>(json);
            }
            catch (Exception)
            {
                throw;
            }

            return node;
        }

        public class GridNode
        {
        }
    }
}