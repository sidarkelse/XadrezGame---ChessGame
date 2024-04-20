using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChessUI.Helpers
{
    public static class ExtensionMethods
    {
        public static void SetImageSource(this Image image, ImageSource source)
        {
            if (image.Dispatcher.CheckAccess())
            {
                image.Source = source;
            }
            else
            {
                image.Dispatcher.Invoke(() => image.SetImageSource(source));
            }
        }

        public static void SetCursor(this Window window, Cursor cursor)
        {
            if(window.Dispatcher.CheckAccess())
            {
                window.Cursor = cursor;
            }
            else
            {
                window.Dispatcher.Invoke(() => window.SetCursor(cursor));
            }
        }
    }
}
