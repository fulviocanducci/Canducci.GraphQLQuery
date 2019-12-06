using System;
namespace Canducci.GraphQLQuery.Interfaces
{
   internal interface IRule: IRuleBase
   {      
      Func<object, string> Convert { get; set; }
   }
}
