using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.Extensions;
using System.Text;
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
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder
          .AppendKeyOpen()
          .AppendQuotationMark()
          .AppendQuery()
          .AppendQuotationMark()
          .AppendPoints()
          .AppendQuotationMark()
          .AppendKeyOpen();
      foreach (IQueryType item in QueryTypes)
      {
        stringBuilder
          .AppendQueryType(item)
          .AppendScalarArguments(item.Arguments, Configuration)
          .AppendFields(item.Fields, Configuration);
      }
      stringBuilder
        .AppendKeyClose()
        .AppendQuotationMark()
        .AppendKeyClose();      
      return stringBuilder.ToString();      
    }
    #endregion
    #region OperatorForString
    public static implicit operator string(TypeQL typeQL) => typeQL.ToStringJson();
    #endregion
  }
}
