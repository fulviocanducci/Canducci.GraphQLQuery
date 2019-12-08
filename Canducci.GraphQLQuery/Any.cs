namespace Canducci.GraphQLQuery
{
   public sealed class Any: Bases.BaseScalar
   {      
      public Any(string name, object value)
      {
         Name = name ?? throw new System.ArgumentNullException(nameof(name));
         Value = value ?? throw new System.ArgumentNullException(nameof(value));
      }
   }
}

/*
Type	Description

Int	Signed 32-bit numeric non-fractional value
Float	Double-precision fractional values as specified by IEEE 754
String	UTF-8 character sequences
Boolean	Boolean type representing true or false
ID	Unique identifier
Extended Scalar Types
Apart from the core scalars we have also added support for an extended set of scalar types:

Type	Description

Byte	
Short	Signed 16-bit numeric non-fractional value
Long	Signed 64-bit numeric non-fractional value
Decimal	.NET Floating Point Type
Url	Url
DateTime	ISO-8601 date time
Date	ISO-8601 date
Uuid	GUID
Any	This type can be anything, string, int, list or object etc.
*/