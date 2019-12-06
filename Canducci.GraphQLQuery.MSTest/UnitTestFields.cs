using Canducci.GraphQLQuery.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Canducci.GraphQLQuery.MSTest
{
   [TestClass()]
   public class UnitTestFields
   {
      [TestMethod]
      public void TestFields()
      {
         IField field0 = new Field("name");
         IField field1 = new Field("name", "alias");
         IField field2 = new Field(new QueryType("name", new Fields(new Field("name"))));
         Fields fields = new Fields(
            field0,
            field1,
            field2
         );

         Assert.IsTrue(fields.Count == 3);
         Assert.IsInstanceOfType(fields, typeof(Fields));
         Assert.IsInstanceOfType(fields[0], typeof(IField));
         Assert.IsInstanceOfType(fields[1], typeof(IField));
         Assert.IsInstanceOfType(fields[2], typeof(IField));

         Assert.AreEqual(fields[0].Name, "name");
         Assert.AreEqual(fields[0].Alias, null);

         Assert.AreEqual(fields[1].Name, "name");
         Assert.AreEqual(fields[1].Alias, "alias");

         Assert.AreEqual(fields[2].Name, null);
         Assert.AreEqual(fields[2].Alias, null);
         Assert.IsInstanceOfType(fields[2].QueryType, typeof(IQueryType));
      }
   }
}
