using WindowSill.API;
using Picker = Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Text;

namespace WindowSill.ColorPicker.UI
{
    public sealed class RadialPickerView : SillPopupContent
    {
        public RadialPickerView(ColorPickerVm colorPickerVm) 
        {
            this.DataContext(
                colorPickerVm,
                (view, vm) => view
                   .Content(
                       new Grid()
                           .VerticalAlignment(VerticalAlignment.Top)
                           .Children(
                                new StackPanel()
                                .Children(
                                    new TextBlock()
                                        .VerticalAlignment(VerticalAlignment.Center)
                                        .HorizontalAlignment(HorizontalAlignment.Center)
                                        .FontWeight(FontWeights.Bold)
                                        .Text("Color Picker"),
                                    new Picker.ColorPicker()
                                        .ColorSpectrumShape(ColorSpectrumShape.Ring)
                                        .Color(x => x.Binding(() => vm.SelectedColorHex).TwoWay())
                                    ))));
            }
    }
}
