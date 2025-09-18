using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Map_war
{
    public partial class Form1 : Form
    {
        private bool isDeletedMode = false;
        private bool isDrawingLine = false;
        private bool isDragging = false;
        private bool isDrawing = false;
        private bool isDeletedLine = false;

        private LineHandler lineHandler = new LineHandler();

        private bool isVertical = false;
        private bool isHorizontal = false;
        private Point lineStartPoint;
        private Point lineEndPoint;
        private List<List<Point>> lines = new List<List<Point>>();
        private List<Point> tmpLine = new List<Point>();
        private Color color_line;
        private Pen drawingPen = new Pen(Color.Black, 10); // Перо для рисования линий
        private Point dragStartPosition;
        private Point panelStartPosition;
        private bool use_text;
        private Point scrollStartPosition; // Добавляем переменную
        private string str_set;
        // Глобальные переменные формы
        float zoom = 0.5f; // коэффициент масштабирования                
        private MapData currentMapData = new MapData();
        // выбраное изображение
        private string ResourceName;
        private string name_znak;
        // изображение, которое будем рисовать по клику       
        Image overlayImage;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            panel_map.TabStop = true;
            panel_map.HorizontalScroll.Visible = true;
            panel_map.VerticalScroll.Visible = true;
            panel_map.HorizontalScroll.Enabled = true;
            panel_map.VerticalScroll.Enabled = true;
            panel_map.MouseMove += panel_map_MouseMove;
            panel_map.MouseDown += panel_map_MouseDown;
            panel_map.MouseUp += panel_map_MouseUp;
            //picture_map.Paint += picture_map_Paint;
            picture_map.Location = new Point(0, 0);
            panel_map.Controls.Add(picture_map);
        }

        private void UpdateScrollBars()
        {
            if (picture_map.Image == null) return;
            // Устанавливаем максимальные значения скролла
            panel_map.HorizontalScroll.Maximum = Math.Max(0, picture_map.Width - panel_map.ClientSize.Width);
            panel_map.VerticalScroll.Maximum = Math.Max(0, picture_map.Height - panel_map.ClientSize.Height);
            // Обновляем видимость скролл-баров
            panel_map.HorizontalScroll.Visible = (picture_map.Width > panel_map.ClientSize.Width);
            panel_map.VerticalScroll.Visible = (picture_map.Height > panel_map.ClientSize.Height);
        }

        private void SetScrollPosition(int x, int y)
        {
            // Ограничиваем значения скролла
            x = Math.Max(0, Math.Min(x, panel_map.HorizontalScroll.Maximum));
            y = Math.Max(0, Math.Min(y, panel_map.VerticalScroll.Maximum));
            // Устанавливаем новые позиции
            panel_map.HorizontalScroll.Value = x;
            panel_map.VerticalScroll.Value = y;
            panel_map.PerformLayout(); // Применяем изменения
        }

        // символ на карту        
        private void Span_ZNAK(object sender, MouseEventArgs e)
        {
            Image img = picture_map.Image;
            if (img == null) return;
            if (overlayImage == null) return;

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

            int x = e.X - offsetX;
            int y = e.Y - offsetY;

            if (x < 0 || y < 0 || x > displayedWidth || y > displayedHeight)
                return; // Клик вне изображения

            float imageX = x / ratio;
            float imageY = y / ratio;

            Draw_Image(imageX, imageY, overlayImage);
            Map_Marker marker = new Map_Marker();
            marker.Pos_X = imageX;
            marker.Pos_Y = imageY;
            marker.ResourceName = this.ResourceName;
            marker.Name_Znak = name_znak;
            currentMapData.Markers.Add(marker);
            if (marker.Name_Znak == null) return;
            Console.WriteLine("Установка знака:" + marker.Pos_X + " " + marker.Pos_Y);
        }

        private void UpdateMarkersUI(List<Map_Marker> markers)
        {
            // Обновите элементы управления, которые отображают маркеры
            foreach (var marker in currentMapData.Markers)
            {
                Draw_Image(marker.Pos_X, marker.Pos_Y, marker.Get_ZNAK());
            }
        }


        private void UpdateLinesUI()
        {
            // Обновите элементы управления, которые отображают маркеры
            foreach (var line in currentMapData.Lines)
            {
                Console.WriteLine($"Update Lines count: {currentMapData.Lines.Count}");
                Draw_Line(line);
            }
        }

        // Исправленный метод клика мышью
        private void panel_map_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isDragging = true;
                dragStartPosition = e.Location;
                panel_map.Cursor = Cursors.SizeAll;
            }
        }
        private void panel_map_MouseMove(object sender, MouseEventArgs e)
        {
            Console.WriteLine($"isDrawingLine {isDrawingLine}");

            if (isDragging && e.Button == MouseButtons.Right)
            {
                int deltaX = e.X - dragStartPosition.X;
                int deltaY = e.Y - dragStartPosition.Y;
                // Вычисляем новые позиции скролла
                int newX = scrollStartPosition.X - deltaX;
                int newY = scrollStartPosition.Y - deltaY;
                // Устанавливаем скролл
                SetScrollPosition(newX, newY);
            }

        }

        private void panel_map_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isDragging = false;
                panel_map.Cursor = Cursors.Default;
            }
            if (isDrawing && e.Button == MouseButtons.Left)
            {
                picture_map.Invalidate();
            }
        }


        private void UpdateTextsUI(List<Map_Text> texts)
        {
            // Обновите элементы управления, которые отображают тексты
            foreach (var text in currentMapData.Texts)
            {
                Draw_Text(text.Position, text.Text_map);
            }
        }

        private void Draw_Image(float X, float Y, Image image)
        {
            // Масштаб для overlayImage
            float scale = 0.5f;
            int newWidth = (int)(image.Width * scale);
            int newHeight = (int)(image.Height * scale);
            // Смещаем, чтобы центрировать
            int drawX = (int)(X - newWidth / 2);
            int drawY = (int)(Y - newHeight / 2);
            // Получаем Bitmap для рисования (убедитесь, что это Bitmap)
            Bitmap baseImage = (Bitmap)picture_map.Image;

            using (Graphics g = Graphics.FromImage(baseImage))
            {
                g.DrawImage(image, new Rectangle(drawX, drawY, newWidth, newHeight));
            }
            picture_map.Invalidate();
        }

        private void Draw_Line(Map_Line line)
        {

            Image img = picture_map.Image;
            if (img == null)
            {
                Console.WriteLine($"img is null, line count: {line.Points}");
                return;
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
            Console.WriteLine($"start foreach ");

            Bitmap bmp = new Bitmap(picture_map.Image);
            Console.WriteLine($"Draw line start");
            for (var i = 1; i < line.Points.Count; i++)
            {
                int x = line.Points[i].X - offsetX;
                int y = line.Points[i].Y - offsetY;

                float imageX = x / ratio;
                float imageY = y / ratio;

                //Point currentLinePoint = new Point((int)imageX, (int)imageY);

                //tmpLine.Add(currentLinePoint);
                

                using (Graphics g = Graphics.FromImage(bmp))
                {
                   
                    Console.WriteLine($"Draw line, tmpline: {line.Points[i].X} {line.Points[i].Y}");
                    color_line = line.color;
                    drawingPen = new Pen(color_line, 10);
                    g.DrawLine(drawingPen, line.Points[i].X, line.Points[i].Y, line.Points[i- 1].X, line.Points[i - 1].Y);             
                }
                
            }
            if (picture_map.Image != null)
            {
                picture_map.Image.Dispose();
            }
            picture_map.Image = bmp;



        }

        // получить координаты мышки в маштабе
        private Point TranslateZoomMousePosition(Point coordinates)
        {
            if (picture_map.Image == null)
                return coordinates;

            int imgWidth = picture_map.Image.Width;
            int imgHeight = picture_map.Image.Height;

            int pbWidth = picture_map.Width;
            int pbHeight = picture_map.Height;

            float imageAspect = (float)imgWidth / imgHeight;
            float controlAspect = (float)pbWidth / pbHeight;

            float scaleFactor;
            int offsetX, offsetY;

            if (imageAspect > controlAspect)
            {
                scaleFactor = (float)pbWidth / imgWidth;
                offsetX = 0;
                offsetY = (int)((pbHeight - imgHeight * scaleFactor) / 2);
            }
            else
            {
                scaleFactor = (float)pbHeight / imgHeight;
                offsetX = (int)((pbWidth - imgWidth * scaleFactor) / 2);
                offsetY = 0;
            }

            int x = (int)((coordinates.X - offsetX) / scaleFactor);
            int y = (int)((coordinates.Y - offsetY) / scaleFactor);

            return new Point(x, y);
        }

        // поставить текст
        private void Span_TEXT(MouseEventArgs e)
        {
            if (str_set == "")
                return;
            button_text.BackColor = Color.White;
            Point imagePoint = TranslateZoomMousePosition(e.Location);
            Draw_Text(imagePoint, str_set);
            Map_Text map_text = new Map_Text();
            map_text.Position = imagePoint;
            map_text.Text_map = str_set;
            currentMapData.Texts.Add(map_text);
        }

        private void Draw_Text(Point Point_Klick, string text)
        {
            if (Point_Klick.X < 0 || Point_Klick.Y < 0 ||
                Point_Klick.X >= picture_map.Image.Width || Point_Klick.Y >= picture_map.Image.Height)
                return; // Клик вне изображения

            Bitmap bmp = new Bitmap(picture_map.Image);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                //string text = str_set;
                Font font = new Font("Arial", 24, FontStyle.Bold);
                Brush brush = Brushes.Black;
                // Рисуем текст с верхним левым углом в точке клика по изображению
                g.DrawString(text, font, brush, Point_Klick);
            }

            if (picture_map.Image != null)
            {
                picture_map.Image.Dispose();
            }
            picture_map.Image = bmp;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            currentMapData.Map = "СВЕТЛОВ";
            picture_map.Image = currentMapData.Get_Map();
            panel_map.MouseWheel += panel1_MouseWheel;
            Add_Dictionary();
        }

        private void Add_Dictionary()
        {
            comboBox_protivnik.Items.Clear();
            foreach (var key in Save_Map.name_znak_protivnik.Keys)
            {
                comboBox_protivnik.Items.Add(key);
            }
            comboBox_own.Items.Clear();
            foreach (var key in Save_Map.name_znak_own.Keys)
            {
                comboBox_own.Items.Add(key);
            }
        }
        private void panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
            Point currentScrollPos = new Point(panel_map.HorizontalScroll.Value, panel_map.VerticalScroll.Value);
            // Инвертируем скролл-позицию
            int scrollX = -currentScrollPos.X;
            int scrollY = -currentScrollPos.Y;

            // Координаты мыши относительно всего содержимого панели
            Point mouseAbsolute = new Point(e.X + scrollX, e.Y + scrollY);

            float oldZoom = zoom;
            if (e.Delta > 0)
                zoom *= 1.1f;
            else
                zoom /= 1.1f;

            // Ограничиваем масштаб
            zoom = Math.Max(0.1f, Math.Min(zoom, 2));
            // Получаем текущие значения прокрутки (AutoScrollPosition возвращает отрицательные значения)
            Point scrollPos = new Point(panel_map.HorizontalScroll.Value, panel_map.VerticalScroll.Value);
            // Пересчитываем размер PictureBox
            picture_map.Width = (int)(picture_map.Image.Width * zoom);
            picture_map.Height = (int)(picture_map.Image.Height * zoom);
            // Обновляем скролл-бары
            UpdateScrollBars();

            // Координаты мыши относительно панели с учётом прокрутки
            int mouseX = e.Location.X - scrollPos.X; // scrollPos.X отрицательное, поэтому минус
            int mouseY = e.Location.Y - scrollPos.Y;

            int newScrollX = (int)((panel_map.HorizontalScroll.Value + e.X) * (zoom / oldZoom) - e.X);
            int newScrollY = (int)((panel_map.VerticalScroll.Value + e.Y) * (zoom / oldZoom) - e.Y);

            // Вычисляем новую позицию скролла (чтобы курсор оставался на том же месте)
            float zoomRatio = zoom / oldZoom;
            int newX = (int)((panel_map.HorizontalScroll.Value + e.X) * zoomRatio - e.X);
            int newY = (int)((panel_map.VerticalScroll.Value + e.Y) * zoomRatio - e.Y);
            // Применяем новую позицию
            SetScrollPosition(newX, newY);
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            panel_map.Focus();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isDragging = true;
                dragStartPosition = e.Location;
                panelStartPosition = new Point(panel_map.HorizontalScroll.Value, panel_map.VerticalScroll.Value); ;
                panel_map.Cursor = Cursors.SizeAll;
            }
            else if (e.Button == MouseButtons.Left)
            {
                //Console.WriteLine("click left");
                //Console.WriteLine($"isDrawingLine {isDrawingLine}");

                if (isDeletedLine)
                {
                    Console.WriteLine($"before {currentMapData.Lines.Count}");
                    var result = lineHandler.DeleteLine(currentMapData.Lines, new Point(e.X, e.Y), picture_map);
                    Console.WriteLine(result);
                    Console.WriteLine($"after {currentMapData.Lines.Count}");
                    picture_map.Image = currentMapData.Get_Map();   
                    UpdateLinesUI();
                }
                else if (isDrawingLine)
                {
                    Draw_Line(e);
                }
                else if (isDeletedMode)
                {
                    Delete_Obg_on_Map(e);
                }
                else if (use_text)
                {
                    Span_TEXT(e);
                }
                else
                {
                    Span_ZNAK(sender, e);
                }
            }
        }


       

        private void Draw_Line(MouseEventArgs e)
        {
            Image img = picture_map.Image;
            if (img == null) return;

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

            int x = e.X - offsetX;
            int y = e.Y - offsetY;

            float imageX = x / ratio;
            float imageY = y / ratio;

            Point currentLinePoint = new Point((int)imageX, (int)imageY);

            tmpLine.Add(currentLinePoint);
            Bitmap bmp = new Bitmap(picture_map.Image);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                if (tmpLine.Count > 1)
                {
                    if (color_line != null)
                        drawingPen = new Pen(color_line, 10);
                    g.DrawLine(drawingPen, tmpLine[tmpLine.Count - 1].X, tmpLine[tmpLine.Count - 1].Y, tmpLine[tmpLine.Count - 2].X, tmpLine[tmpLine.Count - 2].Y);
                }
            }

            if (picture_map.Image != null)
            {
                picture_map.Image.Dispose();
            }
            picture_map.Image = bmp;

        }

        private void Save_List_Point()
        {
            Console.WriteLine($"tmpLine count: {tmpLine.Count}");
            if (tmpLine.Count != 0)
            {
                Map_Line mapData = new Map_Line(tmpLine, color_line);
                currentMapData.Lines.Add(mapData);
                tmpLine.Clear();
            }
        }

        private void Delete_Obg_on_Map(MouseEventArgs e)
        {
            //Console.WriteLine("Процесс удаление обьекта с карты");
            Image img = picture_map.Image;
            //Console.WriteLine($"img {img} overlay {overlayImage}");
            if (img == null) return;

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

            int x = e.X - offsetX;
            int y = e.Y - offsetY;

            float imageX = x / ratio;
            float imageY = y / ratio;
            Point tap = new Point(((int)imageX), (int)imageY);
            bool isHasMarker = false;

            for (var i = 0; i < currentMapData.Markers.Count; i++)
            {
                var znak = currentMapData.Markers[i];
                double d = Math.Sqrt(Math.Pow((znak.Pos_X - tap.X), 2) + Math.Pow((znak.Pos_Y - tap.Y), 2));
                Console.WriteLine(d);
                if (d < 30 && d > 0)
                {
                    currentMapData.Markers.Remove(znak);
                    isHasMarker = true;
                    break;
                }
            }
            //Console.WriteLine($"hasMarker {isHasMarker}");
            if (!isHasMarker)
            {
                Point textPoint = TranslateZoomMousePosition(e.Location);
                for (var i = 0; i < currentMapData.Texts.Count; i++)
                {
                    var text = currentMapData.Texts[i];
                    double d = Math.Sqrt(Math.Pow((text.Position.X - textPoint.X), 2) + Math.Pow((text.Position.Y - textPoint.Y), 2));
                    Console.WriteLine(d);
                    if (d < 50 && d > 0)
                    {
                        currentMapData.Texts.Remove(text);
                        break;
                    }
                }
            }

            picture_map.Image = currentMapData.Get_Map();
            UpdateMarkersUI(currentMapData.Markers);
            UpdateTextsUI(currentMapData.Texts);
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //Console.WriteLine($"is Drawingssssss {isDrawing}");
            //Console.WriteLine($"is Drawing {lines.Count}");
            if (isDragging && e.Button == MouseButtons.Right)
            {
                int deltaX = e.Location.X - dragStartPosition.X;
                int deltaY = e.Location.Y - dragStartPosition.Y;
                // Рассчитываем новое положение скролла
                int newX = panelStartPosition.X - deltaX;
                int newY = panelStartPosition.Y - deltaY;
                // Устанавливаем новое положение скролла
                panel_map.AutoScrollPosition = new Point(newX, newY);
            }
            else if (isDrawingLine && e.Button == MouseButtons.Left)
            {
                var point = TranslateZoomMousePosition(e.Location);
                if (tmpLine.Count == 0 ||
                    Math.Abs(tmpLine.Last().X - point.X) > 2 ||
                    Math.Abs(tmpLine.Last().Y - point.Y) > 2)
                {
                    tmpLine.Add(point);
                    picture_map.Invalidate();
                }
            }
        }

        private void button_text_Click(object sender, EventArgs e)
        {
            str_set = text_input.Text;
            use_text = true;
            picture_test.Image = null;
            button_text.BackColor = Color.Green;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Map files (*.map)|*.map|All files (*.*)|*.*";
                saveFileDialog.DefaultExt = "map";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = saveFileDialog.FileName;
                    Save_Map save = new Save_Map();
                    save.SaveMapDataToFile(path, currentMapData);
                    MessageBox.Show("Файл сохранён успешно.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button_open_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Map files (*.map)|*.map|All files (*.*)|*.*";
                openFileDialog.DefaultExt = "map";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = openFileDialog.FileName;
                    Save_Map save = new Save_Map();
                    currentMapData = save.LoadMapDataFromFile(path);
                    Console.WriteLine($"currentMap data {currentMapData.Lines.Count}");
                    picture_map.Image = currentMapData.Get_Map();
                    if (picture_map.Image == null)
                    {
                        MessageBox.Show("Нет доступной карты!");
                        return;
                    }
                    UpdateMarkersUI(currentMapData.Markers);
                    UpdateTextsUI(currentMapData.Texts);
                    UpdateLinesUI();
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
           "Переключение карты сотрет все обозначение с карты",
           "Внимание",
            MessageBoxButtons.YesNo,     // Кнопки Да и Нет
           MessageBoxIcon.Question      // Значок вопроса
           );

            if (result == DialogResult.Yes)
            {
                picture_map.Image = Properties.Resources.СВЕТЛОВ;
                currentMapData.Clear_Data();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Переключение карты сотрет все обозначение с карты",
            "Внимание",
            MessageBoxButtons.YesNo,     // Кнопки Да и Нет
            MessageBoxIcon.Question      // Значок вопроса
            );

            if (result == DialogResult.Yes)
            {
                picture_map.Image = Properties.Resources.октябрьской_городок;
                currentMapData.Clear_Data();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Переключение карты сотрет все обозначение с карты",
            "Внимание",
             MessageBoxButtons.YesNo,     // Кнопки Да и Нет
             MessageBoxIcon.Question      // Значок вопроса
            );

            if (result == DialogResult.Yes)
            {
                picture_map.Image = Properties.Resources.ефремов;
                currentMapData.Clear_Data();
            }
        }

        // выбор знака
        private void button_set_protivnik_Click(object sender, EventArgs e)
        {
            isDeletedMode = false;
            isDrawingLine = false;
            button_set_protivnik.BackColor = Color.Blue;
            button_set_own.BackColor = Color.White;
            string selected = comboBox_protivnik.SelectedItem?.ToString();
            if (selected == null) return;
            name_znak = selected;
            Selected_Znak(Save_Map.name_znak_protivnik[selected]);
        }

        private void button_set_own_Click(object sender, EventArgs e)
        {
            isDeletedMode = false;
            isDrawingLine = false;
            button_set_own.BackColor = Color.Red;
            button_set_protivnik.BackColor = Color.White;
            string selected = comboBox_own.SelectedItem?.ToString();
            if (selected == null) return;
            name_znak = selected;
            Selected_Znak(Save_Map.name_znak_own[selected]);
        }

        private void Selected_Znak(string resource_name)
        {
            if (resource_name == null)
            {
                Console.WriteLine("selected is null");
                return;
            }
            ResourceName = resource_name;
            Image image = Map_Marker.Get_Image(resource_name);
            if (image == null)
            {
                MessageBox.Show("Элемента нет", "Внимание",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            int angle = (int)numericUpDown_angle.Value;
            overlayImage = Map_Marker.RotateImage(image, angle);
            picture_test.Image = overlayImage;
            use_text = false;
        }

        private void numericUpDown_angle_ValueChanged(object sender, EventArgs e)
        {
            Selected_Znak(ResourceName);
        }

        private void comboBox_protivnik_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_set_protivnik.BackColor = Color.White;
            button_set_own.BackColor = Color.White;
        }

        private void DelButtonMain_Click(object sender, EventArgs e)
        {
            isDeletedMode = !isDeletedMode;
            drawLineButton.BackColor = Color.White;
            DelButtonMain.BackColor = Color.Gray;
            button_Open_panel_own.BackColor = Color.White;
            button_open_panel_protivnik.BackColor = Color.White;
            if (isDeletedMode) DelButtonMain.BackColor = Color.Gray;
            else DelButtonMain.BackColor = Color.White;
        }

        private void button_Open_panel_own_Click(object sender, EventArgs e)
        {
            isDeletedMode = false;
            isDrawingLine = false;
            drawLineButton.BackColor = Color.White;
            DelButtonMain.BackColor = Color.White;
            button_Open_panel_own.BackColor = Color.Gray;
            button_open_panel_protivnik.BackColor = Color.White;
            flowLayoutPanel_own.Visible = !flowLayoutPanel_own.Visible;
            button_set_own.BackColor = Color.White;
        }

        private void button_open_panel_protivnik_Click(object sender, EventArgs e)
        {
            isDeletedMode = false;
            isDrawingLine = false;
            drawLineButton.BackColor = Color.White;
            DelButtonMain.BackColor = Color.White;
            button_Open_panel_own.BackColor = Color.White;
            button_open_panel_protivnik.BackColor = Color.Gray;
            flowLayoutPanel_protivnik.Visible = !flowLayoutPanel_protivnik.Visible;
            button_set_protivnik.BackColor = Color.White;
        }


        private void drawLineButton_Click(object sender, EventArgs e)
        {
            Panel_draw_line.Visible = !Panel_draw_line.Visible;
        }

        private void button_shoice_color_Click(object sender, EventArgs e)
        {
            if (colorDialog_line.ShowDialog() == DialogResult.OK)
            {
                color_line = colorDialog_line.Color; // пример: смена фона формы
            }
        }

        private void button_set_line_Click(object sender, EventArgs e)
        {
            if (!isDrawingLine)
            {
                isDrawingLine = true;
                drawLineButton.BackColor = Color.Gray;
                tmpLine = new List<Point>();
                Save_List_Point();
            }
            else
            {
                drawLineButton.BackColor = Color.White;
                isDrawingLine = false;
                if (tmpLine != null && tmpLine.Count > 1)
                {
                    // Создаем копию точек для сохрFанения
                    var pointsToSave = new List<Point>(tmpLine);
                    lines.Add(pointsToSave);
                    currentMapData.Lines.Add(new Map_Line(pointsToSave, color_line));
                }
                tmpLine = null;
            }
        }

        private void check_Horizontal_CheckedChanged(object sender, EventArgs e)
        {
            if (overlayImage == null) return;
            overlayImage = Map_Marker.FlipHorizontal(overlayImage);
            picture_test.Image = overlayImage;

        }

        private void check_Vertical_CheckedChanged(object sender, EventArgs e)
        {
            if (overlayImage == null) return;
            overlayImage = Map_Marker.FlipVertical(overlayImage);
            picture_test.Image = overlayImage;

        }

        private void DeleteLine_Click(object sender, EventArgs e)
        {
            if (!isDeletedLine)
            {
                isDeletedLine = true;
                DeleteLine.BackColor = Color.Gray;
            }
            else
            {
                isDeletedLine = false;
                DeleteLine.BackColor = Color.White;
            }
        }
    }
}
