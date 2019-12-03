namespace Canducci.GraphQLQuery.Interfaces
{
   public interface IVariable
   {
      string Name { get; }
      string NameType { get; }
      object Value { get; }
      object ValueDefault { get; }
      bool Required { get; }
      IRule Rule { get; }
      string Convert();
      string KeyParam { get; }
      string KeyArgument { get; }
   }
}
