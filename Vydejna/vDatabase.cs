using System;
using System.Collections;
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
// cena       DBcena      Cena           numericUpDownCenaKs     ---  
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
// kodzmeny   DBkodzmeny             // zruseno v SQL
// druh       DBdruhp
// odpis      DBodpis    
// zavod      DBzavod                // zruseno v SQL
// ---- Nasledujici polozku neobsahuje tabulka karta
// ucetkscen  DBucetkscen Ucet. cena/KS  numericUpDownUcetCenaKs
// test       DBtest      // zruseno v SQL
// pomroz     DBpomroz    // zruseno v SQL                              // pomocny rozmer
// kdatum     DBkdatum
// kodd       DBkodd    KD


// Tabulka osoby
// tabulka    promenna      prehled      add/edit okno
//
// jmeno      DBjmeno       Jmeno        TextBoxJmeno
// prijmeni   DBprijmeni    Prijmeni     TextBoxJmeno
// ulice      DBulice       Ulice        TextBoxUlice
// mesto      DBmesto       Mesto        TextBoxMesto
// psc        DBpsc         Psc          TextBoxPsc
// telhome    DBtelHome     Tel. domu    TextBoxTelDomu
// oscislo    DBosCislo     Osobni cislo TextBoxOsCislo     // unikatni hodnota
// odeleni    DBodeleni     Provoz       TextBoxOddeleni
// telzam     DBtelzam      Tel. zamest. TextBoxTelZamest
// stredisko  DBstredisko   Stredisko    TextBoxStredisko
// pujsoub    DBpujSoub
// pracoviste DBpracoviste  Pracoviste   TextBoxPracoviste
// cisznamky  DBcisZnamky   CisloZnamky  TextBoxCisZnamky
// poznamka   DBpoznamka    Poznamky     TextBoxPoznamka


// Tabulka Poskozeno
// tabulka    promenna      prehled      add/edit okno         messenger
// jmeno       DBjmeno      Prijmeni     textBoxPrijmeni       prijmeni
// cislo -> oscislo      DBcislo      Os. Cislo    textBoxOsCislo        oscislo
// dilna       DBdilna      Stredisko    textBoxStredisko      stredisko
// pracoviste  DBpracoviste Provoz       textBoxProvoz         provoz
// vyrobek     FDBvyrobek    Zakazka      textBoxZakázka        zakazka
// nazev       DBnazev      Nazev        textBoxNazev          nazev
// jk          DBJK         Oznaceni JK  textBoxJK             jk                 //  unikatni hodnota
// rozmer      DBrozmer     Rozmer       textBoxRozmer         rozmer
// pocetks     DBpocetks    Vraceno KS   numericUpDownPocetKS  pocetKs 
// cena        DBcena       Cena         numericUpDownCena     cena
// datum       DBdate       Datum        dateTimePickerDatum   datum 
// csn         DBnormacsn   Norma CSN    textBoxCsn            csn
// krjmeno     DBkrjmeno    Jmeno        textBoxJmeno          jmeno
// celkcena    DBcelkCena
// vevcislo    DBvevCislo
// konto       DBkonto      Konto        textBoxKonto          konto





namespace Vydejna
{
    enum kodDB { dbNone = -1, dbSQLite, dbPostgresODBC, dbInformixODBC };
    
    enum defaultPortDBValue { SQLitePortDef = 0, postgresPortDef = 5432, informixPortDef = 9996};


    public delegate Hashtable getDBLineDlg(Int32 poradi, Hashtable DBRow);

    public delegate Boolean tableItemExistDelgInt(Int32 oc);

    public delegate Boolean tableItemExistDelgStr(string oc);

    public abstract class vDatabase
    {
        public static int[] defaultPortDB = new int[] { (int)defaultPortDBValue.SQLitePortDef, 
                                                (int)defaultPortDBValue.postgresPortDef,
                                                (int)defaultPortDBValue.informixPortDef};


        public static string[] defaultLocaleDB = new string[] { "", "", "cs_cz.8859-2" };
        public static string[] defaultDriverDB = new string[] { "", "PostgreSQL UNICODE", "IBM INFORMIX 3.82 32 BIT" };



