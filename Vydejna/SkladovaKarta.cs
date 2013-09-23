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
   public enum sKartaState { show , add, edit };

    public partial class SkladovaKarta : Form
    {

        public class messager
        {
            public string nazev;
            public string jk;
            public string csn;
            public string din;
            public string rozmer;
            public Int64 fyzStav;
            public string vyrobce;
            public decimal cenaKs; //cena
            public decimal ucetCenaKs;
            public decimal ucetCena;
            public string ucet;
            public Int64 minStav;
            public Int64 ucetStav;
            public string poznamka;
            public Int32 poradi;

            public messager(Int32 poradi, string nazev, string jk, string csn, string din, string rozmer, Int64 fyzStav, string vyrobce, decimal cenaKs,
                            decimal ucetCenaKs, decimal ucetCena, string ucet, Int64 minStav, Int64 ucetStav, string poznamka)
            {
                this.nazev = nazev;
                this.jk = jk;
                this.csn = csn;
                this.din = din;
                this.rozmer = rozmer;
                this.fyzStav = fyzStav;
                this.vyrobce = vyrobce;
                this.cenaKs = cenaKs;
                this.ucetCenaKs = ucetCenaKs;
                this.ucetCena = ucetCena;
                this.ucet = ucet;
                this.minStav = minStav;
                this.ucetStav = ucetStav;
                this.poznamka = poznamka;
                this.poradi = poradi;
            }
        }



        private decimal cenaKs;
        private Int32 poradi;
        private Int32 fyzStav;
        private vDatabase myDB;
        private sKartaState state;
        private tableItemExistDelgStr testExistItem;


        public SkladovaKarta(Hashtable DBRow, vDatabase myDataBase, tableItemExistDelgStr testExistItem, sKartaState state = sKartaState.show)
        {
            InitializeComponent();
            this.state = state;
            this.testExistItem = testExistItem;
            if (state == sKartaState.edit) setEditState();
            myDB = myDataBase;
            dataGridViewZmeny.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (state == sKartaState.show)
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
            loadZmenyItems();
        }


        public SkladovaKarta(vDatabase myDataBase, tableItemExistDelgStr testExistItem)
        {
            InitializeComponent();
            myDB = myDataBase;
            this.state = sKartaState.add;
            this.testExistItem = testExistItem;
            dataGridViewZmeny.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            setAddState();
        }

        public void setWinName(string winName)
        {
            this.Text = winName;
        }


        public void setData(Hashtable DBRow) 
        {
            textBoxNazev.Text = Convert.ToString(DBRow["nazev"]);
            textBoxJK.Text = Convert.ToString(DBRow["jk"]);
            numericUpDownUcetStav.Value = Convert.ToInt32(DBRow["ucetstav"]);
            textBoxUcet.Text = Convert.ToString(DBRow["analucet"]);
            textBoxCSN.Text = Convert.ToString(DBRow["normacsn"]);
            textBoxDIN.Text = Convert.ToString(DBRow["normadin"]);
            textBoxVyrobce.Text = Convert.ToString(DBRow["vyrobce"]);
            textBoxRozmer.Text = Convert.ToString(DBRow["rozmer"]);
            fyzStav = Convert.ToInt32(DBRow["fyzstav"]);
            cenaKs = Convert.ToDecimal(DBRow["cena"]);
            numericUpDownCenaKs.Value = Convert.ToDecimal(DBRow["cena"]);
            numericUpDownUcetCenaKs.Value = Convert.ToDecimal(DBRow["ucetkscen"]); 
//            celkCena = Convert.ToDecimal(DBRow["celkcena"]);
            numericUpDownUcetCena.Value = Convert.ToDecimal(DBRow["celkcena"]); //celkova cena
            numericUpDownMinStav.Value = Convert.ToInt32(DBRow["minimum"]);
            textBoxPoznamka.Text = Convert.ToString(DBRow["poznamka"]);
            poradi = Convert.ToInt32(DBRow["poradi"]);
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void loadZmenyItems()
        {
            Application.DoEvents();
            dataGridViewZmeny.Columns.Clear();
            dataGridViewZmeny.DataSource = null;
            Application.DoEvents();

 //           myDB.openDB();

            if (myDB.DBIsOpened())
            {
                try
                {
                    dataGridViewZmeny.DataSource = myDB.loadDataTableZmeny(poradi);  // zde zavolame tabulku                   
                    dataGridViewZmeny.RowHeadersVisible = false;

                    dataGridViewZmeny.Columns[0].HeaderText = "Datum";
                    dataGridViewZmeny.Columns[1].HeaderText = "Poznamka";
                    dataGridViewZmeny.Columns[2].HeaderText = "Příjem";
                    dataGridViewZmeny.Columns[3].HeaderText = "Výdej";
                    dataGridViewZmeny.Columns[4].HeaderText = "Stav";

                    dataGridViewZmeny.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                }
                catch (Exception)
                {
                    MessageBox.Show("Tabulka změn stavu nářadí nelze otevřít.");
                }
                finally
                {
//                    myDB.closeDB();
                }
            }
//            myDB.myDBConn.Dispose();
        }


        private void setEditState()
        {
            setAddState();
            textBoxJK.ReadOnly = true;
        }
        
        private void setAddState()
        {
            textBoxNazev.ReadOnly = false;
            textBoxPoznamka.ReadOnly = false;
            textBoxJK.ReadOnly = false;
            textBoxCSN.ReadOnly = false;
            textBoxDIN.ReadOnly = false;
            textBoxRozmer.ReadOnly = false;
            textBoxVyrobce.ReadOnly = false;
            textBoxUcet.ReadOnly = false;
            
            numericUpDownCenaKs.ReadOnly = false;
            numericUpDownCenaKs.Enabled = true;
            numericUpDownUcetCenaKs.ReadOnly = false;
            numericUpDownUcetCenaKs.Enabled = true;
            numericUpDownUcetCena.ReadOnly = false;
            numericUpDownUcetCena.Enabled = true;
            numericUpDownMinStav.ReadOnly = false;
            numericUpDownMinStav.Enabled = true;
            numericUpDownUcetStav.ReadOnly = false;
            numericUpDownUcetStav.Enabled = true;

        }

        private void SkladovaKarta_Activated(object sender, EventArgs e)
        {
            textBoxNazev.Focus();

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            
            if (textBoxJK.Text.Trim() == "")
            {
                MessageBox.Show("Položka Označení JK není vyplněna.");
            }
            else
            {
                if (state == sKartaState.add)
                {
                    if (testExistItem(textBoxJK.Text.Trim()))
                        {
                        MessageBox.Show("Položka již existuje.");
                    }
                    else
                    {
                        buttonOK.DialogResult = DialogResult.OK;
                        this.DialogResult = DialogResult.OK;
                        Close();
                    }
                }
                else
                {
                    if (state == sKartaState.edit)
                    {
//                        if (myDB.tableNaradiItemExist(textBoxJK.Text.Trim()))
                            if (testExistItem(textBoxJK.Text.Trim()))
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


        }

        public messager getMesseger()
        {
            messager prepravka = new messager(poradi, textBoxNazev.Text, textBoxJK.Text, textBoxCSN.Text, textBoxDIN.Text,
                                  textBoxRozmer.Text, fyzStav, textBoxVyrobce.Text, numericUpDownCenaKs.Value,
                                  numericUpDownUcetCenaKs.Value, numericUpDownUcetCena.Value, textBoxUcet.Text,
                                  Convert.ToInt64( numericUpDownMinStav.Value), Convert.ToInt64(numericUpDownUcetStav.Value), textBoxPoznamka.Text);
            return prepravka;
        }

        private void numericUpDownUcetCenaKs_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDownSK_Enter(object sender, EventArgs e)
        {
            (sender as NumericUpDown).Select(0, (sender as NumericUpDown).Text.Length);
        }


    }
}
