using Canducci.GraphQLQuery.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace Canducci.GraphQLQuery.MSTest
{
   [TestClass]
   public class UnitTestArguments
   {
      [TestMethod]
      public void TestArguments()
      {
         IArgument argument0 = new Argument("id", 1);
         IArgument argument1 = new Argument(new Parameter("source"));
         Arguments arguments = new Arguments(
            argument0,
            argument1
         );

         Assert.IsTrue(arguments.Count == 2);
         Assert.AreEqual(arguments[0].Name, "id");
         Assert.AreEqual(arguments[0].Value, 1);

         Assert.IsInstanceOfType(arguments[1].Value, typeof(Parameter));
         Assert.AreEqual(((Parameter)arguments[1].Value).Name, "source");
      }

      [TestMethod]
      public void TestArgumentsStringBuilder()
      {
         IArgument v0 = new Argument("id", 1);
         IArgument v1 = new Argument("name", "name");
         Arguments variables = new Arguments(v0, v1);
         StringBuilder str = new StringBuilder();
         variables.Append(str);
         Assert.AreEqual("id:1,name:\\\"name\\\"", str.ToString());
      }
   }
}
