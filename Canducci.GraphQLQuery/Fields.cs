using Canducci.GraphQLQuery.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Canducci.GraphQLQuery
{
   public class Fields : List<IField>
   {
      public Fields(params IField[] fields)
      {
         AddRange(fields);
      }
      internal void AppendStringBuilder(StringBuilder str)
      {
         foreach (IField field in this)
         {
            str.Append(string.IsNullOrEmpty(field.Alias) 
               ? field.Name.ToLowerInvariant() 
               : $"{field.Alias.ToLowerInvariant()}:{field.Name.ToLowerInvariant()}");
            if (field is IFieldRelationship fieldRelationship)
            {
               str.Append("{");
               fieldRelationship.Fields.AppendStringBuilder(str);
               str.Append("}");
            }             
            if (field.Equals(this.LastOrDefault()) == false) str.Append(",");            
         }
      }
   }
}
