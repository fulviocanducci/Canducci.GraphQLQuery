using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Canducci.GraphQLQuery.Extensions
{
   internal static class IArgumentExtensions
   {
      internal static IEnumerable<IArgument> DistinctName(this IEnumerable<IArgument> arguments)
      {
         return arguments.Distinct(NameArgumentComparer.Create());
      }
   }
}
