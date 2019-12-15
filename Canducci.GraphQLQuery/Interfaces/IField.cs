namespace Canducci.GraphQLQuery.Interfaces
{
   public interface IField
   {
      string Name { get; }
      string Alias { get; }
      IDirective[] Directives { get; }
      IQueryType QueryType { get; }
   }
}
