using Canducci.GraphQLQuery.Interfaces;
namespace Canducci.GraphQLQuery
{
  public class FieldRelationship : IFieldRelationship
  {
    #region Properties
    public string Name { get; }
    public string Alias { get; }
    public Fields Fields { get; }
    #endregion

    #region Constructors
    public FieldRelationship(string name, Fields fields)
      : this(name, "", fields) { }
    public FieldRelationship(string name, string alias, Fields fields)
    {
      Name = name;
      Alias = alias;
      Fields = fields;
    }
    #endregion

    #region Fabric
    public static IFieldRelationship Create(string name, Fields fields)
      => new FieldRelationship(name, "", fields);
    public static IFieldRelationship Create(string name, string alias, Fields fields)
      => new FieldRelationship(name, alias, fields);
    #endregion
  }
}
