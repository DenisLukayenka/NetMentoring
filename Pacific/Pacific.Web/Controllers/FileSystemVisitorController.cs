using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pacific.Core.Services;
using Pacific.Web.Models;

namespace Pacific.Web.Controllers
{
    public class FileSystemVisitorController : Controller
    {
        private FileSystemVisitor _visitor;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectFolder(SelectedFolderViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                this._visitor = new FileSystemVisitor(viewModel.FolderPath);
                ViewBag.FolderPath = viewModel.FolderPath;
            }

            return PartialView("_FileSystemVisitor");
        }
    }
}