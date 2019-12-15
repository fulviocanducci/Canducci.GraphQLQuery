using System.Collections.Generic;

namespace Canducci.GraphQLQuery.MSTest.Queries.Datas
{
   public class Cities : List<City>
   {
      public Cities()
      {
         var c1 = new City { Id = 1, Name = "SÃO PAULO", StateId = 1, State = new State { Id = 1, Name = "SÃO PAULO" } };
         var c2 = new City { Id = 2, Name = "RIO DE JANEIRO", StateId = 2, State = new State { Id = 2, Name = "RIO DE JANEIRO" } };
         Add(c1);
         Add(c2);
      }
   }
}
