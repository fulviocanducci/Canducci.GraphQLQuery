using Canducci.GraphQLQuery.Internals;
using System;
namespace Canducci.GraphQLQuery.Interfaces
{
   public interface IRule
   {
      Type TypeArgument { get; }
      Format Format { get; }
      Func<object, string> Convert { get; set; }
   }
}
