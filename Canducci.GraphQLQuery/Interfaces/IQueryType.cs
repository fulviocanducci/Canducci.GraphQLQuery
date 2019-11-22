namespace Canducci.GraphQLQuery.Interfaces
{
  public interface IQueryType
  {
    string Name { get; }
    string Alias { get; }
    Arguments Arguments { get; }
    Fields Fields { get; }    
  }
}
