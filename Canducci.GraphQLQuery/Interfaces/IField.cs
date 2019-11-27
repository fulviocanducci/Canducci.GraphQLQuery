namespace Canducci.GraphQLQuery.Interfaces
{
   public interface IField
   {
      string Name { get; }
      string Alias { get; }
      IQueryType QueryType { get; set; }
   }
}
