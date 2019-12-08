namespace Canducci.GraphQLQuery
{
   public struct ID 
   {
      public string Name { get; }
      public object Value { get; }
      public ID(string name, object value)
      {
         Name = name;
         Value = value;
      }
   }
}
