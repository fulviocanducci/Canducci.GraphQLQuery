using Canducci.GraphQLQuery.Interfaces;
namespace Canducci.GraphQLQuery
{
  public class QueryType : IQueryType
  {
    public string Name { get; }
    public string Alias { get; }
    public Arguments Arguments { get; private set; }
    public Fields Fields { get; private set; }
    public QueryType(string name, Arguments arguments, Fields fields)
      : this(name, "", arguments, fields) { }

    public QueryType(string name, Fields fields)
      : this(name, "", null, fields) { }

    public QueryType(string name, string alias, Fields fields)
      : this(name, alias, null, fields) { }
    public QueryType(string name, string alias, Arguments arguments, Fields fields)
    {
      Name = name;
      Alias = alias;
      Arguments = arguments;
      Fields = fields;
    }    
  }
}
