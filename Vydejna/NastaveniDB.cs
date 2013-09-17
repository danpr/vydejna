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
    

    struct vlastnosti
    {
        public string nameDB;
        public Boolean useUmisteni;
        public Boolean useServer;
        public Boolean useNameServer;
        public Boolean usePort;
        public Boolean useLocale;
        public Boolean useDriver;
        public Boolean useUserId;
        public Boolean useUserPasswd;
        public Boolean useAdminId;
        public Boolean useAdminPasswd;


        public vlastnosti(string nameDB, Boolean useUmisteni, Boolean useServer, Boolean useNameServer, Boolean usePort, Boolean useLocale, Boolean useDriver, Boolean useUserId, Boolean useUserPasswd, Boolean useAdminId, Boolean useAdminPasswd)
        {
            this.nameDB = nameDB;
            this.useServer = useServer;
            this.useUmisteni = useUmisteni;
            this.useNameServer = useNameServer;
            this.usePort = usePort;
            this.useLocale = useLocale;
            this.useDriver = useDriver;
            this.useUserId = useUserId;
            this.useUserPasswd = useUserPasswd;
            this.useAdminId = useAdminId;
            this.useAdminPasswd = useAdminPasswd;
        }

    }


    public partial class NastaveniDB : Form
    {

        private vlastnosti[] podporovaneDB = new vlastnosti[3] {new vlastnosti("SQLite",true,false,false,false,false,false,false,false,false,false), 
                                                                new vlastnosti("Postgres-ODBC",false,true,false,true,false,true,true,true,true,true),
                                                                new vlastnosti("Informix-ODBC",false,true,true,true,true,true,true,true,true,true)};

        private parametryDB aktualniParamtryDB;

        public NastaveniDB(parametryDB zadaneParametryDB)
        {
            InitializeComponent();
            aktualniParamtryDB = (parametryDB)zadaneParametryDB.Clone();

            setDBData();
            comboBoxTypDB.SelectedIndex = zadaneParametryDB.codeDB;
        }

        private void setDBData()
        {
            foreach (vlastnosti podporovaneDBItem in podporovaneDB) 
            {
                comboBoxTypDB.Items.Add(podporovaneDBItem.nameDB);
            }

    //        int i = (int)kodDB.dbNone;
            if (comboBoxTypDB.SelectedIndex == (int)kodDB.dbNone)
            {
                textBoxNazev.ReadOnly = true;
                textBoxUmisteni.ReadOnly = true;
                textBoxAdresa.ReadOnly = true;
                numericUpDownPort.Enabled = false;
                numericUpDownPort.ReadOnly = true;
                buttonBrowseDialog.Enabled = false;
                textBoxUserId.ReadOnly = true;
                textBoxUserPasswd.ReadOnly = true;
                textBoxAdminId.ReadOnly = true;
                textBoxAdminPasswd.ReadOnly = true;

            }

        }

        private void comboBoxTypDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            aktualniParamtryDB.codeDB = comboBoxTypDB.SelectedIndex;
            textBoxNazev.ReadOnly = false;
            textBoxNazev.Text = aktualniParamtryDB.nameDB;

            if (podporovaneDB[comboBoxTypDB.SelectedIndex].useUmisteni)
            {
                textBoxUmisteni.Text = aktualniParamtryDB.umistemiDB;
                textBoxUmisteni.ReadOnly = false;
                buttonBrowseDialog.Enabled = true;
            }
            else
            {
                aktualniParamtryDB.umistemiDB = textBoxUmisteni.Text;
                textBoxUmisteni.ReadOnly = true;
                textBoxUmisteni.Text = "";
                buttonBrowseDialog.Enabled = false;
            }

            if (podporovaneDB[comboBoxTypDB.SelectedIndex].useServer)
            {
                textBoxAdresa.Text = aktualniParamtryDB.adresaServerDB;
                textBoxAdresa.ReadOnly = false;
            }
            else
            {
                aktualniParamtryDB.adresaServerDB = textBoxAdresa.Text;
                textBoxAdresa.ReadOnly = true;
                textBoxAdresa.Text = "";
            }

            if (podporovaneDB[comboBoxTypDB.SelectedIndex].useNameServer)
            {
                textBoxJmenoServeru.Text = aktualniParamtryDB.nameDBServeru;
                textBoxAdresa.ReadOnly = false;
            }
            else
            {
                aktualniParamtryDB.nameDBServeru = textBoxJmenoServeru.Text;
                textBoxJmenoServeru.ReadOnly = true;
                textBoxJmenoServeru.Text = "";
            }




            if (podporovaneDB[comboBoxTypDB.SelectedIndex].usePort)
            {
                numericUpDownPort.Value = aktualniParamtryDB.portServerDB;
                numericUpDownPort.ReadOnly = false;
                numericUpDownPort.Enabled = true;
                buttonPortDefault.Enabled = true;
            }
            else
            {
                aktualniParamtryDB.portServerDB = (int)numericUpDownPort.Value;
                numericUpDownPort.Enabled = false;
                numericUpDownPort.ReadOnly = true;
                buttonPortDefault.Enabled = false;
                numericUpDownPort.Value = 0;
            }

            if (podporovaneDB[comboBoxTypDB.SelectedIndex].useLocale)
            {
                textBoxLocal.Text = aktualniParamtryDB.localizaceDBServeru;
                textBoxLocal.ReadOnly = false;
                textBoxLocal.Enabled = true;
                buttonLocalDefault.Enabled = true;
            }
            else
            {
                aktualniParamtryDB.localizaceDBServeru = textBoxLocal.Text;
                textBoxLocal.Enabled = false;
                textBoxLocal.ReadOnly = true;
                buttonLocalDefault.Enabled = false;
                textBoxLocal.Text = "";
            }



            if (podporovaneDB[comboBoxTypDB.SelectedIndex].useUserId)
            {
                textBoxUserId.Text = aktualniParamtryDB.userIdDB; 
                textBoxUserId.ReadOnly = false;
            }
            else
            {
                aktualniParamtryDB.userIdDB = textBoxUserId.Text;
                textBoxUserId.ReadOnly = true;
                textBoxUserId.Text = "";
            }

            if (podporovaneDB[comboBoxTypDB.SelectedIndex].useUserPasswd)
            {
                textBoxUserPasswd.Text = aktualniParamtryDB.userPasswdDB;
                textBoxUserPasswd.ReadOnly = false;
            }
            else
            {
                aktualniParamtryDB.userPasswdDB = textBoxUserPasswd.Text;
                textBoxUserPasswd.ReadOnly = true;
                textBoxUserPasswd.Text = "";
            }



            if (podporovaneDB[comboBoxTypDB.SelectedIndex].useAdminId)
            {
                textBoxAdminId.Text = aktualniParamtryDB.adminIdDB;
                textBoxAdminId.ReadOnly = false;
            }
            else
            {
                aktualniParamtryDB.adminIdDB = textBoxAdminId.Text;
                textBoxAdminId.ReadOnly = true;
                textBoxAdminId.Text = "";
            }

            if (podporovaneDB[comboBoxTypDB.SelectedIndex].useAdminPasswd)
            {
                textBoxAdminPasswd.Text = aktualniParamtryDB.adminPasswdDB;
                textBoxAdminPasswd.ReadOnly = false;
            }
            else
            {
                aktualniParamtryDB.adminPasswdDB = textBoxAdminPasswd.Text;
                textBoxAdminPasswd.ReadOnly = true;
                textBoxAdminPasswd.Text = "";
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonBrowseDialog_Click(object sender, EventArgs e)
        {
            /// nasteveni adresare
            dbFolderBrowserDialog.SelectedPath = textBoxUmisteni.Text;
            dbFolderBrowserDialog.ShowNewFolderButton = false;
            DialogResult result = dbFolderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                textBoxUmisteni.Text = dbFolderBrowserDialog.SelectedPath;
            }

            Application.DoEvents();
            
        }

        public parametryDB getParemetryDB()
        {
            aktualniParamtryDB.nameDB = textBoxNazev.Text;
            aktualniParamtryDB.umistemiDB = textBoxUmisteni.Text;
            aktualniParamtryDB.adresaServerDB = textBoxAdresa.Text;
            aktualniParamtryDB.portServerDB = (int)numericUpDownPort.Value;
            aktualniParamtryDB.userIdDB = textBoxUserId.Text;
            aktualniParamtryDB.userPasswdDB = textBoxUserPasswd.Text;
            aktualniParamtryDB.adminIdDB = textBoxAdminId.Text;
            aktualniParamtryDB.adminPasswdDB = textBoxAdminPasswd.Text;

            return aktualniParamtryDB;
        }

        private void buttonPortDefault_Click(object sender, EventArgs e)
        {
            if (comboBoxTypDB.SelectedIndex > -1)
            {
                numericUpDownPort.Value = vDatabase.defaultPortDB[comboBoxTypDB.SelectedIndex];
            }
        }

        private void checkBoxUserPasswd_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxUserPasswd.Checked == true)
            {
                textBoxUserPasswd.PasswordChar = (char)0;
            }
            else
            {
                textBoxUserPasswd.PasswordChar = '*';
            }
        }


        private void checkBoxAdminPasswd_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAdminPasswd.Checked == true)
            {
                textBoxAdminPasswd.PasswordChar = (char)0;
            }
            else
            {
                textBoxAdminPasswd.PasswordChar = '*';
            }

        }

        private void textBoxAdminPasswd_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonLocalDefault_Click(object sender, EventArgs e)
        {
            if (comboBoxTypDB.SelectedIndex > -1)
            {
                textBoxLocal.Text = vDatabase.defaultLocaleDB[comboBoxTypDB.SelectedIndex];
            }

        }

        private void buttonDriverDefault_Click(object sender, EventArgs e)
        {
            if (comboBoxTypDB.SelectedIndex > -1)
            {
                textBoxDriver.Text = vDatabase.defaultDriverDB[comboBoxTypDB.SelectedIndex];
            }

        }

    }
}
