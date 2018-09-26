using Atlassian.Jira;
using RestSharp;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Tools
{
    public class JiraUtil
    {
        private enum RestMethod
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public async Task<Issue> GetJiraIssue(string issueNumber)
        {
            string url = "https://rkidev.atlassian.net";
            var request = new RestRequest("projects/EPA", Method.GET);
            string username = "schongkim@rkci.com";
            string password = "sean0405";
            //request.AddParameter("testCase","28857");

            // create a connection to JIRA using the Rest client
            var jira = Jira.CreateRestClient(url, username, password);

            // use LINQ syntax to retrieve issues
            //var issues = from i in jira.Issues.Queryable
            //             where i.Assignee == "admin" && i.Priority == "Major"
            //             orderby i.Created
            //             select i;

            Issue response = await jira.Issues.GetIssueAsync(issueNumber);
            return response;
        }
    }
}