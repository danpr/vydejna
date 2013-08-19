using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Microsoft.Win32;


namespace Vydejna
{
    public partial class Vydejna : Form
    {
        private string dbfPath;
        private vDatabase myDB;
        private detail karta;
        private Hashtable DBRow;
        private parametryDB nastaveniDB;

        public Vydejna()
        {
            InitializeComponent();
            myDB = null;

            contextMenuDisable();

            nastaveniDB = new parametryDB();

            loadSettingDB(nastaveniDB);

            karta = new detailNone (); // karta - stavovy objekt - volame vzdy funkci karta.zobrazKartu 
                                       //  a podle toho jakeho je karta typu se objevi prislusne okno


            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            DBRow = new Hashtable();

            if (nastaveniDB.codeDB != (int)kodDB.dbNone)
            {
                myDB = OpenDataBaseHandle();
                myDB.openDB();
            }
        }

        private void loadNaradiItems()
        {
            // nahraje hlavni tabulku - naradi - skladove karty
            Application.DoEvents();
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            Application.DoEvents();

            if (myDB.DBIsOpened())
            {
                try
                {
                    
                    dataGridView1.DataSource = myDB.loadDataTableNaradi();
                    dataGridView1.RowHeadersVisible = false;

                    dataGridView1.Columns[0].HeaderText = "Pořadí";
                    dataGridView1.Columns[1].HeaderText = "KD";
                    dataGridView1.Columns[2].HeaderText = "Název";
                    dataGridView1.Columns[3].HeaderText = "Označení JK";
                    dataGridView1.Columns[4].HeaderText = "KS/výdejna";
                    dataGridView1.Columns[5].HeaderText = "Anal. účet";
                    dataGridView1.Columns[6].HeaderText = "Norma ČSN";
                    dataGridView1.Columns[7].HeaderText = "Norma DIN";
                    dataGridView1.Columns[8].HeaderText = "Výrobce";
                    dataGridView1.Columns[9].HeaderText = "Rozměr";
                    dataGridView1.Columns[10].HeaderText = "Fyzický stav";
                    dataGridView1.Columns[11].HeaderText = "Cena";
                    dataGridView1.Columns[12].HeaderText = "Celková cena";
                    dataGridView1.Columns[13].HeaderText = "Minimální stav";
                    dataGridView1.Columns[14].HeaderText = "Poznámka";
//                    dataGridView1.Columns["cntrcode"].Visible = false;   // cntrcode nezobrazujeme
                    dataGridView1.Columns["ucetkscen"].Visible = false;   // ucetkscen nezobrazujeme
                    dataGridView1.Columns["poradi"].Visible = false;   // poradi nezobrazujeme

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception)
                {
                    MessageBox.Show("Tabulka naradi nelze otevřít.");
                }
                finally
                {
                 //   myDB.closeDB();
                }
            }
          //  myDB.myDBConn.Dispose();
        }


        private void loadZrusenychItems()
        {
            Application.DoEvents();
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            Application.DoEvents();

            if (myDB.DBIsOpened())
            {
                try
                {
                    dataGridView1.DataSource = myDB.loadDataTableZruseno();
                    dataGridView1.RowHeadersVisible = false;

                    dataGridView1.Columns[0].HeaderText = "Název";
                    dataGridView1.Columns[1].HeaderText = "Označení JK";
                    dataGridView1.Columns[2].HeaderText = "KS/výdejna";
                    dataGridView1.Columns[3].HeaderText = "Anal. účet";
                    dataGridView1.Columns[4].HeaderText = "Norma ČSN";
                    dataGridView1.Columns[5].HeaderText = "Norma DIN";
                    dataGridView1.Columns[6].HeaderText = "Výrobce";
                    dataGridView1.Columns[7].HeaderText = "Rozměr";
                    dataGridView1.Columns[8].HeaderText = "Fyzický stav";
                    dataGridView1.Columns[9].HeaderText = "Cena";
                    dataGridView1.Columns[10].HeaderText = "Celková cena";
                    dataGridView1.Columns[11].HeaderText = "Minimální stav";
                    dataGridView1.Columns[12].HeaderText = "Poznámka";
//                    dataGridView1.Columns["cntrcode"].Visible = false;   // cntrcode nezobrazujeme
//                    dataGridView1.Columns["ucetkscen"].Visible = false;   // ucetkscen nezobrazujeme
                    dataGridView1.Columns["poradi"].Visible = false;   // poradi nezobrazujeme
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception)
                {
                    MessageBox.Show("Tabulka AR nelze otevřít.");
                }
                finally
                {
                 //   myDB.closeDB();
                }
            }
          //  myDB.myDBConn.Dispose();
        }




