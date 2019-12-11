using System;
using System.Collections.Generic;

namespace Canducci.GraphQLQuery.MSTest.Queries.Datas
{
   public class Sources : List<Source>
   {
      public Sources()
      {
         Add(new Source() { Id = 1, Name = "Example 1", Created = DateTime.Parse("01/01/1999"), Time = null, Value = 0, Active = true });
         Add(new Source() { Id = 2, Name = "Example 2", Created = DateTime.Parse("02/01/1999"), Time = null, Value = 1, Active = false });
         Add(new Source() { Id = 3, Name = "Example 3", Created = DateTime.Parse("03/01/1999"), Time = null, Value = 2, Active = true });
      }
   }
}
