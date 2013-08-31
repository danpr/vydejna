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


        public VraceneKarta(Hashtable DBRow, vDatabase myDataBase, vKartaState state = vKartaState.show)
        {
            InitializeComponent();
            this.state = state;
            if (state == vKartaState.edit) setEditState();
            myDB = myDataBase;
            if (state == vKartaState.show)
            {
                buttonOK.Visible = false;
                buttonOK.Enabled = false;
            }
            else
            {
                buttonOK.Visible = true;
                buttonOK.Enabled = true;
            }

            setData(DBRow);
        }


        public VraceneKarta(Hashtable DBRow)
        {
            InitializeComponent();
            setData(DBRow);

        }

        public void setData(Hashtable DBRow)
        {
            textBoxJmeno.Text = Convert.ToString(DBRow["krjmeno"]);
            textBoxPrijmeni.Text = Convert.ToString(DBRow["jmeno"]);
            textBoxOsCislo.Text = Convert.ToString(DBRow["cislo"]);
            textBoxStredisko.Text = Convert.ToString(DBRow["dilna"]);
            textBoxProvoz.Text = Convert.ToString(DBRow["pracoviste"]);
            textBoxNazev.Text = Convert.ToString(DBRow["nazev"]);
            textBoxJK.Text = Convert.ToString(DBRow["jk"]);
            numericUpDownPocetKS.Value = Convert.ToInt32(DBRow["pocetks"]);   
            textBoxRozmer.Text = Convert.ToString(DBRow["rozmer"]);
            textBoxCSN.Text = Convert.ToString(DBRow["csn"]);
            numericUpDownCena.Value = Convert.ToDecimal (DBRow["cena"]);
//            DateTime minDate = new DateTime(1753,1,1);
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

        private void setEditState()
        {
            setAddState();
            textBoxJK.ReadOnly = true;
        }

        private void setAddState()
        {
            textBoxJmeno.ReadOnly = false;
            textBoxPrijmeni.ReadOnly = false;
            textBoxOsCislo.ReadOnly = false;
            textBoxStredisko.ReadOnly = false;
            textBoxProvoz.ReadOnly = false;
            textBoxNazev.ReadOnly = false;
            textBoxJK.ReadOnly = false;
            numericUpDownPocetKS.ReadOnly = false;
            numericUpDownPocetKS.Enabled = true;
            textBoxRozmer.ReadOnly = false;
            textBoxCSN.ReadOnly = false;
            numericUpDownCena.ReadOnly = false;
            numericUpDownCena.Enabled = true;
            dateTimePickerDatum.Enabled = true;
            textBoxZakázka.ReadOnly = false;
            textBoxKonto.ReadOnly = false;

        }

        public messager getMesseger()
        {
            messager prepravka = new messager(poradi, textBoxJmeno.Text, textBoxPrijmeni.Text, textBoxOsCislo.Text, textBoxStredisko.Text,
                                  textBoxProvoz.Text, textBoxNazev.Text, textBoxJK.Text, Convert.ToInt64(numericUpDownPocetKS.Value),
                                  textBoxRozmer.Text, textBoxCSN.Text, numericUpDownCena.Value, dateTimePickerDatum.Value,
                                  textBoxZakázka.Text, textBoxKonto.Text);
                                  //Convert.ToInt64(numericUpDownMinStav.Value), Convert.ToInt64(numericUpDownUcetStav.Value), textBoxPoznamka.Text);
            return prepravka;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {

            if (textBoxJK.Text.Trim() == "")
            {
                MessageBox.Show("Položka Označení JK není vyplněna.");
            }
            else
            {

                if (state == vKartaState.edit)
                {
                    if (myDB.tableVracenoItemExist(textBoxJK.Text.Trim()))
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

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
