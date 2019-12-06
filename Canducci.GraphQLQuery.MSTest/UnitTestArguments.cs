using Canducci.GraphQLQuery.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canducci.GraphQLQuery.MSTest
{
   [TestClass]
   public class UnitTestArguments
   {
      [TestMethod]
      public void TestArguments()
      {
         IArgument argument0 = new Argument("id", 1);
         IArgument argument1 = new Argument(new Parameter("id"));
         Arguments arguments = new Arguments(
            argument0,
            argument1
         );

         Assert.IsTrue(arguments.Count == 2);
         Assert.AreEqual(arguments[0].Name, "id");
         Assert.AreEqual(arguments[0].Value, 1);

         Assert.IsInstanceOfType(arguments[1].Value, typeof(Parameter));
         Assert.AreEqual(((Parameter)arguments[1].Value).Name, "id");
      }
   }
}
