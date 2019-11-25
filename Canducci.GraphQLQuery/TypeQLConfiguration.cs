using Canducci.GraphQLQuery.Interfaces;
namespace Canducci.GraphQLQuery
{
  public class TypeQLConfiguration: ITypeQLConfiguration
  {
    public Separation Separation { get; private set; }
    public ArgumentFormat ArgumentFormat { get; private set; }
    public TypeQLConfiguration()
      :this(Separation.Comma, ArgumentFormat.FormatDateTime)
    {
    }
    public TypeQLConfiguration(Separation separation, ArgumentFormat argumentFormat)
    {
      Separation = separation;
      ArgumentFormat = argumentFormat;      
    }    
  }
}
