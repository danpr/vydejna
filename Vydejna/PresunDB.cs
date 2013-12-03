using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Collections;



namespace Vydejna
{
    class PresunDB
    {


    public static void presunVyrazene(vDatabase myDB, string filepath, Hashtable DBJoin)
    {
        DbTransaction myTransaction =  myDB.startTransaction();


        OleDbConnection fbase = new OleDbConnection("Provider=VFPOLEDB.1;CodePage=437;Data Source=" + filepath + ";Exclusive=false;Nulls=false;Collating Sequence=general");
        fbase.Open();
        OleDbCommand fbaseCom = new OleDbCommand("SELECT * FROM " + filepath + "\\DATA\\AR_KARET.DBF",fbase);
        OleDbDataReader dr = fbaseCom.ExecuteReader();
        if (dr.HasRows)
        {
                    string DBnazev;
                    string DBJK;
                    string DBnormaCSN;
                    string DBnormaDIN;
                    string DBvyrobce;
                    double DBcena;
                    string DBpoznamka;
                    int DBminStav;
                    double DBcelkCena;
                    string DBadresa;
                    string DBmov;
                    string DBbAdresa;
                    string DBbMov;
                    string DBcontr;
                    int DBucetStav;
                    int DBfyzStav;
                    string DBrozmer;
                    string DBanalUcet;
                    DateTime DBtDate;
                    string DBstredisko;
                    string DBkodZmeny;
                    string DBdruhP;
                    string DBodpis;
                    string DBzavod;
                    byte[] DBByte;
                    DBByte =  new byte[100]; 

                    while (dr.Read())
                    {
                        if (dr.IsDBNull(0)) DBnazev = ""; else DBnazev = fbaseToUtf16(dr.GetString(0).Trim() );

                        if (dr.IsDBNull(1)) DBJK = ""; else DBJK = dr.GetString(1).Trim();
                        if (dr.IsDBNull(2)) DBnormaCSN = ""; else DBnormaCSN = fbaseToUtf16(dr.GetString(2).Trim());
                        if (dr.IsDBNull(3)) DBnormaDIN = ""; else DBnormaDIN = fbaseToUtf16(dr.GetString(3).Trim());
                        if (dr.IsDBNull(4)) DBvyrobce = ""; else DBvyrobce = fbaseToUtf16(dr.GetString(4).Trim());
                        if (dr.IsDBNull(5)) DBcena = 0; else DBcena = Convert.ToDouble( dr.GetDecimal(5));
                        if (dr.IsDBNull(6)) DBpoznamka = ""; else DBpoznamka = fbaseToUtf16(dr.GetString(6).Trim());
                        if (dr.IsDBNull(7)) DBminStav = 0; else DBminStav = Convert.ToInt32(dr.GetDecimal(7));
                        if (dr.IsDBNull(8)) DBcelkCena = 0; else DBcelkCena = Convert.ToDouble(dr.GetDecimal(8));
                        if (dr.IsDBNull(9)) DBadresa = ""; else DBadresa = fbaseToUtf16(dr.GetString(9).Trim());
                        if (dr.IsDBNull(10)) DBmov = ""; else DBmov = fbaseToUtf16(dr.GetString(10).Trim());

                        if (dr.IsDBNull(11)) DBbAdresa = ""; else DBbAdresa = fbaseToUtf16(dr.GetString(11).Trim());
                        if (dr.IsDBNull(12)) DBbMov = ""; else DBbMov = fbaseToUtf16(dr.GetString(12).Trim());
                        if (dr.IsDBNull(13)) DBcontr = ""; else DBcontr = fbaseToUtf16(dr.GetString(13).Trim());
                        if (dr.IsDBNull(14)) DBucetStav = 0; else DBucetStav = Convert.ToInt32(dr.GetDecimal(14));
                        if (dr.IsDBNull(15)) DBfyzStav = 0; else DBfyzStav = Convert.ToInt32(dr.GetDecimal(15));
                        if (dr.IsDBNull(16)) DBrozmer = ""; else DBrozmer = fbaseToUtf16(dr.GetString(16).Trim());
                        if (dr.IsDBNull(17)) DBanalUcet = ""; else DBanalUcet = fbaseToUtf16(dr.GetString(17).Trim());
                        if (dr.IsDBNull(18)) DBtDate = new DateTime(0); else DBtDate = dr.GetDateTime(18);
                        if (dr.IsDBNull(19)) DBstredisko = ""; else DBstredisko = fbaseToUtf16(dr.GetString(19).Trim());
                        if (dr.IsDBNull(20)) DBkodZmeny = ""; else DBkodZmeny = fbaseToUtf16(dr.GetString(20).Trim());
                        if (dr.IsDBNull(21)) DBdruhP = ""; else DBdruhP = fbaseToUtf16(dr.GetString(21).Trim());
                        if (dr.IsDBNull(22)) DBodpis = ""; else DBodpis = fbaseToUtf16(dr.GetString(22).Trim());
                        if (dr.IsDBNull(23)) DBzavod = ""; else DBzavod = fbaseToUtf16(dr.GetString(23).Trim());

                       Int32 poradi = myDB.addLineKaret(DBnazev, DBJK, DBnormaCSN, DBnormaDIN, DBvyrobce, DBcena, DBpoznamka,
                            DBminStav, DBcelkCena, DBucetStav,
                            DBfyzStav, DBrozmer, DBanalUcet, DBtDate, DBstredisko, DBkodZmeny,
                            DBdruhP, DBodpis, DBzavod);

                        if (DBcontr.Trim() != "")
                        {
                            if (DBJoin.ContainsKey(DBadresa + ":" + DBmov + ":" + DBcontr))
                            {
                                MessageBox.Show("Byla nalezana polozka se stejnym referencnim klicem - chyba dat. - vyrazene");
                            }
                            else
                            {
                                DBJoin.Add( DBadresa+":"+DBmov+":"+DBcontr, poradi);
                            }
                        }
                        Application.DoEvents();




                        if ((DBadresa != DBbAdresa) || (DBmov != DBbMov))
                        {
                            MessageBox.Show("Byla nalezana polozka s rozdilnou adresou - chyba dat. - vyrazene");
                        }
                     }
              }
              fbase.Close();
              fbase.Dispose();
              if (myTransaction != null)

              {
                  myDB.stopTransaction(myTransaction);
              }

       }


