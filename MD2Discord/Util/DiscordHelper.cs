using System;
using System.Drawing;

namespace MD2Discord.Util
{
    public class DiscordHelper
    {
        public static Image DrawText(string text, Font font, Color textColor, Color? backColor = null)
        {
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
    }
}