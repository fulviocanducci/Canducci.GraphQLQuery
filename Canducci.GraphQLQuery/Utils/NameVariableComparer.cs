using Canducci.GraphQLQuery.Interfaces;
using System.Collections.Generic;

namespace Canducci.GraphQLQuery.Utils
{
   internal class NameVariableComparer : IEqualityComparer<IVariable>
   {
      public bool Equals(IVariable x, IVariable y)
      {
         return x.Name == y.Name;
      }

      public int GetHashCode(IVariable obj)
      {
         return obj.Name.GetHashCode();
      }

      internal static NameVariableComparer Create()
         => new NameVariableComparer();
   }
}
