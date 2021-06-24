using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace rp1.Models
{
  public class CalculatorModel
  {
    public List<CalculatorRowModel> calculatorState { get; set; }

    public CalculatorModel()
    {
      System.Diagnostics.Trace.WriteLine("calculatorState");
      calculatorState = new List<CalculatorRowModel>();
      calculatorState.Add(new CalculatorRowModel());
    }
  }
}