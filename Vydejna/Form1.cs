﻿using System;
using System.IO;
using System.IO.Packaging;
using System.IO.Compression;
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
        private ToolTip dbToolTip;
        private Object dataRowSearchSelectedID = null;
        private BindingSource mainBindingSource = null; 




        public Vydejna()
        {
            InitializeComponent();

            Font initFont = ConfigReg.loadSettingFontX("");
            if (initFont != null)
            {
                dataGridView1.Font = initFont;
            }

            labelView.Font = new Font(labelView.Font, FontStyle.Bold);
            labelUser.Text = "";
            labelDate.Text = "";


            dbToolTip = new ToolTip();
            dbToolTip.SetToolTip(labelStateConnection, "");

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

            mainBindingSource = new BindingSource();
            mainBindingSource.DataSource = null;
            dataGridView1.DataSource = mainBindingSource;
           
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            DBRow = new Hashtable();

            if (nastaveniDB.codeDB != (int)kodDB.dbNone)
            {
                myDB = OpenDataBaseHandle();
                myDB.openDB();
                setStateChangeEvent(myDB);
                dbToolTip.SetToolTip(labelStateConnection, myDB.getDBTypAndName());

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
            labelDate.Text = "";
            // nahraje hlavni tabulku - naradi - skladove karty
            Application.DoEvents();
            dataGridView1.Columns.Clear();
            dataRowSearchSelectedID = null;
            (dataGridView1.DataSource as BindingSource).DataSource = null;
            (dataGridView1.DataSource as BindingSource).ResetBindings(true);
            Application.DoEvents();

            if (myDB.DBIsOpened())
            {
                try
                {
                    (dataGridView1.DataSource as BindingSource).DataSource = myDB.loadDataTableNaradi();
                    (dataGridView1.DataSource as BindingSource).ResetBindings(true);
                    
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
            labelDate.Text = "";
            Application.DoEvents();
            dataGridView1.Columns.Clear();
            dataRowSearchSelectedID = null;
            (dataGridView1.DataSource as BindingSource).DataSource = null;
            (dataGridView1.DataSource as BindingSource).ResetBindings(true);

            Application.DoEvents();

            if (myDB.DBIsOpened())
            {
                try
                {
                    (dataGridView1.DataSource as BindingSource).DataSource = myDB.loadDataTableZruseno();
                    (dataGridView1.DataSource as BindingSource).ResetBindings(true);

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
            labelDate.Text = "";
            Application.DoEvents();
            dataGridView1.Columns.Clear();
            dataRowSearchSelectedID = null;
            (dataGridView1.DataSource as BindingSource).DataSource = null;
            (dataGridView1.DataSource as BindingSource).ResetBindings(true);
            Application.DoEvents();

            if (myDB.DBIsOpened())
            {

                VyberDatumu poskozenoDatum = new VyberDatumu();

                try
                {
                    System.Windows.Forms.DialogResult dateChooseResult = poskozenoDatum.ShowDialog();
                    DateTime dateFrom = poskozenoDatum.dateFromValue;
                    DateTime dateTo = poskozenoDatum.dateToValue;
                    poskozenoDatum.Dispose();
                    this.Refresh();

                    if (dateChooseResult == System.Windows.Forms.DialogResult.OK)
                    {
                        setDateLabel(dateFrom, dateTo);
                        (dataGridView1.DataSource as BindingSource).DataSource = myDB.loadDataTablePoskozenoDate(dateFrom, dateTo);
                        (dataGridView1.DataSource as BindingSource).ResetBindings(true);
                    }
                    else
                    {
                        labelDate.Text = "";
                        (dataGridView1.DataSource as BindingSource).DataSource = myDB.loadDataTablePoskozeno();
                        (dataGridView1.DataSource as BindingSource).ResetBindings(true);
                    }


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
            labelDate.Text = "";
            Application.DoEvents();
            dataGridView1.Columns.Clear();
            dataRowSearchSelectedID = null;
            (dataGridView1.DataSource as BindingSource).DataSource = null;
            (dataGridView1.DataSource as BindingSource).ResetBindings(true);
            Application.DoEvents();

            if (myDB.DBIsOpened())
            {

                VyberDatumu vracenoDatum = new VyberDatumu();

                try
                {
                    System.Windows.Forms.DialogResult dateChooseResult = vracenoDatum.ShowDialog();
                    DateTime dateFrom = vracenoDatum.dateFromValue;
                    DateTime dateTo = vracenoDatum.dateToValue;
                    vracenoDatum.Dispose();
                    this.Refresh();

                    if (dateChooseResult == System.Windows.Forms.DialogResult.OK)
                    {
                        setDateLabel(dateFrom, dateTo);
                        (dataGridView1.DataSource as BindingSource).DataSource = myDB.loadDataTableVracenoDate(dateFrom, dateTo);
                        (dataGridView1.DataSource as BindingSource).ResetBindings(true);
                    }
                    else
                    {
                        labelDate.Text = "";
                        (dataGridView1.DataSource as BindingSource).DataSource = myDB.loadDataTableVraceno();
                        (dataGridView1.DataSource as BindingSource).ResetBindings(true);
                    }


                    //                    dataGridView1.DataSource = myDB.loadDataTableVraceno();
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
            labelDate.Text = "";
            Application.DoEvents();
            dataGridView1.Columns.Clear();
            dataRowSearchSelectedID = null;
            (dataGridView1.DataSource as BindingSource).DataSource = null;
            (dataGridView1.DataSource as BindingSource).ResetBindings(true);
            Application.DoEvents();

            if (myDB.DBIsOpened())
            {
                try
                {
                    (dataGridView1.DataSource as BindingSource).DataSource = myDB.loadDataTableOsoby();
                    (dataGridView1.DataSource as BindingSource).ResetBindings(true);
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

                    (dataGridView1.DataSource as BindingSource).DataSource = null;
                    (dataGridView1.DataSource as BindingSource).ResetBindings(true);

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
            (dataGridView1.DataSource as BindingSource).DataSource = null;
            (dataGridView1.DataSource as BindingSource).ResetBindings(true);
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
                    (dataGridView1.DataSource as BindingSource).DataSource = null;
                    (dataGridView1.DataSource as BindingSource).ResetBindings(true);
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
                    (dataGridView1.DataSource as BindingSource).DataSource = null;
                    (dataGridView1.DataSource as BindingSource).ResetBindings(true);
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
                if (MessageBox.Show("Opravdu chcete vymazat/vyčistit tabulky v databázi?", "Vymazání tabulek", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    labelView.Text = "Čistím tabulky";
                    contextMenuDisable();
                    (dataGridView1.DataSource as BindingSource).DataSource = null;
                    (dataGridView1.DataSource as BindingSource).ResetBindings(true);
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
                contextMenuEnable(true, true, true, false, true, true, true);
                karta.provedUvodniSetrideni();
                selectAndFocusFirstRow();
                labelView.Text = "Výdejna nářadí přehled";
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
                contextMenuEnable(false, false, false, false, false, true);
                karta.provedUvodniSetrideni();
                selectAndFocusFirstRow();
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
                karta.provedUvodniSetrideni();
                selectAndFocusFirstRow();
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
                karta.provedUvodniSetrideni();
                selectAndFocusFirstRow();
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
                karta.provedUvodniSetrideni();
                selectAndFocusFirstRow();
                labelView.Text = "Poškozené nářadí";
            }
        }

        // NASTAVENI DATABAZE
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Boolean showPref = true;

            if ((myDB != null) && myDB.DBIsOpened())
            {
                UzivatelData ud = UzivatelData.makeInstance();
                if (!(ud.userIsAdminWM()))
                {
                    showPref = false;
                }
            }

            if (showPref)
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
                    dbToolTip.SetToolTip(labelStateConnection, myDB.getDBTypAndName());
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
                                ((dataGridView1.DataSource as BindingSource).DataSource as DataTable).Rows.Add(poradi, "", mesenger.nazev, mesenger.jk, mesenger.ucetStav, mesenger.ucet, mesenger.csn, mesenger.din, mesenger.vyrobce, mesenger.rozmer, 0, mesenger.cenaKs, mesenger.ucetCena, mesenger.minStav, mesenger.poznamka, mesenger.ucetCenaKs);

//                                int counter = dataGridView1.Rows.Count - 1;

//                                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows[counter].Index;
//                                dataGridView1.Refresh();
//                                dataGridView1.CurrentCell = dataGridView1.Rows[counter].Cells[1];
//                                dataGridView1.Rows[counter].Selected = true;

                                karta.SelectAndShowGridViewRow(poradi);


//                                dataGridView1.BeginInvoke((MethodInvoker)delegate()
//                                {
//                                    dataGridView1.Rows[counter].Selected = true;
//                                    dataGridView1.CurrentCell = dataGridView1[1, counter];
//                                });


                            }
                        }


                    }
                }
            }
        }


        private void contextMenuEnable(Boolean useAdd, Boolean useInc = false, Boolean useDec = false, Boolean useLend = false, Boolean useMark = false, Boolean usePrint = false, Boolean useCorrect = false)
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

            if (useCorrect)
            {
                contextMenuStrip1.Items[12].Enabled = true;
                contextMenuStrip1.Items[12].Visible = true;
                contextMenuStrip1.Items[13].Enabled = true;
                contextMenuStrip1.Items[13].Visible = true;
            }
            else
            {
                contextMenuStrip1.Items[12].Enabled = false;
                contextMenuStrip1.Items[12].Visible = false;
                contextMenuStrip1.Items[13].Enabled = false;
                contextMenuStrip1.Items[13].Visible = false;
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
                karta.provedUvodniSetrideni();
                selectAndFocusFirstRow();
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
                    (dataGridView1.DataSource as BindingSource).DataSource = null;
                    (dataGridView1.DataSource as BindingSource).ResetBindings(true);
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


        private void setDateLabel(DateTime dateFrom, DateTime dateTo)
        {
            labelDate.Text = "Od : " + dateFrom.ToShortDateString() + " do : " + dateTo.ToShortDateString();
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

                MenuItemUcetCena.Checked = myDB.getEnablePrumerUcetCena();
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
                SeznamUzivatelu spravaUzivatelu = new SeznamUzivatelu(myDB, dataGridView1.Font);
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
                ConfigReg.saveSettingFontX(fontDialog1.Font, "");
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


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (myDB.DBIsOpened())
            {
                MenuItemUcetCena.Checked = myDB.getEnablePrumerUcetCena();
            }
            timer1.Start();
        }

        private void MenuItemPrumerUcetCena_Click(object sender, EventArgs e)
        {
            if (myDB.DBIsOpened())
            {
                UzivatelData ud = UzivatelData.makeInstance();
                myDB.enablePrumerUcetCena(!(MenuItemUcetCena.Checked), ud.userID);
                MenuItemUcetCena.Checked = myDB.getEnablePrumerUcetCena();
            }



        }

        private void conMenuCorrectDate(object sender, EventArgs e)
        {
            // oprava chybnych dat

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
            }
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userHasAccessRightsWM(karta.myPermissions.addEnableCode))
            {
                karta.OpravChyby(DBRow);
            }
        }

        private void celkemPoškozenoZaPracovníkaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void ZamykaniStranekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userIsAdminWM())
            {
                vDatabase localDB = OpenDataBaseHandle(false);
                localDB.openDB();
                if (localDB.DBIsOpened())
                {
                    if (!localDB.ZamykaniStranek())
                        MessageBox.Show("Volba zamykání není podporovaná");

                    localDB.closeDB();
                    myDB = OpenDataBaseHandle();
                    myDB.openDB();
                    setStateChangeEvent(myDB);
                }
            }
        }

        private void ZamykaniRadkuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userIsAdminWM())
            {
                vDatabase localDB = OpenDataBaseHandle(false);
                localDB.openDB();
                if (localDB.DBIsOpened())
                {
                    if (!localDB.ZamykaniRadek())
                        MessageBox.Show("Volba zamykání není podporovaná");

                    localDB.closeDB();
                    myDB = OpenDataBaseHandle();
                    myDB.openDB();
                    setStateChangeEvent(myDB);
                }
            }
        }

        private void vyčistěníToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userIsAdminWM())
            {
                if (myDB.DBIsOpened())
                {
                    Int32 errCode = myDB.VycisteniTabulek();
                    if (errCode < 0)
                        if (errCode == -1)
                        {
                            MessageBox.Show("Vyčistěni tabulek není podporováno.");
                        }
                        else
                        {
                            MessageBox.Show("Vyčistěni tabulek se nezdařilo.");
                        }
                    else
                    {
                        MessageBox.Show("Tabulky byly vyčistěny.");
                    }

                    setStateChangeEvent(myDB);
                }
            }

        }

        private void ukončeníProgramuToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void oProgramuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox info = new AboutBox();
            info.ShowDialog();
        }


        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

            dataRowSearchSelectedID = null;
