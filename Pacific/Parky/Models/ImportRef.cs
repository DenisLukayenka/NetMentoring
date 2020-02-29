using System;
using Parky.Models.Visitors;

namespace Parky.Models
{
    public class ImportRef : Ref
    {
        public ImportRef(Type type) : base(type)
        {
        }

        public override object Build(IVisitor visitor)
        {
            return visitor.VisitImportRef(this);
        }
    }
}