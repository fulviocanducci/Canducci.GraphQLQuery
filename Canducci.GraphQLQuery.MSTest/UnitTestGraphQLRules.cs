using Canducci.GraphQLQuery.Internals;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Canducci.GraphQLQuery.MSTest
{


   [TestClass]
   public class UnitTestGraphQLRules
   {
      internal GraphQLRules GraphQLRules;
      internal GraphQLRulesExecute GraphQLRulesExecute;
      internal Guid Identity;

      [TestInitialize()]
      public void InitTestRules()
      {
         GraphQLRules = GraphQLRules.Instance;
         GraphQLRulesExecute = new GraphQLRulesExecute();
         Identity = GraphQLRules.Identity;
      }

      [TestMethod]
      public void TestGraphQLRulesInstances()
      {
         Assert.IsInstanceOfType(GraphQLRules, typeof(GraphQLRules));
         Assert.IsInstanceOfType(GraphQLRules.Instance, typeof(GraphQLRules));
         Assert.IsInstanceOfType(GraphQLRules.Execute, typeof(GraphQLRulesExecute));         
         Assert.IsInstanceOfType(GraphQLRulesExecute, typeof(GraphQLRulesExecute));
         Assert.AreEqual(Identity, GraphQLRules.Identity);
      }

      [TestMethod]
      public void TestRules()
      {         
         var TestInt = GraphQLRules.Rule(typeof(int));
         var TestUInt = GraphQLRules.Rule(typeof(uint));
         var TestFloat = GraphQLRules.Rule(typeof(float));
         var TestDouble = GraphQLRules.Rule(typeof(double));
         var TestString = GraphQLRules.Rule(typeof(string));
         var TestChar = GraphQLRules.Rule(typeof(char));
         var TestBool = GraphQLRules.Rule(typeof(bool));
         var TestID = GraphQLRules.Rule(typeof(ID));
         var TestByte = GraphQLRules.Rule(typeof(byte));
         var TestSByte = GraphQLRules.Rule(typeof(sbyte));
         var TestUShort = GraphQLRules.Rule(typeof(ushort));
         var TestShort = GraphQLRules.Rule(typeof(short));
         var TestULong = GraphQLRules.Rule(typeof(ulong));
         var TestLong = GraphQLRules.Rule(typeof(long));
         var TestDecimal = GraphQLRules.Rule(typeof(decimal));
         var TestUri = GraphQLRules.Rule(typeof(Uri));
         var TestDateTime = GraphQLRules.Rule(typeof(DateTime));
         var TestGuid = GraphQLRules.Rule(typeof(Guid));
         var TestTimeSpan = GraphQLRules.Rule(typeof(TimeSpan));
         var TestObject = GraphQLRules.Rule(typeof(object));
         var TestAny = GraphQLRules.Rule(typeof(Any));

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
         Assert.AreEqual("Boolean", TestBool.Convert());

         Assert.AreEqual(Format.FormatNumber, TestInt.Format);
         Assert.AreEqual(Format.FormatNumber, TestUInt.Format);
         Assert.AreEqual(Format.FormatNumber, TestDouble.Format);
         Assert.AreEqual(Format.FormatNumber, TestFloat.Format);
         Assert.AreEqual(Format.FormatString, TestString.Format);
         Assert.AreEqual(Format.FormatString, TestChar.Format);
         Assert.AreEqual(Format.FormatID, TestID.Format);
         Assert.AreEqual(Format.FormatNumber, TestSByte.Format);
         Assert.AreEqual(Format.FormatNumber, TestByte.Format);
         Assert.AreEqual(Format.FormatNumber, TestUShort.Format);
         Assert.AreEqual(Format.FormatNumber, TestShort.Format);
         Assert.AreEqual(Format.FormatNumber, TestULong.Format);
         Assert.AreEqual(Format.FormatNumber, TestLong.Format);
         Assert.AreEqual(Format.FormatNumber, TestDecimal.Format);
         Assert.AreEqual(Format.FormatUrl, TestUri.Format);
         Assert.AreEqual(Format.FormatDateTime, TestDateTime.Format);
         Assert.AreEqual(Format.FormatGuid, TestGuid.Format);
         Assert.AreEqual(Format.FormatTime, TestTimeSpan.Format);
         Assert.AreEqual(Format.FormatClass, TestObject.Format);
         Assert.AreEqual(Format.FormatAny, TestAny.Format);
         Assert.AreEqual(Format.FormatBool, TestBool.Format);
                  
         Assert.IsInstanceOfType(TestInt.TypeArgument, typeof(int).GetType());
         Assert.IsInstanceOfType(TestUInt.TypeArgument, typeof(uint).GetType());
         Assert.IsInstanceOfType(TestDouble.TypeArgument, typeof(double).GetType());
         Assert.IsInstanceOfType(TestFloat.TypeArgument, typeof(float).GetType());
         Assert.IsInstanceOfType(TestString.TypeArgument, typeof(string).GetType());
         Assert.IsInstanceOfType(TestChar.TypeArgument, typeof(char).GetType());
         Assert.IsInstanceOfType(TestID.TypeArgument, typeof(ID).GetType());
         Assert.IsInstanceOfType(TestSByte.TypeArgument, typeof(sbyte).GetType());
         Assert.IsInstanceOfType(TestByte.TypeArgument, typeof(byte).GetType());
         Assert.IsInstanceOfType(TestUShort.TypeArgument, typeof(ushort).GetType());
         Assert.IsInstanceOfType(TestShort.TypeArgument, typeof(short).GetType());
         Assert.IsInstanceOfType(TestULong.TypeArgument, typeof(ulong).GetType());
         Assert.IsInstanceOfType(TestLong.TypeArgument, typeof(long).GetType());
         Assert.IsInstanceOfType(TestDecimal.TypeArgument, typeof(decimal).GetType());
         Assert.IsInstanceOfType(TestUri.TypeArgument, typeof(Uri).GetType());
         Assert.IsInstanceOfType(TestDateTime.TypeArgument, typeof(DateTime).GetType());
         Assert.IsInstanceOfType(TestGuid.TypeArgument, typeof(Guid).GetType());
         Assert.IsInstanceOfType(TestTimeSpan.TypeArgument, typeof(TimeSpan).GetType());
         Assert.IsInstanceOfType(TestObject.TypeArgument, typeof(object).GetType());
         Assert.IsInstanceOfType(TestAny.TypeArgument, typeof(Any).GetType());
         Assert.IsInstanceOfType(TestBool.TypeArgument, typeof(bool).GetType());

         Assert.AreEqual(Identity, GraphQLRules.Identity);

      }

      [TestMethod]
      public void TestRule()
      {
         var TestInt = new GraphQLRule(typeof(int), Format.FormatNumber, GraphQLRulesExecute.GetFormatIntAction);
         var TestUInt = new GraphQLRule(typeof(uint), Format.FormatNumber, GraphQLRulesExecute.GetFormatIntAction);
         var TestFloat = new GraphQLRule(typeof(float), Format.FormatNumber, GraphQLRulesExecute.GetFormatFloatAction);
         var TestDouble = new GraphQLRule(typeof(double), Format.FormatNumber, GraphQLRulesExecute.GetFormatFloatAction);
         var TestString = new GraphQLRule(typeof(string), Format.FormatString, GraphQLRulesExecute.GetFormatStringAction);
         var TestChar = new GraphQLRule(typeof(char), Format.FormatString, GraphQLRulesExecute.GetFormatStringAction);
         var TestBool = new GraphQLRule(typeof(bool), Format.FormatBool, GraphQLRulesExecute.GetFormatBooleanAction);
         var TestID = new GraphQLRule(typeof(ID), Format.FormatID, GraphQLRulesExecute.GetFormatIDAction);
         var TestByte = new GraphQLRule(typeof(byte), Format.FormatNumber, GraphQLRulesExecute.GetFormatByteAction);
         var TestSByte = new GraphQLRule(typeof(sbyte), Format.FormatNumber, GraphQLRulesExecute.GetFormatByteAction);
         var TestUShort = new GraphQLRule(typeof(ushort), Format.FormatNumber, GraphQLRulesExecute.GetFormatShortAction);
         var TestShort = new GraphQLRule(typeof(short), Format.FormatNumber, GraphQLRulesExecute.GetFormatShortAction);
         var TestULong = new GraphQLRule(typeof(ulong), Format.FormatNumber, GraphQLRulesExecute.GetFormatLongAction);
         var TestLong = new GraphQLRule(typeof(long), Format.FormatNumber, GraphQLRulesExecute.GetFormatLongAction);
         var TestDecimal = new GraphQLRule(typeof(decimal), Format.FormatNumber, GraphQLRulesExecute.GetFormatDecimalAction);
         var TestUri = new GraphQLRule(typeof(Uri), Format.FormatUrl, GraphQLRulesExecute.GetFormatUrlAction);
         var TestDateTime = new GraphQLRule(typeof(DateTime), Format.FormatDateTime, GraphQLRulesExecute.GetFormatDateTimeAction);
         var TestGuid = new GraphQLRule(typeof(Guid), Format.FormatGuid, GraphQLRulesExecute.GetFormatGuidAction);
         var TestTimeSpan = new GraphQLRule(typeof(TimeSpan), Format.FormatTime, GraphQLRulesExecute.GetFormatTimeSpanAction);
         var TestObject = new GraphQLRule(typeof(object), Format.FormatClass, GraphQLRulesExecute.GetFormatClassAction);
         var TestAny = new GraphQLRule(typeof(Any), Format.FormatAny, GraphQLRulesExecute.GetFormatAnyAction);

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

         Assert.AreEqual(Format.FormatNumber, TestInt.Format);
         Assert.AreEqual(Format.FormatNumber, TestUInt.Format);
         Assert.AreEqual(Format.FormatNumber, TestDouble.Format);
         Assert.AreEqual(Format.FormatNumber, TestFloat.Format);
         Assert.AreEqual(Format.FormatString, TestString.Format);
         Assert.AreEqual(Format.FormatString, TestChar.Format);
         Assert.AreEqual(Format.FormatID, TestID.Format);
         Assert.AreEqual(Format.FormatNumber, TestSByte.Format);
         Assert.AreEqual(Format.FormatNumber, TestByte.Format);
         Assert.AreEqual(Format.FormatNumber, TestUShort.Format);
         Assert.AreEqual(Format.FormatNumber, TestShort.Format);
         Assert.AreEqual(Format.FormatNumber, TestULong.Format);
         Assert.AreEqual(Format.FormatNumber, TestLong.Format);
         Assert.AreEqual(Format.FormatNumber, TestDecimal.Format);
         Assert.AreEqual(Format.FormatUrl, TestUri.Format);
         Assert.AreEqual(Format.FormatDateTime, TestDateTime.Format);
         Assert.AreEqual(Format.FormatGuid, TestGuid.Format);
         Assert.AreEqual(Format.FormatTime, TestTimeSpan.Format);
         Assert.AreEqual(Format.FormatClass, TestObject.Format);
         Assert.AreEqual(Format.FormatAny, TestAny.Format);
         Assert.AreEqual(Format.FormatBool, TestBool.Format);

         Assert.IsInstanceOfType(TestInt.TypeArgument, typeof(int).GetType());
         Assert.IsInstanceOfType(TestUInt.TypeArgument, typeof(uint).GetType());
         Assert.IsInstanceOfType(TestDouble.TypeArgument, typeof(double).GetType());
         Assert.IsInstanceOfType(TestFloat.TypeArgument, typeof(float).GetType());
         Assert.IsInstanceOfType(TestString.TypeArgument, typeof(string).GetType());
         Assert.IsInstanceOfType(TestChar.TypeArgument, typeof(char).GetType());
         Assert.IsInstanceOfType(TestID.TypeArgument, typeof(ID).GetType());
         Assert.IsInstanceOfType(TestSByte.TypeArgument, typeof(sbyte).GetType());
         Assert.IsInstanceOfType(TestByte.TypeArgument, typeof(byte).GetType());
         Assert.IsInstanceOfType(TestUShort.TypeArgument, typeof(ushort).GetType());
         Assert.IsInstanceOfType(TestShort.TypeArgument, typeof(short).GetType());
         Assert.IsInstanceOfType(TestULong.TypeArgument, typeof(ulong).GetType());
         Assert.IsInstanceOfType(TestLong.TypeArgument, typeof(long).GetType());
         Assert.IsInstanceOfType(TestDecimal.TypeArgument, typeof(decimal).GetType());
         Assert.IsInstanceOfType(TestUri.TypeArgument, typeof(Uri).GetType());
         Assert.IsInstanceOfType(TestDateTime.TypeArgument, typeof(DateTime).GetType());
         Assert.IsInstanceOfType(TestGuid.TypeArgument, typeof(Guid).GetType());
         Assert.IsInstanceOfType(TestTimeSpan.TypeArgument, typeof(TimeSpan).GetType());
         Assert.IsInstanceOfType(TestObject.TypeArgument, typeof(object).GetType());
         Assert.IsInstanceOfType(TestAny.TypeArgument, typeof(Any).GetType());
         Assert.IsInstanceOfType(TestBool.TypeArgument, typeof(bool).GetType());        
         Assert.IsInstanceOfType(TestBool.Convert, typeof(Func<string>));
         Assert.AreEqual(Identity, GraphQLRules.Identity);

      }

      [TestMethod]
      public void TestGraphQLRulesExecuteMethods()
      {
         Assert.AreEqual("Int", GraphQLRulesExecute.GetFormatIntAction());              
         Assert.AreEqual("Float", GraphQLRulesExecute.GetFormatFloatAction());
         Assert.AreEqual("String", GraphQLRulesExecute.GetFormatStringAction());         
         Assert.AreEqual("Boolean", GraphQLRulesExecute.GetFormatBooleanAction());
         Assert.AreEqual("ID", GraphQLRulesExecute.GetFormatIDAction());
         Assert.AreEqual("Byte", GraphQLRulesExecute.GetFormatByteAction());
         Assert.AreEqual("Short", GraphQLRulesExecute.GetFormatShortAction());
         Assert.AreEqual("Long", GraphQLRulesExecute.GetFormatLongAction());         
         Assert.AreEqual("Decimal", GraphQLRulesExecute.GetFormatDecimalAction());
         Assert.AreEqual("Url", GraphQLRulesExecute.GetFormatUrlAction());
         Assert.AreEqual("DateTime", GraphQLRulesExecute.GetFormatDateTimeAction());
         Assert.AreEqual("Date", GraphQLRulesExecute.GetFormatDateAction());
         Assert.AreEqual("Uuid", GraphQLRulesExecute.GetFormatGuidAction());
         Assert.AreEqual("TimeSpan", GraphQLRulesExecute.GetFormatTimeSpanAction());
         Assert.AreEqual(null, GraphQLRulesExecute.GetFormatClassAction());

         Assert.AreEqual("Int", GraphQLRules.Instance.Execute.GetFormatIntAction());
         Assert.AreEqual("Float", GraphQLRules.Instance.Execute.GetFormatFloatAction());
         Assert.AreEqual("String", GraphQLRules.Instance.Execute.GetFormatStringAction());
         Assert.AreEqual("Boolean", GraphQLRules.Instance.Execute.GetFormatBooleanAction());
         Assert.AreEqual("ID", GraphQLRules.Instance.Execute.GetFormatIDAction());
         Assert.AreEqual("Byte", GraphQLRules.Instance.Execute.GetFormatByteAction());
         Assert.AreEqual("Short", GraphQLRules.Instance.Execute.GetFormatShortAction());
         Assert.AreEqual("Long", GraphQLRules.Instance.Execute.GetFormatLongAction());
         Assert.AreEqual("Decimal", GraphQLRules.Instance.Execute.GetFormatDecimalAction());
         Assert.AreEqual("Url", GraphQLRules.Instance.Execute.GetFormatUrlAction());
         Assert.AreEqual("DateTime", GraphQLRules.Instance.Execute.GetFormatDateTimeAction());
         Assert.AreEqual("Date", GraphQLRules.Instance.Execute.GetFormatDateAction());
         Assert.AreEqual("Uuid", GraphQLRules.Instance.Execute.GetFormatGuidAction());
         Assert.AreEqual("TimeSpan", GraphQLRules.Instance.Execute.GetFormatTimeSpanAction());
         Assert.AreEqual(null, GraphQLRules.Instance.Execute.GetFormatClassAction());
      }
   }
}
