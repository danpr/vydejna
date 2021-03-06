﻿using System;
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
    public partial class Poskozenka : Form
    {


        public class messager
        {
            public string jk;
            public Int32 pocetKs;
            public DateTime datum;
            public string poznamka;
            public Int32 poradi;
            public string osCislo;
            public string jmeno;
            public string prijmeni;
            public string stredisko;
            public string provoz;
            public string rozmer;
            public string konto;
            public double cena;
            public double celkCena;
            public string cisZak;

            public messager(Int32 poradi, string jk, Int32 pocetKs, DateTime datum, string poznamka, string osCislo, string jmeno, string prijmeni,
                            string stredisko, string provoz, string rozmer, string konto, double cena, double celkCena, string cisZak)
            {
                this.jk = jk;
                this.pocetKs = pocetKs;
                this.datum = datum;
                this.poznamka = poznamka;
                this.poradi = poradi;
                this.osCislo = osCislo;
                this.jmeno = jmeno;
                this.prijmeni = prijmeni;
                this.stredisko = stredisko;
                this.provoz = provoz;
                this.rozmer = rozmer;
                this.konto = konto;
                this.cena = cena;
                this.celkCena = celkCena;
                this.cisZak = cisZak;
            }
        }

        private vDatabase myDataBase;
        private Int32 parentPoradi;
        private double cena;
        private double celkCena;
        private Font boldFont;
        private Int32 maximumMnozstvi = 0;


        public Poskozenka(Hashtable DBRow, vDatabase myDataBase, Font myFont, Boolean pujceneNaradi = false)
        {
            InitializeComponent();

            CancelButton = buttonCancel;
            AcceptButton = buttonOK;

            labelJmeno.Text = "";
            labelPrijmeni.Text = "";
            labelStredisko.Text = "";
            labelProvoz.Text = "";

            this.myDataBase = myDataBase;
            parentPoradi = Convert.ToInt32(DBRow["poradi"]);
            cena = Convert.ToDouble(DBRow["cena"]);
            celkCena = Convert.ToDouble(DBRow["celkcena"]);

            if (DBRow.ContainsKey("nazev")) labelNazev.Text = Convert.ToString(DBRow["nazev"]);
            if (DBRow.ContainsKey("jk")) labelJK.Text = Convert.ToString(DBRow["jk"]);
            if (DBRow.ContainsKey("rozmer")) labelRozmer.Text = Convert.ToString(DBRow["rozmer"]);
            labelStav.Text = Convert.ToString(DBRow["fyzstav"])+ " / "+Convert.ToString(DBRow["ucetstav"]);
            labelCena.Text = Convert.ToString(DBRow["cena"]);
            labelCelkCena.Text = Convert.ToString(DBRow["celkcena"]);

//            numericUpDownMnozstvi.Maximum = Convert.ToInt32(DBRow["fyzstav"]);
            maximumMnozstvi = Convert.ToInt32(DBRow["fyzstav"]); ;
            textBoxOsCislo.Focus();

            comboBoxPoznamka.Items.Add("Poškozeno");

            if (pujceneNaradi)
            {
                if (DBRow.ContainsKey("oscislo"))
                {
                  Int32 celkPujc = Convert.ToInt32(DBRow["ucetstav"]) - Convert.ToInt32(DBRow["fyzstav"]);
                  textBoxOsCislo.Text = Convert.ToString(DBRow["oscislo"]);
                  textBoxOsCislo.Enabled = false;
                  buttonChoosePerson.Enabled = false;
                  label13.Text = "Zapůjčeno nyní / celkem :";
                  labelStav.Text = Convert.ToString(DBRow["stavks"]) + " / " + Convert.ToString(celkPujc);
                  comboBoxPoznamka.Items.Add("Vráceno a poškozeno");
                  comboBoxPoznamka.SelectedIndex = 1;
                  if (Convert.ToInt32(DBRow["stavks"]) < celkPujc)
                  {
                      //numericUpDownMnozstvi.Maximum = Convert.ToInt32(DBRow["stavks"]);
                      maximumMnozstvi = Convert.ToInt32(DBRow["stavks"]);
                  }
                  else
                  {
//                      numericUpDownMnozstvi.Maximum = celkPujc;
                      maximumMnozstvi = celkPujc;
                  }
                  if (numericUpDownMnozstvi.Value > maximumMnozstvi)
                  {
                      numericUpDownMnozstvi.Value = maximumMnozstvi;
                  }
                  choosePerson();
                  textBoxCisZak.Focus();
                }
            }
            else comboBoxPoznamka.SelectedIndex = 0;

            if (DBRow.ContainsKey("poznamka"))
            {
                string poznamka = Convert.ToString(DBRow["poznamka"]);
                if (poznamka != "")
                {
                    comboBoxPoznamka.Items.Add(poznamka);
                }
            }

            boldFont = new Font(myFont, FontStyle.Bold);
            this.Font = myFont;
            labelJmeno.Font = boldFont;
            labelPrijmeni.Font = boldFont;
            labelStredisko.Font = boldFont;
            labelProvoz.Font = boldFont;
            labelCena.Font = boldFont;
            labelCelkCena.Font = boldFont;
            labelNazev.Font = boldFont;
            labelJK.Font = boldFont;
            labelRozmer.Font = boldFont;
            labelStav.Font = boldFont;

        }

        public messager getMesseger()
        {
            messager prepravka = new messager(parentPoradi, labelJK.Text, Convert.ToInt32(numericUpDownMnozstvi.Value), dateTimePickerDatum.Value, comboBoxPoznamka.Text, textBoxOsCislo.Text,
                                              labelJmeno.Text, labelPrijmeni.Text, labelStredisko.Text, labelProvoz.Text, labelRozmer.Text, textBoxKonto.Text, cena, celkCena, textBoxCisZak.Text);
            return prepravka;

        }

        private void textBoxCisZak_Leave(object sender, EventArgs e)
        {

        }

        private void textBoxOsCislo_Leave(object sender, EventArgs e)
        {
            // opostime zadani do policka pro osobni cislo
           // kdyz nebyla nalezena zadna data vrati null pokud bula Hashtable jako null zadana
            Hashtable osobyDBRow =  myDataBase.getOsobyLine(textBoxOsCislo.Text, null);
           if (osobyDBRow != null)
           {
               labelJmeno.Text = Convert.ToString(osobyDBRow["jmeno"]);
               labelPrijmeni.Text = Convert.ToString(osobyDBRow["prijmeni"]);
               labelStredisko.Text = Convert.ToString(osobyDBRow["stredisko"]);
               labelProvoz.Text = Convert.ToString(osobyDBRow["pracoviste"]);
               if (numericUpDownMnozstvi.Value > 0) buttonOK.Enabled = true;
           }
           else
           {
               buttonOK.Enabled = false;
               if ((!(buttonCancel.Focused)) && (!(buttonChoosePerson.Focused)))
               {
                   labelJmeno.Text = "";
                   labelPrijmeni.Text = "";
                   labelStredisko.Text = "";
                   labelProvoz.Text = "";
                   MessageBox.Show("Lituji. Osobni číslo neexistuje.");
                   textBoxOsCislo.Focus();
               }
           }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            testMaximalnihoMnozstvi();
            testKompletnosti();
        }


        private void testKompletnosti()
        {
            if ((textBoxOsCislo.Text != "") && (numericUpDownMnozstvi.Value > 0))
            {
                buttonOK.Enabled = true;
            }

            else
            {
                buttonOK.Enabled = false;
            }
        }

        private void testMaximalnihoMnozstvi()
        {
            if (numericUpDownMnozstvi.Value > maximumMnozstvi)
            {
                numericUpDownMnozstvi.Value = maximumMnozstvi;
                numericUpDownMnozstvi.Focus();
                System.Media.SystemSounds.Beep.Play();
                MessageBox.Show("Poškozené množství je příliš veliké. Maximálně je možno odepsat "+Convert.ToString(maximumMnozstvi)+" ks(ů).");
            }
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
                        textBoxOsCislo.Text = Convert.ToString(osobaRow["oscislo"]);
                        textBoxCisZak.Focus();
                    }
                    if (osobaRow.ContainsKey("jmeno")) labelJmeno.Text = Convert.ToString(osobaRow["jmeno"]);
                    if (osobaRow.ContainsKey("prijmeni")) labelPrijmeni.Text = Convert.ToString(osobaRow["prijmeni"]);
                    if (osobaRow.ContainsKey("stredisko")) labelStredisko.Text = Convert.ToString(osobaRow["stredisko"]);
                    if (osobaRow.ContainsKey("pracoviste")) labelProvoz.Text = Convert.ToString(osobaRow["pracoviste"]);
                }
                testKompletnosti();
            }
        }


        private void choosePerson()
        {
            Hashtable osobaRow = null;
            osobaRow = myDataBase.getOsobyLine(textBoxOsCislo.Text, null);
            if (osobaRow != null)
            {
                if (osobaRow.ContainsKey("jmeno")) labelJmeno.Text = Convert.ToString(osobaRow["jmeno"]);
                if (osobaRow.ContainsKey("prijmeni")) labelPrijmeni.Text = Convert.ToString(osobaRow["prijmeni"]);
                if (osobaRow.ContainsKey("stredisko")) labelStredisko.Text = Convert.ToString(osobaRow["stredisko"]);
                if (osobaRow.ContainsKey("pracoviste")) labelProvoz.Text = Convert.ToString(osobaRow["pracoviste"]);
                testKompletnosti();

            }
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
                MessageBox.Show("Je nutno zadat množství poškozeného nářadí.");
            }

        }

        private void numericUpDownMnozstvi_Leave(object sender, EventArgs e)
        {
//            testMaximalnihoMnozstvi();
        }
   
   

    }
}
