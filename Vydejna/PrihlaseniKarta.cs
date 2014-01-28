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
    public partial class PrihlaseniKarta : Form
    {

        private vDatabase myDataBase;

        public PrihlaseniKarta(vDatabase myDataBase)
        {

            InitializeComponent();

            this.myDataBase = myDataBase;
            buttonOK.Enabled = false;


            this.AcceptButton = buttonOK;
            this.CancelButton = buttonCancel;
        }


        private Boolean testKompletnosti()
        {
            if ((textBoxUserID.Text.Length > 0) && (textBoxPass1.Text.Length > 0) )
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void textBoxPass1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                Int32 i = 0;
                e.KeyChar = Convert.ToChar(i);
            }

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

        private void textBoxUserID_TextChanged(object sender, EventArgs e)
        {
            setEnableButtonOK();
        }

        private void textBoxPass1_TextChanged(object sender, EventArgs e)
        {
            setEnableButtonOK();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (testKompletnosti())
            {
                // test existence jmena
                if (UzivatelData.userIDExist(textBoxUserID.Text, myDataBase))
                {
                    // test hesla
                    Close();

                }
                else
                {
                    MessageBox.Show("Jméno uživatele neexistuje.");
                    DialogResult = System.Windows.Forms.DialogResult.None;
                }


            }
        }


    }
}
