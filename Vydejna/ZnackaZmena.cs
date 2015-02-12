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
    public partial class ZnackaZmena : Form
    {
        private Int32 poradi;
        private vDatabase myDB;


        public ZnackaZmena(vDatabase myDB, Int32 poradi)
        {
            InitializeComponent();
            this.poradi = poradi;
            this.myDB = myDB;
            this.CancelButton = this.buttonCancel;
            this.AcceptButton = this.buttonOK;
            loadData();
        }

        private void loadData()
        {
            Hashtable DBRow = myDB.getNaradiLine(poradi,null);
            if (DBRow.Contains("kodd"))
            {
                textBoxZnacka.Text = Convert.ToString(DBRow["kodd"]);
            }
        }

        private void buttonRetry_Click(object sender, EventArgs e)
        {
            loadData();
        }

        public string getMark()
        {
            return textBoxZnacka.Text.Trim();
        }

        private void textBoxZnacka_KeyPress(object sender, KeyPressEventArgs e)
        {
            char znak = e.KeyChar;
            if (znak != ' ') e.KeyChar = Char.ToUpper(znak);
            else e.Handled = true;
        }
    }
}
