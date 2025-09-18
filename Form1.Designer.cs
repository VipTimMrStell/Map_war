
namespace Map_war
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_save = new System.Windows.Forms.Button();
            this.button_text = new System.Windows.Forms.Button();
            this.text_input = new System.Windows.Forms.TextBox();
            this.button_open = new System.Windows.Forms.Button();
            this.button_map_1 = new System.Windows.Forms.Button();
            this.button_map_3 = new System.Windows.Forms.Button();
            this.button_map_2 = new System.Windows.Forms.Button();
            this.panel_map = new System.Windows.Forms.Panel();
            this.picture_map = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.check_Horizontal = new System.Windows.Forms.CheckBox();
            this.check_Vertical = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown_angle = new System.Windows.Forms.NumericUpDown();
            this.picture_test = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel_protivnik = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_protivnik = new System.Windows.Forms.ComboBox();
            this.button_set_protivnik = new System.Windows.Forms.Button();
            this.flowLayoutPanel_own = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_own = new System.Windows.Forms.ComboBox();
            this.button_set_own = new System.Windows.Forms.Button();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.DeleteLine = new System.Windows.Forms.Button();
            this.drawLineButton = new System.Windows.Forms.Button();
            this.button_Open_panel_own = new System.Windows.Forms.Button();
            this.button_open_panel_protivnik = new System.Windows.Forms.Button();
            this.DelButtonMain = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Panel_draw_line = new System.Windows.Forms.FlowLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.button_shoice_color = new System.Windows.Forms.Button();
            this.button_set_line = new System.Windows.Forms.Button();
            this.colorDialog_line = new System.Windows.Forms.ColorDialog();
            this.panel_map.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture_map)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_angle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_test)).BeginInit();
            this.flowLayoutPanel_protivnik.SuspendLayout();
            this.flowLayoutPanel_own.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.Panel_draw_line.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(3, 511);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(152, 23);
            this.button_save.TabIndex = 0;
            this.button_save.Text = "Сохрание карты";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // button_text
            // 
            this.button_text.Location = new System.Drawing.Point(3, 319);
            this.button_text.Name = "button_text";
            this.button_text.Size = new System.Drawing.Size(152, 29);
            this.button_text.TabIndex = 5;
            this.button_text.Text = "Добавить";
            this.button_text.UseVisualStyleBackColor = true;
            this.button_text.Click += new System.EventHandler(this.button_text_Click);
            // 
            // text_input
            // 
            this.text_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.text_input.Location = new System.Drawing.Point(3, 284);
            this.text_input.Name = "text_input";
            this.text_input.Size = new System.Drawing.Size(152, 29);
            this.text_input.TabIndex = 8;
            // 
            // button_open
            // 
            this.button_open.Location = new System.Drawing.Point(3, 482);
            this.button_open.Name = "button_open";
            this.button_open.Size = new System.Drawing.Size(152, 23);
            this.button_open.TabIndex = 6;
            this.button_open.Text = "Открыть карту";
            this.button_open.UseVisualStyleBackColor = true;
            this.button_open.Click += new System.EventHandler(this.button_open_Click);
            // 
            // button_map_1
            // 
            this.button_map_1.Location = new System.Drawing.Point(3, 369);
            this.button_map_1.Name = "button_map_1";
            this.button_map_1.Size = new System.Drawing.Size(152, 23);
            this.button_map_1.TabIndex = 9;
            this.button_map_1.Text = "Светлов";
            this.button_map_1.UseVisualStyleBackColor = true;
            this.button_map_1.Click += new System.EventHandler(this.button12_Click);
            // 
            // button_map_3
            // 
            this.button_map_3.Location = new System.Drawing.Point(3, 398);
            this.button_map_3.Name = "button_map_3";
            this.button_map_3.Size = new System.Drawing.Size(152, 23);
            this.button_map_3.TabIndex = 8;
            this.button_map_3.Text = "Ефремов";
            this.button_map_3.UseVisualStyleBackColor = true;
            this.button_map_3.Click += new System.EventHandler(this.button11_Click);
            // 
            // button_map_2
            // 
            this.button_map_2.Location = new System.Drawing.Point(3, 427);
            this.button_map_2.Name = "button_map_2";
            this.button_map_2.Size = new System.Drawing.Size(152, 23);
            this.button_map_2.TabIndex = 7;
            this.button_map_2.Text = "Октябрьский городок";
            this.button_map_2.UseVisualStyleBackColor = true;
            this.button_map_2.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel_map
            // 
            this.panel_map.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_map.AutoScroll = true;
            this.panel_map.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_map.Controls.Add(this.picture_map);
            this.panel_map.ImeMode = System.Windows.Forms.ImeMode.On;
            this.panel_map.Location = new System.Drawing.Point(189, 12);
            this.panel_map.MaximumSize = new System.Drawing.Size(2000, 1200);
            this.panel_map.MinimumSize = new System.Drawing.Size(870, 740);
            this.panel_map.Name = "panel_map";
            this.panel_map.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel_map.Size = new System.Drawing.Size(983, 814);
            this.panel_map.TabIndex = 1;
            this.panel_map.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            // 
            // picture_map
            // 
            this.picture_map.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picture_map.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.picture_map.Image = global::Map_war.Properties.Resources.СВЕТЛОВ;
            this.picture_map.Location = new System.Drawing.Point(0, 0);
            this.picture_map.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.picture_map.Name = "picture_map";
            this.picture_map.Padding = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.picture_map.Size = new System.Drawing.Size(850, 1303);
            this.picture_map.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picture_map.TabIndex = 0;
            this.picture_map.TabStop = false;
            this.picture_map.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.picture_map.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(5, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Свойства знака";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.check_Horizontal);
            this.flowLayoutPanel1.Controls.Add(this.check_Vertical);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel7);
            this.flowLayoutPanel1.Controls.Add(this.picture_test);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(167, 260);
            this.flowLayoutPanel1.TabIndex = 17;
            // 
            // check_Horizontal
            // 
            this.check_Horizontal.AutoSize = true;
            this.check_Horizontal.Location = new System.Drawing.Point(4, 5);
            this.check_Horizontal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.check_Horizontal.Name = "check_Horizontal";
            this.check_Horizontal.Size = new System.Drawing.Size(159, 30);
            this.check_Horizontal.TabIndex = 47;
            this.check_Horizontal.Text = "зеркальное отображение \r\nпо горезонтали";
            this.check_Horizontal.UseVisualStyleBackColor = true;
            this.check_Horizontal.CheckedChanged += new System.EventHandler(this.check_Horizontal_CheckedChanged);
            // 
            // check_Vertical
            // 
            this.check_Vertical.AutoSize = true;
            this.check_Vertical.Location = new System.Drawing.Point(4, 39);
            this.check_Vertical.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.check_Vertical.Name = "check_Vertical";
            this.check_Vertical.Size = new System.Drawing.Size(159, 30);
            this.check_Vertical.TabIndex = 46;
            this.check_Vertical.Text = "зеркальное отображение \r\nпо вертикали";
            this.check_Vertical.UseVisualStyleBackColor = true;
            this.check_Vertical.CheckedChanged += new System.EventHandler(this.check_Vertical_CheckedChanged);
            // 
            // flowLayoutPanel7
            // 
            this.flowLayoutPanel7.Controls.Add(this.label7);
            this.flowLayoutPanel7.Controls.Add(this.numericUpDown_angle);
            this.flowLayoutPanel7.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel7.Location = new System.Drawing.Point(5, 94);
            this.flowLayoutPanel7.Name = "flowLayoutPanel7";
            this.flowLayoutPanel7.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.flowLayoutPanel7.Size = new System.Drawing.Size(163, 30);
            this.flowLayoutPanel7.TabIndex = 45;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(6, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 44;
            this.label7.Text = "Угол наклона";
            // 
            // numericUpDown_angle
            // 
            this.numericUpDown_angle.Location = new System.Drawing.Point(89, 6);
            this.numericUpDown_angle.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
            this.numericUpDown_angle.Name = "numericUpDown_angle";
            this.numericUpDown_angle.Size = new System.Drawing.Size(58, 20);
            this.numericUpDown_angle.TabIndex = 43;
            this.numericUpDown_angle.ValueChanged += new System.EventHandler(this.numericUpDown_angle_ValueChanged);
            // 
            // picture_test
            // 
            this.picture_test.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picture_test.Location = new System.Drawing.Point(5, 130);
            this.picture_test.Name = "picture_test";
            this.picture_test.Size = new System.Drawing.Size(120, 120);
            this.picture_test.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture_test.TabIndex = 12;
            this.picture_test.TabStop = false;
            // 
            // flowLayoutPanel_protivnik
            // 
            this.flowLayoutPanel_protivnik.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel_protivnik.Controls.Add(this.label1);
            this.flowLayoutPanel_protivnik.Controls.Add(this.comboBox_protivnik);
            this.flowLayoutPanel_protivnik.Controls.Add(this.button_set_protivnik);
            this.flowLayoutPanel_protivnik.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_protivnik.Location = new System.Drawing.Point(188, 406);
            this.flowLayoutPanel_protivnik.Name = "flowLayoutPanel_protivnik";
            this.flowLayoutPanel_protivnik.Size = new System.Drawing.Size(351, 79);
            this.flowLayoutPanel_protivnik.TabIndex = 18;
            this.flowLayoutPanel_protivnik.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Обозначение противника";
            // 
            // comboBox_protivnik
            // 
            this.comboBox_protivnik.FormattingEnabled = true;
            this.comboBox_protivnik.Items.AddRange(new object[] {
            ""});
            this.comboBox_protivnik.Location = new System.Drawing.Point(3, 16);
            this.comboBox_protivnik.Name = "comboBox_protivnik";
            this.comboBox_protivnik.Size = new System.Drawing.Size(342, 21);
            this.comboBox_protivnik.TabIndex = 38;
            this.comboBox_protivnik.SelectedIndexChanged += new System.EventHandler(this.comboBox_protivnik_SelectedIndexChanged);
            // 
            // button_set_protivnik
            // 
            this.button_set_protivnik.Location = new System.Drawing.Point(0, 40);
            this.button_set_protivnik.Margin = new System.Windows.Forms.Padding(0);
            this.button_set_protivnik.Name = "button_set_protivnik";
            this.button_set_protivnik.Size = new System.Drawing.Size(345, 23);
            this.button_set_protivnik.TabIndex = 38;
            this.button_set_protivnik.Text = "Выбрать";
            this.button_set_protivnik.UseVisualStyleBackColor = true;
            this.button_set_protivnik.Click += new System.EventHandler(this.button_set_protivnik_Click);
            // 
            // flowLayoutPanel_own
            // 
            this.flowLayoutPanel_own.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel_own.Controls.Add(this.label3);
            this.flowLayoutPanel_own.Controls.Add(this.comboBox_own);
            this.flowLayoutPanel_own.Controls.Add(this.button_set_own);
            this.flowLayoutPanel_own.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_own.Location = new System.Drawing.Point(187, 321);
            this.flowLayoutPanel_own.Name = "flowLayoutPanel_own";
            this.flowLayoutPanel_own.Size = new System.Drawing.Size(351, 79);
            this.flowLayoutPanel_own.TabIndex = 39;
            this.flowLayoutPanel_own.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Обозначение своих";
            // 
            // comboBox_own
            // 
            this.comboBox_own.FormattingEnabled = true;
            this.comboBox_own.Location = new System.Drawing.Point(3, 16);
            this.comboBox_own.Name = "comboBox_own";
            this.comboBox_own.Size = new System.Drawing.Size(342, 21);
            this.comboBox_own.TabIndex = 38;
            this.comboBox_own.SelectedIndexChanged += new System.EventHandler(this.comboBox_protivnik_SelectedIndexChanged);
            // 
            // button_set_own
            // 
            this.button_set_own.Location = new System.Drawing.Point(0, 40);
            this.button_set_own.Margin = new System.Windows.Forms.Padding(0);
            this.button_set_own.Name = "button_set_own";
            this.button_set_own.Size = new System.Drawing.Size(345, 23);
            this.button_set_own.TabIndex = 38;
            this.button_set_own.Text = "Выбрать";
            this.button_set_own.UseVisualStyleBackColor = true;
            this.button_set_own.Click += new System.EventHandler(this.button_set_own_Click);
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel4.Controls.Add(this.label4);
            this.flowLayoutPanel4.Controls.Add(this.DeleteLine);
            this.flowLayoutPanel4.Controls.Add(this.drawLineButton);
            this.flowLayoutPanel4.Controls.Add(this.button_Open_panel_own);
            this.flowLayoutPanel4.Controls.Add(this.button_open_panel_protivnik);
            this.flowLayoutPanel4.Controls.Add(this.DelButtonMain);
            this.flowLayoutPanel4.Controls.Add(this.label6);
            this.flowLayoutPanel4.Controls.Add(this.text_input);
            this.flowLayoutPanel4.Controls.Add(this.button_text);
            this.flowLayoutPanel4.Controls.Add(this.label5);
            this.flowLayoutPanel4.Controls.Add(this.button_map_1);
            this.flowLayoutPanel4.Controls.Add(this.button_map_3);
            this.flowLayoutPanel4.Controls.Add(this.button_map_2);
            this.flowLayoutPanel4.Controls.Add(this.label8);
            this.flowLayoutPanel4.Controls.Add(this.button_open);
            this.flowLayoutPanel4.Controls.Add(this.button_save);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(6, 278);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(167, 548);
            this.flowLayoutPanel4.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 15);
            this.label4.TabIndex = 20;
            this.label4.Text = "Управление";
            // 
            // DeleteLine
            // 
            this.DeleteLine.Location = new System.Drawing.Point(2, 17);
            this.DeleteLine.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DeleteLine.Name = "DeleteLine";
            this.DeleteLine.Size = new System.Drawing.Size(154, 35);
            this.DeleteLine.TabIndex = 49;
            this.DeleteLine.Text = "Удаление маршрута";
            this.DeleteLine.UseVisualStyleBackColor = true;
            this.DeleteLine.Click += new System.EventHandler(this.DeleteLine_Click);
            // 
            // drawLineButton
            // 
            this.drawLineButton.Location = new System.Drawing.Point(2, 56);
            this.drawLineButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.drawLineButton.Name = "drawLineButton";
            this.drawLineButton.Size = new System.Drawing.Size(153, 49);
            this.drawLineButton.TabIndex = 48;
            this.drawLineButton.Text = "Рисование маршрута";
            this.drawLineButton.UseVisualStyleBackColor = true;
            this.drawLineButton.Click += new System.EventHandler(this.drawLineButton_Click);
            // 
            // button_Open_panel_own
            // 
            this.button_Open_panel_own.Location = new System.Drawing.Point(2, 109);
            this.button_Open_panel_own.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_Open_panel_own.Name = "button_Open_panel_own";
            this.button_Open_panel_own.Size = new System.Drawing.Size(153, 49);
            this.button_Open_panel_own.TabIndex = 45;
            this.button_Open_panel_own.Text = "Выбор значков своих";
            this.button_Open_panel_own.UseVisualStyleBackColor = true;
            this.button_Open_panel_own.Click += new System.EventHandler(this.button_Open_panel_own_Click);
            // 
            // button_open_panel_protivnik
            // 
            this.button_open_panel_protivnik.Location = new System.Drawing.Point(2, 162);
            this.button_open_panel_protivnik.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_open_panel_protivnik.Name = "button_open_panel_protivnik";
            this.button_open_panel_protivnik.Size = new System.Drawing.Size(153, 49);
            this.button_open_panel_protivnik.TabIndex = 44;
            this.button_open_panel_protivnik.Text = "Выбор значков противника";
            this.button_open_panel_protivnik.UseVisualStyleBackColor = true;
            this.button_open_panel_protivnik.Click += new System.EventHandler(this.button_open_panel_protivnik_Click);
            // 
            // DelButtonMain
            // 
            this.DelButtonMain.Location = new System.Drawing.Point(2, 215);
            this.DelButtonMain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DelButtonMain.Name = "DelButtonMain";
            this.DelButtonMain.Size = new System.Drawing.Size(153, 49);
            this.DelButtonMain.TabIndex = 38;
            this.DelButtonMain.Text = "Удалить обьект с карты";
            this.DelButtonMain.UseVisualStyleBackColor = true;
            this.DelButtonMain.Click += new System.EventHandler(this.DelButtonMain_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(3, 266);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 15);
            this.label6.TabIndex = 43;
            this.label6.Text = "Обозначение на карту";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(3, 351);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 46;
            this.label5.Text = "Выбор карты";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(3, 453);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 26);
            this.label8.TabIndex = 47;
            this.label8.Text = "Экспортирование карты\r\nИмпортирование карты";
            // 
            // Panel_draw_line
            // 
            this.Panel_draw_line.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_draw_line.Controls.Add(this.label9);
            this.Panel_draw_line.Controls.Add(this.button_shoice_color);
            this.Panel_draw_line.Controls.Add(this.button_set_line);
            this.Panel_draw_line.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.Panel_draw_line.Location = new System.Drawing.Point(187, 235);
            this.Panel_draw_line.Name = "Panel_draw_line";
            this.Panel_draw_line.Size = new System.Drawing.Size(351, 79);
            this.Panel_draw_line.TabIndex = 40;
            this.Panel_draw_line.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Свойства маршрута";
            // 
            // button_shoice_color
            // 
            this.button_shoice_color.Location = new System.Drawing.Point(0, 13);
            this.button_shoice_color.Margin = new System.Windows.Forms.Padding(0);
            this.button_shoice_color.Name = "button_shoice_color";
            this.button_shoice_color.Size = new System.Drawing.Size(345, 23);
            this.button_shoice_color.TabIndex = 38;
            this.button_shoice_color.Text = "Выбрать цвет";
            this.button_shoice_color.UseVisualStyleBackColor = true;
            this.button_shoice_color.Click += new System.EventHandler(this.button_shoice_color_Click);
            // 
            // button_set_line
            // 
            this.button_set_line.Location = new System.Drawing.Point(0, 36);
            this.button_set_line.Margin = new System.Windows.Forms.Padding(0);
            this.button_set_line.Name = "button_set_line";
            this.button_set_line.Size = new System.Drawing.Size(345, 23);
            this.button_set_line.TabIndex = 39;
            this.button_set_line.Text = "Установить маршрут";
            this.button_set_line.UseVisualStyleBackColor = true;
            this.button_set_line.Click += new System.EventHandler(this.button_set_line_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 841);
            this.Controls.Add(this.flowLayoutPanel4);
            this.Controls.Add(this.flowLayoutPanel_protivnik);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.Panel_draw_line);
            this.Controls.Add(this.flowLayoutPanel_own);
            this.Controls.Add(this.panel_map);
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(1200, 880);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Карта тактической задачи";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel_map.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picture_map)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel7.ResumeLayout(false);
            this.flowLayoutPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_angle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_test)).EndInit();
            this.flowLayoutPanel_protivnik.ResumeLayout(false);
            this.flowLayoutPanel_protivnik.PerformLayout();
            this.flowLayoutPanel_own.ResumeLayout(false);
            this.flowLayoutPanel_own.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.Panel_draw_line.ResumeLayout(false);
            this.Panel_draw_line.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_open;
        private System.Windows.Forms.Button button_text;
        private System.Windows.Forms.TextBox text_input;
        private System.Windows.Forms.Button button_map_3;
        private System.Windows.Forms.Button button_map_2;
        private System.Windows.Forms.Button button_map_1;
        private System.Windows.Forms.PictureBox picture_map;
        private System.Windows.Forms.Panel panel_map;
        private System.Windows.Forms.PictureBox picture_test;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_protivnik;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_protivnik;
        private System.Windows.Forms.Button button_set_protivnik;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_own;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_own;
        private System.Windows.Forms.Button button_set_own;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown_angle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.Button DelButton;
        private System.Windows.Forms.Button DelButtonMain;
        private System.Windows.Forms.Button button_Open_panel_own;
        private System.Windows.Forms.Button button_open_panel_protivnik;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button drawLineButton;
        private System.Windows.Forms.FlowLayoutPanel Panel_draw_line;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button_shoice_color;
        private System.Windows.Forms.ColorDialog colorDialog_line;
        private System.Windows.Forms.Button button_set_line;
        private System.Windows.Forms.Button DeleteLine;
        private System.Windows.Forms.CheckBox check_Horizontal;
        private System.Windows.Forms.CheckBox check_Vertical;
    }
}

