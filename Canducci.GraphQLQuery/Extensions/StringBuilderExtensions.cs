using Canducci.GraphQLQuery.Interfaces;
using System;
using System.Linq;
using System.Text;
namespace Canducci.GraphQLQuery.Extensions
{
  internal static class StringBuilderExtensions
  {
    internal static StringBuilder AppendString(this StringBuilder stringBuilder, string value)
    {
      return stringBuilder.Append(value);
    }
    internal static StringBuilder AppendBracketOpen(this StringBuilder stringBuilder)
    {
      return stringBuilder.Append("(");
    }
    internal static StringBuilder AppendBracketClose(this StringBuilder stringBuilder)
    {
      return stringBuilder.Append(")");
    }
    internal static StringBuilder AppendKeyOpen(this StringBuilder stringBuilder)
    {
      return stringBuilder.Append("{");
    }
    internal static StringBuilder AppendKeyClose(this StringBuilder stringBuilder)
    {
      return stringBuilder.Append("}");
    }
    internal static StringBuilder AppendQuery(this StringBuilder stringBuilder)
    {
      return stringBuilder.Append("query");
    }
    internal static StringBuilder AppendQuotationMark(this StringBuilder stringBuilder)
    {
      return stringBuilder.Append("\"");
    }
    internal static StringBuilder AppendBackslashes(this StringBuilder stringBuilder)
    {
      return stringBuilder.Append("\\");
    }
    internal static StringBuilder AppendPoints(this StringBuilder stringBuilder)
    {
      return stringBuilder.Append(":");
    }
    internal static StringBuilder AppendSeparation(this StringBuilder stringBuilder, ITypeQLConfiguration configuration)
    {
      return stringBuilder.Append(configuration.Separation);
    }
    internal static StringBuilder AppendQueryType(this StringBuilder stringBuilder, IQueryType queryType)
    {
      return AppendField(stringBuilder, queryType.Name, queryType.Alias);
    }
    internal static StringBuilder AppendField(this StringBuilder stringBuilder, string name, string alias)
    {
      return string.IsNullOrEmpty(alias)
        ? stringBuilder.Append(name)
        : stringBuilder.AppendString(alias).AppendPoints().AppendString(name);
    }
    internal static StringBuilder AppendArguments(this StringBuilder stringBuilder, Arguments arguments, ITypeQLConfiguration configuration)
    {
      if (arguments == null || arguments?.Count <= 0)
      {
        return stringBuilder;
      }
      stringBuilder.AppendBracketOpen();
      arguments.ForEach(argument =>
      {
        stringBuilder
          .AppendString(argument.Name)
          .AppendPoints();
        if (argument.TypeValue == typeof(uint) ||
          argument.TypeValue == typeof(short) ||
          argument.TypeValue == typeof(int) ||
          argument.TypeValue == typeof(long) ||
          argument.TypeValue == typeof(float) ||
          argument.TypeValue == typeof(decimal) ||
          argument.TypeValue == typeof(double)) // integer, number
        {
          stringBuilder.Append(argument.Value);
        }
        else if (argument.TypeValue == typeof(bool)) // bool
        {
          stringBuilder.AppendString(((bool)argument.Value).ToString().ToLower());
        }
        else if (argument.TypeValue == typeof(DateTime)) // DateTime
        {
          ArgumentFormat argumentFormat = argument.ArgumentFormat == ArgumentFormat.Default
            ? configuration.ArgumentFormat
            : argument.ArgumentFormat;
          stringBuilder
          .AppendBackslashes()
          .AppendQuotationMark();
          switch (argumentFormat)
          {
            case ArgumentFormat.FormatDate:
              {
                stringBuilder.AppendString(((DateTime)argument.Value).ToString("yyyy-MM-dd"));
                break;
              }
            case ArgumentFormat.FormatDateTime:
              {
                stringBuilder.AppendString(((DateTime)argument.Value).ToString("yyyy-MM-dd HH:mm:ss"));
                break;
              }
            case ArgumentFormat.FormatTime:
              {
                stringBuilder.AppendString(((DateTime)argument.Value).ToString("HH:mm:ss"));
                break;
              }
            default:
              {
                stringBuilder.AppendString(((DateTime)argument.Value).ToString("yyyy-MM-dd HH:mm:ss"));
                break;
              }
          }
          stringBuilder
          .AppendBackslashes()
          .AppendQuotationMark();
        }
        else if (argument.TypeValue == typeof(string)
          || argument.TypeValue == typeof(char))
        { // string texto char        
          stringBuilder
          .AppendBackslashes()
          .AppendQuotationMark()
          .Append(argument.Value)
          .AppendBackslashes()
          .AppendQuotationMark();
        }
        else if (argument.TypeValue.IsClass)
        {
          stringBuilder.AppendKeyOpen();
          foreach (var property in argument.Value.GetType().GetProperties())
          {
            stringBuilder.Append(property.Name.ToLower())
              .AppendPoints()
              .AppendBackslashes()
              .AppendQuotationMark()
              .Append(property.GetValue(argument.Value))
              .AppendBackslashes()
              .AppendQuotationMark();
            if (!property.Equals(argument.Value.GetType().GetProperties().LastOrDefault()))
            {
              stringBuilder.AppendSeparation(configuration);
            }
          }
          stringBuilder.AppendKeyClose();
        }
        if (!argument.Equals(arguments.LastOrDefault()))
        {
          stringBuilder.AppendSeparation(configuration);
        }
      });
      stringBuilder.AppendBracketClose();
      return stringBuilder;
    }
    internal static StringBuilder AppendFields(this StringBuilder stringBuilder, Fields fields, ITypeQLConfiguration configuration)
    {
      if (fields == null || fields?.Count <= 0)
      {
        return stringBuilder;
      }
      void AppendFieldsInternal(StringBuilder _stringBuilder, Fields _fields, ITypeQLConfiguration _configuration)
      {
        _stringBuilder.AppendKeyOpen();
        foreach (IField _field in _fields)
        {
          stringBuilder.AppendField(_field.Name, _field.Alias);
          if (_field is IFieldRelationship item)
          {
            AppendFieldsInternal(stringBuilder, item.Fields, _configuration);
          }
          if (_field.Equals(_fields.LastOrDefault()) == false)
          {
            _stringBuilder.AppendSeparation(configuration);
          }
        };
        _stringBuilder.AppendKeyClose();
      }
      AppendFieldsInternal(stringBuilder, fields, configuration);
      return stringBuilder;
    }
  }
}
