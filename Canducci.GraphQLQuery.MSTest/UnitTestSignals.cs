using Canducci.GraphQLQuery.Internals;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Canducci.GraphQLQuery.MSTest
{
   [TestClass]
   public class UnitTestSignals
   {
      [TestMethod]
      public void TestSignals()
      {
         Assert.AreEqual(Signals.QuotationMark, "\"");
         Assert.AreEqual(Signals.Backslashes, "\\");
         Assert.AreEqual(Signals.ParenthesisOpen, "(");
         Assert.AreEqual(Signals.ParenthesisClose, ")");
         Assert.AreEqual(Signals.BraceOpen, "{");
         Assert.AreEqual(Signals.BraceClose, "}");
         Assert.AreEqual(Signals.Colon, ":");
         Assert.AreEqual(Signals.Semicolon, ";");
         Assert.AreEqual(Signals.Comma, ",");
         Assert.AreEqual(Signals.Query, "query");
         Assert.AreEqual(Signals.Tab, "\t");
         Assert.AreEqual(Signals.DollarSign, "$");
         Assert.AreEqual(Signals.ExclamationPoint, "!");
         Assert.AreEqual(Signals.EqualSign, "=");
         Assert.AreEqual(Signals.Variables, "variables");
         Assert.AreEqual(GetConstants(typeof(Signals)).Count, 15);
      }

      private List<FieldInfo> GetConstants(Type type)
      {
         FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
         return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();
      }
   }
}