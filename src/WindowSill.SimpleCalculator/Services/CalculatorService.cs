using Microsoft.Extensions.Logging;
using System.ComponentModel.Composition;
using WindowSill.SimpleCalculator.Enums;

namespace WindowSill.SimpleCalculator.Services
{
    [Export(typeof(ICalculatorService))]

    public class CalculatorService : ICalculatorService
    {
        public bool ContainstArithmeticOperator(ReadOnlySpan<char> inputSpan)
        {
            return inputSpan.Contains('+') || inputSpan.Contains('-') || inputSpan.Contains('*') || inputSpan.Contains('/');
        }

        public ArithmeticOperator GetArithmeticOperator(ReadOnlySpan<char> inputSpan)
        {
            switch (inputSpan)
            {
                case var span when span.Contains('+'):
                    return ArithmeticOperator.Plus;
                case var span when span.Contains('-'):
                    return ArithmeticOperator.Minus;
                case var span when span.Contains('/'):
                    return ArithmeticOperator.Divide;
                case var span when span.Contains('*'):
                    return ArithmeticOperator.Multiply;
                default:
                    return ArithmeticOperator.None;
            }
        }
    }
}
