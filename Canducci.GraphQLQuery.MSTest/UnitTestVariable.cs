using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.MSTest.Models;
using Canducci.GraphQLQuery.VariablesValueTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Canducci.GraphQLQuery.MSTest
{

   [TestClass]
   public class UnitTestVariable
   {
      [TestMethod]
      public void TestVariable()
      {
         IVariable variableInt = new Variable<int>("id", 1);
         IVariable variableString = new Variable<string>("name", "name");
         IVariable variableFloat = new Variable<float>("value", 100F);
         IVariable variableBoolean = new Variable<bool>("active", true);
         IVariable variableObject = new Variable<Car>("car", new Car(), "input");
         IVariable variableTimeSpan = new Variable<TimeSpan>("time", TimeSpan.Parse("13:00:00"));
         IVariable variableDefaultValue = new Variable<int>("id", 2, "id", true, 0);
         IVariable variableID = new Variable<object>("id", 2, "id", true, 0, Format.FormatID);

         Assert.AreEqual("id", variableInt.Name);
         Assert.AreEqual(null, variableInt.NameType);
         Assert.AreEqual(false, variableInt.Required);
         Assert.AreEqual(null, variableInt.VariableValueDefault);
         Assert.AreEqual(TimeSpan.Parse("13:00:00"), variableTimeSpan.Value);
         Assert.AreEqual(2, variableID.Value);

         Assert.AreEqual("id", variableDefaultValue.Name);
         Assert.AreEqual("id", variableDefaultValue.NameType);
         Assert.AreEqual(true, variableDefaultValue.Required);
         Assert.AreEqual("0", variableDefaultValue.VariableValueDefault.Value);
         Assert.AreEqual("0", variableID.VariableValueDefault.Value);

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
         Assert.AreEqual("id:$id", variableID.GetKeyArgument());
      }

      [TestMethod]
      public void TestVariableRequired()
      {
         IVariable variableInt = new Variable<int>("id", 1, true);
         IVariable variableString = new Variable<string>("name", "name", true);
         IVariable variableFloat = new Variable<float>("value", 100F, true);
         IVariable variableBoolean = new Variable<bool>("active", true, true);
         IVariable variableObject = new Variable<Car>("car", new Car(), "input", true);
         IVariable variableID = new Variable<object>("id", 2, "ID", true, null, Format.FormatID);

         Assert.AreEqual("$id:Int!", variableInt.GetKeyParam());
         Assert.AreEqual("$name:String!", variableString.GetKeyParam());
         Assert.AreEqual("$value:Float!", variableFloat.GetKeyParam());
         Assert.AreEqual("$active:Boolean!", variableBoolean.GetKeyParam());
         Assert.AreEqual("$car:input!", variableObject.GetKeyParam());
         Assert.AreEqual("$id:ID!", variableID.GetKeyParam());
      }

      [TestMethod]
      public void TestVariableDefaultValue()
      {
         IVariable variableObject = new Variable<Car>("car", new Car(), "input", false, VariableValue.Null);
         IVariable variableInt = new Variable<int>("id", 1, false, 0);
         IVariable variableString = new Variable<string>("name", "name", false, "n");
         IVariable variableFloat = new Variable<float>("value", 100F, false, 0);
         IVariable variableBoolean = new Variable<bool>("active", true, false, false);
         IVariable variableID = new Variable<object>("id", 2, "ID", false, 0, Format.FormatID);

         Assert.AreEqual("$car:input=null", variableObject.GetKeyParam());
         Assert.AreEqual("$id:Int=0", variableInt.GetKeyParam());
         Assert.AreEqual("$name:String=n", variableString.GetKeyParam());
         Assert.AreEqual("$value:Float=0", variableFloat.GetKeyParam());
         Assert.AreEqual("$active:Boolean=false", variableBoolean.GetKeyParam());
         Assert.AreEqual("$id:ID=0", variableID.GetKeyParam());

         Assert.AreEqual("input", variableObject.Convert());
         Assert.AreEqual(variableObject.Value, variableObject.GetValue());

         Assert.IsInstanceOfType(variableInt.VariableType.Type, typeof(int).GetType());
         Assert.IsInstanceOfType(variableString.VariableType.Type, typeof(string).GetType());
         Assert.IsInstanceOfType(variableFloat.VariableType.Type, typeof(float).GetType());
         Assert.IsInstanceOfType(variableBoolean.VariableType.Type, typeof(bool).GetType());
         Assert.IsInstanceOfType(variableID.VariableType.Type, typeof(object).GetType());
      }

   }
}
