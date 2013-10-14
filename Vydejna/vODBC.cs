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


namespace Vydejna
{
    class vODBC : vDatabase
    {

        public vODBC(string dataBaseName, string serverAddress, string serverName, string port, string locale, string driver, string userName, string password)
            : base(dataBaseName, serverAddress, serverName, port, locale, driver, userName, password)
        {
        }

        public override void openDB()
        {
        dBConnectionState = false;
        MessageBox.Show("Připojeni této database není naprogramováno.");
        }

        public override void DropTables()
        {
            openDB();
            if (DBIsOpened())
            {
                OdbcCommand cmdKarta = new OdbcCommand("DROP TABLE karta", myDBConn as OdbcConnection);
                try
                {
                    cmdKarta.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cmdKarta.Dispose();
                }


                OdbcCommand cmdNaradi = new OdbcCommand("DROP TABLE naradi", myDBConn as OdbcConnection);
                try
                {
                    cmdNaradi.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cmdNaradi.Dispose();
                }

                OdbcCommand cmdSequence = new OdbcCommand("DROP TABLE tabseq", myDBConn as OdbcConnection);
                try
                {
                    cmdSequence.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cmdSequence.Dispose();
                }


                OdbcCommand cmdVraceno = new OdbcCommand("DROP TABLE vraceno", myDBConn as OdbcConnection);
                try
                {
                    cmdVraceno.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cmdVraceno.Dispose();
                }

                OdbcCommand cmdPoskozeno = new OdbcCommand("DROP TABLE poskozeno", myDBConn as OdbcConnection);
                try
                {
                    cmdPoskozeno.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cmdPoskozeno.Dispose();
                }


                OdbcCommand cmdOsoby = new OdbcCommand("DROP TABLE osoby", myDBConn as OdbcConnection);
                try
                {
                    cmdOsoby.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cmdOsoby.Dispose();
                }

                OdbcCommand cmdZmeny = new OdbcCommand("DROP TABLE zmeny", myDBConn as OdbcConnection);
                try
                {
                    cmdZmeny.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cmdZmeny.Dispose();
                }



             myDBConn.Close();
             myDBConn.Dispose();
             MessageBox.Show("Rušení tabulek dokončeno.");

            }
        }



        public override void CreateTables()
        {
            string commandStringSequence = "create table tabseq ( nazev char (15), poradi integer);";
            string commandStringSequenceInit1 = "INSERT INTO tabseq ( nazev, poradi) VALUES ('naradi', 1)";
            string commandStringSequenceInit2 = "INSERT INTO tabseq ( nazev, poradi) VALUES ('vraceno', 1)";
            string commandStringSequenceInit3 = "INSERT INTO tabseq ( nazev, poradi) VALUES ('poskozeno', 1)";


            string commandStringKarta = "create table karta ( poradi integer, nazev char(60), jk char(15), normacsn char (15)," +
                      "normadin char(15), vyrobce char(40), cena float, poznamka char(60), minimum integer," +
                      "celkcena float," +
                      "ucetstav integer, fyzstav integer, rozmer char(20), analucet char(5), tdate date," +
                      "stredisko char(5), kodzmeny char(3), druh char(3), odpis char(3), zavod char(3));";


            string commandStringNaradi = "create table naradi ( poradi integer, nazev char(60), jk char(15), normacsn char (15)," +
                      "normadin char(15), vyrobce char(40), cena float, poznamka char(60), minimum integer," +
                      "celkcena float," +
                      "ucetstav integer, fyzstav integer, rozmer char(20), analucet char(5), tdate date," +
                      "stredisko char(5), kodzmeny char(3), druh char(3), odpis char(3), zavod char(3), ucetkscen float," +
                      "test char(1), pomroz char(1), kdatum date, kodd char(2) );";


            string commandStringPoskozeno = "create table poskozeno ( poradi integer, jmeno char(15), cislo integer, dilna char(15)," +
                      "pracoviste char(20), vyrobek char(15),nazev char(60), jk char(15), rozmer char(25)," +
                      "pocetks integer, cena float, datum date, csn char(15), krjmeno char(15)," +
                      "celkcena float, vevcislo char(12), konto char(15) );";



            string commandStringVraceno = "create table vraceno ( poradi integer, jmeno char(15), cislo integer, dilna char(15)," +
                      "pracoviste char(20), vyrobek char(15),nazev char(60), jk char(15), rozmer char(25)," +
                      "pocetks integer, cena float, datum date, csn char(15), krjmeno char(15)," +
                      "celkcena float, vevcislo char(12), konto char(15) );";



            string commandStringOsoby = "create table osoby ( jmeno varchar(15), prijmeni varchar(15), ulice varchar(20)," +
                      "mesto varchar(25), psc varchar(7),telhome varchar(15), oscislo varchar(8), odeleni varchar(20)," +
                      "telzam varchar(15), stredisko varchar(10), pujsoub varchar(12), pracoviste varchar(10), cisznamky varchar(5), poznamka varchar(120) );";



            string commandStringZmeny = "create table zmeny ( parporadi integer, pomozjk char(15), datum date, poznamka char(22)," +
                      "prijem integer, vydej integer, zustatek integer, zapkarta char(5), vevcislo char(12)," +
                      "pocivc integer, poradi integer );";


            openDB();
            if (DBIsOpened())
            {
            OdbcCommand cmdKarta = new OdbcCommand(commandStringKarta, myDBConn as OdbcConnection);
            OdbcCommand cmdNaradi = new OdbcCommand(commandStringNaradi, myDBConn as OdbcConnection);
            OdbcCommand cmdSequence = new OdbcCommand(commandStringSequence, myDBConn as OdbcConnection);
            OdbcCommand cmdSequenceInit1 = new OdbcCommand(commandStringSequenceInit1, myDBConn as OdbcConnection);
            OdbcCommand cmdSequenceInit2 = new OdbcCommand(commandStringSequenceInit2, myDBConn as OdbcConnection);
            OdbcCommand cmdSequenceInit3 = new OdbcCommand(commandStringSequenceInit3, myDBConn as OdbcConnection);
            OdbcCommand cmdVraceno = new OdbcCommand(commandStringVraceno, myDBConn as OdbcConnection);
            OdbcCommand cmdPoskozeno = new OdbcCommand(commandStringPoskozeno, myDBConn as OdbcConnection);
            OdbcCommand cmdOsoby = new OdbcCommand(commandStringOsoby, myDBConn as OdbcConnection);
            OdbcCommand cmdZmeny = new OdbcCommand(commandStringZmeny, myDBConn as OdbcConnection);

            try
                {
                    cmdKarta.ExecuteNonQuery();
                    cmdNaradi.ExecuteNonQuery();
                    cmdSequence.ExecuteNonQuery();
                    cmdSequenceInit1.ExecuteNonQuery();
                    cmdSequenceInit2.ExecuteNonQuery();
                    cmdSequenceInit3.ExecuteNonQuery();
                    cmdVraceno.ExecuteNonQuery();
                    cmdPoskozeno.ExecuteNonQuery();
                    cmdOsoby.ExecuteNonQuery();
                    cmdZmeny.ExecuteNonQuery();
                    MessageBox.Show("Tabulky byly vytvořeny.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    myDBConn.Close();
                    cmdZmeny.Dispose();
                    cmdOsoby.Dispose();
                    cmdPoskozeno.Dispose();
                    cmdVraceno.Dispose();
                    cmdSequenceInit1.Dispose();
                    cmdSequenceInit2.Dispose();
                    cmdSequenceInit3.Dispose();
                    cmdSequence.Dispose();
                    cmdNaradi.Dispose();
                    cmdKarta.Dispose();
                    myDBConn.Dispose();
                }
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
                    cmdZmeny.Dispose();
                    cmdOsoby.Dispose();
                    cmdPoskozeno.Dispose();
                    cmdVraceno.Dispose();
                    cmdSequence.Dispose();
                    cmdKarta.Dispose();
                    cmdNaradi.Dispose();
//                    myDBConn.Close();
//                    myDBConn.Dispose();
                }
            }

            // vymaze tabulky

        }



