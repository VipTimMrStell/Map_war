using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map_war
{
    public class Map_Line
    {
        public List<Point> Points { get; set; } // Убрать private set
        public Color color { get; set; }

        public Map_Line(List<Point> points, Color color)
        {
            this.Points = points ?? new List<Point>();
            this.color = color;
            if(color == null){
                this.color = Color.Black;
            }
        }
    }

}
