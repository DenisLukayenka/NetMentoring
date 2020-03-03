using System;
using Parky.Models.Visitors;

namespace Parky.Models
{
    public class Ref
    {
        public Ref(Type type, Type baseType = null)
        {
            this.Type = type;
            this.BaseType = baseType;
        }

        public Type Type { get; set; }
        public Type BaseType { get; set; }

        public virtual object Build(IVisitor visitor)
        {
            return visitor.VisitFullRef(this);
        }
    }
}