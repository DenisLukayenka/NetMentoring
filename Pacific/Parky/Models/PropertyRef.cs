using System;

namespace Parky.Models
{
    public class PropertyRef : Ref
    {
        public PropertyRef(Type type) : base(type)
        {
        }

        public override bool IsConstructorInit { get; } = false;
    }
}