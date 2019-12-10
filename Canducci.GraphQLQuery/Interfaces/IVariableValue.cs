using System;

namespace Canducci.GraphQLQuery.Interfaces
{
   internal interface IVariableValue
   {
      object Value { get; }
      Type Type { get; }
   }
}
