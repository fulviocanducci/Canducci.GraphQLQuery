using Canducci.GraphQLQuery.Interfaces;
using Canducci.GraphQLQuery.Internals;
using System;
using System.Globalization;
namespace Canducci.GraphQLQuery
{
   public class Argument : IArgument
   {
      public string Name { get; }
      public object Value { get; }
      internal IRule Rule { get; }
      public string KeyValue
      {
         get
         {
            return GetKeyValue();
         }
      }
      public Argument(string name, object value)
      {
         Name = name;
         Value = value;
         Rule = Rules.Instance.Rule(value?.GetType());
      }
      public Argument(Parameter value)
      {
         Name = value.Name;
         Value = value;
         Rule = Rules.Instance.Rule(typeof(Parameter));
      }

      //public Argument(ID id)
      //{
      //   Name = id.Name;
      //   Value = id;
      //   Rule = Rules.Instance.Rule(typeof(ID));
      //}
      //public Argument(Any any)
      //{
      //   Name = any.Name;
      //   Value = any;
      //   Rule = Rules.Instance.Rule(typeof(Any));
      //}
      //public Argument(string name, Uri url)
      //{
      //   Name = name;
      //   Value = url;
      //   Rule = Rules.Instance.Rule(typeof(Uri));
      //}

      public string Convert()
      {
         return Rule.Convert(Value);
      }
      private string GetKeyValue()
      {
         if (Rule.Format == Format.FormatClass)
         {
            return string.Format(CultureInfo.InvariantCulture,
               "{0}:{1}{2}{3}",
               Name,
               Signals.BraceOpen,
               Convert(),
               Signals.BraceClose
            );
         }
         return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", Name, Convert());
      }
   }
}