        private void loadPoskozenoItems()
        {
            Application.DoEvents();
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            Application.DoEvents();

            if (myDB.DBIsOpened())
            {
                try
                {

                    dataGridView1.DataSource = myDB.loadDataTablePoskozeno();
                    dataGridView1.RowHeadersVisible = false;

                    dataGridView1.Columns[0].HeaderText = "Název";
                    dataGridView1.Columns[1].HeaderText = "Označení JK";
                    dataGridView1.Columns[2].HeaderText = "Vráceno ks";
                    dataGridView1.Columns[3].HeaderText = "Rozměr";
                    dataGridView1.Columns[4].HeaderText = "Norma ČSN";
                    dataGridView1.Columns[5].HeaderText = "Cena";
                    dataGridView1.Columns[6].HeaderText = "Datum";
                    dataGridView1.Columns[7].HeaderText = "Zakázka";
                    dataGridView1.Columns[8].HeaderText = "Konto";
                    dataGridView1.Columns[9].HeaderText = "Přijmení";
                    dataGridView1.Columns[10].HeaderText = "Jméno";
                    dataGridView1.Columns[11].HeaderText = "Os. číslo";
                    dataGridView1.Columns[12].HeaderText = "Středisko";
                    dataGridView1.Columns[13].HeaderText = "Provoz";
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                }
                catch (Exception)
                {
                    MessageBox.Show("Tabulka poškozeného nářadí nelze otevřít.");
                }
                finally
                {
                //    myDB.closeDB();
                }
            }
          //  myDB.myDBConn.Dispose();
        }




        private void loadVracenoItems()
        {
            Application.DoEvents();
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            Application.DoEvents();

            if (myDB.DBIsOpened())
            {
                try
                {

                    dataGridView1.DataSource = myDB.loadDataTableVraceno();
                    dataGridView1.RowHeadersVisible = false;

                    dataGridView1.Columns[0].HeaderText = "Název";
                    dataGridView1.Columns[1].HeaderText = "Označení JK";
                    dataGridView1.Columns[2].HeaderText = "Vráceno ks";
                    dataGridView1.Columns[3].HeaderText = "Rozměr";
                    dataGridView1.Columns[4].HeaderText = "Norma ČSN";
                    dataGridView1.Columns[5].HeaderText = "Cena";
                    dataGridView1.Columns[6].HeaderText = "Datum";
                    dataGridView1.Columns[7].HeaderText = "Zakázka";
                    dataGridView1.Columns[8].HeaderText = "Konto";
                    dataGridView1.Columns[9].HeaderText = "Přijmení";
                    dataGridView1.Columns[10].HeaderText = "Jméno";
                    dataGridView1.Columns[11].HeaderText = "Os. číslo";
                    dataGridView1.Columns[12].HeaderText = "Středisko";
                    dataGridView1.Columns[13].HeaderText = "Provoz";
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                }
                catch (Exception)
                {
                    MessageBox.Show("Tabulka vraceného nářadí nelze otevřít.");
                }
                finally
                {
                //    myDB.closeDB();
                }
            }
        //    myDB.myDBConn.Dispose();
        }


