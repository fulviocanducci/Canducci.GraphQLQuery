namespace Canducci.GraphQLQuery.Interfaces
{
   public interface IFragmentType
   {
      string Name { get; }
      string NameType { get; }
      string FragmentName { get; }
      string FragmentNameAndType { get; }
   }
}
