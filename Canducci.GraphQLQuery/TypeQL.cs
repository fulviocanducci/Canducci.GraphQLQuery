﻿using Canducci.GraphQLQuery.Extensions;
using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.Internals;
using System.Collections.Generic;
using System.Text;
namespace Canducci.GraphQLQuery
{   
   public class TypeQL : ITypeQL
   {
      public IQueryType[] QueryTypes { get; private set; }
      public Variables Variables { get; private set; } = null;
      public Fragments Fragments { get; private set; } = null;

      public TypeQL(params IQueryType[] queryTypes)
      {
         QueryTypes = queryTypes;
      }

      public TypeQL(Fragments fragments, params IQueryType[] queryTypes)
      {
         Fragments = fragments;
         QueryTypes = queryTypes;
      }

      public TypeQL(Variables variables, params IQueryType[] queryTypes)
      {
         Variables = variables;
         QueryTypes = queryTypes;
      }

      public TypeQL(Variables variables, Fragments fragments, params IQueryType[] queryTypes)
      {
         Variables = variables;
         Fragments = fragments;
         QueryTypes = queryTypes;
      }

      public string ToBodyJson()
      {
         StringBuilder stringBuilder = new StringBuilder();
         stringBuilder.Append(Variables, QueryTypes, Fragments);
         return stringBuilder.ToString().Replace("\\", "");
      }

      public string ToStringJson()
      {
         StringBuilder stringBuilder = new StringBuilder();
         stringBuilder.Append(Signals.BraceOpen);
         stringBuilder.Append(Signals.QuotationMark);
         stringBuilder.Append(Signals.Query);
         stringBuilder.Append(Signals.QuotationMark);
         stringBuilder.Append(Signals.Colon);
         stringBuilder.Append(Signals.QuotationMark);
         stringBuilder.Append(Variables, QueryTypes, Fragments);
         stringBuilder.Append(Signals.QuotationMark);
         stringBuilder.Append<IList<IVariableValue>>(Variables?.Values());
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