//            dataRowSearchSelectedIndex = -1;
            if (e.RowIndex == -1)
            {
                dataRowSearchSelectedID = karta.getIdOfSelectedGridViewRow();
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //trideni ukonceno
            if ((dataRowSearchSelectedID != null) && e.ListChangedType == ListChangedType.Reset)
            {
                dataGridView1.ClearSelection();
                // zavolame nastaveni
                karta.SelectAndShowGridViewRow(dataRowSearchSelectedID);
            }
        }

        private void selectAndFocusFirstRow()
        {
            dataGridView1.ClearSelection();
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.BeginInvoke((MethodInvoker)delegate()
                {
                    dataGridView1.Rows[0].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1[1, 0];
                });
            }
        }




        private void vytvořeníZálohyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UzivatelData ud = UzivatelData.makeInstance();
            if (ud.userIsAdminWM(false) || ud.userHasAccessRightsWM((Int32)permCode.ArchMake))
            {
                if (MessageBox.Show("Opravdu chcete archív všech dat?", "Archivace tabulek", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    vytvoreniArchivu();
                }
            }
        }

        private void vytvoreniArchivu()
        {
            labelView.Text = "Vytvoření archívu";
            Application.DoEvents();


            const int tableCount = 10;

            string[] sqlCommands = new string[tableCount];
            string[] tableName = new string[tableCount];

            sqlCommands[0] = "SELECT * from naradi order by poradi";
            tableName[0] = "naradi";
            sqlCommands[1] = "SELECT * from karta order by poradi";
            tableName[1] = "karta";
            sqlCommands[2] = "SELECT * from poskozeno order by poradi";
            tableName[2] = "poskozeno";
            sqlCommands[3] = "SELECT * from vraceno order by poradi";
            tableName[3] = "vraceno";
            sqlCommands[4] = "SELECT * from pujceno order by poradi";
            tableName[4] = "pujceno";
            sqlCommands[5] = "SELECT * from zmeny order by parporadi, poradi";
            tableName[5] = "zmeny";
            sqlCommands[6] = "SELECT * from osoby order by oscislo";
            tableName[6] = "osoby";
            sqlCommands[7] = "SELECT * from tabseq order by poradi";
            tableName[7] = "tabseq";
            sqlCommands[8] = "SELECT * from nastaveni order by setid";
            tableName[8] = "nastaveni";
            sqlCommands[9] = "SELECT * from uzivatele order by userid";
            tableName[9] = "uzivatele";

            string xmlPath = "";
            DialogResult result = folderBrowserDialogArchivace.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                xmlPath = folderBrowserDialogArchivace.SelectedPath;
            }

            string tempFilePath = xmlPath;

            Application.DoEvents();

            string DateTimeString = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour.ToString() + "." + DateTime.Now.Minute.ToString();

            xmlPath = xmlPath + "\\" + DateTimeString;
            {
                string packageFile = xmlPath + ".dta";
                if (File.Exists(packageFile))
                {
                    File.Delete(packageFile);
                }

                Application.DoEvents();

                progressBarMain.Style = ProgressBarStyle.Continuous;
                progressBarMain.Value = 0;
                progressBarMain.Maximum = tableCount;

                DbTransaction transaction;
                //              {
                int i = -1;
                transaction = myDB.transactionFactory();
                string fileName = "";
                try
                {
                    for (i = 0; i < tableCount; i++)
                    {
                        DataTable dtTable = myDB.loadDataTable(sqlCommands[i], transaction);
                        dtTable.TableName = tableName[i];
                        Application.DoEvents();

                        //                            string fileName = xmlPath + "\\" + tableName[i] + ".db";
                        fileName = tempFilePath + "\\" + tableName[i] + ".db";

                        dtTable.WriteXml(fileName, XmlWriteMode.WriteSchema);
                        Application.DoEvents();

                        addFileIntoPackage(fileName, packageFile);
                        File.Delete(fileName);
                        Application.DoEvents();

                        progressBarMain.Value = i + 1;
                    }
                    myDB.transactionCommit(transaction);
                }
                catch
                {
                    myDB.transactionRollback(transaction);
                    if (i < 0)
                    {
                        MessageBox.Show("Lituji. Nemohu archivovat.");
                    }
                    else
                    {
                        MessageBox.Show("Lituji. Nemohu archivovat tabulku " + tableName[i] + ".");
                    }
                    progressBarMain.Value = 0;
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                    return;
                }

            }
            progressBarMain.Value = 0;
            deleteAllDirectory(xmlPath);
            MessageBox.Show("Data jsou archivovaná.");
            //            }
            labelView.Text = "";
            Application.DoEvents();

        }




        private void deleteAllDirectory(string dir)
        {
            if (Directory.Exists(dir))
            {
                foreach (string file in Directory.GetFiles(dir))
                {
                    File.Delete(file);
                }
                foreach (string subdir in Directory.GetDirectories(dir))
                {
                    deleteAllDirectory(subdir);
                }
                Directory.Delete(dir);
            }
        }


        private void addFileIntoPackage(string fileNameWithPath, string packageFile)
        {
            FileInfo fi = new FileInfo(fileNameWithPath);

            if ((fi.Length > 0) && (fi.Extension == ".db"))
            {
                string filenameInPackage = ".\\" + fi.Name;
                using (Package package = Package.Open(packageFile, FileMode.OpenOrCreate))
                {
                    Uri partUriDocument = PackUriHelper.CreatePartUri(new Uri(filenameInPackage, UriKind.Relative));

                    PackagePart packagePartDocument = package.CreatePart(partUriDocument, System.Net.Mime.MediaTypeNames.Text.Xml);

                    using (FileStream fileStream = new FileStream(fileNameWithPath, FileMode.Open, FileAccess.Read))
                    {
                        CopyCompressStream(fileStream, packagePartDocument.GetStream());
                    }
                }
            }
        }



        private void CopyCompressStream(Stream source, Stream target)
        {
            using (GZipStream compressionStream = new GZipStream(target, CompressionMode.Compress))
            {
                CopyStream(source, compressionStream);
            }
        }

        private static void CopyStream(Stream source, Stream target)
        {
            const int bufSize = 0x1000;
            byte[] buf = new byte[bufSize];
            int bytesRead = 0;
            while ((bytesRead = source.Read(buf, 0, bufSize)) > 0)
                target.Write(buf, 0, bytesRead);
            target.Flush();
            target.Close();
        }

        private void obnovaDatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Opravdu nahrát archív všech dat?\nVšechna současná data budou smazána", "Archivace tabulek", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                nacteniArchivu(false);
            }
        }

        private void nacteniArchivu( Boolean useOnlySettingUser)
        {
            openArchiveFileDialog.Filter = "Archivni data (.dta) | *.dta";

            DialogResult result = openArchiveFileDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                Boolean makeUzivateleTable = false;
                Boolean makeOtherTable = true;
                if (useOnlySettingUser) makeOtherTable = false;

                if (!(useOnlySettingUser))
                {
                    if (MessageBox.Show("Chcete nahrat i seznam a prava uživatelů ?", "Archivace tabulek", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        makeUzivateleTable = true;
                    }
                }
                else
                {
                    makeUzivateleTable = true;
                }

                Application.DoEvents();
                string packageFile = openArchiveFileDialog.FileName;
                using (Package package = Package.Open(packageFile, FileMode.Open))
                {

                    progressBarMain.Style = ProgressBarStyle.Marquee;

                    (dataGridView1.DataSource as BindingSource).DataSource = null;
                    (dataGridView1.DataSource as BindingSource).ResetBindings(true);

                    progressBarMain.MarqueeAnimationSpeed = 100;
                    labelView.Text = "Čtení dat z archívu";
                    Application.DoEvents();

                    DataSet dset = new DataSet();
                    PackagePartCollection archiveCollection = package.GetParts();
                    foreach (PackagePart onePart in archiveCollection)
                    {
                        string name = onePart.Uri.OriginalString;
                        if (name.Length > 3)
                        {
                            string namePref = name.Substring(1, name.Length - 4);
                            string namePost = name.Substring(name.Length-2,2);
                            if (namePost == "db")
                            {
                                labelView.Text = "Čtení dat z archívu - tabulka : " + namePref ;
                                Application.DoEvents();

                                DataTable dTable = new DataTable();
                                dTable.TableName = namePref;
                                dset.Tables.Add(dTable);
                                //provedeme natazeni tabulky
                                Stream inStream = onePart.GetStream();
                                using (GZipStream compressionStream = new GZipStream(inStream, CompressionMode.Decompress))
                                {
                                    dTable.ReadXml(compressionStream);
                                }
                            }
                        }
                    }                   
                    package.Close();

                    progressBarMain.MarqueeAnimationSpeed = 0;
                    labelView.Text = "";
                    Application.DoEvents();

                    labelView.Text = "Ukladání dat do databaze - tabulka : ";
                    Application.DoEvents();
                    progressBarMain.MarqueeAnimationSpeed = 100;

                    Int32 saveError = myDB.saveDataSetToSQL(dset, labelView, makeUzivateleTable, makeOtherTable);
                    progressBarMain.MarqueeAnimationSpeed = 0;

                    progressBarMain.Style = ProgressBarStyle.Blocks;
                    progressBarMain.Value = 0;
                    labelView.Text = "";
                    Application.DoEvents();

                    if (saveError == 0)
                    {
                        MessageBox.Show("Přesun dat z archivu ukončen.");
                    }
                    else
                    {
                        if (saveError == -2)
                        {
                            MessageBox.Show("Databázi se nepodařilo otevřít.");
                        }
                        if (saveError == -1)
                        {
                            MessageBox.Show("Chyba při nahrávání dat do databáze.");
                        }

                    }
                }
            }

        }


        private void CopyUnCompressStream(Stream source, Stream target)
        {
            using (GZipStream compressionStream = new GZipStream(source, CompressionMode.Decompress))
            {
                CopyStream(compressionStream, target);
            }
        }

        private void údržbaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {

        }

        private void obnovaDatProNastaveníToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Opravdu nahrát z archívu nastavení uživatelů ?\nVaše současné nastavení /účty a hesla/ bude smazáno", "Archivace tabulek", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                nacteniArchivu(true);
            }

        }

        private void vyhodnoceniDlePoložkyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SestavaDefault sestava = new SestavaDefault(myDB, new StrategieStavZaPolozku(), dataGridView1.Font);
            sestava.ShowDialog();

        }

        private void stavToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



    }
}
