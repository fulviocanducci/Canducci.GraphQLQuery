using Canducci.GraphQLQuery.Interfaces;
namespace Canducci.GraphQLQuery
{
  public class TypeQLConfiguration: ITypeQLConfiguration
  {
    public string Separation { get; } = ",";
    public ArgumentFormat ArgumentFormat { get; } = ArgumentFormat.FormatDateTime;    
    public TypeQLConfiguration(string separation, ArgumentFormat argumentFormat)
    {
      Separation = separation;
      ArgumentFormat = argumentFormat;      
    }    
  }
}
