using Canducci.GraphQLQuery.Extensions;
using Canducci.GraphQLQuery.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Canducci.GraphQLQuery
{
   public class Fields : List<IField>
   {
      public Fields(params IField[] fields)
      {
         if (fields.DistinctName().Count() != fields.Count())
         {
            throw new Exception("Duplicate Fields names");
         }
         AddRange(fields);
      }
   }
}
