using Canducci.GraphQLQuery.Interfaces;
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
namespace Canducci.GraphQLQuery.Extensions
{
  internal static class StringBuilderExtensions
  { 
    internal static StringBuilder AppendStringWithQuote(this StringBuilder stringBuilder, object value)
    {
      return stringBuilder
          .AppendBackslashes()
          .AppendQuotationMark()
          .Append(value)
          .AppendBackslashes()
          .AppendQuotationMark();
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
      return stringBuilder.Append(configuration.Separation == Separation.Comma ? "," : " ");
    }
    internal static StringBuilder AppendQueryType(this StringBuilder stringBuilder, IQueryType queryType)
    {
      return AppendField(stringBuilder, queryType.Name, queryType.Alias);
    }
    internal static StringBuilder AppendField(this StringBuilder stringBuilder, string name, string alias = null)
    {
      return string.IsNullOrEmpty(alias) ? stringBuilder.Append(name) : stringBuilder.Append(alias).AppendPoints().Append(name);
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
    internal static StringBuilder AppendDateTime(this StringBuilder stringBuilder, object value, ITypeQLConfiguration configuration, ArgumentFormat argumentFormat = ArgumentFormat.Default)
    {
      argumentFormat = argumentFormat == ArgumentFormat.Default ? configuration.ArgumentFormat : argumentFormat;
      stringBuilder.AppendBackslashes().AppendQuotationMark();
      switch (argumentFormat)
      {
        case ArgumentFormat.FormatDate:
          {
            stringBuilder.Append(((DateTime)value).ToString("yyyy-MM-dd"));
            break;
          }
        case ArgumentFormat.FormatDateTime:
          {
            stringBuilder.Append(((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss"));
            break;
          }
        case ArgumentFormat.FormatTime:
          {
            stringBuilder.Append(((DateTime)value).ToString("HH:mm:ss"));
            break;
          }
        default:
          {
            stringBuilder.Append(((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss"));
            break;
          }
      }
      return stringBuilder.AppendBackslashes().AppendQuotationMark();
    }
    internal static StringBuilder AppendBool(this StringBuilder stringBuilder, object value)
    {
      return stringBuilder.Append(((bool)value).ToString().ToLower());      
    }
    internal static StringBuilder AppendNumber(this StringBuilder stringBuilder, object value)
    { 
      return stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "{0}", value));
    }    
    internal static StringBuilder AppendScalarArguments(this StringBuilder stringBuilder, Arguments arguments, ITypeQLConfiguration configuration)
    {
      if (arguments == null || arguments?.Count <= 0)
      {
        return stringBuilder;
      }
      stringBuilder.AppendBracketOpen();
      IArgument argumentLast = arguments?.LastOrDefault();
      foreach (IArgument argument in arguments)
      {
        stringBuilder
          .Append(argument.Name)
          .AppendPoints()
          .AppendTypesOf(argument.TypeValue, argument.Value, configuration, argument.ArgumentFormat);
        if (!argument.Equals(argumentLast))
        {
          stringBuilder.AppendSeparation(configuration);
        }
      }
      return stringBuilder.AppendBracketClose();
    }    
    internal static StringBuilder AppendClassArguments(this StringBuilder stringBuilder, object value, ITypeQLConfiguration configuration, ArgumentFormat argumentFormat = ArgumentFormat.Default)
    {
      stringBuilder.AppendKeyOpen();
      PropertyInfo[] properties = value.GetType().GetProperties();
      PropertyInfo propertyLast = properties.LastOrDefault();
      foreach (PropertyInfo property in properties)
      {
        stringBuilder
          .Append(property.Name.ToLower())
          .AppendPoints()
          .AppendTypesOf(property.PropertyType, property.GetValue(value), configuration, argumentFormat);
        if (!property.Equals(propertyLast))
        {
          stringBuilder.AppendSeparation(configuration);
        }
      }
      stringBuilder.AppendKeyClose();
      return stringBuilder;
    }
    internal static StringBuilder AppendTypesOf(this StringBuilder stringBuilder, Type type, object value, ITypeQLConfiguration configuration, ArgumentFormat argumentFormat = ArgumentFormat.Default)
    {      
      if (type.IsNumber())
      {
        stringBuilder.Append(value);
      }
      else if (type.IsNumberFloat())
      {
        stringBuilder.AppendNumber(value);
      }
      else if (type.IsBool())
      {
        stringBuilder.AppendBool(value);
      }
      else if (type.IsDateTime())
      {
        stringBuilder.AppendDateTime(value, configuration, argumentFormat);
      }
      else if (type.IsString())
      {
        stringBuilder.AppendStringWithQuote(value);
      }
      else if (type.IsClass())
      {
        stringBuilder.AppendClassArguments(value, configuration);
      }
      return stringBuilder;
    }
  }
}
