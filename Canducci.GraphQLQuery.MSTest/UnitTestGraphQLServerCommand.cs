using Canducci.GraphQLQuery.MSTest.Queries;
using Canducci.GraphQLQuery.MSTest.Queries.Types;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Execution.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Canducci.GraphQLQuery.MSTest
{

   [TestClass]
   public class UnitTestGraphQLServerCommand
   {
      public ISchema Schema { get; set; }
      public IQueryExecutor QueryExecutor {get;set;}

      [TestInitialize]
      public void InitGraphQLServerCommand()
      {
         new QueryExecutionOptions { 
            TracingPreference = TracingPreference.Always, 
            IncludeExceptionDetails = true 
         };
         Schema = SchemaBuilder.New()
               .AddType<RemoveType>()
               .AddType<SourceType>()
               .AddQueryType<Query>()
               .ModifyOptions(x => {
                  x.QueryTypeName = "Query";                  
               })
               .Create();

         QueryExecutor = Schema.MakeExecutable();
      }

      private string QueryString(string value)
      {
         value = value.Replace("{\"query\":\"", "");
         int variableExistLocal = value.IndexOf("variables");
         if (variableExistLocal == -1)
         {
            value = value.Substring(0, value.Length - 2);
         }
         else
         {
            value = value.Substring(0, variableExistLocal - 3);
         }
             
         return value;
      }

      [TestMethod]
      public void TestSourceList()
      {
         TypeQL typeQL = new TypeQL(
            new QueryType(
               "sources",
               new Fields(
                  new Field("id"),
                  new Field("name")
               )
            )
         );
         var text = QueryString(typeQL.ToStringJson());
         IExecutionResult result = QueryExecutor.Execute(text);
         var json = result.ToJson();
         Assert.AreEqual(result.Errors.Count, 0);
      }

      [TestMethod]
      public void TestSourceByIdWithArgument()
      {
         TypeQL typeQL = new TypeQL(
            new QueryType(
               "source_find",
               new Fields(
                  new Field("id"),
                  new Field("name")
               ),
               new Arguments(new Argument("id", 1))
            )
         );
         var text = QueryString(typeQL.ToStringJson());
         IExecutionResult result = QueryExecutor.Execute(text);
         var json = result.ToJson();
         Assert.IsTrue(true);
         Assert.AreEqual(result.Errors.Count, 0);
      }

      [TestMethod]
      public void TestSourceByIdWithParameter()
      {
         TypeQL typeQL = new TypeQL(
            new Variables("getSource",
               new Variable<int>("id", 1)
            ),
            new QueryType(
               "source_find",
               new Fields(
                  new Field("id"),
                  new Field("name")
               ),
               new Arguments(new Argument(new Parameter("id","id")))
            )
         );
         var text = QueryString(typeQL.ToStringJson());
         IExecutionResult result = QueryExecutor.Execute(text, new Dictionary<string, object> { ["id"] = 2 });         
         var json = result.ToJson();
         Assert.IsTrue(true);
         Assert.AreEqual(result.Errors.Count, 0);
      }
   }
}
