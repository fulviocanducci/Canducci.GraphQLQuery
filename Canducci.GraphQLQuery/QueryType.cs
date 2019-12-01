using Canducci.GraphQLQuery.Interfaces;
namespace Canducci.GraphQLQuery
{
   public class QueryType : IQueryType
   {
      public string Name { get; }
      public string Alias { get; }
      public Arguments Arguments { get; private set; }
      public Fields Fields { get; private set; }
      public Variables Variables { get; private set; }

      public QueryType(string name, Fields fields) : this(name, "", null, fields, null) { }
      public QueryType(string name, Fields fields, Variables variables) : this(name, "", null, fields, variables) { }
      public QueryType(string name, Arguments arguments, Fields fields) : this(name, "", arguments, fields, null) { }
      public QueryType(string name, Arguments arguments, Fields fields, Variables variables) : this(name, "", arguments, fields, variables) { }
      public QueryType(string name, string alias, Fields fields) : this(name, alias, null, fields, null) { }
      public QueryType(string name, string alias, Fields fields, Variables variables) : this(name, alias, null, fields, variables) { }
      public QueryType(string name, string alias, Arguments arguments, Fields fields, Variables variables)
      {
         Name = name;
         Alias = alias;
         Arguments = arguments;
         Fields = fields;
         Variables = variables;
      }   
   }
}
