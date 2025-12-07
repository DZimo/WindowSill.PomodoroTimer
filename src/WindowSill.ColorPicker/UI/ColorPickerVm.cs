using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using WindowSill.API;

namespace WindowSill.ColorPicker.UI;

public partial class ColorPickerVm : ObservableObject
{
    private readonly IPluginInfo _pluginInfo;
    //[ObservableProperty]
    //private TimeManager timeManager = new();

    //[ObservableProperty]
    //private PomodoroType pomodoroType = PomodoroType.Short;

    [ObservableProperty]
    private SolidColorBrush selectedColorBrush = new SolidColorBrush(Colors.IndianRed);

    [ObservableProperty]
    private double selectedColorHex = 30;

    public static ColorPickerVm? Instance;

    public ColorPickerVm(IPluginInfo? pluginInfo)
    {
        Guard.IsNotNull(pluginInfo, nameof(pluginInfo));

        _pluginInfo = pluginInfo;
        Instance = this;
    }

    public SillView CreateView()
    {
        return new SillView { Content = new ColorPickerView(_pluginInfo, this) };
    }
}