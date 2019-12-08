namespace Canducci.GraphQLQuery
{
   public struct Any
   {
      public string Name { get; }
      public object Value { get; }
      public Any(string name, object value)
      {
         Name = name;
         Value = value;
      }
   }
}
