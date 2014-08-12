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

        private enum evenStateEnum { enable, disable };

        private evenStateEnum evenState = evenStateEnum.disable;

        private string formName = "MCARD";

        public class permissonsData
        {
            public readonly Boolean nazev;
            public readonly Boolean jk;
            public readonly Boolean cenaKs;
            public readonly Boolean ucetCenaKs;
            public readonly Boolean ucetCena;
            public readonly Boolean minimum;
            public readonly Boolean fyzStav;
            public readonly Boolean ucetStav;

            public permissonsData(Boolean nazev, Boolean jk, Boolean cenaKs, Boolean ucetCenaKs, Boolean ucetCena, Boolean minimum, Boolean fyzStav, Boolean ucetStav)
            {
                this.nazev = nazev;
                this.jk = jk;
                this.cenaKs = cenaKs;
                this.ucetCenaKs = ucetCenaKs;
                this.ucetCena = ucetCena;
                this.minimum = minimum;
                this.fyzStav = fyzStav;
                this.ucetStav = ucetStav;
            }
        }
            

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
        private permissonsData readOnlyPermission;
        private Font parentFont;


        public SkladovaKarta(vDatabase myDataBase, Hashtable DBRow, Int32 poradi, tableItemExistDelgStr testExistItem, Font myFont, sKartaState state = sKartaState.show, permissonsData readOnlyPermission = null)
        {
            InitializeComponent();
            this.state = state;
            this.testExistItem = testExistItem;
            this.poradi = poradi;
            this.readOnlyPermission = readOnlyPermission;
            parentFont = myFont;
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
            setFont(myFont);
            setGeometry();
            setColumnWidth();
//            evenState = evenStateEnum.enable;
        }


        public SkladovaKarta(vDatabase myDataBase, tableItemExistDelgStr testExistItem, Font myFont)
        {
            InitializeComponent();

            // jak menit meritko
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            myDB = myDataBase;
            this.state = sKartaState.add;
            this.testExistItem = testExistItem;

            this.CancelButton = this.buttonCancel;
            parentFont = myFont;
            setFont(myFont);

            dataGridViewZmeny.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            setAddState();

            setGeometry();
            setColumnWidth();
            evenState = evenStateEnum.enable;
        }

        private void setFont(Font myFont)
        {
            Font loadFont = ConfigReg.loadSettingFontX(formName);
            if (loadFont != null)
            {
                this.Font = loadFont;
            }
            else
            {
                this.Font = myFont;
            }
        }

        private void setAppFont()
        {
            this.Font = parentFont;
            ConfigReg.deleteSettingFontX(formName);
        }

        private void chooseFont()
        {
            FontDialog fontDialog1 = new FontDialog();
            fontDialog1.Font = this.Font;
            if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ConfigReg.saveSettingFontX(fontDialog1.Font, formName);
                this.Font = fontDialog1.Font;
            }
        }

        private void setGeometry()
        {
            Size size = ConfigReg.loadSettingWindowSize(formName);
            if (!(size.IsEmpty)) this.Size = size;
            Point location = ConfigReg.loadSettingWindowLocation(formName);

            Int32 x = location.X;
            Int32 y = location.Y;
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x > (Screen.PrimaryScreen.Bounds.Width - 20)) x = Screen.PrimaryScreen.Bounds.Width - 20;
            if (y > (Screen.PrimaryScreen.Bounds.Height - 20)) y = Screen.PrimaryScreen.Bounds.Height - 20;

            if (!(location.IsEmpty))
            {
                StartPosition = FormStartPosition.Manual;
                this.SetDesktopLocation(x, y);
            }
            else
            {
                StartPosition = FormStartPosition.WindowsDefaultLocation;
            }

        }

        public void setWinName(string winName)
        {
            this.Text = winName;
        }



        public virtual void setPermission()
        {
            if (readOnlyPermission != null)
            {
                if (!(readOnlyPermission.nazev))
                {
                    textBoxNazev.ReadOnly = true;
                    listBoxNazev.Enabled = false;
                }
                if (!(readOnlyPermission.jk)) textBoxJK.ReadOnly = true;
                if (!(readOnlyPermission.cenaKs))
                {   
                    numericUpDownCenaKs.ReadOnly = true;
                    numericUpDownCenaKs.Increment = 0;
                }
                if (!(readOnlyPermission.ucetCenaKs))
                {
                   numericUpDownUcetCenaKs.ReadOnly = true;
                   numericUpDownUcetCenaKs.Increment = 0;
                }
                if (!(readOnlyPermission.ucetCena))
                {
                   numericUpDownUcetCena.ReadOnly = true;
                   numericUpDownUcetCena.Increment = 0;

                }
                if (!(readOnlyPermission.fyzStav))
                {
                   numericUpDownFyzStav.ReadOnly = true;
                   numericUpDownFyzStav.Increment = 0;
                }
                if (!(readOnlyPermission.ucetStav))
                {
                   numericUpDownUcetStav.ReadOnly = true;
                   numericUpDownUcetStav.Increment = 0;
                }


            }
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
            numericUpDownUcetCena.Value = Convert.ToDecimal(DBRow["celkcena"]); //celkova cena
            numericUpDownMinStav.Value = Convert.ToInt32(DBRow["minimum"]);
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
                    dataGridViewZmeny.Columns["vevcislo"].Visible = false;  

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

            textBoxNazev.BackColor = System.Drawing.SystemColors.Window;
            textBoxJK.BackColor = System.Drawing.SystemColors.Window;
            textBoxCSN.BackColor = System.Drawing.SystemColors.Window;
            textBoxDIN.BackColor = System.Drawing.SystemColors.Window;
            textBoxRozmer.BackColor = System.Drawing.SystemColors.Window;
            textBoxVyrobce.BackColor = System.Drawing.SystemColors.Window;
            textBoxPoznamka.BackColor = System.Drawing.SystemColors.Window;
            textBoxUcet.BackColor = System.Drawing.SystemColors.Window;

            numericUpDownCenaKs.BackColor = System.Drawing.SystemColors.Window;
            numericUpDownUcetCenaKs.BackColor = System.Drawing.SystemColors.Window;
            numericUpDownMinStav.BackColor = System.Drawing.SystemColors.Window;
            numericUpDownUcetCena.BackColor = System.Drawing.SystemColors.Window;
            numericUpDownUcetStav.BackColor = System.Drawing.SystemColors.Window;
            numericUpDownFyzStav.BackColor = System.Drawing.SystemColors.Window;
            }

        private void setEditState()
        {
            setAddEditState();
            numericUpDownUcetCena.ReadOnly = false;
            numericUpDownUcetCena.Increment = 1;
            numericUpDownUcetStav.ReadOnly = false;
            numericUpDownUcetStav.Increment = 1;
            numericUpDownFyzStav.ReadOnly = false;
            numericUpDownFyzStav.Increment = 1;

            buttonCopy.Visible = false;
            buttonCopy.Enabled = false;

            setPermission();
        }


        private void setAddState()
        {
            setAddEditState();

            numericUpDownUcetCena.Enabled = false;
            numericUpDownUcetStav.Enabled = false;
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
            numericUpDownCenaKs.Increment = 1;
            numericUpDownUcetCenaKs.ReadOnly = false;
            numericUpDownUcetCenaKs.Increment = 1;
            numericUpDownMinStav.ReadOnly = false;
            numericUpDownMinStav.Increment = 1;
            listBoxNazev.Enabled = true;
            listBoxNazev.Show();

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
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userHasAccessRightsWM((Int32)permCode.NarOprO))
            {
                if (dataGridViewZmeny.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridViewZmeny.SelectedRows[0];
                    Int32 zmenPoradi = Convert.ToInt32(selectedRow.Cells["poradi"].Value);
                    //                Point point1 = dataGridViewZmeny.CurrentCellAddress;

                    Int32 endRowIndex = selectedRow.Index + 1; // ukazuje za vybranou radku
                    Int32 rowsHeight = 0;
                    for (Int32 i = dataGridViewZmeny.FirstDisplayedCell.RowIndex; i < (endRowIndex); i++)
                    {
                        rowsHeight += dataGridViewZmeny.Rows[i].Height;
                    }
                    int titulekHeight = this.Height - this.ClientSize.Height - (this.Width - this.ClientSize.Width) / 2;

                    int x = this.Location.X + dataGridViewZmeny.Location.X;
                    int y = this.Location.Y + dataGridViewZmeny.Location.Y + dataGridViewZmeny.ColumnHeadersHeight + rowsHeight + titulekHeight;



                    ZmenyOprava opravaZmen = new ZmenyOprava(myDB, poradi, zmenPoradi, parentFont);

                    opravaZmen.StartPosition = FormStartPosition.Manual;
                    if ((x + opravaZmen.Width) > Screen.PrimaryScreen.Bounds.Width)
                    {
                        x = Screen.PrimaryScreen.Bounds.Width - opravaZmen.Width;
                        if (x  < 1)
                        {
                            x = 1;
                        }
                    }
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

                        ZapujceneNaradiKarta zapujcKarta = new ZapujceneNaradiKarta(osCislo, myDB, parentFont);// (DBRow, myDataBase, uKartaState.edit);
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
            SeznamNaradiJednoduchy seznamNar = new SeznamNaradiJednoduchy(myDB, parentFont);
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

        private void contextMenuStripZmeny_Opening(object sender, CancelEventArgs e)
        {

        }

        public virtual void setColumnWidth()
        {
            Hashtable DBTableInfo = ConfigReg.loadSettingWindowTableColumnWidth(formName, "zmeny");
            if (DBTableInfo != null)
            {
                Int32 columnWidth = 0;
                for (Int32 i = 0; i < dataGridViewZmeny.Columns.Count; i++)
                {
                    string myColumnName = dataGridViewZmeny.Columns[i].Name;
                    if (DBTableInfo.ContainsKey(myColumnName))
                    {
                        columnWidth = Convert.ToInt32(DBTableInfo[myColumnName]);
                        try
                        {
                            dataGridViewZmeny.Columns[i].Width = columnWidth;
                        }
                        catch { }
                    }
                }
            }
        }



        private void SkladovaKarta_SizeChanged(object sender, EventArgs e)
        {
            if (evenState == evenStateEnum.enable)
            {
                if (!(this.Size.IsEmpty)) ConfigReg.saveSettingWindowLocationSize(formName, 0, 0, this.Size.Width, this.Size.Height);
            }

        }

        private void SkladovaKarta_LocationChanged(object sender, EventArgs e)
        {
            if (evenState == evenStateEnum.enable)
            {
                if (!(this.Location.IsEmpty)) ConfigReg.saveSettingWindowLocationSize(formName, this.Location.X, this.Location.Y, 0, 0);
            }
        }

        private void SkladovaKarta_Shown(object sender, EventArgs e)
        {
            evenState = evenStateEnum.enable;
        }

        private void dataGridViewZmeny_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (evenState == evenStateEnum.enable)
            {
                ConfigReg.saveSettingWindowTableColumnWidth(formName, "zmeny", e.Column.Name, e.Column.Width);
            }

        }

        private void dataGridViewZmeny_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (evenState == evenStateEnum.enable)
            {
                ConfigReg.saveSettingWindowTableColumnIndex(formName, "zmeny", e.Column.Name, e.Column.DisplayIndex);
            }

        }

        private void prepoctiCelkovouCenu(object sender, EventArgs e)
        {
            numericUpDownUcetCena.Value = numericUpDownUcetCenaKs.Value * numericUpDownUcetStav.Value;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            chooseFont();
        }

        private void p9smoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void písmoAplikaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setAppFont();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
