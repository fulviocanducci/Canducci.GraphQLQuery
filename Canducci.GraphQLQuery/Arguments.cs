using Canducci.GraphQLQuery.Interfaces;
using System.Collections.Generic;

namespace Canducci.GraphQLQuery
{
  public class Arguments : List<IArgument>
  {
    public Arguments(params IArgument[] arguments)
    {
      AddRange(arguments);
    }      
  }
}
