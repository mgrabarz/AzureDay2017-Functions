#r "Microsoft.WindowsAzure.Storage"

using System.Net;
using System.Security.AccessControl;
using Microsoft.WindowsAzure.Storage.Table;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, ICollector<CommitDetails> commitDetailsTable)
{
    // Get request body
    dynamic data = await req.Content.ReadAsAsync<object>();
    dynamic commits = data?.commits;

    if (commits != null)
    {
        foreach (var commit in commits)
        {
            commitDetailsTable.Add(new CommitDetails
            {
                PartitionKey = "AzureDay2017",
                RowKey = commit.id,
                Message = commit.message,
                Author = commit.author.ToString()
            });
        }
    }

    return req.CreateResponse(HttpStatusCode.OK);
}

public class CommitDetails
{
    public string PartitionKey { get; set;}

    public string RowKey { get; set; }

    public string Message { get; set; }

    public string Author { get; set; }
}