using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Rest.Azure;
using static TestSearch.Models;

namespace TestSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private static SearchServiceClient _searchClient;
        private static ISearchIndexClient _indexClient;

        string apiKey = "3EF3E54C72BDBDA5CCFDE64196B77FC0";
        string serviceName = "mytest";
        string indexname = "azuresql-index";
        string connectionString = "Server=tcp:testsearch1dbserver.database.windows.net,1433;Initial Catalog=TestSearch1_db;Persist Security Info=False;User ID=Andrii@testsearch1dbserver;Password=Qw3rty12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        
        public ValuesController()

        {
            _searchClient = new SearchServiceClient(serviceName, new SearchCredentials(apiKey));
            _indexClient = new SearchIndexClient(serviceName, indexname, new SearchCredentials(apiKey));
        }

        // GET api/values
        [HttpGet("upload/{query}")]
        public async Task<ActionResult<IEnumerable<string>>> GetAsync(string query)
        {
            

            //Index index = new Index(
            //    name: "my-test-sql-index",
            //    fields: new List<Field>
            //    {
            //        new Field("PostId", DataType.String, AnalyzerName.EnMicrosoft)
            //            { IsSearchable = true, IsRetrievable = true, IsSortable = true, IsKey = true },
            //        new Field("Title", DataType.String, AnalyzerName.EnMicrosoft)
            //            { IsSearchable = true, IsRetrievable = true, IsSortable = true, IsKey = true },
            //        new Field("Content", DataType.String, AnalyzerName.EnMicrosoft)
            //            { IsSearchable = true, IsRetrievable = true, IsSortable = true, IsKey = true, IsFacetable = true, IsFilterable = true},
            //        new Field ("Blogs", DataType.Collection(DataType.String), AnalyzerName.EnMicrosoft)
            //            { IsSearchable = true, IsRetrievable = true, IsSortable = true, IsKey = true, IsFacetable = true, IsFilterable = true}

            //    });

            //bool exists = await searchService.Indexes.ExistsAsync(index.Name);
            //if (exists)
            //{
            //    await searchService.Indexes.DeleteAsync(index.Name);
            //}

            //await searchService.Indexes.CreateAsync(index);

            //DataSource dataSource = DataSource.AzureSql(
            //    name: "myy-azure-sql",
            //    sqlConnectionString: connectionString,
            //    tableOrViewName: "Posts",
            //    deletionDetectionPolicy: new SoftDeleteColumnDeletionDetectionPolicy(
            //        softDeleteColumnName: "IsDeleted",
            //        softDeleteMarkerValue: "true"));
            //dataSource.DataChangeDetectionPolicy = new SqlIntegratedChangeTrackingPolicy();

            //await searchService.DataSources.CreateOrUpdateAsync(dataSource);

            //Indexer indexer = new Indexer(
            //    name: "myy-azure-sql-indexer",
            //    dataSourceName: dataSource.Name,
            //    targetIndexName: index.Name,
            //    schedule: new IndexingSchedule(TimeSpan.FromMinutes(30)));

            //exists = await searchService.Indexers.ExistsAsync(indexer.Name);
            //if (exists)
            //{
            //    await searchService.Indexers.ResetAsync(indexer.Name);
            //}

            //await searchService.Indexers.CreateOrUpdateAsync(indexer);

            //try
            //{
            //    await searchService.Indexers.RunAsync(indexer.Name);
            //}
            //catch (CloudException e) when (e.Response.StatusCode == (HttpStatusCode)429)
            //{
            //    Console.WriteLine("Failed to run indexer: {0}", e.Response.Content);
            //}

            RunQueris(_indexClient, query);
            //searchClient.CreateIndex(searchClient);
            //searchClient.CreateIndexer(searchClient, connectionString);

            //searchClient.Indexers.Reset("");
            //searchClient.Indexers.Run("");
            

            return new string[] { "value1", "value2" };
        }
        
        private static void RunQueris(ISearchIndexClient indexClient, string query)
        {
            SearchParameters parameters;
            DocumentSearchResult<Post> results;

            results = indexClient.Documents.Search<Post>(query);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        
    }
}
