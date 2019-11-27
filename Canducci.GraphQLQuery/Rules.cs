using Canducci.GraphQLQuery.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
namespace Canducci.GraphQLQuery
{
   public class Rules : List<IRule>, IDisposable
   {
      public string QuotationMark { get; } = "\"";
      public string Backslashes { get; } = "\\";
      public Rules()
      {        
         Add(new Rule(null, Format.FormatNull, GetFormatNullAction));
         Add(new Rule(typeof(ushort), Format.FormatNumber, GetFormatNumberAction));
         Add(new Rule(typeof(short), Format.FormatNumber, GetFormatNumberAction));
         Add(new Rule(typeof(uint), Format.FormatNumber, GetFormatNumberAction));
         Add(new Rule(typeof(int), Format.FormatNumber, GetFormatNumberAction));
         Add(new Rule(typeof(ulong), Format.FormatNumber, GetFormatNumberAction));
         Add(new Rule(typeof(long), Format.FormatNumber, GetFormatNumberAction));
         Add(new Rule(typeof(sbyte), Format.FormatNumber, GetFormatNumberAction));
         Add(new Rule(typeof(byte), Format.FormatNumber, GetFormatNumberAction));
         Add(new Rule(typeof(float), Format.FormatNumber, GetFormatNumberAction));
         Add(new Rule(typeof(decimal), Format.FormatNumber, GetFormatNumberAction));
         Add(new Rule(typeof(double), Format.FormatNumber, GetFormatNumberAction));
         Add(new Rule(typeof(string), Format.FormatText, GetFormatTextAction));
         Add(new Rule(typeof(Guid), Format.FormatText, GetFormatTextAction));
         Add(new Rule(typeof(char), Format.FormatText, GetFormatTextAction));
         Add(new Rule(typeof(DateTime), Format.FormatDateTime, GetFormatDateTimeAction));
         Add(new Rule(typeof(TimeSpan), Format.FormatTime, GetFormatTimeSpanAction));
         Add(new Rule(typeof(bool), Format.FormatDefault, GetFormatBoolAction));
         Add(new Rule(typeof(object), Format.FormatClass, GetFormatClassAction));
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
      public string GetFormatTextAction(object value)
      {
         return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}{4}", Backslashes, QuotationMark, value, Backslashes, QuotationMark);
      }
      public string GetFormatDateTimeAction(object value)
      {
         return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}{4}", Backslashes, QuotationMark, ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss"), Backslashes, QuotationMark);
      }
      public string GetFormatTimeSpanAction(object value)
      {
         return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}{4}", Backslashes, QuotationMark, ((TimeSpan)value).ToString(@"hh\:mm\:ss"), Backslashes, QuotationMark);
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
      public IRule Rule(Type type)
      {
         IRule rule = this.Where(x => x.TypeArgument == type).FirstOrDefault();
         if (rule == null && type.IsClass && typeof(string) != type)
         {
            rule = this.Where(x => x.Format == Format.FormatClass).FirstOrDefault();
         }
         if (rule == null)
         {
            throw new NullReferenceException("Type Error or Inexistent");
         }
         return rule;
      }
      public void Dispose()
      {         
         GC.SuppressFinalize(this);
      }

      #region Singleton
      private static Rules instance = null;
      private static readonly object syncLock = new object();
      public static Rules Instance
      {
         get
         {
            if (instance == null)
            {
               lock (syncLock)
               {
                  if (instance == null)
                  {
                     instance = new Rules();
                  }
               }
            }
            return instance;
         }
      }
      #endregion
   }
}