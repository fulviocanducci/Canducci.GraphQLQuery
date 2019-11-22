using Canducci.GraphQLQuery.MSTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
          new Argument<People>("people", new People(0, "test"))
        ),
        fields: new Fields(
          new Field("id"),
          new Field("name")
        )
      );
      
      var typeQLTest = TypeQLTest(queryType);
      Assert.AreEqual(
        "{\"query\":\"{people_add(people:{id:\"000\",name:\"test\"}){id,name}}\"}",
          typeQLTest.ToStringJson()
      );
    }
  }
}
