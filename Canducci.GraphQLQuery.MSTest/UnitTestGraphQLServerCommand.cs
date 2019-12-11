using Canducci.GraphQLQuery.MSTest.Queries;
using Canducci.GraphQLQuery.MSTest.Queries.Datas;
using Canducci.GraphQLQuery.MSTest.Queries.Types;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Execution.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canducci.GraphQLQuery.MSTest
{

   [TestClass]
   public class UnitTestGraphQLServerCommand
   {
      public ISchema Schema { get; set; }
      public IQueryExecutor QueryExecutor { get; set; }

      [TestInitialize]
      public void InitGraphQLServerCommand()
      {
         new QueryExecutionOptions
         {
            TracingPreference = TracingPreference.Always,
            IncludeExceptionDetails = true
         };
         Schema = SchemaBuilder.New()
               .AddType<RemoveType>()
               .AddType<SourceType>()
               .AddQueryType<Query>()
               .ModifyOptions(x =>
               {
                  x.QueryTypeName = "Query";
               })
               .Create();

         QueryExecutor = Schema.MakeExecutable();
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
         var text = typeQL.ToBodyJson();
         IExecutionResult result = QueryExecutor.Execute(text);
         var json = result.ToJson();
         Assert.AreEqual(result.Errors.Count, 0);
      }

      [TestMethod]
      public void TestSourceListWithAlias()
      {
         TypeQL typeQL = new TypeQL(
            new QueryType(
               "sources", "datas",
               new Fields(
                  new Field("id", "code"),
                  new Field("name", "source_name")
               )
            )
         );
         var text = typeQL.ToBodyJson();
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
         var text = typeQL.ToBodyJson();
         IExecutionResult result = QueryExecutor.Execute(text);
         //var json = result.ToJson();         
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
               new Arguments(new Argument(new Parameter("id", "id")))
            )
         );
         var text = typeQL.ToBodyJson();
         var variables = typeQL.Variables.ToDictionary();
         IExecutionResult result = QueryExecutor.Execute(text, variables);
         //var json = result.ToJson();         
         Assert.AreEqual(result.Errors.Count, 0);
      }

      [TestMethod]
      public void TestSourceAddWithArguments()
      {
         TypeQL typeQL = new TypeQL(
            new QueryType("source_param_add",
               new Fields(
                  new Field("id"),
                  new Field("name"),
                  new Field("value"),
                  new Field("created"),
                  new Field("active"),
                  new Field("time")
               ),
               new Arguments(
                  new Argument("id", 0),
                  new Argument("name", "source 4"),
                  new Argument("value", 1000M),
                  new Argument("created", null),
                  new Argument("active", true),
                  new Argument("time", "13:14:15")
               )
            )
         );
         var text = typeQL.ToBodyJson();         
         IExecutionResult result = QueryExecutor.Execute(text);
         //var json = result.ToJson();         
         Assert.AreEqual(result.Errors.Count, 0);
      }

      [TestMethod]
      public void TestSourceAddWithParameter()
      {
         TypeQL typeQL = new TypeQL(
            new Variables("getSourceAdd",
               new Variable<int>("id", 0),
               new Variable<string>("name", "source 4"),
               new Variable<decimal>("value", 1000M),
               new Variable<System.DateTime?>("created", null),
               new Variable<bool?>("active", true),
               new Variable<System.TimeSpan?>("time", null)
            ),
            new QueryType("source_param_add",
               new Fields(
                  new Field("id"),
                  new Field("name"),
                  new Field("value"),
                  new Field("created"),
                  new Field("active"),
                  new Field("time")
               ),
               new Arguments(
                  new Argument(new Parameter("id")),
                  new Argument(new Parameter("name")),
                  new Argument(new Parameter("value")),
                  new Argument(new Parameter("created")),
                  new Argument(new Parameter("active")),
                  new Argument(new Parameter("time"))
               )
            )
         );
         var text = typeQL.ToBodyJson();
         var variables = typeQL.Variables.ToDictionary();
         IExecutionResult result = QueryExecutor.Execute(text, variables);
         //var json = result.ToJson();         
         Assert.AreEqual(result.Errors.Count, 0);
      }

      [TestMethod]
      public void TestSourceAddWithArgumentsComplex()
      {
         Source source = new Source
         {
            Name = "Complex Type With Arguments"
         };
         TypeQL typeQL = new TypeQL(            
            new QueryType("source_add",
               new Fields(
                  new Field("id"),
                  new Field("name"),
                  new Field("value"),
                  new Field("created"),
                  new Field("active"),
                  new Field("time")
               ),
               new Arguments(
                  new Argument("input", source)
               )
            )
         );
         var text = typeQL.ToBodyJson();         
         IExecutionResult result = QueryExecutor.Execute(text);
         //var json = result.ToJson();         
         Assert.AreEqual(result.Errors.Count, 0);
      }

      [TestMethod]
      public void TestSourceAddWithParameterComplex()
      {
         Source source = new Source
         {
            Name = "Complex Type With Parameter"
         };
         TypeQL typeQL = new TypeQL(
            new Variables("getSourceAdd",
               new Variable<Source>("input", source, "source_input")
            ),
            new QueryType("source_add",
               new Fields(
                  new Field("id"),
                  new Field("name"),
                  new Field("value"),
                  new Field("created"),
                  new Field("active"),
                  new Field("time")
               ),
               new Arguments(
                  new Argument(new Parameter("input"))
               )
            )
         );
         var text = typeQL.ToBodyJson();
         var variables = typeQL.Variables.ToDictionary();
         IExecutionResult result = QueryExecutor.Execute(text, variables);
         //var json = result.ToJson();         
         Assert.AreEqual(result.Errors.Count, 0);
      }

      [TestMethod]
      public void TestSourceWhereInIdArgument()
      {
         int[] id_in = new int[] { 1, 2 };
         TypeQL typeQL = new TypeQL(           
            new QueryType("source_in",
               new Fields(
                  new Field("id"),
                  new Field("name"),
                  new Field("value"),
                  new Field("created"),
                  new Field("active"),
                  new Field("time")
               ),
               new Arguments(
                  new Argument("id_in", id_in)
               )
            )
         );
         var text = typeQL.ToBodyJson();         
         IExecutionResult result = QueryExecutor.Execute(text);
         //var json = result.ToJson();         
         Assert.AreEqual(result.Errors.Count, 0);
      }

      [TestMethod]
      public void TestSourceWhereInIdParameter()
      {
         int[] id_in = new int[] { 1, 2 };
         TypeQL typeQL = new TypeQL(
            new Variables("getSources",
               new Variable<int[]>("id_in", id_in)
            ),
            new QueryType("source_in",
               new Fields(
                  new Field("id"),
                  new Field("name"),
                  new Field("value"),
                  new Field("created"),
                  new Field("active"),
                  new Field("time")
               ),
               new Arguments(
                  new Argument(new Parameter("id_in"))
               )
            )
         );
         var text = typeQL.ToBodyJson();
         var variables = typeQL.Variables.ToDictionary();
         IExecutionResult result = QueryExecutor.Execute(text, variables);
         //var json = result.ToJson();
         Assert.AreEqual(result.Errors.Count, 0);
      }
   }
}
