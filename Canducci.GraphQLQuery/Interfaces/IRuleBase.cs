using System;
namespace Canducci.GraphQLQuery.Interfaces
{
   internal interface IRuleBase
   {
      Type TypeArgument { get; }
      Format Format { get; }
   }
}
