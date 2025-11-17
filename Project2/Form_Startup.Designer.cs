namespace Project2
{
    partial class Form_Startup
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
            this.label_Start = new System.Windows.Forms.Label();
            this.label_End = new System.Windows.Forms.Label();
            this.dateTimePicker_Start = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_End = new System.Windows.Forms.DateTimePicker();
            this.button_LoadTickers = new System.Windows.Forms.Button();
            this.openFileDialog_selector = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label_Start
            // 
            this.label_Start.AutoSize = true;
            this.label_Start.Location = new System.Drawing.Point(12, 47);
            this.label_Start.Name = "label_Start";
            this.label_Start.Size = new System.Drawing.Size(108, 25);
            this.label_Start.TabIndex = 0;
            this.label_Start.Text = "Start Date";
            // 
            // label_End
            // 
            this.label_End.AutoSize = true;
            this.label_End.Location = new System.Drawing.Point(775, 52);
            this.label_End.Name = "label_End";
            this.label_End.Size = new System.Drawing.Size(101, 25);
            this.label_End.TabIndex = 1;
            this.label_End.Text = "End Date";
            // 
            // dateTimePicker_Start
            // 
            this.dateTimePicker_Start.Location = new System.Drawing.Point(152, 47);
            this.dateTimePicker_Start.Name = "dateTimePicker_Start";
            this.dateTimePicker_Start.Size = new System.Drawing.Size(420, 31);
            this.dateTimePicker_Start.TabIndex = 2;
            this.dateTimePicker_Start.Value = new System.DateTime(2023, 3, 1, 0, 0, 0, 0);
            // 
            // dateTimePicker_End
            // 
            this.dateTimePicker_End.Location = new System.Drawing.Point(900, 47);
            this.dateTimePicker_End.Name = "dateTimePicker_End";
            this.dateTimePicker_End.Size = new System.Drawing.Size(408, 31);
            this.dateTimePicker_End.TabIndex = 3;
            this.dateTimePicker_End.Value = new System.DateTime(2023, 5, 1, 0, 0, 0, 0);
            // 
            // button_LoadTickers
            // 
            this.button_LoadTickers.Location = new System.Drawing.Point(596, 41);
            this.button_LoadTickers.Name = "button_LoadTickers";
            this.button_LoadTickers.Size = new System.Drawing.Size(160, 46);
            this.button_LoadTickers.TabIndex = 4;
            this.button_LoadTickers.Text = "Load Tickers";
            this.button_LoadTickers.UseVisualStyleBackColor = true;
            this.button_LoadTickers.Click += new System.EventHandler(this.button_LoadTickers_Click);
            // 
            // openFileDialog_selector
            // 
            this.openFileDialog_selector.DefaultExt = "csv";
            this.openFileDialog_selector.FileName = "AAPL-Day.csv";
            this.openFileDialog_selector.Filter = "\"select stock file\"|*.csv|\"Daily\"|*-Day.csv|\"Weekly\"|*-Week.csv|\"Monthly\"|*-Month" +
    ".csv";
            this.openFileDialog_selector.FilterIndex = 2;
            this.openFileDialog_selector.InitialDirectory = "..\\..\\Stock Data";
            this.openFileDialog_selector.Multiselect = true;
            this.openFileDialog_selector.Title = "File Selector";
            this.openFileDialog_selector.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_selector_FileOk);
            // 
            // Form_Startup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1357, 104);
            this.Controls.Add(this.button_LoadTickers);
            this.Controls.Add(this.dateTimePicker_End);
            this.Controls.Add(this.dateTimePicker_Start);
            this.Controls.Add(this.label_End);
            this.Controls.Add(this.label_Start);
            this.Name = "Form_Startup";
            this.Text = "Choose Tickers to Start:";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Start;
        private System.Windows.Forms.Label label_End;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Start;
        private System.Windows.Forms.DateTimePicker dateTimePicker_End;
        private System.Windows.Forms.Button button_LoadTickers;
        private System.Windows.Forms.OpenFileDialog openFileDialog_selector;
    }
}