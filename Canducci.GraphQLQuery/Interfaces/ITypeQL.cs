namespace Canducci.GraphQLQuery.Interfaces
{
  public interface ITypeQL
  {
    ITypeQLConfiguration Configuration { get; }
    IQueryType[] QueryTypes { get; }    
    string ToStringJson();
  }
}
