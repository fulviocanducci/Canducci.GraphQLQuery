using System;
namespace Canducci.GraphQLQuery.Interfaces
{  
  public interface IArgument
  {
    string Name { get; }
    object Value { get; }
    Type TypeValue { get; }
    ArgumentFormat ArgumentFormat { get; }
  }
}
