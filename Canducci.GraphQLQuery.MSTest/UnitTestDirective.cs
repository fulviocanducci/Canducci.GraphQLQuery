using Canducci.GraphQLQuery.Abstracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canducci.GraphQLQuery.MSTest
{
   [TestClass]
   public class UnitTestDirective
   {
      [TestMethod]
      public void TestDirectives()
      {         
         Skip skip = new Skip("state");
         Include include = new Include("active");
         
         Assert.AreEqual("@skip(if:$state)", skip.Convert());
         Assert.AreEqual("@include(if:$active)", include.Convert());
      }

      [TestMethod]
      public void TestDirectiveInstanceOf()
      {         
         Skip skip0 = new Skip("state");
         Include include0 = new Include("active");
       
         Skip skip1 = Directive.Skip("state");
         Include include1 = Directive.Include("active");

         Skip skip2 = Skip.Create("state");
         Include include2 = Include.Create("active");

         Assert.IsInstanceOfType(skip0, typeof(Skip));
         Assert.IsInstanceOfType(skip1, typeof(Skip));
         Assert.IsInstanceOfType(skip2, typeof(Skip));

         Assert.IsInstanceOfType(include0, typeof(Include));
         Assert.IsInstanceOfType(include1, typeof(Include));
         Assert.IsInstanceOfType(include2, typeof(Include));

      }

      [TestMethod]
      public void TestDirectivesLayout()
      {         
         Skip skip = new Skip("state");
         Include include = new Include("active");

         Assert.AreEqual("@skip(if:${0})", skip.Layout);
         Assert.AreEqual("@include(if:${0})", include.Layout);
      }

      [TestMethod]
      public void TestDirectivesName()
      {         
         Skip skip = new Skip("state");
         Include include = new Include("active");

         Assert.AreEqual("state", skip.Name);
         Assert.AreEqual("active", include.Name);
      }

      [TestMethod]
      public void TestDirectiveWithFabricDirective()
      {         
         Skip skip = Directive.Skip("state");
         Include include = Directive.Include("active");

         Assert.AreEqual("@skip(if:$state)", skip.Convert());
         Assert.AreEqual("@include(if:$active)", include.Convert());
      }
   }
}

/*
*
GraphQL provides several default directives: @deprecated, @skip, and @include.
--------------------------------------------------------------------------------------------
@deprecated(reason: String) - marks field as deprecated with message // only configuration type no implementation
@skip(if: Boolean!) - GraphQL execution skips the field if true by not calling the resolver
@include(if: Boolean!) - Calls resolver for annotated field if true
*
*/