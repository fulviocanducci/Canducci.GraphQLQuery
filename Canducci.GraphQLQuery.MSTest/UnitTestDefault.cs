using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.MSTest.Models;
using Canducci.GraphQLQuery.VariablesValueTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;

namespace Canducci.GraphQLQuery.MSTest
{
   [TestClass]   
   public class UnitTestDefault
   {
      public TypeQL TypeQLTest(params QueryType[] queryTypes)
      {         
         return new TypeQL(queryTypes);
      }

      [TestMethod]
      public void TestPeopleWithFieldsIdName()
      {
         var queryType = new QueryType(
           name: "peoples",
           fields: new Fields(
             new Field("id"),
             new Field("name")
           )
         );
         var typeQLTest = TypeQLTest(queryType);
         Assert.AreEqual("{\"query\":\"{peoples{id,name}}\"}", typeQLTest.ToStringJson());
      }

      [TestMethod]
      public void TestPeopleFindParamIdFieldsIdName()
      {
         var queryType = new QueryType(
           name: "people_find",
           arguments: new Arguments(
             new Argument("id", 1)
           ),
           fields: new Fields(
             new Field("id"),
             new Field("name")
           )
         );
         var typeQLTest = TypeQLTest(queryType);
         Assert.AreEqual("{\"query\":\"{people_find(id:1){id,name}}\"}", typeQLTest.ToStringJson());
      }

      [TestMethod]
      public void TestPeopleAddParamIdNameFieldsIdName()
      {
         var queryType = new QueryType(
           name: "people_add",
           arguments: new Arguments(
             new Argument("people", new People(0, "test", DateTime.Parse("01/01/1970"), true, 0, TimeSpan.Parse("14:25:00")))
           ),
           fields: new Fields(
             new Field("id"),
             new Field("name")
           )
         );

         var typeQLTest = TypeQLTest(queryType);
         Assert.AreEqual(
           "{\"query\":\"{people_add(people:{id:0,name:\\\"test\\\",created:\\\"1970-01-01T00:00:00.000Z\\\",active:true,value:0,hours:\\\"14:25:00\\\"}){id,name}}\"}",
             typeQLTest.ToStringJson()
         );
      }

      [TestMethod]
      public void TestCarAdd()
      {
         Car car = new Car()
         {
            Id = 0,
            Title = "Car 1",
            Purchase = DateTime.Parse("2019-08-14 23:54:18"),
            Value = 10000.00M,
            Active = true,
            Time = null
         };
         TypeQL typeQL = new TypeQL(
           new QueryType(
             "car_add",
             new Fields(
               new Field("id"),
               new Field("title"),
               new Field("purchase"),
               new Field("value"),
               new Field("active"),
               new Field("time")
             ),
             new Arguments(new Argument("car", car))
           )
         );
         string expect = "{\"query\":\"{car_add(car:{id:0,title:\\\"Car 1\\\",purchase:\\\"2019-08-14T23:54:18.000Z\\\",value:10000.00,active:true,time:null}){id,title,purchase,value,active,time}}\"}";
         Assert.AreEqual(expect, typeQL);
      }

      [TestMethod]
      public void TestCarEdit()
      {
         Car car = new Car()
         {
            Id = 1,
            Title = "Car 1",
            Purchase = DateTime.Parse("2019-08-14 23:54:18"),
            Value = 11000.00M,
            Active = true,
            Time = null
         };
         TypeQL typeQL = new TypeQL(
           new QueryType(
             "car_edit",
             new Fields(
               new Field("id"),
               new Field("title"),
               new Field("purchase"),
               new Field("value"),
               new Field("active"),
               new Field("time")
             ),
             new Arguments(new Argument("car", car))
           )
         );
         string expect = "{\"query\":\"{car_edit(car:{id:1,title:\\\"Car 1\\\",purchase:\\\"2019-08-14T23:54:18.000Z\\\",value:11000.00,active:true,time:null}){id,title,purchase,value,active,time}}\"}";
         Assert.AreEqual(expect, typeQL);
      }

      [TestMethod]
      public void TestCarFind()
      {
         TypeQL typeQL = new TypeQL(
           new QueryType(
             "car_find",
             new Fields(
               new Field("id"),
               new Field("title"),
               new Field("purchase"),
               new Field("value"),
               new Field("active")
             ),
             new Arguments(new Argument("id", 1))
           )
         );
         string expect = "{\"query\":\"{car_find(id:1){id,title,purchase,value,active}}\"}";
         Assert.AreEqual(expect, typeQL);
      }

      [TestMethod]
      public void TestCarDelete()
      {
         TypeQL typeQL = new TypeQL(
           new QueryType(
             "car_delete",
             new Fields(
               new Field("description"),
               new Field("operation"),
               new Field("status")
             ),
             new Arguments(new Argument("id", 1))
           )
         );
         string expect = "{\"query\":\"{car_delete(id:1){description,operation,status}}\"}";
         Assert.AreEqual(expect, typeQL);
      }

