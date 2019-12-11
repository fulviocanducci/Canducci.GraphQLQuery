namespace Canducci.GraphQLQuery
{
   public sealed class Parameter
   {
      public string Name { get; }
      public string Variable { get; }

      public Parameter(string name)
         :this(name, null)
      {
      }

      public Parameter(string name, string variable)
      {
         Name = name ?? throw new System.ArgumentNullException(nameof(name));
         Variable = variable;
      }
   }
}
