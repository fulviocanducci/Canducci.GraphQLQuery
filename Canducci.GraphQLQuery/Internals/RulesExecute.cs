using System;
using System.Globalization;
using System.Reflection;
using System.Text;
namespace Canducci.GraphQLQuery.Internals
{
   internal sealed class RulesExecute: IDisposable
   {
      public string GetFormatUrlAction(object value)
      {
         if (value != null && value is Uri _)
         {
            return string.Format(CultureInfo.InvariantCulture, "${0}", value);
         }
         return string.Empty;
      }
      public string GetFormatIDAction(object value)
      {
         if (value != null && value is ID id)
         {
            return string.Format(CultureInfo.InvariantCulture, "${0}", id.Name);
         }
         return string.Empty;
      }

      public string GetFormatAnyAction(object value)
      {
         if (value != null && value is Any any)
         {
            return string.Format(CultureInfo.InvariantCulture, "${0}", any.Name);
         }
         return string.Empty;

      }

      public string GetFormatParameterAction(object value)
      {
         if (value != null && value is Parameter parameter)
         {
            return string.Format(CultureInfo.InvariantCulture, "${0}", parameter.Variable ?? parameter.Name);
         }
         return string.Empty;

      }

      public string GetFormatNullAction(object value = null)
      {
         if (value == null)
         {
            return string.Format(CultureInfo.InvariantCulture, "{0}", "null");
         }
         return string.Format(CultureInfo.InvariantCulture, "{0}", value);
      }

      public string GetFormatNumberAction(object value)
      {
         return string.Format(CultureInfo.InvariantCulture, "{0}", value);
      }

      public string GetFormatBoolAction(object value)
      {
         return string.Format(CultureInfo.InvariantCulture, "{0}", value.ToString().ToLowerInvariant());
      }

      public string GetFormatStringAction(object value)
      {
         return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}{4}", Signals.Backslashes, Signals.QuotationMark, value, Signals.Backslashes, Signals.QuotationMark);
      }

      public string GetFormatCharAction(object value)
      {
         return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}{4}", Signals.Backslashes, Signals.QuotationMark, value, Signals.Backslashes, Signals.QuotationMark);
      }

      public string GetFormatGuidAction(object value)
      {
         return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}{4}", Signals.Backslashes, Signals.QuotationMark, value.ToString(), Signals.Backslashes, Signals.QuotationMark);
      }

      public string GetFormatDateTimeAction(object value)
      {         
         return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}{4}", 
            Signals.Backslashes, 
            Signals.QuotationMark, 
            ((DateTime)value).ToString(Formats.DateTime), 
            Signals.Backslashes, 
            Signals.QuotationMark);
      }

      public string GetFormatTimeSpanAction(object value)
      {
         return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}{4}", 
            Signals.Backslashes, 
            Signals.QuotationMark,
            ((TimeSpan)value).ToString(Formats.Timespan), 
            Signals.Backslashes, 
            Signals.QuotationMark);
      }

      public string GetFormatClassAction(object value)
      {
         Arguments arguments = new Arguments();
         Type type = value.GetType();
         if (type.IsClass && typeof(string) != type)
         {
            foreach (PropertyInfo property in value.GetType().GetProperties())
            {
               arguments.Add(new Argument(property.Name.ToLowerInvariant(), property.GetValue(value)));
            }
         }
         StringBuilder str = new StringBuilder();
         arguments.AppendStringBuilder(str);
         return str.ToString();
      }

      public void Dispose()
      {
         GC.SuppressFinalize(this);
      }
   }
}
