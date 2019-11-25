using Canducci.GraphQLQuery.Interfaces;
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Canducci.GraphQLQuery.Extensions;
namespace Canducci.GraphQLQuery
{
  internal class Builder : IDisposable
  {
    internal StringBuilder StringSource { get; private set; }
    internal string ToStringJson() => StringSource.ToString();
    public Builder()
    {
      StringSource = new StringBuilder();
    }    

    #region AppendTypeScalar
    internal Builder AppendString(string value)
    {
      StringSource.Append(value);
      return this;
    }
    internal Builder AppendNull()
    {
      StringSource.Append("null");
      return this;
    }
    internal Builder AppendDateTime(object value, ITypeQLConfiguration configuration, ArgumentFormat argumentFormat = ArgumentFormat.Default)
    {
      argumentFormat = argumentFormat == ArgumentFormat.Default ? configuration.ArgumentFormat : argumentFormat;
      AppendBackslashes().AppendQuotationMark();
      switch (argumentFormat)
      {
        case ArgumentFormat.FormatDate:
          {
            AppendString(((DateTime)value).ToString("yyyy-MM-dd"));
            break;
          }
        case ArgumentFormat.FormatDateTime:
          {
            AppendString(((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss"));
            break;
          }
        case ArgumentFormat.FormatTime:
          {
            AppendString(((DateTime)value).ToString("HH:mm:ss"));
            break;
          }
        default:
          {
            AppendString(((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss"));
            break;
          }
      }
      return AppendBackslashes().AppendQuotationMark();
    }
    internal Builder AppendBool(bool value)
    {
      return AppendString(value.ToString().ToLower());
    }
    internal Builder AppendNumber<T>(T value)
    {
      return AppendString(string.Format(CultureInfo.InvariantCulture, "{0}", value));      
    }
    #endregion

    #region AppendSignal
    internal Builder AppendStringWithQuote(string value)
    {
      return AppendBackslashes()
       .AppendQuotationMark()
         .AppendString(value)
         .AppendBackslashes()
         .AppendQuotationMark();
    }
    internal Builder AppendBracketOpen()
    {
      return AppendString("(");
    }
    internal Builder AppendBracketClose()
    {
      return AppendString(")");
    }
    internal Builder AppendKeyOpen()
    {
      return AppendString("{");
    }
    internal Builder AppendKeyClose()
    {
      return AppendString("}");
    }
    internal Builder AppendQuery()
    {
      return AppendString("query");
    }
    internal Builder AppendQuotationMark()
    {
      return AppendString("\"");
    }
    internal Builder AppendBackslashes()
    {
      return AppendString("\\");
    }
    internal Builder AppendPoints()
    {
      return AppendString(":");
    }
    internal Builder AppendSeparation(ITypeQLConfiguration configuration)
    {
      return AppendString(configuration.Separation == Separation.Comma ? "," : " ");      
    }
    #endregion

    #region AppendComplexType
    internal Builder AppendQueryType(IQueryType queryType)
    {
      return AppendField(queryType.Name, queryType.Alias);
    }
    internal Builder AppendField(string name, string alias = null)
    {
      return string.IsNullOrEmpty(alias)
        ? AppendString(name) 
        : AppendString(alias).AppendPoints().AppendString(name);
    }
    internal Builder AppendFieldsInternal(Fields fields, ITypeQLConfiguration configuration)
    {
      AppendKeyOpen();
      foreach (IField field in fields)
      {
        AppendField(field.Name, field.Alias);
        if (field is IFieldRelationship item)
        {
          AppendFieldsInternal(item.Fields, configuration);
        }
        if (!field.Equals(fields.LastOrDefault()))
        {
          AppendSeparation(configuration);
        }
      };
      return AppendKeyClose();
    }
    internal Builder AppendFields(Fields fields, ITypeQLConfiguration configuration)
    {
      if (fields == null || fields?.Count <= 0)
      {
        return this;
      }      
      return AppendFieldsInternal(fields, configuration);
    }
    internal Builder AppendScalarArguments(Arguments arguments, ITypeQLConfiguration configuration)
    {
      if (arguments == null || arguments?.Count <= 0)
      {
        return this;
      }
      AppendBracketOpen();
      IArgument argumentLast = arguments?.LastOrDefault();
      foreach (IArgument argument in arguments)
      {
        AppendString(argument.Name)
        .AppendPoints()
        .AppendTypesOf(argument.TypeValue, argument.Value, configuration, argument.ArgumentFormat);
        if (!argument.Equals(argumentLast))
        {
          AppendSeparation(configuration);
        }
      }
      return AppendBracketClose();
    }
    internal Builder AppendClassArguments(object value, ITypeQLConfiguration configuration, ArgumentFormat argumentFormat = ArgumentFormat.Default)
    {
      AppendKeyOpen();
      PropertyInfo[] properties = value.GetType().GetProperties();
      PropertyInfo propertyLast = properties.LastOrDefault();
      foreach (PropertyInfo property in properties)
      {
          AppendString(property.Name.ToLower())
          .AppendPoints()
          .AppendTypesOf(property.PropertyType, property.GetValue(value), configuration, argumentFormat);
        if (!property.Equals(propertyLast))
        {
          AppendSeparation(configuration);
        }
      }
      return AppendKeyClose();
    }
    internal Builder AppendTypesOf(Type type, object value, ITypeQLConfiguration configuration, ArgumentFormat argumentFormat = ArgumentFormat.Default)
    {
      if (value == null)
      {
        AppendNull();
      }
      else if (type.IsNumber() || type.IsNumberFloat())
      {
        AppendNumber(value);
      }
      else if (type.IsBool())
      {
        if (bool.TryParse(value.ToString(), out bool boolValue))
        {
          AppendBool(boolValue);
        }
      }
      else if (type.IsDateTime())
      {
        if (DateTime.TryParse(value.ToString(), out DateTime valueDateTime))
        {
          AppendDateTime(valueDateTime, configuration, argumentFormat);
        }
      }
      else if (type.IsString())
      {
        AppendStringWithQuote(value.ToString());
      }
      else if (type.IsClass())
      {
        AppendClassArguments(value, configuration);
      }
      return this;
    }
    #endregion

    #region IDispose
    public void Dispose()
    {
      StringSource = null;
      GC.SuppressFinalize(this);
    }
    #endregion
  }
}