    public static void presunNaradi(vDatabase myDB, string filepath, Hashtable DBJoin)
    {


        DbTransaction myTransaction = myDB.startTransaction();
        OleDbConnection fbase = new OleDbConnection("Provider=VFPOLEDB.1;Data Source=" + filepath + ";Exclusive=false;Nulls=false;Collating Sequence=general");
        fbase.Open();
        OleDbCommand fbaseCom = new OleDbCommand("SELECT * FROM " + filepath + "\\DATA\\NARADI.DBF", fbase);
        OleDbDataReader dr = fbaseCom.ExecuteReader();
        if (dr.HasRows)
        {
            string DBnazev;
            string DBJK;
            string DBnormaCSN;
            string DBnormaDIN;
            string DBvyrobce;
            double DBcena;
            string DBpoznamka;
            int DBminStav;
            double DBcelkCena;
            string DBadresa;
            string DBmov;
            string DBbAdresa;
            string DBbMov;
            string DBcontr;
            int DBucetStav;
            int DBfyzStav;
            string DBrozmer;
            string DBanalUcet;
            DateTime DBtDate;
            string DBstredisko;
            string DBkodZmeny;
            string DBdruhP;
            string DBodpis;
            string DBzavod;
            DateTime DBkDatum;
            double DBucetkscen;
            string DBtest;
            string DBpomRoz;
            string DBkodD;


            while (dr.Read())
                {
                if (dr.IsDBNull(0)) DBnazev = ""; else DBnazev = fbaseToUtf16(dr.GetString(0).Trim());
                if (dr.IsDBNull(1)) DBJK = ""; else DBJK = dr.GetString(1).Trim();
                if (dr.IsDBNull(2)) DBnormaCSN = ""; else DBnormaCSN = fbaseToUtf16(dr.GetString(2).Trim());
                if (dr.IsDBNull(3)) DBnormaDIN = ""; else DBnormaDIN = fbaseToUtf16(dr.GetString(3).Trim());
                if (dr.IsDBNull(4)) DBvyrobce = ""; else DBvyrobce = fbaseToUtf16(dr.GetString(4).Trim());
                if (dr.IsDBNull(5)) DBcena = 0; else DBcena = Convert.ToDouble(dr.GetDecimal(5));
                if (dr.IsDBNull(6)) DBpoznamka = ""; else DBpoznamka = fbaseToUtf16(dr.GetString(6).Trim());
                if (dr.IsDBNull(7)) DBminStav = 0; else DBminStav = Convert.ToInt32(dr.GetDecimal(7));
                if (dr.IsDBNull(8)) DBcelkCena = 0; else DBcelkCena = Convert.ToDouble(dr.GetDecimal(8));
                if (dr.IsDBNull(9)) DBadresa = ""; else DBadresa = fbaseToUtf16(dr.GetString(9).Trim());
                if (dr.IsDBNull(10)) DBmov = ""; else DBmov = fbaseToUtf16(dr.GetString(10).Trim());
                if (dr.IsDBNull(11)) DBbAdresa = ""; else DBbAdresa = fbaseToUtf16(dr.GetString(11).Trim());
                if (dr.IsDBNull(12)) DBbMov = ""; else DBbMov = fbaseToUtf16(dr.GetString(12).Trim());
                if (dr.IsDBNull(13)) DBcontr = ""; else DBcontr = fbaseToUtf16(dr.GetString(13).Trim());
                if (dr.IsDBNull(14)) DBucetStav = 0; else DBucetStav = Convert.ToInt32(dr.GetDecimal(14));
                if (dr.IsDBNull(15)) DBfyzStav = 0; else DBfyzStav = Convert.ToInt32(dr.GetDecimal(15));
                if (dr.IsDBNull(16)) DBrozmer = ""; else DBrozmer = fbaseToUtf16(dr.GetString(16).Trim());
                if (dr.IsDBNull(17)) DBanalUcet = ""; else DBanalUcet = fbaseToUtf16(dr.GetString(17).Trim());
                if (dr.IsDBNull(18)) DBtDate = new DateTime(0); else DBtDate = dr.GetDateTime(18);
                if (dr.IsDBNull(19)) DBstredisko = ""; else DBstredisko = fbaseToUtf16(dr.GetString(19).Trim());
                if (dr.IsDBNull(20)) DBkodZmeny = ""; else DBkodZmeny = dr.GetString(20).Trim();
                if (dr.IsDBNull(21)) DBdruhP = ""; else DBdruhP = fbaseToUtf16(dr.GetString(21).Trim());
                if (dr.IsDBNull(22)) DBodpis = ""; else DBodpis = fbaseToUtf16(dr.GetString(22).Trim());
                if (dr.IsDBNull(23)) DBzavod = ""; else DBzavod = fbaseToUtf16(dr.GetString(23).Trim());
                if (dr.IsDBNull(24)) DBucetkscen = 0; else DBucetkscen = Convert.ToDouble(dr.GetDecimal(24));
                if (dr.IsDBNull(25)) DBtest = ""; else DBtest = fbaseToUtf16(dr.GetString(25).Trim());
                if (dr.IsDBNull(26)) DBpomRoz = ""; else DBpomRoz = fbaseToUtf16(dr.GetString(26).Trim());
                if (dr.IsDBNull(27)) DBkDatum = new DateTime(0); else DBkDatum = dr.GetDateTime(27);
                if (dr.IsDBNull(28)) DBkodD = ""; else DBkodD = fbaseToUtf16(dr.GetString(28).Trim());

                Int32 poradi = myDB.addLineNaradi(DBnazev, DBJK, DBnormaCSN, DBnormaDIN, DBvyrobce, DBcena, DBpoznamka,
                     DBminStav, DBcelkCena, DBucetStav,
                     DBfyzStav, DBrozmer, DBanalUcet, DBtDate, DBstredisko, DBkodZmeny,
                     DBdruhP, DBodpis, DBzavod, DBucetkscen, DBtest, DBpomRoz, DBkDatum, DBkodD);

                if (DBcontr.Trim() != "")
                {
                    if (DBJoin.ContainsKey(DBadresa + ":" + DBmov + ":" + DBcontr))
                    {
                        MessageBox.Show("Byla nalezana polozka se stejnym referencnim klicem - chyba dat - naradi.");
                    }
                    else
                    {
                        DBJoin.Add(DBadresa+":"+DBmov+":"+DBcontr, poradi);
                    }
                }

                if ((DBadresa != DBbAdresa) || (DBmov != DBbMov))
                {
                    MessageBox.Show("Byla nalezana polozka s rozdilnou adresou - chyba dat. - vyrazene");
                }                
                Application.DoEvents();
            }
        }
        fbase.Close();
        fbase.Dispose();
        if (myTransaction != null)
        {
            myDB.stopTransaction(myTransaction);
        }
    }


