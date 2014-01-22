using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vydejna
{
    public partial class UzivatelKarta : Form
    {
        public UzivatelKarta()
        {
            InitializeComponent();

            this.CancelButton = buttonCancel;
            this.AcceptButton = buttonOK;
            radioButton1.Checked = true;

            buttonOK.Enabled = false;
        }


        private void setEnableButtonOK()
        {
            if (testKompletnosti())
            {
                buttonOK.Enabled = true;
            }
            else
            {
                buttonOK.Enabled = false;
            }

        }


        private Boolean testKompletnosti()
        {
            if ((textBoxUserID.Text.Length > 2) && (textBoxPass1.Text.Length > 3) && (textBoxPass1.Text == textBoxPass2.Text)
                && (( textBoxJmeno.Text.Length > 0) || (textBoxPrijmeni.Text.Length > 0)))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void textBoxUserID_TextChanged(object sender, EventArgs e)
        {
            setEnableButtonOK();
        }

        private void textBoxUserID_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBoxPass1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                Int32 i = 0;
                e.KeyChar = Convert.ToChar(i);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            // spocteme hash hesla


        }
    }
}
