using System;

namespace Canducci.GraphQLQuery.MSTest.Queries.Datas
{
   public class Source
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public decimal? Value { get; set; }
      public DateTime? Created { get; set; }
      public bool? Active { get; set; }
      public DateTime? Time { get; set; }
   }
}
