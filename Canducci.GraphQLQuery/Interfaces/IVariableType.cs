using System;

namespace Canducci.GraphQLQuery.Interfaces
{
   public interface IVariableType
   {
      Type Type { get; }
      bool IsIEnumerable { get; }
      bool IsArray { get; }
      Type TypeInternal { get; }
      string Convert(string value);
   }
}
