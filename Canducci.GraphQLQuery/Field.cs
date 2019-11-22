using Canducci.GraphQLQuery.Interfaces;

namespace Canducci.GraphQLQuery
{
  public class Field : IField
  {
    public string Name { get; }
    public string Alias { get; }
    public Fields Fields { get; }

    public Field(string name)
      :this(name, "") { }
    public Field(string name, string alias)
      : this(name, alias, null) { }
    public Field(string name, Fields fields)
      : this(name, "", fields) { }
    public Field(string name, string alias, Fields fields)
    {
      Name = name;
      Alias = alias;
      Fields = fields;
    }
   
    public static IField Create(string name) 
      => new Field(name);
    public static IField Create(string name, string alias) 
      => new Field(name, alias);
    public static IField Create(string name, Fields fields) 
      => new Field(name, "", fields);
    public static IField Create(string name, string alias, Fields fields) 
      => new Field(name, alias, fields);
  }
}
