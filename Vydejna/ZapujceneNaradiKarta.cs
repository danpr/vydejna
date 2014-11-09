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
    public partial class ZapujceneNaradiKarta : Form
    {

        private string formName = "LCARD";

        private vDatabase myDB;
        Hashtable osobyDBRow;
        private string osCislo;
        private Font parentFont;

        private enum evenStateEnum { enable, disable };

        private evenStateEnum evenState = evenStateEnum.disable;


        public ZapujceneNaradiKarta(string osCislo, vDatabase myDataBase, Font myFont)
        {
            myDB = myDataBase;
            parentFont = myFont;

            InitializeComponent();

            osobyDBRow = myDB.getOsobyLine(osCislo, null);
            if (osobyDBRow != null)
            {
                setData(osobyDBRow);
                loadVypujceneItems();
            }
            dataGridViewZmeny.MultiSelect = false;
            dataGridViewZmeny.ReadOnly = true;
            dataGridViewZmeny.RowHeadersVisible = false;
            dataGridViewZmeny.AllowUserToAddRows = false;
            dataGridViewZmeny.AllowUserToResizeRows = false;

            dataGridViewZmeny.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            CancelButton = buttonCancel;
            setFont(myFont);

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

            setColumnWidth();

        //    evenState = evenStateEnum.enable;
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


        private void ZapujceneNaradiKarta_Load(object sender, EventArgs e)
        {

        }


        private void viewZmenyRemoveSelectedRow(Int32 poradi )
        {
            Int32 counter = dataGridViewZmeny.Rows.Count;
            if (counter > 0)
            {
                counter--; // ukazuje na posledni prvek

                Int32 dataRowIndex =  detail.findIndex(dataGridViewZmeny.DataSource as DataTable, "poradi", poradi);

                Int32 nextIndexAfterSelected = dataGridViewZmeny.SelectedRows[0].Index;
                (dataGridViewZmeny.DataSource as DataTable).Rows.RemoveAt(dataRowIndex);
                counter--; // ukazatel na posledni ... -1 neni zadna

                if (counter > -1) // neni zadna dalsi polozka
                {
                    if (nextIndexAfterSelected > counter) nextIndexAfterSelected = counter;
                    dataGridViewZmeny.FirstDisplayedScrollingRowIndex = dataGridViewZmeny.Rows[nextIndexAfterSelected].Index;
                    dataGridViewZmeny.Refresh();
                    dataGridViewZmeny.CurrentCell = dataGridViewZmeny.Rows[nextIndexAfterSelected].Cells[1];
                    dataGridViewZmeny.Rows[nextIndexAfterSelected].Selected = true;
                }
            }

        }


        public void setData(Hashtable DBRow)
        {
            osCislo = Convert.ToString(DBRow["oscislo"]).Trim();
            labelPrijmeni.Text = Convert.ToString(DBRow["prijmeni"]);
            labelJmeno.Text = Convert.ToString(DBRow["jmeno"]);
            labelOsCislo.Text = Convert.ToString(DBRow["oscislo"]);
            labelStredisko.Text = Convert.ToString(DBRow["stredisko"]);
            labelOddeleni.Text = Convert.ToString(DBRow["odeleni"]);
            labelPracoviste.Text = Convert.ToString(DBRow["pracoviste"]);
            labelCisZnamky.Text = Convert.ToString(DBRow["cisznamky"]);
        }

        private void loadVypujceneItems()
        {
            Application.DoEvents();
            dataGridViewZmeny.Columns.Clear();
            dataGridViewZmeny.DataSource = null;
            Application.DoEvents();

            if (myDB.DBIsOpened())
            {
                try
                {
                    dataGridViewZmeny.DataSource = myDB.loadDataTableVypujcenoNaOsobuNext(labelOsCislo.Text);  // zde zavolame tabulku                   
                    dataGridViewZmeny.RowHeadersVisible = false;

                    dataGridViewZmeny.Columns["poradi"].HeaderText = "Poradi";
                    dataGridViewZmeny.Columns["datum"].HeaderText = "Datum";
                    dataGridViewZmeny.Columns["nazev"].HeaderText = "Název";
                    dataGridViewZmeny.Columns["rozmer"].HeaderText = "Rozměr";
                    dataGridViewZmeny.Columns["jk"].HeaderText = "JK";
                    dataGridViewZmeny.Columns["vevcislo"].HeaderText = "I. ev. číslo";
                    dataGridViewZmeny.Columns["stavks"].HeaderText = "KS";
                    dataGridViewZmeny.Columns["cena"].HeaderText = "Cena";
                    dataGridViewZmeny.Columns["poznamka"].HeaderText = "Poznámka";
                    dataGridViewZmeny.Columns["poradi"].Visible = false;
                    dataGridViewZmeny.Columns["oscislo"].Visible = false;

                    dataGridViewZmeny.Columns["pjmeno"].Visible = false;
                    dataGridViewZmeny.Columns["pprijmeni"].Visible = false;
                    dataGridViewZmeny.Columns["pnazev"].Visible = false;
                    dataGridViewZmeny.Columns["pjk"].Visible = false;
                    dataGridViewZmeny.Columns["nporadi"].Visible = false;
                    dataGridViewZmeny.Columns["zporadi"].Visible = false;
                    dataGridViewZmeny.Columns["pujcks"].Visible = false;

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

        private void zapůjčeníNářadíToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // zobrazime seznam polozek naradi
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userHasAccessRightsWM((Int32)permCode.PracZapN))
            {
                SeznamNaradiJednoduchy seznamNar = new SeznamNaradiJednoduchy(myDB, parentFont);
                //            seznamNar.Font = this.Font;
                if (seznamNar != null)
                {
                    seznamNar.Visible = false;   // formular se automaticky presune do show musime tedy ho vypnout
                    if (seznamNar != null)
                    {
                        try  // protoze konstruktor saznam naradi jednoduchy -  pracuje dlouho s natahovabim polozek - uzivatel jem muze prerusit a tim dojde k odstraneni objektu musime tedy testovat existenci objektu
                        {
                            if (seznamNar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                SeznamNaradiJednoduchy.messager myMesenger = seznamNar.getMesseger();
                                ZapujceniNaradi zapujcNaradi = new ZapujceniNaradi(osobyDBRow, myMesenger.nazev, myMesenger.jk, myMesenger.fyzStav, parentFont);
//                                zapujcNaradi.Font = parentFont
                                if (zapujcNaradi.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                {
                                    // pridame zapujcene naradi
                                    int pujcPoradi;
                                    if ((pujcPoradi = myDB.addNewLineZmenyAndPujceno(myMesenger.poradi, zapujcNaradi.getDatum(), zapujcNaradi.getKs(), zapujcNaradi.getPoznamka(), zapujcNaradi.getVevCislo(), osCislo)) < 0)
                                    {
                                        if (pujcPoradi == -2) MessageBox.Show("Není možno vypůjčit více kusů než je stav na výdejně. Lituji.");
                                        else MessageBox.Show("Vypůjčeni nářadi se nezdařilo. Lituji.");
                                    }
                                    else
                                    {
                                        // prodame do  formulare // 
                                        Hashtable DBPujcRow = new Hashtable();
                                        Int32 zporadi = 0;
                                        if (myDB.getPujcenoLine(pujcPoradi, DBPujcRow) != null)
                                        {
                                            if (DBPujcRow.Contains("zporadi"))
                                            {
                                                zporadi = Convert.ToInt32(DBPujcRow["zporadi"]);
                                            }
                                        }

                                        // poradi, datum. nazev, rozmer, jk, vevcislo, stavks. cena, poznamka, oscislo, pjmeno, prijmeni, pnazev, pjk, pujcks, nporadi, zporadi
                                        // stavks je soucasny stav ks je pouze v tabulce pujceno
                                        // pujcks je brano jako vydej z tabulky zmeny pripadne jako pks (pomocne ks) z tabulky pujceno
                                        (dataGridViewZmeny.DataSource as DataTable).Rows.Add(pujcPoradi, zapujcNaradi.getDatum(), myMesenger.nazev, myMesenger.rozmer, myMesenger.jk, zapujcNaradi.getVevCislo(), zapujcNaradi.getKs(), myMesenger.cena,
                                                                                             zapujcNaradi.getPoznamka(), zapujcNaradi.getOsCiclo(), zapujcNaradi.getJmeno(), zapujcNaradi.getPrijmeni(), myMesenger.nazev, myMesenger.jk, zapujcNaradi.getKs(), myMesenger.poradi, zporadi);
                                        int counter = dataGridViewZmeny.Rows.Count - 1;

                                        dataGridViewZmeny.FirstDisplayedScrollingRowIndex = dataGridViewZmeny.Rows[counter].Index;
                                        dataGridViewZmeny.Refresh();

                                        dataGridViewZmeny.CurrentCell = dataGridViewZmeny.Rows[counter].Cells[1];
                                        dataGridViewZmeny.Rows[counter].Selected = true;
                                    }
                                }
                            }
                        }
                        catch { };
                    }
                }
            }
        }

        private Hashtable makeVypujcDBRow(Hashtable DBRow)
        {
            Hashtable DBVypujcRow = (Hashtable)DBRow.Clone();
            DataGridViewRow myRow = dataGridViewZmeny.SelectedRows[0];

            for (int i = 0; i < dataGridViewZmeny.ColumnCount; i++)
            {
                if (DBVypujcRow.ContainsKey(dataGridViewZmeny.Columns[i].Name))
                {
                    DBVypujcRow.Remove(dataGridViewZmeny.Columns[i].Name);
                }
                DBVypujcRow.Add(dataGridViewZmeny.Columns[i].Name, myRow.Cells[i].Value);
            }
            return DBVypujcRow;

        }


        private void vraceníNářadíToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //vratime naradi
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userHasAccessRightsWM((Int32)permCode.PracVracN))
            {
                if (dataGridViewZmeny.SelectedRows.Count > 0)
                {
                    if ((myDB != null) && (myDB.DBIsOpened()))
                    {
                        Hashtable DBVypujcRow = makeVypujcDBRow(osobyDBRow);
                        VraceniNaradi vraceniNaradi = new VraceniNaradi(DBVypujcRow);
                        vraceniNaradi.Font = parentFont;
                        Int32 pujcPoradi = Convert.ToInt32(DBVypujcRow["poradi"]);

                        if (vraceniNaradi.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            Int32 errCode = myDB.addNewLineZmenyAndVraceno(pujcPoradi, vraceniNaradi.getDatum(), vraceniNaradi.getKs(),
                                vraceniNaradi.getPoznamka(), Convert.ToString(DBVypujcRow["oscislo"]));
                            if (errCode == -4)
                            {
                                MessageBox.Show("Stav změn je záporné číslo. Nejprve opravte data o pohybu nářadí.");
                            }
                            if (errCode == -3)
                            {
                                MessageBox.Show("Neexistují žádné záznamy o pohybu nářadi. Nejprve opravte data o pohybu nářadí.");
                            }
                            if (errCode == -2)
                            {
                                MessageBox.Show("Požadujete vrátit vetší možství než je vypůjčeno. Data byla patrně změněna z jiného pracoviště.");
                            }
                            if (errCode == -1)
                            {
                                MessageBox.Show("Vrácení nářadi se nezdařilo. Lituji.");
                            }
                            if (errCode >= 0)
                            {
                                // opravime tabulku
                                Hashtable DBPujcenoRow = null;
                                DBPujcenoRow = myDB.getPujcenoLine(Convert.ToInt32(DBVypujcRow["poradi"]), DBPujcenoRow);
                                if (DBPujcenoRow != null)
                                {
                                    // opravime radku

                                    // je potreba najit index v datove tabulce - po trideni neni schodny s indexem ve view
                                    Int32 dataRowIndex = detail.findIndex(dataGridViewZmeny.DataSource as DataTable, "poradi", pujcPoradi);
                                    if (dataRowIndex != -1)
                                    {
                                        (dataGridViewZmeny.DataSource as DataTable).Rows[dataRowIndex].SetField(6, Convert.ToString(DBPujcenoRow["stavks"]));
                                        dataGridViewZmeny.Refresh();
                                    }
                                }
                                else
                                {
                                    viewZmenyRemoveSelectedRow(pujcPoradi);
                                    // smazeme radku
                                }
                            }
                        }
                    }
                }
            }
        }


        private void informaceONaradiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewZmeny.SelectedRows.Count > 0)
            {
                if ((myDB != null) && (myDB.DBIsOpened()))
                {
                    DataGridViewRow myRow = dataGridViewZmeny.SelectedRows[0];
                    Int32 nporadi;

                    try
                    {
                        nporadi = Convert.ToInt32(myRow.Cells["nporadi"].Value);
                    }
                    catch
                    {
                        nporadi = -1;
                    }


                    if (nporadi != -1)
                    {
                        Hashtable infoDBRow = myDB.getNaradiLine(nporadi, null);

                        SkladovaKarta sklKarta = new SkladovaKarta(myDB, infoDBRow, nporadi, new tableItemExistDelgStr(myDB.tableNaradiItemExist), parentFont,false);
//                        sklKarta.Font = parentFont
                        sklKarta.setWinName("Skladová karta");
                        sklKarta.ShowDialog();
                    }
                }
            }
        }

        private void informaceOZapůjčeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
            if (dataGridViewZmeny.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedDGVRow = dataGridViewZmeny.SelectedRows[0];
                Int32 poradi = Convert.ToInt32(selectedDGVRow.Cells["poradi"].Value);

                Hashtable vypujcDBRow = myDB.getPujcenoLine(poradi, null);
                if (vypujcDBRow != null)
                {
//                    vypujcDBRow.Add("oscislo", labelOsCislo.Text);
                    Int32 nporadi = -1;
                    if (vypujcDBRow.Contains("nporadi"))
                    {
                        nporadi = Convert.ToInt32(vypujcDBRow["nporadi"]);
                        if (nporadi != -1)
                        {
                            Hashtable infoDBRow = myDB.getNaradiLine(nporadi, null);
                            if (infoDBRow != null)
                            {
                                vypujcDBRow.Add("nazev", infoDBRow["nazev"]);
                                vypujcDBRow.Add("rozmer", infoDBRow["rozmer"]);
                                vypujcDBRow.Add("normacsn", infoDBRow["normacsn"]);
                                vypujcDBRow.Add("jk", infoDBRow["jk"]);
                                vypujcDBRow.Add("cena", infoDBRow["cena"]);
                            }
                        }

                        Int32 zporadi = -1;
                        if (vypujcDBRow.Contains("zporadi"))
                        {
                            zporadi = Convert.ToInt32(vypujcDBRow["zporadi"]);
                            if (zporadi != -1)
                            {
                                Hashtable zmenyDBRow = myDB.getZmenyLine(nporadi, zporadi, null);
                                vypujcDBRow.Add("poznamka", zmenyDBRow["poznamka"]);
                                vypujcDBRow.Add("vevcislo", zmenyDBRow["vevcislo"]);
                                vypujcDBRow.Add("datum", zmenyDBRow["datum"]);
                                vypujcDBRow.Add("vydej", zmenyDBRow["vydej"]);
                            }
                        }



                        ZapujceneNaradiInfo zapNarInfo = new ZapujceneNaradiInfo(vypujcDBRow, parentFont);
//                        zapNarInfo.Font = parentFont
                        zapNarInfo.ShowDialog();


                    }
                }
            }
        }


        private void poskozeniNaradiToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userHasAccessRightsWM((Int32)permCode.NarPosk))
            {
                if (dataGridViewZmeny.SelectedRows.Count > 0)
                {
                    DataGridViewRow myRow = dataGridViewZmeny.SelectedRows[0];

                    DataGridViewRow selectedDGVRow = dataGridViewZmeny.SelectedRows[0];
                    Int32 pujcPoradi = Convert.ToInt32(selectedDGVRow.Cells["poradi"].Value);

                    Hashtable vypujcDBRow = myDB.getPujcenoLine(pujcPoradi, null);
                    if (vypujcDBRow != null)
                    {
                        Int32 nporadi = -1;
                        if (vypujcDBRow.Contains("nporadi"))
                        {
                            nporadi = Convert.ToInt32(vypujcDBRow["nporadi"]);
                            if (nporadi != -1)
                            {
                                Hashtable infoDBRow = myDB.getNaradiLine(nporadi, null);
                                if (infoDBRow != null)
                                {
                                    vypujcDBRow.Add("nazev", infoDBRow["nazev"]);
                                    vypujcDBRow.Add("rozmer", infoDBRow["rozmer"]);
                                    vypujcDBRow.Add("normacsn", infoDBRow["normacsn"]);
                                    vypujcDBRow.Add("jk", infoDBRow["jk"]);
                                    vypujcDBRow.Add("cena", infoDBRow["cena"]);
                                    vypujcDBRow.Add("ucetstav", infoDBRow["ucetstav"]);
                                    vypujcDBRow.Add("fyzstav", infoDBRow["fyzstav"]);
                                    vypujcDBRow.Add("celkcena", infoDBRow["celkcena"]);
                                }
                            }
                        }


                        Poskozenka poskozenka = new Poskozenka(vypujcDBRow, myDB, parentFont, true);
                        if (poskozenka.ShowDialog() == DialogResult.OK)
                        {
                            Poskozenka.messager mesenger = poskozenka.getMesseger();
                            int errCode;
                            // -1 obecna chyba 
                            // -2 pokus o vraceni o odepsani vice kusu nez bylo vypujceno
                            // -3 pokus o odepsani o zruseni o vice nez je ucetni stav // ucetni stav by byl zaporny 
                            // -4 stavks v tabulce zmen je zaporny
                            // -5 v tabulce změn nejsou žádné záznamy

                            // poradi - tabulka pujceno
                            errCode = myDB.addNewLineZmenyAndVracenoAndPoskozeno(pujcPoradi, mesenger.datum, mesenger.pocetKs, mesenger.poznamka, labelOsCislo.Text, mesenger.konto, mesenger.cisZak);



                            if (errCode == -4)
                            {
                                MessageBox.Show("Stav změn je záporné číslo. Nejprve opravte data o pohybu nářadí.");
                            }
                            if (errCode == -5)
                            {
                                MessageBox.Show("Neexistují žádné záznamy o pohybu nářadi. Nejprve opravte data o pohybu nářadí.");
                            }
                            if (errCode == -3)
                            {
                                MessageBox.Show("Nemohu odepsat poškozené položky. Požadováno je odepsat více než je účetní stav. Lituji.");
                            }

                            if (errCode == -2)
                            {
                                MessageBox.Show("Požadujete vrátit vetší možství než je vypůjčeno. Data byla patrně změněna z jiného pracoviště.");
                            }
                            if (errCode == -1)
                            {
                                MessageBox.Show("Vrácení nářadi se nezdařilo. Lituji.");
                            }

                            /////
                            if (errCode == 0)
                            {

                                // opravime tabulku
                                Hashtable DBPujcenoRow;
                                //                                    DBPujcenoRow = myDB.getPujcenoLine(Convert.ToInt32(DBVypujcRow["poradi"]), null);
                                DBPujcenoRow = myDB.getPujcenoLine(pujcPoradi, null);
                                if (DBPujcenoRow != null)
                                {
                                    // opravime radku

                                    // je potreba najit index v datove tabulce - po trideni neni schodny s indexem ve view
                                    Int32 dataRowIndex = detail.findIndex(dataGridViewZmeny.DataSource as DataTable, "poradi", pujcPoradi);
                                    if (dataRowIndex != -1)
                                    {
                                        (dataGridViewZmeny.DataSource as DataTable).Rows[dataRowIndex].SetField("stavks", Convert.ToString(DBPujcenoRow["stavks"]));
                                        dataGridViewZmeny.Refresh();
                                    }
                                }
                                else
                                {
                                    // smazeme vybranou radku
                                    viewZmenyRemoveSelectedRow(pujcPoradi);
                                }
                            }
                        }
                    }
                }
            }
        }


        public virtual void setColumnWidth()
        {
            Hashtable DBTableInfo = ConfigReg.loadSettingWindowTableColumnWidth(formName, "pujceno");
            if (DBTableInfo != null)
            {
                Int32 columnWidth = 0;
                for (Int32 i = 0; i < dataGridViewZmeny.Columns.Count;i++)
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



        private void ZapujceneNaradiKarta_SizeChanged(object sender, EventArgs e)
        {
            if (evenState == evenStateEnum.enable)
            {
                if (!(this.Size.IsEmpty)) ConfigReg.saveSettingWindowLocationSize(formName, 0, 0, this.Size.Width, this.Size.Height);
            }
        }

        private void ZapujceneNaradiKarta_LocationChanged(object sender, EventArgs e)
        {
            if (evenState == evenStateEnum.enable)
            {
                if (!(this.Location.IsEmpty)) ConfigReg.saveSettingWindowLocationSize(formName, this.Location.X, this.Location.Y, 0, 0);
            }

        }

        private void dataGridViewZmeny_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (evenState == evenStateEnum.enable)
            {
                ConfigReg.saveSettingWindowTableColumnWidth(formName, "pujceno", e.Column.Name, e.Column.Width);
            }
        }

        private void dataGridViewZmeny_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (evenState == evenStateEnum.enable)
            {
                ConfigReg.saveSettingWindowTableColumnIndex(formName, "pujceno", e.Column.Name, e.Column.DisplayIndex);
            }
        }

        private void ZapujceneNaradiKarta_Shown(object sender, EventArgs e)
        {
            evenState = evenStateEnum.enable;
        }

        private void vybratPísmoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chooseFont();
        }

        private void písmoAplikaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setAppFont();
        }
    }
}
