using System;

namespace Parky.Models
{
    public class ExportRef : Ref
    {
        public ExportRef(Type type) : base(type)
        {
        }

        public override bool IsConstructorInit { get; } = false;
        public override bool IsPropertiesInit { get; } = false;
    }
}