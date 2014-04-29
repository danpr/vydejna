using System;
using System.IO;
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
        private enum evenStateEnum { enable, disable };

        private evenStateEnum evenState = evenStateEnum.disable;

        private string dbfPath;
        private vDatabase myDB;
        private detail karta;
        private Hashtable DBRow;
        private parametryDB nastaveniDB;

        public Vydejna()
        {
            InitializeComponent();

            Font initFont = ConfigReg.loadSettingFont();
            if (initFont != null)
            {
                dataGridView1.Font = initFont;
            }

            labelView.Font = new Font(labelView.Font, FontStyle.Bold);
            labelUser.Text = "";

            myDB = null;

            contextMenuDisable();

            nastaveniDB = new parametryDB();

            ConfigReg.loadSettingDB(nastaveniDB);

            karta = new detailNone(myDB, dataGridView1); // karta - stavovy objekt - volame vzdy funkci karta.zobrazKartu 
            //  a podle toho jakeho je karta typu se objevi prislusne okno


            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

            dataGridView1.AllowUserToOrderColumns = true;

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            DBRow = new Hashtable();

            if (nastaveniDB.codeDB != (int)kodDB.dbNone)
            {
                myDB = OpenDataBaseHandle();
                myDB.openDB();
                setStateChangeEvent(myDB);
            }

            if ((myDB != null) && (myDB.DBIsOpened()))
            {
                usersTest();
                globalSettingTest();
            }

            Size size = ConfigReg.loadSettingWindowSize("MAIN");
            if (!(size.IsEmpty)) this.Size = size;

            Point location = ConfigReg.loadSettingWindowLocation("MAIN");

            Int32 x = location.X;
            Int32 y = location.Y;
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x > (Screen.PrimaryScreen.Bounds.Width - 20)) x = Screen.PrimaryScreen.Bounds.Width - 20;
            if (y > (Screen.PrimaryScreen.Bounds.Height - 20)) y = Screen.PrimaryScreen.Bounds.Height - 20;

            if (!(location.IsEmpty)) this.SetDesktopLocation(x, y);

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan;

            timer1.Interval = 300000;
            timer1.Start();
        }



        private Hashtable getDBRowFromSelectedRow(Hashtable newDBRow)
        {
            if (newDBRow == null)
            {
                newDBRow = new Hashtable();
            }
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow myRow = dataGridView1.SelectedRows[0];
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    if (newDBRow.ContainsKey(dataGridView1.Columns[i].Name))
                    {
                        newDBRow.Remove(dataGridView1.Columns[i].Name);
                    }
                    newDBRow.Add(dataGridView1.Columns[i].Name, myRow.Cells[i].Value);
                }
            }
            return newDBRow;
        }


        private Hashtable getPoradiOscisloFromSelectedRow()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Hashtable newDBRow = new Hashtable();

                DataGridViewRow myRow = dataGridView1.SelectedRows[0];
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    if ((dataGridView1.Columns[i].Name == "poradi") || (dataGridView1.Columns[i].Name == "oscislo"))
                    {
                        if (newDBRow.ContainsKey(dataGridView1.Columns[i].Name))
                        {
                            newDBRow.Remove(dataGridView1.Columns[i].Name);
                        }
                        newDBRow.Add(dataGridView1.Columns[i].Name, myRow.Cells[i].Value);
                    }
                }
                return newDBRow;
            }
            return null;
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

                    dataGridView1.Columns["poradi"].HeaderText = "Pořadí";
                    dataGridView1.Columns["kodd"].HeaderText = "KD";
                    dataGridView1.Columns["nazev"].HeaderText = "Název";
                    dataGridView1.Columns["jk"].HeaderText = "Označení JK";
                    dataGridView1.Columns["fyzstav"].HeaderText = "Fyzický stav";
                    dataGridView1.Columns["analucet"].HeaderText = "Anal. účet";
                    dataGridView1.Columns["normacsn"].HeaderText = "Norma ČSN";
                    dataGridView1.Columns["normadin"].HeaderText = "Norma DIN";
                    dataGridView1.Columns["vyrobce"].HeaderText = "Výrobce";
                    dataGridView1.Columns["rozmer"].HeaderText = "Rozměr";
                    dataGridView1.Columns["ucetstav"].HeaderText = "KS/výdejna Úč. stav";
                    dataGridView1.Columns["cena"].HeaderText = "Cena";
                    dataGridView1.Columns["celkcena"].HeaderText = "Celková cena";
                    dataGridView1.Columns["minimum"].HeaderText = "Minimální stav";
                    dataGridView1.Columns["poznamka"].HeaderText = "Poznámka";
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

                    dataGridView1.Columns["poradi"].HeaderText = "Pořadí";
                    dataGridView1.Columns["nazev"].HeaderText = "Název";
                    dataGridView1.Columns["jk"].HeaderText = "Označení JK";
                    dataGridView1.Columns["ucetstav"].HeaderText = "KS/výdejna";
                    dataGridView1.Columns["analucet"].HeaderText = "Anal. účet";
                    dataGridView1.Columns["normacsn"].HeaderText = "Norma ČSN";
                    dataGridView1.Columns["normadin"].HeaderText = "Norma DIN";
                    dataGridView1.Columns["vyrobce"].HeaderText = "Výrobce";
                    dataGridView1.Columns["rozmer"].HeaderText = "Rozměr";
                    dataGridView1.Columns["fyzstav"].HeaderText = "Fyzický stav";
                    dataGridView1.Columns["cena"].HeaderText = "Cena";
                    dataGridView1.Columns["celkcena"].HeaderText = "Celková cena";
                    dataGridView1.Columns["minimum"].HeaderText = "Minimální stav";
                    dataGridView1.Columns["poznamka"].HeaderText = "Poznámka";

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

                    dataGridView1.Columns["poradi"].HeaderText = "Pořadí";
                    dataGridView1.Columns["nazev"].HeaderText = "Název";
                    dataGridView1.Columns["jk"].HeaderText = "Označení JK";
                    dataGridView1.Columns["pocetks"].HeaderText = "Poškozeno ks";
                    dataGridView1.Columns["rozmer"].HeaderText = "Rozměr";
                    dataGridView1.Columns["csn"].HeaderText = "Norma ČSN";
                    dataGridView1.Columns["cena"].HeaderText = "Cena";
                    dataGridView1.Columns["datum"].HeaderText = "Datum";
                    dataGridView1.Columns["vyrobek"].HeaderText = "Zakázka";
                    dataGridView1.Columns["konto"].HeaderText = "Konto";
                    dataGridView1.Columns["jmeno"].HeaderText = "Přijmení";
                    dataGridView1.Columns["krjmeno"].HeaderText = "Jméno";
                    dataGridView1.Columns["oscislo"].HeaderText = "Os. číslo";
                    dataGridView1.Columns["dilna"].HeaderText = "Středisko";
                    dataGridView1.Columns["pracoviste"].HeaderText = "Provoz";
                    dataGridView1.Columns["poradi"].Visible = false;   // poradi nezobrazujeme
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

                    dataGridView1.Columns["poradi"].HeaderText = "Pořadí";
                    dataGridView1.Columns["nazev"].HeaderText = "Název";
                    dataGridView1.Columns["jk"].HeaderText = "Označení JK";
                    dataGridView1.Columns["pocetks"].HeaderText = "Vráceno ks";
                    dataGridView1.Columns["rozmer"].HeaderText = "Rozměr";
                    dataGridView1.Columns["csn"].HeaderText = "Norma ČSN";
                    dataGridView1.Columns["cena"].HeaderText = "Cena";
                    dataGridView1.Columns["datum"].HeaderText = "Datum";
                    dataGridView1.Columns["vyrobek"].HeaderText = "Zakázka";
                    dataGridView1.Columns["konto"].HeaderText = "Konto";
                    dataGridView1.Columns["jmeno"].HeaderText = "Přijmení";
                    dataGridView1.Columns["krjmeno"].HeaderText = "Jméno";
                    dataGridView1.Columns["oscislo"].HeaderText = "Os. číslo";
                    dataGridView1.Columns["dilna"].HeaderText = "Středisko";
                    dataGridView1.Columns["pracoviste"].HeaderText = "Provoz";
                    dataGridView1.Columns["poradi"].Visible = false;   // poradi nezobrazujeme
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
                    dataGridView1.DataSource = myDB.loadDataTableOsoby();
                    dataGridView1.RowHeadersVisible = false;


                    dataGridView1.Columns["prijmeni"].HeaderText = "Přijmení";
                    dataGridView1.Columns["jmeno"].HeaderText = "Jméno";
                    dataGridView1.Columns["oscislo"].HeaderText = "Osobní číslo";
                    dataGridView1.Columns["odeleni"].HeaderText = "Provoz";
                    dataGridView1.Columns["stredisko"].HeaderText = "Středisko";
                    dataGridView1.Columns["pracoviste"].HeaderText = "Pracovište";
                    dataGridView1.Columns["cisznamky"].HeaderText = "Číslo znamky";
                    dataGridView1.Columns["ulice"].HeaderText = "Ulice";
                    dataGridView1.Columns["psc"].HeaderText = "PSČ";
                    dataGridView1.Columns["mesto"].HeaderText = "Město";
                    dataGridView1.Columns["telhome"].HeaderText = "Tel. domů";
                    dataGridView1.Columns["telzam"].HeaderText = "Tel. zaměst.";
                    dataGridView1.Columns["poznamka"].HeaderText = "Poznamka";
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


        private vDatabase OpenDataBaseHandle(Boolean useUserPriv = true)
        {
            switch (nastaveniDB.codeDB)
            {
                case (int)kodDB.dbSQLite:
                    string pathDbName = nastaveniDB.umistemiDB + "\\" + nastaveniDB.nameDB;

                    return new vSQLite(pathDbName, "", "", "", "", "", "", "");
                //                 break;
                case (int)kodDB.dbPostgresODBC:
                    if (useUserPriv)
                    {
                        return new vPostgress(nastaveniDB.nameDB, nastaveniDB.adresaServerDB, nastaveniDB.nameDBServeru, nastaveniDB.portServerDB.ToString(),
                               nastaveniDB.localizaceDBServeru, nastaveniDB.driverDB, nastaveniDB.userIdDB, nastaveniDB.userPasswdDB);
                    }
                    else
                    {
                        return new vPostgress(nastaveniDB.nameDB, nastaveniDB.adresaServerDB, nastaveniDB.nameDBServeru, nastaveniDB.portServerDB.ToString(),
                               nastaveniDB.localizaceDBServeru, nastaveniDB.driverDB, nastaveniDB.adminIdDB, nastaveniDB.adminPasswdDB);

                    }

                //                 break;
                case (int)kodDB.dbInformixODBC:
                    if (useUserPriv)
                    {
                        return new vInformixODBC(nastaveniDB.nameDB, nastaveniDB.adresaServerDB, nastaveniDB.nameDBServeru, nastaveniDB.portServerDB.ToString(),
                               nastaveniDB.localizaceDBServeru, nastaveniDB.driverDB, nastaveniDB.userIdDB, nastaveniDB.userPasswdDB);
                    }
                    else
                    {
                        return new vInformixODBC(nastaveniDB.nameDB, nastaveniDB.adresaServerDB, nastaveniDB.nameDBServeru, nastaveniDB.portServerDB.ToString(),
                               nastaveniDB.localizaceDBServeru, nastaveniDB.driverDB, nastaveniDB.adminIdDB, nastaveniDB.adminPasswdDB);

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

            // vztvorisi nove pripojeni na mazani tabulek
            vDatabase localDB = OpenDataBaseHandle(false);
            localDB.openDB();
            if (localDB.DBIsOpened())
            {   // smaze tabulky
                localDB.DropIndexes();
                localDB.DropTables();
                localDB.closeDB();
                MessageBox.Show("Rušení tabulek dokončeno.");
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
                localDB.CreateTables();
                localDB.CreateIndexes();

                localDB.closeDB();
                myDB = OpenDataBaseHandle();
                myDB.openDB();
                setStateChangeEvent(myDB);
                MessageBox.Show("Tabulky byly vytvořeny.");
            }
        }

        private void nahráníDatZDBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userIsAdminWM())
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
                    if (myDB == null)
                    {
                        myDB = OpenDataBaseHandle();
                        myDB.openDB();
                        setStateChangeEvent(myDB);
                    }

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
                    labelView.Text = "Přesouvám tabulku poškozeného nářadí";
                    Application.DoEvents();


                    progressBarMain.MarqueeAnimationSpeed = 100;
                    PresunDB.presunPoskozene(myDB, filepath);
                    progressBarMain.MarqueeAnimationSpeed = 0;


                    // sesty soubor zmeny
                    labelView.Text = "Přesouvám tabulku změn stavu nářadí";
                    Application.DoEvents();


                    progressBarMain.MarqueeAnimationSpeed = 100;
                    PresunDB.presunVyrazeneMDat(myDB, filepath, DBJoin);
                    progressBarMain.MarqueeAnimationSpeed = 0;


                    // sedmy soubor zapujcene
                    labelView.Text = "Přesouvám tabulku zapůjčeného nářadí";
                    Application.DoEvents();


                    progressBarMain.MarqueeAnimationSpeed = 100;
                    PresunDB.presunPerson(myDB, filepath, DBJoin);
                    progressBarMain.MarqueeAnimationSpeed = 0;



                    // paty soubor osoby

                    labelView.Text = "Přesouvám tabulku osob.";
                    Application.DoEvents();


                    progressBarMain.MarqueeAnimationSpeed = 100;
                    PresunDB.presunOsoby(myDB, filepath);
                    progressBarMain.MarqueeAnimationSpeed = 0;


                    progressBarMain.Style = ProgressBarStyle.Blocks;
                    progressBarMain.Value = 0;
                    labelView.Text = "";

                    MessageBox.Show("Přesun dat ukončen.");
                }
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
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userIsAdminWM())
            {
                // vytvoreni tabulek
                if (MessageBox.Show("Opravdu chcete vytvořit tabulky v databázi?", "Vytvoření tabulek", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    labelView.Text = "Vytvářím tabulky";
                    contextMenuDisable();
                    dataGridView1.DataSource = null;
                    karta = new detailNone(null, null);
                    Application.DoEvents();
                    CreateDBTables();
                    labelView.Text = "";
                }
            }
        }

        private void smazániToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userIsAdminWM())
            {
                if (MessageBox.Show("Opravdu chcete odstranit tabulky z databáze?", "Odstranění tabulek", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //zruseni tabulek
                    labelView.Text = "Ruším tabulky";
                    contextMenuDisable();
                    dataGridView1.DataSource = null;
                    karta = new detailNone(null, null);
                    Application.DoEvents();
                    DropDBTables();
                    labelView.Text = "";
                }
            }
        }


        private void vymazáníToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userIsAdminWM())
            {
                if (MessageBox.Show("Opravdu chcete smazat tabulky v databázi?", "Vymazání tabulek", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    labelView.Text = "Mažu tabulky";
                    contextMenuDisable();
                    dataGridView1.DataSource = null;
                    karta = new detailNone(null, null);
                    Application.DoEvents();
                    DeleteDBTables();
                    labelView.Text = "";
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userHasAccessRightsWM((Int32)permCode.Nar))
            {
                // naradi - skladove karty - hlavni tabulka
                labelView.Text = "Výdejna nářadí přehled - Stahuji";
                Application.DoEvents();
                evenState = evenStateEnum.disable;
                loadNaradiItems();
                karta = new detailSklad(myDB, dataGridView1);
                evenState = evenStateEnum.enable;
                labelView.Text = "Výdejna nářadí přehled";
                contextMenuEnable(true, true, true, false, true, true);
            }
        }


        private void contextMenu_add(object sender, EventArgs e)
        {
            // zalozeni nove skladove karty
            if ((myDB != null) && (myDB.DBIsOpened()))
            {
                SkladovaKarta mySkladovaKarta = new SkladovaKarta(myDB, new tableItemExistDelgStr(myDB.tableNaradiItemExist), dataGridView1.Font);
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

                karta.zobrazKartu(DBRow);
            }

        }



        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userHasAccessRightsWM((Int32)permCode.ZNar))
            {
                labelView.Text = "Archív zrušených karet - Stahuji";
                Application.DoEvents();
                evenState = evenStateEnum.disable;
                loadZrusenychItems();
                karta = new detailZruseno(myDB, dataGridView1);
                evenState = evenStateEnum.enable;
                contextMenuEnable(false);
                labelView.Text = "Archív zrušených karet";
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userHasAccessRightsWM((Int32)permCode.VNar))
            {
                labelView.Text = "Vrácené nářadí do skladu - Stahuji";
                Application.DoEvents();
                evenState = evenStateEnum.disable;
                loadVracenoItems();
                karta = new detailVraceno(myDB, dataGridView1, null);
                evenState = evenStateEnum.enable;
                contextMenuEnable(false);
                labelView.Text = "Vrácené nářadí do skladu";
            }
        }

        private void pracovníciProvozuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //osoby
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userHasAccessRightsWM((Int32)permCode.Prac))
            {
                labelView.Text = "Pracovníci provozu - Načítání";
                Application.DoEvents();
                evenState = evenStateEnum.disable;
                loadOsobyItems();
                karta = new detailOsoby(myDB, dataGridView1);
                evenState = evenStateEnum.enable;
                contextMenuEnable(true, false, false, true, false, true);

                labelView.Text = "Pracovníci provozu";
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Hashtable DBRowShow = getPoradiOscisloFromSelectedRow();
            if (DBRowShow != null)
            {
                karta.zobrazKartu(DBRowShow);
            }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userHasAccessRightsWM((Int32)permCode.PNar))
            {
                labelView.Text = "Poškozené nářadí - Stahuji";
                Application.DoEvents();
                evenState = evenStateEnum.disable;
                loadPoskozenoItems();
                karta = new detailPoskozeno(myDB, dataGridView1);
                evenState = evenStateEnum.enable;
                contextMenuEnable(false);
                labelView.Text = "Poškozené nářadí";
            }
        }

        // NASTAVENI DATABAZE
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userIsAdminWM())
            {

                NastaveniDB dbPref = new NastaveniDB(nastaveniDB);
                if (dbPref.ShowDialog() == DialogResult.OK)
                {
                    nastaveniDB = dbPref.getParemetryDB();
                    ConfigReg.saveSettingDB(nastaveniDB);

                    if ((myDB != null) && myDB.DBIsOpened())
                    {
                        myDB.closeDB();
                        myDB = null;
                    }

                    myDB = OpenDataBaseHandle();
                    myDB.openDB();
                    setStateChangeEvent(myDB);
                    if (myDB.DBIsOpened())
                    {
                        usersTest();

                    }
                }
            }
        }


        private void NovaSklKarta(object sender, EventArgs e)
        {
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userHasAccessRightsWM((Int32)permCode.NarAdd))
            {
                // zalozeni nove skladove karty
                if ((myDB != null) && (myDB.DBIsOpened()))
                {
                    SkladovaKarta sklKarta = new SkladovaKarta(myDB, new tableItemExistDelgStr(myDB.tableNaradiItemExist), dataGridView1.Font);
                    if (sklKarta.ShowDialog() == DialogResult.OK)
                    {
                        SkladovaKarta.messager mesenger = sklKarta.getMesseger();

                        Int32 poradi = myDB.addNewLineNaradi(mesenger.nazev, mesenger.jk, mesenger.csn, mesenger.din, mesenger.vyrobce, mesenger.cenaKs, mesenger.poznamka, mesenger.minStav, mesenger.ucetCena, mesenger.ucetStav, mesenger.ucetStav, mesenger.rozmer, mesenger.ucet, mesenger.ucetCenaKs, new DateTime(0));
                        if (poradi != -1)
                        {
                            if (karta.GetType() == typeof(detailSklad))  // zjisti typ karty
                            {
                                (dataGridView1.DataSource as DataTable).Rows.Add(poradi, "", mesenger.nazev, mesenger.jk, mesenger.ucetStav, mesenger.ucet, mesenger.csn, mesenger.din, mesenger.vyrobce, mesenger.rozmer, 0, mesenger.cenaKs, mesenger.ucetCena, mesenger.minStav, mesenger.poznamka, mesenger.ucetCenaKs);

                                int counter = dataGridView1.Rows.Count - 1;

                                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows[counter].Index;
                                dataGridView1.Refresh();
                                dataGridView1.CurrentCell = dataGridView1.Rows[counter].Cells[1];
                                dataGridView1.Rows[counter].Selected = true;

                            }
                        }


                    }
                }
            }
        }


        private void contextMenuEnable(Boolean useAdd, Boolean useInc = false, Boolean useDec = false, Boolean useLend = false, Boolean useMark = false, Boolean usePrint = false)
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


            if (useMark)
            {
                contextMenuStrip1.Items[2].Enabled = true;
                contextMenuStrip1.Items[2].Visible = true;
            }
            else
            {
                contextMenuStrip1.Items[2].Enabled = false;
                contextMenuStrip1.Items[2].Visible = false;
            }

            if (usePrint)
            {
                contextMenuStrip1.Items[4].Enabled = true;
                contextMenuStrip1.Items[4].Visible = true;
            }
            else
            {
                contextMenuStrip1.Items[4].Enabled = false;
                contextMenuStrip1.Items[4].Visible = false;
            }


            if (useInc)  // prijem
            {
                contextMenuStrip1.Items[6].Enabled = true;
                contextMenuStrip1.Items[6].Visible = true;
            }
            else
            {
                contextMenuStrip1.Items[6].Enabled = false;
                contextMenuStrip1.Items[6].Visible = false;
            }




            if (useDec)  // poskozeno
            {
                contextMenuStrip1.Items[7].Enabled = true;
                contextMenuStrip1.Items[7].Visible = true;
            }
            else
            {
                contextMenuStrip1.Items[7].Enabled = false;
                contextMenuStrip1.Items[7].Visible = false;
            }

            if ((!useDec) && (!useInc))
            {
                contextMenuStrip1.Items[5].Enabled = false;
                contextMenuStrip1.Items[5].Visible = false;
            }
            else
            {
                contextMenuStrip1.Items[5].Enabled = true;
                contextMenuStrip1.Items[5].Visible = true;
            }


            if (useLend)  // pujceno
            {
                contextMenuStrip1.Items[9].Enabled = true;
                contextMenuStrip1.Items[9].Visible = true;
                contextMenuStrip1.Items[8].Enabled = true;
                contextMenuStrip1.Items[8].Visible = true;

            }
            else
            {
                contextMenuStrip1.Items[9].Enabled = false;
                contextMenuStrip1.Items[9].Visible = false;
                contextMenuStrip1.Items[8].Enabled = false;
                contextMenuStrip1.Items[8].Visible = false;
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
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userHasAccessRightsWM(karta.myPermissions.addEnableCode))
            {
                karta.pridejKartu();
            }
        }

        private void ConMenuEditItem(object sender, EventArgs e)
        {
            //oprava polozky
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userHasAccessRightsWM(karta.myPermissions.editEnableCode))
            {

                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DBRow = getDBRowFromSelectedRow(DBRow);
                    karta.opravKartu(DBRow);
                }
            }
        }

        private void ConMenuDeleteItem(object sender, EventArgs e)
        {
            // smazani polozky
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userHasAccessRightsWM(karta.myPermissions.deleteEnableCode))
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DBRow = getDBRowFromSelectedRow(DBRow);
                    karta.zrusKartu(DBRow);
                }
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void conMenuAddMat(object sender, EventArgs e)
        {
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userHasAccessRightsWM(karta.myPermissions.prijemEnableCode))
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DBRow = getDBRowFromSelectedRow(DBRow);
                    karta.Prijem(DBRow);
                }
            }
        }

        private void conMenuDelMat_Click(object sender, EventArgs e)
        {
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userHasAccessRightsWM(karta.myPermissions.poskozeniEnableCode))
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DBRow = getDBRowFromSelectedRow(DBRow);
                    karta.Poskozeno(DBRow);
                }
            }
        }

        private void ConMenuPujcNaradi_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DBRow = getDBRowFromSelectedRow(DBRow);
                karta.Zapujceno(DBRow);
            }
        }

        private void zapujceniNaradi_Click(object sender, EventArgs e)
        {
            //Zapujceni vraceni poskozeni
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userHasAccessRightsWM((Int32)permCode.Prac))
            {
                labelView.Text = "Pracovníci provozu - Načítání";
                evenState = evenStateEnum.disable;
                Application.DoEvents();
                loadOsobyItems();
                karta = new detailOsobyZapujcNaradi(myDB, dataGridView1);
                evenState = evenStateEnum.enable;
                contextMenuEnable(true, false, false, true, false, true);
                labelView.Text = "Pracovníci provozu - Zapůjčení nářadí";
            }
        }


        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void ConMenuProhledávani_Click(object sender, EventArgs e)
        {
            karta.NastaveniHledani(this.Location.X + this.Size.Width, this.Top);
        }


        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control && e.KeyCode == Keys.P)
            {
                UzivatelData ud = UzivatelData.makeInstance();
                if (ud.userHasAccessRightsWM(karta.myPermissions.printEnableCode))
                {

                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        DBRow = getDBRowFromSelectedRow(DBRow);
                        karta.vytiskniKartu(DBRow);
                    }
                }
            }

            if (e.KeyData == Keys.F3)
            {
                karta.HledejDalsi(this.Location.Y + this.Size.Width, this.Top);
            }
            if (e.KeyData == Keys.Return)
            {

                Hashtable DBRowShow = getPoradiOscisloFromSelectedRow();
                if (DBRowShow != null)
                {
                    karta.zobrazKartu(DBRowShow);
                }

            }
        }

        private void IndexesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userIsAdminWM())
            {
                // vytvori nove pripojeni na mazani tabulek
                vDatabase localDB = OpenDataBaseHandle(false);
                localDB.openDB();
                if (localDB.DBIsOpened())
                {
                    labelView.Text = "Mažu indexi";
                    contextMenuDisable();
                    dataGridView1.DataSource = null;
                    karta = new detailNone(null, null);
                    Application.DoEvents();
                    localDB.DropIndexes();
                    labelView.Text = "Vytvořím indexi";
                    localDB.CreateIndexes();
                    labelView.Text = "";
                }
                localDB.closeDB();
                MessageBox.Show("Indexy jsou vytvořeny.");
            }
        }

        private void setStateChangeEvent(vDatabase myDB)
        {
            if (myDB != null)
            {
                if (myDB.myDBConn != null)
                {
                    myDB.myDBConn.StateChange += new StateChangeEventHandler(changesStateConnection);
                    setConnectionLabel(myDB.myDBConn.State);
                }
            }
        }


        void changesStateConnection(object sender, StateChangeEventArgs e)
        {
            setConnectionLabel(e.CurrentState);
        }


        private void setConnectionLabel(ConnectionState state)
        {
            switch (state)
            {
                case ConnectionState.Closed:
                    {
                        labelStateConnection.Text = "Spojeni je uzavřeno";
                        break;
                    }
                case ConnectionState.Broken:
                    {
                        labelStateConnection.Text = "Spojení je přerušeno";
                        break;
                    }
                case ConnectionState.Connecting:
                    {
                        labelStateConnection.Text = "Spojení se vytváří";
                        break;
                    }
                case ConnectionState.Open:
                    {
                        labelStateConnection.Text = "Spojení je navázáno";
                        break;
                    }
                default:
                    {
                        labelStateConnection.Text = "";
                        break;
                    }
            }
        }

        private void usersTest()
        {
            if (myDB.DBIsOpened())
            {
                if (!(myDB.tableUzivateleExist()))
                {
                    if (MessageBox.Show("Tabulka uživatelských účtů patrně neexistuje\n a bez ní program nemůže pracovat.\n"
                        + "Požadujete její vytvoření ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        vDatabase localDB = OpenDataBaseHandle(false);
                        localDB.openDB();
                        if (localDB.DBIsOpened())
                        {
                            localDB.CreateTableUzivatele();
                            localDB.closeDB();
                        }
                        if (!(myDB.tableUzivateleExist()))
                        {
                            MessageBox.Show("Tabulka uživatelských účtů patrně neexistuje, program bude ukončen.");
                            myDB.closeDB();
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        myDB.closeDB();
                        Environment.Exit(0);
                    }
                }

                // test pocet user admin
                if (!(myDB.tableUzivateleAdminExist()))
                {
                    if (MessageBox.Show("Neexistuje žádný AMINISTRÁTORSKÝ účet\n a bez něj program bohužel nemůže pracovat.\n"
                        + "Požadujete jeho vytvoření ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // vztvoření uctu
                        UzivatelKarta uk = new UzivatelKarta(myDB, dataGridView1.Font, true);
                        uk.ShowDialog();

                    }
                    else
                    {
                        myDB.closeDB();
                        Environment.Exit(0);
                    }

                }
                if (!(myDB.tableUzivateleAdminExist()))
                {
                    MessageBox.Show("Neexistuje žádný AMINISTRÁTORSKÝ účet, program bude ukončen.");
                    myDB.closeDB();
                    Environment.Exit(0);
                }

                UzivatelData ud = UzivatelData.makeInstance();
                ud.setDB(myDB);

                if (!(ud.login()))
                {
                    myDB.closeDB();
                    Environment.Exit(0);
                }
                labelUser.Text = ud.Jmeno + " " + ud.Prijmeni;
            }
        }



        private void globalSettingTest()
        {
            if (myDB.DBIsOpened())
            {
                if (!(myDB.tableNastaveniExist()))
                {
                    if (MessageBox.Show("Tabulka globálního nastavení patrně neexistuje\n a bez ní program nemůže aktivovat rozšířené funkce.\n"
                        + "Požadujete její vytvoření ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        vDatabase localDB = OpenDataBaseHandle(false);
                        localDB.openDB();
                        if (localDB.DBIsOpened())
                        {
                            localDB.CreateTableNastaveni();
                            localDB.closeDB();
                        }
                        if (!(myDB.tableNastaveniExist()))
                        {
                            MessageBox.Show("Tabulka globálního nastavení patrně neexistuje, program nemůže aktivovat rozšířené funkce.");
                        }
                    }
                }
            }
        }






        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            // sprava uzivatelských účtů
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userIsAdminWM())
            {
                SeznamUzivatelu spravaUzivatelu = new SeznamUzivatelu(myDB, this.Font);
                spravaUzivatelu.ShowDialog();
            }
        }


        private void písmoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UzivatelData ud = UzivatelData.makeInstance();

            // nastaveni pisma
            fontDialog1.Font = dataGridView1.Font;
            if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ConfigReg.saveSettingFont(fontDialog1.Font);
                dataGridView1.Font = fontDialog1.Font;
            }
        }

        private void zmenaHeslaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myDB.DBIsOpened())
            {
                // string userid =  Convert.ToString( dataGridView1.SelectedRows[0].Cells["userid"].Value);
                UzivatelData ud = UzivatelData.makeInstance();

                if (ud.userHasAccessRightsWM((Int32)permCode.PassSet))
                {

                    UzivatelZmenaHesla uzh = new UzivatelZmenaHesla(myDB, ud.userID);
                    uzh.ShowDialog();
                }
            }
        }

        private void NasaveniToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void Vydejna_FormClosing(object sender, FormClosingEventArgs e)
        {
            // zavreni hlavniho okna
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("Požadujete ukončení činnosti programu?", "Vydejna", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }

            }
        }

        private void Vydejna_SizeChanged(object sender, EventArgs e)
        {
            if (!(this.Size.IsEmpty)) ConfigReg.saveSettingWindowLocationSize("MAIN", 0, 0, this.Size.Width, this.Size.Height);
        }

        private void Vydejna_LocationChanged(object sender, EventArgs e)
        {
            if (!(this.Location.IsEmpty)) ConfigReg.saveSettingWindowLocationSize("MAIN", this.Location.X, this.Location.Y, 0, 0);
        }

        private void Vydejna_Shown(object sender, EventArgs e)
        {
            //           evenState = evenStateEnum.enable;
        }



        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (evenState == evenStateEnum.enable)
            {
                if (karta.GetType() != typeof(detailNone))
                {
                    if (karta.jmenoTabulky().Trim() != "")
                    {
                        ConfigReg.saveSettingWindowTableColumnWidth("MAIN", karta.jmenoTabulky(), e.Column.Name, e.Column.Width);
                    }
                }
            }
        }


        private void dataGridView1_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (evenState == evenStateEnum.enable)
            {
                if (karta.GetType() != typeof(detailNone))
                {
                    if (karta.jmenoTabulky() != "")
                    {
                        ConfigReg.saveSettingWindowTableColumnIndex("MAIN", karta.jmenoTabulky(), e.Column.Name, e.Column.DisplayIndex);
                    }
                }
            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {

        }

        private void comMenuChangeMark(object sender, EventArgs e)
        {
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userHasAccessRightsWM(karta.myPermissions.editChangeMarkCode))
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DBRow = getDBRowFromSelectedRow(DBRow);
                    karta.zmenZnacku(DBRow);
                }
            }
        }

        private void ukončeníProgramuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ConMenuPrintItem(object sender, EventArgs e)
        {
            // tisk
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userHasAccessRightsWM(karta.myPermissions.printEnableCode))
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DBRow = getDBRowFromSelectedRow(DBRow);
                    karta.vytiskniKartu(DBRow);
                }
            }
        }

        private void vyhodnoceniPoškozenekDleStřediskaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SestavaDefault sestava = new SestavaDefault(myDB, new StrategiePosStrediska(), dataGridView1.Font);
            sestava.ShowDialog();
        }

        private void vyhodnoceníToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SestavaDefault sestava = new SestavaDefault(myDB, new StrategiePosOsobyZaStred(), dataGridView1.Font);
            sestava.ShowDialog();

        }

        private void seznamZaPracovníkaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SestavaDefault sestava = new SestavaDefault(myDB, new StrategiePosZaOsobu(), dataGridView1.Font);
            sestava.ShowDialog();
        }

        private void vyhodnoceníDleZakázekVeStřediskuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SestavaDefault sestava = new SestavaDefault(myDB, new StrategiePosZakazkaZaStred(), dataGridView1.Font);
            sestava.ShowDialog();
        }

        private void vzhodnoceníDleZakázekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SestavaDefault sestava = new SestavaDefault(myDB, new StrategiePosZakazka(), dataGridView1.Font);
            sestava.ShowDialog();
        }

        private void vyhodnoceníDleKontaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SestavaDefault sestava = new SestavaDefault(myDB, new StrategiePosKonto(), dataGridView1.Font);
            sestava.ShowDialog();
        }

        private void seznamPoškozenekZaZakázkuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SestavaDefault sestava = new SestavaDefault(myDB, new StrategiePosZaZakazku(), dataGridView1.Font);
            sestava.ShowDialog();
        }

        private void seznamPoškozenekZaKontoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SestavaDefault sestava = new SestavaDefault(myDB, new StrategiePosZaKonto(), dataGridView1.Font);
            sestava.ShowDialog();
        }

        private void MenuItemUcetCena_Click(object sender, EventArgs e)
        {
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userIsAdminWM())
            {
                if (MenuItemUcetCena.Checked)
                {
                    DialogResult dialogResult = MessageBox.Show("Opravdu chcete zrušit použití Ůčetni ceny ?", "", MessageBoxButtons.YesNo);
                    if (dialogResult != DialogResult.Yes)
                    {
                        return;
                    }

                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Opravdu chcete aktivovat použití Ůčetni ceny ?", "", MessageBoxButtons.YesNo);
                    if (dialogResult != DialogResult.Yes)
                    {
                        return;
                    }
                }
                // aktivujeme / deaktuvujeme nastaveni databaze
                if (!(MenuItemUcetCena.Checked))
                {
                    if (!(myDB.tableNastaveniExist()))
                    {
                        MessageBox.Show ("Tabulka globálního nastavení patrně neexistuje\n a bez ní program nemůže aktivovat rozšířené funkce.");
                        return;
                    }
                }
                if (myDB.enableUcetCena(!(MenuItemUcetCena.Checked), ud.userID))
                {
                    // myDB.enableUcetCena (!(MenuItemUcetCena.Checked));

                    MenuItemUcetCena.Checked = myDB.getEnableUcetCena();
//                    MenuItemUcetCena.Checked = !(MenuItemUcetCena.Checked);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (myDB.DBIsOpened())
            {
                MenuItemUcetCena.Checked = myDB.getEnableUcetCena();
            }
            timer1.Start();
        }
    }
}