      [TestMethod]
      public void TestItemsNullFields()
      {
         Items item = new Items() { Id = null, Title = "Item", Updated = null };
         TypeQL typeQL = new TypeQL(
           new QueryType(
             "item_add",
             new Fields(
               new Field("id"),
               new Field("title"),
               new Field("updated")
             ),
             new Arguments(
               new Argument("item", item)
             )
           )
         );
         string expect = "{\"query\":\"{item_add(item:{id:null,title:\\\"Item\\\",updated:null}){id,title,updated}}\"}";
         Assert.AreEqual(expect, typeQL);
      }
      
      [TestMethod]
      public void TestItemsGuidEmptyValueAndNullFields()
      {
         Items item = new Items() { Id = Guid.Empty, Title = "Item", Updated = null };
         TypeQL typeQL = new TypeQL(
           new QueryType(
             "item_add",
             new Fields(
               new Field("id"),
               new Field("title"),
               new Field("updated")
             ),
             new Arguments(
               new Argument("item", item)
             )
           )
         );
         string expect = "{\"query\":\"{item_add(item:{id:\\\"00000000-0000-0000-0000-000000000000\\\",title:\\\"Item\\\",updated:null}){id,title,updated}}\"}";
         Assert.AreEqual(expect, typeQL);
      }
      
      [TestMethod]
      public void TestMultipleGraphQL()
      {
         TypeQL typeQL = new TypeQL(
            new QueryType("state_find",
               new Fields(
                  new Field("id"),
                  new Field("uf"),
                  new Field(
                     new QueryType("country",
                        new Fields(
                           new Field("id"),
                           new Field("name")
                        )
                     )
                  )
               ),
               new Arguments(new Argument("id", 11))
            ),
            new QueryType("state_find", "d",
               new Fields(
                  new Field("id"),
                  new Field("uf"),
                  new Field(
                     new QueryType("country",
                        new Fields(
                           new Field("id"),
                           new Field("name")
                        )
                     )
                  )
               ),
               new Arguments(new Argument("id", 12))
            )
         );
         string expect = "{\"query\":\"{state_find(id:11){id,uf,country{id,name}}d:state_find(id:12){id,uf,country{id,name}}}\"}";
         Assert.AreEqual(expect, typeQL);
      }

      [TestMethod]      
      public void TestMultipleGraphQLWithVariables()
      {
         TypeQL typeQL = new TypeQL(
            new Variables("getStates",
               new Variable("id", 11, true, new VariableValueDefaultInt(0))
            ),
            new QueryType("state_find",
               new Fields(
                  new Field("id"),
                  new Field("uf"),
                  new Field(
                     new QueryType("country",
                        new Fields(
                           new Field("id"),
                           new Field("name")
                        )
                     )
                  )
               ),
               new Arguments(new Argument(new Parameter("id")))
            ),
            new QueryType("countries",
               new Fields(
                  new Field("id"),
                  new Field("name"),
                  new Field(
                     new QueryType("state",
                        new Fields(
                           new Field("id"),
                           new Field("uf")
                        )
                     )
                  )
               ),
               new Arguments(
                  new Argument("load", true)
              )
            )
         );
         string expect = "{\"query\":\"query getStates($id:Int!=0){state_find(id:$id){id,uf,country{id,name}}countries(load:true){id,name,state{id,uf}}}\",\"variables\":{\"id\":11}}";
         Assert.AreEqual(expect, typeQL);
      }

      [TestMethod]
      public void TestParamFullNullable()
      {
         TypeQL typeQL = new TypeQL(
            new QueryType("source_param_add",
               new Fields(
                  new Field("id"),
                  new Field("name"),
                  new Field("value"),
                  new Field("active"),
                  new Field("created"),
                  new Field("time")
               ),
               new Arguments(
                  new Argument("id", null),
                  new Argument("name", null),
                  new Argument("value", null),
                  new Argument("active", null),
                  new Argument("created", null),
                  new Argument("time", null)
               )
            )
         );

         string expected = "{\"query\":\"{source_param_add(id:null,name:null,value:null,active:null,created:null,time:null){id,name,value,active,created,time}}\"}";
         Assert.AreEqual(expected, typeQL.ToStringJson());
      }

      [TestMethod]      
      public void TestVariablesNullable()
      {
         Source source = new Source();
         TypeQL typeQL = new TypeQL(
            new Variables("getSource",
               new Variable("input", source, "source_input")
            ),
            new QueryType("source_add",
               new Fields(
                  new Field("id"),
                  new Field("name"),
                  new Field("value"),
                  new Field("active"),
                  new Field("created"),
                  new Field("time")
               ),
               new Arguments(
                  new Argument(
                     new Parameter("input")
                  )
               )
            )
         );
         string expected = "{\"query\":\"query getSource($input:source_input){source_add(input:$input){id,name,value,active,created,time}}\",\"variables\":{\"input\":{\"id\":null,\"name\":null,\"value\":null,\"created\":null,\"active\":null,\"time\":\"00:00:00\"}}}";
         Assert.AreEqual(expected, typeQL.ToStringJson());
      }      
   }
}
