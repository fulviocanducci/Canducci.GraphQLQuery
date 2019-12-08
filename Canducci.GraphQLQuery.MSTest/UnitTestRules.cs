using Canducci.GraphQLQuery.Internals;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Canducci.GraphQLQuery.MSTest

{
   [TestClass]
   public class UnitTestRules
   {
      internal GraphQLRulesExecute Execute;

      [TestInitialize()]
      public void InitTestRules()
      {
         Execute = new GraphQLRulesExecute();
      }

      [TestMethod]
      public void TestRules()
      {
         var TestInt = new GraphQLRule(typeof(int), Format.FormatNumber, Execute.GetFormatIntAction);
         var TestUInt = new GraphQLRule(typeof(uint), Format.FormatNumber, Execute.GetFormatIntAction);

         var TestFloat = new GraphQLRule(typeof(float), Format.FormatNumber, Execute.GetFormatFloatAction);
         var TestDouble = new GraphQLRule(typeof(double), Format.FormatNumber, Execute.GetFormatFloatAction);

         var TestString = new GraphQLRule(typeof(string), Format.FormatString, Execute.GetFormatStringAction);
         var TestChar = new GraphQLRule(typeof(char), Format.FormatString, Execute.GetFormatStringAction);

         var TestBool = new GraphQLRule(typeof(bool), Format.FormatDefault, Execute.GetFormatBooleanAction);

         var TestID = new GraphQLRule(typeof(ID), Format.FormatID, Execute.GetFormatIDAction);

         var TestByte = new GraphQLRule(typeof(byte), Format.FormatNumber, Execute.GetFormatByteAction);
         var TestSByte = new GraphQLRule(typeof(sbyte), Format.FormatNumber, Execute.GetFormatByteAction);

         var TestUShort = new GraphQLRule(typeof(ushort), Format.FormatNumber, Execute.GetFormatShortAction);
         var TestShort = new GraphQLRule(typeof(short), Format.FormatNumber, Execute.GetFormatShortAction);

         var TestULong = new GraphQLRule(typeof(ulong), Format.FormatNumber, Execute.GetFormatLongAction);
         var TestLong = new GraphQLRule(typeof(long), Format.FormatNumber, Execute.GetFormatLongAction);

         var TestDecimal = new GraphQLRule(typeof(decimal), Format.FormatNumber, Execute.GetFormatDecimalAction);

         var TestUri = new GraphQLRule(typeof(Uri), Format.FormatUrl, Execute.GetFormatUrlAction);

         var TestDateTime = new GraphQLRule(typeof(DateTime), Format.FormatDateTime, Execute.GetFormatDateTimeAction);

         var TestGuid = new GraphQLRule(typeof(Guid), Format.FormatGuid, Execute.GetFormatGuidAction);
         var TestTimeSpan = new GraphQLRule(typeof(TimeSpan), Format.FormatTime, Execute.GetFormatTimeSpanAction);

         var TestObject = new GraphQLRule(typeof(object), Format.FormatClass, Execute.GetFormatClassAction);
         var TestAny = new GraphQLRule(typeof(Any), Format.FormatAny, Execute.GetFormatAnyAction);

         Assert.AreEqual("Int", TestInt.Convert());
         Assert.AreEqual("Int", TestUInt.Convert());
         Assert.AreEqual("Float", TestDouble.Convert());
         Assert.AreEqual("Float", TestFloat.Convert());
         Assert.AreEqual("String", TestString.Convert());
         Assert.AreEqual("String", TestChar.Convert());
         Assert.AreEqual("ID", TestID.Convert());
         Assert.AreEqual("Byte", TestSByte.Convert());
         Assert.AreEqual("Byte", TestByte.Convert());
         Assert.AreEqual("Short", TestUShort.Convert());
         Assert.AreEqual("Short", TestShort.Convert());
         Assert.AreEqual("Long", TestULong.Convert());
         Assert.AreEqual("Long", TestLong.Convert());
         Assert.AreEqual("Decimal", TestDecimal.Convert());
         Assert.AreEqual("Url", TestUri.Convert());
         Assert.AreEqual("DateTime", TestDateTime.Convert());
         Assert.AreEqual("Uuid", TestGuid.Convert());
         Assert.AreEqual("TimeSpan", TestTimeSpan.Convert());
         Assert.AreEqual(null, TestObject.Convert());
         Assert.AreEqual("Any", TestAny.Convert());
      }
   }
}
