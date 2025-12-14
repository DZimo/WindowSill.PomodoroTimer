using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using WindowSill.API;
using WindowSill.SimpleCalculator.Enums;
using WindowSill.SimpleCalculator.Services;

namespace WindowSill.SimpleCalculator.UI;

public partial class SimpleCalculatorVm : ObservableObject
{
    private readonly IPluginInfo _pluginInfo;
    private readonly IProcessInteractionService _processInteraction;
    private readonly ICalculatorService _calculatorService;

    //[ObservableProperty]
    //private string selectedArithmeticOP = "";

    [ObservableProperty]
    private ArithmeticOperator selectedArithmeticOP = ArithmeticOperator.None;

    [ObservableProperty]
    private float total = 0;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SelectedNumber))]
    private float x = 0;

    public event EventHandler testEvent;

    private string selectedNumber = "";
    public string SelectedNumber
    {
        get => selectedNumber;
        set 
        {
            if (selectedNumber == value)
                return;

            selectedNumber = value;
            OnPropertyChanged(nameof(SelectedNumber));
        }
    }

    [ObservableProperty]
    private int colorFontSize = 12;

    [ObservableProperty]
    private int colorboxHeight = 18;

    public static SimpleCalculatorVm? Instance;

    public SimpleCalculatorVm(IPluginInfo? pluginInfo, IProcessInteractionService processInteraction, ICalculatorService calculatorService)
    {
        Guard.IsNotNull(pluginInfo, nameof(pluginInfo));
        Guard.IsNotNull(processInteraction, nameof(processInteraction));
        Guard.IsNotNull(calculatorService, nameof(calculatorService));

        _pluginInfo = pluginInfo;
        _processInteraction = processInteraction;
        _calculatorService = calculatorService;
        Instance = this;
    }

    public SillView CreateView()
    {
        return new SillView { Content = new SimpleCalculatorView(_pluginInfo, this) };
    }

    [RelayCommand]
    public void ExtendCalculator()
    {
        SelectedNumber = "99+";
    }

    public void NumberTextboxChanging()
    {
        char[] buffer = new char[selectedNumber.Length];
        var span = buffer.AsSpan();
        selectedNumber.AsSpan().CopyTo(span);
        SelectedArithmeticOP = _calculatorService.GetArithmeticOperator(span);

        if (SelectedArithmeticOP is ArithmeticOperator.None)
            return;

        X = _calculatorService.GetNumberX(span, _calculatorService.ArithmeticOperatorToString(SelectedArithmeticOP).ToString().AsSpan());
        SelectedNumber = "";
    }
}