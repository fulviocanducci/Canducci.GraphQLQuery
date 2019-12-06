using Canducci.GraphQLQuery.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canducci.GraphQLQuery.MSTest
{
   [TestClass]
   public class UnitTestQueryType
   {
      [TestMethod]
      public void TestQueryType()
      {
         IQueryType queryType0 = new QueryType("query", 
            new Fields(new Field("id")));

         IQueryType queryType1 = new QueryType("query",
            new Fields(new Field("id")),
            new Arguments(new Argument("id", 1)));

         IQueryType queryType2 = new QueryType("query", "alias", 
            new Fields(new Field("id")));

         IQueryType queryType3 = new QueryType("query", "alias", 
            new Fields(new Field("id")),
            new Arguments(new Argument("id", 1)));

         Assert.AreEqual(queryType0.Name, "query");
         Assert.IsTrue(queryType0.Fields.Count == 1);
         Assert.AreEqual(queryType1.Name, "query");
         Assert.IsTrue(queryType1.Fields.Count == 1);
         Assert.IsTrue(queryType1.Arguments.Count == 1);
         Assert.AreEqual(queryType2.Name, "query");
         Assert.IsTrue(queryType2.Fields.Count == 1);
         Assert.AreEqual(queryType2.Name, "query");
         Assert.AreEqual(queryType3.Alias,"alias");
         Assert.IsTrue(queryType3.Fields.Count == 1);
         Assert.AreEqual(queryType3.Alias, "alias");
         Assert.IsTrue(queryType3.Arguments.Count == 1);
      }
   }
}
