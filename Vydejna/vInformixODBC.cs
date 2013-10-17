﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Windows.Forms;


namespace Vydejna
{
    class vInformixODBC : vODBC
    {

        public vInformixODBC(string dataBaseName, string serverAddress, string serverName, string port, string locale, string driver, string userName, string password)
            : base(dataBaseName, serverAddress, serverName, port, locale, driver, userName, password)
        {

            dBConnectStr = String.Format("Driver={{{0,1}}};Host={1,1};Server={2,1};Port={3,1};Protocol=onsoctcp;Database={4,1};Uid={6,1};Pwd={7,1};DB_LOCALE={5,1};CLIENT_LOCALE=cs_cz.CP1250",
                driver, serverAddress, serverName, port, dataBaseName, locale, userName, password);

            //Dsn='';Driver={INFORMIX 3.30 32 BIT};Host=hostname;Server=myServerAddress;
            //Service=service-name;Protocol=olsoctcp;Database=myDataBase;Uid=myUsername;
            //Pwd=myPassword;
            //Dsn=Informix;uid=xxxxx;database=sysmaster;host=10.10.10.10;srvr=testdb1;serv=3000;pro=onsoctcp;cloc=en_US.819;dloc=en_US.819;vmb=0;curb=0;scur=0;icur=0;oac=1;optofc=0;rkc=0;odtyp=0;ddfp=0;dnl=0;rcwc=0
        }


        public override void openDB()
        {

            try
            {
                myDBConn = new OdbcConnection(dBConnectStr);
                myDBConn.Open();
                dBConnectionState = true;
            }
            catch
            {
                dBConnectionState = false;
                MessageBox.Show("Database nebyla pripojena");
            }
        }


        public override void DeleteTables()
        {
            openDB();
            if (DBIsOpened())
            {
                OdbcCommand cmdNaradi = new OdbcCommand("DELETE from naradi", myDBConn as OdbcConnection);
                OdbcCommand cmdKarta = new OdbcCommand("DELETE from karta", myDBConn as OdbcConnection);
                OdbcCommand cmdSequence = new OdbcCommand("UPDATE tabseq SET poradi=1 WHERE nazev = 'naradi'", myDBConn as OdbcConnection);
                OdbcCommand cmdVraceno = new OdbcCommand("DELETE from vraceno", myDBConn as OdbcConnection);
                OdbcCommand cmdPoskozeno = new OdbcCommand("DELETE from poskozeno", myDBConn as OdbcConnection);
                OdbcCommand cmdOsoby = new OdbcCommand("DELETE from osoby", myDBConn as OdbcConnection);
                OdbcCommand cmdZmeny = new OdbcCommand("DELETE from zmeny", myDBConn as OdbcConnection);
                try
                {
                    cmdNaradi.ExecuteNonQuery();
                    cmdKarta.ExecuteNonQuery();
                    cmdSequence.ExecuteNonQuery();
                    cmdVraceno.ExecuteNonQuery();
                    cmdPoskozeno.ExecuteNonQuery();
                    cmdOsoby.ExecuteNonQuery();
                    cmdZmeny.ExecuteNonQuery();
                    MessageBox.Show("Tabulky byly smazány");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cmdOsoby.Dispose();
                    cmdPoskozeno.Dispose();
                    cmdVraceno.Dispose();
                    cmdKarta.Dispose();
                    cmdNaradi.Dispose();
                    //                   myDBConn.Close();
                    //                   myDBConn.Dispose();
                }
            }

            // vymaze tabulky

        }



