using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;



namespace Vydejna
{
    public abstract class detail
    {

       public class codeOfPermissions
        {
            public Int32 showEnableCode;
            public Int32 addEnableCode;
            public Int32 editEnableCode;
            public Int32 editChangeMarkCode;
            public Int32 deleteEnableCode;
            public Int32 printEnableCode;
            public Int32 prijemEnableCode;
            public Int32 poskozeniEnableCode;
            public Int32 pujcEnableCode;
            public Int32 vracEnableCode;

            public codeOfPermissions()
            {
                init();
            }

            public void init()
            {
                showEnableCode = -1;
                addEnableCode = -1;
                editEnableCode = -1;
                editChangeMarkCode = -1;
                deleteEnableCode = -1;
                printEnableCode = -1;
                prijemEnableCode = -1;
                poskozeniEnableCode = -1;
                pujcEnableCode = -1;
                vracEnableCode = -1;
            }

        }

        public codeOfPermissions myPermissions;
        public vDatabase myDB;
        public DataGridView myDataGridView;
        private Prohledavani searchWindow;


        public detail(vDatabase myDB, DataGridView myDataGridView)
        {
            this.myDB = myDB;
            this.myDataGridView = myDataGridView;
            searchWindow = null;
            setColumnIndex();
            setColumnWidth();
            myPermissions = new codeOfPermissions();
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
/// Najde v datove tabulce cislo radku jejiz sloupec NAME ma hodnotu value
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

/// <summary>
/// Odstrani polozku z datove tabulky a z pohledu
/// </summary>
/// <param name="poradi">Ukazatel na zaznam v databazi</param>

         public void removeViewSelectedRow(Int32 poradi)
        {
            Int32 counter = myDataGridView.Rows.Count;
            if (counter > 0)
            {
                counter--; // ukazuje na posledni prvek
                Int32 dataRowIndex = detail.findIndex(myDataGridView.DataSource as DataTable, "poradi", poradi);
                Int32 nextIndexAfterSelected = myDataGridView.SelectedRows[0].Index;

                (myDataGridView.DataSource as DataTable).Rows.RemoveAt(dataRowIndex);
                counter--; // ukazatel na posledni ... -1 neni zadna

                if (counter > -1) // neni zadna dalsi polozka
                {
                    if (nextIndexAfterSelected > counter) nextIndexAfterSelected = counter;
                    myDataGridView.FirstDisplayedScrollingRowIndex = myDataGridView.Rows[nextIndexAfterSelected].Index;
                    myDataGridView.Refresh();
                    myDataGridView.CurrentCell = myDataGridView.Rows[nextIndexAfterSelected].Cells[1];
                    myDataGridView.Rows[nextIndexAfterSelected].Selected = true;
                }
            }

        }

         /// <summary>
         /// Odstrani polozku z datove tabulky a z pohledu
         /// </summary>
         /// <param name="poradi">Osobni cislo uyivatele</param>

         public void removeViewSelectedRow(string osCislo)
         {
             Int32 counter = myDataGridView.Rows.Count;
             if (counter > 0)
             {
                 counter--; // ukazuje na posledni prvek
                 Int32 dataRowIndex = detail.findIndex(myDataGridView.DataSource as DataTable, "oscislo", osCislo);
                 Int32 nextIndexAfterSelected = myDataGridView.SelectedRows[0].Index;

                 (myDataGridView.DataSource as DataTable).Rows.RemoveAt(dataRowIndex);
                 counter--; // ukazatel na posledni ... -1 neni zadna

                 if (counter > -1) // neni zadna dalsi polozka
                 {
                     if (nextIndexAfterSelected > counter) nextIndexAfterSelected = counter;
                     myDataGridView.FirstDisplayedScrollingRowIndex = myDataGridView.Rows[nextIndexAfterSelected].Index;
                     myDataGridView.Refresh();
                     myDataGridView.CurrentCell = myDataGridView.Rows[nextIndexAfterSelected].Cells[1];
                     myDataGridView.Rows[nextIndexAfterSelected].Selected = true;
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

        public virtual void zmenZnacku(Hashtable DBRow)
        {
            MessageBox.Show("Není implementováno.");
        }

        public virtual void vytiskniKartu(Hashtable DBRow)
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


        public virtual void OpravChyby(Hashtable DBRow)
        {
            MessageBox.Show("Není implementováno.");
        }



        public virtual string jmenoTabulky()
        {
            return "";
        }

        public virtual string preferovanySloupec()
        {
            return "";
        }

        public virtual void setColumnIndex()
        {
           Hashtable DBTableInfo = ConfigReg.loadSettingWindowTableColumnIndex("MAIN", this.jmenoTabulky());
           if (DBTableInfo != null)
           {
               SortedDictionary< int, string> dict = new SortedDictionary<int, string>();

              // naplnime setrideny seznam  
              foreach (DictionaryEntry item in DBTableInfo)
              {
                  if (!(dict.ContainsKey(Convert.ToInt32(item.Value))))
                  {
                      dict.Add(Convert.ToInt32(item.Value), Convert.ToString(item.Key));
                  }
              }
              // upravime index podle setrideneho seznamu
              foreach (var sortItem in dict)
              {
                  try
                  {
                      myDataGridView.Columns[sortItem.Value.ToString()].DisplayIndex = Convert.ToInt32(sortItem.Key);
                  }
                  catch { }
              }
           }
        }
            


        public virtual void setColumnWidth()
        {
           Hashtable DBTableInfo = ConfigReg.loadSettingWindowTableColumnWidth("MAIN", this.jmenoTabulky());
           if (DBTableInfo != null)
           {

               for (Int32 i = 0; i < myDataGridView.Columns.Count; i++)
//               for (Int32 i = myDataGridView.Columns.Count-1; i > -1; i--)
                   {
                   string myColumnName = myDataGridView.Columns[i].Name;
                   if (DBTableInfo.ContainsKey(myColumnName))
                   {
                       try
                       {
                           myDataGridView.Columns[i].Width = Convert.ToInt32(DBTableInfo[myColumnName]);
                           myDataGridView.Columns[i].MinimumWidth = Convert.ToInt32(DBTableInfo[myColumnName]);
                       }
                       catch { }
                   }
               }
               for (Int32 i = 0; i < myDataGridView.Columns.Count; i++)
               {
                   myDataGridView.Columns[i].MinimumWidth = 10;
               }


           }
        }


        public virtual void NastaveniHledani(Int32 x, Int32 y)
        {
            if (searchWindow == null)
            {
                searchWindow = new Prohledavani(myDataGridView, preferovanySloupec(),"MAIN",jmenoTabulky());
                searchWindow.StartPosition = FormStartPosition.Manual;
            }

            if (x > (Screen.PrimaryScreen.Bounds.Width - searchWindow.Width))
            {
                x = Screen.PrimaryScreen.Bounds.Width - searchWindow.Width;
            }
            if (x < 0) {x = 0;}

            if (y > (Screen.PrimaryScreen.Bounds.Height - searchWindow.Height))
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
            myPermissions.init();
        }


        public override void setColumnWidth()
        {
        }

        public override void HledejDalsi(Int32 x, Int32 y)
        {
        }

    }
    
//-------------------------------------------------------------------------------//    
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
            myPermissions.showEnableCode = (Int32)permCode.Nar;
            myPermissions.addEnableCode = (Int32)permCode.NarAdd;
            myPermissions.editEnableCode = (Int32)permCode.NarEd;
            myPermissions.editChangeMarkCode = (Int32)permCode.NarEdM;
            myPermissions.deleteEnableCode = (Int32)permCode.NarDel;
            myPermissions.printEnableCode = (Int32)permCode.NarPrint;

            myPermissions.prijemEnableCode = (Int32)permCode.NarPrijem;
            myPermissions.poskozeniEnableCode = (Int32)permCode.NarPosk;
        }

        public override void zobrazKartu(Hashtable DBRow) 
            
        {
            if ((myDB != null) && (myDB.DBIsOpened()))
            {
                Int32 poradi = findPoradiInRow(DBRow);
                if (poradi != 0)
                {
                    DBRow = myDB.getNaradiLine(poradi, DBRow);

                    UzivatelData ud = UzivatelData.makeInstance();
                    SkladovaKarta sklKarta = new SkladovaKarta(myDB, DBRow, findPoradiInRow(DBRow), new tableItemExistDelgStr(myDB.tableNaradiItemExist), myDataGridView.Font, true);
                    sklKarta.setWinName("Skladová karta");
//                    sklKarta.Font = myDataGridView.Font;
                    sklKarta.ShowDialog();

                    DBRow = myDB.getNaradiLine(poradi, DBRow);
                    reloadRow((myDataGridView.DataSource as DataTable), findIndex((myDataGridView.DataSource as DataTable), "poradi", poradi), DBRow);
                }
            }
        }

        public override void pridejKartu()
        {
            // zalozeni nove skladove karty
            if ((myDB != null) && (myDB.DBIsOpened()))
            {
                SkladovaKarta sklKarta = new SkladovaKarta(myDB, new tableItemExistDelgStr(myDB.tableNaradiItemExist), myDataGridView.Font);
                sklKarta.setWinName("Skladová karta");
//                sklKarta.Font = myDataGridView.Font;
                if (sklKarta.ShowDialog() == DialogResult.OK)
                {

                    SkladovaKarta.messager mesenger = sklKarta.getMesseger();


                    Int32 poradi = myDB.addNewLineNaradi(mesenger.nazev, mesenger.jk, mesenger.csn, mesenger.din,
                                   mesenger.vyrobce, mesenger.cenaKs, mesenger.poznamka, mesenger.minStav,
                                   mesenger.ucetCena, mesenger.ucetStav, mesenger.ucetStav, mesenger.rozmer,
                                   mesenger.ucet, mesenger.ucetCenaKs, new DateTime(0));
                    if (poradi != -1)
                    {

                        //                        (myDataGridView.DataSource as DataTable).Rows.Add(poradi, "", mesenger.nazev, mesenger.jk, mesenger.ucetStav, mesenger.ucet, mesenger.csn, mesenger.din, mesenger.vyrobce, mesenger.rozmer, 0, mesenger.cenaKs, mesenger.ucetCena, mesenger.minStav, mesenger.poznamka, mesenger.ucetCenaKs);
                        //
                        DataRow newRow = (myDataGridView.DataSource as DataTable).NewRow();
                        newRow["poradi"] = poradi;
                        newRow["nazev"] = mesenger.nazev;
                        newRow["jk"] = mesenger.jk;
                        newRow["fyzstav"] = mesenger.fyzStav;
                        newRow["analucet"] = mesenger.ucet;
                        newRow["normacsn"] = mesenger.csn;
                        newRow["normadin"] = mesenger.din;
                        newRow["vyrobce"] = mesenger.vyrobce;
                        newRow["rozmer"] = mesenger.rozmer;
                        newRow["ucetstav"] = mesenger.ucetStav;
                        newRow["cena"] = mesenger.cenaKs;
                        newRow["celkcena"] = mesenger.ucetCena;
                        newRow["minimum"] = mesenger.minStav;
                        newRow["poznamka"] = mesenger.poznamka;
                        newRow["ucetkscen"] = mesenger.ucetCenaKs;
                        (myDataGridView.DataSource as DataTable).Rows.Add(newRow);
                        //
                        int counter = myDataGridView.Rows.Count - 1;

                        myDataGridView.FirstDisplayedScrollingRowIndex = myDataGridView.Rows[counter].Index;
                        myDataGridView.Refresh();

                        myDataGridView.CurrentCell = myDataGridView.Rows[counter].Cells[1];
                        myDataGridView.Rows[counter].Selected = true;

                    }
                    else
                    {
                        MessageBox.Show("Nepodařilo se přidat položku.");
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

                UzivatelData ud = UzivatelData.makeInstance();
                SkladovaKarta.permissonsData skladEditPerm = new SkladovaKarta.permissonsData(ud.userHasAccessRights((Int32)permCode.NarEdNaz),
                                                                ud.userHasAccessRights((Int32)permCode.NarEdJK), 
                                                                ud.userHasAccessRights((Int32)permCode.NarEdCenaKs),
                                                                ud.userHasAccessRights((Int32)permCode.NarEdUcCenaKs),
                                                                ud.userHasAccessRights((Int32)permCode.NarEdUcCena),
                                                                ud.userHasAccessRights((Int32)permCode.NarEdMin),
                                                                ud.userHasAccessRights((Int32)permCode.NarEdFyStav),
                                                                ud.userHasAccessRights((Int32)permCode.NarEdUcStav));
                SkladovaKarta sklKarta = new SkladovaKarta(myDB, DBRow, poradi, new tableItemExistDelgStr(myDB.tableNaradiItemExist), myDataGridView.Font, true, sKartaState.edit,skladEditPerm);
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
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("nazev", mesenger.nazev);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("jk", mesenger.jk);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("fyzstav", mesenger.fyzStav);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("analucet", mesenger.ucet);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("normacsn", mesenger.csn);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("normadin", mesenger.din);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("vyrobce", mesenger.vyrobce);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("rozmer", mesenger.rozmer);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("ucetstav", mesenger.ucetStav);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("cena", mesenger.cenaKs);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("celkcena", mesenger.ucetCena);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("minimum", mesenger.minStav);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("poznamka", mesenger.poznamka);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("ucetkscen", mesenger.ucetCenaKs);

                            myDataGridView.Refresh();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Opravení karty se nezdařilo.");
                        Hashtable newDBRow = null;
                        newDBRow = myDB.getNaradiLine(poradi, newDBRow);
                        reloadRow((myDataGridView.DataSource as DataTable), findIndex((myDataGridView.DataSource as DataTable), "poradi", poradi), newDBRow);
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


        public override void zmenZnacku(Hashtable DBRow)
        {
            Int32 poradi = Convert.ToInt32(DBRow["poradi"]);
            ZnackaZmena zmenaZnacky = new ZnackaZmena(myDB, poradi);
            if (zmenaZnacky.ShowDialog() == DialogResult.OK)
            {
                Int32 updateIsOk = myDB.editNewLineZnackaNaradi(poradi, zmenaZnacky.getMark());
                // obnovime zobrazeni
                if (updateIsOk != 0)
                {
                    MessageBox.Show("Opravení karty se nezdařilo.");
                }
            }
            Hashtable newDBRow = null;
            newDBRow = myDB.getNaradiLine(poradi, newDBRow);
            reloadRow((myDataGridView.DataSource as DataTable), findIndex((myDataGridView.DataSource as DataTable), "poradi", poradi), newDBRow);
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
                        removeViewSelectedRow(poradi); 
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


        public override void vytiskniKartu(Hashtable DBRow)
        {
            TiskNaradi myTisk = new TiskNaradi(myDB, DBRow);
        }




        public override void Prijem(Hashtable DBRow)
        {
            if ((myDB != null) && (myDB.DBIsOpened()))
            {
                PrijemkaMaterialu prijemka = new PrijemkaMaterialu(DBRow, myDB,myDataGridView.Font);
//                prijemka.Font = myDataGridView.Font;
                if (prijemka.ShowDialog() == DialogResult.OK)
                {
                    PrijemkaMaterialu.messager mesenger = prijemka.getMesseger();
                    if (myDB.addNewLineZmenyAndPrijmuto(mesenger.poradi, mesenger.jk, mesenger.datum, mesenger.pocetKs, mesenger.cena, mesenger.poznamka) < 0)
                    {
                        MessageBox.Show("Příjem materialu se nezdařil. Lituji.");
                    }
                    else
                    {
                        Int32 dataRowIndex = findIndex((myDataGridView.DataSource as DataTable), "poradi", mesenger.poradi);
                        if (dataRowIndex != -1)
                        {
                            // opravime tabulku
                            Hashtable DBBackRow = myDB.getNaradiZmenyLine(mesenger.poradi, null);
                            if (DBBackRow != null)
                            {
                                (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("cena", Convert.ToDouble(DBBackRow["cena"]));
                                (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("celkcena", Convert.ToDouble(DBBackRow["celkcena"]));
                                Int32 fyzStav = 0;
                                Int32 ucetStav = 0;

                                if (DBBackRow.ContainsKey("fyzstav") && DBBackRow.ContainsKey("zmeny_zustatek"))
                                {
                                    fyzStav = Convert.ToInt32(DBBackRow["fyzstav"]);
                                    int zustatek = Convert.ToInt32(DBBackRow["zmeny_zustatek"]); // tabulka zmeny sloupec zustatek
                                    if (zustatek  != fyzStav) MessageBox.Show("Pozor! Patrně nesouhlasí stav karet a fyzicky stav položky.");
                                }
                                if (DBBackRow.ContainsKey("fyzstav"))
                                {
                                    fyzStav = Convert.ToInt32(DBBackRow["fyzstav"]);
                                    (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("fyzstav", fyzStav);
                                }
                                if (DBBackRow.ContainsKey("ucetstav"))
                                {
                                    ucetStav = Convert.ToInt32(DBBackRow["ucetstav"]);
                                    (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("ucetstav", ucetStav);
                                }
//  ucetni stav je pocet ks na stavu /pujcenych a na vydejn/ a fyzicky je pocet ks na vydejne
//                                if (fyzStav != ucetStav) MessageBox.Show("Pozor! Účetni a fyzický stav nesouhlasí.");
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
                Poskozenka poskozenka = new Poskozenka(DBRow, myDB, myDataGridView.Font);
                if (poskozenka.ShowDialog() == DialogResult.OK)
                {
                    Poskozenka.messager mesenger = poskozenka.getMesseger();
                    int errCode;
                    if ((errCode = myDB.addNewLineZmenyAndPoskozeno(mesenger.poradi, mesenger.datum, mesenger.pocetKs, mesenger.poznamka, mesenger.osCislo, mesenger.jmeno, mesenger.prijmeni,
                                                               mesenger.stredisko, mesenger.provoz, mesenger.konto, mesenger.cena, mesenger.celkCena, mesenger.cisZak )) < 0)
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
                            Hashtable DBBackRow = myDB.getNaradiZmenyLine(mesenger.poradi, null);
                            if (DBBackRow != null)
                            {
                                Int32 fyzStav = 0;
                                Int32 ucetStav = 0;

                                (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("celkcena", Convert.ToDouble(DBBackRow["celkcena"]));
                                if (DBBackRow.ContainsKey("fyzstav") && DBBackRow.ContainsKey("zmeny_zustatek"))
                                {
                                    fyzStav = Convert.ToInt32(DBBackRow["fyzstav"]);
                                    int zustatek = Convert.ToInt32(DBBackRow["zmeny_zustatek"]);
                                    if (zustatek != fyzStav) MessageBox.Show("Pozor! Patrně nesouhlasí stav karet a stav vydejny položky.");
                                }
                                if (DBBackRow.ContainsKey("fyzstav"))
                                {
                                    fyzStav = Convert.ToInt32(DBBackRow["fyzstav"]);
                                    (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("fyzstav", fyzStav);
                                }
                                if (DBBackRow.ContainsKey("ucetstav"))
                                {
                                    ucetStav = Convert.ToInt32(DBBackRow["ucetstav"]);
                                    (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("ucetstav", ucetStav);
                                }
                                if (DBBackRow.ContainsKey("fyzstav") && DBBackRow.ContainsKey("ucetstav"))
                                {
                                    if (ucetStav < fyzStav) MessageBox.Show("Pozor! Účetní stav je menší než stav výdejny.");
                                }

                            }
                        }
                    }
                }
            }
        }



        public override void OpravChyby(Hashtable DBRow)
        {
            if ((myDB != null) && (myDB.DBIsOpened()))
            {
                Int32 poradi = findPoradiInRow(DBRow);
                DBRow = myDB.getNaradiLine(poradi, DBRow); // aktualizujeme data

                OpravaKarta opravKarta = new OpravaKarta(myDB, DBRow, poradi, myDataGridView.Font);


                if (opravKarta.ShowDialog() == DialogResult.OK)
                {
                   Int32 errCode = myDB.correctNaradiZmeny(poradi, opravKarta.oldFyzStav, opravKarta.fyzStav, opravKarta.oldUcetStav, opravKarta.ucetStav, opravKarta.getZmenyTab());
                   if (errCode < 0)
                   {

                       if (errCode == -8)
                       {
                           MessageBox.Show("Lituji. Stavy materialz byly změněny - změna z jiného místa? Nemohu uskutečnit opravu.");
                       }
                       if (errCode == -7)
                       {
                           MessageBox.Show("Lituji. Toto nářadí neexistuje - změna z jiného místa? Nemohu uskutečnit opravu.");
                       }
                       if (errCode == -6)
                       {
                           MessageBox.Show("Lituji. Byly změněny operace s nařadím - změna z jiného místa? Nemohu uskutečnit opravu.");
                       }
                       if (errCode == -5)
                       {
                           MessageBox.Show("Lituji. Byla změněna poslení operace s nařadím - změna z jiného místa? Nemohu uskutečnit opravu.");
                       }
                       if (errCode == -4)
                       {
                           MessageBox.Show("Lituji. Byl změněn počet operaci s nařadím - změna z jiného místa? Nemohu uskutečnit opravu.");
                       }
                       if (errCode == -3)
                       {
                           MessageBox.Show("Lituji. S nařadím nebyla provedena žádná operace, Nemohu uskutečnit opravu.");
                       }
                       if (errCode == -2)
                       {
                           MessageBox.Show("Lituji. V tabulce změn nenížádná operace, Nemohu uskutečnit opravu.");
                       }
                       if (errCode == -1)
                       {
                           MessageBox.Show("Uskutečnění opravy dat se nezdařilo. Lituji.");
                       }


                   }
                }
                Hashtable newDBRow = null;
                newDBRow = myDB.getNaradiLine(poradi, newDBRow);
                reloadRow((myDataGridView.DataSource as DataTable), findIndex((myDataGridView.DataSource as DataTable), "poradi", poradi), newDBRow);

            }
        }


        public override string preferovanySloupec()
        {
            return "jk";
        }


        public override string jmenoTabulky()
        {
            return "naradi";
        }


    }

//-------------------------------------------------------------//
    class detailZruseno : detail
    {
        public detailZruseno(vDatabase myDB, DataGridView myDataGridView)
            : base(myDB, myDataGridView)
        {
            myPermissions.showEnableCode = (Int32)permCode.ZNar;
            myPermissions.editEnableCode = (Int32)permCode.ZNarEd;
            myPermissions.deleteEnableCode = (Int32)permCode.ZNarDel;
            myPermissions.printEnableCode = (Int32)permCode.ZNarPrint;
        }

        public override void zobrazKartu(Hashtable DBRow)
        {
            if ((myDB != null) && (myDB.DBIsOpened()))
            {

                Int32 poradi = findPoradiInRow(DBRow);
                DBRow = myDB.getZrusenoLine(poradi, DBRow);

                UzivatelData ud = UzivatelData.makeInstance();
                SkladovaKarta sklKarta = new SkladovaKarta(myDB, DBRow, findPoradiInRow(DBRow), new tableItemExistDelgStr(myDB.tableZrusenoItemExist), myDataGridView.Font,false);
//                sklKarta.Font = myDataGridView.Font;
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
                UzivatelData ud = UzivatelData.makeInstance();
                Int32 poradi = findPoradiInRow(DBRow);
                DBRow = myDB.getZrusenoLine(poradi, DBRow);
                SkladovaKarta sklKarta = new SkladovaKarta(myDB, DBRow, findPoradiInRow(DBRow), new tableItemExistDelgStr(myDB.tableZrusenoItemExist), myDataGridView.Font, false, sKartaState.edit);
//                sklKarta.Font =C;
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
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("nazev", mesenger.nazev);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("jk", mesenger.jk);

                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("ucetstav", mesenger.ucetStav);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("analucet", mesenger.ucet);

                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("normacsn", mesenger.csn);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("normadin", mesenger.din);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("vyrobce", mesenger.vyrobce);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("rozmer", mesenger.rozmer);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("fyzstav", mesenger.fyzStav);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("cena", mesenger.cenaKs);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("celkcena", mesenger.ucetCena);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("minimum", mesenger.minStav);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("poznamka", mesenger.poznamka);

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

                if (myDB.deleteLinePoskozene(poradi))
                {
                    removeViewSelectedRow(poradi);
                }
                else
                {
                    MessageBox.Show("Zrušení karty se nezdařilo.");
                }
            }
        }


        public override void vytiskniKartu(Hashtable DBRow)
        {
            TiskNaradi myTisk = new TiskNaradi(myDB, DBRow,true);
        }


        public override string preferovanySloupec()
        {
            return "nazev";
        }

        public override string jmenoTabulky()
        {
            return "zruseno";
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
            myPermissions.showEnableCode = (Int32)permCode.PNar;
            myPermissions.editEnableCode = (Int32)permCode.PNarEd;
            myPermissions.deleteEnableCode = (Int32)permCode.PNarDel;
        }


        public override void zobrazKartu(Hashtable DBRow)
        {
            Int32 poradi = findPoradiInRow(DBRow);
            DBRow = myDB.getPoskozenoLine(poradi, DBRow);

            VraceneKarta poskozKarta = new VraceneKarta(DBRow,myDataGridView.Font);
            poskozKarta.setWinName("Poškozeno");
//            poskozKarta.Font = myDataGridView.Font;
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
                VraceneKarta poskozKarta = new VraceneKarta(DBRow, myDB, new tableItemExistDelgInt(myDB.tablePoskozenoItemExist), myDataGridView.Font, vKartaState.edit);
//                poskozKarta.Font = myDataGridView.Font;
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
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("nazev", mesenger.nazev);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("jk", mesenger.jk);

                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("pocetks", mesenger.pocetKs);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("rozmer", mesenger.rozmer);

                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("csn", mesenger.csn);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("cena", mesenger.cena);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("datum", mesenger.datum);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("vyrobek", mesenger.zakazka);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("konto", mesenger.konto);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("jmeno", mesenger.prijmeni);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("krjmeno", mesenger.jmeno);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("oscislo", mesenger.oscislo);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("dilna", mesenger.stredisko);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("pracoviste", mesenger.provoz);

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



        public override void zrusKartu(Hashtable DBRow)
        {
            if (MessageBox.Show("Opravdu chcete zrušit záznam o poškození nářadí ?", "Zrušení záznamu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (MessageBox.Show("Jste si opravdu jisti, že chcete zrušit záznam o poškození nářadí ?", "Zrušení záznamu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // zrusime kartu
                    Int32 poradi = Convert.ToInt32(DBRow["poradi"]);

                    if (myDB.deleteLinePoskozene(poradi))
                    {
                        removeViewSelectedRow(poradi);
                    }
                    else
                    {
                        MessageBox.Show("Zrušení zaznamu o poškození se nezdařilo.");
                    }
                }
            }

        }



        public override string preferovanySloupec()
        {
            return "nazev";
        }

        public override string jmenoTabulky()
        {
            return "poskozeno";
        }

    }



    class detailVraceno : detail
    {

        public detailVraceno(vDatabase myDB, DataGridView myDataGridView, System.Drawing.Font myFont)
            : base(myDB, myDataGridView)
        {
            myPermissions.showEnableCode = (Int32)permCode.VNar;
            myPermissions.editEnableCode = (Int32)permCode.VNarEd;
            myPermissions.deleteEnableCode = (Int32)permCode.VNarDel;
        }


        public override void zobrazKartu(Hashtable DBRow)
        {
            Int32 poradi = findPoradiInRow(DBRow);
            DBRow = myDB.getVracenoLine(poradi, DBRow);
            VraceneKarta vracKarta = new VraceneKarta(DBRow, myDataGridView.Font);
//            vracKarta.Font = myDataGridView.Font;
            vracKarta.setWinName("Vraceno");
            vracKarta.ShowDialog();
            DBRow = myDB.getVracenoLine(poradi, DBRow);
            reloadRow((myDataGridView.DataSource as DataTable), findIndex((myDataGridView.DataSource as DataTable), "poradi", poradi), DBRow);
        }
       
        
        public override void opravKartu(Hashtable DBRow)
        {
            Int32 poradi = findPoradiInRow(DBRow);
            DBRow = myDB.getVracenoLine(poradi, DBRow);
            VraceneKarta vracKarta = new VraceneKarta(DBRow, myDB, new tableItemExistDelgInt(myDB.tablePoskozenoItemExist), myDataGridView.Font, vKartaState.edit);
//            vracKarta.Font = myDataGridView.Font;
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
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("naradi", mesenger.nazev);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("jk", mesenger.jk);

                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("pocetks", mesenger.pocetKs);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("rozmer", mesenger.rozmer);

                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("csn", mesenger.csn);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("cena", mesenger.cena);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("datum", mesenger.datum);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("vyrobek", mesenger.zakazka);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("konto", mesenger.konto);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("jmeno", mesenger.prijmeni);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("krjmeno", mesenger.jmeno);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("oscislo", mesenger.oscislo);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("dilna", mesenger.stredisko);
                        (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("pracoviste", mesenger.provoz);

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


        public override void zrusKartu(Hashtable DBRow)
        {
            if (MessageBox.Show("Opravdu chcete zrušit záznam o vráceném nářadí ?", "Zrušení záznamu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (MessageBox.Show("Jste si opravdu jisti, že chcete zrušit záznam o vráceném nářadí ?", "Zrušení záznamu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // zrusime kartu
                    Int32 poradi = Convert.ToInt32(DBRow["poradi"]);

                    if (myDB.deleteLineVracene(poradi))
                    {
                        removeViewSelectedRow(poradi);
                    }
                    else
                    {
                        MessageBox.Show("Zrušení zaznamu o vrácení se nezdařilo.");
                    }
                }
            }

        }


        public override string preferovanySloupec()
        {
            return "nazev";
        }

        public override string jmenoTabulky()
        {
            return "vraceno";
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
            myPermissions.showEnableCode = (Int32)permCode.Prac;
            myPermissions.addEnableCode = (Int32)permCode.PracAdd;
            myPermissions.editEnableCode = (Int32)permCode.PracEd;
            myPermissions.deleteEnableCode = (Int32)permCode.PracDel;
            myPermissions.printEnableCode = (Int32)permCode.PracPrint;

            myPermissions.pujcEnableCode = (Int32)permCode.PracZapN;
            myPermissions.vracEnableCode = (Int32)permCode.PracVracN;
        }


        public override void zobrazKartu(Hashtable DBRow)
        {
            string osCislo = findOsCisloInRow(DBRow);
            DBRow = myDB.getOsobyLine(osCislo, DBRow);

            PracovniciKarta pracKarta = new PracovniciKarta(DBRow, myDB, myDataGridView.Font);
            pracKarta.ShowDialog();

            DBRow = myDB.getOsobyLine(osCislo, DBRow);
            reloadRow((myDataGridView.DataSource as DataTable), findIndex((myDataGridView.DataSource as DataTable), "oscislo", osCislo), DBRow);


        }

        public override void pridejKartu()
        {
            // zalozeni nove skladove karty
            if ((myDB != null) && (myDB.DBIsOpened()))
            {
                PracovniciKarta pracKarta = new PracovniciKarta(myDB, myDataGridView.Font);
                if (pracKarta.ShowDialog() == DialogResult.OK)
                {

                    PracovniciKarta.messager mesenger = pracKarta.getMesseger();
                    Int32 stav = myDB.addNewLineOsoby(mesenger.prijmeni, mesenger.jmeno, mesenger.ulice, mesenger.mesto, mesenger.psc, mesenger.telHome, mesenger.oscislo, mesenger.stredisko, mesenger.cisZnamky, mesenger.oddeleni, mesenger.pracoviste, mesenger.telZam, mesenger.poznamka);
                    if (stav != -1)
                    {
                        DataRow newRow = (myDataGridView.DataSource as DataTable).NewRow();
                        newRow["prijmeni"] = mesenger.prijmeni;
                        newRow["jmeno"] = mesenger.jmeno;
                        newRow["oscislo"] = mesenger.oscislo;
                        newRow["odeleni"] = mesenger.oddeleni;
                        newRow["stredisko"] = mesenger.stredisko;
                        newRow["pracoviste"] = mesenger.pracoviste;
                        newRow["cisznamky"] = mesenger.cisZnamky;
                        newRow["ulice"] = mesenger.ulice;
                        newRow["psc"] = mesenger.psc;
                        newRow["mesto"] = mesenger.mesto;
                        newRow["telhome"] = mesenger.telHome;
                        newRow["telzam"] = mesenger.telZam;
                        newRow["poznamka"] = mesenger.poznamka;
                        (myDataGridView.DataSource as DataTable).Rows.Add(newRow);
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
                PracovniciKarta pracKarta = new PracovniciKarta(DBRow, myDB, myDataGridView.Font, uKartaState.edit);
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
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("prijmeni", mesenger.prijmeni);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("jmeno", mesenger.jmeno);

                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("oscislo", mesenger.oscislo);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("odeleni", mesenger.oddeleni);

                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("stredisko", mesenger.stredisko);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("pracoviste", mesenger.pracoviste);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("cisznamky", mesenger.cisZnamky);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("ulice", mesenger.ulice);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("psc", mesenger.psc);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("mesto", mesenger.mesto);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("telhome", mesenger.telHome);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("telzam", mesenger.telZam);
                            (myDataGridView.DataSource as DataTable).Rows[dataRowIndex].SetField("poznamka", mesenger.poznamka);

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

                ZapujceneNaradiKarta zapujcKarta = new ZapujceneNaradiKarta(osCislo, myDB,myDataGridView.Font);// (DBRow, myDataBase, uKartaState.edit);
                zapujcKarta.ShowDialog();
            }
        }


        public override void vytiskniKartu(Hashtable DBRow)
        {
            TiskVypujcky myTisk = new TiskVypujcky(myDB, DBRow);
        }


        public override void zrusKartu(Hashtable DBRow)
        {
            string osCislo = findOsCisloInRow(DBRow);
            if (osCislo.Trim() != "")
            {
                if (!(myDB.tablePujcenoExistOnOsCislo(osCislo))) // hlavni tabulka zaznam o vypujce
                {
                    if (!(myDB.tablePoskozenoItemExistOnOsCislo(osCislo)))
                    {
                        if (!(myDB.tableVracenoItemExistOnOsCislo(osCislo)))
                        {
                            if (MessageBox.Show("Opravdu chcete zrušit kartu pracovníka ?", "Zrušení karty", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {

                                Int32 errorCode = myDB.deleteLineOsoby(osCislo);
                                if (errorCode == 0)
                                {
                                    removeViewSelectedRow(osCislo);
                                }
                                else
                                {
                                    switch (errorCode)
                                    {
                                        case -1: MessageBox.Show("Databaze není připojena.");
                                            break;
                                        case -2: MessageBox.Show("Chyba databaze.");
                                            break;
                                        case -3: MessageBox.Show("Pracovník má záznam v seznamu pujčeného nářadí, nelze jej zrušit.");
                                            break;
                                        case -4: MessageBox.Show("Pracovník má záznam v seznamu poškozeného nářadí, nelze jej zrušit.");
                                            break;
                                        case -5: MessageBox.Show("Pracovník má výpůjčeno nářadí, nelze jej zrušit.");
                                            break;
                                        case -6: MessageBox.Show("Pracovník již neexistuje, nelze jej proto zrušit.");
                                            break;
                                        default:
                                            MessageBox.Show("Smazání karty pracovníka se nezdařilo.");
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Pracovník má záznam v seznamu pujčeného nářadí, nelze jej zrušit.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Pracovník má záznam v seznamu poškozeného nářadí, nelze jej zrušit.");
                    }
                }
                else
                {
                    MessageBox.Show("Pracovník má výpůjčeno nářadí, nelze jej zrušit.");
                }
            }
        }



        public override string preferovanySloupec()
        {
            return "prijmeni";
        }

        public override string jmenoTabulky()
        {
            return "osoby";
        }

    }

    class detailOsobyZapujcNaradi : detailOsoby
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
                    ZapujceneNaradiKarta zapujcKarta = new ZapujceneNaradiKarta(osCislo, myDB, myDataGridView.Font);// (DBRow, myDataBase, uKartaState.edit);
                    zapujcKarta.ShowDialog();
                    DBRow = myDB.getOsobyLine(osCislo, DBRow);
                    reloadRow((myDataGridView.DataSource as DataTable), findIndex((myDataGridView.DataSource as DataTable), "oscislo", osCislo), DBRow);
                }
            }

        }


}
