﻿using Canducci.GraphQLQuery.VariablesValueTypes;

namespace Canducci.GraphQLQuery.Interfaces
{
   public interface IVariable
   {
      string Name { get; }
      string NameType { get; }
      object Value { get; }
      VariableValueDefault VariableValueDefault { get; }
      bool Required { get; }
      IGraphQLRule GraphQLRule { get; }
      string Convert();
      string GetKeyParam();
      string GetKeyArgument();
   }
}
