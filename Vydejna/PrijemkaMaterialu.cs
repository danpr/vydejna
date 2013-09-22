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



        public PrijemkaMaterialu()
        {
            InitializeComponent();
        }


        public PrijemkaMaterialu(Hashtable DBRow, vDatabase myDataBase)
        {
            InitializeComponent();
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
            setButtonOK();
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

    
    

    }
}
