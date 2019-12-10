using System;

namespace Canducci.GraphQLQuery.Interfaces
{
   internal interface IVariableValue
   {
      string Name { get; }
      object Value { get; }
      Type Type { get; }
   }
}
