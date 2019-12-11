using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Canducci.GraphQLQuery.Extensions
{
   internal static class IFieldExtensions
   {
      internal static IEnumerable<IField> DistinctName(this IEnumerable<IField> fields)
      {
         return fields.Distinct(NameFieldComparer.Create());
      }
   }
}
