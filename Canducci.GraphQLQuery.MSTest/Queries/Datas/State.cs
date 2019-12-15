using System.Collections.Generic;

namespace Canducci.GraphQLQuery.MSTest.Queries.Datas
{
   public class State
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public ICollection<City> Cities { get; set; }
   }
}