    public static void presunVracene(vDatabase myDB, string filepath)
    {
        DbTransaction myTransaction = myDB.startTransaction();
        OleDbConnection fbase = new OleDbConnection("Provider=VFPOLEDB.1;CodePage=437;Data Source=" + filepath + ";Exclusive=false;Nulls=false");
        fbase.Open();
        OleDbCommand fbaseCom = new OleDbCommand("SELECT * FROM " + filepath + "\\DATA\\VRACSKL.DBF", fbase);
        OleDbDataReader dr = fbaseCom.ExecuteReader();
        if (dr.HasRows)
        {
            string DBnazev;
            string DBJK;
            string DBnormaCSN;
            string DBjmeno;
            string DBcislo;
            string DBdilna;
            string DBpracoviste;
            string DBvyrobek;
            int DBpocetks;
            string DBkrJmeno;
            string DBvevCislo;
            string DBkonto;
            string DBrozmer;
            double DBcelkCena;
            double DBcena;
            DateTime DBkDatum;

            while (dr.Read())
            {
                if (dr.IsDBNull(0)) DBjmeno = ""; else DBjmeno = fbaseToUtf16(dr.GetString(0).Trim());
                if (dr.IsDBNull(1)) DBcislo = ""; else DBcislo = Convert.ToString(dr.GetDecimal(1));
                if (dr.IsDBNull(2)) DBdilna = ""; else DBdilna = Convert.ToString(dr.GetDecimal(2));
                if (dr.IsDBNull(3)) DBpracoviste = ""; else DBpracoviste = fbaseToUtf16(dr.GetString(3).Trim());
                if (dr.IsDBNull(4)) DBvyrobek = ""; else DBvyrobek = fbaseToUtf16(dr.GetString(4).Trim());
                if (dr.IsDBNull(5)) DBnazev = ""; else DBnazev = fbaseToUtf16(dr.GetString(5).Trim());
                if (dr.IsDBNull(6)) DBJK = ""; else DBJK = dr.GetString(6).Trim();
                if (dr.IsDBNull(7)) DBrozmer = ""; else DBrozmer = fbaseToUtf16(dr.GetString(7).Trim());
                if (dr.IsDBNull(8)) DBpocetks = 0; else DBpocetks = Convert.ToInt32(dr.GetDecimal(8));
                if (dr.IsDBNull(9)) DBcena = 0; else DBcena = Convert.ToDouble(dr.GetDecimal(9));
                if (dr.IsDBNull(10)) DBkDatum = new DateTime(0); else DBkDatum = dr.GetDateTime(10);
                if (dr.IsDBNull(11)) DBnormaCSN = ""; else DBnormaCSN = fbaseToUtf16(dr.GetString(11).Trim());
                if (dr.IsDBNull(12)) DBkrJmeno = ""; else DBkrJmeno = fbaseToUtf16(dr.GetString(12).Trim());
                if (dr.IsDBNull(13)) DBcelkCena = 0; else DBcelkCena = Convert.ToDouble(dr.GetDecimal(13));
                if (dr.IsDBNull(14)) DBvevCislo = ""; else DBvevCislo = fbaseToUtf16(dr.GetString(14).Trim());
                if (dr.IsDBNull(15)) DBkonto = ""; else DBkonto = fbaseToUtf16(dr.GetString(15).Trim());

                myDB.addLineVraceno(DBjmeno, DBcislo, DBdilna, DBpracoviste, DBvyrobek, DBnazev, DBJK,
                     DBrozmer, DBpocetks, DBcena, DBkDatum, DBnormaCSN, DBkrJmeno, DBcelkCena, DBvevCislo,
                     DBkonto);
                Application.DoEvents();

            }

        }
        fbase.Close();
        fbase.Dispose();
        if (myTransaction != null)
        {
            myDB.stopTransaction(myTransaction);
        }
    }



