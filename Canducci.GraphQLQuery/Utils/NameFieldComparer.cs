using Canducci.GraphQLQuery.Interfaces;
using System.Collections.Generic;

namespace Canducci.GraphQLQuery.Utils
{
   internal class NameFieldComparer : IEqualityComparer<IField>
   {
      public bool Equals(IField x, IField y)
      {
         if (x?.Name == null || y?.Name == null)
         {
            return true;
         }
         return x?.Name == y?.Name;
      }

      public int GetHashCode(IField obj)
      {
         return obj.Name == null ? obj.QueryType.GetHashCode() : obj.Name.GetHashCode();
      }
      internal static NameFieldComparer Create()
         => new NameFieldComparer();
   }
}
