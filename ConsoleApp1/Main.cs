using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
namespace IOnotification_System
{
    public class Issue
    {
        public Fields fields { get; set; }
        public Issue()
        {
            fields = new Fields();
        }
        /// <summary>
        /// This method sends a POST request to Jira to create the specified issue. 
        /// </summary>
        /// <returns>
        /// The specified information POSTed to Jira, or the HTTP Status Response string if the POST fails. 
        /// </returns>
        public string CreateJiraIssue()
        {
            var json = JsonConvert.SerializeObject(this);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //Console.WriteLine(json);
            string postUrl = "https://icpms.atlassian.net/rest/api/2/";
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            client.BaseAddress = new System.Uri(postUrl);
            //Converts your authentication information into Base64 for interpretation by Jira. In the format ("email:API_Token")
            byte[] cred = UTF8Encoding.UTF8.GetBytes("EMAIL:API_TOKEN");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            System.Net.Http.Formatting.MediaTypeFormatter jsonFormatter = new System.Net.Http.Formatting.JsonMediaTypeFormatter();
            System.Net.Http.HttpResponseMessage response = client.PostAsync("issue", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return ("POST Successful Response: " + response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                return ("POST Failed Response: " + response.StatusCode.ToString());
            }
        }
    }
    /// <summary>
    /// Getter and Setter methods for the components of a Jira item.
    /// </summary>
    public class Fields
    {
        public Project project { get; set; }
        public string summary { get; set; }
        public string description { get; set; }
        public Assignee assignee { get; set; }
        public IssueType issuetype { get; set; }
        public Fields()
        {
            project = new Project();
            issuetype = new IssueType();
            assignee = new Assignee();
        }
    }
    public class Project
    {
        public string key { get; set; }
    }
    public class IssueType
    {
        public string name { get; set; }
    }
    public class Assignee
    {
        public string name { get; set; }
    }
/// <summary>
/// The main class used to run the program. 
/// Set your values below to the intended values for the Jira Item you would like to create. 
/// </summary>
    public class run
    {
        public static void Main()
        {
            var item = new Issue();
            item.fields.project.key = "TM";
            item.fields.description = "the description";
            item.fields.issuetype.name = "Bug";
            item.fields.summary = "the summary";
            item.fields.assignee.name = "the name";
            Console.WriteLine(item.CreateJiraIssue());
        }
    }
}