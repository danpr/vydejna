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


        public virtual Boolean moveNaraddiToNewKaret(Int32 DBporadi)
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



        public virtual DataTable loadDataTable(string DBSelect)
        {
            return null;
        }

        public virtual DataTable loadDataTableZmeny (Int32 poradi) //(string kodID)
        {
            return loadDataTable("SELECT datum, CASE WHEN stav = \'O\' THEN \'Poskozeno\'  WHEN stav = \'U\' THEN \'Půjčeno\' WHEN stav = \'V\' THEN \'Vyřazeno\' WHEN stav = \'P\' THEN \'Přijmuto\' WHEN stav = \'R\' THEN \'Vráceno\' END AS stav, poznamka, prijem, vydej, zustatek, zapkarta FROM zmeny WHERE parporadi = " + poradi.ToString() + " order by datum");
        }

        public virtual DataTable loadDataTableVypujceno(Int32 poradi) //(string kodID)
        {
            return loadDataTable("SELECT datum, poznamka, prijem, vydej, zustatek, zapkarta FROM zmeny WHERE stav = \'U\' AND parporadi = " + poradi.ToString() + " order by datum");
        }

        public virtual DataTable loadDataTableVypujcenoNaOsobu(string osCislo) //(string kodID)
        {
            return loadDataTable( "SELECT z.datum, n.nazev, n.rozmer, n.jk, z.vydej, z.poznamka FROM zmeny z, naradi n " +
                                   "WHERE stav = \'U\' AND z.parporadi = n.poradi AND zapkarta =  \'" + osCislo.ToString() + "\' order by z.datum");  
        }

        public virtual DataTable loadDataTableVypujcenoNaOsobuNext(string osCislo) //(string kodID)
        {
            
            return loadDataTable("SELECT p.poradi as poradi, CASE WHEN p.zporadi > 0 THEN z.datum ELSE p.pdatum END as datum, "+
                                 "CASE WHEN p.nporadi < 1 THEN n.nazev ELSE p.pnazev END as nazev, CASE WHEN p.nporadi > 0 THEN n.rozmer ELSE \'\' END as rozmer, " +
                                  "CASE WHEN p.nporadi > 0 THEN n.jk ELSE p.pjk END as jk, CASE WHEN p.zporadi > 0 THEN z.vydej ELSE p.pks END as ks, " +
                                  "CASE WHEN p.nporadi > 0 THEN n.cena ELSE p.pcena END as cena, CASE WHEN p.zporadi > 0 THEN z.poznamka ELSE '' END as poznamka," +
                                  "p.oscislo, p.pjmeno, p.pprijmeni as pprijmeni, p.pnazev as pnazev, p.pjk as pjk, p.nporadi, p.zporadi " +
                                  "FROM pujceno p, naradi n, zmeny z where p.nporadi = n.poradi and p.zporadi = z.poradi and p.nporadi = z.parporadi " +
                                   "AND p.oscislo =  \'" + osCislo.ToString() + "\' order by datum");



        }


        public virtual DataTable loadDataTableNaradi()
        {
            return loadDataTable("SELECT poradi, rtrim(kodd) as kodd, rtrim(nazev) as nazev, rtrim(jk) as jk, ucetstav, rtrim(analucet) as analucet, rtrim(normacsn) as normacsn,"
                                     + " rtrim(normadin) as normadin, rtrim(vyrobce) as vyrobce, rtrim(rozmer) as rozmer, fyzstav, cena, celkcena, minimum,"
                                     + " rtrim(poznamka) as poznamka, ucetkscen from naradi ORDER BY nazev"); 
        }


        public virtual DataTable loadDataTableNaradiJednoduchy()
        {
            return loadDataTable("SELECT poradi, rtrim(nazev) as nazev, rtrim(jk) as jk, rtrim(rozmer) as rozmer,"
                                     + " fyzstav, rtrim(poznamka) as poznamka from naradi WHERE fyzstav > 0 ORDER BY nazev");
        }


        public virtual DataTable loadDataTableZruseno()
        {
            return loadDataTable("SELECT poradi, rtrim(nazev) as nazev, rtrim(jk) as jk, ucetstav, rtrim(analucet) as analucet, rtrim(normacsn) as normacsn,"
                                     + " rtrim(normadin) as normadin, rtrim(vyrobce) as vyrobce, rtrim(rozmer) as rozmer, fyzstav, cena, celkcena, minimum,"
                                     + " rtrim(poznamka) as poznamka from karta ORDER BY nazev");
        }

        public virtual DataTable loadDataTableVraceno()
        {
            return loadDataTable("SELECT poradi, rtrim(nazev) as nazev, rtrim(jk) as jk,  pocetks, rtrim(rozmer) as rozmer, rtrim(csn) as csn,"
                                     + " cena, datum, rtrim(vyrobek) as vyrobek, rtrim(konto) as konto, rtrim(jmeno) as jmeno, rtrim(krjmeno) as krjmmeno, rtrim(oscislo) as oscislo, rtrim(dilna) as dilna, rtrim(pracoviste) as pracoviste"
                                     + " from vraceno ORDER BY datum");
        }

        public virtual DataTable loadDataTablePoskozeno()
        {
            return loadDataTable("SELECT poradi, rtrim(nazev) as nazev, rtrim(jk) as jk,  pocetks, rtrim(rozmer) as rozmer, rtrim(csn) as csn,"
                                     + " cena, datum, rtrim(vyrobek) as vyrobek, rtrim(konto) as konto, rtrim(jmeno) as jmeno, rtrim(krjmeno) as krjmeno, rtrim(oscislo) as oscislo, rtrim(dilna) as dilna, rtrim(pracoviste) as pracoviste"
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



        public virtual Hashtable getDBLine(string DBSelect, Hashtable DBRow)
        {
            return null;
        }

        public virtual Hashtable getNaradiZmenyLine(Int32 poradi, Hashtable DBRow)
        {
            if (DBRow == null) DBRow = new Hashtable();

            string DBSelect = "SELECT * from naradi WHERE poradi = " + poradi.ToString();
            return getDBLine(DBSelect, DBRow);

        }

        public virtual Hashtable getOsobyLine(string oscislo, Hashtable DBRow)
        {
            if (DBRow == null) DBRow = new Hashtable();

            string DBSelect = "SELECT * from osoby WHERE oscislo = \'" + oscislo + "\'";
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
