using Canducci.GraphQLQuery.Interfaces;
namespace Canducci.GraphQLQuery
{
   public class Field : IField
   {
      public string Name { get; }
      public string Alias { get; }
      public IQueryType QueryType { get; }
      public IDirective[] Directives { get; }

      public Field(string name) 
         : this(name, null, null) { }

      public Field(string name, string alias)
         : this(name, alias, null) { }

      public Field(string name, IDirective[] directives) 
         : this(name, null, directives) { }

      public Field(string name, string alias, IDirective[] directives)
      {
         Name = name;
         Alias = alias;
         Directives = directives;
         QueryType = null;
      }

      public Field(IQueryType queryType) : this(null, null, null)
      {
         QueryType = queryType;
      }

      public Field(IQueryType queryType, IDirective[] directives) 
         : this(queryType)
      {
         Directives = directives;
      }
   }
}
