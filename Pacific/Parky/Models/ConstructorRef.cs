using System;

namespace Parky.Models
{
    public class ConstructorRef : Ref
    {
        public ConstructorRef(Type type) : base(type)
        {
        }

        public override bool IsPropertiesInit { get; } = false;
    }
}