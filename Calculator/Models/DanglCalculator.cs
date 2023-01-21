using System.Globalization;

namespace Calculator.ViewModels;

public class DanglCalculator : ICalculator
{
    public string Calculate(string expression)
    {
        var calculation = Dangl.Calculator.Calculator.Calculate(expression);

        return calculation.IsValid ? calculation.Result.ToString(CultureInfo.InvariantCulture) : calculation.ErrorMessage;
    }
}