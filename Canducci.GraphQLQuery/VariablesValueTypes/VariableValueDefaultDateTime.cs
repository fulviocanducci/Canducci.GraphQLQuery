using Canducci.GraphQLQuery.Internals;
using System;
using System.Globalization;

namespace Canducci.GraphQLQuery.VariablesValueTypes
{
   public class VariableValueDefaultDateTime : VariableValueDefault
   {
      public VariableValueDefaultDateTime(DateTime value)
      {
         Value = string.Format(CultureInfo.InvariantCulture, value.ToString(Formats.DateTime));
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
