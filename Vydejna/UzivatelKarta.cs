using System;
using System.Collections.Generic;
using System.Collections;
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
        const Int32 permStrLength = 60; //0 -59

        
        private vDatabase myDataBase;
        private Boolean kartaIsAddType = true;

        // pridani polozky
        public UzivatelKarta(vDatabase myDataBase, Font myFont, Boolean admin = false)
        {
            InitializeComponent();

            kartaIsAddType = true;

            this.myDataBase = myDataBase;

            this.CancelButton = buttonCancel;
            this.AcceptButton = buttonOK;

            buttonOK.Enabled = false;
            if (admin)
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
            else
            {
                radioButton1.Checked = false;
                radioButton2.Checked = true;
            }

            setTreeView();
            this.Font = myFont;
        }

        // editace polozky
        public UzivatelKarta(vDatabase myDataBase, Hashtable DBRow, Font myFont)
        {
            InitializeComponent();

            textBoxUserID.Font = new Font(myFont, FontStyle.Bold);


            kartaIsAddType = false;

            this.myDataBase = myDataBase;

            this.CancelButton = buttonCancel;
            this.AcceptButton = buttonOK;

            this.Font = myFont;

            buttonOK.Enabled = false;

            bool admin = false;

            if (DBRow.ContainsKey("admin"))
            {
                if (Convert.ToString(DBRow["admin"]) != "N")
                {
                    admin = true;
                }
            }


            if (admin)
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
            else
            {
                radioButton1.Checked = false;
                radioButton2.Checked = true;
            }

            if (DBRow.ContainsKey("userid"))
            {
                textBoxUserID.Text = Convert.ToString(DBRow["userid"]);
            }
            textBoxPass1.Enabled = false;
            textBoxPass2.Enabled = false;
            if (DBRow.ContainsKey("jmeno"))
            {
                textBoxJmeno.Text = Convert.ToString(DBRow["jmeno"]);
            }
            if (DBRow.ContainsKey("prijmeni"))
            {
                textBoxPrijmeni.Text = Convert.ToString(DBRow["prijmeni"]);
            }

            setTreeView();
            if (DBRow.ContainsKey("permission"))
            {
                stringToTree(Convert.ToString(DBRow["permission"]));
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
            if (kartaIsAddType) return testKompletnostiAdd(); else return testKompletnostiEdit();
        }

        private Boolean testKompletnostiAdd()
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

        private Boolean testKompletnostiEdit()
        {
            if ((textBoxUserID.Text.Length > 2) && ((textBoxJmeno.Text.Length > 0) || (textBoxPrijmeni.Text.Length > 0)))
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
            if (kartaIsAddType)
            {

                string passHash = UzivatelData.countHashPassd(textBoxPass1.Text);

                string permStr = treeToString();

                // zavolame ulozeni date
                if (!(myDataBase.tableUzivateleItemExist(textBoxUserID.Text)))
                {
                    // uziavtel neexistuje ulozime data
                    Int32 errCode = myDataBase.addNewLineUzivatele(textBoxUserID.Text, passHash, textBoxJmeno.Text, textBoxPrijmeni.Text, permStr, radioButton1.Checked);
                    if (errCode == -1)
                    {
                        MessageBox.Show("Nepodařilo se uložit nového uživatele.");
                        DialogResult = System.Windows.Forms.DialogResult.None;
                    }
                    if (errCode == -2)
                    {
                        MessageBox.Show("Lituji. Uživatel již v systému exisuje. Změna z jiného místa?");
                        DialogResult = System.Windows.Forms.DialogResult.None;
                    }

                }
                else
                {
                    MessageBox.Show("Lituji. Uživatel již v systému exisuje.");
                    DialogResult = System.Windows.Forms.DialogResult.None;
                }
            }
            else
            {
                // editace
                if (myDataBase.tableUzivateleItemExist(textBoxUserID.Text))
                {
                    // uziavtel neexistuje ulozime data
                    string permStr = treeToString();

                    Int32 errCode = myDataBase.editNewLineUzivatele(textBoxUserID.Text, textBoxJmeno.Text, textBoxPrijmeni.Text, permStr, radioButton1.Checked);
                    if (errCode == -1)
                    {
                        MessageBox.Show("Nepodařilo se opravit uživatele.");
                        DialogResult = System.Windows.Forms.DialogResult.None;
                    }
                    if (errCode == -2)
                    {
                        MessageBox.Show("Lituji. Uživatel již v systému exisuje.");
                        DialogResult = System.Windows.Forms.DialogResult.None;
                    }

                }

            }
        }

        public string getUserID()
        {
            return textBoxUserID.Text;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void setTreeView()
        {
            foreach (permStruct ps in UzivatelData.permList)
            {
                if (ps.parent == 0)
                {
                    Int32 index = ps.index;
                    string indexString = index.ToString();
                    TreeNode newNode = treeView1.Nodes.Add(indexString, ps.Description);
                    newNode.Tag = ps.index;
                }
                else
                {
                    // najdeme node
                    TreeNode parentNode = foundNode(ps.parent);
                    if (parentNode != null)
                    {
                        TreeNode newNode = parentNode.Nodes.Add(ps.index.ToString(), ps.Description);
                        newNode.Tag = ps.index;
                    }
                }
            }
            treeView1.ExpandAll();
        }

        private TreeNode foundNode(Int32 index)
        {
            if (treeView1.Nodes != null)
            {

                if (treeView1.Nodes.Find(index.ToString(), false).Count() > 0)
                {
                    return treeView1.Nodes.Find(index.ToString(), false)[0];
                }
                else
                {
                    if (treeView1.Nodes.Find(index.ToString(), true).Count() > 0)
                    {
                        return treeView1.Nodes.Find(index.ToString(), true)[0];
                    }
                    else    return null;
                }
            }
            else return null;


        }

        private void runOverNodesSet(TreeNodeCollection myNodes, char[] permChars)
        {
            foreach (TreeNode tn in myNodes)
            {
                if (tn.Nodes != null)
                {
                    runOverNodesSet(tn.Nodes, permChars);
                }

                if (tn.Checked == true)
                {
                    Int32 uk = (Int32)tn.Tag;
                    if (uk < permStrLength)
                    {
                        permChars[uk] = 'A';

                    }
                }
            }
        }


        private string treeToString()
        {
            char[] permChars = new char[permStrLength];

            for (Int32 i = 0; i < permStrLength; i++) permChars[i] = 'N';
            runOverNodesSet(treeView1.Nodes, permChars);
            return new string(permChars);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                treeView1.Enabled = false;
            }
            else
            {
                treeView1.Enabled = true;
            }
        }

        private void runOverNodesGet(TreeNodeCollection myNodes, char[] permChars)
        {
            foreach (TreeNode tn in myNodes)
            {
                if (tn.Nodes != null)
                {
                    runOverNodesGet(tn.Nodes, permChars);
                }
                Int32 uk = (Int32)tn.Tag;
                if (permChars[uk] != 'N')
                {
                    tn.Checked = true;
                }
                else
                {
                    tn.Checked = false;
                }
            }
        }


        private void stringToTree(string permStr)
        {
            Int32 ls = permStr.Length;
            char[] permChars = new char[permStrLength];
            char[] permCharsHelp = permStr.ToCharArray(0, permStr.Length);
            for (Int32 i = 0; i < permStrLength; i++)
            {
                if (i < ls) permChars[i] = permCharsHelp[i]; else permChars[i] = 'N';
            }
            runOverNodesGet(treeView1.Nodes, permChars);
        }

    }
}
