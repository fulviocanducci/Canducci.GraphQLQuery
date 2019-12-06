using Canducci.GraphQLQuery.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;

namespace Canducci.GraphQLQuery.MSTest
{
   [TestClass]
   public class UnitTestTypeQL
   {
      [TestMethod]
      public void TestTypeQL()
      {
         IQueryType queryType = new QueryType("name", new Fields(new Field("id")));
         Variables variables = new Variables("get", new Variable("id", 1));
         ITypeQL typeQL0 = new TypeQL(queryType);
         ITypeQL typeQL1 = new TypeQL(variables, queryType);

         Assert.IsTrue(typeQL0.QueryTypes.Length == 1);
         Assert.IsNull(typeQL0.Variables);
         Assert.IsTrue(typeQL1.QueryTypes.Length == 1);
         Assert.IsNotNull(typeQL1.Variables);
         Assert.IsTrue(typeQL1.Variables.Count == 1);

         string expected0 = "{\"query\":\"{name{id}}\"}";
         string expected1 = "{\"query\":\"query get($id:Int){name{id}}\",\"variables\":{\"id\":1}}";
         Assert.AreEqual(expected0, typeQL0.ToStringJson());
         Assert.AreEqual(expected1, typeQL1.ToStringJson());

         Assert.IsInstanceOfType(queryType, typeof(IQueryType));
         Assert.IsInstanceOfType(variables, typeof(Variables));
         Assert.IsInstanceOfType(typeQL0, typeof(ITypeQL));
         Assert.IsInstanceOfType(typeQL1, typeof(ITypeQL));
         Assert.IsInstanceOfType(typeQL0.ToStringJson(), typeof(string));
         Assert.IsInstanceOfType(typeQL1.ToStringJson(), typeof(string));         
      }

      [TestMethod]
      public void TestTypeQLMultipleQuery()
      {
         IQueryType queryType0 = new QueryType("states", new Fields(
            new Field("id"),
            new Field("uf"),
            new Field(new QueryType("contries", new Fields(
               new Field("id"),
               new Field("name")
            ))))
         );

         IQueryType queryType1 = new QueryType("contries", new Fields(
               new Field("id"),
               new Field("name")
            )
         );

         ITypeQL typeQL0 = new TypeQL(queryType0, queryType1);
         Assert.IsTrue(typeQL0.QueryTypes.Length == 2);
         Assert.IsNull(typeQL0.Variables);
         string expected0 = "{\"query\":\"{states{id,uf,contries{id,name}}contries{id,name}}\"}";         
         Assert.AreEqual(expected0, typeQL0.ToStringJson());

      }

      [TestMethod]
      public void TestTypeQLMultipleQueryAndVariables()
      {
         Variables variables = new Variables("get", new Variable("load", true));
         IQueryType queryType0 = new QueryType("states", new Fields(
            new Field("id"),
            new Field("uf"),
            new Field(new QueryType("contries", new Fields(
               new Field("id"),
               new Field("name")
            ))))
         );

         IQueryType queryType1 = new QueryType("contries", new Fields(
               new Field("id"),
               new Field("name")
            )
         );

         ITypeQL typeQL0 = new TypeQL(variables, queryType0, queryType1);
         Assert.IsTrue(typeQL0.QueryTypes.Length == 2);
         Assert.IsNotNull(typeQL0.Variables);
         Assert.IsTrue(typeQL0.Variables.Count == 1);
         string expected0 = "{\"query\":\"query get($load:Boolean){states{id,uf,contries{id,name}}contries{id,name}}\",\"variables\":{\"load\":true}}";
         Assert.AreEqual(expected0, typeQL0.ToStringJson());
      }

      [TestMethod]
      public void TestConvertTimeSpan()
      {
         Source source = new Source()
         {
            Time = TimeSpan.Parse("13:02:00")
         };
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

         string expected = "{\"query\":\"query getSource($input:source_input){source_add(input:$input){id,name,value,active,created,time}}\",\"variables\":{\"input\":{\"id\":null,\"name\":null,\"value\":null,\"created\":null,\"active\":null,\"time\":\"13:02:00\"}}}";
         Assert.AreEqual(expected, typeQL.ToStringJson());
      }
   }
}
