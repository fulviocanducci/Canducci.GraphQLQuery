using Canducci.GraphQLQuery.Internals;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace Canducci.GraphQLQuery.MSTest
{
   [TestClass]
   public class UnitTestRules
   {
      internal Rules Rules;
      internal RulesExecute RulesExecute;
      internal Guid Identity;

      [TestInitialize()]
      public void InitTestRules()
      {
         Rules = Rules.Instance;
         RulesExecute = new RulesExecute();
         Identity = Rules.Identity;
      }

      [TestMethod]
      public void TestRulesInstances()
      {
         Assert.IsInstanceOfType(Rules, typeof(Rules));
         Assert.IsInstanceOfType(Rules.Instance, typeof(Rules));
         Assert.IsInstanceOfType(Rules.Execute, typeof(RulesExecute));
         Assert.IsInstanceOfType(RulesExecute, typeof(RulesExecute));
         Assert.AreEqual(Identity, Rules.Identity);
      }

      [TestMethod]
      public void TestRules()
      {
         string GetValue(object value)
         {
            return string.Format(CultureInfo.InvariantCulture,
               "{0}{1}{2}{3}{4}",
               Signals.Backslashes,
               Signals.QuotationMark, value,
               Signals.Backslashes,
               Signals.QuotationMark);
         }
         var TestInt = Rules.Rule(typeof(int));
         var TestUInt = Rules.Rule(typeof(uint));
         var TestFloat = Rules.Rule(typeof(float));
         var TestDouble = Rules.Rule(typeof(double));
         var TestString = Rules.Rule(typeof(string));
         var TestChar = Rules.Rule(typeof(char));
         var TestBool = Rules.Rule(typeof(bool));
         var TestID = Rules.Rule(typeof(ID));
         var TestByte = Rules.Rule(typeof(byte));
         var TestSByte = Rules.Rule(typeof(sbyte));
         var TestUShort = Rules.Rule(typeof(ushort));
         var TestShort = Rules.Rule(typeof(short));
         var TestULong = Rules.Rule(typeof(ulong));
         var TestLong = Rules.Rule(typeof(long));
         var TestDecimal = Rules.Rule(typeof(decimal));
         var TestUri = Rules.Rule(typeof(Uri));
         var TestDateTime = Rules.Rule(typeof(DateTime));
         var TestGuid = Rules.Rule(typeof(Guid));
         var TestTimeSpan = Rules.Rule(typeof(TimeSpan));
         var TestObject = Rules.Rule(typeof(object));
         var TestAny = Rules.Rule(typeof(Any));

         Assert.AreEqual("1", TestInt.Convert(1));
         Assert.AreEqual("1", TestUInt.Convert(1));
         Assert.AreEqual("1", TestDouble.Convert(1F));
         Assert.AreEqual("1", TestFloat.Convert(1F));
         Assert.AreEqual(GetValue("A"), TestString.Convert("A"));
         Assert.AreEqual(GetValue("B"), TestChar.Convert("B"));
         Assert.AreEqual("$id", TestID.Convert(new ID("id","1")));
         Assert.AreEqual("1", TestSByte.Convert((byte)1));
         Assert.AreEqual("0", TestByte.Convert((byte)0));
         Assert.AreEqual("0", TestUShort.Convert((short)0));
         Assert.AreEqual("0", TestShort.Convert((short)0));
         Assert.AreEqual("1", TestULong.Convert(1L));
         Assert.AreEqual("1", TestLong.Convert(1L));
         Assert.AreEqual("1", TestDecimal.Convert(1M));
         Assert.AreEqual("$http://localhost/", TestUri.Convert(new Uri("http://localhost")));
         Assert.AreEqual(GetValue(DateTime.MinValue.ToString(Formats.DateTime)), TestDateTime.Convert(DateTime.MinValue));
         Assert.AreEqual(GetValue(Guid.Empty), TestGuid.Convert(Guid.Empty));
         Assert.AreEqual(GetValue(TimeSpan.MinValue.ToString(Formats.Timespan)), TestTimeSpan.Convert(TimeSpan.MinValue));
         Assert.AreEqual("id:1", TestObject.Convert(new { id = 1 }));
         Assert.AreEqual("$any", TestAny.Convert(new Any("any", "[10,20]")));
         Assert.AreEqual("true", TestBool.Convert(true));

         Assert.AreEqual("null", RulesExecute.GetFormatNullAction(null));
         Assert.AreEqual("$source", RulesExecute.GetFormatParameterAction(new Parameter("source")));
         Assert.AreEqual("1", RulesExecute.GetFormatNumberAction(1));         
         Assert.AreEqual("1", RulesExecute.GetFormatNumberAction(1F));         
         Assert.AreEqual(GetValue("A"), RulesExecute.GetFormatStringAction("A"));
         Assert.AreEqual(GetValue('a'), RulesExecute.GetFormatCharAction('a'));
         Assert.AreEqual(GetValue("B"), RulesExecute.GetFormatStringAction("B"));
         Assert.AreEqual("$id", RulesExecute.GetFormatIDAction(new ID("id", "1")));
         Assert.AreEqual("1", RulesExecute.GetFormatNumberAction((byte)1));
         Assert.AreEqual("0", RulesExecute.GetFormatNumberAction((byte)0));
         Assert.AreEqual("0", RulesExecute.GetFormatNumberAction((short)0));
         Assert.AreEqual("0", RulesExecute.GetFormatNumberAction((short)0));
         Assert.AreEqual("1", RulesExecute.GetFormatNumberAction(1L));
         Assert.AreEqual("1", RulesExecute.GetFormatNumberAction(1L));
         Assert.AreEqual("1", RulesExecute.GetFormatNumberAction(1M));
         Assert.AreEqual("$http://localhost/", RulesExecute.GetFormatUrlAction(new Uri("http://localhost")));
         Assert.AreEqual(GetValue(DateTime.MinValue.ToString(Formats.DateTime)), RulesExecute.GetFormatDateTimeAction(DateTime.MinValue));
         Assert.AreEqual(GetValue(Guid.Empty), RulesExecute.GetFormatGuidAction(Guid.Empty));
         Assert.AreEqual(GetValue(TimeSpan.MinValue.ToString(Formats.Timespan)), RulesExecute.GetFormatTimeSpanAction(TimeSpan.MinValue));
         Assert.AreEqual("id:1", RulesExecute.GetFormatClassAction(new { id = 1 }));
         Assert.AreEqual("$any", RulesExecute.GetFormatAnyAction(new Any("any", "[10,20]")));
         Assert.AreEqual("true", RulesExecute.GetFormatBoolAction(true));

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

         Assert.AreEqual(Identity, Rules.Identity);

      }
   }
}
