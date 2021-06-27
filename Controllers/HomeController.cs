using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
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
      var prevStateSerialized = TempData["prevState"] as string;
      if (prevStateSerialized is not null)
      {
        TempData["prevState"] = null;
        var prevState = JsonSerializer.Deserialize<CalculatorModel>(prevStateSerialized);

        return View(prevState);
      }
      return View();
    }

    [HttpPost]
    public IActionResult Index(CalculatorModel calculator)
    // public IActionResult Index([FromBody] CalculatorModel calculator)
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

    [HttpPost]
    public IActionResult AddRow(CalculatorModel calculator)
    {
      _logger.LogInformation("POST AddRow");
      calculator.calculatorState.Add(new CalculatorRowModel());
      TempData["prevState"] = JsonSerializer.Serialize(calculator);
      return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Calculate(CalculatorModel calculator)
    {
      _logger.LogInformation("POST Calculate");
      var sum = 0;
      foreach (var calculatorRow in calculator.calculatorState)
      {
        sum += Convert.ToInt32(calculatorRow.formula);
      }
      var resultRow = new CalculatorRowModel();
      resultRow.formula = sum.ToString();
      calculator.calculatorState.Add(resultRow);
      TempData["prevState"] = JsonSerializer.Serialize(calculator);
      return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
