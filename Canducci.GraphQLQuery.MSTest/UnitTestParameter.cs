using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Canducci.GraphQLQuery.MSTest
{
   [TestClass]
   public class UnitTestParameter
   {
      [TestMethod]
      public void TestParameter()
      {
         Parameter p0 = new Parameter("name");
         Parameter p1 = new Parameter("name", "variable");
         Assert.AreEqual(p0.Name, "name");
         Assert.AreEqual(p0.Variable, null);
         Assert.AreEqual(p1.Name, "name");
         Assert.AreEqual(p1.Variable, "variable");
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentNullException))]
      public void TestParameterArgumentNullException()
      {
         Parameter p0 = new Parameter(null);
         Assert.IsNotNull(p0.Name);
      }
   }
}
