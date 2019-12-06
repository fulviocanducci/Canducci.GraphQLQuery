using Canducci.GraphQLQuery.Interfaces;
using System;

namespace Canducci.GraphQLQuery.Internals
{
   internal sealed class Rule : IRule
   {
      public Type TypeArgument { get; private set; }
      public Format Format { get; private set; }
      public Func<object, string> Convert { get; set; }
      public Rule(Type typeArgument, Format format, Func<object, string> convert)
      {
         TypeArgument = typeArgument;
         Format = format;
         Convert = convert;
      }
   }
}