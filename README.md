# JiraAPI_POST
small C# program used to POST a Jira issue using the API and the commandline. 

In order to use this program, navigate to the bottom of the Main.cs file and replace the default generic values with some values of your own. You also have to edit
the string "postUrl" to the URL of your specific Jira instance, along with the EMAIL:API_TOKEN placeholder which is stored in the variable "cred". An API Token
can be generated from your Jira Settings. 

*Project Key, IssueType and Description are required.*
