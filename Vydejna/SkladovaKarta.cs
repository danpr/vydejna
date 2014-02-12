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


        public SkladovaKarta(vDatabase myDataBase, Hashtable DBRow, Int32 poradi, tableItemExistDelgStr testExistItem, Font myFont, sKartaState state = sKartaState.show)
        {
            InitializeComponent();
            this.state = state;
            this.testExistItem = testExistItem;
            this.poradi = poradi;
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
                setShowState();
            }
            else
            {
                // edit + add
                if (state == sKartaState.add)
                {
                    setAddState();
                }
                else
                {
                    setEditState();
                }
            }

            setData(DBRow);
            loadZmenyItems();
            this.CancelButton = this.buttonCancel;
            this.Font = myFont;
        }


        public SkladovaKarta(vDatabase myDataBase, tableItemExistDelgStr testExistItem, Font myFont)
        {
            InitializeComponent();

            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            myDB = myDataBase;
            this.state = sKartaState.add;
            this.testExistItem = testExistItem;
            dataGridViewZmeny.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            setAddState();

            this.CancelButton = this.buttonCancel;
            this.Font = myFont;
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
            labelUcetStav.Text = Convert.ToString(DBRow["ucetstav"]);
            textBoxUcet.Text = Convert.ToString(DBRow["analucet"]);
            textBoxCSN.Text = Convert.ToString(DBRow["normacsn"]);
            textBoxDIN.Text = Convert.ToString(DBRow["normadin"]);
            textBoxVyrobce.Text = Convert.ToString(DBRow["vyrobce"]);
            textBoxRozmer.Text = Convert.ToString(DBRow["rozmer"]);
            numericUpDownFyzStav.Value  = Convert.ToDecimal(DBRow["fyzstav"]);
            labelFyzStav.Text = Convert.ToString(DBRow["fyzstav"]);
            cenaKs = Convert.ToDecimal(DBRow["cena"]);
            numericUpDownCenaKs.Value = Convert.ToDecimal(DBRow["cena"]);
            labelCenaKs.Text = Convert.ToString(DBRow["cena"]);
            numericUpDownUcetCenaKs.Value = Convert.ToDecimal(DBRow["ucetkscen"]);
            labelUcetCenaKs.Text = Convert.ToString(DBRow["ucetkscen"]);
            numericUpDownUcetCena.Value = Convert.ToDecimal(DBRow["celkcena"]); //celkova cena
            labelUcetCena.Text = Convert.ToString(DBRow["celkcena"]);
            numericUpDownMinStav.Value = Convert.ToInt32(DBRow["minimum"]);
            labelMinStav.Text = Convert.ToString(DBRow["minimum"]);
            textBoxPoznamka.Text = Convert.ToString(DBRow["poznamka"]);
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
            if (myDB.DBIsOpened())
            {
                try
                {
                    dataGridViewZmeny.DataSource = myDB.loadDataTableZmeny(poradi);  // zde zavolame tabulku                   
                    dataGridViewZmeny.RowHeadersVisible = false;

                    dataGridViewZmeny.Columns["datum"].HeaderText = "Datum";
                    dataGridViewZmeny.Columns["stav"].HeaderText = "Operace";
                    dataGridViewZmeny.Columns["poznamka"].HeaderText = "Poznamka";
                    dataGridViewZmeny.Columns["prijem"].HeaderText = "Příjem";
                    dataGridViewZmeny.Columns["vydej"].HeaderText = "Výdej";
                    dataGridViewZmeny.Columns["zustatek"].HeaderText = "Stav";
                    dataGridViewZmeny.Columns["zapkarta"].HeaderText = "Zapůjčeno na kartu";

                    dataGridViewZmeny.Columns["poradi"].Visible = false;  

                    dataGridViewZmeny.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    for (int i = 0; i < dataGridViewZmeny.Columns.Count; i++)
                    {
                        dataGridViewZmeny.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
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
        }


        private void setShowState()
        {
            buttonOK.Visible = false;
            buttonOK.Enabled = false;
            buttonCopy.Visible = false;
            buttonCopy.Enabled = false;

            numericUpDownCenaKs.Enabled = false;
            numericUpDownCenaKs.Hide();
            labelCenaKs.Show();
            numericUpDownUcetCenaKs.Enabled = false;
            numericUpDownUcetCenaKs.Hide();
            labelUcetCenaKs.Show();
            numericUpDownMinStav.Enabled = false;
            numericUpDownMinStav.Hide();
            labelMinStav.Show();
            numericUpDownUcetCena.Enabled = false;
            numericUpDownUcetCena.Hide();
            labelUcetCena.Show();
            numericUpDownUcetStav.Enabled = false;
            numericUpDownUcetStav.Hide();
            labelUcetStav.Show();
            numericUpDownFyzStav.Enabled = false;
            numericUpDownFyzStav.Hide();
            labelFyzStav.Show();

        }

        private void setEditState()
        {
            setAddEditState();
            numericUpDownUcetCena.ReadOnly = false;
            numericUpDownUcetCena.Enabled = true;
            numericUpDownUcetStav.ReadOnly = false;
            numericUpDownUcetStav.Enabled = true;
            numericUpDownFyzStav.ReadOnly = false;
            numericUpDownFyzStav.Enabled = true;

            buttonCopy.Visible = false;
            buttonCopy.Enabled = false;
        }


        private void setAddState()
        {
            setAddEditState();

            numericUpDownUcetCena.ReadOnly = true;
            numericUpDownUcetCena.Enabled = false;
            numericUpDownUcetStav.ReadOnly = true;
            numericUpDownUcetStav.Enabled = false;
            numericUpDownFyzStav.ReadOnly = true;
            numericUpDownFyzStav.Enabled = false;

            buttonCopy.Visible = true;
            buttonCopy.Enabled = true;
        }


        private void setAddEditState()
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
            numericUpDownMinStav.ReadOnly = false;
            numericUpDownMinStav.Enabled = true;
            listBoxNazev.Enabled = true;
            listBoxNazev.Show();

            numericUpDownCenaKs.Enabled = true;
            numericUpDownCenaKs.Show();
            labelCenaKs.Hide();
            numericUpDownUcetCenaKs.Enabled = true;
            numericUpDownUcetCenaKs.Show();
            labelUcetCenaKs.Hide();
            numericUpDownMinStav.Enabled = true;
            numericUpDownMinStav.Show();
            labelMinStav.Hide();
            numericUpDownUcetCena.Enabled = true;
            numericUpDownUcetCena.Show();
            labelUcetCena.Hide();
            numericUpDownUcetStav.Enabled = true;
            numericUpDownUcetStav.Show();
            labelUcetStav.Hide();
            numericUpDownFyzStav.Enabled = true;
            numericUpDownFyzStav.Show();
            labelFyzStav.Hide();

            buttonOK.Visible = true;
            buttonOK.Enabled = true;
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
            if (dataGridViewZmeny.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewZmeny.SelectedRows[0];
                Int32 zmenPoradi = Convert.ToInt32(selectedRow.Cells["poradi"].Value);
//                Point point1 = dataGridViewZmeny.CurrentCellAddress;

                Int32 endRowIndex = selectedRow.Index +1; // ukazuje za vybranou radku
                Int32 rowsHeight = 0;
                for (Int32 i = dataGridViewZmeny.FirstDisplayedCell.RowIndex; i < (endRowIndex); i++)
                {
                    rowsHeight += dataGridViewZmeny.Rows[i].Height;
                }
                int titulekHeight = this.Height - this.ClientSize.Height  - (this.Width - this.ClientSize.Width)/2;

                int x = this.Location.X + dataGridViewZmeny.Location.X;
                int y = this.Location.Y + dataGridViewZmeny.Location.Y + dataGridViewZmeny.ColumnHeadersHeight + rowsHeight + titulekHeight;


                ZmenyOprava opravaZmen = new ZmenyOprava(myDB, poradi, zmenPoradi, this.Font);

                opravaZmen.StartPosition = FormStartPosition.Manual;
                opravaZmen.SetDesktopLocation(x, y);

                if (opravaZmen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //opravit radku
                    if (myDB.editNewLineZmeny(poradi, zmenPoradi, opravaZmen.getPoznamka(), opravaZmen.getVevcislo()) == false)
                    {
                        MessageBox.Show("Záznam se nepodařilo opravit.");
                    }
                    //dataGridViewZmeny nema povoleno trideni nemusime tedy hledat spravny index
                    Int32 dataRowIndex = dataGridViewZmeny.SelectedRows[0].Index;
                    if (dataRowIndex > -1)
                    {
                        Hashtable DBZRow = myDB.getZmenyLine(poradi, zmenPoradi, null);
                        (dataGridViewZmeny.DataSource as DataTable).Rows[dataRowIndex].SetField("datum", Convert.ToDateTime(DBZRow["datum"]));
                        (dataGridViewZmeny.DataSource as DataTable).Rows[dataRowIndex].SetField("stav", Convert.ToString(DBZRow["stav"]));
                        (dataGridViewZmeny.DataSource as DataTable).Rows[dataRowIndex].SetField("poznamka", Convert.ToString(DBZRow["poznamka"]));
                        (dataGridViewZmeny.DataSource as DataTable).Rows[dataRowIndex].SetField("prijem", Convert.ToInt32(DBZRow["prijem"]));
                        (dataGridViewZmeny.DataSource as DataTable).Rows[dataRowIndex].SetField("vydej", Convert.ToInt32(DBZRow["vydej"]));
                        (dataGridViewZmeny.DataSource as DataTable).Rows[dataRowIndex].SetField("zustatek", Convert.ToInt32(DBZRow["zustatek"]));
                        (dataGridViewZmeny.DataSource as DataTable).Rows[dataRowIndex].SetField("zapkarta", Convert.ToString(DBZRow["zapkarta"]));
                    }

                }
            }
        }


        private void zapujcenoNaKartuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void contextMenu_ZapujcenoNaKartu(object sender, EventArgs e)
        {
            if (dataGridViewZmeny.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewZmeny.SelectedRows[0];
                string osCislo = Convert.ToString(selectedRow.Cells["zapkarta"].Value);
                if (osCislo.Trim() != "")
                {
                    if (myDB.tableOsobyItemExist(osCislo))
                    {

                        ZapujceneNaradiKarta zapujcKarta = new ZapujceneNaradiKarta(osCislo, myDB, this.Font);// (DBRow, myDataBase, uKartaState.edit);
                        zapujcKarta.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Pracovník " + osCislo + "neexistuje v seznamu pracovníků");
                    }
                }
            }

        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            // kopie dat
            // zobrazime seznam polozek naradi
            SeznamNaradiJednoduchy seznamNar = new SeznamNaradiJednoduchy(myDB, this.Font);
            if (seznamNar != null)
            {
                seznamNar.Visible = false;   // formular se automaticky presune do show musime tedy ho vypnout
                if (seznamNar != null)
                {
                    try  // protoze konstruktor "seznam naradi jednoduchy" -  pracuje dlouho s natahovanim polozek - uzivatel je muze prerusit, 
                         // a tim dojde k odstraneni objektu musime tedy testovat existenci objektu
                    {
                        if (seznamNar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            SeznamNaradiJednoduchy.messager myMesenger = seznamNar.getMesseger();
                            Hashtable DBRow = myDB.getNaradiLine(myMesenger.poradi, null);
                            if (DBRow != null)
                            {
                                if (DBRow.ContainsKey("nazev")) textBoxNazev.Text = Convert.ToString(DBRow["nazev"]);
                                else textBoxNazev.Text = "";
                                if (DBRow.ContainsKey("jk")) textBoxJK.Text = Convert.ToString(DBRow["jk"]);
                                else textBoxJK.Text = "";
                                if (DBRow.ContainsKey("normacsn")) textBoxCSN.Text = Convert.ToString(DBRow["normacsn"]);
                                else textBoxCSN.Text = "";
                                if (DBRow.ContainsKey("normadin")) textBoxDIN.Text = Convert.ToString(DBRow["normadin"]);
                                else textBoxDIN.Text = "";
                                if (DBRow.ContainsKey("rozmer")) textBoxRozmer.Text = Convert.ToString(DBRow["rozmer"]);
                                else textBoxRozmer.Text = "";
                                if (DBRow.ContainsKey("vyrobce")) textBoxVyrobce.Text = Convert.ToString(DBRow["vyrobce"]);
                                else textBoxVyrobce.Text = "";
                                if (DBRow.ContainsKey("analucet")) textBoxUcet.Text = Convert.ToString(DBRow["analucet"]);
                                else textBoxUcet.Text = "";


                                if (DBRow.ContainsKey("minimum")) numericUpDownMinStav.Value = Convert.ToInt32(DBRow["minimum"]);
                                else numericUpDownMinStav.Value = 0;
                                if (DBRow.ContainsKey("poznamka")) textBoxPoznamka.Text = Convert.ToString(DBRow["poznamka"]);
                                else textBoxPoznamka.Text = "";

                            }
                            else
                            {
                                MessageBox.Show("Lituji. Položka neexistuje. Změna z jiného místa?");
                            }
                        }
                    }
                    catch { };
                }
            }
        }
    }
}
