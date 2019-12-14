using Canducci.GraphQLQuery.Interfaces;
namespace Canducci.GraphQLQuery
{
   public class Field : IField
   {
      public string Name { get; }
      public string Alias { get; }
      public IQueryType QueryType { get; }
      public IDirective Directive { get; }

      public Field(string name) 
         : this(name, null, null) { }

      public Field(string name, string alias)
         : this(name, alias, null) { }

      public Field(string name, IDirective directive) 
         : this(name, null, directive) { }

      public Field(string name, string alias, IDirective directive)
      {
         Name = name;
         Alias = alias;
         Directive = directive;
         QueryType = null;
      }

      public Field(IQueryType queryType) : this(null, null, null)
      {
         QueryType = queryType;
      }

      public Field(IQueryType queryType, IDirective directive) 
         : this(queryType)
      {
         Directive = directive;
      }
   }
}
