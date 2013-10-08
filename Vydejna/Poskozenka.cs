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
    public partial class Poskozenka : Form
    {


        public class messager
        {
            public string jk;
            public Int32 pocetKs;
            public DateTime datum;
            public string poznamka;
            public Int32 poradi;


            public messager(Int32 poradi, string jk, Int32 pocetKs, DateTime datum, string poznamka)
            {
                this.jk = jk;
                this.pocetKs = pocetKs;
                this.datum = datum;
                this.poznamka = poznamka;
                this.poradi = poradi;
            }
        }

        private vDatabase myDataBase;

        public Poskozenka(Hashtable DBRow, vDatabase myDataBase)
        {
            InitializeComponent();
            this.myDataBase = myDataBase;
            if (DBRow.ContainsKey("nazev")) labelNazev.Text = Convert.ToString(DBRow["nazev"]);
            if (DBRow.ContainsKey("jk")) labelJK.Text = Convert.ToString(DBRow["jk"]);
            if (DBRow.ContainsKey("rozmer")) labelRozmer.Text = Convert.ToString(DBRow["rozmer"]);
            if (DBRow.ContainsKey("poznamka")) labelPoznamka.Text = Convert.ToString(DBRow["poznamka"]);
            labelStav.Text = Convert.ToString(DBRow["fyzstav"])+ " / "+Convert.ToString(DBRow["ucetstav"]);
            labelCena.Text = Convert.ToString(DBRow["cena"]);
            labelCelkCena.Text = Convert.ToString(DBRow["celkcena"]);

            textBoxOsCislo.Focus();
        }

        public messager getMesseger()
        {
            return null;
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
           }
           else
           {
               if (!(buttonCancel.Focused))
               {
                   MessageBox.Show("Lituji. Osobni číslo neexistuje.");
                   textBoxOsCislo.Focus();
               }
           }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            testKompletnosti();
        }


        private void testKompletnosti()
        {
            if ((textBoxOsCislo.Text != "") && (numericUpDownMnozstvi.Value > 0))
            {
                buttonOK.Enabled = true;
            }

        }
   
   

    }
}
