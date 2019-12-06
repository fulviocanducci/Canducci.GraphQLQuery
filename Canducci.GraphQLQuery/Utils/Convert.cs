using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Canducci.GraphQLQuery.Utils
{
   public static class Convert
   {
      public static string ToJsonString<T>(T value)
      {
         var options = new JsonSerializerOptions
         {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
         };
         options.Converters.Add(new TimeSpanConverter());
         options.Converters.Add(new TimeSpanNullableConverter());
         return JsonSerializer.Serialize(value, options);
      }
   }

   public class TimeSpanConverter : JsonConverter<TimeSpan>
   {
      public static TimeSpanConverter Create() => new TimeSpanConverter();
      public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
      {
         if (TimeSpan.TryParse(reader.GetString(), out TimeSpan time))
         {
            return time;
         }
         return TimeSpan.MinValue;
      }
      public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
      {
         writer.WriteStringValue(value.ToString(Internals.Formats.Timespan));
      }
   }
   public class TimeSpanNullableConverter : JsonConverter<TimeSpan?>
   {
      public static TimeSpanConverter Create() => new TimeSpanConverter();
      public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
      {
         var str = reader.GetString();
         if (!string.IsNullOrEmpty(str))
         {
            if (TimeSpan.TryParse(str, out TimeSpan time))
            {
               return time;
            }
         }
         return null;
      }
      public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
      {
         if (value.HasValue)
         {
            writer.WriteStringValue(value?.ToString(Internals.Formats.Timespan));
         }
         else
         {
            writer.WriteNullValue();
         }
      }
   }
}
