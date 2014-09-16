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
    public partial class PrijemkaMaterialu : Form
    {


        public class messager
        {
            public string jk;
            public Int32 pocetKs;
            public DateTime datum;
            public string poznamka;
            public Int32 poradi;
            public decimal cena;


            public messager(Int32 poradi, string jk, Int32 pocetKs, DateTime datum, string poznamka, decimal cena)
            {
                this.jk = jk;
                this.pocetKs = pocetKs;
                this.datum = datum;
                this.poznamka = poznamka;
                this.poradi = poradi;
                this.cena = cena;
            }
        }


        public Int32 parentPoradi;
        private Font boldFont;

        public PrijemkaMaterialu(Hashtable DBRow, vDatabase myDataBase, Font myFont)
        {
            InitializeComponent();
            labelJK.Text = Convert.ToString(DBRow["jk"]);
            labelVyrobce.Text = Convert.ToString(DBRow["vyrobce"]);
            labelNazev.Text = Convert.ToString(DBRow["nazev"]);
            labelDosudKs.Text = Convert.ToString(DBRow["fyzstav"]) + " / " + Convert.ToString(DBRow["ucetstav"]);
            try
            {
                numericUpDownCenaKs.Value = Convert.ToDecimal(DBRow["cena"]);
            }
            catch (ArgumentOutOfRangeException)
            {
                decimal cenaValue = Convert.ToDecimal(DBRow["cena"]);
                if (cenaValue > numericUpDownCenaKs.Maximum)
                {
                    numericUpDownCenaKs.Value = numericUpDownCenaKs.Maximum;
                }
                else
                {
                    numericUpDownCenaKs.Value = numericUpDownCenaKs.Minimum;
                }
            }
            parentPoradi = Convert.ToInt32(DBRow["poradi"]);

            AcceptButton = buttonOK;
            CancelButton = buttonCancel;

            boldFont = new Font(myFont, FontStyle.Bold);
            this.Font = myFont;

            labelNazev.Font = boldFont;
            labelJK.Font = boldFont;
            labelVyrobce.Font = boldFont;
            labelDosudKs.Font = boldFont;
        }



        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDownPrijemKs_ValueChanged(object sender, EventArgs e)
        {
            setButtonOK();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (numericUpDownPrijemKs.Value > 0)
            {
                buttonOK.DialogResult = DialogResult.OK;
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Je nutno zadat množství materiálu.");
            }

        }

        private void setButtonOK ()
        {
        if ((numericUpDownPrijemKs.Value > 0) && (textBoxPoznamka.Text.Trim() != "") && (numericUpDownCenaKs.Value > 0))
            {
                buttonOK.Enabled = true;
            }
            else
            {
                buttonOK.Enabled = false;
            }

        }

        private void textBoxPoznamka_TextChanged(object sender, EventArgs e)
        {
            setButtonOK();
        }

        private void numericUpDownPrijemKs_Enter(object sender, EventArgs e)
        {
            (sender as NumericUpDown).Select(0, (sender as NumericUpDown).Text.Length);
        }


        public messager getMesseger()
        {
            messager prepravka = new messager(parentPoradi, labelJK.Text, Convert.ToInt32(numericUpDownPrijemKs.Value), dateTimePickerDatum.Value, textBoxPoznamka.Text,numericUpDownCenaKs.Value);
            return prepravka;
        }

        private void numericUpDownCenaKs_ValueChanged(object sender, EventArgs e)
        {
            setButtonOK();
        }


    }
}
