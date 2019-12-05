using System;
namespace Canducci.GraphQLQuery.Internals
{
   internal class GraphQLRulesExecute : IDisposable
   {
      public string GetFormatClassAction()
      {
         return null;
      }
      public string GetFormatIntAction()
      {
         return "Int";
      }
      public string GetFormatDecimalAction()
      {
         return "Decimal";
      }
      public string GetFormatNumberAction()
      {
         return "Float";
      }
      public string GetFormatStringAction()
      {
         return "String";
      }
      public string GetFormatBooleanAction()
      {
         return "Boolean";
      }
      public string GetFormatDateAction()
      {
         return "Date";
      }
      public string GetFormatDateTimeAction()
      {
         return "DateTime";
      }
      public string GetFormatTimeSpanAction()
      {
         return "TimeSpan";
      }
      public string GetFormatIDAction()
      {
         return "ID";
      }
      public void Dispose()
      {
         GC.SuppressFinalize(this);
      }
   }
}