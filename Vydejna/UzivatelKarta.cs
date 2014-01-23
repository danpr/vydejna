using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Vydejna
{
    public partial class UzivatelKarta : Form
    {

        private vDatabase myDataBase;

        public UzivatelKarta(vDatabase myDataBase, Boolean admin = false)
        {
            InitializeComponent();

            this.myDataBase = myDataBase;

            this.CancelButton = buttonCancel;
            this.AcceptButton = buttonOK;

            buttonOK.Enabled = false;
            if (admin)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton1.Checked = false;
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
            using (SHA1 sha1FingerPrint = SHA1.Create())
            {
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] passByte = encoder.GetBytes(textBoxPass1.Text);
                sha1FingerPrint.ComputeHash(passByte);
                string passHash = Convert.ToBase64String(sha1FingerPrint.Hash);

                // zavolame ulozeni date

                if (!(myDataBase.tableUzivateleItemExist(textBoxUserID.Text)))
                {
                    // uziavtel neexistuje ulozime data

                }
            }

        }
    }
}
