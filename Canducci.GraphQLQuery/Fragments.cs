using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.Extensions;
using System.Collections.Generic;
using System.Text;

namespace Canducci.GraphQLQuery
{
   public sealed class Fragments: List<IFragment>
   {
      public Fragments(params IFragment[] fragments)
      {         
         AddRange(fragments);
      }
      internal void Append(StringBuilder str = null)
      {
         if (str is null) 
         { 
            str = new StringBuilder(); 
         }
         ForEach(x =>
         {
            str.Append<IQueryType>(new IQueryType[1] { x.QueryType });
         });
      }
   }
}
