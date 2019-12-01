namespace Canducci.GraphQLQuery.Interfaces
{
   public interface IVariable
   {
      string Name { get; set; }
      object Value { get; set; }
      object ValueDefault { get; set; }
      bool Required { get; set; }      
   }
}
