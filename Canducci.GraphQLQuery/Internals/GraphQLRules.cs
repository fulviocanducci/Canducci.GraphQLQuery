using Canducci.GraphQLQuery.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Canducci.GraphQLQuery.Internals
{
   internal class GraphQLRules : List<IRule>, IDisposable
   {
      public GraphQLRulesExecute Execute = new GraphQLRulesExecute();
      public GraphQLRules()
      {         
         Add(new Rule(typeof(ushort), Format.FormatNumber, Execute.GetFormatIntAction));
         Add(new Rule(typeof(short), Format.FormatNumber, Execute.GetFormatIntAction));
         Add(new Rule(typeof(uint), Format.FormatNumber, Execute.GetFormatIntAction));
         Add(new Rule(typeof(int), Format.FormatNumber, Execute.GetFormatIntAction));
         Add(new Rule(typeof(ulong), Format.FormatNumber, Execute.GetFormatIntAction));
         Add(new Rule(typeof(long), Format.FormatNumber, Execute.GetFormatIntAction));
         Add(new Rule(typeof(sbyte), Format.FormatNumber, Execute.GetFormatIntAction));
         Add(new Rule(typeof(byte), Format.FormatNumber, Execute.GetFormatIntAction));
         Add(new Rule(typeof(float), Format.FormatNumber, Execute.GetFormatNumberAction));
         Add(new Rule(typeof(decimal), Format.FormatNumber, Execute.GetFormatNumberAction));
         Add(new Rule(typeof(double), Format.FormatNumber, Execute.GetFormatNumberAction));
         Add(new Rule(typeof(string), Format.FormatText, Execute.GetFormatStringAction));
         Add(new Rule(typeof(Guid), Format.FormatText, Execute.GetFormatStringAction));
         Add(new Rule(typeof(char), Format.FormatText, Execute.GetFormatStringAction));
         Add(new Rule(typeof(DateTime), Format.FormatDateTime, Execute.GetFormatStringAction));
         Add(new Rule(typeof(TimeSpan), Format.FormatTime, Execute.GetFormatStringAction));
         Add(new Rule(typeof(bool), Format.FormatDefault, Execute.GetFormatBooleanAction));         
      }

      public IRule Rule(Type type)
      {
         IRule rule = this.Where(x => x.TypeArgument == type).FirstOrDefault();         
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
      private static GraphQLRules instance = null;
      private static readonly object syncLock = new object();
      public static GraphQLRules Instance
      {
         get
         {
            if (instance == null)
            {
               lock (syncLock)
               {
                  if (instance == null)
                  {
                     instance = new GraphQLRules();
                  }
               }
            }
            return instance;
         }
      }
      #endregion
   }
}