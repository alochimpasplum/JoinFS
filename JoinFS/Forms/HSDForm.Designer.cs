namespace JoinFS.Forms
{
    partial class HSDForm
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
            this.pictureBoxBackground = new System.Windows.Forms.PictureBox();
            this.labelIFF = new System.Windows.Forms.Label();
            this.labelTCN = new System.Windows.Forms.Label();
            this.labelDist = new System.Windows.Forms.Label();
            this.labelRange = new System.Windows.Forms.Label();
            this.labelDL = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxBackground
            // 
            this.pictureBoxBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxBackground.Image = global::JoinFS.Properties.Resources.HSD_background;
            this.pictureBoxBackground.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxBackground.Name = "pictureBoxBackground";
            this.pictureBoxBackground.Size = new System.Drawing.Size(284, 261);
            this.pictureBoxBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBackground.TabIndex = 0;
            this.pictureBoxBackground.TabStop = false;
            // 
            // labelIFF
            // 
            this.labelIFF.AutoSize = true;
            this.labelIFF.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelIFF.Location = new System.Drawing.Point(13, 13);
            this.labelIFF.Name = "labelIFF";
            this.labelIFF.Size = new System.Drawing.Size(52, 13);
            this.labelIFF.TabIndex = 1;
            this.labelIFF.Text = "IFF: 0000";
            // 
            // labelTCN
            // 
            this.labelTCN.AutoSize = true;
            this.labelTCN.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelTCN.Location = new System.Drawing.Point(218, 13);
            this.labelTCN.Name = "labelTCN";
            this.labelTCN.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelTCN.Size = new System.Drawing.Size(54, 13);
            this.labelTCN.TabIndex = 2;
            this.labelTCN.Text = "TCN: 00X";
            this.labelTCN.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelDist
            // 
            this.labelDist.AutoSize = true;
            this.labelDist.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelDist.Location = new System.Drawing.Point(215, 26);
            this.labelDist.Name = "labelDist";
            this.labelDist.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelDist.Size = new System.Drawing.Size(59, 13);
            this.labelDist.TabIndex = 3;
            this.labelDist.Text = "dst: 999nm";
            this.labelDist.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelRange
            // 
            this.labelRange.AutoSize = true;
            this.labelRange.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelRange.Location = new System.Drawing.Point(13, 239);
            this.labelRange.Name = "labelRange";
            this.labelRange.Size = new System.Drawing.Size(60, 13);
            this.labelRange.TabIndex = 4;
            this.labelRange.Text = "rang: 30nm";
            this.labelRange.Click += new System.EventHandler(this.labelRange_Click);
            // 
            // labelDL
            // 
            this.labelDL.AutoSize = true;
            this.labelDL.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelDL.Location = new System.Drawing.Point(13, 26);
            this.labelDL.Name = "labelDL";
            this.labelDL.Size = new System.Drawing.Size(39, 13);
            this.labelDL.TabIndex = 5;
            this.labelDL.Text = "DL: 00";
            // 
            // HSDForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.labelDL);
            this.Controls.Add(this.labelRange);
            this.Controls.Add(this.labelDist);
            this.Controls.Add(this.labelTCN);
            this.Controls.Add(this.labelIFF);
            this.Controls.Add(this.pictureBoxBackground);
            this.MaximizeBox = false;
            this.Name = "HSDForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "HSD";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HSDForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackground)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxBackground;
        private System.Windows.Forms.Label labelIFF;
        private System.Windows.Forms.Label labelTCN;
        private System.Windows.Forms.Label labelDist;
        private System.Windows.Forms.Label labelRange;
        private System.Windows.Forms.Label labelDL;
    }
}