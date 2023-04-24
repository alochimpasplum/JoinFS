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
using System.Numerics;
using System.Security.Policy;

namespace JoinFS.Forms
{
    public partial class HSDForm : Form
    {
        Main main;
        private Sim.Aircraft selfEntity { get; set; }
        TestDL testDL = new TestDL();
        public InfoData selfData { get; set; }
        const double MAX_TACAN_DISTANCE = 240;
        bool isOnTest = false;
        private int rotation;
        private List<DataLinkItem> Items = new List<DataLinkItem>();
        public string[] LeftDL { get; set; }
        public string[] RightDL { get; set; }

        enum ERange { D8 = 8, D30 = 30, D60 = 60, D120 = 120, D240 = 240 }
        public enum EDLType { LEFT, RIGHT, TEST }
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
        /// <summary>
        /// This class is used to put information in the DL
        /// </summary>
        public class DataLinkItem
        {
            const int DL_ITEM_RADIUS = 4;
            const int DL_ITEM_HEADING_SIZE = 7;

            public int X { get; set; }
            public int Y { get; set; }
            public int Heading { get; set; }
            public string FlightLevel { get; set; }
            public string DataLinkCode { get; set; }
            private PictureBox pictureBoxHSD;
            private Color color;
            private Brush brush;
            private int VectorPhase { get; set; }
            private int VectorModule { get; set; }
            public DataLinkItem(int vectorModule, int vectorPhase, int heading, string flightLevel, string dataLinkCode, PictureBox pictureBox, EDLType type)
            {
                VectorPhase = vectorPhase;
                VectorModule = vectorModule;
                Heading = heading;
                FlightLevel = flightLevel;
                DataLinkCode = dataLinkCode;
                pictureBoxHSD = pictureBox;
                
                SetColors(type);
                SetCartesianPoints();
                DrawSelf();
            }
            public void DrawSelf()
            {
                using (Graphics gr = Graphics.FromImage(pictureBoxHSD.Image))
                {
                    using (Pen pen = new Pen(color, 1))
                    {
                        gr.DrawEllipse(pen, new Rectangle(150 + X - (DL_ITEM_RADIUS / 2), 150 + Y - (DL_ITEM_RADIUS / 2), DL_ITEM_RADIUS, DL_ITEM_RADIUS));
                    }
                    using (Pen pen = new Pen(color, 2))
                    {
                        gr.DrawLine(pen, new Point(150 + X, 150 + Y), GetFinalItemHeadingPoint(150 + X, 150 + Y));
                    }
                }
            }
            public void DrawText(int rotation)
            {
                VectorPhase += rotation;
                SetCartesianPoints();

                using (Graphics gr = Graphics.FromImage(pictureBoxHSD.Image))
                using (Font font = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel))
                {
                    gr.DrawString(FlightLevel, font, brush, 150 + X - 8, 150 + Y + 5);
                    gr.DrawString(DataLinkCode, font, brush, 150 + X + 8, 150 + Y - 5);
                }
            }
            private void SetColors(EDLType type)
            {
                switch (type)
                {
                    case EDLType.LEFT:
                        color = Color.LightGreen;
                        brush = Brushes.LightGreen;
                        break;
                    case EDLType.RIGHT:
                        color = Color.SkyBlue;
                        brush = Brushes.SkyBlue;
                        break;
                    case EDLType.TEST:
                        color = Color.Yellow;
                        brush = Brushes.Yellow;
                        break;
                }
            }
            /// <summary>
            /// Used to get cartesian coordinates based on module and phase
            /// </summary>
            private void SetCartesianPoints()
            {
                X = (int)(Math.Sin((VectorPhase * Math.PI) / 180) * VectorModule);
                Y = -1 * (int)(Math.Cos((VectorPhase * Math.PI) / 180) * VectorModule);
            }
            /// <summary>
            /// Used to obtain the final point of the item line
            /// </summary>
            private Point GetFinalItemHeadingPoint(int x, int y)
            {
                int tempX = x + (int)(Math.Sin((Heading * Math.PI) / 180) * DL_ITEM_HEADING_SIZE);
                int tempY = y - (int)(Math.Cos((Heading * Math.PI) / 180) * DL_ITEM_HEADING_SIZE);

                return new Point(tempX, tempY);
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

            Items = new List<DataLinkItem>();

            RightDL = new string[] { "00", "00", "00", "00" };
            LeftDL = new string[] { "00", "00", "00", "00" };
            rotation = 0;

            RefreshWindow();
        }
        private void SetBackgroundImage()
        {
            pictureBoxBackground.Image = JoinFS.Properties.Resources.HSD_background;

            Graphics gr = Graphics.FromImage(pictureBoxBackground.Image);

            using (Pen pen = new Pen(Color.Gray, 1))
            {
                gr.DrawEllipse(pen, new Rectangle(10, 10, 280, 280));
                gr.DrawEllipse(pen, new Rectangle(56, 56, (280/3)*2, (280 / 3) * 2));
                gr.DrawEllipse(pen, new Rectangle(103, 103, 280/3, 280/3));

                gr.DrawLine(pen, new Point(150, 95), new Point(150, 110));
                gr.DrawLine(pen, new Point(150, 95), new Point(155, 97));
                gr.DrawLine(pen, new Point(150, 100), new Point(155, 97));

                gr.DrawLine(pen, new Point(150 + (280 / 6), 150), new Point(150 + (280 / 6) + 8, 150));
                gr.DrawLine(pen, new Point(150 - (280 / 6), 150), new Point(150 - (280 / 6) - 8, 150));
                gr.DrawLine(pen, new Point(150, 150 + (280 / 6)), new Point(150, 150 + (280 / 6) + 8));
                gr.DrawLine(pen, new Point(150, 150 - (280 / 6)), new Point(150, 150 - (280 / 6) - 8));
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

                    if (isOnTest)
                        TestPos();

                    if(!isOnTest)
                        CheckAircrafts(Aircrafts);

                    if(selfEntity != null)
                        SetHSDRotation();

                    if (selfEntity != null)
                        SetSelfLabels();
                    
                    foreach (DataLinkItem item in Items)
                        item.DrawText(rotation);
                    
                    SetSelfOnHSD();
                }
            }
        }
        /// <summary>
        /// Used to check DL items
        /// </summary>
        private void TestPos()
        {
            Items.Clear();

            int[] temp = testDL.GetItem();
            Items.Add(new DataLinkItem(temp[0], temp[1], temp[2], "99", "11", pictureBoxBackground, EDLType.TEST));
        }
        private void CheckAircrafts(List<Sim.Aircraft> aircrafts)
        {
            double tacanDist = MAX_TACAN_DISTANCE;
            Items.Clear();

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
                    if(temp.IFF_CODE == selfData.IFF_CODE)
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

                        // DL
                        if (CheckPairedDL(temp))
                            SetItemDL(aircraft, temp);
                    }
                }
            }

            labelDist.Text = (tacanDist != MAX_TACAN_DISTANCE) ? string.Format("dst: {0}nm",tacanDist.ToString("N2")) : "dst: ---nm";
            labelDist.ForeColor = (tacanDist != MAX_TACAN_DISTANCE) ? Color.LawnGreen : labelDist.ForeColor = SystemColors.ButtonHighlight;
        }
        private void SetItemDL(Sim.Aircraft aircraft, InfoData infoData)
        {
            int module = (int)(140 * Vector.GeodesicDistance(aircraft.Position.geo.x, aircraft.Position.geo.z, selfEntity.Position.geo.x, selfEntity.Position.geo.z) * 0.00053995680346) / (int)rng;
            int phase = (int)Vector.GeodesicBearing(selfEntity.Position.geo.x, selfEntity.Position.geo.z, aircraft.Position.geo.x, aircraft.Position.geo.z);
            int heading = (int)(aircraft.Position.angles.y * 180.0 / Math.PI);
            int fl = (int)Math.Floor((aircraft.Position.geo.y * Sim.FEET_PER_METRE)/1000);
            EDLType tempType = (LeftDL.Contains(infoData.DL_CODE)) ? EDLType.LEFT : EDLType.RIGHT;

            Items.Add(new DataLinkItem(module, phase, heading, fl.ToString(), infoData.DL_CODE, pictureBoxBackground, tempType));
        }
        private bool CheckPairedDL(InfoData data)
        {
            if(data.DL_CODE != "00")
            {
                return LeftDL.Contains(data.DL_CODE) || RightDL.Contains(data.DL_CODE);
            } else
            {
                return false;
            }
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
            rotation = -1*(int)(aircraftPosition.angles.y * 180.0 / Math.PI);

            Bitmap rotatedBmp = new Bitmap(pictureBoxBackground.Image.Height, pictureBoxBackground.Image.Width);
            rotatedBmp.SetResolution(pictureBoxBackground.Image.HorizontalResolution, pictureBoxBackground.Image.VerticalResolution);

            Graphics g = Graphics.FromImage(rotatedBmp);
            g.TranslateTransform(150, 150);
            g.RotateTransform(rotation);
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
        private void LoadConfig()
        {
            if (main.hsdConfigForm != null && main.sim != null)
            {
                main.hsdConfigForm.Show();
                Hide();
            }
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
            labelRange.Text = string.Format("rng: {0}nm", (int)rng);

            if(selfEntity != null)
                RefreshWindow();
        }
        private void labelIFF_Click(object sender, EventArgs e)
        {
            LoadConfig();
        }
        private void labelTCN_Click(object sender, EventArgs e)
        {
            LoadConfig();
        }
        private void labelDL_Click(object sender, EventArgs e)
        {
            LoadConfig();
        }
        private class TestDL
        {
            enum ESide { N, NE, E, SE, S, SW, W, NW }
            enum EDist { FAR, CLOSE, MEDIUM }
            Queue<TestItem> Tests;
            public TestDL()
            {
                Tests = new Queue<TestItem>();
                // Change this in order to show another distance on tests
                FillQueue(EDist.MEDIUM);
            }
            public int[] GetItem()
            {
                TestItem tempItem = Tests.Dequeue();
                int[] temp = tempItem.GetInfo();
                Tests.Enqueue(tempItem);
                return temp;
            }
            private void FillQueue(EDist distance)
            {
                foreach (ESide position in Enum.GetValues(typeof(ESide)))
                {
                    foreach (ESide bearing in Enum.GetValues(typeof(ESide)))
                    {
                        Tests.Enqueue(new TestItem(position, bearing, distance));
                    }
                }
            }

            private class TestItem
            {
                private const int MAX_RANGE = 140;
                public ESide Position { get; set; }
                public ESide Bearing { get; set; }
                public EDist Distance { get; set; }
                public TestItem(ESide position, ESide bearing, EDist distance)
                {
                    Position = position;
                    Bearing = bearing;
                    Distance = distance;
                }
                /// <summary>
                /// 
                /// </summary>
                /// <returns>0 -> vector module, 1 -> vector phase, 2 -> bearing</returns>
                public int[] GetInfo()
                {
                    int[] result = new int[3];
                    int tempDist = (Distance == EDist.CLOSE) ? (MAX_RANGE / 3) : (Distance == EDist.MEDIUM) ? (MAX_RANGE / 3) * 2 : MAX_RANGE;

                    switch (Position)
                    {
                        case ESide.N:
                            result[0] = tempDist;
                            result[1] = 0;
                            break;
                        case ESide.E:
                            result[0] = tempDist;
                            result[1] = 90;
                            break;
                        case ESide.W:
                            result[0] = tempDist;
                            result[1] = 270;
                            break;
                        case ESide.S:
                            result[0] = tempDist;
                            result[1] = 180;
                            break;
                        case ESide.NE:
                            result[0] = tempDist;
                            result[1] = 45;
                            break;
                        case ESide.SE:
                            result[0] = tempDist;
                            result[1] = 135;
                            break;
                        case ESide.SW:
                            result[0] = tempDist;
                            result[1] = 225;
                            break;
                        case ESide.NW:
                            result[0] = tempDist;
                            result[1] = 315;
                            break;
                    }

                    switch (Bearing)
                    {
                        case ESide.N:
                            result[2] = 0;
                            break;
                        case ESide.E:
                            result[2] = 90;
                            break;
                        case ESide.S:
                            result[2] = 180;
                            break;
                        case ESide.W:
                            result[2] = 270;
                            break;
                        case ESide.NE:
                            result[2] = 45;
                            break;
                        case ESide.SE:
                            result[2] = 135;
                            break;
                        case ESide.SW:
                            result[2] = 225;
                            break;
                        case ESide.NW:
                            result[2] = 315;
                            break;
                    }

                    return result;
                }
            }
        }
    }
}