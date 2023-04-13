using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace JoinFS.Forms
{
    public partial class HSDForm : Form
    {
        Main main;
        Sim.Aircraft selfEntity;

        enum ERange { D8 = 8, D30 = 30, D60 = 60, D120 = 120, D240 = 240 }
        ERange rng;

        public HSDForm(Main main)
        {
            this.main = main;

            InitializeComponent();

            Icon = main.icon;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            rng = ERange.D8;

            RefreshWindow();
        }

        private void SetBackgroundImage()
        {
            pictureBoxBackground.Image = JoinFS.Properties.Resources.HSD_background;

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
        }
        private void SetSelfOnHSD()
        {
            Graphics gr = Graphics.FromImage(pictureBoxBackground.Image);

            using (Pen pen = new Pen(Color.Yellow, 2))
            {
                gr.DrawLine(pen, new Point(150, 140), new Point(150, 160));
                gr.DrawLine(pen, new Point(140, 145), new Point(160, 145));
                gr.DrawLine(pen, new Point(145, 155), new Point(155, 155));
            }
        }
        private void RefreshWindow()
        {
            lock (main.conch)
            {
                if(main.sim != null)
                {
                    selfEntity = main.sim.objectList.Find(x => x.owner == Sim.Obj.Owner.Me) as Sim.Aircraft;

                    SetBackgroundImage();

                    if(selfEntity != null)
                        SetHSDRotation();

                    SetSelfLabels();
                    SetSelfOnHSD();
                }
            }
        }
        private void SetSelfLabels()
        {
            labelRange.Text = string.Format("rng: {0}nm", (int)rng);
        }
        private void SetHSDRotation()
        {
            Sim.Pos aircraftPosition = selfEntity.Position;
            int heading = (int)(aircraftPosition.angles.y * 180.0 / Math.PI);

            Bitmap rotatedBmp = new Bitmap(pictureBoxBackground.Image.Height, pictureBoxBackground.Image.Width);
            rotatedBmp.SetResolution(pictureBoxBackground.Image.HorizontalResolution, pictureBoxBackground.Image.VerticalResolution);

            Graphics g = Graphics.FromImage(rotatedBmp);
            g.TranslateTransform(150, 150);
            g.RotateTransform(heading);
            g.TranslateTransform(-150, -150);
            g.DrawImage(pictureBoxBackground.Image, new PointF(0, 0));

            pictureBoxBackground.Image = rotatedBmp;
        }
        private void HSDForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        public void DoRefreshButton(bool force)
        {
            RefreshWindow();
        }

        private void labelRange_Click(object sender, EventArgs e)
        {
            switch (rng)
            {
                case ERange.D8:
                    rng = ERange.D30;
                    break;
                case ERange.D30:
                    rng = ERange.D60;
                    break;
                case ERange.D60:
                    rng = ERange.D120;
                    break;
                case ERange.D120:
                    rng = ERange.D240;
                    break;
                case ERange.D240:
                    rng = ERange.D8;
                    break;
            }
            RefreshWindow();
        }
    }
}
