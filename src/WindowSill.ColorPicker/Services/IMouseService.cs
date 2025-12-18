using Windows.UI;

namespace WindowSill.ColorPicker.Services
{
    public interface IMouseService
    {
        public event EventHandler MouseExited;

        public string GetColorAtCursorNative();

        public string ColorToHEX(Color rgb);

        public void Dispoese();
    }
}