    public static void presunPoskozene(vDatabase myDB, string filepath)
    {
        DbTransaction myTransaction = myDB.startTransaction();
        OleDbConnection fbase = new OleDbConnection("Provider=VFPOLEDB.1;CodePage=437;Data Source=" + filepath + ";Exclusive=false;Nulls=false");
        fbase.Open();
        OleDbCommand fbaseCom = new OleDbCommand("SELECT * FROM " + filepath + "\\DATA\\POSKNAR.DBF", fbase);
        OleDbDataReader dr = fbaseCom.ExecuteReader();
        
        
        if (dr.HasRows)
        {
            string DBnazev;
            string DBJK;
            string DBnormaCSN;
            string DBjmeno;
            string DBcislo;
            string DBdilna;
            string DBpracoviste;
            string DBvyrobek;
            int DBpocetks;
            string DBkrJmeno;
            string DBvevCislo;
            string DBkonto;
            string DBrozmer;
            double DBcelkCena;
            double DBcena;
            DateTime DBkDatum;

            while (dr.Read())
            {
                if (dr.IsDBNull(0)) DBjmeno = ""; else DBjmeno = fbaseToUtf16(dr.GetString(0).Trim());
                if (dr.IsDBNull(1)) DBcislo = ""; else DBcislo = Convert.ToString(dr.GetDecimal(1));
                if (dr.IsDBNull(2)) DBdilna = ""; else DBdilna = Convert.ToString(dr.GetDecimal(2));
                if (dr.IsDBNull(3)) DBpracoviste = ""; else DBpracoviste = fbaseToUtf16(dr.GetString(3).Trim());
                if (dr.IsDBNull(4)) DBvyrobek = ""; else DBvyrobek = fbaseToUtf16(dr.GetString(4).Trim());
                if (dr.IsDBNull(5)) DBnazev = ""; else DBnazev = fbaseToUtf16(dr.GetString(5).Trim());
                if (dr.IsDBNull(6)) DBJK = ""; else DBJK = dr.GetString(6).Trim();
                if (dr.IsDBNull(7)) DBrozmer = ""; else DBrozmer = fbaseToUtf16(dr.GetString(7).Trim());
                if (dr.IsDBNull(8)) DBpocetks = 0; else DBpocetks = Convert.ToInt32(dr.GetDecimal(8));
                if (dr.IsDBNull(9)) DBcena = 0; else DBcena = Convert.ToDouble(dr.GetDecimal(9));
                if (dr.IsDBNull(10)) DBkDatum = new DateTime(0); else DBkDatum = dr.GetDateTime(10);
                if (dr.IsDBNull(11)) DBnormaCSN = ""; else DBnormaCSN = fbaseToUtf16(dr.GetString(11).Trim());
                if (dr.IsDBNull(12)) DBkrJmeno = ""; else DBkrJmeno = fbaseToUtf16(dr.GetString(12).Trim());
                if (dr.IsDBNull(13)) DBcelkCena = 0; else DBcelkCena = Convert.ToDouble(dr.GetDecimal(13));
                if (dr.IsDBNull(14)) DBvevCislo = ""; else DBvevCislo = fbaseToUtf16(dr.GetString(14).Trim());
                if (dr.IsDBNull(15)) DBkonto = ""; else DBkonto = fbaseToUtf16(dr.GetString(15).Trim());

                myDB.addLinePoskozeno(DBjmeno, DBcislo, DBdilna, DBpracoviste, DBvyrobek, DBnazev, DBJK,
                     DBrozmer, DBpocetks, DBcena, DBkDatum, DBnormaCSN, DBkrJmeno, DBcelkCena, DBvevCislo,
                     DBkonto);
                Application.DoEvents();

            }

        }
        fbase.Close();
        fbase.Dispose();
        if (myTransaction != null)
        {
            myDB.stopTransaction(myTransaction);
        }
    }




