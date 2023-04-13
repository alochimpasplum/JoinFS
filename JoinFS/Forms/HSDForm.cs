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
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace JoinFS.Forms
{
    public partial class HSDForm : Form
    {
        Main main;
        Sim.Aircraft selfEntity;
        InfoData selfData;
        const double MAX_TACAN_DISTANCE = 300;

        enum ERange { D8 = 8, D30 = 30, D60 = 60, D120 = 120, D240 = 240 }
        ERange rng;

        /// <summary>
        /// This class is used to get information about military stuff
        /// </summary>
        public class InfoData
        {
            public string IFF_CODE { get; set; }
            public string DL_CODE { get; set; }
            public string AA_TCN { get; set; }
            public InfoData()
            {
                IFF_CODE = "0000";
                DL_CODE = "00";
                AA_TCN = "00X";
            }
            public override string ToString()
            {
                return string.Format("IFF: {0}, DL_CODE: {1}, AA_TCN: {2}", IFF_CODE, DL_CODE, AA_TCN);
            }
        }

        public HSDForm(Main main)
        {
            this.main = main;

            InitializeComponent();

            Icon = main.icon;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            rng = ERange.D8;
            selfData = new InfoData();

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

                    List<Sim.Aircraft> Aircrafts = new List<Sim.Aircraft>();

                    foreach (var obj in main.sim.objectList)
                    {
                        if (obj is Sim.Aircraft && obj.owner == Sim.Obj.Owner.Network)
                        {
                            Aircrafts.Add(obj as Sim.Aircraft);
                        }
                    }

                    SetBackgroundImage();

                    CheckAircrafts(Aircrafts);

                    if(selfEntity != null)
                        SetHSDRotation();

                    if (selfEntity != null)
                        SetSelfLabels();

                    SetSelfOnHSD();
                }
            }
        }
        private void CheckAircrafts(List<Sim.Aircraft> aircrafts)
        {
            double tacanDist = MAX_TACAN_DISTANCE;

            foreach (Sim.Aircraft aircraft in aircrafts)
            {
                InfoData temp = new InfoData();
                try
                {
                    temp = JsonSerializer.Deserialize<InfoData>(aircraft.flightPlan.remarks);
                }
                catch (Exception) { }

                if (temp != null)
                {
                    // Tacan
                    if (CheckPairedTacan(temp))
                    {
                        // get distance to aircraft
                        double d = Vector.GeodesicDistance(aircraft.Position.geo.x, aircraft.Position.geo.z, selfEntity.Position.geo.x, selfEntity.Position.geo.z);
                        // convert to nautical miles
                        d *= 0.00053995680346;

                        if (d < tacanDist)
                            tacanDist = d;
                    }
                }
            }

            labelDist.Text = (tacanDist != MAX_TACAN_DISTANCE) ? string.Format("dst: {0}nm",tacanDist.ToString("N2")) : "dst: ---nm";
        }
        private bool CheckPairedTacan(InfoData data)
        {
            if (data.AA_TCN == "00X" || data.AA_TCN.Length != 3)
                return false;

            if(Int32.TryParse(data.AA_TCN.Substring(0,2), out int networkTacan) && Int32.TryParse(selfData.AA_TCN.Substring(0, 2), out int selfTacan)
                && data.AA_TCN[2] == selfData.AA_TCN[2])
            {
                return networkTacan + 63 == selfTacan || selfTacan + 63 == networkTacan;
            } else
            {
                return false;
            }
        }
        private void SetSelfLabels()
        {
            labelRange.Text = string.Format("rng: {0}nm", (int)rng);

            try
            {
                selfData = JsonSerializer.Deserialize<InfoData>(selfEntity.flightPlan.remarks);
            } catch (Exception) { }

            labelIFF.Text = string.Format("IFF: {0}", selfData.IFF_CODE);
            labelTCN.Text = string.Format("TCN: {0}", selfData.AA_TCN);
            labelDL.Text = string.Format("DL: {0}", selfData.DL_CODE);
        }
        private void SetHSDRotation()
        {
            Sim.Pos aircraftPosition = selfEntity.Position;
            int heading = (int)(aircraftPosition.angles.y * 180.0 / Math.PI);

            Bitmap rotatedBmp = new Bitmap(pictureBoxBackground.Image.Height, pictureBoxBackground.Image.Width);
            rotatedBmp.SetResolution(pictureBoxBackground.Image.HorizontalResolution, pictureBoxBackground.Image.VerticalResolution);

            Graphics g = Graphics.FromImage(rotatedBmp);
            g.TranslateTransform(150, 150);
            g.RotateTransform(-1*heading);
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
