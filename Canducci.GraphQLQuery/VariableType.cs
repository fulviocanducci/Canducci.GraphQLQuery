using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.Internals;
using System;
using System.Collections;

namespace Canducci.GraphQLQuery
{
   public sealed class VariableType : IVariableType
   {
      public VariableType(Type type)
      {
         Type = type ?? throw new ArgumentNullException(nameof(type));
         InitConfigVariableTypeInternal();
      }
      public Type Type { get; }
      public bool IsIEnumerable { get; private set; } = false;
      public bool IsArray { get; private set; } = false;
      public Type TypeInternal { get; private set; }
      internal IGraphQLRule GraphQLRule { get; private set; } = null;
      internal void InitConfigVariableTypeInternal()
      {
         if (Type != typeof(string))
         {
            if (Type.IsArray)
            {
               IsArray = true;
               TypeInternal = Type.GetElementType();
               GraphQLRule = GraphQLRules.Instance.Rule(TypeInternal);
            }
            else if (typeof(IEnumerable).IsAssignableFrom(Type))
            {
               IsIEnumerable = true;
               if (Type.GenericTypeArguments.Length > 0)
               {
                  TypeInternal = Type.GenericTypeArguments[0];
                  GraphQLRule = GraphQLRules.Instance.Rule(TypeInternal);
               }
            }
         }
      }
      public string Convert(string value)
      {
         if (string.IsNullOrEmpty(value))
         {
            return null;
         }
         if (GraphQLRule == null)
         {
            return value;
         }
         return GraphQLRule.Format == Format.FormatClass
            ? string.Format(value, TypeInternal?.Name)
            : string.Format(value, GraphQLRule.Convert());
      }
   }
}
