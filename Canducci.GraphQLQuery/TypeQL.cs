using Canducci.GraphQLQuery.Interfaces;
using System;
using System.Linq;
using System.Text;
namespace Canducci.GraphQLQuery
{
  public class TypeQL : ITypeQL
  {
    #region Properties
    public ITypeQLConfiguration Configuration { get; private set; }
    public IQueryType QueryTypeItem { get; private set; }
    public Arguments Arguments { get; private set; }
    public Fields Fields { get; private set; }
    #endregion

    #region Constructors
    public TypeQL(string name, Fields fields, string alias = "", ITypeQLConfiguration configuration = null)
      : this(name, null, fields, alias, configuration) { }
    public TypeQL(string name, Arguments arguments, Fields fields, string alias = "", ITypeQLConfiguration configuration = null)
    {
      QueryTypeItem = new QueryType(name, alias);
      Arguments = arguments ?? new Arguments();
      Fields = fields ?? new Fields();
      Configuration = configuration ?? new TypeQLConfiguration(",", ArgumentFormat.FormatDateTime);
    }
    #endregion
    public string Render()
    {
      StringBuilder stringBuilder = new StringBuilder();
      //iniType
      RenderOpenKey(stringBuilder);
      RenderAspas(stringBuilder);
      RenderQuery(stringBuilder);
      RenderAspas(stringBuilder);
      RenderTwoPoints(stringBuilder);
      RenderAspas(stringBuilder);
      RenderOpenKey(stringBuilder);
      RenderItem(stringBuilder, QueryTypeItem);
      //endType
      //iniArgument      
      RenderArguments(stringBuilder);
      //endArgument      
      //initFields
      RenderFields(stringBuilder, Fields);
      //endFields
      RenderCloseKey(stringBuilder);
      RenderAspas(stringBuilder);
      RenderCloseKey(stringBuilder);
      //
      return stringBuilder.ToString();
    }

    #region RenderAll
    private void RenderString<T>(StringBuilder stringBuilder, T value)
      => stringBuilder.Append(value);
    private void RenderOpenBracket(StringBuilder stringBuilder)
      => stringBuilder.Append("(");
    private void RenderCloseBracket(StringBuilder stringBuilder)
      => stringBuilder.Append(")");
    private void RenderOpenKey(StringBuilder stringBuilder)
      => stringBuilder.Append("{");
    private void RenderCloseKey(StringBuilder stringBuilder)
      => stringBuilder.Append("}");
    private void RenderQuery(StringBuilder stringBuilder)
      => stringBuilder.Append("query");
    private void RenderAspas(StringBuilder stringBuilder)
      => stringBuilder.Append("\"");
    private void RenderBarra(StringBuilder stringBuilder)
      => stringBuilder.Append("\\");
    private void RenderTwoPoints(StringBuilder stringBuilder)
      => stringBuilder.Append(":");
    private void RenderSpace(StringBuilder stringBuilder)
      => stringBuilder.Append(Configuration.Separation);
    private void RenderItem(StringBuilder stringBuilder, IQueryType item)
      => stringBuilder.Append(string.IsNullOrEmpty(item.Alias) ? item.Name : $"{item.Alias}:{item.Name}");
    #endregion

    #region RenderArguments
    private void RenderArguments(StringBuilder stringBuilder)
    {
      if (Arguments.Count > 0)
      {
        RenderOpenBracket(stringBuilder);
        Arguments.ForEach(argument =>
        {
          if (argument.TypeValue == typeof(uint) ||
            argument.TypeValue == typeof(short) ||
            argument.TypeValue == typeof(int) ||
            argument.TypeValue == typeof(long) ||
            argument.TypeValue == typeof(float) ||
            argument.TypeValue == typeof(decimal) ||
            argument.TypeValue == typeof(double)) // integer, number
          {
            stringBuilder.Append(argument.Name + ":" + argument.Value);
          }
          else if (argument.TypeValue == typeof(DateTime)) // DateTime
          {
            var argumentFormat = argument.ArgumentFormat == ArgumentFormat.None
              ? Configuration.ArgumentFormat
              : argument.ArgumentFormat;
            RenderString(stringBuilder, argument.Name);
            RenderTwoPoints(stringBuilder);
            RenderBarra(stringBuilder);
            RenderAspas(stringBuilder);
            switch (argumentFormat)
            {
              case ArgumentFormat.FormatDate:
                {
                  RenderString(stringBuilder, ((DateTime)argument.Value).ToString("yyyy-MM-dd"));
                  break;
                }
              case ArgumentFormat.FormatDateTime:
                {
                  RenderString(stringBuilder, ((DateTime)argument.Value).ToString("yyyy-MM-dd HH:mm:ss"));
                  break;
                }
              case ArgumentFormat.FormatTime:
                {
                  RenderString(stringBuilder, ((DateTime)argument.Value).ToString("HH:mm:ss"));
                  break;
                }
              default:
                {
                  RenderString(stringBuilder, ((DateTime)argument.Value).ToString("yyyy-MM-dd HH:mm:ss"));
                  break;
                }
            }
            RenderBarra(stringBuilder);
            RenderAspas(stringBuilder);
          }
          else if (argument.TypeValue == typeof(bool)) // bool
          {
            RenderString(stringBuilder, argument.Name);
            RenderTwoPoints(stringBuilder);
            RenderString(stringBuilder, ((bool)argument.Value).ToString().ToLower());
          }
          else // string, text
          {
            RenderString(stringBuilder, argument.Name);
            RenderTwoPoints(stringBuilder);
            RenderBarra(stringBuilder);
            RenderAspas(stringBuilder);
            RenderString(stringBuilder, argument.Value);
            RenderBarra(stringBuilder);
            RenderAspas(stringBuilder);
          }
          if (!argument.Equals(Arguments.LastOrDefault())) RenderSpace(stringBuilder);
        });
        RenderCloseBracket(stringBuilder);
      }
    }
    #endregion

    #region RenderFields
    private void RenderFields(StringBuilder stringBuilder, Fields fields)
    {
      RenderOpenKey(stringBuilder);
      foreach (IField item in fields)
      {
        if (item.Fields?.Count > 0)
        {
          RenderItem(stringBuilder, item);
          RenderFields(stringBuilder, item.Fields);
        }
        else
        {
          RenderItem(stringBuilder, item);
        }
        if (item.Equals(fields.LastOrDefault()) == false)
        {
          RenderSpace(stringBuilder);
        }
      }
      RenderCloseKey(stringBuilder);
    }
    #endregion
  }
}
