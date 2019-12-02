using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.Internals;
using System.Globalization;

namespace Canducci.GraphQLQuery
{
   public class Variable : IVariable
   {
      public Variable(string name, object value, bool required = false, object valueDefault = null)
      {
         Name = name;
         Value = value;
         Required = required;
         ValueDefault = valueDefault;
         Rule = GraphQLRules.Instance.Rule(value.GetType());
      }
      public string Name { get; }
      public object Value { get; }
      public object ValueDefault { get; }
      public bool Required { get; }
      public IRule Rule { get; }
      public string Convert()
      {
         return Rule.Convert(null);
      }
      public string KeyParam
      {
         get
         {              
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}{4}{5}",
               Signals.DollarSign,
               Name,
               Signals.Colon,
               Convert(),
               Required ? Signals.ExclamationPoint : "",
               ValueDefault != null ? $"{Signals.EqualSign}{ValueDefault}" : ""
            );
         }
      }

      public string KeyArgument
      {
         get
         {
            return string.Format(CultureInfo.InvariantCulture, "{0}:{1}{2}", Name, Signals.DollarSign, Name);
         }
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
