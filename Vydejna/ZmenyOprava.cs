using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vydejna
{
    public partial class ZmenyOprava : Form
    {
        private vDatabase myDB;
        private Int32 parPoradi;
        private Int32 poradi;

        public ZmenyOprava(vDatabase myDB, Int32 parPoradi, Int32 poradi)
        {
            InitializeComponent();
            this.myDB = myDB;
            this.parPoradi = parPoradi;
            this.poradi = poradi;
            loadData();
            buttonOK.Enabled = false;
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void loadData()
        {
           Hashtable DBRow = myDB.getZmenyLine(parPoradi, poradi, null);
           if (DBRow != null)
           {
               labelDatum.Text = Convert.ToString(DBRow["datum"]);
               labelOperace.Text = Convert.ToString(DBRow["stav"]);
               textBoxPoznamka.Text = Convert.ToString(DBRow["poznamka"]);
               textBoxVevcislo.Text = Convert.ToString(DBRow["vevcislo"]);
               labelPrijem.Text = Convert.ToString(DBRow["prijem"]);
               labelVydej.Text = Convert.ToString(DBRow["vydej"]);
               labelZustatek.Text = Convert.ToString(DBRow["zustatek"]);
               labelOsCislo.Text = Convert.ToString(DBRow["zapkarta"]);
           }

        }

        private void buttonRetry_Click(object sender, EventArgs e)
        {
            loadData();
            buttonOK.Enabled = true;
        }

        private void textBoxPoznamka_TextChanged(object sender, EventArgs e)
        {
            buttonOK.Enabled = true;
        }

        private void textBoxVevcislo_TextChanged(object sender, EventArgs e)
        {
            buttonOK.Enabled = true;

        }

        public string getPoznamka()
        {
            return textBoxPoznamka.Text;
        }

        public string getVevcislo ()
        {
            return textBoxVevcislo.Text;
        }


    }
}
