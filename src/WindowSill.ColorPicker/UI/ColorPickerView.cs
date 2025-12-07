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
                  .Children(
                      new SillOrientedStackPanel()
                          .VerticalAlignment(VerticalAlignment.Center)
                          .HorizontalAlignment(HorizontalAlignment.Center)
                          .Spacing(1)
                          .Children(
                              new StackPanel()
                                  .Orientation(Orientation.Horizontal)
                                  .VerticalAlignment(VerticalAlignment.Center)
                                  .HorizontalAlignment(HorizontalAlignment.Center)
                                  .Padding(1)
                                  .Margin(1)
                                  .Children(
                                      new TextBox()
                                          .Text("#FFFFFF")
                                          .HorizontalAlignment(HorizontalAlignment.Center),
                                      new StackPanel()
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
