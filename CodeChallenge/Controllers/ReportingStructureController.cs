using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CodeChallenge.Controllers
{
	[ApiController]
	[Route("api/reportingStructure")]
	public class ReportingStructureController : Controller
	{
		private readonly ILogger _logger;
		private readonly IEmployeeService _employeeService;

		public ReportingStructureController(ILogger<ReportingStructureController> logger, IEmployeeService employeeService)
		{
			_logger = logger;
			_employeeService = employeeService;
		}

		[HttpGet("{id}", Name = "getReportingStructureByEmployeeId")]
		public IActionResult GetReportingStructureByEmployeeId(String id)
		{
			_logger.LogDebug($"Received reporting structure get request for employee with id: '{id}'");

			if (_employeeService.GetById(id) == null) return NotFound();

			return Ok(_employeeService.GetReportingStructure(id));
		}
	}
}
