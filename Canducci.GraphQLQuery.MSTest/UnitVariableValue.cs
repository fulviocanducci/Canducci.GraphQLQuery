using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.Internals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canducci.GraphQLQuery.MSTest
{
   [TestClass]
   public class UnitVariableValue
   {
      [TestMethod]
      public void TestVariableValue()
      {
         IVariableValue variableValue = new VariableValue("id", 1, typeof(int));
         Assert.AreEqual(variableValue.Name, "id");
         Assert.AreEqual(variableValue.Value, 1);
         Assert.IsInstanceOfType(variableValue.Type.GetType(), typeof(int).GetType());
         Assert.IsInstanceOfType(variableValue.GetType(), typeof(IVariableValue).GetType());
         Assert.IsInstanceOfType(variableValue.GetType(), typeof(VariableValue).GetType());
      }
   }
}
