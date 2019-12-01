using Canducci.GraphQLQuery.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Canducci.GraphQLQuery
{
   public class Variable : IVariable
   {
      public Variable(string name, object value, bool required = false, object valueDefault = null)
      {         
         Name = name;
         Value = value;
         Required = required;
         ValueDefault = valueDefault;
      }
      public string Name { get; set; }
      public object Value { get; set; }
      public object ValueDefault { get; set; }
      public bool Required { get; set; }
   }
}
