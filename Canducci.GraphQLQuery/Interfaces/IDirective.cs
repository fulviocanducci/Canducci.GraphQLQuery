namespace Canducci.GraphQLQuery.Interfaces
{
   public interface IDirective
   {
      string Layout { get; }
      string Convert();
      string Name { get; }
   }
}
