namespace ImageEdgeDetection
{
    partial class MainForm
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
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.btnOpenOriginal = new System.Windows.Forms.Button();
            this.btnSaveNewImage = new System.Windows.Forms.Button();
            this.buttonMiamiFilter = new System.Windows.Forms.Button();
            this.buttonNightFilter = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxXFilter = new System.Windows.Forms.ListBox();
            this.listBoxYFilter = new System.Windows.Forms.ListBox();
            this.backButton = new System.Windows.Forms.Button();
            this.trackBarThreshold = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonMagicMosaic = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold)).BeginInit();
            this.SuspendLayout();
            // 
            // picPreview
            // 
            this.picPreview.BackColor = System.Drawing.Color.Silver;
            this.picPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Cursor = System.Windows.Forms.Cursors.Default;
            this.picPreview.Location = new System.Drawing.Point(36, 24);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(402, 363);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 13;
            this.picPreview.TabStop = false;
            // 
            // btnOpenOriginal
            // 
            this.btnOpenOriginal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenOriginal.Location = new System.Drawing.Point(36, 402);
            this.btnOpenOriginal.Name = "btnOpenOriginal";
            this.btnOpenOriginal.Size = new System.Drawing.Size(98, 32);
            this.btnOpenOriginal.TabIndex = 15;
            this.btnOpenOriginal.Text = "Load Image";
            this.btnOpenOriginal.UseVisualStyleBackColor = true;
            this.btnOpenOriginal.Click += new System.EventHandler(this.LoadImage);
            // 
            // btnSaveNewImage
            // 
            this.btnSaveNewImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveNewImage.Location = new System.Drawing.Point(340, 402);
            this.btnSaveNewImage.Name = "btnSaveNewImage";
            this.btnSaveNewImage.Size = new System.Drawing.Size(98, 32);
            this.btnSaveNewImage.TabIndex = 16;
            this.btnSaveNewImage.Text = "Save Image";
            this.btnSaveNewImage.UseVisualStyleBackColor = true;
            this.btnSaveNewImage.Click += new System.EventHandler(this.btnSaveNewImage_Click);
            // 
            // buttonMiamiFilter
            // 
            this.buttonMiamiFilter.Location = new System.Drawing.Point(184, 455);
            this.buttonMiamiFilter.Name = "buttonMiamiFilter";
            this.buttonMiamiFilter.Size = new System.Drawing.Size(98, 30);
            this.buttonMiamiFilter.TabIndex = 51;
            this.buttonMiamiFilter.Text = "Miami Filter";
            this.buttonMiamiFilter.UseVisualStyleBackColor = true;
            this.buttonMiamiFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // buttonNightFilter
            // 
            this.buttonNightFilter.Location = new System.Drawing.Point(36, 455);
            this.buttonNightFilter.Name = "buttonNightFilter";
            this.buttonNightFilter.Size = new System.Drawing.Size(98, 30);
            this.buttonNightFilter.TabIndex = 50;
            this.buttonNightFilter.Text = "Night Filter";
            this.buttonNightFilter.UseVisualStyleBackColor = true;
            this.buttonNightFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(275, 502);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 63;
            this.label2.Text = "Y Filter";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 502);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 64;
            this.label1.Text = "X Filter";
            // 
            // listBoxXFilter
            // 
            this.listBoxXFilter.FormattingEnabled = true;
            this.listBoxXFilter.ItemHeight = 16;
            this.listBoxXFilter.Items.AddRange(new object[] {
            "Laplacian3x3",
            "Laplacian5x5",
            "Sobel3x3Horizontal",
            "Sobel3x3Vertical",
            "Prewitt3x3Horizontal",
            "Prewitt3x3Vertical",
            "Kirsch3x3Horizontal",
            "Kirsch3x3Vertical"});
            this.listBoxXFilter.Location = new System.Drawing.Point(111, 527);
            this.listBoxXFilter.Name = "listBoxXFilter";
            this.listBoxXFilter.Size = new System.Drawing.Size(120, 52);
            this.listBoxXFilter.TabIndex = 62;
            this.listBoxXFilter.SelectedIndexChanged += new System.EventHandler(this.listBoxFilter_SelectedIndexChanged);
            // 
            // listBoxYFilter
            // 
            this.listBoxYFilter.FormattingEnabled = true;
            this.listBoxYFilter.ItemHeight = 16;
            this.listBoxYFilter.Items.AddRange(new object[] {
            "Laplacian3x3",
            "Laplacian5x5",
            "Sobel3x3Horizontal",
            "Sobel3x3Vertical",
            "Prewitt3x3Horizontal",
            "Prewitt3x3Vertical",
            "Kirsch3x3Horizontal",
            "Kirsch3x3Vertical"});
            this.listBoxYFilter.Location = new System.Drawing.Point(237, 527);
            this.listBoxYFilter.Name = "listBoxYFilter";
            this.listBoxYFilter.Size = new System.Drawing.Size(120, 52);
            this.listBoxYFilter.TabIndex = 61;
            this.listBoxYFilter.SelectedIndexChanged += new System.EventHandler(this.listBoxFilter_SelectedIndexChanged);
            // 
            // backButton
            // 
            this.backButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.75F, System.Drawing.FontStyle.Bold);
            this.backButton.Location = new System.Drawing.Point(178, 403);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(113, 31);
            this.backButton.TabIndex = 65;
            this.backButton.Text = "RESET";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // trackBarThreshold
            // 
            this.trackBarThreshold.LargeChange = 10;
            this.trackBarThreshold.Location = new System.Drawing.Point(36, 604);
            this.trackBarThreshold.Maximum = 255;
            this.trackBarThreshold.Name = "trackBarThreshold";
            this.trackBarThreshold.Size = new System.Drawing.Size(410, 56);
            this.trackBarThreshold.TabIndex = 66;
            this.trackBarThreshold.Value = 100;
            this.trackBarThreshold.Scroll += new System.EventHandler(this.trackBarThreshold_Scroll);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 588);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 16);
            this.label6.TabIndex = 67;
            this.label6.Text = "Threshold";
            // 
            // buttonMagicMosaic
            // 
            this.buttonMagicMosaic.Location = new System.Drawing.Point(340, 455);
            this.buttonMagicMosaic.Name = "buttonMagicMosaic";
            this.buttonMagicMosaic.Size = new System.Drawing.Size(98, 30);
            this.buttonMagicMosaic.TabIndex = 68;
            this.buttonMagicMosaic.Text = "Magic Mosaic";
            this.buttonMagicMosaic.UseVisualStyleBackColor = true;
            this.buttonMagicMosaic.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(481, 647);
            this.Controls.Add(this.buttonMagicMosaic);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.trackBarThreshold);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxXFilter);
            this.Controls.Add(this.listBoxYFilter);
            this.Controls.Add(this.buttonMiamiFilter);
            this.Controls.Add(this.buttonNightFilter);
            this.Controls.Add(this.btnSaveNewImage);
            this.Controls.Add(this.btnOpenOriginal);
            this.Controls.Add(this.picPreview);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Button btnOpenOriginal;
        private System.Windows.Forms.Button btnSaveNewImage;
        private System.Windows.Forms.Button buttonMiamiFilter;
        private System.Windows.Forms.Button buttonNightFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxXFilter;
        private System.Windows.Forms.ListBox listBoxYFilter;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.TrackBar trackBarThreshold;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonMagicMosaic;
    }
}

