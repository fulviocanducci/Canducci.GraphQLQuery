using Canducci.GraphQLQuery.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Canducci.GraphQLQuery.Internals
{
   internal class Rules : List<IRule>, IDisposable
   {
      public readonly RulesExecute Execute;
      public Rules()
      {
         Execute = new RulesExecute();

         Add(new Rule(typeof(Parameter), Format.FormatDefault, Execute.GetFormatParameterAction));
         Add(new Rule(null, Format.FormatNull, Execute.GetFormatNullAction));

         Add(new Rule(typeof(ushort), Format.FormatNumber, Execute.GetFormatNumberAction));
         Add(new Rule(typeof(short), Format.FormatNumber, Execute.GetFormatNumberAction));
         Add(new Rule(typeof(uint), Format.FormatNumber, Execute.GetFormatNumberAction));
         Add(new Rule(typeof(int), Format.FormatNumber, Execute.GetFormatNumberAction));
         Add(new Rule(typeof(ulong), Format.FormatNumber, Execute.GetFormatNumberAction));
         Add(new Rule(typeof(long), Format.FormatNumber, Execute.GetFormatNumberAction));
         Add(new Rule(typeof(sbyte), Format.FormatNumber, Execute.GetFormatNumberAction));
         Add(new Rule(typeof(byte), Format.FormatNumber, Execute.GetFormatNumberAction));

         Add(new Rule(typeof(float), Format.FormatNumber, Execute.GetFormatNumberAction));
         Add(new Rule(typeof(double), Format.FormatNumber, Execute.GetFormatNumberAction));

         Add(new Rule(typeof(decimal), Format.FormatNumber, Execute.GetFormatNumberAction));

         Add(new Rule(typeof(string), Format.FormatText, Execute.GetFormatTextAction));
         Add(new Rule(typeof(Guid), Format.FormatText, Execute.GetFormatTextAction));
         Add(new Rule(typeof(char), Format.FormatText, Execute.GetFormatTextAction));
         Add(new Rule(typeof(DateTime), Format.FormatDateTime, Execute.GetFormatDateTimeAction));
         Add(new Rule(typeof(TimeSpan), Format.FormatTime, Execute.GetFormatTimeSpanAction));
         Add(new Rule(typeof(bool), Format.FormatDefault, Execute.GetFormatBoolAction));

         Add(new Rule(typeof(object), Format.FormatClass, Execute.GetFormatClassAction));
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
         Execute?.Dispose();
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