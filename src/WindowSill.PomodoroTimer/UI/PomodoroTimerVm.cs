using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Windows.UI;
using WindowSill.API;
using WindowSill.PomodoroTimer.Models;
using WindowSill.PomodoroTimer.Services;

namespace WindowSill.PomodoroTimer.UI;

public partial class PomodoroTimerVm : ObservableObject
{
    private readonly IPluginInfo _pluginInfo;
    public readonly ITimeHandlerService _timeHandlerService;

    [ObservableProperty]
    private TimeManager timeManager = new();

    [ObservableProperty]
    private PomodoroType pomodoroType = PomodoroType.Short;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PomodoroStopped))]
    [NotifyCanExecuteChangedFor(nameof(StartPomodoroCommand))]
    [NotifyCanExecuteChangedFor(nameof(StopPomodoroCommand))]
    private bool pomodoroStarted;

    [ObservableProperty]
    private SolidColorBrush pomodoroColor = new SolidColorBrush(Colors.IndianRed);

    [ObservableProperty]
    private double colorHeight = 5;


    public static PomodoroTimerVm? Instance;

    private bool CanExecuteStart() => PomodoroStarted;

    private bool PomodoroStopped
    {
        get => !PomodoroStarted;
    }

    public string MinutesLeft
    {
        get => $"{_timeHandlerService.GetMinutes(TimeManager):D2}";
    }
    public string SecondsLeft 
    {
        get => $"{_timeHandlerService.GetSeconds(TimeManager):D2}";
    }

    public string TimeLeft
    {
        get => $"{MinutesLeft}:{SecondsLeft}";
    }

    public PomodoroTimerVm(ITimeHandlerService timeHandlerService, IPluginInfo? pluginInfo)
    {
        Guard.IsNotNull(pluginInfo, nameof(pluginInfo));
        Guard.IsNotNull(timeHandlerService, nameof(timeHandlerService));

        _pluginInfo = pluginInfo;
        _timeHandlerService = timeHandlerService;
        Instance = this;
    }

    public SillView CreateView()
    {
        return new SillView { Content = new PomodoroTimerView(_pluginInfo, this) };
    }

    [RelayCommand(CanExecute = nameof(PomodoroStopped))]
    private void StartPomodoro()
    {
        PomodoroStarted = true;

        _timeHandlerService.StartTimer(TimeManager, PomodoroType);
        _timeHandlerService.TimerReduced += OnTimerReduced;
        _timeHandlerService.TimerFinished += OnTimerFinished;
    }

    private void OnTimerFinished(object? sender, TimeManager? e)
    {
        ReserTimersVm();

        _timeHandlerService.ChangeTime(TimeManager, PomodoroType);
    }

    [RelayCommand(CanExecute = nameof(PomodoroStarted))]
    private void StopPomodoro()
    {
        PomodoroStarted = false;

        _timeHandlerService.ResetTimer(TimeManager, PomodoroType);
        _timeHandlerService.TimerReduced -= OnTimerReduced;
        _timeHandlerService.TimerFinished -= OnTimerFinished;

        ReserTimersVm();
    }

    private void OnTimerReduced(object? sender, TimeManager? e)
    {
        TimeManager.Seconds++;
        TimeManager.Minutes = TimeManager.Seconds / 60;

        ThreadHelper.RunOnUIThreadAsync(() =>
        {
            OnPropertyChanged(nameof(MinutesLeft));
            OnPropertyChanged(nameof(SecondsLeft));
            OnPropertyChanged(nameof(TimeLeft));
        });
    }

    private void ReserTimersVm()
    {
        ThreadHelper.RunOnUIThreadAsync(() =>
        {
            TimeManager.Seconds = 0;
            TimeManager.Minutes = 0;

            OnPropertyChanged(nameof(MinutesLeft));
            OnPropertyChanged(nameof(SecondsLeft));
            OnPropertyChanged(nameof(TimeLeft));
        });
    }
}