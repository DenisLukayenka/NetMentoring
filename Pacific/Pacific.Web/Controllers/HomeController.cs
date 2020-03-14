using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pacific.Web.Models;

namespace Pacific.Web.Controllers
{
	[ApiController]
    [Route("/api/home")]
	[EnableCors("AllowAll")]
	[Produces(MediaTypeNames.Application.Json)]
	public class HomeController : ControllerBase
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		[HttpGet("getDefault")]
		public JsonResult Get()
		{
			return new JsonResult(new string[]{ "string", "data" });
		}
	}
}
