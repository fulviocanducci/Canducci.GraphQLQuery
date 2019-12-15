using Canducci.GraphQLQuery.Interfaces;
using System.Linq;

namespace Canducci.GraphQLQuery.Extensions
{
   internal static class IDirectivesExtensions
   {
      internal static string ConvertAll(this IDirective[] directives)
      {
         return $" {string.Join(" ", directives.Select(x => x.Convert()).ToArray())}";
      }
   }
}
