using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.MSTest.Models;
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
      public void TestArgument()
      {
         IArgument argumentNumber = new Argument("id", 1);
         IArgument argumentDecimal = new Argument("value", 125.00M);
         IArgument argumentFloat = new Argument("value", 300.1F);
         IArgument argumentString = new Argument("name", "Souza");
         IArgument argumentDateTime = new Argument("date", DateTime.Parse("01/01/1970"));
         IArgument argumentTimeSpan = new Argument("time", TimeSpan.Parse("10:00:00"));
         IArgument argumentGuid = new Argument("guid", Guid.Empty);
         IArgument argumentBool = new Argument("active", true);
         IArgument argumentNull = new Argument("null", null);

         Assert.AreEqual("1", argumentNumber.Convert());
         Assert.AreEqual("125.00", argumentDecimal.Convert());
         Assert.AreEqual("300.1", argumentFloat.Convert());
         Assert.AreEqual("\\\"Souza\\\"", argumentString.Convert());
         Assert.AreEqual("\\\"1970-01-01 00:00:00\\\"", argumentDateTime.Convert());
         Assert.AreEqual("\\\"10:00:00\\\"", argumentTimeSpan.Convert());
         Assert.AreEqual("\\\"00000000-0000-0000-0000-000000000000\\\"", argumentGuid.Convert());
         Assert.AreEqual("true", argumentBool.Convert());
         Assert.AreEqual("null", argumentNull.Convert());
      }

      [TestMethod]
      public void TestArgumentKeyValue()
      {
         IArgument argumentNumber = new Argument("id", 1);
         IArgument argumentDecimal = new Argument("value", 125.00M);
         IArgument argumentFloat = new Argument("value", 300.1F);
         IArgument argumentString = new Argument("name", "Souza");
         IArgument argumentDateTime = new Argument("date", DateTime.Parse("01/01/1970"));
         IArgument argumentTimeSpan = new Argument("time", TimeSpan.Parse("10:00:00"));
         IArgument argumentGuid = new Argument("guid", Guid.Empty);
         IArgument argumentBool = new Argument("active", true);
         IArgument argumentNull = new Argument("null", null);

         Assert.AreEqual("id:1", argumentNumber.KeyValue);
         Assert.AreEqual("value:125.00", argumentDecimal.KeyValue);
         Assert.AreEqual("value:300.1", argumentFloat.KeyValue);
         Assert.AreEqual("name:\\\"Souza\\\"", argumentString.KeyValue);
         Assert.AreEqual("date:\\\"1970-01-01 00:00:00\\\"", argumentDateTime.KeyValue);
         Assert.AreEqual("time:\\\"10:00:00\\\"", argumentTimeSpan.KeyValue);
         Assert.AreEqual("guid:\\\"00000000-0000-0000-0000-000000000000\\\"", argumentGuid.KeyValue);
         Assert.AreEqual("active:true", argumentBool.KeyValue);
         Assert.AreEqual("null:null", argumentNull.KeyValue);
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
           "{\"query\":\"{people_add(people:{id:0,name:\\\"test\\\",created:\\\"1970-01-01 00:00:00\\\",active:true,value:0,hours:\\\"14:25:00\\\"}){id,name}}\"}",
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
            Active = true
         };
         TypeQL typeQL = new TypeQL(
           new QueryType(
             "car_add",             
             new Fields(
               new Field("id"),
               new Field("title"),
               new Field("purchase"),
               new Field("value"),
               new Field("active")
             ),
             new Arguments(new Argument("car", car))
           )
         );
         string expect = "{\"query\":\"{car_add(car:{id:0,title:\\\"Car 1\\\",purchase:\\\"2019-08-14 23:54:18\\\",value:10000.00,active:true}){id,title,purchase,value,active}}\"}";
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
            Active = true
         };
         TypeQL typeQL = new TypeQL(
           new QueryType(
             "car_edit",
             new Fields(
               new Field("id"),
               new Field("title"),
               new Field("purchase"),
               new Field("value"),
               new Field("active")
             ),
             new Arguments(new Argument("car", car))
           )
         );
         string expect = "{\"query\":\"{car_edit(car:{id:1,title:\\\"Car 1\\\",purchase:\\\"2019-08-14 23:54:18\\\",value:11000.00,active:true}){id,title,purchase,value,active}}\"}";
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
      public void TestMultipleGraphQLWithVariables()
      {
         TypeQL typeQL = new TypeQL(
            new Variables(
               "getStates",
               new Variable("id", 1, true, 0)
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
               new Arguments(
                  new Argument("id",1 )
               )               
            ),
            new QueryType("countries",
               new Fields(
                  new Field("id"),
                  new Field("name")
               )
            )
         );
         string expect = "{\"query\":\"query getStates($id:Int){state_find(id:$id){id,uf,country{id,name}}countries{id,name}}\",\"variables\":{\"id\":11}}";
         Assert.AreEqual(expect, typeQL);
      }
   }
}
