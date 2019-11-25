using Canducci.GraphQLQuery.MSTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
          new Argument<int>("id", 1)
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
          new Argument<People>("people", new People(0, "test", DateTime.Parse("01/01/1970"), true, 0))
        ),
        fields: new Fields(
          new Field("id"),
          new Field("name")
        )
      );
      
      var typeQLTest = TypeQLTest(queryType);
      Assert.AreEqual(
        "{\"query\":\"{people_add(people:{id:0,name:\\\"test\\\",created:\\\"1970-01-01 00:00:00\\\",active:true,value:0}){id,name}}\"}",
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
          new Arguments(new Argument<Car>("car", car)),
          new Fields(
            new Field("id"),
            new Field("title"),
            new Field("purchase"),
            new Field("value"),
            new Field("active")
          )
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
          new Arguments(new Argument<Car>("car", car)),
          new Fields(
            new Field("id"),
            new Field("title"),
            new Field("purchase"),
            new Field("value"),
            new Field("active")
          )
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
          new Arguments(new Argument<int>("id", 1)),
          new Fields(
            new Field("id"),
            new Field("title"),
            new Field("purchase"),
            new Field("value"),
            new Field("active")
          )
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
          new Arguments(new Argument<int>("id", 1)),
          new Fields(
            new Field("description"),
            new Field("operation"),
            new Field("status")
          )
        )
      );
      string expect = "{\"query\":\"{car_delete(id:1){description,operation,status}}\"}";
      Assert.AreEqual(expect, typeQL);
    }
  }
}
