using Canducci.GraphQLQuery.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Canducci.GraphQLQuery
{
   public class Variables: List<IVariable>
   {
      public string QueryName { get; }
      public Variables(string queryName, params IVariable[] variables)
      {
         AddRange(variables);
         QueryName = queryName;
      }

      internal void AppendStringBuilder(StringBuilder str)
      {
         foreach (IVariable variable in this)
         {
            str.Append(variable.Name);
            str.Append(Signals.Colon);
            str.Append(Signals.DollarSign);
            str.Append(variable.Name);
            if (!variable.Equals(this.LastOrDefault()))
            {
               str.Append(Signals.Comma);
            }
         }
      }

      internal IList<KeyValuePair<string, object>> Values()
      {
         return this.Select(x => new KeyValuePair<string, object>(x.Name, x.Value)).ToList();
      }      
   }
}
