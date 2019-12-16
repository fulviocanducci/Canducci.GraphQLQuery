namespace Canducci.GraphQLQuery.Interfaces
{
   public interface ITypeQL : System.IDisposable
   {
      IQueryType[] QueryTypes { get; }
      Variables Variables { get; }
      Fragments Fragments { get; }      
      string ToStringJson();
      string ToBodyJson();
   }
}
