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
            labelVypujceno.Text = Convert.ToString(DBRow["ks"]);
            numericUpDownKs.Maximum = Convert.ToInt32(DBRow["ks"]);
            textBoxPoznamka.Text = "Vráceno";

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
                MessageBox.Show("Je nutno zadat množství vraceného nářadí.");
            }

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

        public Int32 getKs()
        {
            return Convert.ToInt32(numericUpDownKs.Value);
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
