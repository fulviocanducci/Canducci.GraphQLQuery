using Canducci.GraphQLQuery.Interfaces;
using System.Collections.Generic;

namespace Canducci.GraphQLQuery.Utils
{
   internal class NameArgumentComparer : IEqualityComparer<IArgument>
   {
      public bool Equals(IArgument x, IArgument y)
      {
         return x.Name == y.Name;
      }

      public int GetHashCode(IArgument obj)
      {
         return obj.Name.GetHashCode();
      }

      internal static NameArgumentComparer Create()
         => new NameArgumentComparer();
   }
}
