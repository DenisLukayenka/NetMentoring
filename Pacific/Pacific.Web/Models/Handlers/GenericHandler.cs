using System.Linq;
using Pacific.Core.Services;
using Pacific.Web.Models.Requests;
using Pacific.Web.Models.Responses;

namespace Pacific.Web.Models.Handlers
{
    public class GenericHandler: IHandler
    {
        private FileSystemVisitor _visitor;

        public IResponse Execute(IRequest request)
        {
            switch(request)
            {
                case SystemVisitorRequest r:
                    return this.Execute(r);

                default:
                    throw new System.NotImplementedException();
            }
            
        }

        protected SystemVisitorResponse Execute(SystemVisitorRequest request)
        {
            this._visitor = new FileSystemVisitor(request.FolderPath);

            return new SystemVisitorResponse()
            {
                files = this._visitor.Explore().ToArray()
            };
        }
    }
}