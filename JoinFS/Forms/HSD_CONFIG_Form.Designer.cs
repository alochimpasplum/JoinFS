namespace JoinFS.Forms
{
    partial class HSD_CONFIG_Form
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxTCN = new System.Windows.Forms.TextBox();
            this.textBoxDL = new System.Windows.Forms.TextBox();
            this.textBoxIFF = new System.Windows.Forms.TextBox();
            this.radioButtonY = new System.Windows.Forms.RadioButton();
            this.radioButtonX = new System.Windows.Forms.RadioButton();
            this.labelTCN = new System.Windows.Forms.Label();
            this.labelDL = new System.Windows.Forms.Label();
            this.labelIFF = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxTCN);
            this.groupBox1.Controls.Add(this.textBoxDL);
            this.groupBox1.Controls.Add(this.textBoxIFF);
            this.groupBox1.Controls.Add(this.radioButtonY);
            this.groupBox1.Controls.Add(this.radioButtonX);
            this.groupBox1.Controls.Add(this.labelTCN);
            this.groupBox1.Controls.Add(this.labelDL);
            this.groupBox1.Controls.Add(this.labelIFF);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(169, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SELF";
            // 
            // textBoxTCN
            // 
            this.textBoxTCN.Location = new System.Drawing.Point(34, 74);
            this.textBoxTCN.MaxLength = 2;
            this.textBoxTCN.Name = "textBoxTCN";
            this.textBoxTCN.Size = new System.Drawing.Size(51, 20);
            this.textBoxTCN.TabIndex = 10;
            this.textBoxTCN.Text = "99";
            this.textBoxTCN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxTCN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxTCN_KeyPress);
            this.textBoxTCN.Leave += new System.EventHandler(this.textBoxTCN_Leave);
            // 
            // textBoxDL
            // 
            this.textBoxDL.Location = new System.Drawing.Point(34, 48);
            this.textBoxDL.MaxLength = 2;
            this.textBoxDL.Name = "textBoxDL";
            this.textBoxDL.Size = new System.Drawing.Size(51, 20);
            this.textBoxDL.TabIndex = 9;
            this.textBoxDL.Text = "99";
            this.textBoxDL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxDL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxDL_KeyPress);
            this.textBoxDL.Leave += new System.EventHandler(this.textBoxDL_Leave);
            // 
            // textBoxIFF
            // 
            this.textBoxIFF.Location = new System.Drawing.Point(34, 19);
            this.textBoxIFF.MaxLength = 4;
            this.textBoxIFF.Name = "textBoxIFF";
            this.textBoxIFF.Size = new System.Drawing.Size(51, 20);
            this.textBoxIFF.TabIndex = 8;
            this.textBoxIFF.Text = "9999";
            this.textBoxIFF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxIFF.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxIFF_KeyPress);
            this.textBoxIFF.Leave += new System.EventHandler(this.textBoxIFF_Leave);
            // 
            // radioButtonY
            // 
            this.radioButtonY.AutoSize = true;
            this.radioButtonY.Checked = true;
            this.radioButtonY.Location = new System.Drawing.Point(130, 77);
            this.radioButtonY.Name = "radioButtonY";
            this.radioButtonY.Size = new System.Drawing.Size(32, 17);
            this.radioButtonY.TabIndex = 7;
            this.radioButtonY.TabStop = true;
            this.radioButtonY.Text = "Y";
            this.radioButtonY.UseVisualStyleBackColor = true;
            this.radioButtonY.Click += new System.EventHandler(this.radioButtonY_Click);
            this.radioButtonY.Leave += new System.EventHandler(this.radioButtonY_Leave);
            // 
            // radioButtonX
            // 
            this.radioButtonX.AutoSize = true;
            this.radioButtonX.Location = new System.Drawing.Point(91, 77);
            this.radioButtonX.Name = "radioButtonX";
            this.radioButtonX.Size = new System.Drawing.Size(32, 17);
            this.radioButtonX.TabIndex = 6;
            this.radioButtonX.Text = "X";
            this.radioButtonX.UseVisualStyleBackColor = true;
            this.radioButtonX.Click += new System.EventHandler(this.radioButtonX_Click);
            this.radioButtonX.Leave += new System.EventHandler(this.radioButtonX_Leave);
            // 
            // labelTCN
            // 
            this.labelTCN.AutoSize = true;
            this.labelTCN.Location = new System.Drawing.Point(7, 78);
            this.labelTCN.Name = "labelTCN";
            this.labelTCN.Size = new System.Drawing.Size(29, 13);
            this.labelTCN.TabIndex = 5;
            this.labelTCN.Text = "TCN";
            // 
            // labelDL
            // 
            this.labelDL.AutoSize = true;
            this.labelDL.Location = new System.Drawing.Point(7, 51);
            this.labelDL.Name = "labelDL";
            this.labelDL.Size = new System.Drawing.Size(21, 13);
            this.labelDL.TabIndex = 2;
            this.labelDL.Text = "DL";
            // 
            // labelIFF
            // 
            this.labelIFF.AutoSize = true;
            this.labelIFF.Location = new System.Drawing.Point(6, 22);
            this.labelIFF.Name = "labelIFF";
            this.labelIFF.Size = new System.Drawing.Size(22, 13);
            this.labelIFF.TabIndex = 0;
            this.labelIFF.Text = "IFF";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(188, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(195, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // HSD_CONFIG_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 122);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "HSD_CONFIG_Form";
            this.Text = "HSD_CONFIG_Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HSD_CONFIG_Form_FormClosing);
            this.Load += new System.EventHandler(this.HSD_CONFIG_Form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelIFF;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelDL;
        private System.Windows.Forms.Label labelTCN;
        private System.Windows.Forms.RadioButton radioButtonY;
        private System.Windows.Forms.RadioButton radioButtonX;
        private System.Windows.Forms.TextBox textBoxDL;
        private System.Windows.Forms.TextBox textBoxIFF;
        private System.Windows.Forms.TextBox textBoxTCN;
    }
}