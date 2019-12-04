using System;

namespace Canducci.GraphQLQuery.VariablesValueTypes
{
   public abstract class VariableValueDefault
   {
      public virtual string Value { get; set; }

      public static implicit operator VariableValueDefault(int value)
      {
         return new VariableValueDefaultInt(value);
      }
      public static implicit operator VariableValueDefault(bool value)
      {
         return new VariableValueDefaultBoolean(value);
      }
      public static implicit operator VariableValueDefault(DateTime value)
      {
         return new VariableValueDefaultDateTime(value);
      }
      public static implicit operator VariableValueDefault(decimal value)
      {
         return new VariableValueDefaultDecimal(value);
      }
      public static implicit operator VariableValueDefault(float value)
      {
         return new VariableValueDefaultFloat(value);
      }
      public static implicit operator VariableValueDefault(string value)
      {
         return new VariableValueDefaultString(value);
      }
      public static implicit operator VariableValueDefault(VariableValue value)
      {
         return new VariableValueDefaultNull();
      }
   }
}
