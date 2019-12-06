namespace Canducci.GraphQLQuery.Interfaces
{
   public interface IArgument
   {
      string Name { get; }
      object Value { get; }
      string Convert();
      string KeyValue { get; }
   }
}