        public bool dBConnectionState;
        public DbConnection myDBConn;
        public string dBName;
        public string dBuserName;
        public string dBuserPassword;
        public string dBServerAddress;
        public string dBServerName;
        public string dBPort;
        public string dBLocale;
        public string dBDriver;
        public string dBConnectStr;

        public vDatabase(string dataBaseName, string serverAdress, string serverName, string port, string locale, string driver, string userName, string password)
        {
            dBConnectionState = false;
            dBName = dataBaseName;
            dBPort = port;
            dBServerName = serverName;
            dBServerAddress = serverAdress;
            dBLocale = locale;
            dBDriver = driver;
            dBuserName = userName;
            dBuserPassword = password;
            
        }

        public bool DBIsOpened ()
        {
            if (myDBConn.State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public virtual void closeDB()
        {
            if (myDBConn.State == ConnectionState.Open)
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


        public virtual void CreateIndexes()
        {
        }

        public virtual void DropIndexes()
        {
        }


        public virtual void CreateTableUzivatele()
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
                                         string DBdruhp, string DBodpis)
        {
            return 0;
        }

        public virtual Int32 addLineNaradi(string DBnazev, string DBJK, string DBnormacsn, string DBnormadin,
                                         string DBvyrobce, double DBcena, string DBpoznamka, int DBminstav,
                                         double DBcelkcena, int DBucetstav, int DBfyzstav,
                                         string DBrozmer, string DBanalucet, DateTime DBdate, string DBstredisko,
                                         string DBdruhp, string DBodpis, double DBucetkscen, DateTime DBkdatum, string DBkodd)
        {
            return 0;
        }

        public virtual Int32 addLineVraceno(string DBjmeno, string DBosCislo, string DBdilna, string DBpracoviste,
                                         string DBvyrobek, string DBnazev, string DBJK, string DBrozmer, int DBpocetks,                                         
                                         double DBcena, DateTime DBdate, string DBnormacsn, string DBkrjmeno,
                                         double DBcelkCena, string DBvevCislo, string DBkonto)
        {
            return 0;
        }


        public virtual Int32 addLinePoskozeno(string DBjmeno, String DBosCislo, string DBdilna, string DBpracoviste,
                                         string DBvyrobek, string DBnazev, string DBJK, string DBrozmer, int DBpocetks,
                                         double DBcena, DateTime DBdate, string DBnormacsn, string DBkrjmeno,
                                         double DBcelkCena, string DBvevCislo, string DBkonto)
        {
            return 0;
        }

        public virtual void addLineOsoby(string DBprijmeni, string DBjmeno, string DBulice, string DBmesto,
                                         string DBpsc, string DBtelHome, string DBosCislo, string DBodeleni, string DBtelZam,
                                         string DBstredisko, string DBpusSoub, string DBpracoviste, string DBcisZnamky,
                                         string DBPoznamka)
        {
        }

        public virtual void addLineZmeny(int DBparPoradi, string DBpomocJK, DateTime DBdatum, string DBpoznamka, int DBPrijem,
                                         int DBvydej, int DBzustatek, string DBzapKarta, string DBvevCislo,
                                         int DBpocIvc, string DBstav, int DBporadi) 
        {
        }


        public virtual void addLinePujceno(int DBparPoradi, string DBosCislo, DateTime DBdatum, int DBks,
                                         string DBjmeno, string DBPrijmeni, string DBnazev, string DBjk,
                                         double DBcena, int DBzmPoradi)
        {
        }



        public virtual Int32 addNewLineNaradi(string DBnazev, string DBJK, string DBnormacsn, string DBnormadin,
                                         string DBvyrobce, decimal DBcena, string DBpoznamka, long DBminstav,
                                         decimal DBcelkcena, long DBucetstav, long DBfyzstav,
                                         string DBrozmer, string DBanalucet, decimal DBucetkscen, DateTime DBkdatum)
        {
            return -1;
        }




        public virtual Boolean editNewLineNaradi(Int32 poradi, string DBnazev, string DBJK, string DBnormacsn, string DBnormadin,
                                         string DBvyrobce, decimal DBcena, string DBpoznamka, long DBminstav,
                                         decimal DBcelkcena, long DBucetstav, long DBfyzstav,
                                         string DBrozmer, string DBanalucet, decimal DBucetkscen)
        {
            return false;
        }




        public virtual Boolean editNewLinePoskozene(Int32 poradi, string DBkrjmeno, string DBjmeno, string DBosCislo, string DBdilna,
                                         string DBprovoz, string DBnazev, string DBJK, long DBpocetKS,
                                         string DBrozmer, string DBCSN, decimal DBcena,
                                         DateTime DBdatum, string DBvyrobek, string DBkonto)
        {
            return false;
        }

        public virtual Boolean addNewLinePoskozene(string DBkrjmeno, string DBjmeno, string DBosCislo, string DBdilna,
                                         string DBprovoz, string DBnazev, string DBJK, long DBpocetKS,
                                         string DBrozmer, string DBCSN, decimal DBcena,
                                         DateTime DBdatum, string DBvyrobek, string DBkonto)
        {
            return false;
        }



        public virtual Boolean editNewLineVracene(Int32 poradi, string DBkrjmeno, string DBjmeno, string DBosCislo, string DBdilna,
                                         string DBprovoz, string DBnazev, string DBJK, long DBpocetKS,
                                         string DBrozmer, string DBCSN, decimal DBcena,
                                         DateTime DBdatum, string DBvyrobek, string DBkonto)
        {
            return false;
        }


        public virtual Boolean moveNaradiToNewKaret(Int32 DBporadi)
        {
            return false;
        }



        public virtual Boolean editNewLineKaret(Int32 poradi, string DBnazev, string DBJK, string DBnormacsn, string DBnormadin,
                                         string DBvyrobce, decimal DBcena, string DBpoznamka, long DBminstav,
                                         decimal DBcelkcena, long DBucetstav, long DBfyzstav,
                                         string DBrozmer, string DBanalucet)
        {
            return false;
        }


        public virtual Boolean deleteLineKaret(Int32 poradi)
        {
            return false;
        }



        public virtual Int32 addNewLineOsoby(string DBprijmeni, string DBjmeno, string DBulice, string DBmesto,
                                         string DBpsc, string DBtelHome, string DBosCislo, string DBstredisko,
                                         string DBcisZnamky, string DBoddeleni, string DBpracoviste, string DBtelZam,
                                         string DBpoznamka)
        {
            return -1;
        }


        public virtual Int32 addNewLineZmeny(Int32 DBporadi, string DBJK, DateTime DBdatum, Int32 DBprijem, Int32 DBvydej, string DBpoznamka, string DBstav, Int32 DBfyzStavZmena, Int32 DBucetStavZmena, string DBosCislo)
        {
            return -1;
        }


        public virtual Boolean editNewLineZmeny(Int32 DBParPoradi, Int32 DBPoradi, string DBPoznamka, string DBVevcislo)
        {
            return false;
        }


        //  vraci
        // 0 OK
        // -1 zapis se nepodaril
        // -2 stav skladu je mensi nez pozadovana vypujcka
        public virtual Int32 addNewLineZmenyAndPujceno(Int32 DBparPoradi, DateTime DBdatum, Int32 DBks, string DBpoznamka, string DBvevCislo, string DBosCislo)
        {
            return -1;
        }


        public virtual Int32 addNewLineZmenyAndVraceno(Int32 DBporadi, DateTime DBdatum, Int32 DBks, string DBpoznamka, string DBosCislo)
        {
            return -1;
        }



        public virtual Int32 addNewLineZmenyAndPoskozeno(Int32 DBporadi, string DBJK, DateTime DBdatum, Int32 DBvydej, string DBpoznamka,
                                                         string osCislo, string DBjmeno, string DBprijmeni, string DBstredisko, string DBprovoz,
                                                         string DBnazev, string DBrozmer, string DBkonto, double DBcena, double DBcelkCena, string DBcsn, string DBcisZak)
        {
            return -1;
        }



        public virtual Boolean editNewLineOsoby(string DBprijmeni, string DBjmeno, string DBulice, string DBmesto,
                                 string DBpsc, string DBtelHome, string DBosCislo, string DBstredisko,
                                 string DBcisZnamky, string DBoddeleni, string DBpracoviste, string DBtelZam,
                                 string DBpoznamka)
        {
            return false;
        }


        public virtual Int32 addNewLineUzivatele(string DBuserid, string DBpasswdHash, string DBjmeno, string DBprijmeni, string DBpermission,
                               Boolean admin)
        {
            return -1;
        }

        public virtual Int32 editNewLineUzivatele(string DBuserid, string DBjmeno, string DBprijmeni, string DBpermission,
                               Boolean DBadmin)
        {
            return -1;
        }

        public virtual Int32 editNewLinePasswordUzivatele(string DBuserid, string DBpasswdHash)
        {
            return -1;
        }


        public virtual Int32 deleteLineUzivatele(string DBuserid)
        {
            return -1;
        }


        public virtual DataTable loadDataTable(string DBSelect)
        {
            return null;
        }

        public virtual DataTable loadDataTableZmeny (Int32 poradi) //(string kodID)
        {
            return loadDataTable("SELECT poradi, datum, CASE WHEN stav = \'O\' THEN \'Poskozeno\'  WHEN stav = \'U\' THEN \'Půjčeno\' WHEN stav = \'V\' THEN \'Vyřazeno\' WHEN stav = \'P\' THEN \'Přijmuto\' WHEN stav = \'R\' THEN \'Vráceno\' END AS stav, poznamka, prijem, vydej, zustatek, zapkarta FROM zmeny WHERE parporadi = " + poradi.ToString() + " order by poradi"); // datum
        }

        public virtual DataTable loadDataTableVypujceno(Int32 poradi) //(string kodID)
        {
            return loadDataTable("SELECT datum, poznamka, prijem, vydej, zustatek, zapkarta FROM zmeny WHERE stav = \'U\' AND parporadi = " + poradi.ToString() + " order by datum");
        }

        public virtual DataTable loadDataTableVypujcenoNaOsobu(string osCislo) //(string kodID)
        {
            return loadDataTable( "SELECT z.datum, n.nazev, n.rozmer, n.jk, z.vydej, z.poznamka FROM zmeny z, naradi n " +
                                   "WHERE stav = \'U\' AND z.parporadi = n.poradi AND zapkarta =  \'" + osCislo.ToString() + "\' order by z.poradi");  // datum
        }

        public virtual DataTable loadDataTableVypujcenoNaOsobuNext(string osCislo) //(string kodID)
        {
            return loadDataTable("SELECT p.poradi as poradi, CASE WHEN p.zporadi > 0 THEN z.datum ELSE p.pdatum END as datum, " +
                                 "CASE WHEN p.nporadi < 1 THEN n.nazev ELSE p.pnazev END as nazev, CASE WHEN p.nporadi > 0 THEN n.rozmer ELSE \'\' END as rozmer, " +
                                  "CASE WHEN p.nporadi > 0 THEN n.jk ELSE p.pjk END as jk," +
                                  "CASE WHEN p.zporadi > 0 THEN z.vevcislo ELSE \'\' END as vevcislo, p.stavks, " +
//                                  "CASE WHEN p.zporadi > 0 THEN rtrim(z.vevcislo) ELSE \'\' END as vevcislo, p.stavks, " +
                                  "CASE WHEN p.nporadi > 0 THEN n.cena ELSE p.pcena END as cena, CASE WHEN p.zporadi > 0 THEN z.poznamka ELSE '' END as poznamka," +
                                  "p.oscislo, p.pjmeno, p.pprijmeni as pprijmeni, p.pnazev as pnazev, p.pjk as pjk, CASE WHEN p.zporadi > 0 THEN z.vydej ELSE p.pks END as pujcks, p.nporadi, p.zporadi " +
                                  "FROM pujceno p, naradi n, zmeny z where p.nporadi = n.poradi and p.zporadi = z.poradi and p.nporadi = z.parporadi " +
                                   "AND p.oscislo =  \'" + osCislo.ToString() + "\' order by p.poradi"); // datum


        }


        public virtual DataTable loadDataTableNaradi()
        {
            return loadDataTable("SELECT poradi, kodd, nazev, jk, fyzstav, analucet, normacsn,"
                                     + " normadin, vyrobce, rozmer, ucetstav, cena, celkcena, minimum,"
                                     + " poznamka, ucetkscen from naradi ORDER BY nazev, jk");
        }


        public virtual DataTable loadDataTableNaradiJednoduchy()
        {
            return loadDataTable("SELECT poradi, nazev, jk, rozmer,"
                                     + " fyzstav, poznamka, cena from naradi WHERE fyzstav > 0 ORDER BY nazev, jk");
        }

        public virtual DataTable loadDataPartTableNaradiNazev(string nazev)
        {
            return loadDataTable("SELECT nazev FROM naradi where nazev like \'" + nazev + "%\' ORDER BY nazev");
        }


        public virtual DataTable loadDataTableZruseno()
        {
            return loadDataTable("SELECT poradi, nazev, jk, ucetstav, analucet, normacsn,"
                                     + " normadin, vyrobce, rozmer, fyzstav, cena, celkcena, minimum,"
                                     + " poznamka from karta ORDER BY nazev, jk");
        }

        public virtual DataTable loadDataTableVraceno()
        {
            return loadDataTable("SELECT poradi, nazev, jk,  pocetks, rozmer, csn,"
                                     + " cena, datum, vyrobek, konto, jmeno, krjmeno, oscislo, dilna, pracoviste"
                                     + " from vraceno ORDER BY datum, poradi");
        }

        public virtual DataTable loadDataTablePoskozeno()
        {
            return loadDataTable("SELECT poradi, nazev, jk,  pocetks, rozmer, csn,"
                                     + " cena, datum, vyrobek, konto, jmeno, krjmeno, oscislo, dilna, pracoviste"
                                     + " from poskozeno ORDER BY datum, poradi");
        }



        public virtual DataTable loadDataTableOsoby()
        {
            return loadDataTable("SELECT prijmeni, jmeno, oscislo, odeleni, stredisko, pracoviste, cisznamky,"
                                     +"ulice, psc, mesto, telhome, telzam, poznamka from osoby order by prijmeni");

        }


        public virtual DataTable loadDataTableUzivatele()
        {
            return loadDataTable("SELECT jmeno, prijmeni, userid, admin from uzivatele order by prijmeni, jmeno");

        }


        public virtual Boolean tableUzivateleExist()
        {
            return false;
        }


        public virtual Boolean tableNaradiItemExist(string jk)
        {
          return  tableItemExist("select count(*) as countJK from naradi where jk = ?", jk);
        }

        public virtual Boolean tableZrusenoItemExist(string jk)
        {
            return tableItemExist("select count(*) as countJK from karta where jk = ?", jk);
        }


        public virtual Boolean tableOsobyItemExist(string oc)
        {
            return tableItemExist("select count(*) as countOC from osoby where oscislo = ?", oc);
        }

        public virtual Boolean tableVracenoItemExist(Int32 oc)
        {
            return tableItemExist("select count(*) as countOC from vraceno where poradi = ?", oc);
        }


        public virtual Boolean tablePoskozenoItemExist(Int32 oc)
        {
            return tableItemExist("select count(*) as countOC from poskozeno where poradi = ?", oc);
        }

        public virtual Boolean tablePujcenoExistOnNPoradi (Int32 oc)
        {
            return tableItemExist("select count(*) as countOC from pujceno where nporadi = ?", oc);

        }

        public virtual Boolean tableUzivateleAdminExist()
        {
            return tableItemExist("select count(*) as countOC from uzivatele where admin = ?", "A");
        }


        public virtual Boolean tableUzivateleItemExist(string userID)
        {
            return tableItemExist("select count(*) as countID from uzivatele where userid = ?", userID);
        }


        public virtual Hashtable getDBLine(string DBSelect, Hashtable DBRow)
        {
            return null;
        }

        // predefinovano povinne
        public virtual Hashtable getNaradiZmenyLine(Int32 poradi, Hashtable DBRow)
        {
            return null;
        }

        public virtual Hashtable getZmenyLine(Int32 parPoradi, Int32 poradi, Hashtable DBRow)
        {
            if (DBRow == null) DBRow = new Hashtable();
            string DBSelect = "SELECT poradi, datum, CASE WHEN stav = \'O\' THEN \'Poskozeno\'  WHEN stav = \'U\' THEN \'Půjčeno\' WHEN stav = \'V\' THEN \'Vyřazeno\' WHEN stav = \'P\' THEN \'Přijmuto\' WHEN stav = \'R\' THEN \'Vráceno\' END AS stav, poznamka, prijem, vydej, zustatek, zapkarta FROM zmeny WHERE parporadi = " + parPoradi.ToString() + " AND poradi = "+ poradi.ToString() + " order by datum";
            return getDBLine(DBSelect, DBRow);
        }



        public virtual Hashtable getNaradiLine(Int32 poradi, Hashtable DBRow)
        {
            if (DBRow == null) DBRow = new Hashtable();

            string DBSelect = "SELECT * from naradi WHERE poradi = " + poradi.ToString();
            return getDBLine(DBSelect, DBRow);
        }

        public virtual Hashtable getPoskozenoLine(Int32 poradi, Hashtable DBRow)
        {
            if (DBRow == null) DBRow = new Hashtable();

            string DBSelect = "SELECT * from poskozeno WHERE poradi = " + poradi.ToString();
            return getDBLine(DBSelect, DBRow);
        }

        public virtual Hashtable getVracenoLine(Int32 poradi, Hashtable DBRow)
        {
            if (DBRow == null) DBRow = new Hashtable();

            string DBSelect = "SELECT * from vraceno WHERE poradi = " + poradi.ToString();
            return getDBLine(DBSelect, DBRow);
        }


        public virtual Hashtable getZrusenoLine(Int32 poradi, Hashtable DBRow)
        {
            if (DBRow == null) DBRow = new Hashtable();

            string DBSelect = "SELECT * from karta WHERE poradi = " + poradi.ToString();
            return getDBLine(DBSelect, DBRow);
        }



        public virtual Hashtable getPujcenoLine(Int32 poradi, Hashtable DBRow)
        {
            if (DBRow == null) DBRow = new Hashtable();

            string DBSelect = "SELECT * from pujceno WHERE poradi = " + poradi.ToString();
            return getDBLine(DBSelect, DBRow);

        }


        public virtual Hashtable getOsobyLine(string oscislo, Hashtable DBRow)
        {
            if (DBRow == null) DBRow = new Hashtable();

            string DBSelect = "SELECT * from osoby WHERE oscislo = \'" + oscislo + "\'";
            return getDBLine(DBSelect, DBRow);

        }

        public virtual Hashtable getUzivateleLine(string userID, Hashtable DBRow)
        {
            if (DBRow == null) DBRow = new Hashtable();

            string DBSelect = "SELECT * from uzivatele WHERE userid = \'" + userID + "\'";
            return getDBLine(DBSelect, DBRow);

        }


        public Boolean tableItemExist(string DBSelect, string item)
        {

           Int64 itemRows =  countOfRows(DBSelect, item);
           if (itemRows == -1)
           {
               return false;  // chyba
           }
           else
           {
               if (itemRows == 0)
               {
                   return false; // nenalezeno
                }
               else
               {
                   return true;  // nalezeno
               }
           }
        }


        public Boolean tableItemExist(string DBSelect, Int32 item)
        {

            Int64 itemRows = countOfRows(DBSelect, item);
            if (itemRows == -1)
            {
                return false;  // chyba
            }
            else
            {
                if (itemRows == 0)
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

        public virtual Int64 countOfRows(string DBSelect, Int32 whileValue)
        {
            return -1;
        }


    }



}
