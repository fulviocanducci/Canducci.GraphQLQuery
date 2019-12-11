using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Canducci.GraphQLQuery.Extensions
{
   internal static class IVariableExtensions
   {
      internal static IEnumerable<IVariable> DistinctName(this IEnumerable<IVariable> variables)
      {
         return variables.Distinct(NameVariableComparer.Create());
      }
   }
}
