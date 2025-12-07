namespace ascii_mapper
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
            this.upload_image_button = new System.Windows.Forms.Button();
            this.TrackBar1 = new System.Windows.Forms.TrackBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // upload_image_button
            // 
            this.upload_image_button.Location = new System.Drawing.Point(13, 13);
            this.upload_image_button.Name = "upload_image_button";
            this.upload_image_button.Size = new System.Drawing.Size(120, 23);
            this.upload_image_button.TabIndex = 0;
            this.upload_image_button.Text = "Upload Image";
            this.upload_image_button.UseVisualStyleBackColor = true;
            this.upload_image_button.Click += new System.EventHandler(this.Upload_Image_Button_Click);
            // 
            // TrackBar1
            // 
            this.TrackBar1.Location = new System.Drawing.Point(147, 12);
            this.TrackBar1.Name = "TrackBar1";
            this.TrackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TrackBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TrackBar1.Size = new System.Drawing.Size(45, 800);
            this.TrackBar1.TabIndex = 1;
            this.TrackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.TrackBar1.Value = 10;
            this.TrackBar1.Scroll += new System.EventHandler(this.TrackBar1_Scroll);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(198, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1200, 800);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 44);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Grayscale";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1518, 929);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.TrackBar1);
            this.Controls.Add(this.upload_image_button);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button upload_image_button;
        private System.Windows.Forms.TrackBar TrackBar1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
    }
}

