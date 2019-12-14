namespace Canducci.GraphQLQuery.Interfaces
{
   public interface IField
   {
      string Name { get; }
      string Alias { get; }
      IDirective Directive { get; }
      IQueryType QueryType { get; }
   }
}
