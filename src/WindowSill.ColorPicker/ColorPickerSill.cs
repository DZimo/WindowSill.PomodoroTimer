using CommunityToolkit.Diagnostics;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Windows.System;
using WindowSill.API;
using WindowSill.ColorPicker.UI;

namespace WindowSill.ColorPicker;

[Export(typeof(ISill))]
[Name("WindowSill.ColorPicker")]
[Priority(Priority.Lowest)]
public sealed class ColorPickerSill : ISill, ISillSingleView
{
    private ColorPickerVm? pomodoroTimerVm;
    private IPluginInfo pluginInfo;

    public SillView? View { get; private set; }

    [ImportingConstructor]
    public ColorPickerSill(IPluginInfo pluginInfo)
    {
        this.pluginInfo = pluginInfo;
        pomodoroTimerVm = new ColorPickerVm(pluginInfo);

        View = pomodoroTimerVm.CreateView();
        UpdateColorHeight();

        View.IsSillOrientationOrSizeChanged += (o, p) =>
        {
            UpdateColorHeight();
        };
    }

    private void UpdateColorHeight()
    {
    }

    public string DisplayName => "/WindowSill.PomodoroTimer/Misc/DisplayName".GetLocalizedString();

    public IconElement CreateIcon()
         => new ImageIcon
         {
             Source = new SvgImageSource(new Uri(System.IO.Path.Combine(pluginInfo.GetPluginContentDirectory(), "Assets", "pomodoro_logo.svg")))
         };

    public SillView? PlaceholderView => null;

    public SillSettingsView[]? SettingsViews => null;

    public ValueTask OnActivatedAsync()
    {
        return ValueTask.CompletedTask;
    }

    public ValueTask OnDeactivatedAsync()
    {
        View = null;
        pomodoroTimerVm = null;

        return ValueTask.CompletedTask;
    }
}
