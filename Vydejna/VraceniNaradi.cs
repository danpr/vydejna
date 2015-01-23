using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vydejna
{
    public partial class VraceniNaradi : Form
    {
        private Int32 maximumMnozstvi = 0;

        public VraceniNaradi(Hashtable DBRow)
        {
            InitializeComponent();

            buttonOK.Enabled = false;

            labelPrijmeni.Text = Convert.ToString(DBRow["prijmeni"]);
            labelJmeno.Text = Convert.ToString(DBRow["jmeno"]);
            labelOsCislo.Text = Convert.ToString(DBRow["oscislo"]);
            labelStredisko.Text = Convert.ToString(DBRow["stredisko"]);
            labelProvoz.Text = Convert.ToString(DBRow["odeleni"]);
            labelNazev.Text = Convert.ToString(DBRow["nazev"]);
            labelIEvCislo.Text = Convert.ToString(DBRow["vevcislo"]);
            labelJK.Text = Convert.ToString(DBRow["jk"]);
            labelVypujceno.Text = Convert.ToString(DBRow["stavks"]);

//            numericUpDownMnozstvi.Maximum = Convert.ToInt32(DBRow["stavks"]);
            maximumMnozstvi = Convert.ToInt32(DBRow["stavks"]); ;
            textBoxPoznamka.Text = "Vráceno";

            AcceptButton = buttonOK;
            CancelButton = buttonCancel;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (numericUpDownMnozstvi.Value > 0)
            {
                buttonOK.DialogResult = DialogResult.OK;
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Je nutno zadat množství vraceného nářadí.");
            }

        }

        private void numericUpDownKs_ValueChanged(object sender, EventArgs e)
        {
            testMaximalnihoMnozstvi();
            if ((numericUpDownMnozstvi.Value > 0))
            {
                buttonOK.Enabled = true;
            }
            else
            {
                buttonOK.Enabled = false;
            }
        }


        private void testMaximalnihoMnozstvi()
        {
            if (numericUpDownMnozstvi.Value > maximumMnozstvi)
            {
                numericUpDownMnozstvi.Value = maximumMnozstvi;
                numericUpDownMnozstvi.Focus();
                System.Media.SystemSounds.Beep.Play();
                MessageBox.Show("Vrácené množství je příliš veliké. Maximálně je možno vrátit " + Convert.ToString(maximumMnozstvi) + " ks(ů).");
            }
        }




        public Int32 getKs()
        {
            return Convert.ToInt32(numericUpDownMnozstvi.Value);
        }


        public string getPoznamka()
        {
            return textBoxPoznamka.Text;
        }


        public DateTime getDatum()
        {
            return Convert.ToDateTime(dateTimePickerDatum.Value);
        }



    }
}
