using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;



namespace Vydejna
{
    abstract class detail
    {
        public vDatabase myDB;
        public DataGridView myDataGridView;
        private Prohledavani searchWindow;


        public detail(vDatabase myDB, DataGridView myDataGridView)
        {
            this.myDB = myDB;
            this.myDataGridView = myDataGridView;
            searchWindow = null;
        }

                
        /// <summary>
        /// Najde v DB radku hodnotu sloupce poradi;
        /// </summary>
        /// <param name="DBRow"> Radka DB tabulky</param>
        /// <returns>Hodnotu sloupce poradi</returns>
        public static Int32 findPoradiInRow(Hashtable DBRow)
        {
            if (DBRow != null)
            {
                Int32 poradi = 0;
                if (DBRow.Contains("poradi"))
                {
                    poradi = Convert.ToInt32(DBRow["poradi"]);
                    return poradi;
                }
            }
            return 0;
        }


        public static string findOsCisloInRow(Hashtable DBRow)
        {
            if (DBRow != null)
            {
                string osCislo = "";
                if (DBRow.Contains("oscislo"))
                {
                    osCislo = Convert.ToString(DBRow["oscislo"]);
                    return osCislo;
                }
            }
            return "";
        }


/// <summary>
/// Najde v datove tabulce cislo radku jejiz sloupec name ma hodnotu value
/// </summary>
/// <param name="myDT">Datova tabulka</param>
/// <param name="name">Jmeno prohledavaneho sloupce</param>
/// <param name="value">Hledana hodnota</param>
/// <returns></returns>
        public static Int32 findIndex(DataTable myDT, string name, Int32 value)
        {
            if (myDT == null) return -1;
            if (name.Trim() == "") return -1;
            if (myDT.Columns.Contains(name))
            {

                for (int x = 0; x < myDT.Rows.Count; x++)
                {
                    if (Convert.ToInt32(myDT.Rows[x][name]) == value)
                    {
                        return x;
                    }
                }
            }
            return -1;
        }


        public static Int32 findIndex(DataTable myDT, string name, string value)
        {
            if (myDT == null) return -1;
            if (name.Trim() == "") return -1;
            if (myDT.Columns.Contains(name))
            {

                for (int x = 0; x < myDT.Rows.Count; x++)
                {
                    if (Convert.ToString(myDT.Rows[x][name]) == value)
                    {
                        return x;
                    }
                }
            }
            return -1;
        }


        public void reloadRow (DataTable myDT, Int32 index, Hashtable DBRow)
        {
            if ((myDT != null) && (index != -1) && (DBRow != null))
            {
                foreach (string name in DBRow.Keys)
                {
                    if (myDT.Columns.Contains(name))
                    {
                         myDT.Rows[index].SetField(name, DBRow[name]);
                    }
                }

            }
        }
        //-------------------------------------- virtualni metody -------------------//

        public virtual void zobrazKartu(Hashtable DBRow)
        {
        }

        public virtual void pridejKartu()
        {
            MessageBox.Show("Není implementováno.");
        }

        public virtual void opravKartu(Hashtable DBRow)
        {
            MessageBox.Show("Není implementováno.");
        }

        public virtual void zrusKartu(Hashtable DBRow)
        {
            MessageBox.Show("Není implementováno.");
        }

        public virtual void Prijem(Hashtable DBRow)
        {
            MessageBox.Show("Není implementováno.");
        }

        public virtual void Poskozeno(Hashtable DBRow)
        {
            MessageBox.Show("Není implementováno.");
        }


        public virtual void Zapujceno(Hashtable DBRow)
        {
            MessageBox.Show("Není implementováno.");
        }

        public virtual string preferovanySloupec()
        {
            return "";
        }

        public virtual void NastaveniHledani(Int32 x, Int32 y)
        {
            if (searchWindow == null)
            {
                searchWindow = new Prohledavani(myDataGridView, preferovanySloupec());
                searchWindow.StartPosition = FormStartPosition.Manual;
            }

            if (x > (Screen.PrimaryScreen.Bounds.Width - 50))
            {
                x = Screen.PrimaryScreen.Bounds.Width - searchWindow.Width;
            }
            if (x < 0) {x = 0;}

            if (y > (Screen.PrimaryScreen.Bounds.Height - 50))
            {
                y = Screen.PrimaryScreen.Bounds.Height - searchWindow.Height;
            }
            if (y < 0) { y = 0; }


            searchWindow.SetDesktopLocation(x, y);
            searchWindow.ShowDialog();
        }

