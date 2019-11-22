namespace Canducci.GraphQLQuery.Interfaces
{
  public interface IFieldRelationship : IField
  {
    Fields Fields { get; }
    Arguments Arguments { get; }
  }
}
