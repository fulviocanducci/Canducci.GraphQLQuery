using Canducci.GraphQLQuery.Interfaces;
namespace Canducci.GraphQLQuery
{
   public class Field : IField
   {
      public string Name { get; }
      public string Alias { get; }
      public IQueryType QueryType { get; set; }
      public Field(string name) : this(name, "") { }
      public Field(string name, string alias)
      {
         Name = name;
         Alias = alias;
         QueryType = null;
      }
      public Field(IQueryType queryType) : this(null, null)
      {
         QueryType = queryType;
      }
   }
}
