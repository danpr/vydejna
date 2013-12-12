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

                OdbcCommand cmdPujceno = new OdbcCommand("DROP TABLE pujceno", myDBConn as OdbcConnection);
                try
                {
                    cmdPujceno.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cmdPujceno.Dispose();
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
            string commandStringSequenceInit4 = "INSERT INTO tabseq ( nazev, poradi) VALUES ('pujceno', 1)";


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


            string commandStringPoskozeno = "create table poskozeno ( poradi integer, jmeno char(15), oscislo char(8), dilna char(15)," +
                      "pracoviste char(20), vyrobek char(15),nazev char(60), jk char(15), rozmer char(25)," +
                      "pocetks integer, cena float, datum date, csn char(15), krjmeno char(15)," +
                      "celkcena float, vevcislo char(12), konto char(15) );";



            string commandStringVraceno = "create table vraceno ( poradi integer, jmeno char(15), oscislo char(8), dilna char(15)," +
                      "pracoviste char(20), vyrobek char(15),nazev char(60), jk char(15), rozmer char(25)," +
                      "pocetks integer, cena float, datum date, csn char(15), krjmeno char(15)," +
                      "celkcena float, vevcislo char(12), konto char(15) );";



            string commandStringOsoby = "create table osoby ( jmeno varchar(15), prijmeni varchar(15), ulice varchar(20)," +
                      "mesto varchar(25), psc varchar(7),telhome varchar(15), oscislo varchar(8), odeleni varchar(20)," +
                      "telzam varchar(15), stredisko varchar(10), pujsoub varchar(12), pracoviste varchar(10), cisznamky varchar(5), poznamka varchar(120) );";



            string commandStringZmeny = "create table zmeny ( parporadi integer, pomozjk char(15), datum date, poznamka char(22)," +
                      "prijem integer, vydej integer, zustatek integer, zapkarta char(8), vevcislo char(12)," +
                      "pocivc integer, stav char(1), poradi integer );";


            string commandStringPujceno = "create table pujceno (poradi integer, oscislo varchar(8), nporadi integer, zporadi integer, pjmeno varchar(15)," +
                      "pprijmeni varchar(15), pnazev varchar(60), pjk varchar(15), pdatum date, pks integer, pcena float);";


            openDB();
            if (DBIsOpened())
            {
            OdbcCommand cmdKarta = new OdbcCommand(commandStringKarta, myDBConn as OdbcConnection);
            OdbcCommand cmdNaradi = new OdbcCommand(commandStringNaradi, myDBConn as OdbcConnection);
            OdbcCommand cmdSequence = new OdbcCommand(commandStringSequence, myDBConn as OdbcConnection);
            OdbcCommand cmdSequenceInit1 = new OdbcCommand(commandStringSequenceInit1, myDBConn as OdbcConnection);
            OdbcCommand cmdSequenceInit2 = new OdbcCommand(commandStringSequenceInit2, myDBConn as OdbcConnection);
            OdbcCommand cmdSequenceInit3 = new OdbcCommand(commandStringSequenceInit3, myDBConn as OdbcConnection);
            OdbcCommand cmdSequenceInit4 = new OdbcCommand(commandStringSequenceInit4, myDBConn as OdbcConnection);
            OdbcCommand cmdVraceno = new OdbcCommand(commandStringVraceno, myDBConn as OdbcConnection);
            OdbcCommand cmdPoskozeno = new OdbcCommand(commandStringPoskozeno, myDBConn as OdbcConnection);
            OdbcCommand cmdOsoby = new OdbcCommand(commandStringOsoby, myDBConn as OdbcConnection);
            OdbcCommand cmdZmeny = new OdbcCommand(commandStringZmeny, myDBConn as OdbcConnection);
            OdbcCommand cmdPujceno = new OdbcCommand(commandStringPujceno, myDBConn as OdbcConnection);

            try
                {
                    cmdKarta.ExecuteNonQuery();
                    cmdNaradi.ExecuteNonQuery();
                    cmdSequence.ExecuteNonQuery();
                    cmdSequenceInit1.ExecuteNonQuery();
                    cmdSequenceInit2.ExecuteNonQuery();
                    cmdSequenceInit3.ExecuteNonQuery();
                    cmdSequenceInit4.ExecuteNonQuery();
                    cmdVraceno.ExecuteNonQuery();
                    cmdPoskozeno.ExecuteNonQuery();
                    cmdOsoby.ExecuteNonQuery();
                    cmdZmeny.ExecuteNonQuery();
                    cmdPujceno.ExecuteNonQuery();
                    MessageBox.Show("Tabulky byly vytvořeny.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    myDBConn.Close();
                    cmdPujceno.Dispose();
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
                OdbcCommand cmd1Sequence = new OdbcCommand("UPDATE tabseq SET poradi=1 WHERE nazev = 'naradi'", myDBConn as OdbcConnection);
                OdbcCommand cmd2Sequence = new OdbcCommand("UPDATE tabseq SET poradi=1 WHERE nazev = 'vraceno'", myDBConn as OdbcConnection);
                OdbcCommand cmd3Sequence = new OdbcCommand("UPDATE tabseq SET poradi=1 WHERE nazev = 'poskozeno'", myDBConn as OdbcConnection);
                OdbcCommand cmd4Sequence = new OdbcCommand("UPDATE tabseq SET poradi=1 WHERE nazev = 'pujceno'", myDBConn as OdbcConnection);
                OdbcCommand cmdVraceno = new OdbcCommand("DELETE from vraceno", myDBConn as OdbcConnection);
                OdbcCommand cmdPoskozeno = new OdbcCommand("DELETE from poskozeno", myDBConn as OdbcConnection);
                OdbcCommand cmdOsoby = new OdbcCommand("DELETE from osoby", myDBConn as OdbcConnection);
                OdbcCommand cmdZmeny = new OdbcCommand("DELETE from zmeny", myDBConn as OdbcConnection);
                OdbcCommand cmdPujceno = new OdbcCommand("DELETE from pujceno", myDBConn as OdbcConnection);
                try
                {
                    cmdNaradi.ExecuteNonQuery();
                    cmdKarta.ExecuteNonQuery();
                    cmd1Sequence.ExecuteNonQuery();
                    cmd2Sequence.ExecuteNonQuery();
                    cmd3Sequence.ExecuteNonQuery();
                    cmd4Sequence.ExecuteNonQuery();
                    cmdVraceno.ExecuteNonQuery();
                    cmdPoskozeno.ExecuteNonQuery();
                    cmdOsoby.ExecuteNonQuery();
                    cmdZmeny.ExecuteNonQuery();
                    cmdPujceno.ExecuteNonQuery();
                    MessageBox.Show("Tabulky byly smazány");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cmdPujceno.Dispose();
                    cmdZmeny.Dispose();
                    cmdOsoby.Dispose();
                    cmdPoskozeno.Dispose();
                    cmdVraceno.Dispose();
                    cmd1Sequence.Dispose();
                    cmd2Sequence.Dispose();
                    cmd3Sequence.Dispose();
                    cmd4Sequence.Dispose();
                    cmdKarta.Dispose();
                    cmdNaradi.Dispose();
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


        public override Int32 addLineVraceno(string DBjmeno, string DBcislo, string DBdilna, string DBpracoviste,
                                         string DBvyrobek, string DBnazev, string DBJK, string DBrozmer, int DBpocetks,
                                         double DBcena, DateTime DBdate, string DBnormacsn, string DBkrjmeno,
                                         double DBcelkCena, string DBvevCislo, string DBkonto)
        {
            string commandStringSeq1 = "SELECT poradi FROM tabseq WHERE nazev = 'vraceno'";
            string commandStringSeq2 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'vraceno'";

            string commandString = "INSERT INTO vraceno ( poradi, jmeno, oscislo, dilna, pracoviste, vyrobek, nazev, jk, rozmer, pocetks, cena, datum, csn, krjmeno, celkcena, vevcislo, konto) " +
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
                OdbcParameter p2 = new OdbcParameter("p2", OdbcType.NChar);
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





        public override Int32 addLinePoskozeno(string DBjmeno, string DBcislo, string DBdilna, string DBpracoviste,
                                         string DBvyrobek, string DBnazev, string DBJK, string DBrozmer, int DBpocetks,
                                         double DBcena, DateTime DBdate, string DBnormacsn, string DBkrjmeno,
                                         double DBcelkCena, string DBvevCislo, string DBkonto)
        {

            string commandStringSeq1 = "SELECT poradi FROM tabseq WHERE nazev = 'poskozeno'";
            string commandStringSeq2 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'poskozeno'";

            string commandString = "INSERT INTO poskozeno ( poradi, jmeno, oscislo, dilna, pracoviste, vyrobek, nazev, jk, rozmer, pocetks, cena, datum, csn, krjmeno, celkcena, vevcislo, konto) " +
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
                OdbcParameter p2 = new OdbcParameter("p2", OdbcType.NChar);
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
                                         int DBpocIvc, string DBstav, int DBporadi)
        {

            string commandString = "INSERT INTO zmeny (parporadi, pomozjk, datum, poznamka, prijem, vydej, zustatek, zapkarta, vevcislo, pocivc, stav, poradi )" + 
                  "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";


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
                OdbcParameter p10 = new OdbcParameter("p10", OdbcType.NChar);
                p10.Value = DBstav;
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
                cmd.Parameters.Add(p10);
                cmd.Parameters.Add(p14);
                cmd.ExecuteNonQuery();
            }
        }


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
                    catch { }

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





        public override Int32 addNewLineZmeny(Int32 DBporadi, string DBJK, DateTime DBdatum, Int32 DBprijem, Int32 DBvydej, string DBpoznamka, string DBstav, Int32 DBfyzStavZmena, Int32 DBucetStavZmena, string DBosCislo)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandString1 = "UPDATE naradi set fyzstav = fyzstav + ?, ucetstav = ucetstav + ?  where poradi = ? ";

                string commandString2 = "INSERT INTO zmeny (parporadi, pomozjk, datum, poznamka, prijem, vydej, zustatek, zapkarta, vevcislo, pocivc, stav, poradi )" +
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";


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
                    cmdr.Transaction = transaction;

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
                    pn1.Value = DBfyzStavZmena; // DBprijem - DBvydej;
                    OdbcParameter pn2 = new OdbcParameter("p2", OdbcType.Int);
                    pn2.Value = DBucetStavZmena; // DBprijem - DBvydej;
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
                    p8.Value = DBosCislo;
                    OdbcParameter p9 = new OdbcParameter("p9", OdbcType.NChar);
                    p9.Value = "";
                    OdbcParameter p10 = new OdbcParameter("p10", OdbcType.Int);
                    p10.Value = 0;
                    OdbcParameter p11 = new OdbcParameter("p11", OdbcType.NChar);
                    p11.Value = DBstav;
                    OdbcParameter p12 = new OdbcParameter("p12", OdbcType.Int);
                    p12.Value = poradi;

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
                    cmd2.Parameters.Add(p12);


                    cmd2.Transaction = transaction;
                    cmd2.ExecuteNonQuery();

                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Commit();
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




        public override Int32 addNewLineZmenyAndVraceno(Int32 DBporadi, Int32 DBparPoradi, Int32 DBzmenPoradi, DateTime DBdatum, Int32 DBks, string DBpoznamka, string DBosCislo, string DBJK, string DBievCislo)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandReadString1 = "SELECT zustatek FROM zmeny WHERE poradi = ? ";
                string commandReadString2 = "SELECT pks FROM pujceno WHERE poradi = ? ";
                string commandReadString3 = "SELECT poradi, zustatek from zmeny where parporadi = ? ORDER BY poradi DESC";

                string commandString1 = "UPDATE naradi SET fyzstav = fyzstav + ? WHERE poradi = ? ";

                string commandString2 = "INSERT INTO zmeny (parporadi, pomozjk, datum, poznamka, prijem, vydej, zustatek, zapkarta, vevcislo, pocivc, stav, poradi )" +
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";

                string commandString3 = "UPDATE pujceno SET pks = pks - ? WHERE poradi = ? ";
                string commandString4 = "DELETE FROM pujceno  WHERE poradi = ? ";

                
                try
                {
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }

                    // zjistime stav vypujcene
                    Int32 pujcKs = 0;
                    OdbcCommand cmdr1 = new OdbcCommand(commandReadString1, myDBConn as OdbcConnection);
                    cmdr1.Transaction = transaction;
                    OdbcParameter px = new OdbcParameter("px", DbType.Int32);
                    px.Value = DBzmenPoradi;
                    cmdr1.Parameters.Add(px);
                    OdbcDataReader zmenReader = cmdr1.ExecuteReader();
                    if (zmenReader.Read())
                    {
                        pujcKs = zmenReader.GetInt32(0); 
                    }
                    else
                    {
                        OdbcCommand cmdr2 = new OdbcCommand(commandReadString2, myDBConn as OdbcConnection);
                        cmdr2.Transaction = transaction;
                        OdbcParameter pz = new OdbcParameter("pz", DbType.Int32);
                        pz.Value = DBporadi;
                        cmdr2.Parameters.Add(pz);
                        OdbcDataReader pujcReader = cmdr2.ExecuteReader();
                        if (pujcReader.Read())
                        {
                            pujcKs = pujcReader.GetInt32(0);
                        }
                        else
                        {
                            if (transaction != null)
                            {
                                (transaction as OdbcTransaction).Rollback();
                            }
                            return -1;  // chyba
                        }
                    }
                    if (pujcKs < DBks)
                    {
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -2;  // pozadavek na odepsani vice kusu nez je mozno
                    }



                    Int32 poradi;
                    Int32 zustatek;

                    OdbcCommand cmdr3 = new OdbcCommand(commandReadString3, myDBConn as OdbcConnection);
                    OdbcParameter pm = new OdbcParameter("pm", DbType.Int32);
                    pm.Value = DBparPoradi;
                    cmdr3.Parameters.Add(pm);
                    OdbcDataReader seqReader1 = cmdr3.ExecuteReader();
                    if (seqReader1.Read() == true)
                    {
                        poradi = seqReader1.GetInt32(0) + 1;
                        zustatek = seqReader1.GetInt32(1); //zmeny.stav - posledni
                    }
                    else
                    {
                        poradi = 1;
                        zustatek = 0;
                        seqReader1.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -3;  // nebylo nic pujceno
                    }
                    seqReader1.Close();




                    // tab naradi

                    OdbcCommand cmd1 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);
                    OdbcParameter pn1 = new OdbcParameter("pn1", OdbcType.Int);
                    pn1.Value = DBks; 
                    OdbcParameter pn2 = new OdbcParameter("pn2", OdbcType.Int);
                    pn2.Value = DBparPoradi;
                    cmd1.Parameters.Add(pn1);
                    cmd1.Parameters.Add(pn2);
                    cmd1.Transaction = transaction;
                    cmd1.ExecuteNonQuery();

                    //  tab zmeny
                    OdbcCommand cmd2 = new OdbcCommand(commandString2, myDBConn as OdbcConnection);
                    OdbcParameter p1 = new OdbcParameter("p1", OdbcType.Int);
                    p1.Value = DBparPoradi;
                    OdbcParameter p2 = new OdbcParameter("p2", OdbcType.NChar);
                    p2.Value = DBJK;
                    OdbcParameter p3 = new OdbcParameter("p3", OdbcType.Date);
                    p3.Value = DBdatum;
                    OdbcParameter p4 = new OdbcParameter("p4", OdbcType.NChar);
                    p4.Value = DBpoznamka;
                    OdbcParameter p5 = new OdbcParameter("p5", OdbcType.Int);
                    p5.Value = 0;
                    OdbcParameter p6 = new OdbcParameter("p6", OdbcType.Int);
                    p6.Value = DBks;
                    OdbcParameter p7 = new OdbcParameter("p7", OdbcType.Int);
                    p7.Value = zustatek - DBks;
                    OdbcParameter p8 = new OdbcParameter("p8", OdbcType.NChar);
                    p8.Value = DBosCislo;
                    OdbcParameter p9 = new OdbcParameter("p9", OdbcType.NChar);
                    p9.Value = DBievCislo;
                    OdbcParameter p10 = new OdbcParameter("p10", OdbcType.Int);
                    p10.Value = 0;
                    OdbcParameter p11 = new OdbcParameter("p11", OdbcType.NChar);
                    p11.Value = "R";
                    OdbcParameter p12 = new OdbcParameter("p12", OdbcType.Int);
                    p12.Value = poradi;
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
                    cmd2.Parameters.Add(p12);
                    cmd2.Transaction = transaction;
                    cmd2.ExecuteNonQuery();


                    OdbcCommand cmd3 = new OdbcCommand(commandString3, myDBConn as OdbcConnection);
                    OdbcParameter pna1 = new OdbcParameter("pna1", OdbcType.Int);
                    pn1.Value = DBks;
                    OdbcParameter pna2 = new OdbcParameter("pna2", OdbcType.Int);
                    pn2.Value = DBparPoradi;
                    cmd1.Parameters.Add(pna1);
                    cmd1.Parameters.Add(pna2);
                    cmd1.Transaction = transaction;
                    cmd1.ExecuteNonQuery();

                    OdbcCommand cmd4 = new OdbcCommand(commandString4, myDBConn as OdbcConnection);
                    OdbcParameter pnd1 = new OdbcParameter("pnd1", OdbcType.Int);
                    pnd1.Value = DBks;
                    cmd1.Parameters.Add(pnd1);
                    cmd1.Transaction = transaction;
                    cmd1.ExecuteNonQuery();

                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Commit();
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


                return 0;
            }
            return -1;
        }



        public override Int32 addNewLineZmenyAndPujceno(Int32 DBparPoradi, string DBJK, DateTime DBdatum, Int32 DBks, string DBpoznamka,
                                                        string DBosCislo, string DBjmeno, string DBprijmeni, string DBnazev, double DBcena)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandReadString1 = "SELECT poradi, zustatek from zmeny where parporadi = ? ORDER BY poradi DESC";
                string commandReadString2 = "SELECT fyzstav from naradi where poradi = ? ";
                string commandReadString3 = "SELECT poradi FROM tabseq WHERE nazev = 'pujceno'";


                string commandString1 = "UPDATE naradi set fyzstav = fyzstav - ?  where poradi = ? ";
                string commandString2 = "INSERT INTO zmeny (parporadi, pomozjk, datum, poznamka, prijem, vydej, zustatek, zapkarta, vevcislo, pocivc, stav, poradi )" +
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";

                string commandString5 = "INSERT INTO pujceno ( poradi, oscislo, nporadi, zporadi, pjmeno, pprijmeni, pnazev, pjk, pdatum, pks, pcena )" +
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";

                string commandString6 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'pujceno'";

                Int32 pujcPoradi = 0;
                try
                {
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }

                    OdbcCommand cmdr = new OdbcCommand(commandReadString1, myDBConn as OdbcConnection);
                    OdbcParameter px = new OdbcParameter("px", DbType.Int32);
                    px.Value = DBparPoradi;
                    cmdr.Parameters.Add(px);

                    OdbcCommand cmdr2 = new OdbcCommand(commandReadString2, myDBConn as OdbcConnection);
                    OdbcParameter prm = new OdbcParameter("prm", DbType.Int32);
                    prm.Value = DBparPoradi;
                    cmdr2.Parameters.Add(prm);
                    OdbcDataReader seqReader2 = cmdr2.ExecuteReader();
                    int fyzstav = 0;

                    if (seqReader2.Read() == true)
                    {
                        fyzstav = seqReader2.GetInt32(0); // naradi.fyzstav
                    }
                    else
                    {
                        // material neexistuje zrusime transakci a navratime chybu
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -1;
                    }

                    if (fyzstav < DBks)
                    // pozadavek na odpis vice ks nez je existujici stav na vydejne
                    {
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -2;
                    }


                    Int32 poradi;
                    Int32 zustatek;

                    OdbcDataReader seqReader1 = cmdr.ExecuteReader();
                    // true osCisloExist
                    if (seqReader1.Read() == true)
                    {
                        poradi = seqReader1.GetInt32(0) + 1;
                        zustatek = seqReader1.GetInt32(1); //zmeny.stav - posledni
                    }
                    else
                    {
                        poradi = 1;
                        zustatek = fyzstav;
                    }
                    seqReader1.Close();


                    OdbcCommand cmdSeq2 = new OdbcCommand(commandReadString3, myDBConn as OdbcConnection);
                    OdbcDataReader seqReader3 = cmdSeq2.ExecuteReader();
                    seqReader3.Read();
                    pujcPoradi = seqReader3.GetInt32(0);
                    seqReader3.Close();

                    // tab naradi

                    OdbcCommand cmd1 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);
                    OdbcParameter pn1 = new OdbcParameter("pn1", OdbcType.Int);
                    pn1.Value = DBks; // DBprijem - DBvydej;
                    OdbcParameter pn2 = new OdbcParameter("pn2", OdbcType.Int);
                    pn2.Value = DBparPoradi;
                    cmd1.Parameters.Add(pn1);
                    cmd1.Parameters.Add(pn2);
                    cmd1.Transaction = transaction;
                    cmd1.ExecuteNonQuery();

                    //  tab zmeny
                    OdbcCommand cmd2 = new OdbcCommand(commandString2, myDBConn as OdbcConnection);
                    OdbcParameter p1 = new OdbcParameter("p1", OdbcType.Int);
                    p1.Value = DBparPoradi;
                    OdbcParameter p2 = new OdbcParameter("p2", OdbcType.NChar);
                    p2.Value = DBJK;
                    OdbcParameter p3 = new OdbcParameter("p3", OdbcType.Date);
                    p3.Value = DBdatum;
                    OdbcParameter p4 = new OdbcParameter("p4", OdbcType.NChar);
                    p4.Value = DBpoznamka;
                    OdbcParameter p5 = new OdbcParameter("p5", OdbcType.Int);
                    p5.Value = 0;
                    OdbcParameter p6 = new OdbcParameter("p6", OdbcType.Int);
                    p6.Value = DBks;
                    OdbcParameter p7 = new OdbcParameter("p7", OdbcType.Int);
                    p7.Value = zustatek - DBks;
                    OdbcParameter p8 = new OdbcParameter("p8", OdbcType.NChar);
                    p8.Value = DBosCislo;
                    OdbcParameter p9 = new OdbcParameter("p9", OdbcType.NChar);
                    p9.Value = "";
                    OdbcParameter p10 = new OdbcParameter("p10", OdbcType.Int);
                    p10.Value = 0;
                    OdbcParameter p11 = new OdbcParameter("p11", OdbcType.NChar);
                    p11.Value = "U";
                    OdbcParameter p12 = new OdbcParameter("p12", OdbcType.Int);
                    p12.Value = poradi;
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
                    cmd2.Parameters.Add(p12);
                    cmd2.Transaction = transaction;
                    cmd2.ExecuteNonQuery();


                    OdbcCommand cmd = new OdbcCommand(commandString5, myDBConn as OdbcConnection);

                    OdbcParameter pp0 = new OdbcParameter("pp0", OdbcType.Int);
                    pp0.Value = pujcPoradi;
                    OdbcParameter pp1 = new OdbcParameter("pp1", OdbcType.NChar);
                    pp1.Value = DBosCislo;
                    OdbcParameter pp2 = new OdbcParameter("pp2", OdbcType.Int);
                    pp2.Value = DBparPoradi;
                    OdbcParameter pp3 = new OdbcParameter("pp3", OdbcType.Int);
                    pp3.Value = poradi; // DBzmPoradi;
                    OdbcParameter pp4 = new OdbcParameter("pp4", OdbcType.NChar);
                    pp4.Value = DBjmeno;
                    OdbcParameter pp5 = new OdbcParameter("pp5", OdbcType.NChar);
                    pp5.Value = DBprijmeni;
                    OdbcParameter pp6 = new OdbcParameter("pp6", OdbcType.NChar);
                    pp6.Value = DBnazev;
                    OdbcParameter pp7 = new OdbcParameter("pp7", OdbcType.NChar);
                    pp7.Value = DBJK;
                    OdbcParameter pp8 = new OdbcParameter("pp8", OdbcType.Date);
                    pp8.Value = DBdatum;
                    OdbcParameter pp9 = new OdbcParameter("pp9", OdbcType.Int);
                    pp9.Value = DBks;
                    OdbcParameter pp10 = new OdbcParameter("pp10", OdbcType.Double);
                    pp10.Value = DBcena;

                    cmd.Parameters.Add(pp0);
                    cmd.Parameters.Add(pp1);
                    cmd.Parameters.Add(pp2);
                    cmd.Parameters.Add(pp3);
                    cmd.Parameters.Add(pp4);
                    cmd.Parameters.Add(pp5);
                    cmd.Parameters.Add(pp6);
                    cmd.Parameters.Add(pp7);
                    cmd.Parameters.Add(pp8);
                    cmd.Parameters.Add(pp9);
                    cmd.Parameters.Add(pp10);
                    cmd.ExecuteNonQuery();

                    OdbcCommand cmdSeq3 = new OdbcCommand(commandString6, myDBConn as OdbcConnection);
                    cmdSeq3.ExecuteNonQuery();

                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Commit();
                    }

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
                return 0;
            }
            return -1;
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
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    } catch { }

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
                    OdbcParameter p30 = new OdbcParameter("p30", OdbcType.Int);
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


        public override Boolean moveNaraddiToNewKaret(Int32 DBporadi)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandString1 = "INSERT INTO karta ( poradi, nazev, jk, normacsn, normadin, vyrobce, cena, poznamka, minimum, celkcena, ucetstav, fyzstav, rozmer, analucet, tdate, stredisko, kodzmeny, druh, odpis, zavod ) " +
                      "SELECT poradi, nazev, jk, normacsn, normadin, vyrobce, cena, poznamka, minimum, celkcena, ucetstav, fyzstav, rozmer, analucet, tdate, stredisko, kodzmeny, druh, odpis, zavod FROM naradi WHERE poradi = ?";

                string commandString2 = "DELETE FROM naradi  where poradi = ? ";


                try
                {
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }

                    // prekopirujeme
                    OdbcCommand cmd1 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);
                    OdbcParameter p1 = new OdbcParameter("p1", OdbcType.Int);
                    p1.Value = DBporadi;
                    cmd1.Parameters.Add(p1);

                    cmd1.Transaction = transaction;
                    cmd1.ExecuteNonQuery();

                    //zrusime
                    OdbcCommand cmd2 = new OdbcCommand(commandString2, myDBConn as OdbcConnection);
                    OdbcParameter p2 = new OdbcParameter("p2", OdbcType.Int);
                    p2.Value = DBporadi;
                    cmd2.Parameters.Add(p2);

                    cmd2.Transaction = transaction;
                    cmd2.ExecuteNonQuery();


                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Commit();
                    }


                }
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Rollback();
                    }
                    return false;  // chyba
                }
                return true;  // ok
            }
            return false;  // database neni otevrena
        }




        public override Int32 addNewLineZmenyAndPoskozeno(Int32 DBporadi, string DBJK, DateTime DBdatum, Int32 DBvydej, string DBpoznamka,
                                                          string osCislo, string DBjmeno, string DBprijmeni, string DBstredisko, string DBprovoz,
                                                          string DBnazev, string DBrozmer, string DBkonto, double DBcena, double DBcelkCena, string DBcsn, string DBcisZak)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandStringRead1 = "SELECT poradi, zustatek from zmeny where parporadi = ? ORDER BY poradi DESC";
                string commandStringRead2 = "SELECT fyzstav, ucetstav FROM naradi where poradi = ? ";
                string commandStringRead3 = "SELECT poradi FROM tabseq WHERE nazev = 'poskozeno'";

                string commandString1 = "UPDATE naradi set fyzstav = fyzstav - ?, ucetstav = ucetstav - ?  where poradi = ? ";
                string commandString2 = "INSERT INTO zmeny (parporadi, pomozjk, datum, poznamka, prijem, vydej, zustatek, zapkarta, vevcislo, pocivc, stav, poradi )" +
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";
                string commandString4 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'poskozeno'";
                string commandString5 = "INSERT INTO poskozeno ( poradi, jmeno, oscislo, dilna, pracoviste, vyrobek, nazev, jk, rozmer, pocetks, cena, datum, csn, krjmeno, celkcena, vevcislo, konto) " +
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

                    OdbcCommand cmdr2 = new OdbcCommand(commandStringRead2, myDBConn as OdbcConnection);
                    OdbcParameter px2 = new OdbcParameter("px2", DbType.Int32);
                    px2.Value = DBporadi;
                    cmdr2.Parameters.Add(px2);
                    cmdr2.Transaction = transaction;
                    OdbcDataReader myReader2 = cmdr2.ExecuteReader();
                    // true fyzstav exist -- zaznam mohl bzt mezitim smazan
                    if (myReader2.Read() == true)
                    {
                        Int32 fyzstav = myReader2.GetInt32(myReader2.GetOrdinal("fyzstav"));
                        Int32 ucetstav = myReader2.GetInt32(myReader2.GetOrdinal("ucetstav"));
                        myReader2.Close();

                        if ((fyzstav < DBvydej) || (ucetstav < DBvydej))
                        {
                            if (transaction != null)
                            {
                                (transaction as OdbcTransaction).Rollback();
                            }
                            return -2;
                        }

                        // poradi pro zmeny
                        OdbcCommand cmdr1 = new OdbcCommand(commandStringRead1, myDBConn as OdbcConnection);
                        OdbcParameter px = new OdbcParameter("px", DbType.Int32);
                        px.Value = DBporadi;
                        cmdr1.Parameters.Add(px);

                        Int32 poradi;
                        Int32 zustatek;

                        cmdr1.Transaction = transaction;
                        OdbcDataReader myReader = cmdr1.ExecuteReader();
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
                        OdbcCommand cmdSeq1 = new OdbcCommand(commandStringRead3, myDBConn as OdbcConnection);
                        OdbcDataReader seqReader = cmdSeq1.ExecuteReader();
                        seqReader.Read();
                        Int32 poradiPoskozeno = seqReader.GetInt32(0);
                        seqReader.Close();



                        OdbcCommand cmd1 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);

                        OdbcParameter pn1 = new OdbcParameter("p1", OdbcType.Int);
                        pn1.Value = DBvydej;
                        OdbcParameter pn2 = new OdbcParameter("p2", OdbcType.Int);
                        pn2.Value = DBvydej;
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
                        p7.Value = zustatek - DBvydej;
                        OdbcParameter p8 = new OdbcParameter("p8", OdbcType.NChar);
                        p8.Value = osCislo;
                        OdbcParameter p9 = new OdbcParameter("p9", OdbcType.NChar);
                        p9.Value = "";
                        OdbcParameter p10 = new OdbcParameter("p10", OdbcType.Int);
                        p10.Value = 0;
                        OdbcParameter p11 = new OdbcParameter("p11", OdbcType.NChar);
                        p11.Value = "O";
                        OdbcParameter p12 = new OdbcParameter("p12", OdbcType.Int);
                        p12.Value = poradi;

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
                        cmd2.Parameters.Add(p12);

                        cmd2.Transaction = transaction;
                        cmd2.ExecuteNonQuery();

                        // pridani radku do tabulky zruseneho materialu
                        OdbcCommand cmd3 = new OdbcCommand(commandString5, myDBConn as OdbcConnection);

                        OdbcParameter pz0 = new OdbcParameter("pz0", OdbcType.Int);
                        pz0.Value = poradiPoskozeno;
                        OdbcParameter pz1 = new OdbcParameter("pz1", OdbcType.NChar); // prijmeni
                        pz1.Value = DBprijmeni;
                        OdbcParameter pz2 = new OdbcParameter("pz2", OdbcType.Int);
                        pz2.Value = osCislo;
                        OdbcParameter pz3 = new OdbcParameter("pz3", OdbcType.NChar);
                        pz3.Value = DBstredisko;
                        OdbcParameter pz4 = new OdbcParameter("pz4", OdbcType.NChar);
                        pz4.Value = DBprovoz;
                        OdbcParameter pz5 = new OdbcParameter("pz5", OdbcType.NChar);
                        pz5.Value = DBcisZak;
                        OdbcParameter pz6 = new OdbcParameter("pz6", OdbcType.NChar);
                        pz6.Value = DBnazev;
                        OdbcParameter pz7 = new OdbcParameter("pz7", OdbcType.NChar);
                        pz7.Value = DBJK;
                        OdbcParameter pz8 = new OdbcParameter("pz8", OdbcType.NChar);
                        pz8.Value = DBrozmer;
                        OdbcParameter pz9 = new OdbcParameter("pz9", OdbcType.Int);
                        pz9.Value = DBvydej;
                        OdbcParameter pz10 = new OdbcParameter("pz10", OdbcType.Double);
                        pz10.Value = DBcena;
                        OdbcParameter pz11 = new OdbcParameter("pz11", OdbcType.Date);
                        pz11.Value = DBdatum;
                        OdbcParameter pz12 = new OdbcParameter("pz12", OdbcType.NChar);
                        pz12.Value = DBcsn;
                        OdbcParameter pz13 = new OdbcParameter("pz13", OdbcType.NChar);
                        pz13.Value = DBjmeno;
                        OdbcParameter pz14 = new OdbcParameter("pz14", OdbcType.Double);
                        pz14.Value = DBcelkCena;
                        OdbcParameter pz15 = new OdbcParameter("pz15", OdbcType.NChar);
                        pz15.Value = ""; // DBvevCislo;
                        OdbcParameter pz16 = new OdbcParameter("pz16", OdbcType.NChar);
                        pz16.Value = DBkonto;

                        cmd3.Parameters.Add(pz0);
                        cmd3.Parameters.Add(pz1);
                        cmd3.Parameters.Add(pz2);
                        cmd3.Parameters.Add(pz3);
                        cmd3.Parameters.Add(pz4);
                        cmd3.Parameters.Add(pz5);
                        cmd3.Parameters.Add(pz6);
                        cmd3.Parameters.Add(pz7);
                        cmd3.Parameters.Add(pz8);
                        cmd3.Parameters.Add(pz9);
                        cmd3.Parameters.Add(pz10);
                        cmd3.Parameters.Add(pz11);
                        cmd3.Parameters.Add(pz12);
                        cmd3.Parameters.Add(pz13);
                        cmd3.Parameters.Add(pz14);
                        cmd3.Parameters.Add(pz15);
                        cmd3.Parameters.Add(pz16);
                        cmd3.Transaction = transaction;
                        cmd3.ExecuteNonQuery();

                        OdbcCommand cmd4 = new OdbcCommand(commandString4, myDBConn as OdbcConnection);
                        cmd4.Transaction = transaction;
                        cmd4.ExecuteNonQuery();


                    }
                    else
                    {
                        myReader2.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -1;
                    }
                 if (transaction != null)
                 {
                     (transaction as OdbcTransaction).Commit();
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





        public override Boolean editNewLinePoskozene(Int32 poradi, string DBkrjmeno, string DBjmeno, string DBosCislo, string DBdilna,
                                 string DBprovoz, string DBnazev, string DBJK, long DBpocetKS,
                                 string DBrozmer, string DBCSN, decimal DBcena,
                                 DateTime DBdatum, string DBvyrobek, string DBkonto)
        {

            string commandString1 = "UPDATE poskozeno set jmeno = ?, oscislo =?, dilna = ?, pracoviste = ?, vyrobek = ?, nazev = ?, rozmer = ?, pocetks = ?, cena = ?, datum = ?, csn = ?, krjmeno = ?, konto = ?, jk = ? " +
                  "where  poradi = ?";

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

            string commandString1 = "UPDATE vraceno set jmeno = ?, oscislo =?, dilna = ?, pracoviste = ?, vyrobek = ?, nazev = ?, rozmer = ?, pocetks = ?, cena = ?, datum = ?, csn = ?, krjmeno = ?, konto = ?, jk = ? " +
                  "where  poradi = ?";

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
            string commandString2 = "UPDATE karta set nazev = ?, jk = ?, normacsn = ?, normadin = ?, vyrobce = ?, cena = ?, poznamka = ?, minimum = ?, celkcena = ?,  ucetstav = ?, fyzstav = ?, rozmer = ?, analucet = ?" +
                            "where  poradi = ?";

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

        public override Boolean deleteLineKaret(Int32 poradi)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandString1 = "DELETE FROM karta WHERE poradi = ? ";

                try
                {
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }

                    //zrusime
                    OdbcCommand cmd1 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);
                    OdbcParameter p1 = new OdbcParameter("p1", OdbcType.Int);
                    p1.Value = poradi;
                    cmd1.Parameters.Add(p1);

                    cmd1.Transaction = transaction;
                    cmd1.ExecuteNonQuery();

                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Commit();
                    }

                }
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Rollback();
                    }
                    return false;  // chyba
                }
                return true;  // ok
            }
            return false;  // database neni otevrena

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
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch { }

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


        public override void addLinePujceno(int DBparPoradi, string DBosCislo, DateTime DBdatum, int DBks,
                                         string DBjmeno, string DBPrijmeni, string DBnazev, string DBjk,
                                         double DBcena, int DBzmPoradi)
        {
            string commandString = "INSERT INTO pujceno ( poradi, oscislo, nporadi, zporadi, pjmeno, pprijmeni, pnazev, pjk, pdatum, pks, pcena )" +
                  "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";

            string commandStringSeq1 = "SELECT poradi FROM tabseq WHERE nazev = 'pujceno'";
            string commandStringSeq2 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'pujceno'";
            //            string commandStringSeq3 = "SELECT poradi FROM zmeny WHERE parporadi = ? AND zapkarta = ? AND stav = 'U' AND datum = ? ";
            string commandStringSeq3 = "SELECT poradi FROM zmeny WHERE parporadi = ? AND stav = 'U' AND zapkarta = ?";


            if (DBIsOpened())
            {

                OdbcCommand cmdSeq1 = new OdbcCommand(commandStringSeq1, myDBConn as OdbcConnection);
                OdbcDataReader seqReader1 = cmdSeq1.ExecuteReader();
                seqReader1.Read();
                Int32 pujcPoradi = seqReader1.GetInt32(0);
                seqReader1.Close();


                OdbcCommand cmdSeq3 = new OdbcCommand(commandStringSeq3, myDBConn as OdbcConnection);
                OdbcParameter pz0 = new OdbcParameter("pz0", OdbcType.Int);
                pz0.Value = DBparPoradi;
                OdbcParameter pz1 = new OdbcParameter("pz1", OdbcType.NChar);
                pz1.Value = DBosCislo;
                OdbcParameter pz2 = new OdbcParameter("pz2", OdbcType.NChar);
                pz2.Value = DBdatum;
                cmdSeq3.Parameters.Add(pz0);
                cmdSeq3.Parameters.Add(pz1);
                //    cmdSeq3.Parameters.Add(pz2);
                OdbcDataReader seqReader2 = cmdSeq3.ExecuteReader();

                Int32 zmenyPoradi;
                if (seqReader2.Read() == true)
                {
                    zmenyPoradi = seqReader2.GetInt32(0);
                    seqReader2.Close();
                }
                else
                {
                    zmenyPoradi = 0;
                    MessageBox.Show("Neexistuje zmena stavu pro vypujcení "+ DBparPoradi.ToString() + " : "+ DBosCislo);
                }

                OdbcCommand cmd = new OdbcCommand(commandString, myDBConn as OdbcConnection);

                OdbcParameter p0 = new OdbcParameter("p0", OdbcType.Int);
                p0.Value = pujcPoradi;
                OdbcParameter p1 = new OdbcParameter("p1", OdbcType.NChar);
                p1.Value = DBosCislo;
                OdbcParameter p2 = new OdbcParameter("p2", OdbcType.Int);
                p2.Value = DBparPoradi;
                OdbcParameter p3 = new OdbcParameter("p3", OdbcType.Int);
                p3.Value = zmenyPoradi; // DBzmPoradi;
                OdbcParameter p4 = new OdbcParameter("p4", OdbcType.NChar);
                p4.Value = DBjmeno;
                OdbcParameter p5 = new OdbcParameter("p5", OdbcType.NChar);
                p5.Value = DBPrijmeni;
                OdbcParameter p6 = new OdbcParameter("p6", OdbcType.NChar);
                p6.Value = DBnazev;
                OdbcParameter p7 = new OdbcParameter("p7", OdbcType.NChar);
                p7.Value = DBjk;
                OdbcParameter p8 = new OdbcParameter("p8", OdbcType.Date);
                p8.Value = DBdatum ;
                OdbcParameter p9 = new OdbcParameter("p9", OdbcType.Int);
                p9.Value = DBks;
                OdbcParameter p10 = new OdbcParameter("p10", OdbcType.Double);
                p10.Value = DBks;

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
                cmd.ExecuteNonQuery();

                OdbcCommand cmdSeq2 = new OdbcCommand(commandStringSeq2, myDBConn as OdbcConnection);
                cmdSeq2.ExecuteNonQuery();
            }
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


        public override Hashtable getNaradiZmenyLine(Int32 poradi, Hashtable DBRow)
        {
            if (DBRow == null) DBRow = new Hashtable();


            string DBSelect = "SELECT n.poradi, n.fyzstav as fyzstav, n.ucetstav as ucetstav, z.zustatek as zmeny_zustatek FROM naradi  n, zmeny z " +
                              "WHERE z.poradi = (SELECT MAX(s.poradi) FROM zmeny s WHERE z.parporadi = s.parporadi GROUP BY s.parporadi) " +
                              "AND z.parporadi = n.poradi and z.parporadi = " + poradi.ToString();

            return getDBLine(DBSelect, DBRow);
        }



    }
}
