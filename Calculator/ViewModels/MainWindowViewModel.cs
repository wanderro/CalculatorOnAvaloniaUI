using System;
using System.Windows.Input;
using Prism.Commands;
using ReactiveUI;

namespace Calculator.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ICalculator _calculator;
    private string _displayText = "0";

    public string DisplayText
    {
        get => _displayText;
        set
        {
            _displayText = value;
            OnPropertyChanged();
        }
    }

    public ICommand SetInputSymbolCommand { get; }
    public ICommand CalcCommand { get; }
    public ICommand ClearLastCommand { get; }
    public ICommand AllClearCommand { get; }
    

    public MainWindowViewModel(ICalculator calculator)
    {
        _calculator = calculator;
        CalcCommand = new DelegateCommand(OnCalc);;
        ClearLastCommand = new DelegateCommand(OnClearLastSymbol);;
        AllClearCommand = new DelegateCommand(AllClear);;
        SetInputSymbolCommand = new DelegateCommand<string?>(SetSymbol);
    }

    private void AllClear() => DisplayText = "0";

    private void OnClearLastSymbol()
    {
        if (!string.IsNullOrWhiteSpace(DisplayText))
        {
            DisplayText = DisplayText[..^1];

            if (string.IsNullOrWhiteSpace(DisplayText))
            {
                AllClear();
            }
        }
    }

    private void OnCalc()
    {
        DisplayText = _calculator.Calculate(DisplayText);
    }

    private void SetSymbol(string? parameter)
    {
        if (DisplayText == "0" && IsDigit(parameter))
        {
            DisplayText = string.Empty;
        }
        DisplayText += parameter;
    }

    private static bool IsDigit(string? parameter)
    {
        return int.TryParse(parameter, out var d);
    }
}