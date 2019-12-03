using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public class Source
   {
      public Guid? Id { get; set; }
      public string Name { get; set; }
      public decimal? Value { get; set; }
      public DateTime? Created { get; set; }
      public bool? Active { get; set; }
   }
}
