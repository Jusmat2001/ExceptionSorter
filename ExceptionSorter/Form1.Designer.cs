namespace ExceptionSorter
{
    partial class Form1
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
            this.lWindow = new System.Windows.Forms.ListView();
            this.loadBtn = new System.Windows.Forms.Button();
            this.tiffBtn = new System.Windows.Forms.Button();
            this.statStrip = new System.Windows.Forms.StatusStrip();
            this.fileBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lWindow
            // 
            this.lWindow.BackColor = System.Drawing.SystemColors.Control;
            this.lWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lWindow.Location = new System.Drawing.Point(12, 12);
            this.lWindow.Name = "lWindow";
            this.lWindow.Size = new System.Drawing.Size(525, 268);
            this.lWindow.TabIndex = 0;
            this.lWindow.UseCompatibleStateImageBehavior = false;
            this.lWindow.View = System.Windows.Forms.View.List;
            // 
            // loadBtn
            // 
            this.loadBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(179)))), ((int)(((byte)(157)))));
            this.loadBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadBtn.Location = new System.Drawing.Point(12, 286);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(114, 30);
            this.loadBtn.TabIndex = 1;
            this.loadBtn.Text = "Load (Ready)";
            this.loadBtn.UseVisualStyleBackColor = false;
            this.loadBtn.Click += new System.EventHandler(this.preBtn_Click);
            // 
            // tiffBtn
            // 
            this.tiffBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(254)))), ((int)(((byte)(201)))));
            this.tiffBtn.Location = new System.Drawing.Point(214, 286);
            this.tiffBtn.Name = "tiffBtn";
            this.tiffBtn.Size = new System.Drawing.Size(114, 30);
            this.tiffBtn.TabIndex = 2;
            this.tiffBtn.Text = "Tiff   (Set)";
            this.tiffBtn.UseVisualStyleBackColor = false;
            this.tiffBtn.Click += new System.EventHandler(this.sortBtn_Click);
            // 
            // statStrip
            // 
            this.statStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statStrip.Location = new System.Drawing.Point(0, 358);
            this.statStrip.Name = "statStrip";
            this.statStrip.Size = new System.Drawing.Size(549, 22);
            this.statStrip.TabIndex = 3;
            this.statStrip.Text = "statusStrip1";
            // 
            // fileBtn
            // 
            this.fileBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(254)))), ((int)(((byte)(203)))));
            this.fileBtn.Location = new System.Drawing.Point(426, 286);
            this.fileBtn.Name = "fileBtn";
            this.fileBtn.Size = new System.Drawing.Size(111, 30);
            this.fileBtn.TabIndex = 4;
            this.fileBtn.Text = "File  (Go)";
            this.fileBtn.UseVisualStyleBackColor = false;
            this.fileBtn.Click += new System.EventHandler(this.fileBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(549, 380);
            this.Controls.Add(this.fileBtn);
            this.Controls.Add(this.statStrip);
            this.Controls.Add(this.tiffBtn);
            this.Controls.Add(this.loadBtn);
            this.Controls.Add(this.lWindow);
            this.Name = "Form1";
            this.Text = "Exception Sorter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lWindow;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.Button tiffBtn;
        private System.Windows.Forms.StatusStrip statStrip;
        private System.Windows.Forms.Button fileBtn;
    }
}

