using System;
namespace Canducci.GraphQLQuery.Internals
{
   internal class GraphQLRulesExecute : IDisposable
   {
      public string GetFormatIntAction(object value = null)
      {
         return "Int";
      }
      public string GetFormatNumberAction(object value = null)
      {
         return "Float";
      }
      public string GetFormatStringAction(object value = null)
      {
         return "Float";
      }
      public string GetFormatBooleanAction(object value = null)
      {
         return "Boolean";
      }

      public string GetFormatIDAction(object value = null)
      {
         return "ID";
      }
      public void Dispose()
      {
         GC.SuppressFinalize(this);
      }
   }
}