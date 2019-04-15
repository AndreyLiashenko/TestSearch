using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSearch
{
    public static class SearchExtensions
    {
        public static void CreateIndex(this SearchServiceClient serviceClient, SearchServiceClient client)
        {
            
            client.Indexes.Create(new Index
            {
                Name = "myy-index-sql",
                Fields = new List<Field>
                {
                    new Field("PostId", DataType.String, AnalyzerName.EnMicrosoft)
                        { IsSearchable = true, IsRetrievable = true, IsSortable = true, IsKey = true },
                    new Field("Title", DataType.String, AnalyzerName.EnMicrosoft)
                        { IsSearchable = true, IsRetrievable = true, IsSortable = true, IsKey = true },
                    new Field("Content", DataType.String, AnalyzerName.EnMicrosoft)
                        { IsSearchable = true, IsRetrievable = true, IsSortable = true, IsKey = true, IsFacetable = true, IsFilterable = true},
                    new Field ("Blogs", DataType.Collection(DataType.String), AnalyzerName.EnMicrosoft)
                        { IsSearchable = true, IsRetrievable = true, IsSortable = true, IsKey = true, IsFacetable = true, IsFilterable = true}

                }
            });

            //bool exists = client.Indexes.ExistsAsync()
        }

        //public static void CreateIndexer(this SearchServiceClient serviceClient, SearchServiceClient client, string connectionString)
        //{
        //    if (!client.DataSources.Exists("mytest"))
        //    {
        //        client.DataSources.Create(new DataSource
        //        {
        //            Name = "mytest",
        //            Type = "documentdb",
        //            Container = new DataContainer { Name = "myindex" },
        //            Credentials = new DataSourceCredentials { ConnectionString = connectionString }

        //        });
                
        //    }
        //    if (!client.Indexers.Exists("mydatabasedb"))
        //    {
        //        client.Indexers.Create(new Indexer
        //        {
        //            Name = "mydatabasedb",
        //            DataSourceName = "mytest",
        //            TargetIndexName = "myindex"
        //        });
        //    }
        //}

        private static Index CraeteCombineIndex (SearchServiceClient client)
        {
            Index index = new Index
            {
                Name = "my-index-two-table",
                Fields = new List<Field>
                    {
                        new Field("PostId", DataType.String, AnalyzerName.EnMicrosoft)
                            { IsSearchable = true, IsRetrievable = true, IsSortable = true, IsKey = true },
                        new Field("Title", DataType.String, AnalyzerName.EnMicrosoft)
                            { IsSearchable = true, IsRetrievable = true, IsSortable = true, IsKey = true },
                        new Field("Content", DataType.String, AnalyzerName.EnMicrosoft)
                            { IsSearchable = true, IsRetrievable = true, IsSortable = true, IsKey = true, IsFacetable = true, IsFilterable = true},
                        new Field ("Blogs", DataType.Collection(DataType.String), AnalyzerName.EnMicrosoft)
                            { IsSearchable = true, IsRetrievable = true, IsSortable = true, IsKey = true, IsFacetable = true, IsFilterable = true}

                    },
                Suggesters = new List<Suggester>
                        {
                            new Suggester
                            {
                                Name = "cn",
                                SourceFields = {"Content"}
                            }
                        }
            };

            bool exists = client.Indexes.ExistsAsync(index.Name).GetAwaiter().GetResult();
            if(exists)
            {
                client.Indexes.DeleteAsync(index.Name).Wait();
            }
            client.Indexes.CreateAsync(index).Wait();
            return index;
        }

        //private static Indexer CreateTwoTableIndexer(SearchServiceClient client, Index index, string dataSource)
        //{
        //    var indexer = new Indexer
        //        {
        //        Name = "name-indexer",
        //        DataSourceName = dataSource,
        //        TargetIndexName = index.Name,
        //        FieldMappings = new List<FieldMapping> {new FieldMapping("PostId", ) }
        //    };
        //}
    }
}
