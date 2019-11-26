namespace Canducci.GraphQLQuery.Interfaces
{
   public interface ITypeQL: System.IDisposable
   {
      ITypeQLConfiguration Configuration { get; }
      IQueryType[] QueryTypes { get; }
      string ToStringJson();
   }
}
