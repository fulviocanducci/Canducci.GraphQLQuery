using Canducci.GraphQLQuery.Internals;
using System;
namespace Canducci.GraphQLQuery.Interfaces
{
   public interface IGraphQLRule
   {
      Type TypeArgument { get; }
      Format Format { get; }
      Func<string> Convert { get; set; }
   }
}