        public override Int32 addLineKaret(string DBnazev,string DBJK, string DBnormacsn, string DBnormadin,
                                         string DBvyrobce, double DBcena, string DBpoznamka, int DBminstav,
                                         double DBcelkcena, int DBucetstav, int DBfyzstav,
                                         string DBrozmer, string DBanalucet, DateTime DBdate, string DBstredisko,
                                         string DBkodzmeny, string DBdruhp, string DBodpis, string DBzavod)
        {
            string commandStringSeq1 = "SELECT poradi FROM tabseq WHERE nazev = 'naradi'";
            string commandStringSeq2 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'naradi'";

            string commandString = "INSERT INTO karta ( poradi, nazev, jk, normacsn, normadin, vyrobce, cena, poznamka, minimum, celkcena, ucetstav, fyzstav, rozmer, analucet, tdate, stredisko, kodzmeny, druh, odpis, zavod ) " +
                  "VALUES ( ? ,?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";


            if (DBIsOpened())
            {


                OdbcCommand cmdSeq1 = new OdbcCommand(commandStringSeq1, myDBConn as OdbcConnection);

                OdbcDataReader seqReader = cmdSeq1.ExecuteReader();
                seqReader.Read();
                int poradi = seqReader.GetInt32(0);
                seqReader.Close();


                OdbcCommand cmd = new OdbcCommand(commandString, myDBConn as OdbcConnection);

                OdbcParameter p0 = new OdbcParameter("p1", OdbcType.Int);
                p0.Value = poradi;
                OdbcParameter p1 = new OdbcParameter("p1", OdbcType.NChar);
                p1.Value = DBnazev;
                OdbcParameter p2 = new OdbcParameter("p2", OdbcType.NChar);
                p2.Value = DBJK;
                OdbcParameter p3 = new OdbcParameter("p3", OdbcType.NChar);
                p3.Value = DBnormacsn;
                OdbcParameter p4 = new OdbcParameter("p4", OdbcType.NChar);
                p4.Value = DBnormadin;
                OdbcParameter p5 = new OdbcParameter("p5", OdbcType.NChar);
                p5.Value = DBvyrobce;
                OdbcParameter p6 = new OdbcParameter("p6", OdbcType.Double);
                p6.Value = DBcena;
                OdbcParameter p7 = new OdbcParameter("p7", OdbcType.NChar);
                p7.Value = DBpoznamka;
                OdbcParameter p8 = new OdbcParameter("p8", OdbcType.Int);
                p8.Value = DBminstav;
                OdbcParameter p9 = new OdbcParameter("p9", OdbcType.Double);
                p9.Value = DBcelkcena;
                OdbcParameter p15 = new OdbcParameter("p15", OdbcType.Int);
                p15.Value = DBucetstav;
                OdbcParameter p16 = new OdbcParameter("p16", OdbcType.Int);
                p16.Value = DBfyzstav;
                OdbcParameter p17 = new OdbcParameter("p17", OdbcType.NChar);
                p17.Value = DBrozmer;
                OdbcParameter p18 = new OdbcParameter("p18", OdbcType.NChar);
                p18.Value = DBanalucet;
                OdbcParameter p19 = new OdbcParameter("p19", OdbcType.Date);
                p19.Value = DBdate;
                OdbcParameter p20 = new OdbcParameter("p20", OdbcType.NChar);
                p20.Value = DBstredisko;
                OdbcParameter p21 = new OdbcParameter("p21", OdbcType.NChar);
                p21.Value = DBkodzmeny;
                OdbcParameter p22 = new OdbcParameter("p22", OdbcType.NChar);
                p22.Value = DBdruhp;
                OdbcParameter p23 = new OdbcParameter("p23", OdbcType.NChar);
                p23.Value = DBodpis;
                OdbcParameter p24 = new OdbcParameter("p24", OdbcType.NChar);
                p24.Value = DBzavod;


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
                cmd.ExecuteNonQuery();

                OdbcCommand cmdSeq2 = new OdbcCommand(commandStringSeq2, myDBConn as OdbcConnection);


                cmdSeq2.ExecuteNonQuery();



                return poradi;
            }
            else return 0;
        }

