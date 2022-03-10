using HotChocolate.Language;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Canducci.GraphQLQuery.CustomTypes.MSTest
{
   [TestClass]
   public class UnitTestTimeSpanValueNodeDefault
   {
      public TimeSpanValueNode TimeSpanValueNode { get; private set; }

      [TestInitialize()]
      public void InitTestDefault()
      {
         TimeSpanValueNode = new TimeSpanValueNode(SyntaxKind.ScalarTypeExtension, TimeSpan.Parse("01:01:01"), null);
      }

      [TestMethod]
      public void TestTimeSpanValueNode()
      {
         Assert.AreEqual(TimeSpanValueNode.Kind, SyntaxKind.ScalarTypeExtension);
         Assert.AreEqual(TimeSpanValueNode.Location, null);
         Assert.AreEqual(TimeSpanValueNode.Value.TotalSeconds, TimeSpan.Parse("01:01:01").TotalSeconds);
      }
   }
}
