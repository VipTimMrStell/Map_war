using System;
using System.Drawing;
using Newtonsoft.Json;

namespace Map_war
{
    public class Map_Marker
    {
        //public string Resource_Map;
        public string ResourceName { get; set; }   // Имя ресурса
        public string Name_Znak { get; set; }       // Название знака
        public float Pos_X { get; set; }            // Координата X
        public float Pos_Y { get; set; }            // Координата Y        

        public Image Get_ZNAK()
        {
            // Получаем изображение из ресурсов по имени
            return (Image)Properties.Resources.ResourceManager.GetObject(ResourceName);            
        }
        public static Image Get_Image(string resourcename)
        {
            return (Image)Properties.Resources.ResourceManager.GetObject(resourcename);
        }        

        public static Image RotateImage(Image img, float angle)
        {
            if (img == null) return null;
            // Радианы для расчёта sin и cos
            double radians = angle * Math.PI / 180;
            // Ширина и высота исходного изображения
            double cos = Math.Abs(Math.Cos(radians));
            double sin = Math.Abs(Math.Sin(radians));
            int newWidth = (int)Math.Round(img.Width * cos + img.Height * sin);
            int newHeight = (int)Math.Round(img.Width * sin + img.Height * cos);

            Bitmap rotatedBmp = new Bitmap(newWidth, newHeight);
            rotatedBmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            using (Graphics g = Graphics.FromImage(rotatedBmp))
            {
                // Переносим начало координат в центр нового изображения
                g.TranslateTransform((float)newWidth / 2, (float)newHeight / 2);
                // Поворачиваем
                g.RotateTransform(angle);
                // Смещаем обратно на центр исходника
                g.TranslateTransform(-(float)img.Width / 2, -(float)img.Height / 2);
                // Рисуем исходное изображение
                g.DrawImage(img, new Point(0, 0));
            }
            return rotatedBmp;
        }

        public static Image FlipVertical(Image img)
        {
            Bitmap bmp = new Bitmap(img);
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return bmp;
        }

        public static Image FlipHorizontal(Image img)
        {            
            Bitmap bmp = new Bitmap(img);
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
            return bmp;
        }

    }
}