        public override Int32 addLineNaradi(string DBnazev, string DBJK, string DBnormacsn, string DBnormadin,
                                         string DBvyrobce, double DBcena, string DBpoznamka, int DBminstav,
                                         double DBcelkcena, int DBucetstav, int DBfyzstav,
                                         string DBrozmer, string DBanalucet, DateTime DBdate, string DBstredisko,
                                         string DBkodzmeny, string DBdruhp, string DBodpis, string DBzavod,
                                         double DBucetkscen, string DBtest, string DBpomroz, DateTime DBkdatum, string DBkodd)
        {

            string commandStringSeq1 = "SELECT poradi FROM tabseq WHERE nazev = 'naradi'";
            string commandStringSeq2 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'naradi'";

            string commandString = "INSERT INTO naradi ( poradi, nazev, jk, normacsn, normadin, vyrobce, cena, poznamka, minimum, celkcena, ucetstav, fyzstav, rozmer, analucet, tdate, stredisko, kodzmeny, druh, odpis, zavod, ucetkscen, test, pomroz, kdatum, kodd ) " +
                  "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";

            if (DBIsOpened())
            {

                OdbcCommand cmdSeq1 = new OdbcCommand(commandStringSeq1, myDBConn as OdbcConnection);
                OdbcDataReader seqReader = cmdSeq1.ExecuteReader();
                seqReader.Read();
                int poradi = seqReader.GetInt32(0);
                seqReader.Close();              
                
                
                OdbcCommand cmd = new OdbcCommand(commandString, myDBConn as OdbcConnection);

                OdbcParameter p0 = new OdbcParameter("p1", OdbcType.Int);
                p0.Value = poradi;
                OdbcParameter p1 = new OdbcParameter("p1", OdbcType.NChar);
                p1.Value = DBnazev;
                OdbcParameter p2 = new OdbcParameter("p2", OdbcType.NChar);
                p2.Value = DBJK;
                OdbcParameter p3 = new OdbcParameter("p3", OdbcType.NChar);
                p3.Value = DBnormacsn;
                OdbcParameter p4 = new OdbcParameter("p4", OdbcType.NChar);
                p4.Value = DBnormadin;
                OdbcParameter p5 = new OdbcParameter("p5", OdbcType.NChar);
                p5.Value = DBvyrobce;
                OdbcParameter p6 = new OdbcParameter("p6", OdbcType.Double);
                p6.Value = DBcena;
                OdbcParameter p7 = new OdbcParameter("p7", OdbcType.NChar);
                p7.Value = DBpoznamka;
                OdbcParameter p8 = new OdbcParameter("p8", OdbcType.Int);
                p8.Value = DBminstav;
                OdbcParameter p9 = new OdbcParameter("p9", OdbcType.Double);
                p9.Value = DBcelkcena;
                OdbcParameter p15 = new OdbcParameter("p15", OdbcType.Int);
                p15.Value = DBucetstav;
                OdbcParameter p16 = new OdbcParameter("p16", OdbcType.Int);
                p16.Value = DBfyzstav;
                OdbcParameter p17 = new OdbcParameter("p17", OdbcType.NChar);
                p17.Value = DBrozmer;
                OdbcParameter p18 = new OdbcParameter("p18", OdbcType.NChar);
                p18.Value = DBanalucet;
                OdbcParameter p19 = new OdbcParameter("p19", OdbcType.Date);
                p19.Value = DBdate;
                OdbcParameter p20 = new OdbcParameter("p20", OdbcType.NChar);
                p20.Value = DBstredisko;
                OdbcParameter p21 = new OdbcParameter("p21", OdbcType.NChar);
                p21.Value = DBkodzmeny;
                OdbcParameter p22 = new OdbcParameter("p22", OdbcType.NChar);
                p22.Value = DBdruhp;
                OdbcParameter p23 = new OdbcParameter("p23", OdbcType.NChar);
                p23.Value = DBodpis;
                OdbcParameter p24 = new OdbcParameter("p24", OdbcType.NChar);
                p24.Value = DBzavod;
                OdbcParameter p25 = new OdbcParameter("p25", OdbcType.Double);
                p25.Value = DBucetkscen;
                OdbcParameter p26 = new OdbcParameter("p26", OdbcType.NChar);
                p26.Value = DBtest;
                OdbcParameter p27 = new OdbcParameter("p27", OdbcType.NChar);
                p27.Value = DBpomroz;
                OdbcParameter p28 = new OdbcParameter("p28", OdbcType.Date);
                p28.Value = DBkdatum;
                OdbcParameter p29 = new OdbcParameter("p29", OdbcType.NChar);
                p29.Value = DBkodd;


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
                cmd.ExecuteNonQuery();

                OdbcCommand cmdSeq2 = new OdbcCommand(commandStringSeq2, myDBConn as OdbcConnection);
                cmdSeq2.ExecuteNonQuery();
                return poradi;
            }
            return 0;
        }


