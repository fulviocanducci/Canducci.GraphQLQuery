using Canducci.GraphQLQuery.Interfaces;
namespace Canducci.GraphQLQuery
{
   public class FieldRelationship : IFieldRelationship
   {
      public string Name { get; }
      public string Alias { get; }
      public Fields Fields { get; }
      public FieldRelationship(string name, Fields fields)
        : this(name, "", fields) { }
      public FieldRelationship(string name, string alias, Fields fields)
      {
         Name = name;
         Alias = alias;
         Fields = fields;
      }
   }
}
