using Canducci.GraphQLQuery.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canducci.GraphQLQuery.MSTest
{
   [TestClass()]
   public class UnitTestFied
   {
      [TestMethod]
      public void TestField()
      {
         IField field0 = new Field("name");
         IField field1 = new Field("name", "alias");
         IField field2 = new Field(new QueryType("name", new Fields(new Field("name"))));

         Assert.IsInstanceOfType(field0, typeof(IField));
         Assert.IsInstanceOfType(field1, typeof(IField));
         Assert.IsInstanceOfType(field2, typeof(IField));

         Assert.AreEqual(field0.Name, "name");
         Assert.AreEqual(field0.Alias, null);

         Assert.AreEqual(field1.Name, "name");
         Assert.AreEqual(field1.Alias, "alias");

         Assert.AreEqual(field2.Name, null);
         Assert.AreEqual(field2.Alias, null);
         Assert.IsInstanceOfType(field2.QueryType, typeof(IQueryType));
      }
   }
}
