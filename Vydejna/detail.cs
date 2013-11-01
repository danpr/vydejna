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

        public virtual void zobrazKartu(Hashtable DBRow, vDatabase myDataBase)
        {
        }

        public virtual void pridejKartu(vDatabase myDataBase, DataGridView myDataGridView)
        {
            MessageBox.Show("Není implementováno.");
        }

        public virtual void opravKartu(Hashtable DBRow, vDatabase myDataBase, DataGridView myDataGridView)
        {
            MessageBox.Show("Není implementováno.");
        }

        public virtual void zrusKartu(Hashtable DBRow, vDatabase myDataBase, DataGridView myDataGridView)
        {
            MessageBox.Show("Není implementováno.");
        }

        public virtual void Prijem(Hashtable DBRow, vDatabase myDataBase, DataGridView myDataGridView)
        {
            MessageBox.Show("Není implementováno.");
        }

        public virtual void Poskozeno(Hashtable DBRow, vDatabase myDataBase, DataGridView myDataGridView)
        {
            MessageBox.Show("Není implementováno.");
        }


        public virtual void Zapujceno(Hashtable DBRow, vDatabase myDataBase)
        {
            MessageBox.Show("Není implementováno.");
        }

    
    }

    class detailNone : detail
    {
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
         
        
        public override void zobrazKartu(Hashtable DBRow, vDatabase myDataBase) 
            
        {
            if ((myDataBase != null) && (myDataBase.DBIsOpened()))
            {
                SkladovaKarta sklKarta = new SkladovaKarta(DBRow, myDataBase, new tableItemExistDelgStr(myDataBase.tableNaradiItemExist));
                sklKarta.setWinName("Skladová karta");
                sklKarta.ShowDialog();
            }
        }

        public override void pridejKartu(vDatabase myDataBase, DataGridView myDataGridView)
        {
            // zalozeni nove skladove karty
            if ((myDataBase != null) && (myDataBase.DBIsOpened()))
            {
                SkladovaKarta sklKarta = new SkladovaKarta(myDataBase, new tableItemExistDelgStr(myDataBase.tableNaradiItemExist));
                sklKarta.setWinName("Skladová karta");
                if (sklKarta.ShowDialog() == DialogResult.OK)
                {

                    SkladovaKarta.messager mesenger = sklKarta.getMesseger();

                    Int32 poradi = myDataBase.addNewLineNaradi(mesenger.nazev, mesenger.jk, mesenger.csn, mesenger.din, mesenger.vyrobce, mesenger.cenaKs, mesenger.poznamka, mesenger.minStav, mesenger.ucetCena, mesenger.ucetStav, mesenger.ucetStav, mesenger.rozmer, mesenger.ucet, mesenger.ucetCenaKs, new DateTime(0));
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


        public override void opravKartu(Hashtable DBRow, vDatabase myDataBase, DataGridView myDataGridView)
        {
            if ((myDataBase != null) && (myDataBase.DBIsOpened()))
            {
                SkladovaKarta sklKarta = new SkladovaKarta(DBRow, myDataBase, new tableItemExistDelgStr(myDataBase.tableNaradiItemExist), sKartaState.edit);
                if (sklKarta.ShowDialog() == DialogResult.OK)
                {
                    SkladovaKarta.messager mesenger = sklKarta.getMesseger();
                    Boolean updateIsOk = myDataBase.editNewLineNaradi(mesenger.poradi ,mesenger.nazev, mesenger.jk, mesenger.csn, mesenger.din, mesenger.vyrobce, mesenger.cenaKs, mesenger.poznamka, mesenger.minStav, mesenger.ucetCena, mesenger.ucetStav, mesenger.fyzStav, mesenger.rozmer, mesenger.ucet, mesenger.ucetCenaKs);
                    if (updateIsOk)
                    {
                        // je potreba najit index v datove tabulce - po trideni neni schodny s indexem ve view
                        Int32 dataRowIndex = -1;
                        for (int x = 0; x < (myDataGridView.DataSource as DataTable).Rows.Count - 1; x++)
                        {
                            if (Convert.ToInt32((myDataGridView.DataSource as DataTable).Rows[x][0]) == mesenger.poradi)
                            {
                                dataRowIndex = x;
                                break;
                            }
                        }

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
                }
            }
        }


        public override void zrusKartu(Hashtable DBRow, vDatabase myDataBase, DataGridView myDataGridView)
        {
            if (MessageBox.Show("Opravdu chcete zrušit kartu ?", "Zrušení karty", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // zrusime kartu
                Int32 poradi = Convert.ToInt32(DBRow["poradi"]);

                if (myDataBase.moveNaraddiToNewKaret(poradi))
                {
                    // smazeme z obrazovky
                    // je potreba najit index v datove tabulce - po trideni neni schodny s indexem ve view
                    Int32 dataRowIndex = -1;
                    for (int x = 0; x < (myDataGridView.DataSource as DataTable).Rows.Count - 1; x++)
                    {
                        if (Convert.ToInt32((myDataGridView.DataSource as DataTable).Rows[x][0]) == poradi)
                        {
                            dataRowIndex = x;
                            break;
                        }
                    }
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



        public override void Prijem(Hashtable DBRow, vDatabase myDataBase, DataGridView myDataGridView)
        {
            if ((myDataBase != null) && (myDataBase.DBIsOpened()))
            {
                PrijemkaMaterialu prijemka = new PrijemkaMaterialu(DBRow, myDataBase);
                if (prijemka.ShowDialog() == DialogResult.OK)
                {
                    PrijemkaMaterialu.messager mesenger = prijemka.getMesseger();
                    if (myDataBase.addNewLineZmeny(mesenger.poradi, mesenger.jk, mesenger.datum, mesenger.pocetKs, 0, mesenger.poznamka) < 0)
                    {
                        MessageBox.Show("Příjem materialu se nezdařil. Lituji.");
                    }
                    else
                    {
                        Int32 dataRowIndex = -1;
                        for (int x = 0; x < (myDataGridView.DataSource as DataTable).Rows.Count - 1; x++)
                        {
                            if (Convert.ToInt32((myDataGridView.DataSource as DataTable).Rows[x][0]) == mesenger.poradi)
                            {
                                dataRowIndex = x;
                                break;
                            }
                        }
                        if (dataRowIndex != -1)
                        {
                            // opravime tabulku
                            Hashtable DBrow = myDataBase.getNaradiZmenyLine(mesenger.poradi, null);
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

        public override void Poskozeno(Hashtable DBRow, vDatabase myDataBase, DataGridView myDataGridView)
        {
            if ((myDataBase != null) && (myDataBase.DBIsOpened()))
            {
                Poskozenka poskozenka = new Poskozenka(DBRow, myDataBase);
                if (poskozenka.ShowDialog() == DialogResult.OK)
                {
                    Poskozenka.messager mesenger = poskozenka.getMesseger();

                    if (myDataBase.addNewLineZmenyAndPoskozeno(mesenger.poradi, mesenger.jk, mesenger.datum, mesenger.pocetKs, mesenger.poznamka, mesenger.osCislo, mesenger.jmeno, mesenger.prijmeni,
                                                               mesenger.stredisko, mesenger.provoz, mesenger.nazev, mesenger.rozmer, mesenger.konto, mesenger.cena, mesenger.celkCena, mesenger.csn, mesenger.cisZak ) < 0)
                    {
                        MessageBox.Show("Odepsání poškozeneho materialu se nezdařilo. Lituji.");
                    }
                    else
                    {
                        Int32 dataRowIndex = -1;
                        for (int x = 0; x < (myDataGridView.DataSource as DataTable).Rows.Count - 1; x++)
                        {
                            if (Convert.ToInt32((myDataGridView.DataSource as DataTable).Rows[x][0]) == mesenger.poradi)
                            {
                                dataRowIndex = x;
                                break;
                            }
                        }
                        if (dataRowIndex != -1)
                        {
                            // opravime tabulku
                            Hashtable DBrow = myDataBase.getNaradiZmenyLine(mesenger.poradi, null);
                            if (DBrow != null)
                            {
                                Int32 fyzStav = 0;
                                Int32 ucetStav = 0;

                                if (DBrow.ContainsKey("ucetstav") && DBrow.ContainsKey("zmeny_zustatek"))
                                {
                                    ucetStav = Convert.ToInt32(DBrow["ucetstav"]);
                                    int zustatek = Convert.ToInt32(DBrow["zmeny_zustatek"]);
                                    if (zustatek != ucetStav) MessageBox.Show("Pozor! Patrně nesouhlasí stav karet a učetní stav položky.");
                                }

                                if (DBrow.ContainsKey("fyzstav"))
                                {
                                    fyzStav = Convert.ToInt32(DBrow["fyzstav"]);
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


    }


    class detailZruseno : detail
    {
        public override void zobrazKartu(Hashtable DBRow, vDatabase myDataBase)
        {
            if ((myDataBase != null) && (myDataBase.DBIsOpened()))
            {
                SkladovaKarta sklKarta = new SkladovaKarta(DBRow, myDataBase, new tableItemExistDelgStr(myDataBase.tableZrusenoItemExist));
                sklKarta.setWinName("Zrušená karta");
                sklKarta.ShowDialog();
            }
        }

        public override void opravKartu(Hashtable DBRow, vDatabase myDataBase, DataGridView myDataGridView)
        {
            if ((myDataBase != null) && (myDataBase.DBIsOpened()))
            {
                SkladovaKarta sklKarta = new SkladovaKarta(DBRow, myDataBase,new tableItemExistDelgStr(myDataBase.tableZrusenoItemExist), sKartaState.edit);
                sklKarta.setWinName("Zrušená karta");
                if (sklKarta.ShowDialog() == DialogResult.OK)
                {
                    SkladovaKarta.messager mesenger = sklKarta.getMesseger();
                    Boolean updateIsOk = myDataBase.editNewLineKaret(mesenger.poradi, mesenger.nazev, mesenger.jk, mesenger.csn, mesenger.din, mesenger.vyrobce, mesenger.cenaKs, mesenger.poznamka, mesenger.minStav, mesenger.ucetCena, mesenger.ucetStav, mesenger.fyzStav, mesenger.rozmer, mesenger.ucet);
                    if (updateIsOk)
                    {
                        // je potreba najit index v datove tabulce - po trideni neni schodny s indexem ve view
                        Int32 dataRowIndex = -1;
                        for (int x = 0; x < (myDataGridView.DataSource as DataTable).Rows.Count - 1; x++)
                        {
                            if (Convert.ToInt32((myDataGridView.DataSource as DataTable).Rows[x][0]) == mesenger.poradi)
                            {
                                dataRowIndex = x;
                                break;
                            }

                        }

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

                }
            }

        }


        public override void zrusKartu(Hashtable DBRow, vDatabase myDataBase, DataGridView myDataGridView)
        {
            if (MessageBox.Show("Opravdu chcete zrušit kartu ?", "Zrušení karty", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // zrusime kartu
                Int32 poradi = Convert.ToInt32(DBRow["poradi"]);

                if (myDataBase.deleteLineKaret(poradi))
                {
                    // smazeme z obrazovky
                    // je potreba najit index v datove tabulce - po trideni neni schodny s indexem ve view
                    Int32 dataRowIndex = -1;
                    for (int x = 0; x < (myDataGridView.DataSource as DataTable).Rows.Count - 1; x++)
                    {
                        if (Convert.ToInt32((myDataGridView.DataSource as DataTable).Rows[x][0]) == poradi)
                        {
                            dataRowIndex = x;
                            break;
                        }
                    }
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



        public override void zobrazKartu(Hashtable DBRow, vDatabase myDataBase)
        {
            VraceneKarta poskozKarta = new VraceneKarta(DBRow);
            poskozKarta.setWinName("Poškozeno");
            poskozKarta.ShowDialog();
        }

        public override void opravKartu(Hashtable DBRow, vDatabase myDataBase, DataGridView myDataGridView)
        {
            if ((myDataBase != null) && (myDataBase.DBIsOpened()))
            {
                VraceneKarta poskozKarta = new VraceneKarta(DBRow, myDataBase, new tableItemExistDelgInt(myDataBase.tablePoskozenoItemExist), vKartaState.edit);
                poskozKarta.setWinName("Poškozeno");
                if (poskozKarta.ShowDialog() == DialogResult.OK)
                {
                    VraceneKarta.messager mesenger = poskozKarta.getMesseger();
                    Boolean updateIsOk = myDataBase.editNewLinePoskozene(mesenger.poradi, mesenger.jmeno, mesenger.prijmeni, mesenger.oscislo, mesenger.stredisko, mesenger.provoz, mesenger.nazev, mesenger.jk, mesenger.pocetKs, mesenger.rozmer, mesenger.csn, mesenger.cena, mesenger.datum, mesenger.zakazka, mesenger.konto);
                    if (updateIsOk)
                    {
                        // je potreba najit index v datove tabulce - po trideni neni schodny s indexem ve view
                        Int32 dataRowIndex = -1;
                        for (int x = 0; x < (myDataGridView.DataSource as DataTable).Rows.Count - 1; x++)
                        {
                            if (Convert.ToInt32((myDataGridView.DataSource as DataTable).Rows[x][0]) == mesenger.poradi)
                            {
                                dataRowIndex = x;
                                break;
                            }

                        }

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
                }
            }
        }

    }



    class detailVraceno : detail
    {

        public override void zobrazKartu(Hashtable DBRow, vDatabase myDataBase)
        {
            VraceneKarta vracKarta = new VraceneKarta(DBRow);
            vracKarta.setWinName("Vraceno");
            vracKarta.ShowDialog();
        }
       
        
        public override void opravKartu(Hashtable DBRow, vDatabase myDataBase, DataGridView myDataGridView)
        {
            VraceneKarta vracKarta = new VraceneKarta(DBRow, myDataBase, new tableItemExistDelgInt(myDataBase.tablePoskozenoItemExist), vKartaState.edit);
            vracKarta.setWinName("Vraceno");
            if (vracKarta.ShowDialog() == DialogResult.OK)
            {
                VraceneKarta.messager mesenger = vracKarta.getMesseger();
                Boolean updateIsOk = myDataBase.editNewLineVracene(mesenger.poradi, mesenger.jmeno, mesenger.prijmeni, mesenger.oscislo, mesenger.stredisko, mesenger.provoz, mesenger.nazev, mesenger.jk, mesenger.pocetKs, mesenger.rozmer, mesenger.csn, mesenger.cena, mesenger.datum, mesenger.zakazka, mesenger.konto);
                if (updateIsOk)
                {
                    // je potreba najit index v datove tabulce - po trideni neni schodny s indexem ve view
                    Int32 dataRowIndex = -1;
                    for (int x = 0; x < (myDataGridView.DataSource as DataTable).Rows.Count - 1; x++)
                    {
                        if (Convert.ToInt32((myDataGridView.DataSource as DataTable).Rows[x][0]) == mesenger.poradi)
                        {
                            dataRowIndex = x;
                            break;
                        }

                    }

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
            }
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
        
        public override void zobrazKartu(Hashtable DBRow, vDatabase myDataBase)
        {
            PracovniciKarta pracKarta = new PracovniciKarta(DBRow, myDataBase);
            pracKarta.ShowDialog();
        }

        public override void pridejKartu(vDatabase myDataBase, DataGridView myDataGridView)
        {
            // zalozeni nove skladove karty
            if ((myDataBase != null) && (myDataBase.DBIsOpened()))
            {
                PracovniciKarta pracKarta = new PracovniciKarta(myDataBase);
                if (pracKarta.ShowDialog() == DialogResult.OK)
                {

                    PracovniciKarta.messager mesenger = pracKarta.getMesseger();
                    Int32 stav = myDataBase.addNewLineOsoby(mesenger.prijmeni, mesenger.jmeno, mesenger.ulice, mesenger.mesto, mesenger.psc, mesenger.telHome, mesenger.oscislo, mesenger.stredisko, mesenger.cisZnamky, mesenger.oddeleni, mesenger.pracoviste, mesenger.telZam, mesenger.poznamka);
                    if (stav != -1)
                    {
                        (myDataGridView.DataSource as DataTable).Rows.Add(mesenger.prijmeni, mesenger.jmeno, mesenger.oscislo, mesenger.oddeleni, mesenger.stredisko, mesenger.pracoviste, mesenger.cisZnamky, mesenger.ulice,mesenger.psc, mesenger.mesto, mesenger.telHome, mesenger.telZam, mesenger.poznamka);
                        int counter = myDataGridView.Rows.Count - 1;

                        myDataGridView.FirstDisplayedScrollingRowIndex = myDataGridView.Rows[counter].Index;
                        myDataGridView.Refresh();
                        myDataGridView.CurrentCell = myDataGridView.Rows[counter].Cells[1];
                        myDataGridView.Rows[counter].Selected = true;
                    }


                }
            }
        }


        public override void opravKartu(Hashtable DBRow, vDatabase myDataBase, DataGridView myDataGridView)
        {
            if ((myDataBase != null) && (myDataBase.DBIsOpened()))
            {
                PracovniciKarta pracKarta = new PracovniciKarta(DBRow, myDataBase, uKartaState.edit);
                if (pracKarta.ShowDialog() == DialogResult.OK)
                {
                    PracovniciKarta.messager mesenger = pracKarta.getMesseger();


                    Boolean updateIsOk = myDataBase.editNewLineOsoby(mesenger.prijmeni, mesenger.jmeno, mesenger.ulice, mesenger.mesto,
                                                 mesenger.psc, mesenger.telHome, mesenger.oscislo, mesenger.stredisko,
                                                 mesenger.cisZnamky, mesenger.oddeleni, mesenger.pracoviste, mesenger.telZam, mesenger.poznamka);
                    if (updateIsOk)
                    {
                        // je potreba najit index v datove tabulce - po trideni neni schodny s indexem ve view
                        Int32 dataRowIndex = -1;
                        for (int x = 0; x < (myDataGridView.DataSource as DataTable).Rows.Count - 1; x++)
                        {
                            if (Convert.ToString((myDataGridView.DataSource as DataTable).Rows[x][2]) == mesenger.oscislo)
                            {
                                dataRowIndex = x;
                                break;
                            }

                        }

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
            }
        }

        public override void Zapujceno(Hashtable DBRow, vDatabase myDataBase)
        {
            if ((myDataBase != null) && (myDataBase.DBIsOpened()))
            {
                ZapujceneNaradiKarta zapujcKarta = new ZapujceneNaradiKarta();// (DBRow, myDataBase, uKartaState.edit);
                if (zapujcKarta.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Není dopracovano.");
                }
            }
        }


    }

}
