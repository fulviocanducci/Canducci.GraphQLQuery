using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.Internals;
using Canducci.GraphQLQuery.VariablesValueTypes;
using System;
using System.Globalization;
namespace Canducci.GraphQLQuery
{
   public class Variable<T> : IVariable<T>, IVariable
   {
      public Type VariableType { get; }
      public string Name { get; }
      public T Value { get; }      
      public string NameType { get; }
      public VariableValueDefault VariableValueDefault { get; }
      public bool Required { get; }
      internal IGraphQLRule GraphQLRule { get; }
      object IVariable.Value
      {
         get
         {
            return Value;
         }
      }
      public Variable(string name, T value, string nameType, bool required = false, VariableValueDefault variableValueDefault = null)
         :this(name, value, required, variableValueDefault)
      {         
         NameType = nameType;
      }
      public Variable(string name, T value, bool required = false, VariableValueDefault variableValueDefault = null)
      {
         Type type = typeof(T);
         Name = name ?? throw new ArgumentNullException(nameof(name));
         Value = value;
         VariableType = GetTypeVariableTypeOrObject(type);
         GraphQLRule = GraphQLRules.Instance.Rule(type);         
         Required = required;
         VariableValueDefault = variableValueDefault;                  
         NameType = null;
      }
      internal Type GetTypeVariableTypeOrObject(Type type)
      {
         return type == typeof(ID) || type == typeof(Any) 
            ? (new object()).GetType()
            : type;
      }
      public string Convert()
      {         
         return GraphQLRule.Convert() ?? NameType;
      }
      public string GetKeyParam()
      {
         return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}{4}{5}",
            Signals.DollarSign,
            Name,
            Signals.Colon,
            Convert(),
            Required ? Signals.ExclamationPoint : "",
            VariableValueDefault != null ? $"{Signals.EqualSign}{VariableValueDefault.Value}" : ""
         );
      }
      public string GetKeyArgument()
      {
         return string.Format(CultureInfo.InvariantCulture, "{0}:{1}{2}", Name, Signals.DollarSign, Name);
      }
      public object GetValue()
      {
         object value = Value;
         if (Format.FormatID == GraphQLRule.Format)
         {
            if (value is ID id)
            {
               value = id.Value;
            }
         } 
         else if (Format.FormatAny == GraphQLRule.Format)
         {
            if (value is Any any)
            {
               value = any.Value;
            }
         }
         return value;
      }      
   }
}

/*
 *
- Int: A signed 32‐bit integer.
- Float: A signed double-precision floating-point value.
- String: A UTF‐8 character sequence.
- Boolean: true or false.
- ID: The ID scalar type represents a unique identifier, 
often used to refetch an object or as the key for a cache. 
The ID type is serialized in the same way as a String; however, 
defining it as an ID signifies that it is not intended to be human‐readable.
*/
