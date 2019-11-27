namespace Canducci.GraphQLQuery.Interfaces
{
   public interface ITypeQL: System.IDisposable
   {      
      IQueryType[] QueryTypes { get; }
      string ToStringJson();
   }
}
