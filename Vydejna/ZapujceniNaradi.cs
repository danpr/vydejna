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
    public partial class ZapujceniNaradi : Form
    {
        public ZapujceniNaradi(Hashtable DBRow, string nazev, string JK , Int32 fyzStav)
        {
            InitializeComponent();

            labelPrijmeni.Text = Convert.ToString(DBRow["prijmeni"]);
            labelJmeno.Text = Convert.ToString(DBRow["jmeno"]);
            labelOsCislo.Text = Convert.ToString(DBRow["oscislo"]);
            labelStredisko.Text = Convert.ToString(DBRow["stredisko"]);
            labelProvoz.Text = Convert.ToString(DBRow["odeleni"]);

            labelNazev.Text = nazev;
            labelJK.Text = JK;
            labelFyzStav.Text = Convert.ToString(fyzStav);

            numericUpDownKs.Maximum = fyzStav;
            textBoxPoznamka.Text = "Zapůjčeno";

        }
    }
}
