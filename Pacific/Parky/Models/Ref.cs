using System;

namespace Parky.Models
{
    public class Ref
    {
        public Ref(Type type)
        {
            this.Type = type;
        }

        public Type Type { get; set; }

        public virtual bool IsConstructorInit { get; } = true;
        public virtual bool IsPropertiesInit { get; } = true;
    }
}