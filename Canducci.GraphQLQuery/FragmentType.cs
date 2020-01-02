using Canducci.GraphQLQuery.Interfaces;
namespace Canducci.GraphQLQuery
{
   public sealed class FragmentType : IFragmentType
   {
      public string Name { get; }
      public string NameType { get; }

      public FragmentType(string name, string nameType)
      {
         Name = name ?? throw new System.ArgumentNullException(nameof(name));
         NameType = nameType ?? throw new System.ArgumentNullException(nameof(nameType));
      }

      public string FragmentName
      {
         get
         {
            return $"...{Name}";
         }
      }

      public string FragmentNameAndType
      {
         get
         {
            return $"fragment {Name} on {NameType}";
         }
      }
   }
}
