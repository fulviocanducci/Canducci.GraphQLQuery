using System;
namespace Canducci.GraphQLQuery.Interfaces
{
   internal interface IGraphQLRule: IRuleBase
   {      
      Func<string> Convert { get; set; }
   }
}
