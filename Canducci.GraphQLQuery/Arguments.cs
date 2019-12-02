using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.Internals;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Canducci.GraphQLQuery
{
   public class Arguments : List<IArgument>
   {
      public Arguments(params IArgument[] arguments)
      {
         AddRange(arguments);
      }
      internal void AppendStringBuilder(StringBuilder str = null)
      {
         if (Count > 0)
         {            
            foreach (IArgument argument in this)
            {
               str.Append(argument.KeyValue);
               if (!argument.Equals(this.LastOrDefault()))
               {
                  str.Append(Signals.Comma);
               }
            }
         }
      }
   }
}
