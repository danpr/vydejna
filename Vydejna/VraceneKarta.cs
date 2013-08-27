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

        private vKartaState state;
        private vDatabase myDB;


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
            textBoxCena.Text = Convert.ToString(DBRow["cena"]);
            dateTimePickerDatum.Value = Convert.ToDateTime(DBRow["datum"]);
            textBoxZakázka.Text = Convert.ToString(DBRow["vyrobek"]);
            textBoxKonto.Text = Convert.ToString(DBRow["konto"]);

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
            textBoxCena.ReadOnly = false;

            dateTimePickerDatum.Enabled = true;
            textBoxZakázka.ReadOnly = false;
            textBoxKonto.ReadOnly = false;

        }



    }
}
