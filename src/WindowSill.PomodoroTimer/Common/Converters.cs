using WindowSill.PomodoroTimer.Services;

namespace WindowSill.PomodoroTimer.Common
{
    public static class Converters
    {
        public static readonly IValueConverter PomodoroTypeConverter = new PomodoroTypeConverter();
    }
}
