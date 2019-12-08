namespace Canducci.GraphQLQuery
{
   public sealed class ID : Bases.BaseScalar
   {      
      public ID(string name, object value)
      {
         Name = name ?? throw new System.ArgumentNullException(nameof(name));
         Value = value ?? throw new System.ArgumentNullException(nameof(value));
      }
   }
}
