using System.Collections.Generic;
namespace Canducci.GraphQLQuery.Interfaces
{
   public interface ITypeQL : System.IDisposable
   {
      IQueryType[] QueryTypes { get; }
      Variables Variables { get; }
      string ToStringJson();
      string ToBodyJson();
   }
}
