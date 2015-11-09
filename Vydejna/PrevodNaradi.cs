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
    public partial class PrevodNaradi : Form
    {

        private vDatabase myDataBase;

        public PrevodNaradi()
        {
            InitializeComponent();
        }

        private void buttonChoosePerson_Click(object sender, EventArgs e)
        {
            VyberRadku vyberOsoby = new VyberRadku(myDataBase, this.Font);
            if (vyberOsoby.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Hashtable osobaRow = vyberOsoby.getDBRowFromSelectedRow(null);
                if (osobaRow != null)
                {
                    if (osobaRow.ContainsKey("oscislo"))
                    {
                        textBoxOsCisloNove.Text = Convert.ToString(osobaRow["oscislo"]);
/////                        textBoxCisZak.Focus();
                    }
                    if (osobaRow.ContainsKey("jmeno")) labelJmenoNove.Text = Convert.ToString(osobaRow["jmeno"]);
                    if (osobaRow.ContainsKey("prijmeni")) labelPrijmeniNove.Text = Convert.ToString(osobaRow["prijmeni"]);
                    if (osobaRow.ContainsKey("stredisko")) labelStrediskoNove.Text = Convert.ToString(osobaRow["stredisko"]);
                    if (osobaRow.ContainsKey("pracoviste")) labelProvozNove.Text = Convert.ToString(osobaRow["pracoviste"]);
                }
                testKompletnosti();
            }
        }


        private void testKompletnosti()
        {
            if ((textBoxOsCisloNove.Text != "") && (numericUpDownMnozstvi.Value > 0))
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
