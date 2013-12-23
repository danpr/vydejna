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
            public Int32 fyzStav;
            public string vyrobce;
            public decimal cenaKs; //cena
            public decimal ucetCenaKs;
            public decimal ucetCena;
            public string ucet;
            public Int32 minStav;
            public Int32 ucetStav;
            public string poznamka;
            public Int32 poradi;

            public messager(Int32 poradi, string nazev, string jk, string csn, string din, string rozmer, Int32 fyzStav, string vyrobce, decimal cenaKs,
                            decimal ucetCenaKs, decimal ucetCena, string ucet, Int32 minStav, Int32 ucetStav, string poznamka)
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
        private vDatabase myDB;
        private sKartaState state;
        private tableItemExistDelgStr testExistItem;
        private string oldJK;


        public SkladovaKarta(Hashtable DBRow, vDatabase myDataBase, tableItemExistDelgStr testExistItem, sKartaState state = sKartaState.show)
        {
            InitializeComponent();
            this.state = state;
            this.testExistItem = testExistItem;
            if (state == sKartaState.edit) setEditState();
            myDB = myDataBase;
            dataGridViewZmeny.MultiSelect = false;
            dataGridViewZmeny.ReadOnly = true;
            dataGridViewZmeny.RowHeadersVisible = false;
            dataGridViewZmeny.AllowUserToAddRows = false;
            dataGridViewZmeny.AllowUserToDeleteRows = false;
            dataGridViewZmeny.AllowUserToResizeRows = false;
            dataGridViewZmeny.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            listBoxNazev.Hide();
            
            if (state == sKartaState.show)
            {
                buttonOK.Visible = false;
                buttonOK.Enabled = false;
            }
            else
            {
                buttonOK.Visible = true;
                buttonOK.Enabled = true;
                setEditState();
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
            oldJK = textBoxJK.Text;
            numericUpDownUcetStav.Value = Convert.ToInt32(DBRow["ucetstav"]);
            textBoxUcet.Text = Convert.ToString(DBRow["analucet"]);
            textBoxCSN.Text = Convert.ToString(DBRow["normacsn"]);
            textBoxDIN.Text = Convert.ToString(DBRow["normadin"]);
            textBoxVyrobce.Text = Convert.ToString(DBRow["vyrobce"]);
            textBoxRozmer.Text = Convert.ToString(DBRow["rozmer"]);
            numericUpDownFyzStav.Value  = Convert.ToDecimal(DBRow["fyzstav"]);
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
                    dataGridViewZmeny.Columns[1].HeaderText = "Operace";
                    dataGridViewZmeny.Columns[2].HeaderText = "Poznamka";
                    dataGridViewZmeny.Columns[3].HeaderText = "Příjem";
                    dataGridViewZmeny.Columns[4].HeaderText = "Výdej";
                    dataGridViewZmeny.Columns[5].HeaderText = "Stav";
                    dataGridViewZmeny.Columns[6].HeaderText = "Zapůjčeno na kartu";

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
            numericUpDownUcetCena.ReadOnly = false;
            numericUpDownUcetCena.Enabled = true;
            numericUpDownUcetStav.ReadOnly = false;
            numericUpDownUcetStav.Enabled = true;
            numericUpDownFyzStav.ReadOnly = false;
            numericUpDownFyzStav.Enabled = true;
        //    textBoxJK.ReadOnly = true;

            
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
            numericUpDownUcetCena.ReadOnly = true;
            numericUpDownUcetCena.Enabled = false;
            numericUpDownMinStav.ReadOnly = false;
            numericUpDownMinStav.Enabled = true;
            numericUpDownUcetStav.ReadOnly = true;
            numericUpDownUcetStav.Enabled = false;
            numericUpDownFyzStav.ReadOnly = true;
            numericUpDownFyzStav.Enabled = false;

            listBoxNazev.Enabled = true;
            listBoxNazev.Show();

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
                        if (MessageBox.Show("Položka s tímto číslem položky již existuje. Opravdu chcete pokračovat dál ?","", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            if (MessageBox.Show("Opravdu chcete opakovaně použít toto číslo položky ?","", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            {
                                buttonOK.DialogResult = DialogResult.OK;
                                this.DialogResult = DialogResult.OK;
                                Close();
                            }                            

                        }

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
                        if ((testExistItem(textBoxJK.Text.Trim())) && ((oldJK.Trim() != textBoxJK.Text.Trim())))
                        {
                            if (MessageBox.Show("Položka s tímto ČÍSLEM POLOŽKY již existuje. Opravdu chcete pokračovat dál ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            {
                                if (MessageBox.Show("Položka s tímto ČÍSLEM POLOŽKY již existuje. Jste si opravdu chcete pokračovat dál ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                                {
                                    buttonOK.DialogResult = DialogResult.OK;
                                    this.DialogResult = DialogResult.OK;
                                    Close();
                                }
                            }
                        }
                        else
                        {
                            buttonOK.DialogResult = DialogResult.OK;
                            this.DialogResult = DialogResult.OK;
                            Close();
                        }
                    }
                }
            }


        }

        public messager getMesseger()
        {
            messager prepravka = new messager(poradi, textBoxNazev.Text, textBoxJK.Text, textBoxCSN.Text, textBoxDIN.Text,
                                  textBoxRozmer.Text, Convert.ToInt32(numericUpDownFyzStav.Value), textBoxVyrobce.Text, numericUpDownCenaKs.Value,
                                  numericUpDownUcetCenaKs.Value, numericUpDownUcetCena.Value, textBoxUcet.Text,
                                  Convert.ToInt32( numericUpDownMinStav.Value), Convert.ToInt32(numericUpDownUcetStav.Value), textBoxPoznamka.Text);
            return prepravka;
        }

        private void numericUpDownUcetCenaKs_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDownSK_Enter(object sender, EventArgs e)
        {
            (sender as NumericUpDown).Select(0, (sender as NumericUpDown).Text.Length);
        }


        private void textBoxNazev_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNazev.Text.Length > 2)
            {
                // natahneme data
                DataTable dtNaradi = myDB.loadDataPartTableNaradiNazev(textBoxNazev.Text);
                listBoxNazev.DataSource = dtNaradi;
                listBoxNazev.DisplayMember = "nazev";
            }
            else
            {
                DataTable dtNaradi = myDB.loadDataPartTableNaradiNazev("xxxxxxxxxxxxxxxx");
                listBoxNazev.DataSource = dtNaradi;
                listBoxNazev.DisplayMember = "nazev";
            }

        }



        private void listBoxNazev_Click(object sender, EventArgs e)
        {
            textBoxNazev.Text = listBoxNazev.Text;
        }


        private void ContextMenu_opravaUdaju(object sender, EventArgs e)
        {
            //
            ZmenyOprava opravaZmen = new ZmenyOprava();
            opravaZmen.ShowDialog();

        }


    }
}
