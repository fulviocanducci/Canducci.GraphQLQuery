using Canducci.GraphQLQuery.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Canducci.GraphQLQuery.MSTest
{
   [TestClass]
   public class UnitTestVariables
   {
      [TestMethod]   
      [ExpectedException(typeof(Exception))]
      public void TestVariables()
      {
         IVariable v0 = new Variable("id", 1);
         IVariable v1 = new Variable("id", 1);
         Variables variables = new Variables("get", v0, v1);
         Assert.AreEqual(variables.QueryName, "get");
      }

      [TestMethod]
      public void TestVariablesCount()
      {
         IVariable v0 = new Variable("id", 1);
         IVariable v1 = new Variable("name", "name");
         Variables variables = new Variables("get", v0, v1);
         Assert.IsTrue(variables.Values().Count == 2);
         Assert.IsTrue(variables.Count == 2);
         Assert.AreEqual(variables.QueryName, "get");
         Assert.IsInstanceOfType(variables, typeof(Variables));
      }

      [TestMethod]
      public void TestVariablesStringBuilder()
      {
         IVariable v0 = new Variable("id", 1);
         IVariable v1 = new Variable("name", "name");
         Variables variables = new Variables("get", v0, v1);
         StringBuilder str = new StringBuilder();
         variables.AppendStringBuilder(str);
         Assert.AreEqual("(id:$id,name:$name)", str.ToString());
      }
   }
}
