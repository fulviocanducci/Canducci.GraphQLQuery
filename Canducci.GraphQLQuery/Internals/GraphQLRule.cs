using Canducci.GraphQLQuery.Interfaces;
using System;

namespace Canducci.GraphQLQuery
{
   internal sealed class GraphQLRule : IGraphQLRule
   {
      public Type TypeArgument { get; private set; }
      public Format Format { get; private set; }
      public Func<string> Convert { get; set; }
      public GraphQLRule(Type typeArgument, Format format, Func<string> convert)
      {
         TypeArgument = typeArgument;
         Format = format;
         Convert = convert;
      }
   }
}