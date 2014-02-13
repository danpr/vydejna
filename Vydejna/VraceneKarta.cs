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
    public enum vKartaState { show, add, edit };

    public partial class VraceneKarta : Form
    {


        public class messager
        {
            public string jmeno;
            public string prijmeni;
            public string oscislo;
            public string stredisko;
            public string provoz;
            public string nazev;
            public string jk;
            public Int64 pocetKs; //cena
            public string rozmer;
            public string csn;
            public decimal cena;
            public DateTime datum;
            public string zakazka;
            public string konto;
            public Int32 poradi;
            
            
            public messager(Int32 poradi, string jmeno, string prijmeni, string oscislo, string stredisko, string provoz, string nazev, string jk,
                            Int64 pocetKs, string rozmer, string csn, decimal cena, DateTime datum, string zakazka, string konto)
            {
                this.jmeno = jmeno;
                this.prijmeni = prijmeni;
                this.oscislo = oscislo;
                this.stredisko = stredisko;
                this.provoz = provoz;
                this.nazev = nazev;
                this.jk = jk;
                this.pocetKs = pocetKs;
                this.rozmer = rozmer;
                this.csn = csn;
                this.cena = cena;
                this.datum = datum;
                this.zakazka = zakazka;
                this.konto = konto;
                this.poradi = poradi;
            }
        }


        private vKartaState state;
        private vDatabase myDB;
        private Int32 poradi;
        private tableItemExistDelgInt testExistItem;

        public VraceneKarta(Hashtable DBRow, vDatabase myDataBase, tableItemExistDelgInt testExistItem, Font myFont, vKartaState state = vKartaState.show)
        {
            InitializeComponent();
            this.state = state;
            this.testExistItem = testExistItem;

            myDB = myDataBase;
            if (state == vKartaState.edit) setEditState();
            else setShowState();

            setData(DBRow);

            AcceptButton = buttonOK;
            CancelButton = buttonCancel;
            this.Font = myFont;
        }

        public void setWinName (string winName)
        {
            this.Text = winName;
        }


        public VraceneKarta(Hashtable DBRow, Font myFont)
        {
            InitializeComponent();
            this.state = vKartaState.show;
            setShowState();
            setData(DBRow);
            CancelButton = buttonCancel;
            this.Font = myFont;
        }

        public void setData(Hashtable DBRow)
        {
            textBoxJmeno.Text = Convert.ToString(DBRow["krjmeno"]).Trim();
            textBoxPrijmeni.Text = Convert.ToString(DBRow["jmeno"]).Trim();
            textBoxOsCislo.Text = Convert.ToString(DBRow["oscislo"]);
            textBoxStredisko.Text = Convert.ToString(DBRow["dilna"]);
            textBoxProvoz.Text = Convert.ToString(DBRow["pracoviste"]);
            textBoxNazev.Text = Convert.ToString(DBRow["nazev"]);
            textBoxJK.Text = Convert.ToString(DBRow["jk"]);
            numericUpDownPocetKS.Value = Convert.ToInt32(DBRow["pocetks"]);   
            textBoxRozmer.Text = Convert.ToString(DBRow["rozmer"]);
            textBoxCSN.Text = Convert.ToString(DBRow["csn"]);
            numericUpDownCena.Value = Convert.ToDecimal (DBRow["cena"]);
            if (Convert.ToDateTime(DBRow["datum"]) < new DateTime(1753, 1, 1)) 
            {
                dateTimePickerDatum.Value = new DateTime(1890,1,1);
            }
            else
            {
            dateTimePickerDatum.Value = Convert.ToDateTime(DBRow["datum"]);
            }

            textBoxZakázka.Text = Convert.ToString(DBRow["vyrobek"]);
            textBoxKonto.Text = Convert.ToString(DBRow["konto"]);
            poradi = Convert.ToInt32(DBRow["poradi"]);

        }


        private void setShowState()
        {
            textBoxJmeno.BackColor = System.Drawing.SystemColors.Window;
            textBoxPrijmeni.BackColor = System.Drawing.SystemColors.Window;
            textBoxOsCislo.BackColor = System.Drawing.SystemColors.Window;
            textBoxProvoz.BackColor = System.Drawing.SystemColors.Window;
            textBoxStredisko.BackColor = System.Drawing.SystemColors.Window;
            textBoxRozmer.BackColor = System.Drawing.SystemColors.Window;
            textBoxZakázka.BackColor = System.Drawing.SystemColors.Window;
            textBoxNazev.BackColor = System.Drawing.SystemColors.Window;
            textBoxJK.BackColor = System.Drawing.SystemColors.Window;
            textBoxKonto.BackColor = System.Drawing.SystemColors.Window;
            textBoxCSN.BackColor = System.Drawing.SystemColors.Window;
            numericUpDownCena.BackColor = System.Drawing.SystemColors.Window;
            numericUpDownPocetKS.BackColor = System.Drawing.SystemColors.Window;

            buttonOK.Visible = false;
            buttonOK.Enabled = false;
        }

        private void setEditState()
        {
            textBoxJmeno.ReadOnly = false;
            textBoxPrijmeni.ReadOnly = false;
            textBoxOsCislo.ReadOnly = false;
            textBoxStredisko.ReadOnly = false;
            textBoxProvoz.ReadOnly = false;
            textBoxNazev.ReadOnly = false;
            textBoxJK.ReadOnly = false;
            numericUpDownPocetKS.ReadOnly = false;
            numericUpDownPocetKS.Increment = 1;
            textBoxRozmer.ReadOnly = false;
            textBoxCSN.ReadOnly = false;
            numericUpDownCena.ReadOnly = false;
            numericUpDownCena.Increment = 1;
            dateTimePickerDatum.Enabled = true;
            textBoxZakázka.ReadOnly = false;
            textBoxKonto.ReadOnly = false;
            textBoxJK.ReadOnly = true;

            buttonOK.Visible = true;
            buttonOK.Enabled = true;
        }


        public messager getMesseger()
        {
            messager prepravka = new messager(poradi, textBoxJmeno.Text, textBoxPrijmeni.Text, textBoxOsCislo.Text, textBoxStredisko.Text,
                                  textBoxProvoz.Text, textBoxNazev.Text, textBoxJK.Text, Convert.ToInt32(numericUpDownPocetKS.Value),
                                  textBoxRozmer.Text, textBoxCSN.Text, numericUpDownCena.Value, dateTimePickerDatum.Value,
                                  textBoxZakázka.Text, textBoxKonto.Text);
                                  //Convert.ToInt64(numericUpDownMinStav.Value), Convert.ToInt64(numericUpDownUcetStav.Value), textBoxPoznamka.Text);
            return prepravka;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {

            if (state == vKartaState.edit)
            {
//                if (myDB.tablePoskozenoItemExist(poradi))
                if (testExistItem(poradi))               
                {
                   buttonOK.DialogResult = DialogResult.OK;
                   this.DialogResult = DialogResult.OK;
                   Close();
                }
                else
                {
                    MessageBox.Show("Položka již neexistuje.");
                }
            }    
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void numericUpDownPocetKS_Enter(object sender, EventArgs e)
        {
            (sender as NumericUpDown).Select(0, (sender as NumericUpDown).Text.Length);
        }

        private void numericUpDownCena_Enter(object sender, EventArgs e)
        {
            (sender as NumericUpDown).Select(0, (sender as NumericUpDown).Text.Length);
        }


    }
}
