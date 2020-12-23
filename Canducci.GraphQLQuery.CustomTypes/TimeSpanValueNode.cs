using HotChocolate.Language;
using System;
using System.Collections.Generic;

namespace Canducci.GraphQLQuery.CustomTypes
{
    public class TimeSpanValueNode : IValueNode<TimeSpan>
    {
        public TimeSpanValueNode(SyntaxKind kind, TimeSpan value, Location location)
        {
            Kind = kind;
            Value = value;
            Location = location;
        }

        public TimeSpan Value { get; }

        public Location Location { get; }

        public SyntaxKind Kind { get; }

        object IValueNode.Value { get; }

        public bool Equals(IValueNode other)
        {
            return other.GetHashCode() == GetHashCode();
        }

        public IEnumerable<ISyntaxNode> GetNodes()
        {
            throw new NotImplementedException();
        }

        public string ToString(bool indented)
        {
            throw new NotImplementedException();
        }
    }
}
