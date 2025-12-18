using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using WindowSill.API;
using WindowSill.PomodoroTimer.Models;
using WindowSill.PomodoroTimer.UI;

namespace WindowSill.PomodoroTimer.Settings
{
    internal sealed partial class SettingsVm : ObservableObject
    {
        ISettingsProvider _settingsProvider;

        public SettingsVm(ISettingsProvider settingsProvider) 
        { 
            Guard.IsNotNull(settingsProvider);

            _settingsProvider = settingsProvider;
        }

        public TimeDisplayMode TimeDisplayMode
        {
            get => _settingsProvider.GetSetting(Settings.DisplayMode);
            set
            {
                _settingsProvider.SetSetting(Settings.DisplayMode, value);
                OnPropertyChanged(nameof(PomodoroTimerVm.TimeDisplayMode));
                OnPropertyChanged(nameof(SettingsVm.TimeDisplayMode));
            }
        }
    }
}
