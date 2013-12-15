using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vydejna
{
    public partial class ZapujceniNaradi : Form
    {
        public ZapujceniNaradi(Hashtable DBRow, string nazev, string JK , Int32 fyzStav)
        {
            InitializeComponent();

            buttonOK.Enabled = false;

            labelPrijmeni.Text = Convert.ToString(DBRow["prijmeni"]);
            labelJmeno.Text = Convert.ToString(DBRow["jmeno"]);
            labelOsCislo.Text = Convert.ToString(DBRow["oscislo"]);
            labelStredisko.Text = Convert.ToString(DBRow["stredisko"]);
            labelProvoz.Text = Convert.ToString(DBRow["odeleni"]);

            labelNazev.Text = nazev;
            labelJK.Text = JK;
            labelFyzStav.Text = Convert.ToString(fyzStav);

            textBoxVevCislo.Text = "";

            numericUpDownKs.Maximum = fyzStav;
            textBoxPoznamka.Text = "Zapůjčeno";

        }

        private void numericUpDownKs_ValueChanged(object sender, EventArgs e)
        {
            if ((numericUpDownKs.Value > 0))
            {
                buttonOK.Enabled = true;
            }

            else
            {
                buttonOK.Enabled = false;
            }

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (numericUpDownKs.Value > 0)
            {
                buttonOK.DialogResult = DialogResult.OK;
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Je nutno zadat množství vypujčeného nářadí.");
            }

        }

        public Int32 getKs()
        {
            return Convert.ToInt32( numericUpDownKs.Value);
        }


        public string getPoznamka()
        {
            return textBoxPoznamka.Text;
        }


        public DateTime getDatum()
        {
            return Convert.ToDateTime( dateTimePickerDatum.Value);
        }


        public string getOsCiclo()
        {
            return labelOsCislo.Text;
        }

        public string getJmeno()
        {
            return labelJmeno.Text;
        }

        public string getPrijmeni()
        {
            return labelPrijmeni.Text;
        }


        public string getVevCislo()
        {
            return textBoxVevCislo.Text;
        }
    }
}
