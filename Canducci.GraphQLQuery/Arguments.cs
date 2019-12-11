using Canducci.GraphQLQuery.Extensions;
using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canducci.GraphQLQuery
{
   public class Arguments : List<IArgument>
   {
      public Arguments(params IArgument[] arguments)
      {
         if (arguments.DistinctName().Count() != arguments.Count())
         {
            throw new Exception("Duplicate Argument names");
         }
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
