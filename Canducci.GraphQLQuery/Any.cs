namespace Canducci.GraphQLQuery
{
   public sealed class Any: Bases.BaseScalar
   {      
      public Any(string name, object value)
      {
         Name = name ?? throw new System.ArgumentNullException(nameof(name));
         Value = value ?? throw new System.ArgumentNullException(nameof(value));
      }
   }
}
