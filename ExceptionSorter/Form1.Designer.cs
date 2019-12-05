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
            this.bPreviewFileBtn = new System.Windows.Forms.Button();
            this.bPdfToTifBtn = new System.Windows.Forms.Button();
            this.statStrip = new System.Windows.Forms.StatusStrip();
            this.slStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.bFileTifBtn = new System.Windows.Forms.Button();
            this.statStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lWindow
            // 
            this.lWindow.BackColor = System.Drawing.SystemColors.Control;
            this.lWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lWindow.HideSelection = false;
            this.lWindow.Location = new System.Drawing.Point(12, 12);
            this.lWindow.Name = "lWindow";
            this.lWindow.Size = new System.Drawing.Size(525, 268);
            this.lWindow.TabIndex = 0;
            this.lWindow.UseCompatibleStateImageBehavior = false;
            this.lWindow.View = System.Windows.Forms.View.List;
            // 
            // bPreviewFileBtn
            // 
            this.bPreviewFileBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bPreviewFileBtn.Location = new System.Drawing.Point(12, 286);
            this.bPreviewFileBtn.Name = "bPreviewFileBtn";
            this.bPreviewFileBtn.Size = new System.Drawing.Size(100, 30);
            this.bPreviewFileBtn.TabIndex = 1;
            this.bPreviewFileBtn.Text = "Preview Files";
            this.bPreviewFileBtn.UseVisualStyleBackColor = true;
            this.bPreviewFileBtn.Click += new System.EventHandler(this.bPreviewFileBtn_Click);
            // 
            // bPdfToTifBtn
            // 
            this.bPdfToTifBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bPdfToTifBtn.Location = new System.Drawing.Point(219, 286);
            this.bPdfToTifBtn.Name = "bPdfToTifBtn";
            this.bPdfToTifBtn.Size = new System.Drawing.Size(100, 30);
            this.bPdfToTifBtn.TabIndex = 2;
            this.bPdfToTifBtn.Text = "PDF --> Tif";
            this.bPdfToTifBtn.UseVisualStyleBackColor = true;
            this.bPdfToTifBtn.Click += new System.EventHandler(this.bPdfToTifBtn_Click);
            // 
            // statStrip
            // 
            this.statStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slStatusLabel});
            this.statStrip.Location = new System.Drawing.Point(0, 355);
            this.statStrip.Name = "statStrip";
            this.statStrip.Size = new System.Drawing.Size(549, 25);
            this.statStrip.TabIndex = 3;
            this.statStrip.Text = "statusStrip1";
            // 
            // slStatusLabel
            // 
            this.slStatusLabel.Name = "slStatusLabel";
            this.slStatusLabel.Size = new System.Drawing.Size(151, 20);
            this.slStatusLabel.Text = "toolStripStatusLabel1";
            // 
            // bFileTifBtn
            // 
            this.bFileTifBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bFileTifBtn.Location = new System.Drawing.Point(437, 286);
            this.bFileTifBtn.Name = "bFileTifBtn";
            this.bFileTifBtn.Size = new System.Drawing.Size(100, 30);
            this.bFileTifBtn.TabIndex = 4;
            this.bFileTifBtn.Text = "File Tifs";
            this.bFileTifBtn.UseVisualStyleBackColor = true;
            this.bFileTifBtn.Click += new System.EventHandler(this.bFileTifBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(549, 380);
            this.Controls.Add(this.bFileTifBtn);
            this.Controls.Add(this.statStrip);
            this.Controls.Add(this.bPdfToTifBtn);
            this.Controls.Add(this.bPreviewFileBtn);
            this.Controls.Add(this.lWindow);
            this.Name = "Form1";
            this.Text = "Exception Sorter";
            this.statStrip.ResumeLayout(false);
            this.statStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lWindow;
        private System.Windows.Forms.Button bPreviewFileBtn;
        private System.Windows.Forms.Button bPdfToTifBtn;
        private System.Windows.Forms.StatusStrip statStrip;
        private System.Windows.Forms.Button bFileTifBtn;
        private System.Windows.Forms.ToolStripStatusLabel slStatusLabel;
    }
}

