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
    public partial class VraceneKarta : Form
    {
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
            textBoxCena.Text = Convert.ToString(DBRow["cena"]);
            dateTimePickerDatum.Value = Convert.ToDateTime(DBRow["datum"]);
            textBoxZakázka.Text = Convert.ToString(DBRow["vyrobek"]);
            textBoxKonto.Text = Convert.ToString(DBRow["konto"]);

        }

    }
}