        public virtual void HledejDalsi(Int32 x, Int32 y)
        {
            if (searchWindow == null)
            {
                NastaveniHledani(x, y);
            }
            else
            {
                searchWindow.najdiRadku();
            }
        }
    


    }

    class detailNone : detail
    {
        public detailNone(vDatabase myDB, DataGridView myDataGridView)
            : base(myDB, myDataGridView)
        {
        }

        public override void HledejDalsi(Int32 x, Int32 y)
        {
        }

    }
    
    
    class detailSklad : detail  // karta naradi
    {

        // DBRow        Sklad Karta             mesenger    
        // poradi       poradi                  poradi
        // kodd            --                    ---
        // nazev        textBoxNazev.Text       nazev
        // jk           textBoxJK               jk
        // ucetstav     numericUpDownUcetStav   ucetStav
        // analucet     textBoxUcet             ucet
        // normacsn     textBoxCSN              csn
        // normadin     textBoxDIN              din
        // vyrobce      textBoxVyrobce          vyrobce
        // rozmer       textBoxRozmer           rozmer
        // fyzstav      fyzStav                 fyzStav
        // cena         numericUpDownCenaKs     cenaKs
        // celkcena     numericUpDownUcetCena   ucetCena
        // minimum      numericUpDownMinStav    minStav
        // poznamka     textBoxPoznamka         poznamka
        // ucetkscen    numericUpDownUcetCenaKs ucetCenaKs

        public detailSklad(vDatabase myDB, DataGridView myDataGridView)
            : base(myDB, myDataGridView)
        {
        }

        public override void zobrazKartu(Hashtable DBRow) 
            
        {
            if ((myDB != null) && (myDB.DBIsOpened()))
            {
                Int32 poradi = findPoradiInRow(DBRow);
                DBRow = myDB.getNaradiLine(poradi,DBRow);

                SkladovaKarta sklKarta = new SkladovaKarta(myDB, DBRow, findPoradiInRow (DBRow), new tableItemExistDelgStr(myDB.tableNaradiItemExist));
                sklKarta.setWinName("Skladová karta");
                sklKarta.ShowDialog();

                DBRow = myDB.getNaradiLine(poradi, DBRow);
                reloadRow((myDataGridView.DataSource as DataTable), findIndex((myDataGridView.DataSource as DataTable), "poradi", poradi), DBRow);
            }
        }

        public override void pridejKartu()
        {
            // zalozeni nove skladove karty
            if ((myDB != null) && (myDB.DBIsOpened()))
            {
                SkladovaKarta sklKarta = new SkladovaKarta(myDB, new tableItemExistDelgStr(myDB.tableNaradiItemExist));
                sklKarta.setWinName("Skladová karta");
                if (sklKarta.ShowDialog() == DialogResult.OK)
                {

                    SkladovaKarta.messager mesenger = sklKarta.getMesseger();

                    Int32 poradi = myDB.addNewLineNaradi(mesenger.nazev, mesenger.jk, mesenger.csn, mesenger.din, mesenger.vyrobce, mesenger.cenaKs, mesenger.poznamka, mesenger.minStav, mesenger.ucetCena, mesenger.ucetStav, mesenger.ucetStav, mesenger.rozmer, mesenger.ucet, mesenger.ucetCenaKs, new DateTime(0));
                    if (poradi != -1)
                    {
                       (myDataGridView.DataSource as DataTable).Rows.Add(poradi, "", mesenger.nazev, mesenger.jk, mesenger.ucetStav, mesenger.ucet, mesenger.csn, mesenger.din, mesenger.vyrobce, mesenger.rozmer, 0, mesenger.cenaKs, mesenger.ucetCena, mesenger.minStav, mesenger.poznamka, mesenger.ucetCenaKs);
                       int counter = myDataGridView.Rows.Count - 1;

                       myDataGridView.FirstDisplayedScrollingRowIndex = myDataGridView.Rows[counter].Index;
                       myDataGridView.Refresh();

                       myDataGridView.CurrentCell = myDataGridView.Rows[counter].Cells[1];
                       myDataGridView.Rows[counter].Selected = true;

                    }

                }
            }
        }


