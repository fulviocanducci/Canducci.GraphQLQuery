namespace Canducci.GraphQLQuery.Interfaces
{
   public interface IArgument
   {
      string Name { get; }
      object Value { get; }
      IRule Rule { get; }
      string Convert();
      string KeyValue { get; }
   }
}
