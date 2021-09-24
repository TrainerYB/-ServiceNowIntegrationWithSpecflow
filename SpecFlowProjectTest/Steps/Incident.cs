using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpecFlowTraining;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace SpecFlowProjectTest.Steps
{
    [Binding]
    public class CCPropertiesSteps
    {
        private Incident _inc;
        IRestResponse response;
        private string baseUrl = "https://dev98654.service-now.com";

        [Given(@"User with ID (.*) and password (.*)")]
        public void GivenUserWithIDAndPassword(string userId, String password)
        {
            RestClient client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(userName, password);
        }

        [When(@"Validating the user authentication")]
        public void WhenValidatingTheUserAuthentication()
        {
            var request = new RestRequest("resource", Method.GET);
            response = client.Execute(request);
        }

        [Then(@"Response code should be (.*)")]
        public void ThenResponseCodeShouldBe(string responseCode)
        {
            Assert.AreEqual(response.StatusCode, responseCode);
        }

        [Given(@"User with ID (.*) and Password (.*) is authenticated for the ServiceNow Instance")]
        public void GivenUserAndPasswordIsAuthenticated(string userId, String password)
        {
            RestClient client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(userName, password);
            var request = new RestRequest("resource", Method.GET);
            response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, responseCode);
        }


        [When(@"an incident id is shared for the user with number as (.*)")]
        public void WhenAnIncidentIdIsSharedForTheuserWithTheNumber(string incidentNumber)
        {
            var request = new RestRequest("resource", Method.GET);
            request.AddParameter("number", incidentNumber);
            response = client.Execute(request);
        }

        [Then(@"the most recent incident  is as follows")]
        public void ThenMostRecentIncidentIsAsFollws(Table table)
        {
            //---------Weekly Typed Conversion---------------
            var number = table.Rows.First(row => row["attribute"] == "number")["value"];
            var responseCode = table.Rows.First(row => row["attribute"] == "ResponseCode")["value"];

            Assert.AreEqual(response.StatusCode, responseCode);
            Assert.AreEqual(response.number, number);
        }
    }
}
