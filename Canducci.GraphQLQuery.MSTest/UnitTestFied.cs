﻿using Canducci.GraphQLQuery.Abstracts;
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
         IField field3 = new Field("name", 
            new IDirective[] { Directive.Skip("status"), Directive.Include("active") });
         IField field4 = new Field("name", "alias", new IDirective[] { Directive.Include("active") });
         IField field5 = new Field(new QueryType("name", new Fields(new Field("name"))), 
            new IDirective[] { Directive.Include("active") });

         Assert.IsInstanceOfType(field0, typeof(IField));
         Assert.IsInstanceOfType(field1, typeof(IField));
         Assert.IsInstanceOfType(field2, typeof(IField));
         Assert.IsInstanceOfType(field3, typeof(IField));
         Assert.IsInstanceOfType(field4, typeof(IField));

         Assert.AreEqual(field0.Name, "name");
         Assert.AreEqual(field0.Alias, null);
         Assert.AreEqual(field3.Name, "name");
         Assert.AreEqual(field3.Alias, null);
         
         Assert.IsInstanceOfType(field3.Directives[0].GetType(), typeof(Skip).GetType());
         Assert.IsInstanceOfType(field3.Directives[1].GetType(), typeof(Include).GetType());

         Assert.AreEqual(field1.Name, "name");
         Assert.AreEqual(field1.Alias, "alias");
         Assert.AreEqual(field4.Name, "name");
         Assert.AreEqual(field4.Alias, "alias");
         Assert.IsInstanceOfType(field4.Directives[0].GetType(), typeof(Include).GetType());

         Assert.AreEqual(field2.Name, null);
         Assert.AreEqual(field2.Alias, null);
         Assert.IsInstanceOfType(field2.QueryType, typeof(IQueryType));

         Assert.IsNotNull(field5);
         Assert.IsTrue(string.IsNullOrEmpty(field5.Alias));
         Assert.IsNull(field5?.FragmentType);
         Assert.IsNotNull(field5?.Directives);
         Assert.IsNotNull(field5.QueryType);
      }
   }
}
