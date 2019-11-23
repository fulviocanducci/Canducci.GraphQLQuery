using Canducci.GraphQLQuery.Interfaces;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
namespace Canducci.GraphQLQuery.Extensions
{
  internal static class TypeExtensions
  {
    public static Type[] NumberTypes = new Type[]
    {
      typeof(ushort),
      typeof(short),
      typeof(uint),
      typeof(int),
      typeof(ulong),
      typeof(long),      
    };
    public static Type[] NumberFloat = new Type[]
    {
      typeof(float),
      typeof(decimal),
      typeof(double)
    };
    public static bool IsNumber(this IArgument argument)
    {
      return NumberTypes.Contains(argument.TypeValue);
    }
    public static bool IsNumberFloat(this IArgument argument)
    {
      return NumberFloat.Contains(argument.TypeValue);
    }
    public static bool IsBool(this IArgument argument)
    {
      return argument.TypeValue == typeof(bool);
    }
    public static bool IsDateTime(this IArgument argument)
    {
      return argument.TypeValue == typeof(DateTime);
    }
    public static bool IsString(this IArgument argument)
    {
      return argument.TypeValue == typeof(string) || argument.TypeValue == typeof(char);
    }
    public static bool IsClass(this IArgument argument)
    {
      return argument.TypeValue.IsClass;
    }
    public static bool IsNumber(this Type type)
    {
      return NumberTypes.Contains(type);
    }
    public static bool IsNumberFloat(this Type type)      
    {
      return NumberFloat.Contains(type);
    }
    public static bool IsBool(this Type type)
    {
      return type == typeof(bool);
    }
    public static bool IsDateTime(this Type type)
    {
      return type == typeof(DateTime);
    }
    public static bool IsString(this Type type)
    {
      return type == typeof(string) || type == typeof(char);
    }
  }
  internal static class StringBuilderExtensions
  {
    internal static StringBuilder AppendString(this StringBuilder stringBuilder, string value)
    {
      return stringBuilder.Append(value);
    }
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
    internal static StringBuilder AppendDateTime(this StringBuilder stringBuilder, IArgument argument, ITypeQLConfiguration configuration, object value = null)
    {
      value = value ?? argument.Value;
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
            stringBuilder.AppendString(((DateTime)value).ToString("yyyy-MM-dd"));
            break;
          }
        case ArgumentFormat.FormatDateTime:
          {
            stringBuilder.AppendString(((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss"));
            break;
          }
        case ArgumentFormat.FormatTime:
          {
            stringBuilder.AppendString(((DateTime)value).ToString("HH:mm:ss"));
            break;
          }
        default:
          {
            stringBuilder.AppendString(((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss"));
            break;
          }
      }
      stringBuilder
      .AppendBackslashes()
      .AppendQuotationMark();
      return stringBuilder;
    }
    internal static StringBuilder AppendBool(this StringBuilder stringBuilder, object value)
    {
      stringBuilder.AppendString(((bool)value).ToString().ToLower());
      return stringBuilder;
    }
    internal static StringBuilder AppendNumber(this StringBuilder stringBuilder, object value)
    { 
      return stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "{0}", value));
    }
    #region Arguments_Group_Special
    internal static StringBuilder AppendArguments(this StringBuilder stringBuilder, Arguments arguments, ITypeQLConfiguration configuration)
    {
      if (arguments == null || arguments?.Count <= 0)
      {
        return stringBuilder;
      }
      stringBuilder.AppendBracketOpen();
      foreach (IArgument argument in arguments)
      {
        stringBuilder
          .AppendString(argument.Name)
          .AppendPoints();
        if (argument.IsNumber())
        {
          stringBuilder.Append(argument.Value);
        }
        else if (argument.IsNumberFloat())
        {
          stringBuilder.AppendNumber(argument.Value);
        }
        else if (argument.IsBool())
        {
          stringBuilder.AppendBool(argument.Value);
        }
        else if (argument.IsDateTime())
        {
          stringBuilder.AppendDateTime(argument, configuration);
        }
        else if (argument.IsString())
        {
          stringBuilder.AppendStringWithQuote(argument.Value);
        }
        else if (argument.IsClass())
        {
          stringBuilder.AppendClassArguments(argument, configuration);
        }
        if (!argument.Equals(arguments.LastOrDefault()))
        {
          stringBuilder.AppendSeparation(configuration);
        }
      }
      stringBuilder.AppendBracketClose();
      return stringBuilder;
    }
    internal static StringBuilder AppendClassArguments(this StringBuilder stringBuilder, IArgument argument, ITypeQLConfiguration configuration)
    {
      stringBuilder.AppendKeyOpen();
      foreach (var property in argument.Value.GetType().GetProperties())
      {
        stringBuilder.AppendString(property.Name.ToLower())
          .AppendPoints();
        if (property.PropertyType.IsNumber())
        {
          stringBuilder.Append(property.GetValue(argument.Value));
        }
        else if (property.PropertyType.IsNumberFloat())
        {
          stringBuilder.AppendNumber(property.GetValue(argument.Value));
        }
        else if (property.PropertyType.IsBool())
        {
          stringBuilder.AppendBool(property.GetValue(argument.Value));
        }
        else if (property.PropertyType.IsDateTime())
        {
          stringBuilder.AppendDateTime(argument, configuration, property.GetValue(argument.Value));
        }
        else if (property.PropertyType.IsString())
        {
          stringBuilder.AppendStringWithQuote(property.GetValue(argument.Value));
        }
        if (!property.Equals(argument.Value.GetType().GetProperties().LastOrDefault()))
        {
          stringBuilder.AppendSeparation(configuration);
        }
      }
      stringBuilder.AppendKeyClose();
      return stringBuilder;
    }
    #endregion
  }
}
