using HotChocolate.Language;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Canducci.GraphQLQuery.CustomTypes.MSTest
{
   [TestClass]
   public class UnitTestTimeSpanTypeDefault
   {
      public TimeSpanType TimeSpanType { get; private set; }

      [TestInitialize()]
      public void InitTestDefault()
      {
         TimeSpanType = new TimeSpanType();
      }

      [TestMethod]
      public void TestTimeSpanType()
      {
         Assert.AreEqual(TimeSpanType.Name.Value, "TimeSpan");
         Assert.AreEqual(TimeSpanType.ClrType, typeof(System.TimeSpan));
         Assert.AreEqual(TimeSpanType.Description, "TimeSpan Type HotChocolate");
         Assert.AreEqual(TimeSpanType.Kind, HotChocolate.Types.TypeKind.Scalar);
      }

      [TestMethod]
      public void TestTimeSpanTypeIsInstanceOfType()
      {
         Assert.IsTrue(TimeSpanType.IsInstanceOfType(new StringValueNode("")));
         Assert.IsTrue(TimeSpanType.IsInstanceOfType(new TimeSpanValueNode(NodeKind.ScalarTypeDefinition, TimeSpan.MinValue, null)));
         Assert.IsTrue(TimeSpanType.IsInstanceOfType(new NullValueNode(null)));
         Assert.IsTrue(TimeSpanType.IsInstanceOfType(null));
      }
      [TestMethod]
      public void TestTimeSpanTypeParseLiteral()
      {
         Assert.ThrowsException<ArgumentNullException>(() => TimeSpanType.ParseLiteral(null));
         var min = TimeSpan.Parse("01:01:01");
         Assert.AreEqual(min, TimeSpanType.ParseLiteral(new StringValueNode(min.ToString(@"hh\:mm\:ss"))));
         Assert.AreEqual(min, TimeSpanType.ParseLiteral(new TimeSpanValueNode(NodeKind.ScalarTypeDefinition, min, null)));
         Assert.AreEqual(null, TimeSpanType.ParseLiteral(new NullValueNode(null)));
      }

      [TestMethod]
      public void TestTimeSpanTypeParseValue()
      {
         var nullValueNode = new NullValueNode(null);
         var timeSpanValueNode = new TimeSpanValueNode(NodeKind.ScalarTypeDefinition, TimeSpan.Parse("01:01:01"), null);
         Assert.IsInstanceOfType(nullValueNode, TimeSpanType.ParseValue("").GetType());
         Assert.IsInstanceOfType(timeSpanValueNode, TimeSpanType.ParseValue("01:01:01").GetType());
         Assert.IsInstanceOfType(timeSpanValueNode, TimeSpanType.ParseValue("01:01:01").GetType());
         Assert.ThrowsException<ArgumentException>(() => TimeSpanType.ParseValue("undefined"));
      }

      [TestMethod]
      public void TestTimeSpanSerialize()
      {
         Assert.IsNull(TimeSpanType.Serialize(null));
         Assert.IsInstanceOfType(TimeSpanType.Serialize(TimeSpan.Parse("01:01:01")), typeof(TimeSpan));
         Assert.IsNull(TimeSpanType.Serialize(""));
      }

      [TestMethod]
      public void TestTimeSpanTrySerialize()
      {
         bool return0 = TimeSpanType.TryDeserialize(null, out object value);
         Assert.IsTrue(return0);
         Assert.IsNull(value);
         bool return1 = TimeSpanType.TryDeserialize(TimeSpan.Parse("01:01:01"), out value);
         Assert.IsTrue(return1);
         Assert.IsInstanceOfType(value, typeof(TimeSpan));
         Assert.AreEqual("01:01:01", ((TimeSpan)value).ToString(@"hh\:mm\:ss"));
      }
   }
}
