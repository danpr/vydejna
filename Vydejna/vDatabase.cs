using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SQLite;
using System.Windows.Forms;

//    tabulka naradi + karta

// tabulka    promenna    prehled   add/edit okno
//
// nazev      DBnazev     Nazev          textBoxNazev
// JK         DBJK        Oznaceni JK    textBoxJK
// normacsn   DBnormacsn  Norma ČSN      textBoxCsn
// normadin   DBnormadin  Norma DIN      textBoxDin 
// vyrobce    DBvyrobce   Vyrobce        textBoxVyrobce
// cena       DBcena      Cena           numericUpDownCenaKs
// poznamka   DBpoznamka  Poznamka       textBoxPoznamka
// minimum    DBminstav   Minimální stav numericUpDownMinStav
// celkcena   DBcelkcena  Celková cena   numericUpDownUcetCena
// adrdir     DBadresa
// movsoub    DBmov
// badrdir    DBbadresa
// bmovsoub   DBbmov
// cntrcode   DBctrl                                              (cntrcode)
// ucetstav   DBucetstav  KS/výdejna     numericUpDownUcetStav
// fyzstav    DBfyzstav   Fyzický stav
// rozmer     DBrozmer                   textBoxRozmer
// analucet   DBanalucet  Anal. účet     textBoxUcet.Text
// date       DBdate
// stredisko  DBstredisko
// kodzmeny   DBkodzmeny
// druh       DBdruhp
// odpis      DBodpis
// zavod      DBzavod
// ---- Nasledujici polozku neobsahuje tabulka karta
// ucetkscen  DBucetkscen Ucet. cena/KS  numericUpDownUcetCenaKs
// test       DBtest
// pomroz     DBpomroz                                  // pomocny rozmer
// kdatum     DBkdatum
// kodd       DBkodd    KD


namespace Vydejna
{
    enum kodDB { dbNone = -1, dbSQLite, dbPostgresODBC, dbInformixODBC };
    enum defaultPortDBValue { SQLitePortDef = 0, postgresPortDef = 5432, informixPortDef = 9996};



    public abstract class vDatabase
    {
        public static int[] defaultPortDB = new int[] { (int)defaultPortDBValue.SQLitePortDef, 
                                                (int)defaultPortDBValue.postgresPortDef,
                                                (int)defaultPortDBValue.informixPortDef};


        //public static int[] defaultPortDB = new int[] { 0, 5432, 9996 };

        public bool dBConnectionState;
        public DbConnection myDBConn;
        public string dBName;
        public string dBuserName;
        public string dBuserPassword;
        public string dBServer;
        public string dbPort;
        public string dBConnectStr;

        public vDatabase(string dataBaseName, string serverName, string port, string userName, string password)
        {
            dBConnectionState = false;
            dBName = dataBaseName;
            dBServer = port;
            dBServer = serverName;
            dBuserName = userName;
            dBuserPassword = password;
        }

        public bool DBIsOpened ()
        {
            return dBConnectionState;
        }


        public virtual void closeDB()
        {
            if (dBConnectionState == true)
            {
                myDBConn.Close();
                myDBConn.Dispose();
                dBConnectionState = false;
            }
        }

        public abstract void openDB();

        public virtual void DropTables()
        {
        }
 
        public virtual void CreateTables()
        {
        }

        public virtual void DeleteTables()
        {
        }

        public virtual DbTransaction startTransaction()
        {
            return null;   
        }


        public virtual void stopTransaction(DbTransaction transaction)
        {
        }


        public virtual void fillDataGridView(string selectLine, DataGridView myDGV)
        {
        }

        public virtual Int32 addLineKaret(string DBnazev,string DBJK, string DBnormacsn, string DBnormadin,
                                         string DBvyrobce, double DBcena, string DBpoznamka, int DBminstav, 
                                         double DBcelkcena, int DBucetstav, int DBfyzstav, 
                                         string DBrozmer, string DBanalucet, DateTime DBdate, string DBstredisko,
                                         string DBkodzmeny, string DBdruhp, string DBodpis, string DBzavod)
        {
            return 0;
        }

        public virtual Int32 addLineNaradi(string DBnazev, string DBJK, string DBnormacsn, string DBnormadin,
                                         string DBvyrobce, double DBcena, string DBpoznamka, int DBminstav,
                                         double DBcelkcena, int DBucetstav, int DBfyzstav,
                                         string DBrozmer, string DBanalucet, DateTime DBdate, string DBstredisko,
                                         string DBkodzmeny, string DBdruhp, string DBodpis, string DBzavod,
                                         double DBucetkscen, string DBtest, string DBpomroz, DateTime DBkdatum, string DBkodd)
        {
            return 0;
        }

