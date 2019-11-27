using Canducci.GraphQLQuery.Interfaces;
using System.Text;
namespace Canducci.GraphQLQuery
{
   public class TypeQL : ITypeQL
   {
      public IQueryType[] QueryTypes { get; private set; }
      public TypeQL(params IQueryType[] queryTypes)
      {
         QueryTypes = queryTypes;
      }
      public string ToStringJson()
      {       
         StringBuilder str = new StringBuilder();
         foreach (IQueryType item in QueryTypes)
         {
            str.Append("{");
            str.Append("\"");
            str.Append("query");
            str.Append("\"");
            str.Append(":");
            str.Append("\"");
            str.Append("{");            
            str.Append(string.IsNullOrEmpty(item.Alias) ? item.Name.ToLowerInvariant(): $"{item.Alias.ToLowerInvariant()}:{item.Name.ToLowerInvariant()}");
            if (item?.Arguments?.Count > 0)
            {
               str.Append("(");
               item.Arguments.AppendStringBuilder(str);
               str.Append(")");
            }            
            if (item?.Fields?.Count > 0)
            {
               str.Append("{");
               item.Fields.AppendStringBuilder(str);
               str.Append("}");
            }                     
            str.Append("}");
            str.Append("\"");
            str.Append("}");
         }
         return str.ToString();
      }
      public static implicit operator string(TypeQL typeQL)
      {
         return typeQL.ToStringJson();
      }
      public void Dispose()
      {
         QueryTypes = null;
         System.GC.SuppressFinalize(this);
      }
   }
}
