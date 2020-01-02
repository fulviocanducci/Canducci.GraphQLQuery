using Canducci.GraphQLQuery.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canducci.GraphQLQuery.MSTest
{

   [TestClass]
   public class UnitFragmentType
   {
      [TestMethod]
      public void TestFragmentType()
      {
         IFragmentType fragmentType = new FragmentType("fields", "state_type");
         Assert.IsInstanceOfType(fragmentType.GetType(), typeof(IFragmentType).GetType());
         Assert.IsInstanceOfType(fragmentType.GetType(), typeof(FragmentType).GetType());
         Assert.AreEqual(fragmentType.FragmentName, "...fields");
         Assert.AreEqual(fragmentType.FragmentNameAndType, "fragment fields on state_type");
         Assert.AreEqual(fragmentType.Name, "fields");
         Assert.AreEqual(fragmentType.NameType, "state_type");
      }
   }
}
