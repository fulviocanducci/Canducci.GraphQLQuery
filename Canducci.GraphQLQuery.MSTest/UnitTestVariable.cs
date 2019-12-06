using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.MSTest.Models;
using Canducci.GraphQLQuery.VariablesValueTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canducci.GraphQLQuery.MSTest
{

   [TestClass]
   public class UnitTestVariable
   {
      [TestMethod]
      public void TestVariable()
      {
         IVariable variableInt = new Variable("id", 1);
         IVariable variableString = new Variable("name", "name");
         IVariable variableFloat = new Variable("value", 100F);
         IVariable variableBoolean = new Variable("active", true);
         IVariable variableObject = new Variable("car", new Car(), "input");
         IVariable variableDefaultValue = new Variable("id", 2, "id", true, 0);

         Assert.AreEqual("id", variableInt.Name);
         Assert.AreEqual(null, variableInt.NameType);
         Assert.AreEqual(false, variableInt.Required);
         Assert.AreEqual(null, variableInt.VariableValueDefault);

         Assert.AreEqual("id", variableDefaultValue.Name);
         Assert.AreEqual("id", variableDefaultValue.NameType);
         Assert.AreEqual(true, variableDefaultValue.Required);
         Assert.AreEqual("0", variableDefaultValue.VariableValueDefault.Value);

         Assert.AreEqual("$id:Int", variableInt.GetKeyParam());
         Assert.AreEqual("id:$id", variableInt.GetKeyArgument());
         Assert.AreEqual("$name:String", variableString.GetKeyParam());
         Assert.AreEqual("name:$name", variableString.GetKeyArgument());
         Assert.AreEqual("$value:Float", variableFloat.GetKeyParam());
         Assert.AreEqual("value:$value", variableFloat.GetKeyArgument());
         Assert.AreEqual("$active:Boolean", variableBoolean.GetKeyParam());
         Assert.AreEqual("active:$active", variableBoolean.GetKeyArgument());
         Assert.AreEqual("$car:input", variableObject.GetKeyParam());
         Assert.AreEqual("car:$car", variableObject.GetKeyArgument());
      }

      [TestMethod]
      public void TestVariableRequired()
      {
         IVariable variableInt = new Variable("id", 1, true);
         IVariable variableString = new Variable("name", "name", true);
         IVariable variableFloat = new Variable("value", 100F, true);
         IVariable variableBoolean = new Variable("active", true, true);
         IVariable variableObject = new Variable("car", new Car(), "input", true);

         Assert.AreEqual("$id:Int!", variableInt.GetKeyParam());
         Assert.AreEqual("$name:String!", variableString.GetKeyParam());
         Assert.AreEqual("$value:Float!", variableFloat.GetKeyParam());
         Assert.AreEqual("$active:Boolean!", variableBoolean.GetKeyParam());
         Assert.AreEqual("$car:input!", variableObject.GetKeyParam());
      }

      [TestMethod]
      public void TestVariableDefaultValue()
      {
         IVariable variableObject = new Variable("car", new Car(), "input", false, VariableValue.Null);
         IVariable variableInt = new Variable("id", 1, false, 0);
         IVariable variableString = new Variable("name", "name", false, "n");
         IVariable variableFloat = new Variable("value", 100F, false, 0);
         IVariable variableBoolean = new Variable("active", true, false, false);

         Assert.AreEqual("$car:input=null", variableObject.GetKeyParam());
         Assert.AreEqual("$id:Int=0", variableInt.GetKeyParam());
         Assert.AreEqual("$name:String=n", variableString.GetKeyParam());
         Assert.AreEqual("$value:Float=0", variableFloat.GetKeyParam());
         Assert.AreEqual("$active:Boolean=false", variableBoolean.GetKeyParam());
      }

   }
}
