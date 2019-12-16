using Canducci.GraphQLQuery.Interfaces;
using System;

namespace Canducci.GraphQLQuery
{
   public sealed class Fragment : IFragment
   {      
      public QueryType QueryType { get; }
      public Fragment(QueryType queryType)
      {         
         QueryType = queryType ?? throw new ArgumentNullException(nameof(queryType));    
      }
   }
}
