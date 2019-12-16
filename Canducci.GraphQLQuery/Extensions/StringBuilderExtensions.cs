using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.Internals;
using Canducci.GraphQLQuery.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canducci.GraphQLQuery.Extensions
{
   internal static class StringBuilderExtensions
   {
      internal static StringBuilder Append(this StringBuilder stringBuilder, Variables variables, IQueryType[] queryTypes)
      {
         stringBuilder.Append<Variables>(variables);
         stringBuilder.Append<IQueryType>(queryTypes);
         stringBuilder.Append(Signals.BraceClose);
         return stringBuilder;
      }

      internal static StringBuilder Append(this StringBuilder stringBuilder, string alias, string name, IDirective[] directives = null)
      {
         if (name != null)
         {
            stringBuilder.Append(string.IsNullOrEmpty(alias) ? name : $"{alias}{Signals.Colon}{name}");
         }
         if (directives != null && directives.Length > 0)
         {
            stringBuilder.Append(directives.ConvertAll());
         }
         return stringBuilder;
      }

      internal static StringBuilder Append<T>(this StringBuilder stringBuilder, Variables variables) where T : Variables
      {
         if (variables?.Count > 0)
         {
            stringBuilder.Append($"{Signals.Query} {variables.QueryName}");
            stringBuilder.Append(Signals.ParenthesisOpen);
            IVariable variableLast = variables.LastOrDefault();
            foreach (IVariable variable in variables)
            {
               stringBuilder.Append(variable.GetKeyParam());
               if (!variable.Equals(variableLast))
               {
                  stringBuilder.Append(Signals.Comma);
               }
            }
            stringBuilder.Append(Signals.ParenthesisClose);
         }
         stringBuilder.Append(Signals.BraceOpen);
         return stringBuilder;
      }
      internal static StringBuilder Append<T>(this StringBuilder str, T values) where T : IList<IVariableValue>
      {
         if (values?.Count > 0)
         {
            using (VariablesObjectBuilder variablesObjectBuilder = VariablesObjectBuilder.Create())
            {
               str.Append(Signals.Comma);
               str.Append(Signals.QuotationMark);
               str.Append(Signals.Variables);
               str.Append(Signals.QuotationMark);
               str.Append(Signals.Colon);
               str.Append(Utils.Convert.ToJsonString
                  (
                     variablesObjectBuilder.CreateObjectWithValues(values)
                  )
               );
            }
         }
         return str;
      }

      internal static StringBuilder Append<T>(this StringBuilder stringBuilder, IList<T> queryTypes) where T : IQueryType
      {
         foreach (IQueryType queryType in queryTypes)
         {
            stringBuilder.Append(queryType.Alias, queryType.Name);
            if (queryType?.Arguments?.Count > 0)
            {
               stringBuilder.Append(Signals.ParenthesisOpen);
               queryType.Arguments.Append(stringBuilder);
               stringBuilder.Append(Signals.ParenthesisClose);
            }
            if (queryType.FragmentType != null)
            {
               stringBuilder.Append(queryType.FragmentType.FragmentNameAndType);
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
                  else if (field.FragmentType != null)
                  {
                     stringBuilder.Append(field.FragmentType.FragmentName);
                  }
                  else
                  {
                     stringBuilder.Append(field.Alias, field.Name, field?.Directives);
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
