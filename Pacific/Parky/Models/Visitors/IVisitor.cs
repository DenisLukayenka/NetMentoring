namespace Parky.Models.Visitors
{
    public interface IVisitor
    {
         object VisitFullRef(Ref @ref);

         object VisitExportRef(ExportRef @ref);

         object VisitConstructorRef(ConstructorRef @ref);

         object VisitImportRef(ImportRef @ref);
    }
}