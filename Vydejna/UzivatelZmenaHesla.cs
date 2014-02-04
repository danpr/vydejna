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
    public partial class UzivatelZmenaHesla : Form
    {
        private vDatabase myDataBase;

        public UzivatelZmenaHesla(vDatabase myDataBase, string userid, Boolean oldPass = true)
        {
            InitializeComponent();

            this.myDataBase = myDataBase;

            labelUserID.Text = userid;
            if (!(oldPass))
            {
                textBoxOldPass.Enabled = false;
                textBoxOldPass.ReadOnly = true;
            }

            setEnableButtonOK();
        }

        private void textBoxOldPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                Int32 i = 0;
                e.KeyChar = Convert.ToChar(i);
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

        private void textBoxPass2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                Int32 i = 0;
                e.KeyChar = Convert.ToChar(i);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            // test
            string passOldHash = "";
            if (textBoxOldPass.Enabled)
            {
                passOldHash = UzivatelData.countHashPassd(textBoxOldPass.Text);
            }
            string passHash = UzivatelData.countHashPassd(textBoxPass1.Text);


            // zavolame ulozeni date
            if (myDataBase.tableUzivateleItemExist(labelUserID.Text))
            {
                Boolean oldPassIsOK = true;
                if (textBoxOldPass.Enabled)
                {
                    Hashtable DBRow;
                    DBRow = myDataBase.getUzivateleLine(labelUserID.Text, null);
                    if (DBRow.ContainsKey("password"))
                    {
                        if (passOldHash == Convert.ToString(DBRow["password"]).Trim())
                        {
                            // heslo ok
                            oldPassIsOK = true;
                        }
                        else
                        {
                            MessageBox.Show("Lituji. Původní heslo není spravné.");
                            // heslo false
                            oldPassIsOK = false;
                            DialogResult = System.Windows.Forms.DialogResult.None;
                        }
                    }
                    else // nenacetlo se heslo
                    {
                        DialogResult = System.Windows.Forms.DialogResult.None;

                    }

                }
                else oldPassIsOK = true;
                // uzivatel neexistuje ulozime data
                if (oldPassIsOK)
                {
                    Int32 errCode = myDataBase.editNewLinePasswordUzivatele(labelUserID.Text, passHash);
                    if (errCode == -1)
                    {
                        MessageBox.Show("Nepodařilo se změnit heslo.");
                        DialogResult = System.Windows.Forms.DialogResult.None;
                    }
                    if (errCode == -2)
                    {
                        MessageBox.Show("Lituji. Uživatel již v systému neexisuje. Změna z jiného místa?");
                        DialogResult = System.Windows.Forms.DialogResult.None;
                    }
                }
            }
            else
            {
                MessageBox.Show("Lituji. Uživatel již v systému neexisuje.");
                DialogResult = System.Windows.Forms.DialogResult.None;
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
            if (((textBoxOldPass.Text.Length > 2) || (!(textBoxOldPass.Enabled))) 
                && ((textBoxPass1.Text.Length > 2) || (textBoxPass2.Text.Length > 2)) && (textBoxPass1.Text == textBoxPass2.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void textBoxOldPass_TextChanged(object sender, EventArgs e)
        {
            setEnableButtonOK();
        }

        private void textBoxPass1_TextChanged(object sender, EventArgs e)
        {
             setEnableButtonOK();
        }

        private void textBoxPass2_TextChanged(object sender, EventArgs e)
        {
            setEnableButtonOK();
        }

    }
}
