namespace Canducci.GraphQLQuery.Bases
{
   public abstract class BaseScalar
   {
      public string Name { get; protected set; }
      public object Value { get; protected set; }
   }
}
