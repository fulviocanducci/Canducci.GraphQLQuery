using Canducci.GraphQLQuery.Interfaces;
namespace Canducci.GraphQLQuery
{
  public class FieldRelationship : IFieldRelationship
  {
    #region Properties
    public string Name { get; }
    public string Alias { get; }
    public Fields Fields { get; }
    public Arguments Arguments { get; private set; }
    #endregion

    #region Constructors
    public FieldRelationship(string name, Fields fields)
      : this(name, "", null, fields) { }    
    public FieldRelationship(string name, Arguments arguments, Fields fields)
      : this(name, "", arguments, fields) { }
    public FieldRelationship(string name, string alias, Fields fields)
      : this(name, alias, null, fields) { }
    public FieldRelationship(string name, string alias, Arguments arguments, Fields fields)
    {
      Name = name;
      Alias = alias;
      Arguments = arguments;
      Fields = fields;
    }
    #endregion

    #region Fabric
    public static IFieldRelationship Create(string name, Fields fields)
      => new FieldRelationship(name, "", null, fields);
    public static IFieldRelationship Create(string name, Arguments arguments, Fields fields)
      => new FieldRelationship(name, "", arguments, fields);
    public static IFieldRelationship Create(string name, string alias, Arguments arguments, Fields fields)
      => new FieldRelationship(name, alias, arguments, fields);
    #endregion
  }
}
