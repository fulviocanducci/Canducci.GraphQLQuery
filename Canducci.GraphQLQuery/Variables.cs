﻿using Canducci.GraphQLQuery.Extensions;
using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.Internals;
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
         if (variables.DistinctName().Count() != variables.Count())
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

      public Dictionary<string, object> ToDictionary()
      {  
         return this.ToDictionary(x => x.Name, y => y.GetValue());
      }

      internal IList<IVariableValue> Values()
      {
         IList<IVariableValue> dic = new List<IVariableValue>();
         foreach (var item in this)
         {
            if (!dic.Any(x => x.Name == item.Name))
            {
               dic.Add(new VariableValue(item.Name, item.GetValue(), item.VariableType.Type));
            }
         }
         return dic;
      }
   }
}
