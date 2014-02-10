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
    public partial class ZapujceneNaradiInfo : Form
    {
        private Hashtable DBRow;

        public ZapujceneNaradiInfo(Hashtable DBRow, Font myFont)
        {
            InitializeComponent();
            this.DBRow = DBRow;
            setData();
            CancelButton = buttonCancel;
            this.Font = myFont;
        }


        private void setData()
        {
            if (DBRow.Contains("nazev")) labelNazev.Text = Convert.ToString(DBRow["nazev"]);
            else labelNazev.Text = "";
            if (DBRow.Contains("rozmer")) labelRozmer.Text = Convert.ToString(DBRow["rozmer"]);
            else labelRozmer.Text = "";
            if (DBRow.Contains("jk")) labelJK.Text = Convert.ToString(DBRow["jk"]);
            else labelJK.Text = "";
            if (DBRow.Contains("normacsn")) labelCSN.Text = Convert.ToString(DBRow["normacsn"]);
            else labelCSN.Text = "";
            if (DBRow.Contains("poznamka")) labelPoznamka.Text = Convert.ToString(DBRow["poznamka"]);
            else labelPoznamka.Text = "";
            if (DBRow.Contains("vevcislo")) labelVevcislo.Text = Convert.ToString(DBRow["vevcislo"]);
            else labelVevcislo.Text = "";
            if (DBRow.Contains("cena")) labelCena.Text = Convert.ToString(DBRow["cena"]);
            else labelCena.Text = "";
            if (DBRow.Contains("datum")) labelDatum.Text = Convert.ToString(DBRow["datum"]);
            else labelDatum.Text = "";
            if (DBRow.Contains("vydej")) labelKs.Text = Convert.ToString(DBRow["vydej"]);
            else labelKs.Text = "";
            if (DBRow.Contains("stavks")) labelStavks.Text = Convert.ToString(DBRow["stavks"]);
            else labelStavks.Text = "";

            
            //----------------------
            if (DBRow.Contains("pnazev")) labelPNazev.Text = Convert.ToString(DBRow["pnazev"]);
            else labelPNazev.Text = "";
            if (DBRow.Contains("pjk")) labelPJK.Text = Convert.ToString(DBRow["pjk"]);
            else labelPJK.Text = "";
            if (DBRow.Contains("pdatum")) labelPDatum.Text = Convert.ToString(DBRow["pdatum"]);
            else labelPDatum.Text = "";
            if (DBRow.Contains("pcena")) labelPCena.Text = Convert.ToString(DBRow["pcena"]);
            else labelPCena.Text = "";
            if (DBRow.Contains("pks")) labelPKs.Text = Convert.ToString(DBRow["pks"]);
            else labelPKs.Text = "";
            if (DBRow.Contains("pjmeno")) labelPJmeno.Text = Convert.ToString(DBRow["pjmeno"]);
            else labelPJmeno.Text = "";
            if (DBRow.Contains("pprijmeni")) labelPPrijmeni.Text = Convert.ToString(DBRow["pprijmeni"]);
            else labelPPrijmeni.Text = "";


        }
    }
}
