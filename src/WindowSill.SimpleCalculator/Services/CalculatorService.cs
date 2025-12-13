using Microsoft.Extensions.Logging;
using System.ComponentModel.Composition;

namespace WindowSill.SimpleCalculator.Services
{
    [Export(typeof(ICalculatorService))]

    public class CalculatorService : ICalculatorService
    {
       
    }
}
