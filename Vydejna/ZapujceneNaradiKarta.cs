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

        private vDatabase myDB;
        Hashtable osobyDBRow;
        private string osCislo;

        private enum evenStateEnum { enable, disable };

        private evenStateEnum evenState = evenStateEnum.disable;


        public ZapujceneNaradiKarta(string osCislo, vDatabase myDataBase, Font myFont)
        {
            myDB = myDataBase;
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
            this.Font = myFont;

            Size size = ConfigReg.loadSettingWindowSize("LEND");
            if (!(size.IsEmpty)) this.Size = size;
            evenState = evenStateEnum.enable;
        }

        private void ZapujceneNaradiKarta_Load(object sender, EventArgs e)
        {

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

                    dataGridViewZmeny.Columns[0].HeaderText = "Poradi";
                    dataGridViewZmeny.Columns[1].HeaderText = "Datum";
                    dataGridViewZmeny.Columns[2].HeaderText = "Název";
                    dataGridViewZmeny.Columns[3].HeaderText = "Rozměr";
                    dataGridViewZmeny.Columns[4].HeaderText = "JK";
                    dataGridViewZmeny.Columns[5].HeaderText = "I. ev. číslo";
                    dataGridViewZmeny.Columns[6].HeaderText = "KS";
                    dataGridViewZmeny.Columns[7].HeaderText = "Cena";
                    dataGridViewZmeny.Columns[8].HeaderText = "Poznámka";
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
            SeznamNaradiJednoduchy seznamNar = new SeznamNaradiJednoduchy(myDB, this.Font);
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
                            ZapujceniNaradi zapujcNaradi = new ZapujceniNaradi(osobyDBRow, myMesenger.nazev, myMesenger.jk, myMesenger.fyzStav, this.Font);
                            zapujcNaradi.Font = this.Font;
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
            if (dataGridViewZmeny.SelectedRows.Count > 0)
            {
                if ((myDB != null) && (myDB.DBIsOpened()))
                {
                    Hashtable DBVypujcRow = makeVypujcDBRow(osobyDBRow);
                    VraceniNaradi vraceniNaradi = new VraceniNaradi(DBVypujcRow);
                    vraceniNaradi.Font = this.Font;
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
                        if (errCode == 0)
                        {
                            // opravime tabulku
                            Hashtable DBPujcenoRow = null;
                            DBPujcenoRow = myDB.getPujcenoLine(Convert.ToInt32(DBVypujcRow["poradi"]), DBPujcenoRow);
                            if (DBPujcenoRow != null)
                            {
                                // opravime radku

                                // je potreba najit index v datove tabulce - po trideni neni schodny s indexem ve view
                                Int32 dataRowIndex = -1;
                                for (int x = 0; x < (dataGridViewZmeny.DataSource as DataTable).Rows.Count; x++)
                                {
                                    if (Convert.ToInt32((dataGridViewZmeny.DataSource as DataTable).Rows[x]["poradi"]) == pujcPoradi)
                                    {
                                        dataRowIndex = x;
                                        break;
                                    }
                                }
                                if (dataRowIndex != -1)
                                {
                                    (dataGridViewZmeny.DataSource as DataTable).Rows[dataRowIndex].SetField(6, Convert.ToString(DBPujcenoRow["stavks"]));
                                    dataGridViewZmeny.Refresh();
                                }
                            }
                            else
                            {
                                // smazeme radku
                                dataGridViewZmeny.Rows.Remove(dataGridViewZmeny.SelectedRows[0]);
                                Int32 counter = dataGridViewZmeny.Rows.Count - 1;
                                if (counter > 0)
                                {
                                    dataGridViewZmeny.FirstDisplayedScrollingRowIndex = dataGridViewZmeny.Rows[counter].Index;
                                    dataGridViewZmeny.Refresh();
                                    dataGridViewZmeny.CurrentCell = dataGridViewZmeny.Rows[counter].Cells[1];
                                    dataGridViewZmeny.Rows[counter].Selected = true;
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
                    Int32 nporadi = -1;

                    for (int i = 0; i < dataGridViewZmeny.ColumnCount; i++)
                    {
                        if (dataGridViewZmeny.Columns[i].Name == "nporadi")
                        {
                            nporadi = Convert.ToInt32(myRow.Cells[i].Value);
                        }
                    }

                    if (nporadi != -1)
                    {
                        Hashtable infoDBRow = myDB.getNaradiLine(nporadi, null);

                        SkladovaKarta sklKarta = new SkladovaKarta(myDB, infoDBRow, nporadi, new tableItemExistDelgStr(myDB.tableNaradiItemExist), this.Font);
                        sklKarta.Font = this.Font;
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
                Int32 poradi = Convert.ToInt32(selectedDGVRow.Cells[0].Value);

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



                        ZapujceneNaradiInfo zapNarInfo = new ZapujceneNaradiInfo(vypujcDBRow, this.Font);
                        zapNarInfo.Font = this.Font;
                        zapNarInfo.ShowDialog();


                    }
                }
            }
        }


        private void poskozeniNaradiToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridViewZmeny.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedDGVRow = dataGridViewZmeny.SelectedRows[0];
                Int32 poradi = Convert.ToInt32(selectedDGVRow.Cells[0].Value);

                Hashtable vypujcDBRow = myDB.getPujcenoLine(poradi, null);
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

//                        Int32 zporadi = -1;
                        // pujceno.stavks je soucasny stav zapujceneho naradi danemu uzivately
                        // zmeny.vydej je kolik mu bylo povodne pujceno

//                        if (vypujcDBRow.Contains("zporadi"))
//                        {
//                            zporadi = Convert.ToInt32(vypujcDBRow["zporadi"]);
//                            if (zporadi != -1)
//                            {
//                                Hashtable zmenyDBRow = myDB.getZmenyLine(nporadi, zporadi, null);
//                                vypujcDBRow.Add("poznamka", zmenyDBRow["poznamka"]);
//                                vypujcDBRow.Add("vevcislo", zmenyDBRow["vevcislo"]);
//                                vypujcDBRow.Add("datum", zmenyDBRow["datum"]);
//                                vypujcDBRow.Add("vydej", zmenyDBRow["vydej"]);
//                            }
//                        }


                        Poskozenka poskozenka = new Poskozenka(vypujcDBRow, myDB, this.Font,true);
                        if (poskozenka.ShowDialog() == DialogResult.OK)
                        {
                            Poskozenka.messager mesenger = poskozenka.getMesseger();
                                                int errCode;
                                                                // poradi - tabulka pujceno
                                                if ((errCode = myDB.addNewLineZmenyAndVracenoAndPoskozeno(poradi, mesenger.datum, mesenger.pocetKs, mesenger.poznamka, labelOsCislo.Text, mesenger.konto, mesenger.cisZak)) < 0)
                                                {
                                                    if (errCode == -2)
                                                        MessageBox.Show("Nemohu odepsat poškozené položky. Učetní stav nebo stav výdejny je menší než požadované množství. Lituji.");
                                                    else
                                                        MessageBox.Show("Odepsání poškozených položek se nezdařilo. Lituji.");
                            
                                                }

                        }

                        //                        ZapujceneNaradiInfo zapNarInfo = new ZapujceneNaradiInfo(vypujcDBRow, this.Font);
                        //                        zapNarInfo.Font = this.Font;
                        //                        zapNarInfo.ShowDialog();


                    }
                }
            }

        }

        private void ZapujceneNaradiKarta_SizeChanged(object sender, EventArgs e)
        {
            if (evenState == evenStateEnum.enable)
            {
                if (!(this.Size.IsEmpty)) ConfigReg.saveSettingWindowLocationSize("LEND", 0, 0, this.Size.Width, this.Size.Height);
            }
        }
    }
}
