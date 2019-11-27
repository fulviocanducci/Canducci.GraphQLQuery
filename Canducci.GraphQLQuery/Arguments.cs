using Canducci.GraphQLQuery.Interfaces;
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
         foreach (IArgument argument in this)
         {
            str.Append(argument.KeyValue);
            if (argument.Equals(this.LastOrDefault()) == false)
            {
               str.Append(",");
            }
         }
      }
   }
}
