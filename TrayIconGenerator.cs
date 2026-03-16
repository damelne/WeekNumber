using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


public static class TrayIconGenerator
{
    public static System.Drawing.Icon CreateWeekIcon(int week)
    {
        int size = 32;

        var visual = new DrawingVisual();
        using (var dc = visual.RenderOpen())
        {
            dc.DrawRectangle(Brushes.Yellow, null, new Rect(0, 0, size, size));

            var text = new FormattedText(
                week.ToString(),
                CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                new Typeface("Segoe UI"),
                22,
                Brushes.Black,
                1.25);

            var pos = new Point(
                (size - text.Width) / 2,
                (size - text.Height) / 2);

            dc.DrawText(text, pos);
        }

        var bmp = new RenderTargetBitmap(size, size, 96, 96, PixelFormats.Pbgra32);
        bmp.Render(visual);

        var encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(bmp));

        using var ms = new MemoryStream();
        encoder.Save(ms);

        using var bitmap = new System.Drawing.Bitmap(ms);
        return System.Drawing.Icon.FromHandle(bitmap.GetHicon());
    }
}