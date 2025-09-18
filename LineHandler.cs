using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Map_war
{
    internal class LineHandler
    {

        public bool DeleteLine(List<Map_Line> lines, Point tapPoint, PictureBox picture_map, float threshold = 15)
        {
            Image img = picture_map.Image;
            if (img == null)
            {
                return false;
            }

            int pbWidth = picture_map.Width;
            int pbHeight = picture_map.Height;
            int imgWidth = img.Width;
            int imgHeight = img.Height;

            float ratioWidth = (float)pbWidth / imgWidth;
            float ratioHeight = (float)pbHeight / imgHeight;
            float ratio = Math.Min(ratioWidth, ratioHeight);

            int displayedWidth = (int)(imgWidth * ratio);
            int displayedHeight = (int)(imgHeight * ratio);

            int offsetX = (pbWidth - displayedWidth) / 2;
            int offsetY = (pbHeight - displayedHeight) / 2;
            // Переводим координаты клика из координат PictureBox в координаты изображения
            int x = tapPoint.X - offsetX;
            int y = tapPoint.Y - offsetY;
            if (displayedWidth <= 0 || displayedHeight <= 0) return false;
            float ratio = Math.Min((float)pbWidth / imgWidth, (float)pbHeight / imgHeight);
            int imgX = (int)(x / ratio);
            int imgY = (int)(y / ratio);
            tapPoint = new Point(imgX, imgY);

            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
               
                // Проверяем все сегменты линии
                for (int j = 1; j < line.Points.Count; j++)
                {
                    Point p1 = line.Points[j - 1];
                    Point p2 = line.Points[j];
                    var d = DistanceToLineSegment(p1, p2, tapPoint);
                    Console.WriteLine(tapPoint);
                    Console.WriteLine(p1);
                    Console.WriteLine(p2);
                    Console.WriteLine(d);
                    Console.WriteLine("-------------------------------------");
                    // Если расстояние от точки до сегмента линии меньше порога
                    if (d <= threshold)
                    {
                        lines.RemoveAt(i);
                        return true; // Линия найдена и удалена
                    }
                }
            }
            return false; // Нажатие не на линию
        }

        // Метод для вычисления расстояния от точки до отрезка
        float DistanceToLineSegment(Point p1, Point p2, Point tapPoint)
        {
            // Вектор отрезка
            float segmentLengthSquared = (p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y);

            // Если отрезок вырожден в точку
            if (segmentLengthSquared == 0)
                return (float)Math.Sqrt((tapPoint.X - p1.X) * (tapPoint.X - p1.X) +
                                       (tapPoint.Y - p1.Y) * (tapPoint.Y - p1.Y));

            // Параметр проекции точки на отрезок (0 <= t <= 1)
            float t = Math.Max(0, Math.Min(1,
                ((tapPoint.X - p1.X) * (p2.X - p1.X) +
                 (tapPoint.Y - p1.Y) * (p2.Y - p1.Y)) / segmentLengthSquared));

            // Точка проекции на отрезок
            Point projection = new Point(
                (int)(p1.X + t * (p2.X - p1.X)),
                (int)(p1.Y + t * (p2.Y - p1.Y)));

            // Расстояние от точки до проекции
            return (float)Math.Sqrt((tapPoint.X - projection.X) * (tapPoint.X - projection.X) +
                                   (tapPoint.Y - projection.Y) * (tapPoint.Y - projection.Y));
        }





    }
}
