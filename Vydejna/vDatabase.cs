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
// cislo       DBcislo      Os. Cislo    textBoxOsCislo        oscislo
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

        public virtual Int32 addLineVraceno(string DBjmeno, int DBcislo, string DBdilna, string DBpracoviste,
                                         string DBvyrobek, string DBnazev, string DBJK, string DBrozmer, int DBpocetks,                                         
                                         double DBcena, DateTime DBdate, string DBnormacsn, string DBkrjmeno,
                                         double DBcelkCena, string DBvevCislo, string DBkonto)
        {
            return 0;
        }


        public virtual Int32 addLinePoskozeno(string DBjmeno, int DBcislo, string DBdilna, string DBpracoviste,
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

        public virtual Boolean editNewLineVracene(Int32 poradi, string DBkrjmeno, string DBjmeno, string DBosCislo, string DBdilna,
                                         string DBprovoz, string DBnazev, string DBJK, long DBpocetKS,
                                         string DBrozmer, string DBCSN, decimal DBcena,
                                         DateTime DBdatum, string DBvyrobek, string DBkonto)
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


        public virtual Int32 addNewLineOsoby(string DBprijmeni, string DBjmeno, string DBulice, string DBmesto,
                                         string DBpsc, string DBtelHome, string DBosCislo, string DBstredisko,
                                         string DBcisZnamky, string DBoddeleni, string DBpracoviste, string DBtelZam,
                                         string DBpoznamka)
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
            return loadDataTable("SELECT poradi, nazev, jk,  pocetks, rozmer, csn,"
                                     + " cena, datum, vyrobek, konto, jmeno, krjmeno, cislo, dilna, pracoviste"
                                     + " from vraceno ORDER BY datum");
        }

        public virtual DataTable loadDataTablePoskozeno()
        {
            return loadDataTable("SELECT poradi, nazev, jk,  pocetks, rozmer, csn,"
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
