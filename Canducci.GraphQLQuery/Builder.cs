using Canducci.GraphQLQuery.Extensions;
using Canducci.GraphQLQuery.Interfaces;
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
namespace Canducci.GraphQLQuery
{
   internal class Builder : IDisposable
   {
      protected internal StringBuilder StringSource { get; private set; }
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
         return AppendString("null");
      }
      internal Builder AppendDateTime(DateTime value, ITypeQLConfiguration configuration, ArgumentFormat argumentFormat = ArgumentFormat.Default)
      {
         argumentFormat = argumentFormat == ArgumentFormat.Default ? configuration.ArgumentFormat : argumentFormat;
         AppendBackslashes();
         AppendQuotationMark();
         switch (argumentFormat)
         {
            case ArgumentFormat.FormatDate:
               {
                  AppendString(value.ToString("yyyy-MM-dd"));
                  break;
               }
            case ArgumentFormat.FormatDateTime:
               {
                  AppendString(value.ToString("yyyy-MM-dd HH:mm:ss"));
                  break;
               }
            case ArgumentFormat.FormatTime:
               {
                  AppendString(value.ToString("HH\\:mm\\:ss"));
                  break;
               }
            default:
               {
                  AppendString(value.ToString("yyyy-MM-dd HH:mm:ss"));
                  break;
               }
         }
         AppendBackslashes();
         return AppendQuotationMark();
      }
      internal Builder AppendBool(bool value)
      {
         return AppendString(value.ToString().ToLower());
      }
      internal Builder AppendNumber<T>(T value)
      {
         return AppendString(string.Format(CultureInfo.InvariantCulture, "{0}", value));
      }
      internal Builder AppendGuid(Guid value)
      {
         AppendBackslashes();
         AppendQuotationMark();
         AppendString(value.ToString());
         AppendBackslashes();
         return AppendQuotationMark();
      }
      internal Builder AppendTimeSpan(TimeSpan value)
      {
         AppendBackslashes();
         AppendQuotationMark();
         AppendString(value.ToString("hh\\:mm\\:ss", CultureInfo.InvariantCulture));
         AppendBackslashes();
         return AppendQuotationMark();
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
         type.IsNullable(out Type localType);
         if (value == null)
         {
            AppendNull();
         }
         else if (localType.IsString())
         {
            AppendStringWithQuote(value.ToString());
         }
         else if (localType.IsClass() && !localType.IsString())
         {
            AppendClassArguments(value, configuration);
         }
         else if (localType.IsGuid())
         {
            if (Guid.TryParse(value.ToString(), out Guid valueGuid))
            {
               AppendGuid(valueGuid);
            }
         }
         else if (localType.IsNumber() || localType.IsNumberFloat() || localType.IsBigInteger())
         {
            AppendNumber(value);
         }
         else if (localType.IsBool())
         {
            if (bool.TryParse(value.ToString(), out bool valueBool))
            {
               AppendBool(valueBool);
            }
         }
         else if (localType.IsDateTime())
         {
            if (DateTime.TryParse(value.ToString(), out DateTime valueDateTime))
            {
               AppendDateTime(valueDateTime, configuration, argumentFormat);
            }
         }
         else if (localType.IsDateTimeOffset())
         {
            if (DateTimeOffset.TryParse(value.ToString(), out DateTimeOffset valueDateTimeOffSet))
            {
               AppendDateTime(valueDateTimeOffSet.DateTime, configuration, argumentFormat);
            }
         }
         else if (localType.IsTimeSpan())
         {
            if (TimeSpan.TryParse(value.ToString(), out TimeSpan timeSpan))
            {
               AppendTimeSpan(timeSpan);
            }
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