        public override Int32 addLineVraceno(string DBjmeno, int DBcislo, string DBdilna, string DBpracoviste,
                                         string DBvyrobek, string DBnazev, string DBJK, string DBrozmer, int DBpocetks,
                                         double DBcena, DateTime DBdate, string DBnormacsn, string DBkrjmeno,
                                         double DBcelkCena, string DBvevCislo, string DBkonto)
        {
            string commandStringSeq1 = "SELECT poradi FROM tabseq WHERE nazev = 'vraceno'";
            string commandStringSeq2 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'vraceno'";

            string commandString = "INSERT INTO vraceno ( poradi, jmeno, cislo, dilna, pracoviste, vyrobek, nazev, jk, rozmer, pocetks, cena, datum, csn, krjmeno, celkcena, vevcislo, konto) " +
                  "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";

            if (DBIsOpened())
            {
                OdbcCommand cmdSeq1 = new OdbcCommand(commandStringSeq1, myDBConn as OdbcConnection);
                OdbcDataReader seqReader = cmdSeq1.ExecuteReader();
                seqReader.Read();
                Int32 poradi = seqReader.GetInt32(0);
                seqReader.Close();              

                OdbcCommand cmd = new OdbcCommand(commandString, myDBConn as OdbcConnection);

                OdbcParameter p0 = new OdbcParameter("p0",OdbcType.Int);
                p0.Value = poradi;
                OdbcParameter p1 = new OdbcParameter("p1", OdbcType.NChar);
                p1.Value = DBjmeno;
                OdbcParameter p2 = new OdbcParameter("p2", OdbcType.Int);
                p2.Value = DBcislo;
                OdbcParameter p3 = new OdbcParameter("p3", OdbcType.NChar);
                p3.Value = DBdilna;
                OdbcParameter p4 = new OdbcParameter("p4", OdbcType.NChar);
                p4.Value = DBpracoviste;
                OdbcParameter p5 = new OdbcParameter("p5", OdbcType.NChar);
                p5.Value = DBvyrobek;
                OdbcParameter p6 = new OdbcParameter("p6", OdbcType.NChar);
                p6.Value = DBnazev;
                OdbcParameter p7 = new OdbcParameter("p7", OdbcType.NChar);
                p7.Value = DBJK;
                OdbcParameter p8 = new OdbcParameter("p8", OdbcType.NChar);
                p8.Value = DBrozmer;
                OdbcParameter p9 = new OdbcParameter("p9", OdbcType.Int);
                p9.Value = DBpocetks;
                OdbcParameter p10 = new OdbcParameter("p10", OdbcType.Double);
                p10.Value = DBcena;
                OdbcParameter p11 = new OdbcParameter("p11", OdbcType.Date);
                p11.Value = DBdate;
                OdbcParameter p12 = new OdbcParameter("p12", OdbcType.NChar);
                p12.Value = DBnormacsn;
                OdbcParameter p13 = new OdbcParameter("p13", OdbcType.NChar);
                p13.Value = DBkrjmeno;
                OdbcParameter p14 = new OdbcParameter("p14", OdbcType.Double);
                p14.Value = DBcelkCena;
                OdbcParameter p15 = new OdbcParameter("p15", OdbcType.NChar);
                p15.Value = DBvevCislo;
                OdbcParameter p16 = new OdbcParameter("p16", OdbcType.NChar);
                p16.Value = DBkonto;

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
                cmd.Parameters.Add(p10);
                cmd.Parameters.Add(p11);
                cmd.Parameters.Add(p12);
                cmd.Parameters.Add(p13);
                cmd.Parameters.Add(p14);
                cmd.Parameters.Add(p15);
                cmd.Parameters.Add(p16);
                cmd.ExecuteNonQuery();

                OdbcCommand cmdSeq2 = new OdbcCommand(commandStringSeq2, myDBConn as OdbcConnection);
                cmdSeq2.ExecuteNonQuery();
                return poradi;

            }
            return 0;
        }





        public override Int32 addLinePoskozeno(string DBjmeno, int DBcislo, string DBdilna, string DBpracoviste,
                                         string DBvyrobek, string DBnazev, string DBJK, string DBrozmer, int DBpocetks,
                                         double DBcena, DateTime DBdate, string DBnormacsn, string DBkrjmeno,
                                         double DBcelkCena, string DBvevCislo, string DBkonto)
        {

            string commandStringSeq1 = "SELECT poradi FROM tabseq WHERE nazev = 'poskozeno'";
            string commandStringSeq2 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'poskozeno'";

            string commandString = "INSERT INTO poskozeno ( poradi, jmeno, cislo, dilna, pracoviste, vyrobek, nazev, jk, rozmer, pocetks, cena, datum, csn, krjmeno, celkcena, vevcislo, konto) " +
                  "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";



            if (DBIsOpened())
            {

                OdbcCommand cmdSeq1 = new OdbcCommand(commandStringSeq1, myDBConn as OdbcConnection);
                OdbcDataReader seqReader = cmdSeq1.ExecuteReader();
                seqReader.Read();
                Int32 poradi = seqReader.GetInt32(0);
                seqReader.Close();

                OdbcCommand cmd = new OdbcCommand(commandString, myDBConn as OdbcConnection);

                OdbcParameter p0 = new OdbcParameter("p0", OdbcType.Int);
                p0.Value = poradi;
                OdbcParameter p1 = new OdbcParameter("p1", OdbcType.NChar);
                p1.Value = DBjmeno;
                OdbcParameter p2 = new OdbcParameter("p2", OdbcType.Int);
                p2.Value = DBcislo;
                OdbcParameter p3 = new OdbcParameter("p3", OdbcType.NChar);
                p3.Value = DBdilna;
                OdbcParameter p4 = new OdbcParameter("p4", OdbcType.NChar);
                p4.Value = DBpracoviste;
                OdbcParameter p5 = new OdbcParameter("p5", OdbcType.NChar);
                p5.Value = DBvyrobek;
                OdbcParameter p6 = new OdbcParameter("p6", OdbcType.NChar);
                p6.Value = DBnazev;
                OdbcParameter p7 = new OdbcParameter("p7", OdbcType.NChar);
                p7.Value = DBJK;
                OdbcParameter p8 = new OdbcParameter("p8", OdbcType.NChar);
                p8.Value = DBrozmer;
                OdbcParameter p9 = new OdbcParameter("p9", OdbcType.Int);
                p9.Value = DBpocetks;
                OdbcParameter p10 = new OdbcParameter("p10", OdbcType.Double);
                p10.Value = DBcena;
                OdbcParameter p11 = new OdbcParameter("p11", OdbcType.Date);
                p11.Value = DBdate;
                OdbcParameter p12 = new OdbcParameter("p12", OdbcType.NChar);
                p12.Value = DBnormacsn;
                OdbcParameter p13 = new OdbcParameter("p13", OdbcType.NChar);
                p13.Value = DBkrjmeno;
                OdbcParameter p14 = new OdbcParameter("p14", OdbcType.Double);
                p14.Value = DBcelkCena;
                OdbcParameter p15 = new OdbcParameter("p15", OdbcType.NChar);
                p15.Value = DBvevCislo;
                OdbcParameter p16 = new OdbcParameter("p15", OdbcType.NChar);
                p16.Value = DBkonto;

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
                cmd.Parameters.Add(p10);
                cmd.Parameters.Add(p11);
                cmd.Parameters.Add(p12);
                cmd.Parameters.Add(p13);
                cmd.Parameters.Add(p14);
                cmd.Parameters.Add(p15);
                cmd.Parameters.Add(p16);
                cmd.ExecuteNonQuery();

                OdbcCommand cmdSeq2 = new OdbcCommand(commandStringSeq2, myDBConn as OdbcConnection);
                cmdSeq2.ExecuteNonQuery();
                return poradi;
            }
            return 0;
        }


