using System;
using System.Linq;

namespace Canducci.GraphQLQuery.Extensions
{
  internal static class TypeExtensions
  {
    public static Type[] NumberTypes = new Type[]
    {
      typeof(ushort),
      typeof(short),
      typeof(uint),
      typeof(int),
      typeof(ulong),
      typeof(long),      
    };
    public static Type[] NumberFloat = new Type[]
    {
      typeof(float),
      typeof(decimal),
      typeof(double)
    };    
    public static bool IsNumber(this Type type)
    {
      return NumberTypes.Contains(type);
    }
    public static bool IsNumberFloat(this Type type)   
    {
      return NumberFloat.Contains(type);
    }
    public static bool IsBool(this Type type)
    {
      return type == typeof(bool);
    }
    public static bool IsDateTime(this Type type)
    {
      return type == typeof(DateTime);
    }
    public static bool IsString(this Type type)
    {
      return type == typeof(string) || type == typeof(char);
    }
    public static bool IsClass(this Type type)
    {
      return type.IsClass;
    }
  }
}
