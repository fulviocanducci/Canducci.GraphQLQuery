using Canducci.GraphQLQuery.Interfaces;
using System;

namespace Canducci.GraphQLQuery.Internals
{
   internal class VariableValue : IVariableValue
   {
      public string Name { get; }
      public object Value { get; }
      public Type Type { get; }
      public VariableValue(string name, object value, Type type)
      {
         Name = name ?? throw new ArgumentNullException(nameof(name));
         Type = type ?? throw new ArgumentNullException(nameof(type));
         Value = value;
      }
   }
}