        public override void opravKartu(Hashtable DBRow)
        {
            if ((myDB != null) && (myDB.DBIsOpened()))
            {
                Int32 poradi = findPoradiInRow(DBRow);
                DBRow = myDB.getNaradiLine(poradi,DBRow);
                SkladovaKarta sklKarta = new SkladovaKarta(myDB, DBRow, poradi, new tableItemExistDelgStr(myDB.tableNaradiItemExist), sKartaState.edit);
                if (sklKarta.ShowDialog() == DialogResult.OK)
                {
                    SkladovaKarta.messager mesenger = sklKarta.getMesseger();
                    Boolean updateIsOk = myDB.editNewLineNaradi(poradi, mesenger.nazev, mesenger.jk, mesenger.csn, mesenger.din, mesenger.vyrobce, mesenger.cenaKs, mesenger.poznamka, mesenger.minStav, mesenger.ucetCena, mesenger.ucetStav, mesenger.fyzStav, mesenger.rozmer, mesenger.ucet, mesenger.ucetCenaKs);
                    if (updateIsOk)
                    {
                        // je potreba najit index v datove tabulce - po trideni neni schodny s indexem ve view
                        Int32 dataRowIndex = findIndex((myDataGridView.DataSource as DataTable), "poradi", poradi);

                        if (dataRowIndex != -1)
                        {
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(2, mesenger.nazev);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(3, mesenger.jk);

                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(4, mesenger.ucetStav);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(5, mesenger.ucet);

                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(6, mesenger.csn);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(7, mesenger.din);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(8, mesenger.vyrobce);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(9, mesenger.rozmer);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(10, mesenger.fyzStav);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(11, mesenger.cenaKs);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(12, mesenger.ucetCena);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(13, mesenger.minStav);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(14, mesenger.poznamka);

                            myDataGridView.Refresh();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Opravení karty se nezdařilo.");
                    }

                }
                else
                {
                    Hashtable newDBRow = null;
                    newDBRow = myDB.getNaradiLine(poradi, newDBRow);
                    reloadRow((myDataGridView.DataSource as DataTable), findIndex((myDataGridView.DataSource as DataTable), "poradi", poradi), newDBRow);
                }
            }
        }


        public override void zrusKartu(Hashtable DBRow)
        {
            if (MessageBox.Show("Opravdu chcete zrušit kartu ?", "Zrušení karty", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // zrusime kartu
                Int32 poradi = Convert.ToInt32(DBRow["poradi"]);

                if (!(myDB.tablePujcenoExistOnNPoradi(poradi)))
                {

                    if (myDB.moveNaradiToNewKaret(poradi))
                    {
                        // smazeme z obrazovky
                        // je potreba najit index v datove tabulce - po trideni neni schodny s indexem ve view
                        Int32 dataRowIndex = findIndex((myDataGridView.DataSource as DataTable), "poradi", poradi);
                        if (dataRowIndex != -1)
                        {
                            // smazeme radku
                            (myDataGridView.DataSource as DataTable).Rows.RemoveAt(dataRowIndex);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Zrušení karty se nezdařilo.");
                    }
                }
                else
                {
                    MessageBox.Show("Na kartě jsou zapůjčené položky, nelze proto zrušit. Lituji");
                }
            }
        }



        public override void Prijem(Hashtable DBRow)
        {
            if ((myDB != null) && (myDB.DBIsOpened()))
            {
                PrijemkaMaterialu prijemka = new PrijemkaMaterialu(DBRow, myDB);
                if (prijemka.ShowDialog() == DialogResult.OK)
                {
                    PrijemkaMaterialu.messager mesenger = prijemka.getMesseger();
                    if (myDB.addNewLineZmeny(mesenger.poradi, mesenger.jk, mesenger.datum, mesenger.pocetKs, 0, mesenger.poznamka, "P", mesenger.pocetKs, mesenger.pocetKs, "") < 0)
                    {
                        MessageBox.Show("Příjem materialu se nezdařil. Lituji.");
                    }
                    else
                    {
                        Int32 dataRowIndex = findIndex((myDataGridView.DataSource as DataTable), "poradi", mesenger.poradi);
                        if (dataRowIndex != -1)
                        {
                            // opravime tabulku
                            Hashtable DBrow = myDB.getNaradiZmenyLine(mesenger.poradi, null);
                            if (DBrow != null)
                            {
                                Int32 fyzStav = 0;
                                Int32 ucetStav = 0;

                                if (DBrow.ContainsKey("ucetstav") && DBrow.ContainsKey("zmeny_zustatek"))
                                {
                                    ucetStav = Convert.ToInt32(DBrow["ucetstav"]);
                                    int zustatek = Convert.ToInt32(DBrow["zmeny_zustatek"]);
                                    if (zustatek  != ucetStav) MessageBox.Show("Pozor! Patrně nesouhlasí stav karet a učetní stav položky.");
                                }
                                if (DBrow.ContainsKey("fyzstav"))
                                {
                                    fyzStav =  Convert.ToInt32(DBrow["fyzstav"]);
                                    (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(10, fyzStav);
                                }
                                if (DBrow.ContainsKey("ucetstav"))
                                {
                                    ucetStav = Convert.ToInt32(DBrow["ucetstav"]);
                                    (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(4, ucetStav);
                                }
                                if (fyzStav != ucetStav) MessageBox.Show("Pozor! Účetni a fyzický stav nesouhlasí.");
                            }
                        }
                    }
                }
            }
        }

        public override void Poskozeno(Hashtable DBRow)
        {
            if ((myDB != null) && (myDB.DBIsOpened()))
            {
                Poskozenka poskozenka = new Poskozenka(DBRow, myDB);
                if (poskozenka.ShowDialog() == DialogResult.OK)
                {
                    Poskozenka.messager mesenger = poskozenka.getMesseger();
                    int errCode;
                    if ((errCode = myDB.addNewLineZmenyAndPoskozeno(mesenger.poradi, mesenger.jk, mesenger.datum, mesenger.pocetKs, mesenger.poznamka, mesenger.osCislo, mesenger.jmeno, mesenger.prijmeni,
                                                               mesenger.stredisko, mesenger.provoz, mesenger.nazev, mesenger.rozmer, mesenger.konto, mesenger.cena, mesenger.celkCena, mesenger.csn, mesenger.cisZak )) < 0)
                    {
                        if (errCode == -2)
                        MessageBox.Show("Nemohu odepsat poškozené položky. Učetní stav nebo stav výdejny je menší než požadované množství. Lituji.");
                        else
                        MessageBox.Show("Odepsání poškozených položek se nezdařilo. Lituji.");

                    }
                    else
                    {
                        Int32 dataRowIndex = findIndex((myDataGridView.DataSource as DataTable), "poradi", mesenger.poradi);
                        if (dataRowIndex != -1)
                        {
                            // opravime tabulku
                            Hashtable DBrow = myDB.getNaradiZmenyLine(mesenger.poradi, null);
                            if (DBrow != null)
                            {
                                Int32 fyzStav = 0;
                                Int32 ucetStav = 0;

                                if (DBrow.ContainsKey("fyzstav") && DBrow.ContainsKey("zmeny_zustatek"))
                                {
                                    fyzStav = Convert.ToInt32(DBrow["fyzstav"]);
                                    int zustatek = Convert.ToInt32(DBrow["zmeny_zustatek"]);
                                    if (zustatek != fyzStav) MessageBox.Show("Pozor! Patrně nesouhlasí stav karet a stav vydejny položky.");
                                }
                                if (DBrow.ContainsKey("fyzstav") && DBrow.ContainsKey("ucetstav"))
                                {
                                    fyzStav = Convert.ToInt32(DBrow["fyzstav"]);
                                    ucetStav = Convert.ToInt32(DBrow["ucetstav"]);
                                    if (ucetStav < fyzStav) MessageBox.Show("Pozor! Účetní stav je menší než stav výdejny.");
                                }

                            }
                        }
                    }
                }
            }
        }

        public override string preferovanySloupec()
        {
            return "nazev";
        }
    }


    class detailZruseno : detail
    {
        public detailZruseno(vDatabase myDB, DataGridView myDataGridView)
            : base(myDB, myDataGridView)
        {
        }

        public override void zobrazKartu(Hashtable DBRow)
        {
            if ((myDB != null) && (myDB.DBIsOpened()))
            {

                Int32 poradi = findPoradiInRow(DBRow);
                DBRow = myDB.getZrusenoLine(poradi, DBRow);
                SkladovaKarta sklKarta = new SkladovaKarta(myDB, DBRow, findPoradiInRow(DBRow), new tableItemExistDelgStr(myDB.tableZrusenoItemExist));
                sklKarta.setWinName("Zrušená karta");
                sklKarta.ShowDialog();
                DBRow = myDB.getZrusenoLine(poradi, DBRow);
                reloadRow((myDataGridView.DataSource as DataTable), findIndex((myDataGridView.DataSource as DataTable), "poradi", poradi), DBRow);
            }
        }

        public override void opravKartu(Hashtable DBRow)
        {
            if ((myDB != null) && (myDB.DBIsOpened()))
            {
                Int32 poradi = findPoradiInRow(DBRow);
                DBRow = myDB.getZrusenoLine(poradi, DBRow);
                SkladovaKarta sklKarta = new SkladovaKarta(myDB, DBRow, findPoradiInRow(DBRow), new tableItemExistDelgStr(myDB.tableZrusenoItemExist), sKartaState.edit);
                sklKarta.setWinName("Zrušená karta");
                if (sklKarta.ShowDialog() == DialogResult.OK)
                {
                    SkladovaKarta.messager mesenger = sklKarta.getMesseger();
                    Boolean updateIsOk = myDB.editNewLineKaret(mesenger.poradi, mesenger.nazev, mesenger.jk, mesenger.csn, mesenger.din, mesenger.vyrobce, mesenger.cenaKs, mesenger.poznamka, mesenger.minStav, mesenger.ucetCena, mesenger.ucetStav, mesenger.fyzStav, mesenger.rozmer, mesenger.ucet);
                    if (updateIsOk)
                    {
                        // je potreba najit index v datove tabulce - po trideni neni schodny s indexem ve view
                        Int32 dataRowIndex = findIndex((myDataGridView.DataSource as DataTable), "poradi", mesenger.poradi);

                        if (dataRowIndex != -1)
                        {
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(1, mesenger.nazev);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(2, mesenger.jk);

                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(3, mesenger.ucetStav);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(4, mesenger.ucet);

                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(5, mesenger.csn);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(6, mesenger.din);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(7, mesenger.vyrobce);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(8, mesenger.rozmer);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(10, mesenger.cenaKs);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(11, mesenger.ucetCena);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(12, mesenger.minStav);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(13, mesenger.poznamka);

                            myDataGridView.Refresh();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Opravení karty se nezdařilo.");
                    }

                }
                else
                {
                    Hashtable newDBRow = null;
                    newDBRow = myDB.getZrusenoLine(poradi, newDBRow);
                    reloadRow((myDataGridView.DataSource as DataTable), findIndex((myDataGridView.DataSource as DataTable), "poradi", poradi), newDBRow);
                }

            }

        }


        public override void zrusKartu(Hashtable DBRow)
        {
            if (MessageBox.Show("Opravdu chcete zrušit kartu ?", "Zrušení karty", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // zrusime kartu
                Int32 poradi = Convert.ToInt32(DBRow["poradi"]);

                if (myDB.deleteLineKaret(poradi))
                {
                    // smazeme z obrazovky
                    // je potreba najit index v datove tabulce - po trideni neni schodny s indexem ve view
                    Int32 dataRowIndex = findIndex((myDataGridView.DataSource as DataTable),"poradi",poradi);
                    if (dataRowIndex != -1)
                    {
                        // smazeme radku
                        (myDataGridView.DataSource as DataTable).Rows.RemoveAt(dataRowIndex);
                    }
                }
                else
                {
                    MessageBox.Show("Zrušení karty se nezdařilo.");
                }
            }
        }

        public override string preferovanySloupec()
        {
            return "nazev";
        }
    }

    class detailPoskozeno : detail
    {

        // DBRow        Vrac Karta              mesenger    
        // nazev        textBoxNazev            nazev
        // jk           textBoxJK               jk
        // pocetks      numericUpDownPocetKS    pocetKs
        // rozmer       textBoxRozmer           rozmer
        // csn          textBoxCSN              csn
        // cena         numericUpDownCena       cena
        // datum        dateTimePickerDatum     datum
        // vyrobek      textBoxZakázka          zakazka
        // konto        textBoxKonto            konto
        // jmeno        textBoxPrijmeni.Text    prijmeni
        // krjmeno      textBoxJmeno.Text       jmeno
        // cislo        textBoxOsCislo          oscislo
        // dilna        textBoxStredisko        stredisko
        // pracoviste   textBoxProvoz           provoz

        public detailPoskozeno(vDatabase myDB, DataGridView myDataGridView)
            : base(myDB,myDataGridView)
        {
        }


        public override void zobrazKartu(Hashtable DBRow)
        {
            Int32 poradi = findPoradiInRow(DBRow);
            DBRow = myDB.getPoskozenoLine(poradi, DBRow);

            VraceneKarta poskozKarta = new VraceneKarta(DBRow);
            poskozKarta.setWinName("Poškozeno");
            poskozKarta.ShowDialog();
            DBRow = myDB.getPoskozenoLine(poradi, DBRow);
            reloadRow((myDataGridView.DataSource as DataTable), findIndex((myDataGridView.DataSource as DataTable), "poradi", poradi), DBRow);

        }

        public override void opravKartu(Hashtable DBRow)
        {
            if ((myDB != null) && (myDB.DBIsOpened()))
            {
                Int32 poradi = findPoradiInRow(DBRow);
                DBRow = myDB.getPoskozenoLine(poradi, DBRow);
                VraceneKarta poskozKarta = new VraceneKarta(DBRow, myDB, new tableItemExistDelgInt(myDB.tablePoskozenoItemExist), vKartaState.edit);
                poskozKarta.setWinName("Poškozeno");
                if (poskozKarta.ShowDialog() == DialogResult.OK)
                {
                    VraceneKarta.messager mesenger = poskozKarta.getMesseger();
                    Boolean updateIsOk = myDB.editNewLinePoskozene(mesenger.poradi, mesenger.jmeno, mesenger.prijmeni, mesenger.oscislo, mesenger.stredisko, mesenger.provoz, mesenger.nazev, mesenger.jk, mesenger.pocetKs, mesenger.rozmer, mesenger.csn, mesenger.cena, mesenger.datum, mesenger.zakazka, mesenger.konto);
                    if (updateIsOk)
                    {
                        // je potreba najit index v datove tabulce - po trideni neni schodny s indexem ve view
                        Int32 dataRowIndex = findIndex((myDataGridView.DataSource as DataTable), "poradi", mesenger.poradi);
                        if (dataRowIndex != -1)
                        {
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(1, mesenger.nazev);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(2, mesenger.jk);

                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(3, mesenger.pocetKs);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(4, mesenger.rozmer);

                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(5, mesenger.csn);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(6, mesenger.cena);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(7, mesenger.datum);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(8, mesenger.zakazka);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(9, mesenger.konto);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(10, mesenger.prijmeni);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(11, mesenger.jmeno);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(12, mesenger.oscislo);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(13, mesenger.stredisko);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(14, mesenger.provoz);

                            myDataGridView.Refresh();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Opravení karty se nezdařilo.");
                    }
                }
                else
                {
                    Hashtable newDBRow = null;
                    newDBRow = myDB.getPoskozenoLine(poradi, newDBRow);
                    reloadRow((myDataGridView.DataSource as DataTable), findIndex((myDataGridView.DataSource as DataTable), "poradi", poradi), newDBRow);
                }
            }
        }

        public override string preferovanySloupec()
        {
            return "nazev";
        }
    }



    class detailVraceno : detail
    {

        public detailVraceno(vDatabase myDB, DataGridView myDataGridView)
            : base(myDB, myDataGridView)
        {
        }


        public override void zobrazKartu(Hashtable DBRow)
        {
            Int32 poradi = findPoradiInRow(DBRow);
            DBRow = myDB.getVracenoLine(poradi, DBRow);
            VraceneKarta vracKarta = new VraceneKarta(DBRow);
            vracKarta.setWinName("Vraceno");
            vracKarta.ShowDialog();
            DBRow = myDB.getVracenoLine(poradi, DBRow);
            reloadRow((myDataGridView.DataSource as DataTable), findIndex((myDataGridView.DataSource as DataTable), "poradi", poradi), DBRow);
        }
       
        
        public override void opravKartu(Hashtable DBRow)
        {
            Int32 poradi = findPoradiInRow(DBRow);
            DBRow = myDB.getVracenoLine(poradi, DBRow);
            VraceneKarta vracKarta = new VraceneKarta(DBRow, myDB, new tableItemExistDelgInt(myDB.tablePoskozenoItemExist), vKartaState.edit);
            vracKarta.setWinName("Vraceno");
            if (vracKarta.ShowDialog() == DialogResult.OK)
            {
                VraceneKarta.messager mesenger = vracKarta.getMesseger();
                Boolean updateIsOk = myDB.editNewLineVracene(mesenger.poradi, mesenger.jmeno, mesenger.prijmeni, mesenger.oscislo, mesenger.stredisko, mesenger.provoz, mesenger.nazev, mesenger.jk, mesenger.pocetKs, mesenger.rozmer, mesenger.csn, mesenger.cena, mesenger.datum, mesenger.zakazka, mesenger.konto);
                if (updateIsOk)
                {
                    // je potreba najit index v datove tabulce - po trideni neni schodny s indexem ve view
                    Int32 dataRowIndex = findIndex((myDataGridView.DataSource as DataTable), "poradi", mesenger.poradi);

                    if (dataRowIndex != -1)
                    {
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(1, mesenger.nazev);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(2, mesenger.jk);

                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(3, mesenger.pocetKs);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(4, mesenger.rozmer);

                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(5, mesenger.csn);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(6, mesenger.cena);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(7, mesenger.datum);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(8, mesenger.zakazka);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(9, mesenger.konto);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(10, mesenger.prijmeni);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(11, mesenger.jmeno);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(12, mesenger.oscislo);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(13, mesenger.stredisko);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(14, mesenger.provoz);

                        myDataGridView.Refresh();
                    }
                }
                else
                {
                    MessageBox.Show("Opravení karty se nezdařilo.");
                }

            }
            else
            {
                Hashtable newDBRow = null;
                newDBRow = myDB.getVracenoLine(poradi, newDBRow);
                reloadRow((myDataGridView.DataSource as DataTable), findIndex((myDataGridView.DataSource as DataTable), "poradi", poradi), newDBRow);
            }
        }

        public override string preferovanySloupec()
        {
            return "nazev";
        }
    }



    class detailOsoby : detail

    {

        // DBRow      Tabulka      Sklad Karta

        // prijmeni   Prijmeni     TextBoxJmeno
        // jmeno      Jmeno        TextBoxJmeno
        // oscislo    Osobni cislo TextBoxOsCislo
        // odeleni    Provoz       TextBoxOddeleni
        // stredisko  Stredisko    TextBoxStredisko
        // pracoviste Pracoviste   TextBoxPracoviste
        // cisznamky  CisloZnamky  TextBoxCisZnamky
        // ulice      Ulice        TextBoxUlice
        // psc        Psc          TextBoxPsc
        // mesto      Mesto        TextBoxMesto
        // telhome    Tel. domu    TextBoxTelDomu
        // telzam     Tel. zamest. TextBoxTelZamest
        // poznamka   Poznamky     TextBoxPoznamka

        // pujsoub    

        public detailOsoby(vDatabase myDB, DataGridView myDataGridView)
            : base(myDB, myDataGridView)
        {
        }


        public override void zobrazKartu(Hashtable DBRow)
        {
            string osCislo = findOsCisloInRow(DBRow);
            DBRow = myDB.getOsobyLine(osCislo, DBRow);
            PracovniciKarta pracKarta = new PracovniciKarta(DBRow, myDB);
            pracKarta.ShowDialog();

            DBRow = myDB.getOsobyLine(osCislo, DBRow);
            reloadRow((myDataGridView.DataSource as DataTable), findIndex((myDataGridView.DataSource as DataTable), "oscislo", osCislo), DBRow);


        }

        public override void pridejKartu()
        {
            // zalozeni nove skladove karty
            if ((myDB != null) && (myDB.DBIsOpened()))
            {
                PracovniciKarta pracKarta = new PracovniciKarta(myDB);
                if (pracKarta.ShowDialog() == DialogResult.OK)
                {

                    PracovniciKarta.messager mesenger = pracKarta.getMesseger();
                    Int32 stav = myDB.addNewLineOsoby(mesenger.prijmeni, mesenger.jmeno, mesenger.ulice, mesenger.mesto, mesenger.psc, mesenger.telHome, mesenger.oscislo, mesenger.stredisko, mesenger.cisZnamky, mesenger.oddeleni, mesenger.pracoviste, mesenger.telZam, mesenger.poznamka);
                    if (stav != -1)
                    {
                        (myDataGridView.DataSource as DataTable).Rows.Add(mesenger.prijmeni, mesenger.jmeno, mesenger.oscislo, mesenger.oddeleni, mesenger.stredisko, mesenger.pracoviste, mesenger.cisZnamky, mesenger.ulice,mesenger.psc, mesenger.mesto, mesenger.telHome, mesenger.telZam, mesenger.poznamka);
                        int counter = myDataGridView.Rows.Count -1;

                        myDataGridView.FirstDisplayedScrollingRowIndex = myDataGridView.Rows[counter].Index;
                        myDataGridView.Refresh();
                        myDataGridView.CurrentCell = myDataGridView.Rows[counter].Cells[1];
                        myDataGridView.Rows[counter].Selected = true;
                    }


                }
            }
        }


        public override void opravKartu(Hashtable DBRow)
        {
            if ((myDB != null) && (myDB.DBIsOpened()))
            {
                string osCislo = findOsCisloInRow(DBRow);
                DBRow = myDB.getOsobyLine(osCislo, DBRow);
                PracovniciKarta pracKarta = new PracovniciKarta(DBRow, myDB, uKartaState.edit);
                if (pracKarta.ShowDialog() == DialogResult.OK)
                {
                    PracovniciKarta.messager mesenger = pracKarta.getMesseger();
                    Boolean updateIsOk = myDB.editNewLineOsoby(mesenger.prijmeni, mesenger.jmeno, mesenger.ulice, mesenger.mesto,
                                                 mesenger.psc, mesenger.telHome, mesenger.oscislo, mesenger.stredisko,
                                                 mesenger.cisZnamky, mesenger.oddeleni, mesenger.pracoviste, mesenger.telZam, mesenger.poznamka);
                    if (updateIsOk)
                    {
                        // je potreba najit index v datove tabulce - po trideni neni schodny s indexem ve view

                        Int32 dataRowIndex = findIndex((myDataGridView.DataSource as DataTable), "oscislo", mesenger.oscislo);

                        if (dataRowIndex != -1)
                        {
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(0, mesenger.prijmeni);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(1, mesenger.jmeno);

                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(2, mesenger.oscislo);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(3, mesenger.oddeleni);

                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(4, mesenger.stredisko);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(5, mesenger.pracoviste);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(6, mesenger.cisZnamky);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(7, mesenger.ulice);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(8, mesenger.psc);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(9, mesenger.mesto);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(10, mesenger.telHome);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(11, mesenger.telZam);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField(12, mesenger.poznamka);

                            myDataGridView.Refresh();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Opravení karty se nezdařilo.");
                    }
                }
                else
                {
                    Hashtable newDBRow = null;
                    newDBRow = myDB.getOsobyLine(osCislo, newDBRow);
                    reloadRow((myDataGridView.DataSource as DataTable), findIndex((myDataGridView.DataSource as DataTable), "oscislo", osCislo), newDBRow);
                }

            }
    }

        public override void Zapujceno(Hashtable DBRow)
        {
            if ((myDB != null) && (myDB.DBIsOpened()))
            {
                string osCislo = Convert.ToString(DBRow["oscislo"]);

                ZapujceneNaradiKarta zapujcKarta = new ZapujceneNaradiKarta(osCislo, myDB);// (DBRow, myDataBase, uKartaState.edit);
                zapujcKarta.ShowDialog();
            }
        }
        public override string preferovanySloupec()
        {
            return "prijmeni";
        }

    }

    class detailOsobyZapujcNaradi : detail
        {

        public detailOsobyZapujcNaradi(vDatabase myDB, DataGridView myDataGridView)
            : base(myDB, myDataGridView)
        {
        }



        public override void zobrazKartu(Hashtable DBRow)
            {
                if ((myDB != null) && (myDB.DBIsOpened()))
                {
                    string osCislo = findOsCisloInRow(DBRow);
                    DBRow = myDB.getOsobyLine(osCislo, DBRow);
                    ZapujceneNaradiKarta zapujcKarta = new ZapujceneNaradiKarta(osCislo, myDB);// (DBRow, myDataBase, uKartaState.edit);
                    zapujcKarta.ShowDialog();
                    DBRow = myDB.getOsobyLine(osCislo, DBRow);
                    reloadRow((myDataGridView.DataSource as DataTable), findIndex((myDataGridView.DataSource as DataTable), "oscislo", osCislo), DBRow);
                }
            }


            public override void Zapujceno(Hashtable DBRow)
            {
                if ((myDB != null) && (myDB.DBIsOpened()))
                {
                    string osCislo = Convert.ToString(DBRow["oscislo"]);
                    ZapujceneNaradiKarta zapujcKarta = new ZapujceneNaradiKarta(osCislo, myDB);// (DBRow, myDataBase, uKartaState.edit);
                    zapujcKarta.ShowDialog();
                }
            }

            public override string preferovanySloupec()
            {
                return "prijmeni";
            }

        }


}
