using System;
using System.Linq;
using System.Numerics;

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
      typeof(byte),
      typeof(sbyte)
    };
    public static Type[] NumberFloat = new Type[]
    {
      typeof(float),
      typeof(decimal),
      typeof(double)
    };    
    public static bool IsNullable(this Type type, out Type newType)
    {
      if (type.Name.IndexOf("Nullable") >= 0)
      {
        if (type.GenericTypeArguments?.Count() == 1)
        {
          newType = type.GenericTypeArguments[0];
          return true;
        }
      }
      newType = type;
      return false;
    }
    public static bool IsNumber(this Type type)
    {
      return NumberTypes.Contains(type);
    }
    public static bool IsBigInteger(this Type type)
    {
      return type == typeof(BigInteger);
    }
    public static bool IsNumberFloat(this Type type)   
    {
      return NumberFloat.Contains(type);
    }
    public static bool IsGuid(this Type type)
    {
      return type == typeof(Guid);
    }
    public static bool IsBool(this Type type)
    {
      return type == typeof(bool);
    }
    public static bool IsDateTime(this Type type)
    {
      return type == typeof(DateTime);
    }
    public static bool IsDateTimeOffset(this Type type)
    {
      return type == typeof(DateTimeOffset);
    }
    public static bool IsTimeSpan(this Type type)
    {
      return type == typeof(TimeSpan);
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
