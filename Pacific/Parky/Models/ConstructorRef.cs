using System;
using Parky.Models.Visitors;

namespace Parky.Models
{
    public class ConstructorRef : Ref
    {
        public ConstructorRef(Type type) : base(type)
        {
        }

        public override object Build(IVisitor visitor)
        {
            return visitor.VisitConstructorRef(this);
        }
    }
}