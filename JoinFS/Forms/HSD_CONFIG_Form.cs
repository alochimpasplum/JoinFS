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
            textBoxIFF.Text = main.hsdForm.selfData.IFF_CODE;
            textBoxDL.Text = main.hsdForm.selfData.DL_CODE;
            textBoxTCN.Text = main.hsdForm.selfData.AA_TCN.Substring(0, 2);
            radioButtonX.Checked = main.hsdForm.selfData.AA_TCN.Substring(2, 1) == "X";
            radioButtonY.Checked = main.hsdForm.selfData.AA_TCN.Substring(2, 1) == "Y";
        }
        private void ApplyChanges()
        {
            lock (main.conch)
            {
                main.sim.userFlightPlan.remarks = JsonSerializer.Serialize<InfoData>(main.hsdForm.selfData);
            }
        }
        private void SetTCN()
        {
            string temp = textBoxDL.Text;

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
        }

        private void textBoxDL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if(Int32.TryParse(textBoxDL.Text, out int value))
                {
                    while(textBoxDL.Text.Length < 2)
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
        }

        private void textBoxTCN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
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
        }
    }
}
