using System;
using Parky.Models.Visitors;

namespace Parky.Models
{
    public class ExportRef : Ref
    {
        public ExportRef(Type type, Type baseType = null) : base(type, baseType)
        {
        }

        public override object Build(IVisitor visitor)
        {
            return visitor.VisitExportRef(this);
        }
    }
}