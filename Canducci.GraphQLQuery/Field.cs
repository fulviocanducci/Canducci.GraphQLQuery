using Canducci.GraphQLQuery.Interfaces;
namespace Canducci.GraphQLQuery
{
   public class Field : IField
   {
      public string Name { get; }
      public string Alias { get; }
      public Field(string name)
        : this(name, "") { }
      public Field(string name, string alias)
      {
         Name = name;
         Alias = alias;
      }
   }
}
