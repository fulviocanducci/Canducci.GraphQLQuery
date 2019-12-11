using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.MSTest.Queries.Datas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Canducci.GraphQLQuery.MSTest
{
   [TestClass()]
   public class UnitTestVariableType
   {
      public IVariableType VariableTypeUri = new VariableType(typeof(Uri));
      public IVariableType VariableTypeInt = new VariableType(typeof(int));
      public IVariableType VariableTypeIntArray = new VariableType(typeof(int[]));
      public IVariableType VariableTypeUInt = new VariableType(typeof(uint));
      public IVariableType VariableTypeULong = new VariableType(typeof(ulong));
      public IVariableType VariableTypeLong = new VariableType(typeof(long));
      public IVariableType VariableTypeFloat = new VariableType(typeof(float));

      public IVariableType VariableTypeString = new VariableType(typeof(string));
      public IVariableType VariableTypeArrayString = new VariableType(typeof(string[]));
      public IVariableType VariableTypeIEnumerableString = new VariableType(typeof(IEnumerable<string>));

      public IVariableType VariableTypeDateTime = new VariableType(typeof(DateTime));
      public IVariableType VariableTypeTimeSpan = new VariableType(typeof(TimeSpan));
      public IVariableType VariableTypeByte = new VariableType(typeof(byte));
      public IVariableType VariableTypeDecimal = new VariableType(typeof(decimal));
      public IVariableType VariableTypeDouble = new VariableType(typeof(double));
      public IVariableType VariableTypeBool = new VariableType(typeof(bool));
      public IVariableType VariableTypeIEnumerable = new VariableType(typeof(IEnumerable));
      public IVariableType VariableTypeIEnumerableGenerics = new VariableType(typeof(IEnumerable<>));
      public IVariableType VariableTypeIEnumerableIntGenerics = new VariableType(typeof(IEnumerable<int>));
      public IVariableType VariableTypeIEnumerableClassGenerics = new VariableType(typeof(IEnumerable<Source>));

      [TestMethod()]
      [ExpectedException(typeof(ArgumentNullException))]
      public void TestVariableTypeException()
      {
         IVariableType VariableTypeException = new VariableType(null);
      }

      [TestMethod()]
      public void TestVariableType()
      {
         Assert.IsInstanceOfType(VariableTypeUri, typeof(IVariableType));
         
         Assert.IsInstanceOfType(VariableTypeInt.Type, typeof(int).GetType());
         Assert.IsNull(VariableTypeInt.TypeInternal);
         Assert.IsFalse(VariableTypeInt.IsArray);
         Assert.IsFalse(VariableTypeInt.IsIEnumerable);
         Assert.IsNull(VariableTypeInt.Convert(null));
         Assert.AreEqual(null, VariableTypeInt.Convert(string.Empty));

         Assert.IsInstanceOfType(VariableTypeIEnumerableClassGenerics.Type, typeof(IEnumerable<Source>).GetType());
         Assert.IsNotNull(VariableTypeIEnumerableClassGenerics.TypeInternal);
         Assert.IsFalse(VariableTypeIEnumerableClassGenerics.IsArray);
         Assert.IsTrue(VariableTypeIEnumerableClassGenerics.IsIEnumerable);         
         Assert.AreEqual("[Source]", VariableTypeIEnumerableClassGenerics.Convert("[{0}]"));

         Assert.IsInstanceOfType(VariableTypeUri.Type, typeof(Uri).GetType());
         Assert.IsInstanceOfType(VariableTypeUInt.Type, typeof(uint).GetType());
         Assert.IsInstanceOfType(VariableTypeULong.Type, typeof(ulong).GetType());
         Assert.IsInstanceOfType(VariableTypeLong.Type, typeof(long).GetType());
         Assert.IsInstanceOfType(VariableTypeFloat.Type, typeof(float).GetType());
         Assert.IsInstanceOfType(VariableTypeString.Type, typeof(string).GetType());
         Assert.IsInstanceOfType(VariableTypeDateTime.Type, typeof(DateTime).GetType());
         Assert.IsInstanceOfType(VariableTypeTimeSpan.Type, typeof(TimeSpan).GetType());
         Assert.IsInstanceOfType(VariableTypeByte.Type, typeof(byte).GetType());
         Assert.IsInstanceOfType(VariableTypeDecimal.Type, typeof(decimal).GetType());
         Assert.IsInstanceOfType(VariableTypeDouble.Type, typeof(double).GetType());
         Assert.IsInstanceOfType(VariableTypeBool.Type, typeof(bool).GetType());
         Assert.IsInstanceOfType(VariableTypeIEnumerable.Type, typeof(IEnumerable).GetType());
         Assert.IsInstanceOfType(VariableTypeIEnumerableGenerics.Type, typeof(IEnumerable<>).GetType());
         Assert.IsInstanceOfType(VariableTypeIEnumerableIntGenerics.Type, typeof(IEnumerable<int>).GetType());

         Assert.IsInstanceOfType(VariableTypeIntArray.Type, typeof(int[]).GetType());
         Assert.IsTrue(VariableTypeIntArray.IsArray);
         Assert.IsFalse(VariableTypeIntArray.IsIEnumerable);
         Assert.IsInstanceOfType(VariableTypeIntArray.TypeInternal, typeof(int).GetType());
         Assert.AreEqual("[Int]", VariableTypeIntArray.Convert("[{0}]"));

         Assert.IsInstanceOfType(VariableTypeIEnumerableIntGenerics.Type, typeof(IEnumerable<int>).GetType());
         Assert.IsFalse(VariableTypeIEnumerableIntGenerics.IsArray);
         Assert.IsTrue(VariableTypeIEnumerableIntGenerics.IsIEnumerable);
         Assert.IsInstanceOfType(VariableTypeIEnumerableIntGenerics.TypeInternal, typeof(int).GetType());
         Assert.AreEqual("[Int]", VariableTypeIEnumerableIntGenerics.Convert("[{0}]"));

         Assert.IsInstanceOfType(VariableTypeArrayString.Type, typeof(string[]).GetType());
         Assert.IsTrue(VariableTypeArrayString.IsArray);
         Assert.IsFalse(VariableTypeArrayString.IsIEnumerable);
         Assert.IsInstanceOfType(VariableTypeArrayString.TypeInternal, typeof(string).GetType());
         Assert.AreEqual("[String]", VariableTypeArrayString.Convert("[{0}]"));

         Assert.IsInstanceOfType(VariableTypeIEnumerableString.Type, typeof(IEnumerable<string>).GetType());
         Assert.IsFalse(VariableTypeIEnumerableString.IsArray);
         Assert.IsTrue(VariableTypeIEnumerableString.IsIEnumerable);
         Assert.IsInstanceOfType(VariableTypeIEnumerableString.TypeInternal, typeof(string).GetType());
         Assert.AreEqual("[String]", VariableTypeIEnumerableString.Convert("[{0}]"));
      }
   }
}
