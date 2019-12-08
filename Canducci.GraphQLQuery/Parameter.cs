namespace Canducci.GraphQLQuery
{
   public sealed class Parameter
   {
      public string Name { get; }
      public Parameter(string name)
      {
         Name = name ?? throw new System.ArgumentNullException(nameof(name));
      }
   }
}