        public override void addLineOsoby(string DBprijmeni, string DBjmeno, string DBulice, string DBmesto,
                                         string DBpsc, string DBtelHome, string DBosCislo, string DBodeleni, string DBtelZam,
                                         string DBstredisko, string DBpujSoub, string DBpracoviste, string DBcisZnamky,
                                         string DBPoznamka)
        {

            string commandString = "INSERT INTO osoby (prijmeni, jmeno, ulice, mesto, psc, telhome, oscislo, odeleni, telzam, stredisko, pujsoub, pracoviste, cisznamky, poznamka )" +
                  "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";




            if (DBIsOpened())
            {

                OdbcCommand cmd = new OdbcCommand(commandString, myDBConn as OdbcConnection);

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
                p8.Value = DBodeleni;
                OdbcParameter p9 = new OdbcParameter("p9", OdbcType.NVarChar);
                p9.Value = DBtelZam;
                OdbcParameter p10 = new OdbcParameter("p10", OdbcType.NVarChar);
                p10.Value = DBstredisko;
                OdbcParameter p11 = new OdbcParameter("p11", OdbcType.NVarChar);
                p11.Value = DBpujSoub;
                OdbcParameter p12 = new OdbcParameter("p12", OdbcType.NVarChar);
                p12.Value = DBpracoviste;
                OdbcParameter p13 = new OdbcParameter("p13", OdbcType.NVarChar);
                p13.Value = DBcisZnamky;
                OdbcParameter p14 = new OdbcParameter("p14", OdbcType.NVarChar);
                p14.Value = DBPoznamka;

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
                cmd.ExecuteNonQuery();
            }
        }


        public override void addLineZmeny(int DBparPoradi, string DBpomocJK, DateTime DBdatum, string DBpoznamka, int DBPrijem,
                                         int DBvydej, int DBzustatek, string DBzapKarta, string DBvevCislo,
                                         int DBpocIvc, int DBporadi)
        {

            string commandString = "INSERT INTO zmeny (parporadi, pomozjk, datum, poznamka, prijem, vydej, zustatek, zapkarta, vevcislo, pocivc, poradi )" + //, nazev, vyber, lastsoub, aktadr, cena, ucetkscen, jk )" +
                  "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";


            if (DBIsOpened())
            {

                OdbcCommand cmd = new OdbcCommand(commandString, myDBConn as OdbcConnection);

                OdbcParameter p0 = new OdbcParameter("p0", OdbcType.Int);
                p0.Value = DBparPoradi;
                OdbcParameter p1 = new OdbcParameter("p1", OdbcType.NChar);
                p1.Value = DBpomocJK;
                OdbcParameter p2 = new OdbcParameter("p2", OdbcType.Date);
                p2.Value = DBdatum;
                OdbcParameter p3 = new OdbcParameter("p3", OdbcType.NChar);
                p3.Value = DBpoznamka;
                OdbcParameter p4 = new OdbcParameter("p4", OdbcType.Int);
                p4.Value = DBPrijem;
                OdbcParameter p5 = new OdbcParameter("p5", OdbcType.Int);
                p5.Value = DBvydej;
                OdbcParameter p6 = new OdbcParameter("p6", OdbcType.Int);
                p6.Value = DBzustatek;
                OdbcParameter p7 = new OdbcParameter("p7", OdbcType.NChar);
                p7.Value = DBzapKarta;
                OdbcParameter p8 = new OdbcParameter("p8", OdbcType.NChar);
                p8.Value = DBvevCislo;
                OdbcParameter p9 = new OdbcParameter("p9", OdbcType.Int);
                p9.Value = DBpocIvc;
                OdbcParameter p14 = new OdbcParameter("p14", OdbcType.Int);
                p14.Value = DBporadi;


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
                cmd.Parameters.Add(p14);
                cmd.ExecuteNonQuery();
            }
        }

        public override Int32 addNewLineNaradi(string DBnazev, string DBJK, string DBnormacsn, string DBnormadin,
                                         string DBvyrobce, decimal DBcena, string DBpoznamka, long DBminstav,
                                         decimal DBcelkcena, long DBucetstav, long DBfyzstav,
                                         string DBrozmer, string DBanalucet, decimal DBucetkscen, DateTime DBkdatum)
        {
            return -1;
        }


        public override Int32 addNewLineZmeny(Int32 DBporadi, string DBJK, DateTime DBdatum, Int32 DBprijem, Int32 DBvydej, string DBpoznamka)
        {
            return -1;
        }



                // pridani nove polozky do tabulky osoby
        public override Int32 addNewLineOsoby(string DBprijmeni, string DBjmeno, string DBulice, string DBmesto,
                                         string DBpsc, string DBtelHome, string DBosCislo, string DBstredisko,
                                         string DBcisZnamky, string DBoddeleni, string DBpracoviste, string DBtelZam,
                                         string DBpoznamka)
        {
            return -1;
        }