        private void loadOsobyItems()
        {
            Application.DoEvents();

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            Application.DoEvents();
            

            if (myDB.DBIsOpened())
            {
                try
                {
                    //myDB.loadDataTableOsoby(myDataTable);
                    dataGridView1.DataSource = myDB.loadDataTableOsoby();
                    dataGridView1.RowHeadersVisible = false;


                    dataGridView1.Columns[0].HeaderText = "Přijmení";
                    dataGridView1.Columns[1].HeaderText = "Jméno";
                    dataGridView1.Columns[2].HeaderText = "Osobní číslo";
                    dataGridView1.Columns[3].HeaderText = "Provoz";
                    dataGridView1.Columns[4].HeaderText = "Středisko";
                    dataGridView1.Columns[5].HeaderText = "Pracovište";
                    dataGridView1.Columns[6].HeaderText = "Číslo znamky";
                    dataGridView1.Columns[7].HeaderText = "Ulice";
                    dataGridView1.Columns[8].HeaderText = "PSČ";
                    dataGridView1.Columns[9].HeaderText = "Město";
                    dataGridView1.Columns[10].HeaderText = "Tel. domů";
                    dataGridView1.Columns[11].HeaderText = "Tel. zaměst.";
                    dataGridView1.Columns[12].HeaderText = "Poznamka";
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


                }
                catch (Exception)
                {
                    MessageBox.Show("Tabulku pracovníků provozu nelze otevřít.");
                }
                finally
                {
                //    myDB.closeDB();
                }
            }
       //     myDB.myDBConn.Dispose();
        }


        private vDatabase OpenDataBaseHandle(Boolean useUserPriv = true )
        {
            switch (nastaveniDB.codeDB)
            {
               case (int)kodDB.dbSQLite:
                 string pathDbName =  nastaveniDB.umistemiDB +"\\"+ nastaveniDB.nameDB;

                 return new vSQLite (pathDbName,"","","","");
//                 break;
               
               case (int)kodDB.dbPostgresODBC:
                 if (useUserPriv)
                 {
                     return new vPostgress(nastaveniDB.nameDB, nastaveniDB.adresaServerDB, nastaveniDB.portServerDB.ToString(),
                            nastaveniDB.userIdDB, nastaveniDB.userPasswdDB);
                 }
                 else
                 {
                     return new vPostgress(nastaveniDB.nameDB, nastaveniDB.adresaServerDB, nastaveniDB.portServerDB.ToString(),
                            nastaveniDB.adminIdDB, nastaveniDB.adminPasswdDB);

                 }

//                 break;

                default:
                   return null;
//                 break; 


            }
        }


        public void DropDBTables()
        {
            // zrusi tabulky    
            myDB.closeDB();
            myDB = null;


            vDatabase localDB = OpenDataBaseHandle(false);
            localDB.openDB();
            if (localDB.DBIsOpened())
            {
                localDB.DropTables(); // take provede .closeDB()                localDB.closeDB();
            }
        }


        public void DeleteDBTables()
        {
            // maze tabulky    
               myDB.DeleteTables();
        }
       

        public void CreateDBTables()
        {
           // vytvori tabulky
            if ((myDB != null) && myDB.DBIsOpened())
            {
                myDB.closeDB();
                myDB = null;
            }

            vDatabase localDB = OpenDataBaseHandle(false);
            localDB.openDB();
            if (localDB.DBIsOpened())
            {
                localDB.CreateTables(); // take provede .closeDB()
                myDB = OpenDataBaseHandle();
                myDB.openDB();
            }
        }

        private void nahráníDatZDBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dbfPath = "";
            folderBrowserDialog1.ShowNewFolderButton = false;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                dbfPath = folderBrowserDialog1.SelectedPath;
            }

            Application.DoEvents();

