using System.Windows.Forms.DataVisualization.Charting;

namespace Project2
{
    partial class Form_Stock_Visualizer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.openFileDialog_selector = new System.Windows.Forms.OpenFileDialog();
            this.dateTimePicker_end = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_start = new System.Windows.Forms.DateTimePicker();
            this.button_Refresh = new System.Windows.Forms.Button();
            this.chart_OHLCV = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button_fileSelect = new System.Windows.Forms.Button();
            this.timer_Simulation = new System.Windows.Forms.Timer(this.components);
            this.button_Simulate = new System.Windows.Forms.Button();
            this.hScrollBar_Speed = new System.Windows.Forms.HScrollBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.textBox_Speed = new System.Windows.Forms.TextBox();
            this.label_Speed = new System.Windows.Forms.Label();
            this.comboBox_selectPattern = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart_OHLCV)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog_selector
            // 
            this.openFileDialog_selector.DefaultExt = "csv";
            this.openFileDialog_selector.FileName = "AAPL-Day.csv";
            this.openFileDialog_selector.Filter = "\"select stock file\"|*.csv|\"Daily\"|*-Day.csv|\"Weekly\"|*-Week.csv|\"Monthly\"|*-Month" +
    ".csv";
            this.openFileDialog_selector.FilterIndex = 2;
            this.openFileDialog_selector.InitialDirectory = "..\\Stock Data";
            this.openFileDialog_selector.Multiselect = true;
            this.openFileDialog_selector.Title = "File Selector";
            this.openFileDialog_selector.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_selector_fileOK);
            // 
            // dateTimePicker_end
            // 
            this.dateTimePicker_end.Location = new System.Drawing.Point(1243, 739);
            this.dateTimePicker_end.Name = "dateTimePicker_end";
            this.dateTimePicker_end.Size = new System.Drawing.Size(405, 31);
            this.dateTimePicker_end.TabIndex = 10;
            this.dateTimePicker_end.Value = new System.DateTime(2025, 8, 1, 12, 45, 0, 0);
            // 
            // dateTimePicker_start
            // 
            this.dateTimePicker_start.Location = new System.Drawing.Point(360, 740);
            this.dateTimePicker_start.Name = "dateTimePicker_start";
            this.dateTimePicker_start.Size = new System.Drawing.Size(405, 31);
            this.dateTimePicker_start.TabIndex = 9;
            this.dateTimePicker_start.Value = new System.DateTime(2023, 3, 24, 12, 45, 0, 0);
            // 
            // button_Refresh
            // 
            this.button_Refresh.BackColor = System.Drawing.SystemColors.Window;
            this.button_Refresh.Enabled = false;
            this.button_Refresh.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button_Refresh.Location = new System.Drawing.Point(208, 739);
            this.button_Refresh.Name = "button_Refresh";
            this.button_Refresh.Size = new System.Drawing.Size(146, 67);
            this.button_Refresh.TabIndex = 13;
            this.button_Refresh.Text = "Refresh";
            this.button_Refresh.UseVisualStyleBackColor = false;
            this.button_Refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // chart_OHLCV
            // 
            chartArea1.Name = "ChartArea_OHLC";
            chartArea2.AlignWithChartArea = "ChartArea_OHLC";
            chartArea2.Name = "ChartArea_Volume";
            this.chart_OHLCV.ChartAreas.Add(chartArea1);
            this.chart_OHLCV.ChartAreas.Add(chartArea2);
            this.chart_OHLCV.Location = new System.Drawing.Point(12, 0);
            this.chart_OHLCV.Name = "chart_OHLCV";
            this.chart_OHLCV.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            this.chart_OHLCV.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black};
            series1.ChartArea = "ChartArea_OHLC";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.CustomProperties = "PriceDownColor=Red, PriceUpColor=0\\, 192\\, 0";
            series1.IsXValueIndexed = true;
            series1.Name = "Series_OHLC";
            series1.XValueMember = "Date";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series1.YValueMembers = "High,Low,Open,Close";
            series1.YValuesPerPoint = 4;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series2.ChartArea = "ChartArea_Volume";
            series2.IsXValueIndexed = true;
            series2.Name = "Series_Volume";
            series2.XValueMember = "Date";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series2.YValueMembers = "Volume";
            this.chart_OHLCV.Series.Add(series1);
            this.chart_OHLCV.Series.Add(series2);
            this.chart_OHLCV.Size = new System.Drawing.Size(1949, 723);
            this.chart_OHLCV.TabIndex = 14;
            this.chart_OHLCV.Text = "chart_OHLCV";
            title1.Name = "title_Volume";
            title1.Position.Auto = false;
            title1.Position.Height = 6.01177F;
            title1.Position.Width = 94F;
            title1.Position.X = 3F;
            title1.Position.Y = 47F;
            title1.Text = "Volume";
            title1.Visible = false;
            this.chart_OHLCV.Titles.Add(title1);
            // 
            // button_fileSelect
            // 
            this.button_fileSelect.BackColor = System.Drawing.SystemColors.Window;
            this.button_fileSelect.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_fileSelect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button_fileSelect.Location = new System.Drawing.Point(789, 740);
            this.button_fileSelect.Name = "button_fileSelect";
            this.button_fileSelect.Size = new System.Drawing.Size(246, 67);
            this.button_fileSelect.TabIndex = 7;
            this.button_fileSelect.Text = "Select Ticker File:";
            this.button_fileSelect.UseVisualStyleBackColor = false;
            this.button_fileSelect.Click += new System.EventHandler(this.button_fileSelect_Click);
            // 
            // timer_Simulation
            // 
            this.timer_Simulation.Interval = 2000;
            this.timer_Simulation.Tick += new System.EventHandler(this.timer_Simulation_Tick);
            // 
            // button_Simulate
            // 
            this.button_Simulate.Location = new System.Drawing.Point(39, 739);
            this.button_Simulate.Name = "button_Simulate";
            this.button_Simulate.Size = new System.Drawing.Size(146, 67);
            this.button_Simulate.TabIndex = 15;
            this.button_Simulate.Text = "Simulate";
            this.button_Simulate.UseVisualStyleBackColor = true;
            this.button_Simulate.Click += new System.EventHandler(this.button_simulate_Click);
            // 
            // hScrollBar_Speed
            // 
            this.hScrollBar_Speed.Location = new System.Drawing.Point(194, 809);
            this.hScrollBar_Speed.Maximum = 2000;
            this.hScrollBar_Speed.Minimum = 1;
            this.hScrollBar_Speed.Name = "hScrollBar_Speed";
            this.hScrollBar_Speed.Size = new System.Drawing.Size(1328, 41);
            this.hScrollBar_Speed.TabIndex = 16;
            this.hScrollBar_Speed.Value = 1;
            this.hScrollBar_Speed.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Speed_Scroll);
            // 
            // textBox_Speed
            // 
            this.textBox_Speed.Location = new System.Drawing.Point(1548, 819);
            this.textBox_Speed.Name = "textBox_Speed";
            this.textBox_Speed.Size = new System.Drawing.Size(100, 31);
            this.textBox_Speed.TabIndex = 17;
            this.textBox_Speed.TextChanged += new System.EventHandler(this.textBox_Speed_TextChanged);
            // 
            // label_Speed
            // 
            this.label_Speed.AutoSize = true;
            this.label_Speed.Location = new System.Drawing.Point(34, 819);
            this.label_Speed.Name = "label_Speed";
            this.label_Speed.Size = new System.Drawing.Size(145, 25);
            this.label_Speed.TabIndex = 18;
            this.label_Speed.Text = "Speed (in ms)";
            // 
            // comboBox_selectPattern
            // 
            this.comboBox_selectPattern.Enabled = false;
            this.comboBox_selectPattern.FormattingEnabled = true;
            this.comboBox_selectPattern.Items.AddRange(new object[] {
            "Doji",
            "Marubozu",
            "Hammer",
            "Inverted Hammer",
            "Engulfing",
            "Harami"});
            this.comboBox_selectPattern.Location = new System.Drawing.Point(1078, 740);
            this.comboBox_selectPattern.Name = "comboBox_selectPattern";
            this.comboBox_selectPattern.Size = new System.Drawing.Size(121, 33);
            this.comboBox_selectPattern.TabIndex = 19;
            this.comboBox_selectPattern.SelectedIndexChanged += new System.EventHandler(this.comboBox_selectPattern_SelectedIndexChanged);
            // 
            // Form_Stock_Visualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1984, 856);
            this.Controls.Add(this.comboBox_selectPattern);
            this.Controls.Add(this.label_Speed);
            this.Controls.Add(this.textBox_Speed);
            this.Controls.Add(this.hScrollBar_Speed);
            this.Controls.Add(this.button_Simulate);
            this.Controls.Add(this.chart_OHLCV);
            this.Controls.Add(this.button_Refresh);
            this.Controls.Add(this.button_fileSelect);
            this.Controls.Add(this.dateTimePicker_end);
            this.Controls.Add(this.dateTimePicker_start);
            this.Name = "Form_Stock_Visualizer";
            this.Text = "Form_Stock_Visualizer";
            ((System.ComponentModel.ISupportInitialize)(this.chart_OHLCV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog_selector;
        private System.Windows.Forms.DateTimePicker dateTimePicker_end;
        private System.Windows.Forms.DateTimePicker dateTimePicker_start;
        private System.Windows.Forms.Button button_Refresh;
        private Chart chart_OHLCV;
        private System.Windows.Forms.Button button_fileSelect;
        private System.Windows.Forms.Timer timer_Simulation;
        private System.Windows.Forms.Button button_Simulate;
        private System.Windows.Forms.HScrollBar hScrollBar_Speed;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox textBox_Speed;
        private System.Windows.Forms.Label label_Speed;
        private System.Windows.Forms.ComboBox comboBox_selectPattern;
    }
}