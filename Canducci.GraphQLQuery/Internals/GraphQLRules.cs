using Canducci.GraphQLQuery.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Canducci.GraphQLQuery.Internals
{
   internal class GraphQLRules : List<IGraphQLRule>, IDisposable
   {
      public readonly GraphQLRulesExecute Execute;
      public GraphQLRules()
      {
         Execute = new GraphQLRulesExecute();
         Add(new GraphQLRule(typeof(ushort), Format.FormatNumber, Execute.GetFormatIntAction));
         Add(new GraphQLRule(typeof(short), Format.FormatNumber, Execute.GetFormatIntAction));
         Add(new GraphQLRule(typeof(uint), Format.FormatNumber, Execute.GetFormatIntAction));
         Add(new GraphQLRule(typeof(int), Format.FormatNumber, Execute.GetFormatIntAction));
         Add(new GraphQLRule(typeof(ulong), Format.FormatNumber, Execute.GetFormatIntAction));
         Add(new GraphQLRule(typeof(long), Format.FormatNumber, Execute.GetFormatIntAction));
         Add(new GraphQLRule(typeof(sbyte), Format.FormatNumber, Execute.GetFormatIntAction));
         Add(new GraphQLRule(typeof(byte), Format.FormatNumber, Execute.GetFormatIntAction));

         Add(new GraphQLRule(typeof(float), Format.FormatNumber, Execute.GetFormatNumberAction));
         Add(new GraphQLRule(typeof(double), Format.FormatNumber, Execute.GetFormatNumberAction));
         
         Add(new GraphQLRule(typeof(decimal), Format.FormatNumber, Execute.GetFormatDecimalAction));
         
         Add(new GraphQLRule(typeof(string), Format.FormatText, Execute.GetFormatStringAction));
         Add(new GraphQLRule(typeof(Guid), Format.FormatText, Execute.GetFormatStringAction));
         Add(new GraphQLRule(typeof(char), Format.FormatText, Execute.GetFormatStringAction));
         Add(new GraphQLRule(typeof(DateTime), Format.FormatDateTime, Execute.GetFormatDateTimeAction));
         Add(new GraphQLRule(typeof(TimeSpan), Format.FormatTime, Execute.GetFormatTimeSpanAction));
         Add(new GraphQLRule(typeof(bool), Format.FormatDefault, Execute.GetFormatBooleanAction));
         
         Add(new GraphQLRule(typeof(object), Format.FormatClass, Execute.GetFormatClassAction));
      }

      public IGraphQLRule Rule(Type type)
      {
         IGraphQLRule rule = this.Where(x => x.TypeArgument == type).FirstOrDefault();
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