            if (dbfPath != "") 
            {
                String filepath = dbfPath;
                progressBarMain.Style = ProgressBarStyle.Marquee;

                dataGridView1.DataSource = null;

                // prvni soubor AR_KARET
               // string connString = @"Provider=vfpoledb.1;Data Source=c:\wwapps\wc3\wwdemo;Exclusive=false;Nulls=false";

                Hashtable DBJoin;
                DBJoin = new Hashtable();

                DBJoin.Clear();

                DBJoin.Add("test", 1);

                myDB = OpenDataBaseHandle();
                myDB.openDB();

                labelView.Text = "Přesouvám tabulku vyřazených karet";
                Application.DoEvents();


                progressBarMain.MarqueeAnimationSpeed = 100;
                PresunDB.presunVyrazene(myDB, filepath, DBJoin);
                progressBarMain.MarqueeAnimationSpeed = 0;
 

                // druhy soubor naradi
                labelView.Text = "Přesouvám tabulku nářadí";
                Application.DoEvents();

                progressBarMain.MarqueeAnimationSpeed = 100;
                PresunDB.presunNaradi(myDB, filepath, DBJoin);
                progressBarMain.MarqueeAnimationSpeed = 0;

                // treti soubor vraceno
                labelView.Text = "Přesouvám tabulku vráceného nářadí";
                Application.DoEvents();


                progressBarMain.MarqueeAnimationSpeed = 100;
                PresunDB.presunVracene(myDB, filepath);
                progressBarMain.MarqueeAnimationSpeed = 0;

                // ctrvrty soubor vyrazeno
                labelView.Text = "Přesouvám tabulku poskozenzeného nářadí";
                Application.DoEvents();


                progressBarMain.MarqueeAnimationSpeed = 100;
                PresunDB.presunPoskozene(myDB, filepath);
                progressBarMain.MarqueeAnimationSpeed = 0;


                // sestz soubor zmeny
                labelView.Text = "Přesouvám tabulku změn stavu nářadí";
                Application.DoEvents();


                progressBarMain.MarqueeAnimationSpeed = 100;
//                PresunDB.presunZmeny(myDB, filepath);
                PresunDB.presunVyrazeneMDat(myDB, filepath, DBJoin);
                progressBarMain.MarqueeAnimationSpeed = 0;



                // paty soubor osoby
                
                labelView.Text = "Přesouvám tabulku osob.";
                Application.DoEvents();


                progressBarMain.MarqueeAnimationSpeed = 100;
                PresunDB.presunOsoby(myDB, filepath);
                progressBarMain.MarqueeAnimationSpeed = 0;
//                myDB.closeDB();


                progressBarMain.Style = ProgressBarStyle.Blocks;
                progressBarMain.Value=0;
                labelView.Text = "";
            }
        }

        private void konecProgramuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            Close();
        }

        private void vytvoreniTabulekToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void vytvoreniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // vytvoreni tabulek
            labelView.Text = "Vytvářím tabulky";
            contextMenuDisable();
            dataGridView1.DataSource = null;
            karta = new detailNone();
            Application.DoEvents();
            CreateDBTables();
            labelView.Text = "";
        }

        private void smazániToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //zruseni tabulek
            labelView.Text = "Ruším tabulky";
            contextMenuDisable();
            dataGridView1.DataSource = null;
            karta = new detailNone();
            Application.DoEvents();
            DropDBTables();
            labelView.Text = "";

        }


        private void vymazáníToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labelView.Text = "Mažu tabulky";
            contextMenuDisable();
            dataGridView1.DataSource = null;
            karta = new detailNone();
            Application.DoEvents();
            DeleteDBTables();
            labelView.Text = "";
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // naradi - skladove karty - hlavni tabulka
            labelView.Text = "Výdejna nářadí přehled - Stahuji";
            Application.DoEvents();
            loadNaradiItems();
            karta = new detailSklad();
            labelView.Text = "Výdejna nářadí přehled";
            contextMenuEnable(true);
        }


        private void contextMenu_add(object sender, EventArgs e)
        {
            // zalozeni nove skladove karty
            if ((myDB != null) && (myDB.DBIsOpened()))
            {
                SkladovaKarta mySkladovaKarta = new SkladovaKarta(myDB);
                mySkladovaKarta.ShowDialog();
            }
        }

        private void contextMenu_edit(object sender, EventArgs e)
        {
            int index = dataGridView1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index > -1)
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    if (DBRow.ContainsKey(dataGridView1.Columns[i].Name))
                    {
                        DBRow.Remove(dataGridView1.Columns[i].Name);
                    }
                    DBRow.Add(dataGridView1.Columns[i].Name, dataGridView1[i, index].Value);

                }

                karta.zobrazKartu(DBRow, myDB);
            }

        }



        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            labelView.Text = "Archív zrušených karet - Stahuji";
            Application.DoEvents();
            loadZrusenychItems();
            karta = new detailZruseno();
            contextMenuEnable(false);
            labelView.Text = "Archív zrušených karet";
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            labelView.Text = "Vrácené nářadí do skladu - Stahuji";
            Application.DoEvents();
            loadVracenoItems();
            karta = new detailVraceno();
            contextMenuEnable(false);
            labelView.Text = "Vrácené nářadí do skladu";
        }

        private void pracovníciProvozuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //osoby
            labelView.Text = "Pracovníci provozu - Načítání";
            Application.DoEvents();
            loadOsobyItems();
            karta = new detailOsoby();
            contextMenuEnable(true);
            labelView.Text = "Pracovníci provozu";
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (index > -1)
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    if (DBRow.ContainsKey(dataGridView1.Columns[i].Name))
                    {
                        DBRow.Remove(dataGridView1.Columns[i].Name);
                    }
                    DBRow.Add(dataGridView1.Columns[i].Name, dataGridView1[i, index].Value);

                }

                karta.zobrazKartu(DBRow,myDB);
            }

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            labelView.Text = "Poškozené nářadí - Stahuji";
            Application.DoEvents();
            loadPoskozenoItems();
            karta = new detailPoskozeno();
            contextMenuEnable(false);
            labelView.Text = "Poškozené nářadí";
          
        }

