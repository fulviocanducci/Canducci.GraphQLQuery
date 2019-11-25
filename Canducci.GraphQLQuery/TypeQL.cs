using Canducci.GraphQLQuery.Interfaces;
namespace Canducci.GraphQLQuery
{  
  public class TypeQL : ITypeQL
  {
    #region Properties
    public ITypeQLConfiguration Configuration { get; private set; }
    public IQueryType[] QueryTypes { get; private set; }    
    #endregion
    #region Constructors
    public TypeQL(params IQueryType[] queryTypes)
      :this(new TypeQLConfiguration(Separation.Comma, ArgumentFormat.FormatDateTime), queryTypes) { }
    public TypeQL(ITypeQLConfiguration configuration, params IQueryType[] queryTypes)
    {
      QueryTypes = queryTypes;
      Configuration = configuration;
    }
    #endregion    
    #region ToStringJson
    public string ToStringJson()
    {
      using (Builder stringSourceBuilder = new Builder())
      {
        stringSourceBuilder
            .AppendKeyOpen()
            .AppendQuotationMark()
            .AppendQuery()
            .AppendQuotationMark()
            .AppendPoints()
            .AppendQuotationMark()
            .AppendKeyOpen();
        foreach (IQueryType item in QueryTypes)
        {
          stringSourceBuilder
            .AppendQueryType(item)
            .AppendScalarArguments(item.Arguments, Configuration)
            .AppendFields(item.Fields, Configuration);
        }
        stringSourceBuilder
          .AppendKeyClose()
          .AppendQuotationMark()
          .AppendKeyClose();
        return stringSourceBuilder.ToStringJson();
      }   
    }
    #endregion
    #region OperatorForString
    public static implicit operator string(TypeQL typeQL) => typeQL.ToStringJson();
    #endregion
  }
}
