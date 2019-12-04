using System.Text.Json;

namespace Canducci.GraphQLQuery.Utils
{
   public static class Convert
   {
      public static string ToJsonString<T>(T value)
      {
         return JsonSerializer.Serialize(value, new JsonSerializerOptions
         {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
         }); ;
      }
   }
}