        public override Boolean editNewLineNaradi(Int32 poradi, string DBnazev, string DBJK, string DBnormacsn, string DBnormadin,
                                         string DBvyrobce, decimal DBcena, string DBpoznamka, long DBminstav,
                                         decimal DBcelkcena, long DBucetstav, long DBfyzstav,
                                         string DBrozmer, string DBanalucet, decimal DBucetkscen)
        {
            string commandString2 = "UPDATE naradi set nazev = ?, jk = ?, normacsn = ?, normadin = ?, vyrobce = ?, cena = ?, poznamka = ?, minimum = ?, celkcena = ?,  ucetstav = ?, fyzstav = ?, rozmer = ?, analucet = ?, ucetkscen = ? " +
                            "where  poradi = ?";

            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                try
                {
                    transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                    OdbcCommand cmd = new OdbcCommand(commandString2, myDBConn as OdbcConnection);

                    OdbcParameter p1 = new OdbcParameter("p1", OdbcType.NChar);
                    p1.Value = DBnazev;
                    OdbcParameter p2 = new OdbcParameter("p2", OdbcType.NChar);
                    p2.Value = DBJK;
                    OdbcParameter p3 = new OdbcParameter("p3", OdbcType.NChar);
                    p3.Value = DBnormacsn;
                    OdbcParameter p4 = new OdbcParameter("p4", OdbcType.NChar);
                    p4.Value = DBnormadin;
                    OdbcParameter p5 = new OdbcParameter("p5", OdbcType.NChar);
                    p5.Value = DBvyrobce;
                    OdbcParameter p6 = new OdbcParameter("p6", OdbcType.Double);
                    p6.Value = DBcena;
                    OdbcParameter p7 = new OdbcParameter("p7", OdbcType.NChar);
                    p7.Value = DBpoznamka;
                    OdbcParameter p8 = new OdbcParameter("p8", OdbcType.Int);
                    p8.Value = DBminstav;
                    OdbcParameter p9 = new OdbcParameter("p9", OdbcType.Double);
                    p9.Value = DBcelkcena;
                    OdbcParameter p15 = new OdbcParameter("p15", OdbcType.Int);
                    p15.Value = DBucetstav;
                    OdbcParameter p16 = new OdbcParameter("p16", OdbcType.Int);
                    p16.Value = DBfyzstav;
                    OdbcParameter p17 = new OdbcParameter("p17", OdbcType.NChar);
                    p17.Value = DBrozmer;
                    OdbcParameter p18 = new OdbcParameter("p18", OdbcType.NChar);
                    p18.Value = DBanalucet;
                    OdbcParameter p25 = new OdbcParameter("p25", OdbcType.Double);
                    p25.Value = DBucetkscen;
                    OdbcParameter p30 = new OdbcParameter("p30", OdbcType.NChar);
                    p30.Value = poradi;


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
                    cmd.Parameters.Add(p25);
                    cmd.Parameters.Add(p30);

                    cmd.Transaction = transaction;

                    cmd.ExecuteNonQuery();
                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Commit();
                    }
                    return true;

                }
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Rollback();
                    }
                    return false;
                }

            }

            return true;

        }


        public override Boolean editNewLinePoskozene(Int32 poradi, string DBkrjmeno, string DBjmeno, string DBosCislo, string DBdilna,
                                 string DBprovoz, string DBnazev, string DBJK, long DBpocetKS,
                                 string DBrozmer, string DBCSN, decimal DBcena,
                                 DateTime DBdatum, string DBvyrobek, string DBkonto)
        {

            string commandString1 = "UPDATE poskozeno set jmeno = ?, cislo =?, dilna = ?, pracoviste = ?, vyrobek = ?, nazev = ?, rozmer = ?, pocetks = ?, cena = ?, datum = ?, csn = ?, krjmeno = ?, konto = ?, jk = ? " +
                  "where  poradi = ?";

            OdbcTransaction transaction = null;
            if (DBIsOpened())
            {
                try
                {
                    transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                    OdbcCommand cmd = new OdbcCommand(commandString1, myDBConn as OdbcConnection);

                    OdbcParameter p1 = new OdbcParameter("p1", OdbcType.NChar);
                    p1.Value = DBjmeno;
                    OdbcParameter p2 = new OdbcParameter("p2", OdbcType.NChar);
                    p2.Value = DBosCislo;
                    OdbcParameter p3 = new OdbcParameter("p3", OdbcType.NChar);
                    p3.Value = DBdilna;
                    OdbcParameter p4 = new OdbcParameter("p4", OdbcType.NChar);
                    p4.Value = DBprovoz;
                    OdbcParameter p5 = new OdbcParameter("p5", OdbcType.NChar);
                    p5.Value = DBvyrobek;
                    OdbcParameter p6 = new OdbcParameter("p6", OdbcType.NChar);
                    p6.Value = DBnazev;
                    OdbcParameter p7 = new OdbcParameter("p7", OdbcType.NChar);
                    p7.Value = DBrozmer;
                    OdbcParameter p8 = new OdbcParameter("p8", OdbcType.Int);
                    p8.Value = DBpocetKS;
                    OdbcParameter p9 = new OdbcParameter("p9", OdbcType.Double);
                    p9.Value = DBcena;
                    OdbcParameter p10 = new OdbcParameter("p10", OdbcType.Date);
                    p10.Value = DBdatum;
                    OdbcParameter p11 = new OdbcParameter("p11", OdbcType.NChar);
                    p11.Value = DBCSN;
                    OdbcParameter p12 = new OdbcParameter("p12", OdbcType.NChar);
                    p12.Value = DBkrjmeno;
                    OdbcParameter p13 = new OdbcParameter("p13", OdbcType.NChar);
                    p13.Value = DBkonto;
                    OdbcParameter p14 = new OdbcParameter("p14", OdbcType.NChar);
                    p14.Value = DBJK;
                    OdbcParameter p15 = new OdbcParameter("p15", OdbcType.Int);
                    p15.Value = poradi;


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
                    cmd.Parameters.Add(p15);

                    cmd.Transaction = transaction;

                    cmd.ExecuteNonQuery();
                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Commit();
                    }
                    return true;

                }
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Rollback();
                    }
                    return false;
                }

            }

            return true;
        }



        public override Boolean editNewLineVracene(Int32 poradi, string DBkrjmeno, string DBjmeno, string DBosCislo, string DBdilna,
                                 string DBprovoz, string DBnazev, string DBJK, long DBpocetKS,
                                 string DBrozmer, string DBCSN, decimal DBcena,
                                 DateTime DBdatum, string DBvyrobek, string DBkonto)
        {

            string commandString1 = "UPDATE vraceno set jmeno = ?, cislo =?, dilna = ?, pracoviste = ?, vyrobek = ?, nazev = ?, rozmer = ?, pocetks = ?, cena = ?, datum = ?, csn = ?, krjmeno = ?, konto = ?, jk = ? " +
                  "where  poradi = ?";

            OdbcTransaction transaction = null;
            if (DBIsOpened())
            {
                try
                {
                    transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                    OdbcCommand cmd = new OdbcCommand(commandString1, myDBConn as OdbcConnection);

                    OdbcParameter p1 = new OdbcParameter("p1", OdbcType.NChar);
                    p1.Value = DBjmeno;
                    OdbcParameter p2 = new OdbcParameter("p2", OdbcType.NChar);
                    p2.Value = DBosCislo;
                    OdbcParameter p3 = new OdbcParameter("p3", OdbcType.NChar);
                    p3.Value = DBdilna;
                    OdbcParameter p4 = new OdbcParameter("p4", OdbcType.NChar);
                    p4.Value = DBprovoz;
                    OdbcParameter p5 = new OdbcParameter("p5", OdbcType.NChar);
                    p5.Value = DBvyrobek;
                    OdbcParameter p6 = new OdbcParameter("p6", OdbcType.NChar);
                    p6.Value = DBnazev;
                    OdbcParameter p7 = new OdbcParameter("p7", OdbcType.NChar);
                    p7.Value = DBrozmer;
                    OdbcParameter p8 = new OdbcParameter("p8", OdbcType.Int);
                    p8.Value = DBpocetKS;
                    OdbcParameter p9 = new OdbcParameter("p9", OdbcType.Double);
                    p9.Value = DBcena;
                    OdbcParameter p10 = new OdbcParameter("p10", OdbcType.Date);
                    p10.Value = DBdatum;
                    OdbcParameter p11 = new OdbcParameter("p11", OdbcType.NChar);
                    p11.Value = DBCSN;
                    OdbcParameter p12 = new OdbcParameter("p12", OdbcType.NChar);
                    p12.Value = DBkrjmeno;
                    OdbcParameter p13 = new OdbcParameter("p13", OdbcType.NChar);
                    p13.Value = DBkonto;
                    OdbcParameter p14 = new OdbcParameter("p14", OdbcType.NChar);
                    p14.Value = DBJK;
                    OdbcParameter p15 = new OdbcParameter("p15", OdbcType.Int);
                    p15.Value = poradi;


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
                    cmd.Parameters.Add(p15);

                    cmd.Transaction = transaction;

                    cmd.ExecuteNonQuery();
                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Commit();
                    }
                    return true;

                }
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Rollback();
                    }
                    return false;
                }

            }

            return true;
        }


        public override Boolean editNewLineKaret(Int32 poradi, string DBnazev, string DBJK, string DBnormacsn, string DBnormadin,
                                         string DBvyrobce, decimal DBcena, string DBpoznamka, long DBminstav,
                                         decimal DBcelkcena, long DBucetstav, long DBfyzstav,
                                         string DBrozmer, string DBanalucet)
        {
            string commandString2 = "UPDATE naradi set nazev = ?, jk = ?, normacsn = ?, normadin = ?, vyrobce = ?, cena = ?, poznamka = ?, minimum = ?, celkcena = ?,  ucetstav = ?, fyzstav = ?, rozmer = ?, analucet = ?" +
                            "where  poradi = ?";

            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                try
                {
                    transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                    OdbcCommand cmd = new OdbcCommand(commandString2, myDBConn as OdbcConnection);

                    OdbcParameter p1 = new OdbcParameter("p1", OdbcType.NChar);
                    p1.Value = DBnazev;
                    OdbcParameter p2 = new OdbcParameter("p2", OdbcType.NChar);
                    p2.Value = DBJK;
                    OdbcParameter p3 = new OdbcParameter("p3", OdbcType.NChar);
                    p3.Value = DBnormacsn;
                    OdbcParameter p4 = new OdbcParameter("p4", OdbcType.NChar);
                    p4.Value = DBnormadin;
                    OdbcParameter p5 = new OdbcParameter("p5", OdbcType.NChar);
                    p5.Value = DBvyrobce;
                    OdbcParameter p6 = new OdbcParameter("p6", OdbcType.Double);
                    p6.Value = DBcena;
                    OdbcParameter p7 = new OdbcParameter("p7", OdbcType.NChar);
                    p7.Value = DBpoznamka;
                    OdbcParameter p8 = new OdbcParameter("p8", OdbcType.Int);
                    p8.Value = DBminstav;
                    OdbcParameter p9 = new OdbcParameter("p9", OdbcType.Double);
                    p9.Value = DBcelkcena;
                    OdbcParameter p15 = new OdbcParameter("p15", OdbcType.Int);
                    p15.Value = DBucetstav;
                    OdbcParameter p16 = new OdbcParameter("p16", OdbcType.Int);
                    p16.Value = DBfyzstav;
                    OdbcParameter p17 = new OdbcParameter("p17", OdbcType.NChar);
                    p17.Value = DBrozmer;
                    OdbcParameter p18 = new OdbcParameter("p18", OdbcType.NChar);
                    p18.Value = DBanalucet;
                    OdbcParameter p30 = new OdbcParameter("p30", OdbcType.NChar);
                    p30.Value = poradi;


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
                    cmd.Parameters.Add(p30);

                    cmd.Transaction = transaction;

                    cmd.ExecuteNonQuery();
                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Commit();
                    }
                    return true;

                }
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Rollback();
                    }
                    return false;
                }

            }

            return true;

        }


        public override Boolean editNewLineOsoby(string DBprijmeni, string DBjmeno, string DBulice, string DBmesto,
                                         string DBpsc, string DBtelHome, string DBosCislo, string DBstredisko,
                                         string DBcisZnamky, string DBoddeleni, string DBpracoviste, string DBtelZam,
                                         string DBpoznamka)
        {


            string commandString2 = "UPDATE osoby SET prijmeni = ?, jmeno = ?, ulice =?, mesto = ?, psc = ?, telhome = ?, odeleni = ?, telzam = ?, stredisko = ?, pujsoub = ?, pracoviste = ?, cisznamky = ?, poznamka = ? " +
                  "WHERE oscislo = ?";

            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                try
                {
                    transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);


                    OdbcCommand cmd = new OdbcCommand(commandString2, myDBConn as OdbcConnection);

                    OdbcParameter p1 = new OdbcParameter("p1", DbType.String);
                    p1.Value = DBprijmeni;
                    OdbcParameter p2 = new OdbcParameter("p2", DbType.String);
                    p2.Value = DBjmeno;
                    OdbcParameter p3 = new OdbcParameter("p3", DbType.String);
                    p3.Value = DBulice;
                    OdbcParameter p4 = new OdbcParameter("p4", DbType.String);
                    p4.Value = DBmesto;
                    OdbcParameter p5 = new OdbcParameter("p5", DbType.String);
                    p5.Value = DBpsc;
                    OdbcParameter p6 = new OdbcParameter("p6", DbType.String);
                    p6.Value = DBtelHome;
                    OdbcParameter p8 = new OdbcParameter("p8", DbType.String);
                    p8.Value = DBoddeleni;
                    OdbcParameter p9 = new OdbcParameter("p9", DbType.String);
                    p9.Value = DBtelZam;
                    OdbcParameter p10 = new OdbcParameter("p10", DbType.String);
                    p10.Value = DBstredisko;
                    OdbcParameter p11 = new OdbcParameter("p11", DbType.String);
                    p11.Value = "";
                    OdbcParameter p12 = new OdbcParameter("p12", DbType.String);
                    p12.Value = DBpracoviste;
                    OdbcParameter p13 = new OdbcParameter("p13", DbType.String);
                    p13.Value = DBcisZnamky;
                    OdbcParameter p14 = new OdbcParameter("p14", DbType.String);
                    p14.Value = DBpoznamka;
                    OdbcParameter p15 = new OdbcParameter("p15", DbType.String);
                    p15.Value = DBosCislo;


                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    cmd.Parameters.Add(p4);
                    cmd.Parameters.Add(p5);
                    cmd.Parameters.Add(p6);
                    cmd.Parameters.Add(p8);
                    cmd.Parameters.Add(p9);
                    cmd.Parameters.Add(p10);
                    cmd.Parameters.Add(p11);
                    cmd.Parameters.Add(p12);
                    cmd.Parameters.Add(p13);
                    cmd.Parameters.Add(p14);
                    cmd.Parameters.Add(p15);

                    cmd.Transaction = transaction;

                    cmd.ExecuteNonQuery();
                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Commit();
                    }
                    return true;

                }  //try
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Rollback();
                    }
                    return false;
                }
            } // db is opened
            else return true;
        }




        public override DataTable loadDataTable(string DBSelect)
        {
            if (DBIsOpened())
            {
                OdbcDataAdapter myDataAdapter = new OdbcDataAdapter(DBSelect, myDBConn as OdbcConnection);

                DataTable DBDataTable = new DataTable();
                DBDataTable.Locale = System.Globalization.CultureInfo.InvariantCulture;
                myDataAdapter.Fill(DBDataTable);

                myDataAdapter.Dispose();
                return DBDataTable;
            }
            return null;
        }

        public override Int64 countOfRows(string DBSelect, string whileValue)
        {

            if (DBIsOpened())
            {
                OdbcCommand cmd = new OdbcCommand(DBSelect, myDBConn as OdbcConnection);

                OdbcParameter p1 = new OdbcParameter("p1", OdbcType.NChar);
                p1.Value = whileValue;
                cmd.Parameters.Add(p1);
                OdbcDataReader myReader = cmd.ExecuteReader();
                myReader.Read();
                Int64 countRows = myReader.GetInt64(0);
                myReader.Close();
                return countRows;
             }
            return -1;
        }

        public override Int64 countOfRows(string DBSelect, Int32 whileValue)
        {

            if (DBIsOpened())
            {
                OdbcCommand cmd = new OdbcCommand(DBSelect, myDBConn as OdbcConnection);

                OdbcParameter p1 = new OdbcParameter("p1", OdbcType.Int);
                p1.Value = whileValue;
                cmd.Parameters.Add(p1);
                OdbcDataReader myReader = cmd.ExecuteReader();
                myReader.Read();
                Int64 countRows = myReader.GetInt64(0);
                myReader.Close();
                return countRows;
            }
            return -1;
        }


        public override Hashtable getDBLine(string DBSelect, Hashtable DBRow)
        {
            if (DBIsOpened())
            {
                OdbcCommand cmdr0 = new OdbcCommand(DBSelect, myDBConn as OdbcConnection);
                OdbcDataReader myReader = cmdr0.ExecuteReader();

                if (myReader.Read())
                {
                    //                    Int32 countporadi = myReader.GetInt32(0);

                    for (int i = 0; i < myReader.FieldCount; i++)
                    {

                        if (DBRow.ContainsKey(myReader.GetName(i)))
                        {
                            DBRow.Remove(myReader.GetName(i));
                        }
                        DBRow.Add(myReader.GetName(i), myReader.GetValue(i));
                    }

                    myReader.Close();
                    return DBRow;
                }
                else
                {

                    myReader.Close();
                    return null;
                }
            }
            else return null;
        }


        public override Hashtable getNaradiLine(Int32 poradi, Hashtable DBRow)
        {
            if (DBRow == null) DBRow = new Hashtable();



            string DBSelect = "SELECT n.poradi, n.fyzstav as fyzstav, n.ucetstav as ucetstav, z.zustatek as zmeny_zustatek FROM naradi  n, zmeny z " +
                              "WHERE z.poradi = (SELECT MAX(s.poradi) FROM zmeny s WHERE z.parporadi = s.parporadi GROUP BY s.parporadi) " +
                              "AND z.parporadi = n.poradi and z.parporadi = " + poradi.ToString();



            return getDBLine(DBSelect, DBRow);
        }



    }
}
