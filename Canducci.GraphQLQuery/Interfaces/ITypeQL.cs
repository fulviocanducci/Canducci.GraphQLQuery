namespace Canducci.GraphQLQuery.Interfaces
{
  public interface ITypeQL
  {
    ITypeQLConfiguration Configuration { get; }
    IQueryType QueryTypeItem { get; }
    Arguments Arguments { get; }
    Fields Fields { get; }
    string Render();
  }
}
