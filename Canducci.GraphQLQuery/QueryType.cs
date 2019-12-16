using Canducci.GraphQLQuery.Interfaces;
namespace Canducci.GraphQLQuery
{
   public sealed class QueryType : IQueryType
   {
      public string Name { get; } = null;
      public string Alias { get; } = null;
      public Fields Fields { get; } = null;
      public Arguments Arguments { get; } = null;
      public FragmentType FragmentType { get; } = null;

      public QueryType(FragmentType fragmentType, Fields fields)
      {
         FragmentType = fragmentType ?? throw new System.ArgumentNullException(nameof(fragmentType));
         Fields = fields ?? throw new System.ArgumentNullException(nameof(fields));
      }

      public QueryType(string name, Fields fields)
      {
         Name = name ?? throw new System.ArgumentNullException(nameof(name));
         Fields = fields ?? throw new System.ArgumentNullException(nameof(fields));
      }

      public QueryType(string name, Fields fields, Arguments arguments)
      {
         Name = name ?? throw new System.ArgumentNullException(nameof(name));
         Fields = fields ?? throw new System.ArgumentNullException(nameof(fields));
         Arguments = arguments ?? throw new System.ArgumentNullException(nameof(arguments));
      }

      public QueryType(string name, string alias, Fields fields)
      {
         Name = name ?? throw new System.ArgumentNullException(nameof(name));
         Alias = alias ?? throw new System.ArgumentNullException(nameof(alias));
         Fields = fields ?? throw new System.ArgumentNullException(nameof(fields));
      }

      public QueryType(string name, string alias, Fields fields, Arguments arguments)
      {
         Name = name ?? throw new System.ArgumentNullException(nameof(name));
         Alias = alias ?? throw new System.ArgumentNullException(nameof(alias));
         Fields = fields ?? throw new System.ArgumentNullException(nameof(fields));
         Arguments = arguments ?? throw new System.ArgumentNullException(nameof(arguments));
      }
   }
}
