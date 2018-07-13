using System;
using System.Drawing;

namespace MD2Discord.Util
{
    public static class DiscordHelper
    {
        public static Image DrawText(string text, float fontSize, Color textColor, Color? backColor = null, FontFamily fontFamily=null)
        {
            if (fontFamily == null) fontFamily = new FontFamily("bandy.ttf");
            var font = fontFamily.GetFont(fontSize);
            Image img = new Bitmap(1,1);
            var drawing = Graphics.FromImage(img);

            var textSize = drawing.MeasureString(text, font);
            
            img.Dispose();
            drawing.Dispose();
            
            img = new Bitmap((int) textSize.Width, (int) textSize.Height);
            
            drawing = Graphics.FromImage(img);

            drawing.Clear(backColor ?? Color.Transparent);

            Brush textBrush = new SolidBrush(textColor);
            drawing.DrawString(text,font,textBrush,0,0);

            drawing.Save();

            return img;
        }

        private static Font GetFont(this FontFamily family, float size, FontStyle style=FontStyle.Regular)
        {
            return new Font(family, size, style);
        }
    }
}