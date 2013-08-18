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

        public virtual void opravKartu(Hashtable DBRow, vDatabase myDataBase)
        {
            MessageBox.Show("Není implementováno.");
        }

        public virtual void zrusKartu(Hashtable DBRow, vDatabase myDataBase)
        {
            MessageBox.Show("Není implementováno.");
        }

    
    }

    class detailNone : detail
    {
    }
    
    
    class detailSklad : detail  // karta naradi
    {
        public override void zobrazKartu(Hashtable DBRow, vDatabase myDataBase) 
            
        {
            if ((myDataBase != null) && (myDataBase.DBIsOpened()))
            {
                SkladovaKarta sklKarta = new SkladovaKarta(DBRow, myDataBase);
                sklKarta.ShowDialog();
            }
        }

        public override void pridejKartu(vDatabase myDataBase, DataGridView myDataGridView)
        {
            // zalozeni nove skladove karty
            if ((myDataBase != null) && (myDataBase.DBIsOpened()))
            {
                SkladovaKarta sklKarta = new SkladovaKarta(myDataBase);
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


        public override void opravKartu(Hashtable DBRow, vDatabase myDataBase)
        {
            if ((myDataBase != null) && (myDataBase.DBIsOpened()))
            {
                SkladovaKarta sklKarta = new SkladovaKarta(DBRow, myDataBase, sKartaState.edit);
                if (sklKarta.ShowDialog() == DialogResult.OK)
                {
                    SkladovaKarta.messager mesenger = sklKarta.getMesseger();
                    Boolean updateIsOk = myDataBase.editNewLineNaradi(mesenger.poradi ,mesenger.nazev, mesenger.jk, mesenger.csn, mesenger.din, mesenger.vyrobce, mesenger.cenaKs, mesenger.poznamka, mesenger.minStav, mesenger.ucetCena, mesenger.ucetStav, mesenger.ucetStav, mesenger.rozmer, mesenger.ucet, mesenger.ucetCenaKs, new DateTime(0));
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
                SkladovaKarta sklKarta = new SkladovaKarta(DBRow, myDataBase);
                sklKarta.ShowDialog();
            }
        }

        public override void opravKartu(Hashtable DBRow, vDatabase myDataBase)
        {
            if ((myDataBase != null) && (myDataBase.DBIsOpened()))
            {
                SkladovaKarta sklKarta = new SkladovaKarta(DBRow, myDataBase, sKartaState.edit);
                sklKarta.ShowDialog();
            }
        }
    }

    class detailPoskozeno : detail
    {
        public override void zobrazKartu(Hashtable DBRow, vDatabase myDataBase)
        {
            VraceneKarta sklKarta = new VraceneKarta(DBRow);
            sklKarta.ShowDialog();
        }

        public override void opravKartu(Hashtable DBRow, vDatabase myDataBase)
        {
            if ((myDataBase != null) && (myDataBase.DBIsOpened()))
            {
                SkladovaKarta sklKarta = new SkladovaKarta(DBRow, myDataBase, sKartaState.edit);
                sklKarta.ShowDialog();
            }
        }

    }



    class detailVraceno : detail
    {
        public override void zobrazKartu(Hashtable DBRow, vDatabase myDataBase)
        {
            VraceneKarta sklKarta = new VraceneKarta(DBRow);
            sklKarta.ShowDialog();
        }

    }



    class detailOsoby : detail

    {
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


        public override void opravKartu(Hashtable DBRow, vDatabase myDataBase)
        {
            if ((myDataBase != null) && (myDataBase.DBIsOpened()))
            {
                PracovniciKarta pracKarta = new PracovniciKarta(DBRow, myDataBase, uKartaState.edit);
                if (pracKarta.ShowDialog() == DialogResult.OK)
                {
                    PracovniciKarta.messager mesenger = pracKarta.getMesseger();
                }
            }
        }



    }

}