// NASTAVENI DATABAZE
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            NastaveniDB dbPref = new NastaveniDB(nastaveniDB);
            if (dbPref.ShowDialog() == DialogResult.OK)
            {
              nastaveniDB = dbPref.getParemetryDB();
              saveSettingDB(nastaveniDB);

              if ((myDB != null) && myDB.DBIsOpened())
              {
                  myDB.closeDB();
                  myDB = null;
              }

              myDB = OpenDataBaseHandle();
              myDB.openDB();


            }

        }


        private void loadSettingDB(parametryDB myParametryDB)
        {
            RegistryKey klic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS\DB", true);
            if (klic != null)
            {
                myParametryDB.nameDB = klic.GetValue("Name").ToString();
                myParametryDB.codeDB = Convert.ToInt32(klic.GetValue("CodeDB").ToString());
                myParametryDB.umistemiDB = klic.GetValue("Location").ToString();
                myParametryDB.adresaServerDB = klic.GetValue("ServerAddr").ToString();
                myParametryDB.portServerDB = Convert.ToInt32(klic.GetValue("ServerPort").ToString());
                myParametryDB.userIdDB = klic.GetValue("UserId").ToString();
                myParametryDB.userPasswdDB= klic.GetValue("UserPassword").ToString();
                myParametryDB.adminIdDB = klic.GetValue("AdminId").ToString();
                myParametryDB.adminPasswdDB = klic.GetValue("AdminPassword").ToString();


            }
            else
            {
                myParametryDB.nameDB = "";
                myParametryDB.codeDB = -1;
                myParametryDB.umistemiDB = "";
                myParametryDB.adresaServerDB = "";
                myParametryDB.userIdDB = "";
                myParametryDB.userPasswdDB = "";
                myParametryDB.adminIdDB = "";
                myParametryDB.adminPasswdDB = "";
            }
        }

        private void saveSettingDB(parametryDB myParametryDB )
        {
            RegistryKey regHelpKlic;
            RegistryKey klic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS\DB", true);
            if (klic == null)
            {
                RegistryKey regKlic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS", true);
                if (regKlic == null)
                {
                    regHelpKlic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE", true);
                    regHelpKlic.CreateSubKey("CS");
                }
                regHelpKlic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS", true);
                regHelpKlic.CreateSubKey("DB");
                klic = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CS\DB", true);
            }
            // zapis polozky
            klic.SetValue("Name", myParametryDB.nameDB);
            klic.SetValue("CodeDB", myParametryDB.codeDB);
            klic.SetValue("Location", myParametryDB.umistemiDB);
            klic.SetValue("ServerAddr", myParametryDB.adresaServerDB);
            klic.SetValue("ServerPort", myParametryDB.portServerDB);
            klic.SetValue("UserId", myParametryDB.userIdDB);
            klic.SetValue("UserPassword", myParametryDB.userPasswdDB);
            klic.SetValue("AdminId", myParametryDB.adminIdDB);
            klic.SetValue("AdminPassword", myParametryDB.adminPasswdDB);
        }

        private void NovaSklKarta(object sender, EventArgs e)
        {

            // zalozeni nove skladove karty
            if ((myDB != null) && (myDB.DBIsOpened()))
            {
                SkladovaKarta sklKarta = new SkladovaKarta(myDB);
                if (sklKarta.ShowDialog() == DialogResult.OK)
                {
                    SkladovaKarta.messager mesenger = sklKarta.getMesseger();

                    Int32 poradi = myDB.addNewLineNaradi(mesenger.nazev, mesenger.jk, mesenger.csn, mesenger.din, mesenger.vyrobce, mesenger.cenaKs, mesenger.poznamka, mesenger.minStav, mesenger.ucetCena, mesenger.ucetStav, mesenger.ucetStav, mesenger.rozmer, mesenger.ucet, mesenger.ucetCenaKs, new DateTime(0));
                    if (poradi != -1) 
                    {
                       if (karta.GetType() == typeof(detailSklad))  // zjisti typ karty
                        {
                            (dataGridView1.DataSource as DataTable).Rows.Add(poradi, "", mesenger.nazev, mesenger.jk, mesenger.ucetStav, mesenger.ucet, mesenger.csn, mesenger.din, mesenger.vyrobce, mesenger.rozmer, 0, mesenger.cenaKs, mesenger.ucetCena, mesenger.minStav, mesenger.poznamka, mesenger.ucetCenaKs);

                            int counter = dataGridView1.Rows.Count-1;

                            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows[counter].Index;
                            dataGridView1.Refresh();
                            dataGridView1.CurrentCell = dataGridView1.Rows[counter].Cells[1];
                            dataGridView1.Rows[counter].Selected = true;

                        }
                    }

                    //("SELECT poradi, kodd, nazev, jk, ucetstav, analucet, normacsn, normadin, vyrobce, rozmer, fyzstav, cena, celkcena, minimum, poznamka, ucetkscen from naradi ORDER BY nazev"); 



                }
            }
          
        }


        private void contextMenuEnable(Boolean useAdd)
        {
            if (useAdd)
            {
                contextMenuStrip1.Items[0].Enabled = true;
                contextMenuStrip1.Items[0].Visible = true;
            }
            else
            {
                contextMenuStrip1.Items[0].Enabled = false;
                contextMenuStrip1.Items[0].Visible = false;
            }
            contextMenuStrip1.Enabled = true;
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
        }


        private void contextMenuDisable()
        {
            contextMenuStrip1.Enabled = false;
            dataGridView1.ContextMenuStrip = null;
        }


        private void ConMenuAddItem(object sender, EventArgs e)
        {
            // pridani polozky

            karta.pridejKartu(myDB,dataGridView1);
        }

        private void ConMenuEditItem(object sender, EventArgs e)
        {
            //oprava polozky
            if ( dataGridView1.SelectedRows.Count > 0)
            {
               //int index
               DataGridViewRow myRow = dataGridView1.SelectedRows[0];
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    if (DBRow.ContainsKey(dataGridView1.Columns[i].Name))
                    {
                        DBRow.Remove(dataGridView1.Columns[i].Name);
                    }
                    DBRow.Add(dataGridView1.Columns[i].Name,myRow.Cells[i].Value);
                }
                karta.opravKartu(DBRow, myDB, dataGridView1);
            }

        }

        private void ConMenuDeleteItem(object sender, EventArgs e)
        {
            // smazani polozky
            karta.zrusKartu(DBRow, myDB);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


    }
}
