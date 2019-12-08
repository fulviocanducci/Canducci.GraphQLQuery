using Canducci.GraphQLQuery.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Canducci.GraphQLQuery.MSTest
{

   [TestClass]
   public class UnitTestArgument
   {
      [TestMethod]
      public void TestArgument()
      {
         IArgument argumentId = new Argument("id", 1);
         IArgument argumentTime = new Argument("time", TimeSpan.Parse("03:03:25"));
         IArgument argumentParameter = new Argument(new Parameter("id"));

         Assert.AreEqual(argumentId.Name, "id");
         Assert.AreEqual(argumentId.Value, 1);

         Assert.AreEqual(argumentTime.Name, "time");
         Assert.AreEqual(argumentTime.Value, TimeSpan.Parse("03:03:25"));

         Assert.IsInstanceOfType(argumentParameter.Value, typeof(Parameter));
         Assert.AreEqual(((Parameter)argumentParameter.Value).Name, "id");
      }

      [TestMethod]
      public void TestArgumentConvert()
      {
         IArgument argumentNumber = new Argument("id", 1);
         IArgument argumentDecimal = new Argument("value", 125.00M);
         IArgument argumentFloat = new Argument("value", 300.1F);
         IArgument argumentString = new Argument("name", "Paul");
         IArgument argumentDateTime = new Argument("date", DateTime.Parse("1970-01-01"));
         IArgument argumentTimeSpan = new Argument("time", TimeSpan.Parse("10:00:00"));
         IArgument argumentGuid = new Argument("guid", Guid.Empty);
         IArgument argumentBool = new Argument("active", true);
         IArgument argumentNull = new Argument("null", null);
         
         IArgument argumentParameter = new Argument(new Parameter("id"));        

         Assert.AreEqual("1", argumentNumber.Convert());
         Assert.AreEqual("125.00", argumentDecimal.Convert());
         Assert.AreEqual("300.1", argumentFloat.Convert());
         Assert.AreEqual("\\\"Paul\\\"", argumentString.Convert());
         Assert.AreEqual("\\\"1970-01-01T00:00:00.000Z\\\"", argumentDateTime.Convert());
         Assert.AreEqual("\\\"10:00:00\\\"", argumentTimeSpan.Convert());
         Assert.AreEqual("\\\"00000000-0000-0000-0000-000000000000\\\"", argumentGuid.Convert());
         Assert.AreEqual("true", argumentBool.Convert());
         Assert.AreEqual("null", argumentNull.Convert());

         Assert.AreEqual("$id", argumentParameter.Convert());         
      }

      [TestMethod]
      public void TestArgumentKeyValue()
      {
         IArgument argumentNumber = new Argument("id", 1);
         IArgument argumentDecimal = new Argument("value", 125.00M);
         IArgument argumentFloat = new Argument("value", 300.1F);
         IArgument argumentString = new Argument("name", "Paul");
         IArgument argumentDateTime = new Argument("date", DateTime.Parse("01/01/1970"));
         IArgument argumentTimeSpan = new Argument("time", TimeSpan.Parse("10:00:00"));
         IArgument argumentGuid = new Argument("guid", Guid.Empty);
         IArgument argumentBool = new Argument("active", true);
         IArgument argumentNull = new Argument("null", null);
         IArgument argumentParameter = new Argument(new Parameter("id"));
         Assert.AreEqual("id:1", argumentNumber.KeyValue);
         Assert.AreEqual("value:125.00", argumentDecimal.KeyValue);
         Assert.AreEqual("value:300.1", argumentFloat.KeyValue);
         Assert.AreEqual("name:\\\"Paul\\\"", argumentString.KeyValue);
         Assert.AreEqual("date:\\\"1970-01-01T00:00:00.000Z\\\"", argumentDateTime.KeyValue);
         Assert.AreEqual("time:\\\"10:00:00\\\"", argumentTimeSpan.KeyValue);
         Assert.AreEqual("guid:\\\"00000000-0000-0000-0000-000000000000\\\"", argumentGuid.KeyValue);
         Assert.AreEqual("active:true", argumentBool.KeyValue);
         Assert.AreEqual("null:null", argumentNull.KeyValue);
         Assert.AreEqual("id:$id", argumentParameter.KeyValue);         
      }
   }
}
