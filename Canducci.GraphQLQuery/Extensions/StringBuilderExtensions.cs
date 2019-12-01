using Canducci.GraphQLQuery.Interfaces;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;

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

      internal static StringBuilder Append<T>(this StringBuilder stringBuilder, Variables variables)
         where T: Variables
      {
         if (variables?.Count > 0)
         {
            stringBuilder.Append($"{Signals.Query} {variables.QueryName}");
            stringBuilder.Append(Signals.ParenthesisOpen);
            IVariable variableLast = variables.LastOrDefault();
            foreach (IVariable variable in variables)
            {
               stringBuilder.Append($"{Signals.DollarSign}{variable.Name}{Signals.Colon}{variable.Name}");
               if (variable.Required)
               {
                  stringBuilder.Append(Signals.ExclamationPoint);
               }
               if (variable.ValueDefault != null)
               {
                  stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "{0}{1}", Signals.EqualSign, variable.ValueDefault));
               }
               if (!variable.Equals(variableLast))
               {
                  stringBuilder.Append(Signals.Comma);
               }
            }
            stringBuilder.Append(Signals.ParenthesisClose);
         }
         return stringBuilder;
      }

      internal static StringBuilder Append<T>(this StringBuilder str, T values)
         where T: List<KeyValuePair<string, object>>
      {
         if (values.Count > 0)
         {
            str.Append(Signals.Comma);
            str.Append(Signals.QuotationMark);
            str.Append(Signals.Variables);
            str.Append(Signals.QuotationMark);
            str.Append(Signals.Colon);
            str.Append(JsonSerializer.Serialize(values, typeof(IList<KeyValuePair<string, object>>)));
         }
         return str;
      }

      internal static StringBuilder Append<T>(this StringBuilder stringBuilder, IList<T> queryTypes)
         where T : IQueryType
      {
         stringBuilder.Append(Signals.QuotationMark);
         Dictionary<string, object> variables = new Dictionary<string, object>();
         foreach (IQueryType queryType in queryTypes)
         {
            stringBuilder.Append<Variables>(queryType?.Variables);
            stringBuilder.Append(Signals.BraceOpen);
            stringBuilder.Append(queryType.Alias, queryType.Name);
            if (queryType.Variables?.Count > 0)
            {
               stringBuilder.Append(Signals.ParenthesisOpen);
               queryType.Variables.AppendStringBuilder(stringBuilder);
               stringBuilder.Append(Signals.ParenthesisClose);
               variables.AddRange(queryType.Variables.Values());
            }
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
            stringBuilder.Append(Signals.BraceClose);            
         }
         stringBuilder.Append(Signals.QuotationMark);
         //stringBuilder.Append<List<KeyValuePair<string, object>>>(variables);
         return stringBuilder;
      }
   }
}
