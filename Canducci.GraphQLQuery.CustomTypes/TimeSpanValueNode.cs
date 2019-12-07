using HotChocolate.Language;
using System;

namespace Canducci.GraphQLQuery.CustomTypes
{
   public class TimeSpanValueNode : IValueNode<TimeSpan>
   {
      public TimeSpanValueNode(NodeKind kind, TimeSpan value, Location location)
      {
         Kind = kind;
         Value = value;
         Location = location;
      }
      public TimeSpan Value { get; }

      public NodeKind Kind { get; }

      public Location Location { get; }

      object IValueNode.Value { get; }

      public bool Equals(IValueNode other)
      {
         return other.GetHashCode() == GetHashCode();
      }
   }
}