        // pridani nove polozky do tabulky naradi
        public override Int32 addNewLineNaradi(string DBnazev, string DBJK, string DBnormacsn, string DBnormadin,
                                         string DBvyrobce, decimal DBcena, string DBpoznamka, long DBminstav,
                                         decimal DBcelkcena, long DBucetstav, long DBfyzstav,
                                         string DBrozmer, string DBanalucet, decimal DBucetkscen, DateTime DBkdatum)
        {



            // opravit nepracuje pro prvni polozku
            string commandStringSeq0 = "SELECT count(*) as countporadi from naradi";
            string commandStringSeq1 = "SELECT MAX(poradi) as maxporadi from naradi";
            string commandStringSeq2 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'naradi'";


            string commandString2 = "INSERT INTO naradi ( poradi, nazev, jk, normacsn, normadin, vyrobce, cena, poznamka, minimum, celkcena,  ucetstav, fyzstav, rozmer, analucet, tdate, stredisko, kodzmeny, druh, odpis, zavod, ucetkscen, test, pomroz, kdatum, kodd ) " +
                  "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '' )";

            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                try
                {
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }


                    Int32 maxporadi;

                    OdbcCommand cmdr0 = new OdbcCommand(commandStringSeq0, myDBConn as OdbcConnection);
                    cmdr0.Transaction = transaction;
                    OdbcDataReader myReader0 = cmdr0.ExecuteReader();
                    myReader0.Read();
                    Int32 countporadi = myReader0.GetInt32(0);
                    myReader0.Close();

                    if (countporadi == 0) maxporadi = 1;
                    else
                    {

                        OdbcCommand cmdr1 = new OdbcCommand(commandStringSeq1, myDBConn as OdbcConnection);
                        cmdr1.Transaction = transaction;
                        OdbcDataReader myReader1 = cmdr1.ExecuteReader();
                        myReader1.Read();
                        maxporadi = myReader1.GetInt32(0);
                        myReader1.Close();
                        maxporadi++;
                    }

                    OdbcCommand cmd = new OdbcCommand(commandString2, myDBConn as OdbcConnection);

                    OdbcParameter p0 = new OdbcParameter("p0", OdbcType.Int);
                    p0.Value = maxporadi;
                    OdbcParameter p1 = new OdbcParameter("p1", OdbcType.NVarChar);
                    p1.Value = DBnazev;
                    OdbcParameter p2 = new OdbcParameter("p2", OdbcType.NVarChar);
                    p2.Value = DBJK;
                    OdbcParameter p3 = new OdbcParameter("p3", OdbcType.NVarChar);
                    p3.Value = DBnormacsn;
                    OdbcParameter p4 = new OdbcParameter("p4", OdbcType.NVarChar);
                    p4.Value = DBnormadin;
                    OdbcParameter p5 = new OdbcParameter("p5", OdbcType.NVarChar);
                    p5.Value = DBvyrobce;
                    OdbcParameter p6 = new OdbcParameter("p6", OdbcType.Double);
                    p6.Value = DBcena;
                    OdbcParameter p7 = new OdbcParameter("p7", OdbcType.NVarChar);
                    p7.Value = DBpoznamka;
                    OdbcParameter p8 = new OdbcParameter("p8", OdbcType.Int);
                    p8.Value = DBminstav;
                    OdbcParameter p9 = new OdbcParameter("p9", OdbcType.Double);
                    p9.Value = DBcelkcena;
                    OdbcParameter p15 = new OdbcParameter("p15", OdbcType.Int);
                    p15.Value = DBucetstav;
                    OdbcParameter p16 = new OdbcParameter("p16", OdbcType.Int);
                    p16.Value = DBfyzstav;
                    OdbcParameter p17 = new OdbcParameter("p17", OdbcType.NVarChar);
                    p17.Value = DBrozmer;
                    OdbcParameter p18 = new OdbcParameter("p18", OdbcType.NVarChar);
                    p18.Value = DBanalucet;
                    OdbcParameter p19 = new OdbcParameter("p19", OdbcType.Date);
                    p19.Value = new DateTime(0);
                    OdbcParameter p20 = new OdbcParameter("p20", OdbcType.NVarChar);
                    p20.Value = "";
                    OdbcParameter p21 = new OdbcParameter("p21", OdbcType.NVarChar);
                    p21.Value = "";
                    OdbcParameter p22 = new OdbcParameter("p22", OdbcType.NVarChar);
                    p22.Value = "";
                    OdbcParameter p23 = new OdbcParameter("p23", OdbcType.NVarChar);
                    p23.Value = "";
                    OdbcParameter p24 = new OdbcParameter("p24", OdbcType.NVarChar);
                    p24.Value = "";
                    OdbcParameter p25 = new OdbcParameter("p25", OdbcType.Double);
                    p25.Value = DBucetkscen;
                    OdbcParameter p26 = new OdbcParameter("p26", OdbcType.NVarChar);
                    p26.Value = "";
                    OdbcParameter p27 = new OdbcParameter("p27", OdbcType.NVarChar);
                    p27.Value = "";
                    OdbcParameter p28 = new OdbcParameter("p28", OdbcType.Date);
                    p28.Value = DBkdatum;
                    OdbcParameter p29 = new OdbcParameter("p29", OdbcType.NVarChar);
                    p29.Value = "";


                    cmd.Parameters.Add(p0);
                    cmd.Parameters.Add(p1);

                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    cmd.Parameters.Add(p4);
                    cmd.Parameters.Add(p5);
                    cmd.Parameters.Add(p6);
                    cmd.Parameters.Add(p7);
                    cmd.Parameters.Add(p8);
                    cmd.Parameters.Add(p9);
                    cmd.Parameters.Add(p15);
                    cmd.Parameters.Add(p16);
                    cmd.Parameters.Add(p17);
                    cmd.Parameters.Add(p18);
                    cmd.Parameters.Add(p19);
                    cmd.Parameters.Add(p20);
                    cmd.Parameters.Add(p21);
                    cmd.Parameters.Add(p22);
                    cmd.Parameters.Add(p23);
                    cmd.Parameters.Add(p24);
                    cmd.Parameters.Add(p25);
                    cmd.Parameters.Add(p26);
                    cmd.Parameters.Add(p27);
                    cmd.Parameters.Add(p28);
                    cmd.Parameters.Add(p29);

                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();

                    OdbcCommand cmdSeq2 = new OdbcCommand(commandStringSeq2, myDBConn as OdbcConnection);

                    cmdSeq2.Transaction = transaction;
                    cmdSeq2.ExecuteNonQuery();


                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Commit();
                    }
                    return maxporadi;

                }
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Rollback();
                    }
                    return -1;
                }
            }
            else return -1;

        }


        // pridani nove polozky do tabulky osoby
        public override Int32 addNewLineOsoby(string DBprijmeni, string DBjmeno, string DBulice, string DBmesto,
                                         string DBpsc, string DBtelHome, string DBosCislo, string DBstredisko,
                                         string DBcisZnamky, string DBoddeleni, string DBpracoviste, string DBtelZam,
                                         string DBpoznamka)
        {

            string commandString1 = "SELECT oscislo from osoby where oscislo = ? ";

            string commandString2 = "INSERT INTO osoby ( prijmeni, jmeno, ulice, mesto, psc, telhome, oscislo, odeleni, telzam, stredisko, pujsoub, pracoviste, cisznamky, poznamka ) " +
                  "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";


            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                try
                {
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch { }
                    OdbcCommand cmdr = new OdbcCommand(commandString1, myDBConn as OdbcConnection);

                    OdbcParameter px = new OdbcParameter("px", DbType.String);
                    px.Value = DBosCislo;
                    cmdr.Parameters.Add(px);

                    cmdr.Transaction = transaction;
                    OdbcDataReader myReader = cmdr.ExecuteReader();


                    bool osCisloExist = myReader.Read();
                    // true osCisloExist
                    myReader.Close();

                    if (!osCisloExist)
                    {

                        OdbcCommand cmd = new OdbcCommand(commandString2, myDBConn as OdbcConnection);

                        OdbcParameter p1 = new OdbcParameter("p1", OdbcType.NVarChar);
                        p1.Value = DBprijmeni;
                        OdbcParameter p2 = new OdbcParameter("p2", OdbcType.NVarChar);
                        p2.Value = DBjmeno;
                        OdbcParameter p3 = new OdbcParameter("p3", OdbcType.NVarChar);
                        p3.Value = DBulice;
                        OdbcParameter p4 = new OdbcParameter("p4", OdbcType.NVarChar);
                        p4.Value = DBmesto;
                        OdbcParameter p5 = new OdbcParameter("p5", OdbcType.NVarChar);
                        p5.Value = DBpsc;
                        OdbcParameter p6 = new OdbcParameter("p6", OdbcType.NVarChar);
                        p6.Value = DBtelHome;
                        OdbcParameter p7 = new OdbcParameter("p7", OdbcType.NVarChar);
                        p7.Value = DBosCislo;
                        OdbcParameter p8 = new OdbcParameter("p8", OdbcType.NVarChar);
                        p8.Value = DBoddeleni;
                        OdbcParameter p9 = new OdbcParameter("p9", OdbcType.NVarChar);
                        p9.Value = DBtelZam;
                        OdbcParameter p10 = new OdbcParameter("p10", OdbcType.NVarChar);
                        p10.Value = DBstredisko;
                        OdbcParameter p11 = new OdbcParameter("p11", OdbcType.NVarChar);
                        p11.Value = "";
                        OdbcParameter p12 = new OdbcParameter("p12", OdbcType.NVarChar);
                        p12.Value = DBpracoviste;
                        OdbcParameter p13 = new OdbcParameter("p13", OdbcType.NVarChar);
                        p13.Value = DBcisZnamky;
                        OdbcParameter p14 = new OdbcParameter("p14", OdbcType.NVarChar);
                        p14.Value = DBpoznamka;


                        cmd.Parameters.Add(p1);
                        cmd.Parameters.Add(p2);
                        cmd.Parameters.Add(p3);
                        cmd.Parameters.Add(p4);
                        cmd.Parameters.Add(p5);
                        cmd.Parameters.Add(p6);
                        cmd.Parameters.Add(p7);
                        cmd.Parameters.Add(p8);
                        cmd.Parameters.Add(p9);
                        cmd.Parameters.Add(p10);
                        cmd.Parameters.Add(p11);
                        cmd.Parameters.Add(p12);
                        cmd.Parameters.Add(p13);
                        cmd.Parameters.Add(p14);

                        cmd.Transaction = transaction;

                        cmd.ExecuteNonQuery();

                    }


                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Commit();
                    }

                    if (!osCisloExist) return 0;
                    else return -1;

                }  //try
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Rollback();
                    }
                    return -1;
                }
            } // db is opened
            else return -1;
        }


        // pridani nove polozky do tabulky zmeny
        public override Int32 addNewLineZmeny(Int32 DBporadi, string DBJK, DateTime DBdatum, Int32 DBprijem, Int32 DBvydej, string DBpoznamka)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandString1 = "UPDATE naradi set fyzstav = fyzstav + ?, ucetstav = ucetstav + ?  where poradi = ? ";

                string commandString2 = "INSERT INTO zmeny (parporadi, pomozjk, datum, poznamka, prijem, vydej, zustatek, zapkarta, vevcislo, pocivc, poradi )" +
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";
                
                
                string commandString3 = "SELECT poradi, zustatek from zmeny where parporadi = ? ORDER BY poradi DESC";

                try
                {
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }


                    OdbcCommand cmdr = new OdbcCommand(commandString3, myDBConn as OdbcConnection);

                    OdbcParameter px = new OdbcParameter("px", DbType.Int32);
                    px.Value = DBporadi;
                    cmdr.Parameters.Add(px);

                    Int32 poradi;
                    Int32 zustatek;

                    OdbcDataReader myReader = cmdr.ExecuteReader();
                    // true osCisloExist
                    if (myReader.Read() == true)
                    {
                        poradi = myReader.GetInt32(0) + 1; // zjistime nove poradi
                        zustatek = myReader.GetInt32(1);
                    }
                    else
                    {
                        poradi = 1;
                        zustatek = 0;
                    }

                    myReader.Close();


                    OdbcCommand cmd1 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);

                    OdbcParameter pn1 = new OdbcParameter("p1", OdbcType.Int);
                    pn1.Value = DBprijem - DBvydej;
                    OdbcParameter pn2 = new OdbcParameter("p2", OdbcType.Int);
                    pn2.Value = DBprijem - DBvydej;
                    OdbcParameter pn3 = new OdbcParameter("p3", OdbcType.Int);
                    pn3.Value = DBporadi;

                    cmd1.Parameters.Add(pn1);
                    cmd1.Parameters.Add(pn2);
                    cmd1.Parameters.Add(pn3);

                    cmd1.Transaction = transaction;
                    cmd1.ExecuteNonQuery();

                    OdbcCommand cmd2 = new OdbcCommand(commandString2, myDBConn as OdbcConnection);

                    OdbcParameter p1 = new OdbcParameter("p1", OdbcType.Int);
                    p1.Value = DBporadi;
                    OdbcParameter p2 = new OdbcParameter("p2", OdbcType.NChar);
                    p2.Value = DBJK;
                    OdbcParameter p3 = new OdbcParameter("p3", OdbcType.Date);
                    p3.Value = DBdatum;
                    OdbcParameter p4 = new OdbcParameter("p4", OdbcType.NChar);
                    p4.Value = DBpoznamka;
                    OdbcParameter p5 = new OdbcParameter("p5", OdbcType.Int);
                    p5.Value = DBprijem;
                    OdbcParameter p6 = new OdbcParameter("p6", OdbcType.Int);
                    p6.Value = DBvydej;
                    OdbcParameter p7 = new OdbcParameter("p7", OdbcType.Int);
                    p7.Value = zustatek + DBprijem - DBvydej;
                    OdbcParameter p8 = new OdbcParameter("p8", OdbcType.NChar);
                    p8.Value = "";
                    OdbcParameter p9 = new OdbcParameter("p9", OdbcType.NChar);
                    p9.Value = "";
                    OdbcParameter p10 = new OdbcParameter("p10", OdbcType.Int);
                    p10.Value = 0;
                    OdbcParameter p11 = new OdbcParameter("p11", OdbcType.Int);
                    p11.Value = poradi;

                    cmd2.Parameters.Add(p1);
                    cmd2.Parameters.Add(p2);
                    cmd2.Parameters.Add(p3);
                    cmd2.Parameters.Add(p4);
                    cmd2.Parameters.Add(p5);
                    cmd2.Parameters.Add(p6);
                    cmd2.Parameters.Add(p7);
                    cmd2.Parameters.Add(p8);
                    cmd2.Parameters.Add(p9);
                    cmd2.Parameters.Add(p10);
                    cmd2.Parameters.Add(p11);

                    
                    cmd2.Transaction = transaction;
                    cmd2.ExecuteNonQuery();

                    
                }
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Rollback();
                    }
                    return -1;  // chyba
                }
                return 0;  // ok
            }
            return -1;  // databaze neni otevrena
        }


        public override Int32 addNewLineZmenyAndPoskozeno(Int32 DBporadi, string DBJK, DateTime DBdatum, Int32 DBvydej, string DBpoznamka,
                                                          string osCislo, string DBjmeno, string DBprijmeni, string DBstredisko, string DBprovoz,
                                                          string DBnazev, string DBrozmer, string DBkonto, double DBcena, double DBcelkCena, string DBcsn, string DBcisZak)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandString1 = "UPDATE naradi set fyzstav = fyzstav - ?, ucetstav = ucetstav - ?  where poradi = ? ";

                string commandString2 = "INSERT INTO zmeny (parporadi, pomozjk, datum, poznamka, prijem, vydej, zustatek, zapkarta, vevcislo, pocivc, poradi )" +
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";


                string commandString3 = "SELECT poradi, zustatek from zmeny where parporadi = ? ORDER BY poradi DESC";
                string commandString4 = "SELECT fyzstav FROM naradi where poradi = ? ";


                string commandString5 = "SELECT poradi FROM tabseq WHERE nazev = 'poskozeno'";
                string commandString6 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'poskozeno'";

                string commandString7 = "INSERT INTO poskozeno ( poradi, jmeno, cislo, dilna, pracoviste, vyrobek, nazev, jk, rozmer, pocetks, cena, datum, csn, krjmeno, celkcena, vevcislo, konto) " +
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";


                try
                {
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }

                    OdbcCommand cmdr2 = new OdbcCommand(commandString4, myDBConn as OdbcConnection);

                    OdbcParameter px2 = new OdbcParameter("px2", DbType.Int32);
                    px2.Value = DBporadi;
                    cmdr2.Parameters.Add(px2);
                    cmdr2.Transaction = transaction;
                    OdbcDataReader myReader2 = cmdr2.ExecuteReader();
                    // true fyzstav exist -- zaznam mohl bzt meyitom smazan
                    if (myReader2.Read() == true)
                    {
                        Int32 fyzstav = myReader2.GetInt32(myReader2.GetOrdinal("fyzstav"));
                        myReader2.Close();

                        // poradi pro zmeny
                        OdbcCommand cmdr = new OdbcCommand(commandString3, myDBConn as OdbcConnection);
                        OdbcParameter px = new OdbcParameter("px", DbType.Int32);
                        px.Value = DBporadi;
                        cmdr.Parameters.Add(px);

                        Int32 poradi;
                        Int32 zustatek;

                        cmdr.Transaction = transaction;
                        OdbcDataReader myReader = cmdr.ExecuteReader();
                        // true osCisloExist
                        if (myReader.Read() == true)
                        {
                           poradi = myReader.GetInt32(0) + 1; // zjistime nove poradi
                           zustatek = myReader.GetInt32(1);
                        }
                        else
                        {
                           poradi = 1;
                           zustatek = fyzstav;
                        }
                        myReader.Close();

                       // zjisteni   poradi pro tabulku poskoyeneho naradi
                       OdbcCommand cmdSeq1 = new OdbcCommand(commandString5, myDBConn as OdbcConnection);
                       OdbcDataReader seqReader = cmdSeq1.ExecuteReader();
                       seqReader.Read();
                       Int32 poradiPoskozeno = seqReader.GetInt32(0);
                       seqReader.Close();



                       OdbcCommand cmd1 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);

                       OdbcParameter pn1 = new OdbcParameter("p1", OdbcType.Int);
                       pn1.Value =  DBvydej;
                       OdbcParameter pn2 = new OdbcParameter("p2", OdbcType.Int);
                       pn2.Value =  DBvydej;
                       OdbcParameter pn3 = new OdbcParameter("p3", OdbcType.Int);
                       pn3.Value = DBporadi;

                       cmd1.Parameters.Add(pn1);
                       cmd1.Parameters.Add(pn2);
                       cmd1.Parameters.Add(pn3);

                       cmd1.Transaction = transaction;
                       cmd1.ExecuteNonQuery();

                       OdbcCommand cmd2 = new OdbcCommand(commandString2, myDBConn as OdbcConnection);

                       OdbcParameter p1 = new OdbcParameter("p1", OdbcType.Int);
                       p1.Value = DBporadi;
                       OdbcParameter p2 = new OdbcParameter("p2", OdbcType.NChar);
                       p2.Value = DBJK;
                       OdbcParameter p3 = new OdbcParameter("p3", OdbcType.Date);
                       p3.Value = DBdatum;
                       OdbcParameter p4 = new OdbcParameter("p4", OdbcType.NChar);
                       p4.Value = DBpoznamka;
                       OdbcParameter p5 = new OdbcParameter("p5", OdbcType.Int);
                       p5.Value = 0;
                       OdbcParameter p6 = new OdbcParameter("p6", OdbcType.Int);
                       p6.Value = DBvydej;
                       OdbcParameter p7 = new OdbcParameter("p7", OdbcType.Int);
                       p7.Value = zustatek  - DBvydej;
                       OdbcParameter p8 = new OdbcParameter("p8", OdbcType.NChar);
                       p8.Value = osCislo;
                       OdbcParameter p9 = new OdbcParameter("p9", OdbcType.NChar);
                       p9.Value = "";
                       OdbcParameter p10 = new OdbcParameter("p10", OdbcType.Int);
                       p10.Value = 0;
                       OdbcParameter p11 = new OdbcParameter("p11", OdbcType.Int);
                       p11.Value = poradi;

                       cmd2.Parameters.Add(p1);
                       cmd2.Parameters.Add(p2);
                       cmd2.Parameters.Add(p3);
                       cmd2.Parameters.Add(p4);
                       cmd2.Parameters.Add(p5);
                       cmd2.Parameters.Add(p6);
                       cmd2.Parameters.Add(p7);
                       cmd2.Parameters.Add(p8);
                       cmd2.Parameters.Add(p9);
                       cmd2.Parameters.Add(p10);
                       cmd2.Parameters.Add(p11);
                        
                       cmd2.Transaction = transaction;
                       cmd2.ExecuteNonQuery();

                        // pridani radku do tabulky zruseneho materialu
                       OdbcCommand cmd3 = new OdbcCommand(commandString7, myDBConn as OdbcConnection);

                       OdbcParameter pz0 = new OdbcParameter("p0", OdbcType.Int);
                       pz0.Value = poradiPoskozeno ;
                       OdbcParameter pz1 = new OdbcParameter("p1", OdbcType.NChar); // prijmeni
                       pz1.Value = DBprijmeni;
                       OdbcParameter pz2 = new OdbcParameter("p2", OdbcType.Int);
                       pz2.Value = osCislo;
                       OdbcParameter pz3 = new OdbcParameter("p3", OdbcType.NChar);
                       pz3.Value = DBstredisko;
                       OdbcParameter pz4 = new OdbcParameter("p4", OdbcType.NChar);
                       pz4.Value = DBprovoz;
                       OdbcParameter pz5 = new OdbcParameter("p5", OdbcType.NChar);
                       pz5.Value = DBcisZak;
                       OdbcParameter pz6 = new OdbcParameter("p6", OdbcType.NChar);
                       pz6.Value = DBnazev;
                       OdbcParameter pz7 = new OdbcParameter("p7", OdbcType.NChar);
                       pz7.Value = DBJK;
                       OdbcParameter pz8 = new OdbcParameter("p8", OdbcType.NChar);
                       pz8.Value = DBrozmer;
                       OdbcParameter pz9 = new OdbcParameter("p9", OdbcType.Int);
                       pz9.Value = DBvydej;
                       OdbcParameter pz10 = new OdbcParameter("p10", OdbcType.Double);
                       pz10.Value = DBcena;
                       OdbcParameter pz11 = new OdbcParameter("p11", OdbcType.Date);
                       pz11.Value = DBdatum;
                       OdbcParameter pz12 = new OdbcParameter("p12", OdbcType.NChar);
                       pz12.Value = DBcsn;
                       OdbcParameter pz13 = new OdbcParameter("p13", OdbcType.NChar);
                       pz13.Value = DBjmeno;
                       OdbcParameter pz14 = new OdbcParameter("p14", OdbcType.Double);
                       pz14.Value = DBcelkCena;
                       OdbcParameter pz15 = new OdbcParameter("p15", OdbcType.NChar);
                       pz15.Value = ""; // DBvevCislo;
                       OdbcParameter pz16 = new OdbcParameter("p15", OdbcType.NChar);
                       pz16.Value = DBkonto;

                       cmd3.Parameters.Add(pz0);
                       cmd3.Parameters.Add(pz1);
                       cmd3.Parameters.Add(pz2);
                       cmd3.Parameters.Add(pz3);
                       cmd3.Parameters.Add(pz4);
                       cmd3.Parameters.Add(pz5);
                       cmd3.Parameters.Add(pz6);
                       cmd3.Parameters.Add(p7);
                       cmd3.Parameters.Add(p8);
                       cmd3.Parameters.Add(p9);
                       cmd3.Parameters.Add(p10);
                       cmd3.Parameters.Add(p11);
                       cmd3.Parameters.Add(pz12);
                       cmd3.Parameters.Add(pz13);
                       cmd3.Parameters.Add(pz14);
                       cmd3.Parameters.Add(pz15);
                       cmd3.Parameters.Add(pz16);
                       cmd3.Transaction = transaction;
                       cmd3.ExecuteNonQuery();

                       OdbcCommand cmd4 = new OdbcCommand(commandString7, myDBConn as OdbcConnection);
                       cmd4.Transaction = transaction;
                       cmd4.ExecuteNonQuery();


                    }
                    else
                    {
                       myReader2.Close();
                       return -1;
                    }


                }
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Rollback();
                    }
                    return -1;  // chyba
                }
                return 0;  // ok
            }
            return -1;  // databaze neni otevrena
        }



  //      public override Hashtable getNaradiZmenyLine(Int32 poradi, Hashtable DBRow)
  //      {
  //          if (DBRow == null) DBRow = new Hashtable();

        //          string DBSelect = "SELECT n.poradi AS poradi, n.fyzstav AS fyzstav, n.ucetstav AS ucetstav, x.zustatek AS zmeny_zustatek FROM naradi n, " +
  //                            "TABLE ( MULTISET (SELECT * FROM zmeny z WHERE z.poradi = (SELECT MAX(s.poradi) FROM zmeny s " +
  //                            "WHERE z.parporadi = s.parporadi GROUP BY s.parporadi) )) x WHERE n.poradi = x.parporadi AND n.poradi = " + poradi.ToString();

    //        return getDBLine(DBSelect, DBRow);
      //  }


    }
}
