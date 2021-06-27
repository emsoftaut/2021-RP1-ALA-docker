using System.ComponentModel.DataAnnotations;

namespace rp1.Models
{
  public class CalculatorRowModel
  {
    [Required]
    public string label { get; set; }

    [Required]
    public string formula { get; set; }
    public string result { get; set; }

    // Model bound complex types must not be abstract or value types and must have a parameterless constructor.
    public CalculatorRowModel()
    {
      label = "";
      formula = "";
      result = "";
    }
  }
}