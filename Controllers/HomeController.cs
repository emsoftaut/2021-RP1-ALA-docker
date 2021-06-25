using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using rp1.Models;

namespace rp1.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    // public IActionResult Index(CalculatorModel calculator)
    public IActionResult Index([FromBody] CalculatorModel calculator)
    {
      _logger.LogInformation("POST Index");
      foreach (var calculatorRow in calculator.calculatorState)
      {
        _logger.LogInformation("Label" + calculatorRow.label);
        _logger.LogInformation("Formula" + calculatorRow.formula);
        _logger.LogInformation("Result" + calculatorRow.result);
      }
      return View(calculator);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
