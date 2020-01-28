namespace AStar
{
    partial class MainWindow
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
            this.openButton = new System.Windows.Forms.Button();
            this.goButton = new System.Windows.Forms.Button();
            this.cppRadioButton = new System.Windows.Forms.RadioButton();
            this.asmRadioButton = new System.Windows.Forms.RadioButton();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.threadLabel = new System.Windows.Forms.Label();
            this.optionsGroupBox = new System.Windows.Forms.GroupBox();
            this.folderPathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.optionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // openButton
            // 
            this.openButton.AutoSize = true;
            this.openButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.openButton.Location = new System.Drawing.Point(214, 23);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(94, 23);
            this.openButton.TabIndex = 0;
            this.openButton.Text = "Choose Folder...";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // goButton
            // 
            this.goButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.goButton.Location = new System.Drawing.Point(137, 56);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(171, 76);
            this.goButton.TabIndex = 1;
            this.goButton.Text = "GO!";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // cppRadioButton
            // 
            this.cppRadioButton.AutoSize = true;
            this.cppRadioButton.Checked = true;
            this.cppRadioButton.Location = new System.Drawing.Point(11, 52);
            this.cppRadioButton.Name = "cppRadioButton";
            this.cppRadioButton.Size = new System.Drawing.Size(44, 17);
            this.cppRadioButton.TabIndex = 2;
            this.cppRadioButton.TabStop = true;
            this.cppRadioButton.Text = "C++";
            this.cppRadioButton.UseVisualStyleBackColor = true;
            // 
            // asmRadioButton
            // 
            this.asmRadioButton.AutoSize = true;
            this.asmRadioButton.Location = new System.Drawing.Point(60, 52);
            this.asmRadioButton.Name = "asmRadioButton";
            this.asmRadioButton.Size = new System.Drawing.Size(48, 17);
            this.asmRadioButton.TabIndex = 3;
            this.asmRadioButton.Text = "ASM";
            this.asmRadioButton.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(62, 23);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(44, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Multiselect = true;
            // 
            // threadLabel
            // 
            this.threadLabel.AutoSize = true;
            this.threadLabel.Location = new System.Drawing.Point(6, 25);
            this.threadLabel.Name = "threadLabel";
            this.threadLabel.Size = new System.Drawing.Size(49, 13);
            this.threadLabel.TabIndex = 5;
            this.threadLabel.Text = "Threads:";
            // 
            // optionsGroupBox
            // 
            this.optionsGroupBox.Controls.Add(this.threadLabel);
            this.optionsGroupBox.Controls.Add(this.numericUpDown1);
            this.optionsGroupBox.Controls.Add(this.cppRadioButton);
            this.optionsGroupBox.Controls.Add(this.asmRadioButton);
            this.optionsGroupBox.Location = new System.Drawing.Point(12, 51);
            this.optionsGroupBox.Name = "optionsGroupBox";
            this.optionsGroupBox.Size = new System.Drawing.Size(119, 81);
            this.optionsGroupBox.TabIndex = 8;
            this.optionsGroupBox.TabStop = false;
            this.optionsGroupBox.Text = "Options";
            // 
            // folderPathTextBox
            // 
            this.folderPathTextBox.Location = new System.Drawing.Point(12, 25);
            this.folderPathTextBox.Name = "folderPathTextBox";
            this.folderPathTextBox.Size = new System.Drawing.Size(196, 20);
            this.folderPathTextBox.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Input folder path:";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 145);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.folderPathTextBox);
            this.Controls.Add(this.optionsGroupBox);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.openButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "A*";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.optionsGroupBox.ResumeLayout(false);
            this.optionsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.RadioButton cppRadioButton;
        private System.Windows.Forms.RadioButton asmRadioButton;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label threadLabel;
        private System.Windows.Forms.GroupBox optionsGroupBox;
        private System.Windows.Forms.TextBox folderPathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}

