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
        private Int32 maximumMnozstvi = 0;
        private vDatabase myDataBase;
        private string osCislo;

        public PrevodNaradi(Hashtable DBRow, vDatabase myDataBase)
        {
            InitializeComponent();
            CancelButton = buttonCancel;
            AcceptButton = buttonOK;

            buttonOK.Enabled = false;

            labelJmenoNove.Text = "";
            labelPrijmeniNove.Text = "";
            labelStrediskoNove.Text = "";
            labelProvozNove.Text = "";

            this.myDataBase = myDataBase;

            labelPrijmeni.Text = Convert.ToString(DBRow["prijmeni"]);
            labelJmeno.Text = Convert.ToString(DBRow["jmeno"]);
            labelOsCislo.Text = Convert.ToString(DBRow["oscislo"]);
            osCislo = Convert.ToString(DBRow["oscislo"]).Trim();
            labelStredisko.Text = Convert.ToString(DBRow["stredisko"]);
            labelProvoz.Text = Convert.ToString(DBRow["odeleni"]);
            labelNazev.Text = Convert.ToString(DBRow["nazev"]);
            labelIEvCislo.Text = Convert.ToString(DBRow["vevcislo"]);
            labelJK.Text = Convert.ToString(DBRow["jk"]);
            labelVypujceno.Text = Convert.ToString(DBRow["stavks"]);

            maximumMnozstvi = Convert.ToInt32(DBRow["stavks"]);
            numericUpDownMnozstvi.Value = maximumMnozstvi;
            numericUpDownMnozstvi.Maximum = maximumMnozstvi;

            textBoxPoznamka.Text = "Převedeno";

            string lastNewOsCislo = ConfigReg.loadSettingLastNewOsCislo();
            if (lastNewOsCislo != null)
            {
                if (lastNewOsCislo.Trim() != "")
                {
                    if (myDataBase.tableOsobyItemExist(lastNewOsCislo))
                    {
                        Hashtable osobaRow = myDataBase.getOsobyLine(lastNewOsCislo, null);
                        showNewOsobaInfo(osobaRow);
                        testKompletnosti();
                    }
                }
            }
        }

        private void buttonChoosePerson_Click(object sender, EventArgs e)
        {
            loadNewOsCisloFromDialog();
        }

        private void loadNewOsCisloFromDialog()
        {
            VyberRadku vyberOsoby = new VyberRadku(myDataBase, this.Font);
            if (vyberOsoby.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Hashtable osobaRow = vyberOsoby.getDBRowFromSelectedRow(null);
                showNewOsobaInfo(osobaRow);
                if (textBoxOsCisloNove.Text == osCislo)
                {
                    MessageBox.Show("Nelze převádět na stejného pracovníka.");
                }
                testKompletnosti();
            }
        }


        private void showNewOsobaInfo(Hashtable osobaRow)
        {
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

        }

        private void testKompletnosti()
        {
            if ((textBoxOsCisloNove.Text != "") && (numericUpDownMnozstvi.Value > 0) && (textBoxOsCisloNove.Text != osCislo ))
            {
                buttonOK.Enabled = true;
            }

            else
            {
                buttonOK.Enabled = false;
            }
        }


        public Int32 getKs()
        {
            return Convert.ToInt32(numericUpDownMnozstvi.Value);
        }


        public string getPoznamka()
        {
            return textBoxPoznamka.Text;
        }


        public DateTime getDatum()
        {
            return Convert.ToDateTime(dateTimePickerDatum.Value);
        }


        public string getNewOsCislo()
        {
            return textBoxOsCisloNove.Text.Trim();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (numericUpDownMnozstvi.Value > 0)
            {
                buttonOK.DialogResult = DialogResult.OK;
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Je nutno zadat množství převedeného nářadí.");
            }

        }



    }
}
