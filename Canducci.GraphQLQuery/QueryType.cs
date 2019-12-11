using Canducci.GraphQLQuery.Interfaces;
namespace Canducci.GraphQLQuery
{
   public class QueryType : IQueryType
   {
      public string Name { get; }
      public string Alias { get; }
      public Fields Fields { get; private set; }
      public Arguments Arguments { get; private set; } = null;      

      public QueryType(string name, Fields fields)
      {
         Name = name;
         Fields = fields;
      }     

      public QueryType(string name, Fields fields, Arguments arguments)
      {
         Name = name;
         Fields = fields;
         Arguments = arguments;
      }
      
      public QueryType(string name, string alias, Fields fields)
      {
         Name = name;
         Alias = alias;
         Fields = fields;
      }
     
      public QueryType(string name, string alias, Fields fields, Arguments arguments)
      {
         Name = name;
         Alias = alias;
         Arguments = arguments;
         Fields = fields;         
      }   
   }
}
