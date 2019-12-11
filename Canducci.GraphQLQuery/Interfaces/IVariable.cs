using Canducci.GraphQLQuery.VariablesValueTypes;
using System;

namespace Canducci.GraphQLQuery.Interfaces
{
   public interface IVariable
   {
      IVariableType VariableType { get; }
      string Name { get; }
      string NameType { get; }
      object Value { get; }
      VariableValueDefault VariableValueDefault { get; }
      bool Required { get; }
      string Convert();
      string GetKeyParam();
      string GetKeyArgument();
      object GetValue();
   }
   public interface IVariable<T> : IVariable
   {      
      new T Value { get; }      
   }
}
