using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using static JoinFS.Forms.HSDForm;

namespace JoinFS.Forms
{
    public partial class HSD_CONFIG_Form : Form
    {
        public Sim.FlightPlan plan;

        Main main;
        public HSD_CONFIG_Form(Main main)
        {
            this.main = main;
            InitializeComponent();

            Icon = main.icon;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private void HSD_CONFIG_Form_Load(object sender, EventArgs e)
        {
            LoadValues();
        }
        private void LoadValues()
        {
            lock (main.conch)
            {
                textBoxIFF.Text = main.hsdForm.selfData.IFF_CODE;
                textBoxDL.Text = main.hsdForm.selfData.DL_CODE;
                textBoxTCN.Text = main.hsdForm.selfData.AA_TCN.Substring(0, 2);
                radioButtonX.Checked = main.hsdForm.selfData.AA_TCN.Substring(2, 1) == "X";
                radioButtonY.Checked = main.hsdForm.selfData.AA_TCN.Substring(2, 1) == "Y";
            }
        }
        private void ApplyChanges()
        {
            lock (main.conch)
            {
                Sim.Aircraft selfEntity = main.sim.objectList.Find(x => x.owner == Sim.Obj.Owner.Me) as Sim.Aircraft;
                selfEntity.flightPlan.remarks = JsonSerializer.Serialize<InfoData>(main.hsdForm.selfData);
            }
        }
        private void SetTCN()
        {
            string temp = textBoxTCN.Text;

            if (radioButtonX.Checked)
                temp += "X";
            if (radioButtonY.Checked)
                temp += "Y";

            main.hsdForm.selfData.AA_TCN = temp;
            ApplyChanges();
        }

        private void radioButtonX_Click(object sender, EventArgs e)
        {
            SetTCN();
        }

        private void radioButtonY_Click(object sender, EventArgs e)
        {
            SetTCN();
        }

        private void textBoxIFF_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                CheckIFF();
            }
        }
        private void CheckIFF()
        {
            if (Int32.TryParse(textBoxIFF.Text, out int value))
            {
                while (textBoxIFF.Text.Length < 4)
                {
                    textBoxIFF.Text = "0" + textBoxIFF.Text;
                }

                main.hsdForm.selfData.IFF_CODE = textBoxIFF.Text;
                ApplyChanges();
            }
            else
            {
                // TODO: mostrar el mensaje "introduce un valor válido"
                LoadValues();
            }
        }

        private void textBoxDL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                CheckDL();
            }
        }
        private void CheckDL()
        {
            if (Int32.TryParse(textBoxDL.Text, out int value))
            {
                while (textBoxDL.Text.Length < 2)
                {
                    textBoxDL.Text = "0" + textBoxDL.Text;
                }

                main.hsdForm.selfData.DL_CODE = textBoxDL.Text;
                ApplyChanges();
            }
            else
            {
                // TODO: mostrar el mensaje "introduce un valor válido"
                LoadValues();
            }
        }

        private void textBoxTCN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                CheckTCN();
            }
        }
        private void CheckTCN()
        {
            if (Int32.TryParse(textBoxTCN.Text, out int value))
            {
                while (textBoxTCN.Text.Length < 2)
                {
                    textBoxTCN.Text = "0" + textBoxTCN.Text;
                }

                SetTCN();
            }
            else
            {
                // TODO: mostrar el mensaje "introduce un valor válido"
                LoadValues();
            }
        }

        private void HSD_CONFIG_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void textBoxIFF_Leave(object sender, EventArgs e)
        {
            CheckIFF();
        }

        private void textBoxDL_Leave(object sender, EventArgs e)
        {
            CheckDL();
        }

        private void textBoxTCN_Leave(object sender, EventArgs e)
        {
            CheckTCN();
        }

        private void radioButtonX_Leave(object sender, EventArgs e)
        {
            CheckTCN();
        }

        private void radioButtonY_Leave(object sender, EventArgs e)
        {
            CheckTCN();
        }
    }
}
