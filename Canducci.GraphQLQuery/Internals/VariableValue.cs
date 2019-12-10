using Canducci.GraphQLQuery.Interfaces;
using System;

namespace Canducci.GraphQLQuery.Internals
{
   internal class VariableValue : IVariableValue
   {
      public VariableValue(object value, Type type)
      {
         Value = value;
         Type = type ?? throw new ArgumentNullException(nameof(type));
      }
      public object Value { get; }
      public Type Type { get; }
   }
}
