using Canducci.GraphQLQuery.Extensions;
using Canducci.GraphQLQuery.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Canducci.GraphQLQuery
{
   public class Fields : List<IField>
   {
      public Fields(params IField[] fields)
      {
         AddFields(fields);
      }

      public Fields(params string[] fields)
      {
         IField[] fieldsArray = new Field[fields.Length];
         for (int i = 0; i < fields.Length; i++)
         {
            string field = fields[i];
            if (field.IndexOf(",") == -1)
            {
               fieldsArray[i] = new Field(field);
            }
            else
            {
               string[] fieldWithAlias = field.Split(',');
               fieldsArray[i] = new Field(fieldWithAlias[0], fieldWithAlias[1]);
            }
         }
         AddFields(fieldsArray);
      }

      internal void AddFields(IField[] fields)
      {
         if (fields.DistinctName().Count() != fields.Count())
         {
            throw new Exception("Duplicate Fields names");
         }
         AddRange(fields);
      }
      
   }
}
