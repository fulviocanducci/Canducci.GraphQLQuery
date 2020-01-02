using Canducci.GraphQLQuery.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Canducci.GraphQLQuery.MSTest
{
   [TestClass()]
   public class UnitTestFields
   {
      [TestMethod]
      [ExpectedException(typeof(Exception))]      
      public void TestFieldsDuplicateName()
      {
         IField field0 = new Field("name");
         IField field1 = new Field("name", "alias");
         Fields fields = new Fields(
            field0,
            field1
         );
         Assert.IsTrue(fields.Count == 2);
      }

      [TestMethod]
      [ExpectedException(typeof(Exception))]
      public void TestFieldsQueryTypeDuplicateName()
      {
         IField field0 = new Field(new QueryType("name", new Fields(new Field("name"), new Field("name"))));
         IField field1 = new Field(new QueryType("name", new Fields(new Field("year"), new Field("year"))));
         IField field2 = new Field(new QueryType("name", new Fields("year", "year")));
         Fields fields = new Fields(
            field0,
            field1,
            field2
         );
         Assert.IsTrue(fields.Count == 3);
      }

      [TestMethod]
      public void TestFieldsStringSimpleParam()
      {
         Fields fields = new Fields(
            "id",
            "name",
            "created",
            "value",
            "active"
         );
         Assert.IsInstanceOfType(fields.GetType(), typeof(Fields).GetType());
         Assert.IsTrue(fields.Count == 5);
      }

      [TestMethod]
      public void TestFieldsStringSimpleParamWithAlias()
      {
         Fields fields = new Fields(
            "id,_id",
            "name,_name"
         );
         Assert.IsInstanceOfType(fields.GetType(), typeof(Fields).GetType());
         Assert.IsTrue(fields.Count == 2);
         Assert.AreEqual(fields[0].Name, "id");
         Assert.AreEqual(fields[0].Alias, "_id");
         Assert.AreEqual(fields[1].Name, "name");
         Assert.AreEqual(fields[1].Alias, "_name");
      }

      
      [TestMethod]
      public void TestFields()
      {
         IField field0 = new Field("name");
         IField field1 = new Field("created", "alias");
         IField field2 = new Field(new QueryType("name", new Fields(new Field("name"), new Field("year"))));
         IField field3 = new Field(new QueryType("name", new Fields(new Field("name"))));
         Fields fields = new Fields(
            field0,
            field1,
            field2,
            field3
         );

         Assert.IsTrue(fields.Count == 4);
         Assert.IsInstanceOfType(fields, typeof(Fields));
         Assert.IsInstanceOfType(fields[0], typeof(IField));
         Assert.IsInstanceOfType(fields[1], typeof(IField));
         Assert.IsInstanceOfType(fields[2], typeof(IField));

         Assert.AreEqual(fields[0].Name, "name");
         Assert.AreEqual(fields[0].Alias, null);

         Assert.AreEqual(fields[1].Name, "created");
         Assert.AreEqual(fields[1].Alias, "alias");

         Assert.AreEqual(fields[2].Name, null);
         Assert.AreEqual(fields[2].Alias, null);
         Assert.IsInstanceOfType(fields[2].QueryType, typeof(IQueryType));
      }
   }
}
