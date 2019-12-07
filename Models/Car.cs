using System;
namespace Canducci.GraphQLQuery.MSTest.Models
{
   public class Car
   {
      public int Id { get; set; }
      public string Title { get; set; }
      public DateTime Purchase { get; set; }
      public decimal Value { get; set; }
      public bool Active { get; set; }
      public TimeSpan? Time { get; set; }

   }
}
