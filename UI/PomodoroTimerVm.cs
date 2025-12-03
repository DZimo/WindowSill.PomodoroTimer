using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using WindowSill.API;

namespace WindowSill.PerfCounter.UI;

public partial class PomodoroTimerVm : ObservableObject
{
    private IPluginInfo pluginInfo; 

    public PomodoroTimerVm(IPluginInfo? pluginInfo)
    {
        Guard.IsNotNull(pluginInfo, nameof(pluginInfo));
        this.pluginInfo = pluginInfo;
    }

    public SillView CreateView()
    {
        return new SillView { Content = new PomodoroTimerView(pluginInfo, this) };
    }
}