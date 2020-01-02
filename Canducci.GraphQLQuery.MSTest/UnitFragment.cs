using Canducci.GraphQLQuery.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canducci.GraphQLQuery.MSTest
{
   [TestClass]
   public class UnitFragment
   {
      [TestMethod]
      public void TestFragment()
      {
         IFragment fragment = new Fragment(new QueryType("get", new Fields("id", "name")));
         Assert.IsInstanceOfType(fragment.QueryType.GetType(), typeof(IQueryType).GetType());
         Assert.IsInstanceOfType(fragment.QueryType.GetType(), typeof(QueryType).GetType());
         Assert.IsNotNull(fragment.QueryType);
         Assert.AreEqual(fragment.QueryType.Fields.Count, 2);
         Assert.AreEqual(fragment.QueryType.Name, "get");
         Assert.IsNull(fragment.QueryType.Alias);
         Assert.IsNull(fragment.QueryType.Arguments);
         Assert.IsNull(fragment.QueryType.FragmentType);         
      }
   }
}
