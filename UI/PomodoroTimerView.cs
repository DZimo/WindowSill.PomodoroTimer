using CommunityToolkit.Diagnostics;

using Microsoft.UI.Xaml.Media.Imaging;

using WindowSill.API;

namespace WindowSill.PerfCounter.UI;

public sealed class PomodoroTimerView : Button
{
    private readonly ImageIcon startIcon = new();
    private readonly ImageIcon stopIcon = new();
    private readonly ImageIcon quarterIcon = new();
    private readonly ImageIcon breakIcon = new();
    private readonly ImageIcon workIcon = new();

    public PomodoroTimerView(IPluginInfo pluginInfo, PomodoroTimerVm pomodoroVm)
    {
        //var sillFontSize = (Double)Application.Current.Resources["SillFontSize"];

        this.DataContext(
          pomodoroVm,
          (view, vm) => view
          .Style(x => x.StaticResource("SillButtonStyle"))
          .Height(double.NaN)
          .Background(new SolidColorBrush(Colors.Transparent))
          .Content(
              new Grid()
                  .Children(
                      new SillOrientedStackPanel()
                          .VerticalAlignment(VerticalAlignment.Center)
                          .HorizontalAlignment(HorizontalAlignment.Stretch)
                          .VerticalAlignment(VerticalAlignment.Stretch)
                          .Spacing(8)
                          .Children(
                              new StackPanel()
                                  .Orientation(Orientation.Vertical)
                                  .Spacing(4)
                                  .Children(
                                      new Button()
                                          .Content(
                                              new TextBlock()
                                                 .Style(x => x.ThemeResource("CaptionTextBlockStyle"))
                                                 .FontSize(x => x.ThemeResource("SillFontSize"))
                                                 .Text("25")
                                              ),
                                      new Button()
                                          .Content(
                                              new TextBlock()
                                                 .Style(x => x.ThemeResource("CaptionTextBlockStyle"))
                                                 .FontSize(x => x.ThemeResource("SillFontSize"))
                                                 .Text("50")
                                              )
                                  ),
                              new StackPanel()
                                  .VerticalAlignment(VerticalAlignment.Center)
                                  .Orientation(Orientation.Horizontal)
                                  .Spacing(4)
                                  .Children(
                                      new Button()
                                          .Style(x => x.StaticResource("IconButton"))
                                          .Content("\xE768"),
                                      new TextBlock()
                                          .MinWidth(32)
                                          .VerticalAlignment(VerticalAlignment.Center)
                                  ),
                              new StackPanel()
                                  .VerticalAlignment(VerticalAlignment.Center)
                                  .Orientation(Orientation.Horizontal)
                                  .Spacing(4)
                                  .Children(
                                      stopIcon
                                          .Source(new SvgImageSource(new Uri(System.IO.Path.Combine(pluginInfo.GetPluginContentDirectory(), "Assets", "memory_slot.svg")))),
                                      new TextBlock()
                                          .MinWidth(32)
                                          .VerticalAlignment(VerticalAlignment.Center)
                                  )
                              )
                      )
        ));
    }
}
