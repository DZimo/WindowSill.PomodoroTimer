using Windows.UI;
using WindowSill.SimpleCalculator.Enums;

namespace WindowSill.SimpleCalculator.Services
{
    public interface ICalculatorService
    {
        public bool ContainstArithmeticOperator(ReadOnlySpan<char> inputSpan);
        public ArithmeticOperator GetArithmeticOperator(ReadOnlySpan<char> inputSpan);
    }
}
