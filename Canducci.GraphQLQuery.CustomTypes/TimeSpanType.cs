using HotChocolate.Language;
using HotChocolate.Types;
using System;

namespace Canducci.GraphQLQuery.CustomTypes
{
   public class TimeSpanType : ScalarType, INullableType
   {
      public override Type ClrType => typeof(TimeSpan);      
      public TimeSpanType() :
         base("TimeSpan")
      {
         Description = "TimeSpan Type HotChocolate";         
      }

      public override bool IsInstanceOfType(IValueNode literal)
      {
         if (literal == null)
         {
            return true;
         }

         return literal is StringValueNode
            || literal is TimeSpanValueNode
            || literal is NullValueNode;
      }

      public override object ParseLiteral(IValueNode literal)
      {
         if (literal == null)
         {
            throw new ArgumentNullException(nameof(literal));
         }

         if (literal is StringValueNode stringLiteral)
         {
            if (TimeSpan.TryParse(stringLiteral.Value, out TimeSpan valueTimeSpan))
            {
               return valueTimeSpan;
            }
            return null;
         }

         if (literal is TimeSpanValueNode timeSpanLiteral)
         {
            return timeSpanLiteral.Value;
         }

         if (literal is NullValueNode)
         {
            return null;
         }

         throw new ArgumentException("The TimeSpan type can only parse string literals.", nameof(literal));
      }

      public override IValueNode ParseValue(object value)
      {
         if (value == null)
         {
            return new NullValueNode(null);
         }

         if (value is string valueString)
         {
            if (string.IsNullOrEmpty(valueString))
            {
               return new NullValueNode(null);
            }
            if (TimeSpan.TryParse(value.ToString(), out TimeSpan valueStringTimeSpan))
            {
               return new TimeSpanValueNode(NodeKind.ScalarTypeDefinition, valueStringTimeSpan, null);
            }
         }

         if (value is TimeSpan valueTimeSpan)
         {
            return new TimeSpanValueNode(NodeKind.ScalarTypeDefinition, valueTimeSpan, null);
         }

         throw new ArgumentException("The TimeSpan type can only parse value.", "value");
      }

      public override object Serialize(object value)
      {
         if (value != null)
         {
            if (value is TimeSpan s)
            {
               return s;
            }
         }
         return null;
      }

      public override bool TryDeserialize(object serialized, out object value)
      {
         if (TimeSpan.TryParse(serialized?.ToString(), out TimeSpan s))
         {
            value = s;
         }
         else
         {
            value = null;
         }
         return true;
      }
   }
}
