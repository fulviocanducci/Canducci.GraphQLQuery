using System;
using System.Collections.Generic;
using System.Text;

namespace Canducci.GraphQLQuery.Extensions
{
   internal static class DictionaryExtensions
   {
      public static Dictionary<string, object> AddRange(this Dictionary<string, object> dic, IList<KeyValuePair<string, object>> values)
      {
         foreach(KeyValuePair<string, object> value in values)
         {
            dic.Add(value.Key, value.Value);
         }
         return dic;
      }
   }
}
