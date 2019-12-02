using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.Internals;
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
               str.Append(variable.KeyArgument);
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
            dic.Add(item.Name, item.Value);
         }
         return dic;
      }
   }
}
