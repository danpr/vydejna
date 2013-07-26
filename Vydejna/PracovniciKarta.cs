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
    public partial class PracovniciKarta : Form
    {
        public PracovniciKarta(Hashtable DBRow)
        {
            InitializeComponent();
            setData(DBRow);
        }


        public void setData(Hashtable DBRow)
        {
            textBoxPrijmeni.Text = Convert.ToString(DBRow["prijmeni"]);
            textBoxJmeno.Text = Convert.ToString(DBRow["jmeno"]);
            textBoxUlice.Text = Convert.ToString(DBRow["ulice"]);
            textBoxMesto.Text = Convert.ToString(DBRow["mesto"]);
            textBoxPSC.Text = Convert.ToString(DBRow["psc"]);
            textBoxTelDomu.Text = Convert.ToString(DBRow["telhome"]);
            textBoxOsCislo.Text = Convert.ToString(DBRow["oscislo"]);
            textBoxStredisko.Text = Convert.ToString(DBRow["stredisko"]);

            textBoxCisZnamky.Text = Convert.ToString(DBRow["cisznamky"]);

            textBoxProvoz.Text = Convert.ToString(DBRow["odeleni"]);
            textBoxPracoviste.Text = Convert.ToString(DBRow["pracoviste"]);
            textBoxTelZamest.Text = Convert.ToString(DBRow["telzam"]);
            textBoxPoznamka.Text = Convert.ToString(DBRow["poznamka"]);
        
        }
    }
}
