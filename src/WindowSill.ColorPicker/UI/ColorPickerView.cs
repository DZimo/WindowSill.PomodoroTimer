using System.Numerics;
using WindowSill.API;

namespace WindowSill.ColorPicker.UI;

public sealed class ColorPickerView : UserControl
{
    public ColorPickerView(IPluginInfo pluginInfo, ColorPickerVm pomodoroVm)
    {
        this.DataContext(
          pomodoroVm,
          (view, vm) => view
          .Content(
              new Grid()
                  .VerticalAlignment(VerticalAlignment.Top)
                  .Children(
                      new SillOrientedStackPanel()
                          .Spacing(1)
                          .Children(
                              new StackPanel()
                                  .Orientation(Orientation.Horizontal)
                                  .Children(
                                      new TextBox()
                                          .PlaceholderText("#FFFFFF")
                                          .PlaceholderForeground(Colors.Gray)
                                          .FontSize(6)
                                          .TextAlignment(TextAlignment.Center)
                                          .AcceptsReturn(false)
                                          .FontStretch(Windows.UI.Text.FontStretch.Expanded)
                                          .HorizontalTextAlignment(TextAlignment.Center)
                                          .VerticalContentAlignment(VerticalAlignment.Top)
                                          .VerticalAlignment (VerticalAlignment.Center)
                                          .TextWrapping(TextWrapping.NoWrap)
                                          .MinHeight(0)
                                          .Height(18)
                                          .MaxWidth(75)
                                          .Width(75)
                                          .MaxLength(7)
                                          .Margin(0, 0, 0, 0)
                                          .Text(x => x.Binding(() => vm.SelectedColorHex).TwoWay())
                                          .BorderBrush(x => x.Binding(() => vm.SelectedColorBrush).OneWay()),
                                      new StackPanel()
                                          .Width(20)
                                          .Margin(2, 0, 0, 0)
                                          .Background(x => x.Binding(() => vm.SelectedColorBrush).OneWay()),
                                      new StackPanel()
                                      .Orientation(Orientation.Horizontal)
                                          .Children(
                                                new Button()
                                                   .Style(x => x.StaticResource("IconButton"))
                                                   .Content("\xEf3c"),
                                                new Button()
                                                   .Style(x => x.StaticResource("IconButton"))
                                                   .Content("\xE8c8")
                                  ))
                      )
        )));
    }
}
