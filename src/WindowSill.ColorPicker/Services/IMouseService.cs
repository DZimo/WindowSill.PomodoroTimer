using Windows.UI;

namespace WindowSill.ColorPicker.Services
{
    public interface IMouseService
    {
        public Color CurrentColor { get; set; }  

        public event EventHandler Exited;

        public string GetColorAtCursorNative();

        public void ShowColorNative();

        public void Dispoese();
    }
}
