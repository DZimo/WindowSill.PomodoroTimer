using CommunityToolkit.WinUI.Controls;
using WindowSill.API;
using WindowSill.PomodoroTimer.Models;

namespace WindowSill.PomodoroTimer.Settings
{
    internal class SettingsView : UserControl
    {
        public SettingsView(ISettingsProvider settingsProvider)
        {
            this.DataContext(
                new SettingsVm(settingsProvider),
                (view, viewModel) => view
                .Content(
                    new StackPanel()
                        .Spacing(2)
                        .Children(
                            new TextBlock()
                                .Style(x => x.ThemeResource("BodyStrongTextBlockStyle"))
                                .Margin(0, 0, 0, 8)
                                .Text("/WindowSill.PomodoroTimer/Misc/General".GetLocalizedString()),
                            new SettingsCard()
                                .Header("/WindowSill.PomodoroTimer/Misc/DisplayMode".GetLocalizedString())
                                .Description("/WindowSill.PomodoroTimer/Misc/DisplayModeDesc".GetLocalizedString())
                                .Tag("test")
                                .HeaderIcon(
                                    new FontIcon()
                                        .Glyph("\uECC5"))
                                .Content(
                              new ComboBox()
                                  .ItemsSource(Enum.GetValues(typeof(TimeDisplayMode)))
                                  .SelectedItem(
                                        x => x.Binding(() => viewModel.TimeDisplayMode)
                                              .TwoWay()
                                              .UpdateSourceTrigger(UpdateSourceTrigger.PropertyChanged)
                                )
                        )
                        )
                )
            );
        }
    }
}
