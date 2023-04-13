using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoinFS.Forms
{
    public partial class HSDForm : Form
    {
        Main main;

        public HSDForm(Main main)
        {
            this.main = main;

            InitializeComponent();

            Icon = main.icon;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            SetBackgroundImage();
        }

        private void SetBackgroundImage()
        {
            Graphics gr = Graphics.FromImage(pictureBoxBackground.Image);

            using (Pen pen = new Pen(Color.White, 1))
            {
                gr.DrawEllipse(pen, new Rectangle(10, 10, 280, 280));
                gr.DrawEllipse(pen, new Rectangle(56, 56, (280/3)*2, (280 / 3) * 2));
                gr.DrawEllipse(pen, new Rectangle(103, 103, 280/3, 280/3));

                gr.DrawLine(pen, new Point(150, 95), new Point(150, 110));
                gr.DrawLine(pen, new Point(150, 95), new Point(155, 97));
                gr.DrawLine(pen, new Point(150, 100), new Point(155, 97));
            }

            using (Pen pen = new Pen(Color.Yellow, 2))
            {
                gr.DrawLine(pen, new Point(150, 140), new Point(150, 160));
                gr.DrawLine(pen, new Point(140, 145), new Point(160, 145));
                gr.DrawLine(pen, new Point(145, 155), new Point(155, 155));
            }
        }

        private void HSDForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
