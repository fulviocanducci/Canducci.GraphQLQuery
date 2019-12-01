using Canducci.GraphQLQuery.Extensions;
using Canducci.GraphQLQuery.Interfaces;
using System.Linq;
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
         StringBuilder stringBuilder = new StringBuilder();
         stringBuilder.Append(Signals.BraceOpen);
         stringBuilder.Append(Signals.QuotationMark);
         stringBuilder.Append(Signals.Query);
         stringBuilder.Append(Signals.QuotationMark);
         stringBuilder.Append(Signals.Colon);                  
         stringBuilder.Append<IQueryType>(QueryTypes);
         stringBuilder.Append(Signals.BraceClose);
         return stringBuilder.ToString();
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
