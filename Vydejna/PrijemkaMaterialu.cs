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


        public PrijemkaMaterialu(Hashtable DBRow, vDatabase myDataBase)
        {
            InitializeComponent();
            labelJK.Text = Convert.ToString(DBRow["jk"]);
            labelVyrobce.Text = Convert.ToString(DBRow["vyrobce"]);
            labelNazev.Text = Convert.ToString(DBRow["nazev"]);
            labelDosudKs.Text = Convert.ToString(DBRow["ucetstav"]);

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
        }

        private void setButtonOK ()
        {
        if ((numericUpDownPrijemKs.Value > 0) && (textBoxPoznamka.Text.Trim() != ""))
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

    
    

    }
}
