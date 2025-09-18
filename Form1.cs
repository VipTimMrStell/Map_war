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
        // Выделение области для удаления
        private bool isSelectingDelete = false;
        private Point selectionStartClient;
        private Point selectionEndClient;

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
            picture_map.Paint += picture_map_Paint;
            picture_map.Location = new Point(0, 0);
            panel_map.Controls.Add(picture_map);
            picture_map.MouseUp += picture_map_MouseUp;
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
                // Запоминаем текущие позиции скролла для корректного перетаскивания
                scrollStartPosition = new Point(panel_map.HorizontalScroll.Value, panel_map.VerticalScroll.Value);
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

            Bitmap bmp = new Bitmap(picture_map.Image);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                var penColor = line.color.IsEmpty ? Color.Black : line.color;
                using (var pen = new Pen(penColor, 10))
                {
                    for (var i = 1; i < line.Points.Count; i++)
                    {
                        g.DrawLine(pen, line.Points[i - 1], line.Points[i]);
                    }
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

            int pbWidth = picture_map.Width - picture_map.Padding.Horizontal;
            int pbHeight = picture_map.Height - picture_map.Padding.Vertical;

            float imageAspect = (float)imgWidth / imgHeight;
            float controlAspect = (float)pbWidth / pbHeight;

            float scaleFactor;
            int offsetX, offsetY;

            if (imageAspect > controlAspect)
            {
                scaleFactor = (float)pbWidth / imgWidth;
                offsetX = picture_map.Padding.Left;
                offsetY = picture_map.Padding.Top + (int)((pbHeight - imgHeight * scaleFactor) / 2);
            }
            else
            {
                scaleFactor = (float)pbHeight / imgHeight;
                offsetX = picture_map.Padding.Left + (int)((pbWidth - imgWidth * scaleFactor) / 2);
                offsetY = picture_map.Padding.Top;
            }

            int x = (int)((coordinates.X - offsetX) / scaleFactor);
            int y = (int)((coordinates.Y - offsetY) / scaleFactor);

            return new Point(x, y);
        }

        private Rectangle GetImageRectFromClientPoints(Point clientA, Point clientB)
        {
            Point a = TranslateZoomMousePosition(clientA);
            Point b = TranslateZoomMousePosition(clientB);
            int x = Math.Min(a.X, b.X);
            int y = Math.Min(a.Y, b.Y);
            int w = Math.Abs(a.X - b.X);
            int h = Math.Abs(a.Y - b.Y);
            return new Rectangle(x, y, w, h);
        }

        private void picture_map_Paint(object sender, PaintEventArgs e)
        {
            if (isSelectingDelete)
            {
                var rc = new Rectangle(
                    Math.Min(selectionStartClient.X, selectionEndClient.X),
                    Math.Min(selectionStartClient.Y, selectionEndClient.Y),
                    Math.Abs(selectionStartClient.X - selectionEndClient.X),
                    Math.Abs(selectionStartClient.Y - selectionEndClient.Y));
                using (var pen = new Pen(Color.Red, 1))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    e.Graphics.DrawRectangle(pen, rc);
                }
            }
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
            UpdateScrollBars();
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
                    // Начинаем выделение области для удаления маршрутов
                    isSelectingDelete = true;
                    selectionStartClient = e.Location;
                    selectionEndClient = e.Location;
                    picture_map.Invalidate();
                }
                else if (isDrawingLine)
                {
                    Draw_Line(e);
                }
                else if (isDeletedMode)
                {
                    // Начинаем выделение области для удаления объектов
                    isSelectingDelete = true;
                    selectionStartClient = e.Location;
                    selectionEndClient = e.Location;
                    picture_map.Invalidate();
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
                    if (!color_line.IsEmpty)
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
            else if (isSelectingDelete && (isDeletedMode || isDeletedLine) && e.Button == MouseButtons.Left)
            {
                selectionEndClient = e.Location;
                picture_map.Invalidate();
            }
        }

        private void picture_map_MouseUp(object sender, MouseEventArgs e)
        {
            if (isSelectingDelete && e.Button == MouseButtons.Left)
            {
                var rect = GetImageRectFromClientPoints(selectionStartClient, selectionEndClient);
                // Если почти клик — делаем небольшой прямоугольник
                if (rect.Width < 4 && rect.Height < 4)
                {
                    var p = TranslateZoomMousePosition(selectionStartClient);
                    rect = new Rectangle(p.X - 4, p.Y - 4, 8, 8);
                }

                if (isDeletedLine)
                {
                    DeleteLinesInRect(rect);
                }
                else if (isDeletedMode)
                {
                    DeleteObjectsInRect(rect);
                }

                isSelectingDelete = false;
                picture_map.Image = currentMapData.Get_Map();
                UpdateMarkersUI(currentMapData.Markers);
                UpdateTextsUI(currentMapData.Texts);
                UpdateLinesUI();
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
            if (isDeletedMode) { isDeletedLine = false; }
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
                isDeletedMode = false;
            }
            else
            {
                isDeletedLine = false;
                DeleteLine.BackColor = Color.White;
            }
        }

        private void DeleteLinesInRect(Rectangle imgRect)
        {
            for (int i = currentMapData.Lines.Count - 1; i >= 0; i--)
            {
                var line = currentMapData.Lines[i];
                bool intersects = false;
                if (line.Points.Any(pt => imgRect.Contains(pt)))
                {
                    intersects = true;
                }
                else
                {
                    for (int j = 1; j < line.Points.Count && !intersects; j++)
                    {
                        if (SegmentIntersectsRectangle(line.Points[j - 1], line.Points[j], imgRect))
                            intersects = true;
                    }
                }
                if (intersects)
                {
                    currentMapData.Lines.RemoveAt(i);
                }
            }
        }

        private void DeleteObjectsInRect(Rectangle imgRect)
        {
            for (int i = currentMapData.Markers.Count - 1; i >= 0; i--)
            {
                var m = currentMapData.Markers[i];
                if (imgRect.Contains((int)m.Pos_X, (int)m.Pos_Y))
                {
                    currentMapData.Markers.RemoveAt(i);
                }
            }
            for (int i = currentMapData.Texts.Count - 1; i >= 0; i--)
            {
                var t = currentMapData.Texts[i];
                if (imgRect.Contains(t.Position))
                {
                    currentMapData.Texts.RemoveAt(i);
                }
            }
        }

        private bool SegmentIntersectsRectangle(Point a, Point b, Rectangle r)
        {
            if (r.Contains(a) || r.Contains(b)) return true;
            var r1 = new Point(r.Left, r.Top);
            var r2 = new Point(r.Right, r.Top);
            var r3 = new Point(r.Right, r.Bottom);
            var r4 = new Point(r.Left, r.Bottom);
            return SegmentsIntersect(a, b, r1, r2) ||
                   SegmentsIntersect(a, b, r2, r3) ||
                   SegmentsIntersect(a, b, r3, r4) ||
                   SegmentsIntersect(a, b, r4, r1);
        }

        private static int Orientation(Point p, Point q, Point r)
        {
            long val = (long)(q.Y - p.Y) * (r.X - q.X) - (long)(q.X - p.X) * (r.Y - q.Y);
            if (val == 0) return 0;
            return (val > 0) ? 1 : 2;
        }

        private static bool OnSegment(Point p, Point q, Point r)
        {
            return q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                   q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y);
        }

        private static bool SegmentsIntersect(Point p1, Point q1, Point p2, Point q2)
        {
            int o1 = Orientation(p1, q1, p2);
            int o2 = Orientation(p1, q1, q2);
            int o3 = Orientation(p2, q2, p1);
            int o4 = Orientation(p2, q2, q1);

            if (o1 != o2 && o3 != o4) return true;

            if (o1 == 0 && OnSegment(p1, p2, q1)) return true;
            if (o2 == 0 && OnSegment(p1, q2, q1)) return true;
            if (o3 == 0 && OnSegment(p2, p1, q2)) return true;
            if (o4 == 0 && OnSegment(p2, q1, q2)) return true;
            return false;
        }
    }
}
