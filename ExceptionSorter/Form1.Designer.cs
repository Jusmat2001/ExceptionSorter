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
            this.preBtn = new System.Windows.Forms.Button();
            this.sortBtn = new System.Windows.Forms.Button();
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
            // preBtn
            // 
            this.preBtn.Location = new System.Drawing.Point(12, 286);
            this.preBtn.Name = "preBtn";
            this.preBtn.Size = new System.Drawing.Size(100, 30);
            this.preBtn.TabIndex = 1;
            this.preBtn.Text = "Preview Files";
            this.preBtn.UseVisualStyleBackColor = true;
            this.preBtn.Click += new System.EventHandler(this.preBtn_Click);
            // 
            // sortBtn
            // 
            this.sortBtn.Location = new System.Drawing.Point(165, 286);
            this.sortBtn.Name = "sortBtn";
            this.sortBtn.Size = new System.Drawing.Size(100, 30);
            this.sortBtn.TabIndex = 2;
            this.sortBtn.Text = "Tiff and File";
            this.sortBtn.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(549, 380);
            this.Controls.Add(this.sortBtn);
            this.Controls.Add(this.preBtn);
            this.Controls.Add(this.lWindow);
            this.Name = "Form1";
            this.Text = "Exception Sorter";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lWindow;
        private System.Windows.Forms.Button preBtn;
        private System.Windows.Forms.Button sortBtn;
    }
}

