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

    public CalculatorRowModel()
    {
      label = "a";
      formula = "b";
      result = "c";
    }
  }
}