using Canducci.GraphQLQuery.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Canducci.GraphQLQuery.Extensions
{
   internal static class StringBuilderExtensions
   {
      internal static StringBuilder Append(this StringBuilder stringBuilder, string alias, string name)
      {
         stringBuilder.Append(string.IsNullOrEmpty(alias)
               ? name.ToLowerInvariant()
               : $"{alias.ToLowerInvariant()}{Signals.Colon}{name.ToLowerInvariant()}");
         return stringBuilder;
      }
      internal static StringBuilder Append<T>(this StringBuilder stringBuilder, IList<T> queryTypes)
         where T : IQueryType
      {
         foreach (IQueryType queryType in queryTypes)
         {
            stringBuilder.Append(queryType.Alias, queryType.Name);
            if (queryType?.Arguments?.Count > 0)
            {
               stringBuilder.Append(Signals.ParenthesisOpen);
               queryType.Arguments.AppendStringBuilder(stringBuilder);
               stringBuilder.Append(Signals.ParenthesisClose);
            }
            if (queryType?.Fields?.Count > 0)
            {
               stringBuilder.Append(Signals.BraceOpen);
               IField fieldLast = queryType.Fields.LastOrDefault();
               foreach (IField field in queryType.Fields)
               {
                  if (field.QueryType != null)
                  {
                     stringBuilder.Append<IQueryType>(new IQueryType[1] { field.QueryType });
                  }
                  else
                  {
                     stringBuilder.Append(field.Alias, field.Name);
                  }
                  if (!field.Equals(fieldLast))
                  {
                     stringBuilder.Append(Signals.Comma);
                  }
               }
            }
            stringBuilder.Append(Signals.BraceClose);
         }
         return stringBuilder;
      }
   }
}
