using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.MSTest.Queries;
using Canducci.GraphQLQuery.MSTest.Queries.Datas;
using Canducci.GraphQLQuery.MSTest.Queries.Directives;
using Canducci.GraphQLQuery.MSTest.Queries.Input;
using Canducci.GraphQLQuery.MSTest.Queries.Types;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Execution.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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
               .AddDirectiveType<UpperDirectiveType>()
               .AddType<RemoveType>()
               .AddType<SourceType>()
               .AddType<CarType>()
               .AddType<StateType>()
               .AddType<StateInput>()
               .AddType<SourceInput>()
               .AddType<CityInput>()
               .AddType<CarInput>()
               .AddQueryType<Query>()
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
      public void TestSourceListFieldSimplyParamString()
      {
         TypeQL typeQL = new TypeQL(
            new QueryType("sources", new Fields("id", "name"))
         );
         var text = typeQL.ToBodyJson();
         IExecutionResult result = QueryExecutor.Execute(text);
         var json = result.ToJson();
         Assert.AreEqual(result.Errors.Count, 0);
      }

      [TestMethod]
      public void TestSourceListFieldSimplyParamStringWithAlias()
      {
         TypeQL typeQL = new TypeQL(
            new QueryType("sources", new Fields("id,_id", "name,_name"))
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
         IExecutionResult result = QueryExecutor.Execute(text, typeQL.Variables.ToDictionary());
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
         IExecutionResult result = QueryExecutor.Execute(text, typeQL.Variables.ToDictionary());
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
         IExecutionResult result = QueryExecutor.Execute(text, typeQL.Variables.ToDictionary());
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
         IExecutionResult result = QueryExecutor.Execute(text, typeQL.Variables.ToDictionary());
         //var json = result.ToJson();
         Assert.AreEqual(result.Errors.Count, 0);
      }


      [TestMethod]
      public void TestSourceListWithDirectiveInclude()
      {
         TypeQL typeQL = new TypeQL(
            new Variables("getSourcesList",
               new Variable<bool>("status", true, true, true)
            ),
            new QueryType(
               "sources",
               new Fields(
                  new Field("id", new IDirective[] { new Include("status") }),
                  new Field("name")
               )
            )
         );
         var text = typeQL.ToBodyJson();
         IExecutionResult result = QueryExecutor.Execute(text, typeQL.Variables.ToDictionary());
         var json = result.ToJson();
         Assert.AreEqual(result.Errors.Count, 0);
      }

      [TestMethod]
      public void TestSourceListWithDirectiveSkip()
      {
         TypeQL typeQL = new TypeQL(
            new Variables("getSourcesList",
               new Variable<bool>("active", true, true, true)
            ),
            new QueryType(
               "sources",
               new Fields(
                  new Field("id", new IDirective[] { new Skip("active") }),
                  new Field("name")
               )
            )
         );
         var text = typeQL.ToBodyJson();
         IExecutionResult result = QueryExecutor.Execute(text, typeQL.Variables.ToDictionary());
         var json = result.ToJson();
         Assert.AreEqual(result.Errors.Count, 0);
      }

      [TestMethod]
      public void TestSourceListWithDirectiveCustom()
      {
         TypeQL typeQL = new TypeQL(
            new Variables(
               "getSources",
               new Variable<bool>("source", true, true, true)
            ),
            new QueryType(
               "sources",
               new Fields(
                  new Field("id"),
                  new Field("name", new IDirective[] { new Queries.Types.Upper(), new Skip("source") }),
                  new Field("active")
               )
            )
         );
         var text = typeQL.ToBodyJson();
         IExecutionResult result = QueryExecutor.Execute(text, typeQL.Variables.ToDictionary());
         var json = result.ToJson();
         Assert.AreEqual(result.Errors.Count, 0);
      }

      [TestMethod]
      public void TestStateList()
      {
         TypeQL typeQL = new TypeQL(
            new QueryType(
               "states",
               new Fields(
                  new Field("id"),
                  new Field("name"),
                  new Field(new QueryType("cities",
                     new Fields(
                        new Field("id"),
                        new Field("name")
                     ))
                 )
               )
            )
         );
         var text = typeQL.ToBodyJson();
         //var text1 = typeQL.ToStringJson();
         IExecutionResult result = QueryExecutor.Execute(text);
         var json = result.ToJson();
         Assert.AreEqual(result.Errors.Count, 0);
      }

      [TestMethod]
      public void TestCitiesList()
      {
         TypeQL typeQL = new TypeQL(
            new QueryType(
               "cities",
               new Fields(
                  new Field("id"),
                  new Field("name"),
                  new Field("stateId"),
                  new Field(new QueryType("state",
                     new Fields(
                        new Field("id"),
                        new Field("name")
                     )
                  ))
               )
            )
         );
         var text = typeQL.ToBodyJson();
         //Debug.Print(typeQL.ToStringJson());
         IExecutionResult result = QueryExecutor.Execute(text);
         var json = result.ToJson();
         Assert.AreEqual(result.Errors.Count, 0);
      }

      [TestMethod]
      public void TestStateWithFragment()
      {
         FragmentType fragmentType = new FragmentType("fields", "state_type");
         TypeQL typeQL = new TypeQL(
            new Fragments(
               new Fragment(
                  new QueryType(fragmentType,
                     new Fields(new Field("id"), new Field("name"))
                  )
               )
            ),
            new QueryType(
               "states",
               new Fields(
                  new Field(fragmentType)
               )
            )
         );
         var text = typeQL.ToBodyJson();
         //var text0 = typeQL.ToStringJson();
         IExecutionResult result = QueryExecutor.Execute(text);
         var json = result.ToJson();
         Assert.AreEqual(result.Errors.Count, 0);
      }

      [TestMethod]
      public void TestStateAndCitiesWithFragment()
      {
         var fragmentType = new FragmentType("fields", "state_type");
         TypeQL typeQL = new TypeQL(
            new Fragments(
               new Fragment(
                  new QueryType(fragmentType,
                     new Fields(
                        new Field("id"),
                        new Field("name"),
                        new Field(new QueryType("cities",
                           new Fields(
                              new Field("id"),
                              new Field("name"),
                              new Field("stateId")
                           )
                        ))
                     )
                  )
               )
            ),
            new QueryType(
               "states",
               new Fields(
                  new Field(fragmentType)
               )
            ),
            new QueryType(
               "states", "data",
               new Fields(
                  new Field(fragmentType)
               )
            )
         );
         var text = typeQL.ToBodyJson();
         IExecutionResult result = QueryExecutor.Execute(text);
         var json = result.ToJson();
         Assert.AreEqual(result.Errors.Count, 0);
      }


      [TestMethod]
      public void TestStateAddWithFragmentWithArguments()
      {
         var fragmentType = new FragmentType("fields", "state_type");
         TypeQL typeQL = new TypeQL(
            new Fragments(
               new Fragment(
                  new QueryType(fragmentType,
                  new Fields(new Field("id"), new Field("name"))
                  )
               )
            ),
            new QueryType(
               "state_add",
               new Fields(
                  new Field(fragmentType)
               ),
               new Arguments(
                  new Argument("input",
                     new State { Id = 0, Name = "MINAS GERAIS", Cities = new List<City>() }
                  )
              )
            )
         );
         var text = typeQL.ToBodyJson();
         IExecutionResult result = QueryExecutor.Execute(text);
         var json = result.ToJson();
         Assert.AreEqual(result.Errors.Count, 0);
      }

      [TestMethod]
      public void TestStateAddWithFragmentWithVariables()
      {
         var state = new State { Id = 0, Name = "MINAS GERAIS", Cities = new List<City>() };
         var fragmentType = new FragmentType("fields", "state_type");
         var fragments = new Fragments(new Fragment(new QueryType(fragmentType, new Fields(new Field("id"), new Field("name")))));
         var arguments = new Arguments(new Argument(new Parameter("input")));
         var variables = new Variables("getState", new Variable<object>("input", state, "state_input"));
         var queryTypes = new QueryType("state_add", new Fields(new Field(fragmentType)), arguments);

         TypeQL typeQL = new TypeQL(variables, fragments, queryTypes);

         var text = typeQL.ToBodyJson();
         //var textComplete = typeQL.ToStringJson();
         var varDic = typeQL.Variables.ToDictionary();
         IExecutionResult result = QueryExecutor.Execute(text, varDic);
         var json = result.ToJson();
         Assert.AreEqual(result.Errors.Count, 0);
      }

      [TestMethod]
      public void TestCarAddWithFragmentWithVariables()
      {
         Car car = new Car
         {
            Active = true,
            Purchase = DateTime.Parse("02/01/1999"),
            Time = TimeSpan.Parse("13:14:55"),
            Title = "Car One",
            Value = 10000M,
            Id = 0
         };
         var fragmentType = new FragmentType("fields", "car_type");
         var fragments = new Fragments(new Fragment(new QueryType(fragmentType, new Fields(new Field("id"), new Field("title"), new Field("active")))));
         var arguments = new Arguments(new Argument(new Parameter("input")));
         var variables = new Variables("getCar", new Variable<object>("input", car, "car_input"));
         var queryTypes = new QueryType("car_add", new Fields(new Field(fragmentType)), arguments);

         TypeQL typeQL = new TypeQL(variables, fragments, queryTypes);

         var text = typeQL.ToBodyJson();
         //var textComplete = typeQL.ToStringJson();
         var varDic = typeQL.Variables.ToDictionary();
         IExecutionResult result = QueryExecutor.Execute(text, varDic);
         var json = result.ToJson();
         Assert.AreEqual(result.Errors.Count, 0);
      }

   }
}