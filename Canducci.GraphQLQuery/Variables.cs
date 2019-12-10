using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.Internals;
using Canducci.GraphQLQuery.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canducci.GraphQLQuery
{
   public class Variables : List<IVariable>
   {
      public string QueryName { get; }
      public Variables(string queryName, params IVariable[] variables)
      {
         if (variables.Distinct(new VariableComparer()).Count() != variables.Count())
         {
            throw new Exception("Duplicate Variable names");
         }
         AddRange(variables);
         QueryName = queryName;
      }
      internal void AppendStringBuilder(StringBuilder str)
      {
         if (Count > 0)
         {
            str.Append(Signals.ParenthesisOpen);
            foreach (IVariable variable in this)
            {
               str.Append(variable.GetKeyArgument());
               if (!variable.Equals(this.LastOrDefault()))
               {
                  str.Append(Signals.Comma);
               }
            }
            str.Append(Signals.ParenthesisClose);
         }
      }
      internal Dictionary<string, object> Values()
      {
         Dictionary<string, object> dic = new Dictionary<string, object>();
         foreach (var item in this)
         {
            if (!dic.ContainsKey(item.Name))
            {
               dic.Add(item.Name, item.GetValue());
            }
         }
         return dic;
      }

      public static implicit operator Dictionary<string, object>(Variables variables)
      {
         return variables?.Values();
      }
   }
}
