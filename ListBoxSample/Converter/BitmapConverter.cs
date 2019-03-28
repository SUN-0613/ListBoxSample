using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace ListBoxSample.Converter
{


    /// <summary>
    /// BitmapをControls.Image.Sourceへ表示するためのコンバータ
    /// </summary>
    public class BitmapConverter : IValueConverter
    {

        /// <summary>
        /// BitmapをBitmapSourceへ変換
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is Bitmap bitmap)
            {

                var handle = bitmap.GetHbitmap();

                try
                {

                    return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());



                }
                catch { }
                finally
                {
                    DeleteObject(handle);
                }

            }

            return null;

        }

        /// <summary>
        /// 今回は使用しない
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

    }

}
