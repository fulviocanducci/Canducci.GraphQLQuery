using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Canducci.GraphQLQuery.MSTest")]
namespace Canducci.GraphQLQuery.Internals
{   
   internal class GraphQLRulesExecute : IDisposable
   {
      public string GetFormatIntAction()
      {
         return "Int";
      }

      public string GetFormatFloatAction()
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

      public string GetFormatIDAction()
      {
         return "ID";
      }

      public string GetFormatByteAction()
      {
         return "Byte";
      }

      public string GetFormatShortAction()
      {
         return "Short";
      }

      public string GetFormatLongAction()
      {
         return "Long";
      }

      public string GetFormatDecimalAction()
      {
         return "Decimal";
      }

      public string GetFormatUrlAction()
      {
         return "Url";
      }

      public string GetFormatDateTimeAction()
      {
         return "DateTime";
      }

      public string GetFormatDateAction()
      {
         return "Date";
      }

      public string GetFormatGuidAction()
      {
         return "Uuid";
      }
      public string GetFormatTimeSpanAction()
      {
         return "TimeSpan";
      }

      public string GetFormatAnyAction()
      {
         return "Any";
      }

      public string GetFormatClassAction()
      {
         return null;
      }

      public void Dispose()
      {
         GC.SuppressFinalize(this);
      }
   }
}

//Ref. https://hotchocolate.io/docs/custom-scalar-types