        public virtual void addLineVraceno(string DBjmeno, int DBcislo, string DBdilna, string DBpracoviste,
                                         string DBvyrobek, string DBnazev, string DBJK, string DBrozmer, int DBpocetks,                                         
                                         double DBcena, DateTime DBdate, string DBnormacsn, string DBkrjmeno,
                                         double DBcelkCena, string DBvevCislo, string DBkonto)
        {
        }


        public virtual void addLinePoskozeno(string DBjmeno, int DBcislo, string DBdilna, string DBpracoviste,
                                         string DBvyrobek, string DBnazev, string DBJK, string DBrozmer, int DBpocetks,
                                         double DBcena, DateTime DBdate, string DBnormacsn, string DBkrjmeno,
                                         double DBcelkCena, string DBvevCislo, string DBkonto)
        {
        }

        public virtual void addLineOsoby(string DBprijmeni, string DBjmeno, string DBulice, string DBmesto,
                                         string DBpsc, string DBtelHome, string DBosCislo, string DBodeleni, string DBtelZam,
                                         string DBstredisko, string DBpusSoub, string DBpracoviste, string DBcisZnamky,
                                         string DBPoznamka)
        {
        }

        public virtual void addLineZmeny(int DBparPoradi, string DBpomocJK, DateTime DBdatum, string DBpoznamka, int DBPrijem,
                                         int DBvydej, int DBzustatek, string DBzapKarta, string DBvevCislo,
                                         int DBpocIvc, string DBcontrCod, string DBdosudNvrc, string DBprijTyp,
                                         string DBvydejTyp, int DBporadi, string DBstav) //, string DBnazev, string DBvyber,
//                                         string DBlastSoub, string DBaktAdr, double DBcena, double DBucetKsCen, string DBjk)
        {
        }


        public virtual Int32 addNewLineNaradi(string DBnazev, string DBJK, string DBnormacsn, string DBnormadin,
                                         string DBvyrobce, decimal DBcena, string DBpoznamka, long DBminstav,
                                         decimal DBcelkcena, long DBucetstav, long DBfyzstav,
                                         string DBrozmer, string DBanalucet, decimal DBucetkscen, DateTime DBkdatum)
        {
            return -1;
        }


        public virtual DataTable loadDataTable(string DBSelect)
        {
            return null;
        }

        public virtual DataTable loadDataTableZmeny (Int32 poradi) //(string kodID)
        {
            return loadDataTable("SELECT datum, poznamka, prijem, vydej, zustatek FROM zmeny WHERE parporadi = " + poradi.ToString());
        }



        public virtual DataTable loadDataTableNaradi()
        {
            return loadDataTable("SELECT poradi, kodd, nazev, jk, ucetstav, analucet, normacsn,"
                                     + " normadin, vyrobce, rozmer, fyzstav, cena, celkcena, minimum,"
                                     + " poznamka, ucetkscen from naradi ORDER BY nazev"); 
        }



        public virtual DataTable loadDataTableZruseno()
        {
            return loadDataTable("SELECT poradi, nazev, jk, ucetstav, analucet, normacsn,"
                                     + " normadin, vyrobce, rozmer, fyzstav, cena, celkcena, minimum,"
                                     + " poznamka from karta ORDER BY nazev");
        }

        public virtual DataTable loadDataTableVraceno()
        {
            return loadDataTable("SELECT nazev, jk,  pocetks, rozmer, csn,"
                                     + " cena, datum, vyrobek, konto, jmeno, krjmeno, cislo, dilna, pracoviste"
                                     + " from vraceno ORDER BY datum");
        }

        public virtual DataTable loadDataTablePoskozeno()
        {
            return loadDataTable("SELECT nazev, jk,  pocetks, rozmer, csn,"
                                     + " cena, datum, vyrobek, konto, jmeno, krjmeno, cislo, dilna, pracoviste"
                                     + " from poskozeno ORDER BY datum");
        }



        public virtual DataTable loadDataTableOsoby()
        {
            return loadDataTable("SELECT prijmeni, jmeno, oscislo, odeleni, stredisko, pracoviste, cisznamky,"
                                     +"ulice, psc, mesto, telhome, telzam, poznamka from osoby order by prijmeni");

        }


        public virtual Boolean tableNaradiItemExist(string jk)
        {
          return  tableItemExist("select count(*) as countJK from naradi where jk = ?", jk);
        }


        public Boolean tableItemExist(string DBSelect, string jk)
        {

           Int64 jkRows =  countOfRows(DBSelect, jk);
           if (jkRows == -1)
           {
               return false;  // chyba
           }
           else
           {
               if (jkRows == 0)
               {
                   return false; // nenalezeno
                }
               else
               {
                   return true;  // nalezeno
               }
           }
        }


        public virtual Int64 countOfRows(string DBSelect, string whileValue)
        {
            return -1;
        }
    }



}
