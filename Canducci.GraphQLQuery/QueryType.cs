using Canducci.GraphQLQuery.Interfaces;
namespace Canducci.GraphQLQuery
{
  public class QueryType : IQueryType
  {
    public QueryType(string name)
    {
      Name = name;
    }
    public QueryType(string name, string alias)
    {
      Name = name;
      Alias = alias;
    }
    public string Name { get; }
    public string Alias { get; }
  }
}