    public static void presunVyrazeneMDat(vDatabase myDB, string filepath, Hashtable DBJoin)
    {
        presunZmenyMDatAdr(myDB, filepath, "MDAT", DBJoin);
        presunZmenyMDatAdr(myDB, filepath, "MDAT1", DBJoin);
    }

    private static void presunZmenyMDatAdr(vDatabase myDB, string filepath, string subAddr, Hashtable DBJoin)
    {
        string mdataPath = filepath + "\\" + subAddr;
        // seznam souboru
        if (Directory.Exists(mdataPath))
        {
            DbTransaction myTransaction = myDB.startTransaction();

            DirectoryInfo addr = new DirectoryInfo(mdataPath);
            foreach (FileInfo dbfFile in addr.GetFiles ("*.dbf"))
            {
                string fileName = dbfFile.Name;
                string nameFileWithExt = Path.GetFileNameWithoutExtension(dbfFile.Name);
                string index = subAddr + ":" + nameFileWithExt;
                presunZmenyMDatFile(myDB, mdataPath,fileName, index, DBJoin);
            }
            if (myTransaction != null)
            {
                myDB.stopTransaction(myTransaction);
            }

        }
    }

    private static void presunZmenyMDatFile(vDatabase myDB, string filepath, string fileName, string index, Hashtable DBJoin)
    {
        OleDbConnection fbase = new OleDbConnection("Provider=VFPOLEDB.1;CodePage=437;Data Source=" + filepath + ";Exclusive=false;Nulls=false");
        fbase.Open();
        OleDbCommand fbaseCom = new OleDbCommand("SELECT * FROM " + filepath + "\\"+fileName, fbase);
        OleDbDataReader dr = fbaseCom.ExecuteReader();
        if (dr.HasRows)
        {
            string DBpomocJK;
            DateTime DBdatum;
            string DBpoznamka;
            int DBprijem;
            int DBvydej;
            int DBzustatek;
            string DBzapKarta;
            string DBvevCislo;
            int DBpocIvc;
            string DBcontrCod;
            string DBdosudNvrc;
            string DBprijTyp;
            string DBvydejTyp;
            int DBporadi;
            string DBstav;


            while (dr.Read())
            {
                // 0 nepouzivame  - cislo
                if (dr.IsDBNull(0)) DBpomocJK = ""; else DBpomocJK = dr.GetString(0).Trim(); //P_OZJK
                if (dr.IsDBNull(1)) DBdatum = new DateTime(0); else DBdatum = dr.GetDateTime(1);
                if (dr.IsDBNull(2)) DBpoznamka = ""; else DBpoznamka = fbaseToUtf16(dr.GetString(2).Trim());
                if (dr.IsDBNull(3)) DBprijem = 0; else DBprijem = Convert.ToInt32(dr.GetDecimal(3));
                if (dr.IsDBNull(4)) DBvydej = 0; else DBvydej = Convert.ToInt32(dr.GetDecimal(4));
                if (dr.IsDBNull(5)) DBzustatek = 0; else DBzustatek = Convert.ToInt32(dr.GetDecimal(5));
                if (dr.IsDBNull(6)) DBzapKarta = ""; else DBzapKarta = fbaseToUtf16(dr.GetString(6).Trim());//mesto
                if (dr.IsDBNull(7)) DBvevCislo = ""; else DBvevCislo = fbaseToUtf16(dr.GetString(7).Trim());//pcs
                if (dr.IsDBNull(8)) DBpocIvc = 0; else DBpocIvc = Convert.ToInt32(dr.GetDecimal(8));
                if (dr.IsDBNull(9)) DBcontrCod = ""; else DBcontrCod = dr.GetString(9).Trim();
                if (dr.IsDBNull(10)) DBdosudNvrc = ""; else DBdosudNvrc = dr.GetString(10).Trim();
                if (dr.IsDBNull(11)) DBprijTyp = ""; else DBprijTyp = dr.GetString(11).Trim();
                if (dr.IsDBNull(12)) DBvydejTyp = ""; else DBvydejTyp = dr.GetString(12).Trim();//telzam
                if (dr.IsDBNull(13)) DBporadi = 0; else DBporadi = Convert.ToInt32(dr.GetDecimal(13));
                if (dr.IsDBNull(14)) DBstav = ""; else DBstav = dr.GetString(14).Trim();

                Int32 poradi;
                if (DBcontrCod != "" )
                {
                    string joinIndex = index + ":" + DBcontrCod;
                    if (DBJoin.ContainsKey(joinIndex))
                    {
                        poradi = (Int32)DBJoin[joinIndex];
                    }
                    else
                    {
                        poradi = 0; // neprirazeno
                    }

                }
                else
                {
                    poradi = -1; // chyba v puvodnich datech
                }
                // vygenerujeme novy stav

                string DBpoznamkaUpper = DBpoznamka.ToUpper();
                string testString;

                if (DBpoznamkaUpper.Length > 9) testString = DBpoznamkaUpper.Substring(0, 9);
                else testString = DBpoznamkaUpper;
                if (testString == "POŠKOZENO") DBstav = "O";
                else
                {
                    if (DBpoznamkaUpper.Length > 5) testString = DBpoznamkaUpper.Substring(0, 5);
                    else testString = DBpoznamkaUpper;
                    if (testString == "POŠK.") DBstav = "O";
                    else
                    {
                        if (DBpoznamkaUpper.Length > 7) testString = DBpoznamkaUpper.Substring(0, 7);
                        else testString = DBpoznamkaUpper;
                        if ((testString  == "POŠKOZ.")) DBstav = "O";
                        else
                        {
                            if (DBpoznamkaUpper.Length > 7) testString = DBpoznamkaUpper.Substring(0, 7);
                            else testString = DBpoznamkaUpper;
                            if (testString == "PŮJČENO" ) DBstav = "U";
                            else
                            {
                                if (DBpoznamkaUpper.Length > 4) testString = DBpoznamkaUpper.Substring(0, 4);
                                else testString = DBpoznamkaUpper;
                                if (testString == "PŮJ.") DBstav = "U";
                                else
                                {
                                    if (DBpoznamkaUpper.Length > 5) testString = DBpoznamkaUpper.Substring(0, 5);
                                    else testString = DBpoznamkaUpper;
                                    if ((testString == "PŮJČ.") || (DBpoznamka == "oprava - půjčeno")) DBstav = "U";
                                    else
                                    {
                                        if (DBpoznamkaUpper.Length > 7) testString = DBpoznamkaUpper.Substring(0, 7);
                                        else testString = DBpoznamkaUpper;
                                        if (testString == "VRÁCENO") DBstav = "R";
                                        else
                                        {
                                            if (DBpoznamkaUpper.Length > 8) testString = DBpoznamkaUpper.Substring(0, 8);
                                            else testString = DBpoznamkaUpper;
                                            if (testString == "VYŘAZENO") DBstav = "V";
                                            else
                                            {
                                                if (DBpoznamkaUpper.Length > 5) testString = DBpoznamkaUpper.Substring(0, 5);
                                                else testString = DBpoznamkaUpper;
                                                if ((testString == "NÁKUP") || (DBpoznamkaUpper == "ZE STARÉ KARTY")) DBstav = "P";
                                                else
                                                {
                                                    if ((DBvydej > 0) && (DBprijem == 0))
                                                    {
                                                        DBstav = "V"; //Vyrazeno
                                                    }
                                                    else
                                                    {
                                                        DBstav = "P"; // Přijem "Nákup"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }


                myDB.addLineZmeny(poradi, DBpomocJK, DBdatum, DBpoznamka, DBprijem, DBvydej, DBzustatek, DBzapKarta, DBvevCislo,
                    DBpocIvc, DBstav, DBporadi );

                Application.DoEvents();

            }

        }
        fbase.Close();
        fbase.Dispose();
    }




    public static void presunPerson(vDatabase myDB, string filepath, Hashtable DBJoin)
    {
        string personPath = filepath + "\\PERSON";
        // seznam souboru
        if (Directory.Exists(personPath))
        {
            DbTransaction myTransaction = myDB.startTransaction();

            DirectoryInfo addr = new DirectoryInfo(personPath);
            foreach (FileInfo dbfFile in addr.GetFiles("*.dbf"))
            {
                string fileName = dbfFile.Name;
//                string nameFileWithExt = Path.GetFileNameWithoutExtension(dbfFile.Name);
                presunPersonFile(myDB, personPath, fileName, DBJoin);
            }
            if (myTransaction != null)
            {
                myDB.stopTransaction(myTransaction);
            }

        }
    }


    private static void presunPersonFile(vDatabase myDB, string filepath, string fileName, Hashtable DBJoin)
    {
        OleDbConnection fbase = new OleDbConnection("Provider=VFPOLEDB.1;CodePage=437;Data Source=" + filepath + ";Exclusive=false;Nulls=false");
        fbase.Open();
            OleDbCommand fbaseCom = new OleDbCommand("SELECT * FROM " + filepath + "\\" + fileName, fbase);
           OleDbDataReader dr;
            try
            {
                dr = fbaseCom.ExecuteReader();

            if (dr.HasRows)
            {
                string DBjmeno;
                string DBprijmeni;
                string DBosCislo;
                DateTime DBdatum;
                string DBpoznamka;
                int DBks;
                string DBnazev;
                string DBivCislo;
                string DBvevCislo;
                string DBcontrCod;
                string DBmov;
                string DBrozmer;
                string DBcsn;
                double DBcena;


                while (dr.Read())
                {
                    if (dr.IsDBNull(0)) DBjmeno = ""; else DBjmeno = fbaseToUtf16(dr.GetString(0).Trim());
                    if (dr.IsDBNull(1)) DBprijmeni = ""; else DBprijmeni = fbaseToUtf16(dr.GetString(1).Trim());
                    if (dr.IsDBNull(2)) DBosCislo = ""; else DBosCislo = dr.GetString(2).Trim();
                    if (dr.IsDBNull(3)) DBdatum = new DateTime(0); else DBdatum = dr.GetDateTime(3);
                    if (dr.IsDBNull(4)) DBnazev = ""; else DBnazev = fbaseToUtf16(dr.GetString(4).Trim());
                    if (dr.IsDBNull(5)) DBrozmer = ""; else DBrozmer = fbaseToUtf16(dr.GetString(5).Trim());
                    if (dr.IsDBNull(6)) DBpoznamka = ""; else DBpoznamka = fbaseToUtf16(dr.GetString(6).Trim());
                    if (dr.IsDBNull(7)) DBivCislo = ""; else DBivCislo = dr.GetString(7).Trim();
                    if (dr.IsDBNull(8)) DBks = 0; else DBks = Convert.ToInt32(dr.GetDecimal(8));
                    if (dr.IsDBNull(9)) DBcontrCod = ""; else DBcontrCod = dr.GetString(9).Trim();
                    if (dr.IsDBNull(10)) DBmov = ""; else DBmov = dr.GetString(10).Trim();
                    if (dr.IsDBNull(11)) DBcsn = ""; else DBcsn = dr.GetString(11).Trim();
                    if (dr.IsDBNull(12)) DBcena = 0; else DBcena = Convert.ToDouble(dr.GetDecimal(12));
                    if (dr.IsDBNull(13)) DBvevCislo = ""; else DBvevCislo = fbaseToUtf16(dr.GetString(13).Trim());//pcs

                    Int32 poradi;
                    if (DBcontrCod != "")
                    {
                        string joinIndex = "MDAT:" + DBmov + ":" + DBcontrCod;
                        if (DBJoin.ContainsKey(joinIndex))
                        {
                            poradi = (Int32)DBJoin[joinIndex];
                        }
                        else
                        {
                            joinIndex = "MDAT1:" + DBmov + ":" + DBcontrCod;
                            if (DBJoin.ContainsKey(joinIndex))
                            {
                                poradi = (Int32)DBJoin[joinIndex];
                            }
                            else
                            {
                                poradi = 0; // neprirazeno
                                MessageBox.Show("Vypujčení - Položka " + DBnazev + " JK : " + DBivCislo + " pro uživatele " + DBosCislo + " neexisuje. ");
                            }

                        }

                    }
                    else
                    {
                        poradi = -1; // chyba v puvodnich datech
                    }
                    // vygenerujeme novy stav

                    Int32 DBzmPoradi = 0;


                    myDB.addLinePujceno(poradi, DBosCislo, DBdatum, DBks, DBjmeno, DBprijmeni, DBnazev, DBivCislo,
                        DBcena, DBzmPoradi);

                    Application.DoEvents();

                }

            }

            fbase.Close();
            fbase.Dispose();
                    }
            catch
            {
                MessageBox.Show("Nemohu nacist soubor " + fileName);
            }
            finally
            {
                fbase.Close();
                fbase.Dispose();
            }

    }



    public static void presunOsoby(vDatabase myDB, string filepath)
    {
        DbTransaction myTransaction = myDB.startTransaction();
        OleDbConnection fbase = new OleDbConnection("Provider=VFPOLEDB.1;CodePage=437;Data Source=" + filepath + "\\DATA;Exclusive=false;Nulls=false");
        fbase.Open();
        OleDbCommand fbaseCom = new OleDbCommand("SELECT * FROM " + filepath + "\\DATA\\PERSON.DBF", fbase);
        OleDbDataReader dr = fbaseCom.ExecuteReader();
        if (dr.HasRows)
        {
            string DBprijmeni;
            string DBjmeno;
            string DBulice;
            string DBmesto;
            string DBpsc;
            string DBtelHome;
            string DBosCislo;
            string DBodeleni;
            string DBtelZam;
            string DBstredisko;
            string DBpujSoub;
            string DBpracoviste;
            string DBcisZnamky;
            string DBPoznamka;


            while (dr.Read())
            {
                // 0 nepouzivame  - cislo
                if (dr.IsDBNull(1)) DBprijmeni = ""; else DBprijmeni = fbaseToUtf16(dr.GetString(1).Trim()); //prijmeni
                if (dr.IsDBNull(2)) DBjmeno = ""; else DBjmeno = fbaseToUtf16(dr.GetString(2).Trim());//jmeno
                if (dr.IsDBNull(3)) DBulice = ""; else DBulice = fbaseToUtf16(dr.GetString(3).Trim());//ulice
                if (dr.IsDBNull(4)) DBmesto = ""; else DBmesto = fbaseToUtf16(dr.GetString(4).Trim());//mesto
                if (dr.IsDBNull(5)) DBpsc = ""; else DBpsc = fbaseToUtf16(dr.GetString(5).Trim());//pcs
                if (dr.IsDBNull(6)) DBtelHome = ""; else DBtelHome= dr.GetString(6).Trim();//telhome
                if (dr.IsDBNull(7)) DBosCislo = ""; else DBosCislo = dr.GetString(7).Trim();//oscislo
                if (dr.IsDBNull(8)) DBodeleni = ""; else DBodeleni = fbaseToUtf16(dr.GetString(8).Trim());//oddeleni
                if (dr.IsDBNull(9)) DBtelZam = ""; else DBtelZam = dr.GetString(9).Trim();//telzam
                if (dr.IsDBNull(10)) DBPoznamka = ""; else DBPoznamka = dr.GetString(10).Trim();//pracpzn
                //11 nepouzivame lock
                if (dr.IsDBNull(12)) DBstredisko = ""; else DBstredisko = Convert.ToString((dr.GetDecimal(12)));  // .GetDeci(12).;//strediski
                if (dr.IsDBNull(13)) DBpujSoub = ""; else DBpujSoub = dr.GetString(13).Trim();//pusjsoub
                if (dr.IsDBNull(14)) DBpracoviste = ""; else DBpracoviste = fbaseToUtf16(dr.GetString(14).Trim()); //pracoviste
                if (dr.IsDBNull(15)) DBcisZnamky = ""; else DBcisZnamky = dr.GetString(15).Trim();//cis_znamky

                myDB.addLineOsoby(DBprijmeni, DBjmeno, DBulice, DBmesto, DBpsc, DBtelHome, DBosCislo,
                     DBodeleni, DBtelZam, DBstredisko, DBpujSoub, DBpracoviste, DBcisZnamky, DBPoznamka);
                Application.DoEvents();

            }

        }
        fbase.Close();
        fbase.Dispose();
        if (myTransaction != null)
        {
            myDB.stopTransaction(myTransaction);
        }

    }


    private static string fbaseToUtf16(string inputString)
    {
        Encoding cp852 = Encoding.GetEncoding(852);
        Encoding unicode = Encoding.Unicode;

        byte[] buffer1 = Encoding.Default.GetBytes(inputString);
        byte[] buffer2 = Encoding.Convert(cp852, unicode, buffer1);
        return unicode.GetString(buffer2);

        
    }


    }
}
