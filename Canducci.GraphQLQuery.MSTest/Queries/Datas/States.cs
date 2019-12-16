using System.Collections.Generic;
using System.Linq;

namespace Canducci.GraphQLQuery.MSTest.Queries.Datas
{
   public class States : List<State>
   {
      public States()
      {
         var st1 = new State
         {
            Id = 1,
            Name = "SP",
            Cities = new List<City>()
         };
         st1.Cities.Add(new City { Id = 1, Name = "SÃO PAULO", StateId = 1 });
         var st2 = new State
         {
            Id = 2,
            Name = "RJ",
            Cities = new List<City>()
         };
         st2.Cities.Add(new City { Id = 2, Name = "RIO DE JANEIRO", StateId = 2 });
         Add(st1);
         Add(st2);
      }

      public State AddState(State state)
      {
         state.Id = this.LastOrDefault().Id + 1;
         Add(state);
         return state;
      }
   }
}
