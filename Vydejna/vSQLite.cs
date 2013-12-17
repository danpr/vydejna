using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Windows.Forms;



namespace Vydejna
{
    class vSQLite : vDatabase
    {


        public vSQLite(string dataBaseName, string serverAddress, string serverName, string port, string locale, string driver, string userName, string password)
            : base(dataBaseName, serverAddress, serverName, port, locale, driver, userName, password)
        {
            dBConnectStr = String.Format("Data Source={0,1};Version=3;New=False;Compress=True;", dataBaseName);
        }

        public override void openDB()
        {

            try
            {
                myDBConn = new SQLiteConnection(dBConnectStr);
                myDBConn.Open();
                dBConnectionState = true;
            }
            catch
            {
                dBConnectionState = false;
                MessageBox.Show("Database nebyla pripojena");
            }

        }



        public override void DropTables()
        {
            openDB();
            if (DBIsOpened())
            {
                SQLiteCommand cmdKarta = new SQLiteCommand("DROP TABLE karta", myDBConn as SQLiteConnection);
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

                SQLiteCommand cmdNaradi = new SQLiteCommand("DROP TABLE naradi", myDBConn as SQLiteConnection);
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

                SQLiteCommand cmdSequence = new SQLiteCommand("DROP TABLE tabseq", myDBConn as SQLiteConnection);
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
                
                
                
                SQLiteCommand cmdVraceno = new SQLiteCommand("DROP TABLE vraceno", myDBConn as SQLiteConnection);
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

                SQLiteCommand cmdPoskozeno = new SQLiteCommand("DROP TABLE poskozeno", myDBConn as SQLiteConnection);
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


                SQLiteCommand cmdOsoby = new SQLiteCommand("DROP TABLE osoby", myDBConn as SQLiteConnection);
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

                SQLiteCommand cmdZmeny = new SQLiteCommand("DROP TABLE zmeny", myDBConn as SQLiteConnection);
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

                SQLiteCommand cmdPujceno = new SQLiteCommand("DROP TABLE pujceno", myDBConn as SQLiteConnection);
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



        public override void DeleteTables()
        {
            openDB();
            if (DBIsOpened())
            {
                SQLiteCommand cmdNaradi = new SQLiteCommand("DELETE from naradi", myDBConn as SQLiteConnection);
                SQLiteCommand cmdKarta = new SQLiteCommand("DELETE from karta", myDBConn as SQLiteConnection);
                SQLiteCommand cmdSequence1 = new SQLiteCommand("UPDATE tabseq SET poradi=1 WHERE nazev = 'naradi'", myDBConn as SQLiteConnection);
                SQLiteCommand cmdSequence2 = new SQLiteCommand("UPDATE tabseq SET poradi=1 WHERE nazev = 'vraceno'", myDBConn as SQLiteConnection);
                SQLiteCommand cmdSequence3 = new SQLiteCommand("UPDATE tabseq SET poradi=1 WHERE nazev = 'poskozeno'", myDBConn as SQLiteConnection);
                SQLiteCommand cmdSequence4 = new SQLiteCommand("UPDATE tabseq SET poradi=1 WHERE nazev = 'pujceno'", myDBConn as SQLiteConnection);
                SQLiteCommand cmdVraceno = new SQLiteCommand("DELETE from vraceno", myDBConn as SQLiteConnection);
                SQLiteCommand cmdPoskozeno = new SQLiteCommand("DELETE from poskozeno", myDBConn as SQLiteConnection);
                SQLiteCommand cmdOsoby = new SQLiteCommand("DELETE from osoby", myDBConn as SQLiteConnection);
                SQLiteCommand cmdZmeny = new SQLiteCommand("DELETE from zmeny", myDBConn as SQLiteConnection);
                SQLiteCommand cmdPujceno = new SQLiteCommand("DELETE from pujceno", myDBConn as SQLiteConnection);
                //                SQLiteCommand cmdVacuum = new OdbcCommand("VACUUM", myDBConn as SQLiteConnection);
                try
                {
                    cmdNaradi.ExecuteNonQuery();
                    cmdKarta.ExecuteNonQuery();
                    cmdSequence1.ExecuteNonQuery();
                    cmdSequence2.ExecuteNonQuery();
                    cmdSequence3.ExecuteNonQuery();
                    cmdSequence4.ExecuteNonQuery();
                    cmdVraceno.ExecuteNonQuery();
                    cmdPoskozeno.ExecuteNonQuery();
                    cmdOsoby.ExecuteNonQuery();
                    cmdZmeny.ExecuteNonQuery();
                    cmdPujceno.ExecuteNonQuery();
                    //    cmdVacuum.ExecuteNonQuery();
                    MessageBox.Show("Tabulky byly smazány");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    //  cmdVacuum.Dispose();
                    cmdPujceno.Dispose();
                    cmdZmeny.Dispose();
                    cmdOsoby.Dispose();
                    cmdPoskozeno.Dispose();
                    cmdVraceno.Dispose();
                    cmdSequence1.Dispose();
                    cmdSequence2.Dispose();
                    cmdSequence3.Dispose();
                    cmdSequence4.Dispose();
                    cmdKarta.Dispose();
                    cmdNaradi.Dispose();
                }
            }

            // vymaze tabulky

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

            string commandStringOsoby = "create table osoby ( jmeno char(15), prijmeni char(15), ulice char(20)," +
                      "mesto char(25), psc char(7),telhome char(15), oscislo char(8), odeleni char(20)," +
                      "telzam char(15), stredisko char(10), pujsoub char(12), pracoviste char(10), cisznamky char(5), poznamka char(120) );";


            string commandStringZmeny = "create table zmeny ( parporadi integer, pomozjk varchar(15), datum date, poznamka varchar(22)," +
                      "prijem integer, vydej integer, zustatek integer, zapkarta varchar(8), vevcislo varchar(12)," +
                      "pocivc integer, stav varchar(1), poradi integer );";

            string commandStringPujceno = "create table pujceno (poradi integer, oscislo varchar(8), nporadi integer, zporadi integer, stavks integer, pjmeno varchar(15)," +
                      "pprijmeni varchar(15), pnazev varchar(60), pjk varchar(15), pdatum date, pks integer, pcena float);";


            openDB();
            if (DBIsOpened())
            {
                SQLiteCommand cmdKarta = new SQLiteCommand(commandStringKarta, myDBConn as SQLiteConnection);
                SQLiteCommand cmdNaradi = new SQLiteCommand(commandStringNaradi, myDBConn as SQLiteConnection);
                SQLiteCommand cmdSequence = new SQLiteCommand(commandStringSequence, myDBConn as SQLiteConnection);
                SQLiteCommand cmdSequenceInit1 = new SQLiteCommand(commandStringSequenceInit1, myDBConn as SQLiteConnection);
                SQLiteCommand cmdSequenceInit2 = new SQLiteCommand(commandStringSequenceInit2, myDBConn as SQLiteConnection);
                SQLiteCommand cmdSequenceInit3 = new SQLiteCommand(commandStringSequenceInit3, myDBConn as SQLiteConnection);
                SQLiteCommand cmdSequenceInit4 = new SQLiteCommand(commandStringSequenceInit4, myDBConn as SQLiteConnection);
                SQLiteCommand cmdVraceno = new SQLiteCommand(commandStringVraceno, myDBConn as SQLiteConnection);
                SQLiteCommand cmdPoskozeno = new SQLiteCommand(commandStringPoskozeno, myDBConn as SQLiteConnection);
                SQLiteCommand cmdOsoby = new SQLiteCommand(commandStringOsoby, myDBConn as SQLiteConnection);
                SQLiteCommand cmdZmeny = new SQLiteCommand(commandStringZmeny, myDBConn as SQLiteConnection);
                SQLiteCommand cmdPujceno = new SQLiteCommand(commandStringPujceno, myDBConn as SQLiteConnection);
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



        public override DbTransaction startTransaction()
        {
            try
            {
                SQLiteTransaction transaction = (myDBConn as SQLiteConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                return transaction;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public override void stopTransaction(DbTransaction transaction)
        {
            if (transaction != null)
            {
                (transaction as SQLiteTransaction).Commit();

            }
        }





        public override Int32 addLineKaret(string DBnazev, string DBJK, string DBnormacsn, string DBnormadin,
                                         string DBvyrobce, double DBcena, string DBpoznamka, int DBminstav,
                                         double DBcelkcena, int DBucetstav, int DBfyzstav,
                                         string DBrozmer, string DBanalucet, DateTime DBdate, string DBstredisko,
                                         string DBkodzmeny, string DBdruhp, string DBodpis, string DBzavod)
        {

            string commandStringSeq1 = "SELECT poradi FROM tabseq WHERE nazev = 'naradi'";
            string commandStringSeq2 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'naradi'";
            
            string commandString = "INSERT INTO karta ( poradi, nazev, jk, normacsn, normadin, vyrobce, cena, poznamka, minimum, celkcena, ucetstav, fyzstav, rozmer, analucet, tdate, stredisko, kodzmeny, druh, odpis, zavod) " +
                  "VALUES ( ? ,?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";


            if (DBIsOpened())
            {

                SQLiteCommand cmdSeq1 = new SQLiteCommand(commandStringSeq1, myDBConn as SQLiteConnection);
                SQLiteDataReader seqReader = cmdSeq1.ExecuteReader();
                seqReader.Read();
                Int32 poradi = seqReader.GetInt32(0);
                seqReader.Close();

                SQLiteCommand cmd = new SQLiteCommand(commandString, myDBConn as SQLiteConnection);

                cmd.Parameters.Add("@p0", DbType.Int32);
                cmd.Parameters["@p0"].Value = poradi;
                cmd.Parameters.Add("@p1", DbType.String);
                cmd.Parameters["@p1"].Value = DBnazev;
                cmd.Parameters.Add("@p2", DbType.String);
                cmd.Parameters["@p2"].Value = DBJK;
                cmd.Parameters.Add("@p3", DbType.String);
                cmd.Parameters["@p3"].Value = DBnormacsn;
                cmd.Parameters.Add("@p4", DbType.String);
                cmd.Parameters["@p4"].Value = DBnormadin;
                cmd.Parameters.Add("@p5", DbType.String);
                cmd.Parameters["@p5"].Value = DBvyrobce;
                cmd.Parameters.Add("@p6", DbType.Double);
                cmd.Parameters["@p6"].Value = DBcena;
                cmd.Parameters.Add("@p7", DbType.String);
                cmd.Parameters["@p7"].Value = DBpoznamka;
                cmd.Parameters.Add("@p8", DbType.Int64);
                cmd.Parameters["@p8"].Value = DBminstav;
                cmd.Parameters.Add("@p9", DbType.Double);
                cmd.Parameters["@p9"].Value = DBcelkcena;

                cmd.Parameters.Add("@p15", DbType.Int64);
                cmd.Parameters["@p15"].Value = DBucetstav;
                cmd.Parameters.Add("@p16", DbType.Int64);
                cmd.Parameters["@p16"].Value = DBfyzstav;
                cmd.Parameters.Add("@p17", DbType.String);
                cmd.Parameters["@p17"].Value = DBrozmer;
                cmd.Parameters.Add("@p18", DbType.String);
                cmd.Parameters["@p18"].Value = DBanalucet;
                cmd.Parameters.Add("@p19", DbType.Date);
                cmd.Parameters["@p19"].Value = DBdate;
                cmd.Parameters.Add("@p20", DbType.String);
                cmd.Parameters["@p20"].Value = DBstredisko;
                cmd.Parameters.Add("@p21", DbType.String);
                cmd.Parameters["@p21"].Value = DBkodzmeny;
                cmd.Parameters.Add("@p22", DbType.String);
                cmd.Parameters["@p22"].Value = DBdruhp;
                cmd.Parameters.Add("@p23", DbType.String);
                cmd.Parameters["@p23"].Value = DBodpis;
                cmd.Parameters.Add("@p24", DbType.String);
                cmd.Parameters["@p24"].Value = DBzavod;

                cmd.ExecuteNonQuery();

                SQLiteCommand cmdSeq2 = new SQLiteCommand(commandStringSeq2, myDBConn as SQLiteConnection);
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

                SQLiteCommand cmdSeq1 = new SQLiteCommand(commandStringSeq1, myDBConn as SQLiteConnection);
                SQLiteDataReader seqReader = cmdSeq1.ExecuteReader();
                seqReader.Read();
                Int32 poradi = seqReader.GetInt32(0);
                seqReader.Close();              

                SQLiteCommand cmd = new SQLiteCommand(commandString, myDBConn as SQLiteConnection);

                SQLiteParameter p0 = new SQLiteParameter(DbType.Int32);
                p0.Value = poradi;
                SQLiteParameter p1 = new SQLiteParameter(DbType.String);
                p1.Value = DBnazev;
                SQLiteParameter p2 = new SQLiteParameter(DbType.String);
                p2.Value = DBJK;
                SQLiteParameter p3 = new SQLiteParameter(DbType.String);
                p3.Value = DBnormacsn;
                SQLiteParameter p4 = new SQLiteParameter(DbType.String);
                p4.Value = DBnormadin;
                SQLiteParameter p5 = new SQLiteParameter(DbType.String);
                p5.Value = DBvyrobce;
                SQLiteParameter p6 = new SQLiteParameter(DbType.Double);
                p6.Value = DBcena;
                SQLiteParameter p7 = new SQLiteParameter(DbType.String);
                p7.Value = DBpoznamka;
                SQLiteParameter p8 = new SQLiteParameter(DbType.Int64);
                p8.Value = DBminstav;
                SQLiteParameter p9 = new SQLiteParameter(DbType.Double);
                p9.Value = DBcelkcena;
                SQLiteParameter p15 = new SQLiteParameter(DbType.Int64);
                p15.Value = DBucetstav;
                SQLiteParameter p16 = new SQLiteParameter(DbType.Int64);
                p16.Value = DBfyzstav;
                SQLiteParameter p17 = new SQLiteParameter(DbType.String);
                p17.Value = DBrozmer;
                SQLiteParameter p18 = new SQLiteParameter(DbType.String);
                p18.Value = DBanalucet;
                SQLiteParameter p19 = new SQLiteParameter(DbType.Date);
                p19.Value = DBdate;
                SQLiteParameter p20 = new SQLiteParameter(DbType.String);
                p20.Value = DBstredisko;
                SQLiteParameter p21 = new SQLiteParameter(DbType.String);
                p21.Value = DBkodzmeny;
                SQLiteParameter p22 = new SQLiteParameter(DbType.String);
                p22.Value = DBdruhp;
                SQLiteParameter p23 = new SQLiteParameter(DbType.String);
                p23.Value = DBodpis;
                SQLiteParameter p24 = new SQLiteParameter(DbType.String);
                p24.Value = DBzavod;
                SQLiteParameter p25 = new SQLiteParameter(DbType.String);
                p25.Value = DBucetkscen;
                SQLiteParameter p26 = new SQLiteParameter(DbType.String);
                p26.Value = DBtest;
                SQLiteParameter p27 = new SQLiteParameter(DbType.String);
                p27.Value = DBpomroz;
                SQLiteParameter p28 = new SQLiteParameter(DbType.Date);
                p28.Value = DBkdatum;
                SQLiteParameter p29 = new SQLiteParameter(DbType.String);
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

                SQLiteCommand cmdSeq2 = new SQLiteCommand(commandStringSeq2, myDBConn as SQLiteConnection);
                cmdSeq2.ExecuteNonQuery();
                return poradi;

            }
            return 0;
        }


        public override Int32 addLineVraceno(string DBjmeno, string DBosCislo, string DBdilna, string DBpracoviste,
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

                SQLiteCommand cmdSeq1 = new SQLiteCommand(commandStringSeq1, myDBConn as SQLiteConnection);
                SQLiteDataReader seqReader = cmdSeq1.ExecuteReader();
                seqReader.Read();
                Int32 poradi = seqReader.GetInt32(0);
                seqReader.Close();              

                SQLiteCommand cmd = new SQLiteCommand(commandString, myDBConn as SQLiteConnection);

                SQLiteParameter p0 = new SQLiteParameter(DbType.Int32);
                p0.Value = poradi;
                SQLiteParameter p1 = new SQLiteParameter("p1", DbType.String);
                p1.Value = DBjmeno;
                SQLiteParameter p2 = new SQLiteParameter("p2", DbType.String);
                p2.Value = DBosCislo;
                SQLiteParameter p3 = new SQLiteParameter("p3", DbType.String);
                p3.Value = DBdilna;
                SQLiteParameter p4 = new SQLiteParameter("p4", DbType.String);
                p4.Value = DBpracoviste;
                SQLiteParameter p5 = new SQLiteParameter("p5", DbType.String);
                p5.Value = DBvyrobek;
                SQLiteParameter p6 = new SQLiteParameter("p6", DbType.String);
                p6.Value = DBnazev;
                SQLiteParameter p7 = new SQLiteParameter("p7", DbType.String);
                p7.Value = DBJK;
                SQLiteParameter p8 = new SQLiteParameter("p8", DbType.String);
                p8.Value = DBrozmer;
                SQLiteParameter p9 = new SQLiteParameter("p9", DbType.Int64);
                p9.Value = DBpocetks;
                SQLiteParameter p10 = new SQLiteParameter("p10", DbType.Double);
                p10.Value = DBcena;
                SQLiteParameter p11 = new SQLiteParameter("p11", DbType.Date);
                p11.Value = DBdate;
                SQLiteParameter p12 = new SQLiteParameter("p12", DbType.String);
                p12.Value = DBnormacsn;
                SQLiteParameter p13 = new SQLiteParameter("p13", DbType.String);
                p13.Value = DBkrjmeno;
                SQLiteParameter p14 = new SQLiteParameter("p14", DbType.Double);
                p14.Value = DBcelkCena;
                SQLiteParameter p15 = new SQLiteParameter("p15", DbType.String);
                p15.Value = DBvevCislo;
                SQLiteParameter p16 = new SQLiteParameter("p16", DbType.String);
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

                SQLiteCommand cmdSeq2 = new SQLiteCommand(commandStringSeq2, myDBConn as SQLiteConnection);
                cmdSeq2.ExecuteNonQuery();
                return poradi;

            }
            return 0;
        }





        public override Int32 addLinePoskozeno(string DBjmeno, string DBosCislo, string DBdilna, string DBpracoviste,
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

                SQLiteCommand cmdSeq1 = new SQLiteCommand(commandStringSeq1, myDBConn as SQLiteConnection);
                SQLiteDataReader seqReader = cmdSeq1.ExecuteReader();
                seqReader.Read();
                Int32 poradi = seqReader.GetInt32(0);
                seqReader.Close();

                SQLiteCommand cmd = new SQLiteCommand(commandString, myDBConn as SQLiteConnection);

                SQLiteParameter p0 = new SQLiteParameter(DbType.Int32);
                p0.Value = poradi;
                SQLiteParameter p1 = new SQLiteParameter("p1", DbType.String);
                p1.Value = DBjmeno;
                SQLiteParameter p2 = new SQLiteParameter("p2", DbType.String);
                p2.Value = DBosCislo;
                SQLiteParameter p3 = new SQLiteParameter("p3", DbType.String);
                p3.Value = DBdilna;
                SQLiteParameter p4 = new SQLiteParameter("p4", DbType.String);
                p4.Value = DBpracoviste;
                SQLiteParameter p5 = new SQLiteParameter("p5", DbType.String);
                p5.Value = DBvyrobek;
                SQLiteParameter p6 = new SQLiteParameter("p6", DbType.String);
                p6.Value = DBnazev;
                SQLiteParameter p7 = new SQLiteParameter("p7", DbType.String);
                p7.Value = DBJK;
                SQLiteParameter p8 = new SQLiteParameter("p8", DbType.String);
                p8.Value = DBrozmer;
                SQLiteParameter p9 = new SQLiteParameter("p9", DbType.Int64);
                p9.Value = DBpocetks;
                SQLiteParameter p10 = new SQLiteParameter("p10", DbType.Double);
                p10.Value = DBcena;
                SQLiteParameter p11 = new SQLiteParameter("p11", DbType.Date);
                p11.Value = DBdate;
                SQLiteParameter p12 = new SQLiteParameter("p12", DbType.String);
                p12.Value = DBnormacsn;
                SQLiteParameter p13 = new SQLiteParameter("p13", DbType.String);
                p13.Value = DBkrjmeno;
                SQLiteParameter p14 = new SQLiteParameter("p14", DbType.Double);
                p14.Value = DBcelkCena;
                SQLiteParameter p15 = new SQLiteParameter("p15", DbType.String);
                p15.Value = DBvevCislo;
                SQLiteParameter p16 = new SQLiteParameter("p16", DbType.String);
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

                SQLiteCommand cmdSeq2 = new SQLiteCommand(commandStringSeq2, myDBConn as SQLiteConnection);
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

                SQLiteCommand cmd = new SQLiteCommand(commandString, myDBConn as SQLiteConnection);

                SQLiteParameter p1 = new SQLiteParameter("p1", DbType.String);
                p1.Value = DBprijmeni;
                SQLiteParameter p2 = new SQLiteParameter("p2", DbType.String);
                p2.Value = DBjmeno;
                SQLiteParameter p3 = new SQLiteParameter("p3", DbType.String);
                p3.Value = DBulice;
                SQLiteParameter p4 = new SQLiteParameter("p4", DbType.String);
                p4.Value = DBmesto;
                SQLiteParameter p5 = new SQLiteParameter("p5", DbType.String);
                p5.Value = DBpsc;
                SQLiteParameter p6 = new SQLiteParameter("p6", DbType.String);
                p6.Value = DBtelHome;
                SQLiteParameter p7 = new SQLiteParameter("p7", DbType.String);
                p7.Value = DBosCislo;
                SQLiteParameter p8 = new SQLiteParameter("p8", DbType.String);
                p8.Value = DBodeleni;
                SQLiteParameter p9 = new SQLiteParameter("p9", DbType.String);
                p9.Value = DBtelZam;
                SQLiteParameter p10 = new SQLiteParameter("p10", DbType.String);
                p10.Value = DBstredisko;
                SQLiteParameter p11 = new SQLiteParameter("p11", DbType.String);
                p11.Value = DBpujSoub;
                SQLiteParameter p12 = new SQLiteParameter("p12", DbType.String);
                p12.Value = DBpracoviste;
                SQLiteParameter p13 = new SQLiteParameter("p13", DbType.String);
                p13.Value = DBcisZnamky;
                SQLiteParameter p14 = new SQLiteParameter("p14", DbType.String);
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

            string commandString = "INSERT INTO zmeny ( parporadi, pomozjk, datum, poznamka, prijem, vydej, zustatek, zapkarta, vevcislo, pocivc, stav, poradi )" +
                  "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";


            //    string commandStringZmeny = "create table zmeny ( pomozjk char(15), datum date, poznamka char(22)," +
            //              "prijem integer, vydej integer, zustatek integer, zapkarta char(5), vevcislo char(12)," +
            //              "pocivc integer, contrcod char(12), dosudnvrc char(1), prijtyp char(2), vydejtyp char(2)," +
            //              "poradi integer, stav char(1), nazev char(60), vyber char(1), lastsoub char(8), aktadr char(9)," +
            //              "cena float, ucetkscen float, jk char(15) );";


            if (DBIsOpened())
            {

                SQLiteCommand cmd = new SQLiteCommand(commandString, myDBConn as SQLiteConnection);

                SQLiteParameter p0 = new SQLiteParameter("p0", DbType.Int32);
                p0.Value = DBparPoradi;
                SQLiteParameter p1 = new SQLiteParameter("p1", DbType.String);
                p1.Value = DBpomocJK;
                SQLiteParameter p2 = new SQLiteParameter("p2", DbType.Date);
                p2.Value = DBdatum;
                SQLiteParameter p3 = new SQLiteParameter("p3", DbType.String);
                p3.Value = DBpoznamka;
                SQLiteParameter p4 = new SQLiteParameter("p4", DbType.Int64);
                p4.Value = DBPrijem;
                SQLiteParameter p5 = new SQLiteParameter("p5", DbType.Int64);
                p5.Value = DBvydej;
                SQLiteParameter p6 = new SQLiteParameter("p6", DbType.Int64);
                p6.Value = DBzustatek;
                SQLiteParameter p7 = new SQLiteParameter("p7", DbType.String);
                p7.Value = DBzapKarta;
                SQLiteParameter p8 = new SQLiteParameter("p8", DbType.String);
                p8.Value = DBvevCislo;
                SQLiteParameter p9 = new SQLiteParameter("p9", DbType.Int64);
                p9.Value = DBpocIvc;
                SQLiteParameter p10 = new SQLiteParameter("p10", DbType.String);
                p10.Value = DBstav;
                SQLiteParameter p14 = new SQLiteParameter("p14", DbType.Int64);
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


        public override void addLinePujceno(int DBparPoradi, string DBosCislo, DateTime DBdatum, int DBks,
                                         string DBjmeno, string DBPrijmeni, string DBnazev, string DBjk,
                                         double DBcena, int DBzmPoradi)
        {
            string commandString = "INSERT INTO pujceno ( poradi, oscislo, nporadi, zporadi, stavks, pjmeno, pprijmeni, pnazev, pjk, pdatum, pks, pcena )" +
                  "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";

            string commandStringSeq1 = "SELECT poradi FROM tabseq WHERE nazev = 'pujceno'";
            string commandStringSeq2 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'pujceno'";
            string commandStringSeq3 = "SELECT poradi FROM zmeny WHERE parporadi = ? AND stav = 'U' AND zapkarta = ?  AND datum = ? ";
            

            if (DBIsOpened())
            {

                SQLiteCommand cmdSeq1 = new SQLiteCommand(commandStringSeq1, myDBConn as SQLiteConnection);
                SQLiteDataReader seqReader1 = cmdSeq1.ExecuteReader();
                seqReader1.Read();
                Int32 pujcPoradi = seqReader1.GetInt32(0);
                seqReader1.Close();


                SQLiteCommand cmdSeq3 = new SQLiteCommand(commandStringSeq3, myDBConn as SQLiteConnection);
                SQLiteParameter pz0 = new SQLiteParameter("pz0", DbType.Int32);
                pz0.Value = DBparPoradi;
                SQLiteParameter pz1 = new SQLiteParameter("pz1", DbType.String);
                pz1.Value = DBosCislo;
                SQLiteParameter pz2 = new SQLiteParameter("pz2", DbType.Date);
                pz2.Value = DBdatum;
                cmdSeq3.Parameters.Add(pz0);
                cmdSeq3.Parameters.Add(pz1);
                cmdSeq3.Parameters.Add(pz2);
                SQLiteDataReader seqReader2 = cmdSeq3.ExecuteReader();

                Int32 zmenyPoradi;
                if (seqReader2.Read() == true)
                {
                    zmenyPoradi = seqReader2.GetInt32(0);
                    seqReader2.Close();
                }
                else
                {
                    zmenyPoradi = 0;
                    MessageBox.Show("Neexistuje zmena stavu pro vypujcení. Poradi/Nazev:" + DBparPoradi.ToString() + " - " + DBnazev + " OSCislo: " + DBosCislo + " - " + DBPrijmeni);
                }

                SQLiteCommand cmd = new SQLiteCommand(commandString, myDBConn as SQLiteConnection);

                SQLiteParameter p0 = new SQLiteParameter("p0", DbType.Int32);
                p0.Value = pujcPoradi;
                SQLiteParameter p1 = new SQLiteParameter("p1", DbType.String);
                p1.Value = DBosCislo;
                SQLiteParameter p2 = new SQLiteParameter("p2", DbType.Int32);
                p2.Value = DBparPoradi;
                SQLiteParameter p3 = new SQLiteParameter("p3", DbType.Int32);
                p3.Value = zmenyPoradi; // DBzmPoradi;
                SQLiteParameter p4 = new SQLiteParameter("p4", DbType.Int32);
                p4.Value = DBks; // DBzmPoradi;

                SQLiteParameter p5 = new SQLiteParameter("p5", DbType.String);
                p5.Value = DBjmeno;
                SQLiteParameter p6 = new SQLiteParameter("p6", DbType.String);
                p6.Value = DBPrijmeni;
                SQLiteParameter p7 = new SQLiteParameter("p7", DbType.String);
                p7.Value = DBnazev;
                SQLiteParameter p8 = new SQLiteParameter("p8", DbType.String);
                p8.Value = DBjk;
                SQLiteParameter p9 = new SQLiteParameter("p9", DbType.Date);
                p9.Value = DBdatum;
                SQLiteParameter p10 = new SQLiteParameter("p10", DbType.Int32);
                p10.Value = DBks;
                SQLiteParameter p11 = new SQLiteParameter("p11", DbType.Double);
                p11.Value = DBcena;

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
                cmd.ExecuteNonQuery();

                SQLiteCommand cmdSeq2 = new SQLiteCommand(commandStringSeq2, myDBConn as SQLiteConnection);
                cmdSeq2.ExecuteNonQuery();
                }
        }


        public override DataTable loadDataTable(string DBSelect)
        {
            if (DBIsOpened())
            {
                SQLiteDataAdapter myDataAdapter = new SQLiteDataAdapter(DBSelect, myDBConn as SQLiteConnection);

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
                SQLiteCommand cmd = new SQLiteCommand(DBSelect, myDBConn as SQLiteConnection);

                SQLiteParameter p1 = new SQLiteParameter("p1", DbType.String);
                p1.Value = whileValue;
                cmd.Parameters.Add(p1);
                SQLiteDataReader myReader = cmd.ExecuteReader();
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
                SQLiteCommand cmd = new SQLiteCommand(DBSelect, myDBConn as SQLiteConnection);

                SQLiteParameter p1 = new SQLiteParameter("p1", DbType.Int32);
                p1.Value = whileValue;
                cmd.Parameters.Add(p1);
                SQLiteDataReader myReader = cmd.ExecuteReader();
                myReader.Read();
                Int64 countRows = myReader.GetInt64(0);
                myReader.Close();
                return countRows;
            }
            return -1;
        }


        // pridani nove polozky do tabulky naradi
        public override Int32 addNewLineNaradi(string DBnazev, string DBJK, string DBnormacsn, string DBnormadin,
                                         string DBvyrobce, decimal DBcena, string DBpoznamka, long DBminstav,
                                         decimal DBcelkcena, long DBucetstav, long DBfyzstav,
                                         string DBrozmer, string DBanalucet, decimal DBucetkscen, DateTime DBkdatum)
        {
            string commandStringSeq0 = "SELECT count(*) as countporadi from naradi";
            string commandStringSeq1 = "SELECT MAX(poradi) as maxporadi from naradi";
            string commandStringSeq2 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'naradi'";

            string commandString2 = "INSERT INTO naradi ( poradi, nazev, jk, normacsn, normadin, vyrobce, cena, poznamka, minimum, celkcena,  ucetstav, fyzstav, rozmer, analucet, tdate, stredisko, kodzmeny, druh, odpis, zavod, ucetkscen, test, pomroz, kdatum, kodd ) " +
                  "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '' )";

            SQLiteTransaction transaction = null;

            if (DBIsOpened())
            {
                try
                {
                    Int32 maxporadi;
                    transaction = (myDBConn as SQLiteConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                    SQLiteCommand cmd0 = new SQLiteCommand(commandStringSeq0, myDBConn as SQLiteConnection);
                    SQLiteDataReader myReader0 = cmd0.ExecuteReader();
                    myReader0.Read();
                    Int32 countporadi = myReader0.GetInt32(0);
                    myReader0.Close();

                    if (countporadi == 0) maxporadi = 1;
                    else
                    {


                        SQLiteCommand cmd1 = new SQLiteCommand(commandStringSeq1, myDBConn as SQLiteConnection);
                        SQLiteDataReader myReader1 = cmd1.ExecuteReader();
                        myReader1.Read();
                        maxporadi = myReader1.GetInt32(0);
                        myReader1.Close();
                        maxporadi++;
                    }

                    SQLiteCommand cmd = new SQLiteCommand(commandString2, myDBConn as SQLiteConnection);

                    SQLiteParameter p0 = new SQLiteParameter("p0", DbType.String);
                    p0.Value = maxporadi;
                    SQLiteParameter p1 = new SQLiteParameter("p1", DbType.String);
                    p1.Value = DBnazev;
                    SQLiteParameter p2 = new SQLiteParameter("p2", DbType.String);
                    p2.Value = DBJK;
                    SQLiteParameter p3 = new SQLiteParameter("p3", DbType.String);
                    p3.Value = DBnormacsn;
                    SQLiteParameter p4 = new SQLiteParameter("p4", DbType.String);
                    p4.Value = DBnormadin;
                    SQLiteParameter p5 = new SQLiteParameter("p5", DbType.String);
                    p5.Value = DBvyrobce;
                    SQLiteParameter p6 = new SQLiteParameter("p6", DbType.Double);
                    p6.Value = DBcena;
                    SQLiteParameter p7 = new SQLiteParameter("p7", DbType.String);
                    p7.Value = DBpoznamka;
                    SQLiteParameter p8 = new SQLiteParameter("p8", DbType.Int64);
                    p8.Value = DBminstav;
                    SQLiteParameter p9 = new SQLiteParameter("p9", DbType.Double);
                    p9.Value = DBcelkcena;
                    SQLiteParameter p15 = new SQLiteParameter("p15", DbType.Int64);
                    p15.Value = DBucetstav;
                    SQLiteParameter p16 = new SQLiteParameter("p16", DbType.Int64);
                    p16.Value = DBfyzstav;
                    SQLiteParameter p17 = new SQLiteParameter("p17", DbType.String);
                    p17.Value = DBrozmer;
                    SQLiteParameter p18 = new SQLiteParameter("p18", DbType.String);
                    p18.Value = DBanalucet;
                    SQLiteParameter p19 = new SQLiteParameter("p19", DbType.Date);
                    p19.Value = new DateTime(0);
                    SQLiteParameter p20 = new SQLiteParameter("p20", DbType.String);
                    p20.Value = "";
                    SQLiteParameter p21 = new SQLiteParameter("p21", DbType.String);
                    p21.Value = "";
                    SQLiteParameter p22 = new SQLiteParameter("p22", DbType.String);
                    p22.Value = "";
                    SQLiteParameter p23 = new SQLiteParameter("p23", DbType.String);
                    p23.Value = "";
                    SQLiteParameter p24 = new SQLiteParameter("p24", DbType.String);
                    p24.Value = "";
                    SQLiteParameter p25 = new SQLiteParameter("p25", DbType.Double);
                    p25.Value = DBucetkscen;
                    SQLiteParameter p26 = new SQLiteParameter("p26", DbType.String);
                    p26.Value = "";
                    SQLiteParameter p27 = new SQLiteParameter("p27", DbType.String);
                    p27.Value = "";
                    SQLiteParameter p28 = new SQLiteParameter("p28", DbType.Date);
                    p28.Value = DBkdatum;
                    SQLiteParameter p29 = new SQLiteParameter("p29", DbType.String);
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

                    SQLiteCommand cmdSeq2 = new SQLiteCommand(commandStringSeq2, myDBConn as SQLiteConnection);

                    cmdSeq2.Transaction = transaction;
                    cmdSeq2.ExecuteNonQuery();
                    
                    if (transaction != null)
                    {
                        (transaction as SQLiteTransaction).Commit();
                    }
                    return maxporadi;

                }
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as SQLiteTransaction).Rollback();
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

            SQLiteTransaction transaction = null;

            if (DBIsOpened())
            {
                try
                {
                    transaction = (myDBConn as SQLiteConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                    SQLiteCommand cmd = new SQLiteCommand(commandString1, myDBConn as SQLiteConnection);

                    SQLiteParameter px = new SQLiteParameter("px", DbType.String);
                    px.Value = DBosCislo;
                    cmd.Parameters.Add(px);

                    SQLiteDataReader myReader = cmd.ExecuteReader();
                    

                    bool osCisloExist = myReader.Read();
                    // true osCisloExist
                    myReader.Close();

                    if (!osCisloExist)
                    {

                        cmd = new SQLiteCommand(commandString2, myDBConn as SQLiteConnection);

                        SQLiteParameter p1 = new SQLiteParameter("p1", DbType.String);
                        p1.Value = DBprijmeni;
                        SQLiteParameter p2 = new SQLiteParameter("p2", DbType.String);
                        p2.Value = DBjmeno;
                        SQLiteParameter p3 = new SQLiteParameter("p3", DbType.String);
                        p3.Value = DBulice;
                        SQLiteParameter p4 = new SQLiteParameter("p4", DbType.String);
                        p4.Value = DBmesto;
                        SQLiteParameter p5 = new SQLiteParameter("p5", DbType.String);
                        p5.Value = DBpsc;
                        SQLiteParameter p6 = new SQLiteParameter("p6", DbType.String);
                        p6.Value = DBtelHome;
                        SQLiteParameter p7 = new SQLiteParameter("p7", DbType.String);
                        p7.Value = DBosCislo;
                        SQLiteParameter p8 = new SQLiteParameter("p8", DbType.String);
                        p8.Value = DBoddeleni;
                        SQLiteParameter p9 = new SQLiteParameter("p9", DbType.String);
                        p9.Value = DBtelZam;
                        SQLiteParameter p10 = new SQLiteParameter("p10", DbType.String);
                        p10.Value = DBstredisko;
                        SQLiteParameter p11 = new SQLiteParameter("p11", DbType.String);
                        p11.Value = "";
                        SQLiteParameter p12 = new SQLiteParameter("p12", DbType.String);
                        p12.Value = DBpracoviste;
                        SQLiteParameter p13 = new SQLiteParameter("p13", DbType.String);
                        p13.Value = DBcisZnamky;
                        SQLiteParameter p14 = new SQLiteParameter("p14", DbType.String);
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
                        (transaction as SQLiteTransaction).Commit();
                    }

                    if (!osCisloExist) return 0; //os cislo existuje nezapisujeme
                    else return -1;

                }  //try
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as SQLiteTransaction).Rollback();
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

            SQLiteTransaction transaction = null;

            if (DBIsOpened())
            {
                try
                {
                    transaction = (myDBConn as SQLiteConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                    SQLiteCommand cmd = new SQLiteCommand(commandString2, myDBConn as SQLiteConnection);

                    SQLiteParameter p1 = new SQLiteParameter("p1", DbType.String);
                    p1.Value = DBnazev;
                    SQLiteParameter p2 = new SQLiteParameter("p2", DbType.String);
                    p2.Value = DBJK;
                    SQLiteParameter p3 = new SQLiteParameter("p3", DbType.String);
                    p3.Value = DBnormacsn;
                    SQLiteParameter p4 = new SQLiteParameter("p4", DbType.String);
                    p4.Value = DBnormadin;
                    SQLiteParameter p5 = new SQLiteParameter("p5", DbType.String);
                    p5.Value = DBvyrobce;
                    SQLiteParameter p6 = new SQLiteParameter("p6", DbType.Double);
                    p6.Value = DBcena;
                    SQLiteParameter p7 = new SQLiteParameter("p7", DbType.String);
                    p7.Value = DBpoznamka;
                    SQLiteParameter p8 = new SQLiteParameter("p8", DbType.Int64);
                    p8.Value = DBminstav;
                    SQLiteParameter p9 = new SQLiteParameter("p9", DbType.Double);
                    p9.Value = DBcelkcena;
                    SQLiteParameter p15 = new SQLiteParameter("p15", DbType.Int64);
                    p15.Value = DBucetstav;
                    SQLiteParameter p16 = new SQLiteParameter("p16", DbType.Int64);
                    p16.Value = DBfyzstav;
                    SQLiteParameter p17 = new SQLiteParameter("p17", DbType.String);
                    p17.Value = DBrozmer;
                    SQLiteParameter p18 = new SQLiteParameter("p18", DbType.String);
                    p18.Value = DBanalucet;
                    SQLiteParameter p25 = new SQLiteParameter("p25", DbType.Double);
                    p25.Value = DBucetkscen;
                    SQLiteParameter p30 = new SQLiteParameter("p30", DbType.String);
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
                        (transaction as SQLiteTransaction).Commit();
                    }
                    return true;

                }
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as SQLiteTransaction).Rollback();
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

            SQLiteTransaction transaction = null;
            if (DBIsOpened())
            {
                try
                {
                    transaction = (myDBConn as SQLiteConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                    SQLiteCommand cmd = new SQLiteCommand(commandString1, myDBConn as SQLiteConnection);

                    SQLiteParameter p1 = new SQLiteParameter("p1", DbType.String);
                    p1.Value = DBjmeno;
                    SQLiteParameter p2 = new SQLiteParameter("p2", DbType.String);
                    p2.Value = DBosCislo;
                    SQLiteParameter p3 = new SQLiteParameter("p3", DbType.String);
                    p3.Value = DBdilna;
                    SQLiteParameter p4 = new SQLiteParameter("p4", DbType.String);
                    p4.Value =DBprovoz;
                    SQLiteParameter p5 = new SQLiteParameter("p5", DbType.String);
                    p5.Value = DBvyrobek;
                    SQLiteParameter p6 = new SQLiteParameter("p6", DbType.String);
                    p6.Value = DBnazev;
                    SQLiteParameter p7 = new SQLiteParameter("p7", DbType.String);
                    p7.Value = DBrozmer;
                    SQLiteParameter p8 = new SQLiteParameter("p8", DbType.Int64);
                    p8.Value = DBpocetKS;
                    SQLiteParameter p9 = new SQLiteParameter("p9", DbType.Double);
                    p9.Value = DBcena;
                    SQLiteParameter p10 = new SQLiteParameter("p10", DbType.Date);
                    p10.Value = DBdatum;
                    SQLiteParameter p11 = new SQLiteParameter("p11", DbType.String);
                    p11.Value = DBCSN;
                    SQLiteParameter p12 = new SQLiteParameter("p12", DbType.String);
                    p12.Value = DBkrjmeno;
                    SQLiteParameter p13 = new SQLiteParameter("p13", DbType.String);
                    p13.Value = DBkonto;
                    SQLiteParameter p14 = new SQLiteParameter("p14", DbType.String);
                    p14.Value = DBJK;
                    SQLiteParameter p15 = new SQLiteParameter("p15", DbType.Int32);
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
                        (transaction as SQLiteTransaction).Commit();
                    }
                    return true;

                }
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as SQLiteTransaction).Rollback();
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

            SQLiteTransaction transaction = null;
            if (DBIsOpened())
            {
                try
                {
                    transaction = (myDBConn as SQLiteConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                    SQLiteCommand cmd = new SQLiteCommand(commandString1, myDBConn as SQLiteConnection);

                    SQLiteParameter p1 = new SQLiteParameter("p1", DbType.String);
                    p1.Value = DBjmeno;
                    SQLiteParameter p2 = new SQLiteParameter("p2", DbType.String);
                    p2.Value = DBosCislo;
                    SQLiteParameter p3 = new SQLiteParameter("p3", DbType.String);
                    p3.Value = DBdilna;
                    SQLiteParameter p4 = new SQLiteParameter("p4", DbType.String);
                    p4.Value = DBprovoz;
                    SQLiteParameter p5 = new SQLiteParameter("p5", DbType.String);
                    p5.Value = DBvyrobek;
                    SQLiteParameter p6 = new SQLiteParameter("p6", DbType.String);
                    p6.Value = DBnazev;
                    SQLiteParameter p7 = new SQLiteParameter("p7", DbType.String);
                    p7.Value = DBrozmer;
                    SQLiteParameter p8 = new SQLiteParameter("p8", DbType.Int64);
                    p8.Value = DBpocetKS;
                    SQLiteParameter p9 = new SQLiteParameter("p9", DbType.Double);
                    p9.Value = DBcena;
                    SQLiteParameter p10 = new SQLiteParameter("p10", DbType.Date);
                    p10.Value = DBdatum;
                    SQLiteParameter p11 = new SQLiteParameter("p11", DbType.String);
                    p11.Value = DBCSN;
                    SQLiteParameter p12 = new SQLiteParameter("p12", DbType.String);
                    p12.Value = DBkrjmeno;
                    SQLiteParameter p13 = new SQLiteParameter("p13", DbType.String);
                    p13.Value = DBkonto;
                    SQLiteParameter p14 = new SQLiteParameter("p14", DbType.String);
                    p14.Value = DBJK;
                    SQLiteParameter p15 = new SQLiteParameter("p15", DbType.Int32);
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
                        (transaction as SQLiteTransaction).Commit();
                    }
                    return true;

                }
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as SQLiteTransaction).Rollback();
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

            SQLiteTransaction transaction = null;

            if (DBIsOpened())
            {
                try
                {
                    transaction = (myDBConn as SQLiteConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                    SQLiteCommand cmd = new SQLiteCommand(commandString2, myDBConn as SQLiteConnection);

                    SQLiteParameter p1 = new SQLiteParameter("p1", DbType.String);
                    p1.Value = DBnazev;
                    SQLiteParameter p2 = new SQLiteParameter("p2", DbType.String);
                    p2.Value = DBJK;
                    SQLiteParameter p3 = new SQLiteParameter("p3", DbType.String);
                    p3.Value = DBnormacsn;
                    SQLiteParameter p4 = new SQLiteParameter("p4", DbType.String);
                    p4.Value = DBnormadin;
                    SQLiteParameter p5 = new SQLiteParameter("p5", DbType.String);
                    p5.Value = DBvyrobce;
                    SQLiteParameter p6 = new SQLiteParameter("p6", DbType.Double);
                    p6.Value = DBcena;
                    SQLiteParameter p7 = new SQLiteParameter("p7", DbType.String);
                    p7.Value = DBpoznamka;
                    SQLiteParameter p8 = new SQLiteParameter("p8", DbType.Int64);
                    p8.Value = DBminstav;
                    SQLiteParameter p9 = new SQLiteParameter("p9", DbType.Double);
                    p9.Value = DBcelkcena;
                    SQLiteParameter p15 = new SQLiteParameter("p15", DbType.Int64);
                    p15.Value = DBucetstav;
                    SQLiteParameter p16 = new SQLiteParameter("p16", DbType.Int64);
                    p16.Value = DBfyzstav;
                    SQLiteParameter p17 = new SQLiteParameter("p17", DbType.String);
                    p17.Value = DBrozmer;
                    SQLiteParameter p18 = new SQLiteParameter("p18", DbType.String);
                    p18.Value = DBanalucet;
                    SQLiteParameter p30 = new SQLiteParameter("p30", DbType.String);
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
                        (transaction as SQLiteTransaction).Commit();
                    }
                    return true;

                }
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as SQLiteTransaction).Rollback();
                    }
                    return false;
                }

            }

            return true;
        }



        public override Boolean deleteLineKaret(Int32 poradi)
        {
            SQLiteTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandString1 = "DELETE FROM karta WHERE poradi = ? ";

                try
                {
                    try
                    {
                        transaction = (myDBConn as SQLiteConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }

                    //zrusime
                    SQLiteCommand cmd1 = new SQLiteCommand(commandString1, myDBConn as SQLiteConnection);
                    SQLiteParameter p1 = new SQLiteParameter("p1", DbType.Int32);
                    p1.Value = poradi;
                    cmd1.Parameters.Add(p1);

                    cmd1.Transaction = transaction;
                    cmd1.ExecuteNonQuery();

                    if (transaction != null)
                    {
                        (transaction as SQLiteTransaction).Commit();
                    }

                }
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as SQLiteTransaction).Rollback();
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

            SQLiteTransaction transaction = null;

            if (DBIsOpened())
            {
                try
                {
                    transaction = (myDBConn as SQLiteConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);


                    SQLiteCommand cmd = new SQLiteCommand(commandString2, myDBConn as SQLiteConnection);

                    SQLiteParameter p1 = new SQLiteParameter("p1", DbType.String);
                    p1.Value = DBprijmeni;
                    SQLiteParameter p2 = new SQLiteParameter("p2", DbType.String);
                    p2.Value = DBjmeno;
                    SQLiteParameter p3 = new SQLiteParameter("p3", DbType.String);
                    p3.Value = DBulice;
                    SQLiteParameter p4 = new SQLiteParameter("p4", DbType.String);
                    p4.Value = DBmesto;
                    SQLiteParameter p5 = new SQLiteParameter("p5", DbType.String);
                    p5.Value = DBpsc;
                    SQLiteParameter p6 = new SQLiteParameter("p6", DbType.String);
                    p6.Value = DBtelHome;
                    SQLiteParameter p8 = new SQLiteParameter("p8", DbType.String);
                    p8.Value = DBoddeleni;
                    SQLiteParameter p9 = new SQLiteParameter("p9", DbType.String);
                    p9.Value = DBtelZam;
                    SQLiteParameter p10 = new SQLiteParameter("p10", DbType.String);
                    p10.Value = DBstredisko;
                    SQLiteParameter p11 = new SQLiteParameter("p11", DbType.String);
                    p11.Value = "";
                    SQLiteParameter p12 = new SQLiteParameter("p12", DbType.String);
                    p12.Value = DBpracoviste;
                    SQLiteParameter p13 = new SQLiteParameter("p13", DbType.String);
                    p13.Value = DBcisZnamky;
                    SQLiteParameter p14 = new SQLiteParameter("p14", DbType.String);
                    p14.Value = DBpoznamka;
                    SQLiteParameter p15 = new SQLiteParameter("p15", DbType.String);
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
                        (transaction as SQLiteTransaction).Commit();
                    }
                    return true;

                }  //try
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as SQLiteTransaction).Rollback();
                    }
                    return false;
                }
            } // db is opened
            else return true;
        }


        public override Boolean moveNaraddiToNewKaret(Int32 DBporadi)
        {
            SQLiteTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandString2 = "DELETE FROM naradi  where poradi = ? ";


                string commandString1 = "INSERT INTO karta ( poradi, nazev, jk, normacsn, normadin, vyrobce, cena, poznamka, minimum, celkcena, ucetstav, fyzstav, rozmer, analucet, tdate, stredisko, kodzmeny, druh, odpis, zavod ) " +
                      "SELECT poradi, nazev, jk, normacsn, normadin, vyrobce, cena, poznamka, minimum, celkcena, ucetstav, fyzstav, rozmer, analucet, tdate, stredisko, kodzmeny, druh, odpis, zavod FROM naradi WHERE poradi = ?";


                try
                {
                    try
                    {
                        transaction = (myDBConn as SQLiteConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }

                    SQLiteCommand cmd1 = new SQLiteCommand(commandString1, myDBConn as SQLiteConnection);
                    SQLiteParameter p1 = new SQLiteParameter("p1", DbType.Int32);
                    p1.Value = DBporadi;
                    cmd1.Parameters.Add(p1);

                    cmd1.Transaction = transaction;
                    cmd1.ExecuteNonQuery();



                    SQLiteCommand cmd2 = new SQLiteCommand(commandString2, myDBConn as SQLiteConnection);
                    SQLiteParameter p2 = new SQLiteParameter("p2", DbType.Int32);
                    p2.Value = DBporadi;
                    cmd2.Parameters.Add(p2);

                    cmd2.Transaction = transaction;
                    cmd2.ExecuteNonQuery();

                    if (transaction != null)
                    {
                        (transaction as SQLiteTransaction).Commit();
                    }


                }
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as SQLiteTransaction).Rollback();
                    }
                    return false;  // chyba
                }
                return true;  // ok
            }
            return false;  // database neni otevrena
        }



        // pridani nove polozky do tabulky zmeny
        public override Int32 addNewLineZmeny(Int32 DBporadi, string DBJK, DateTime DBdatum, Int32 DBprijem, Int32 DBvydej, string DBpoznamka, string DBstav, Int32 DBfyzStavZmena, Int32 DBucetStavZmena, string DBosCislo)
        {
            SQLiteTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandString1 = "UPDATE naradi set fyzstav = fyzstav + ?, ucetstav = ucetstav+ ?  where poradi = ? ";
                string commandString2 = "INSERT INTO zmeny (parporadi, pomozjk, datum, poznamka, prijem, vydej, zustatek, zapkarta, vevcislo, pocivc, stav, poradi )" +
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";
                string commandString3 = "SELECT poradi, zustatek from zmeny where parporadi = ? ORDER BY poradi DESC";

                try
                {
                    try
                    {
                        transaction = (myDBConn as SQLiteConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }


                    SQLiteCommand cmdr = new SQLiteCommand(commandString3, myDBConn as SQLiteConnection);

                    SQLiteParameter px = new SQLiteParameter("px", DbType.Int32);
                    px.Value = DBporadi;
                    cmdr.Parameters.Add(px);

                    Int32 poradi;
                    Int32 zustatek;

                    SQLiteDataReader myReader = cmdr.ExecuteReader();
                    // true osCisloExist
                    if (myReader.Read() == true)
                    {
                        poradi = myReader.GetInt32(0) + 1;
                        zustatek = myReader.GetInt32(1);
                    }
                    else
                    {
                        poradi = 1;
                        zustatek = 0;

                    }

                    myReader.Close();



                    SQLiteCommand cmd1 = new SQLiteCommand(commandString1, myDBConn as SQLiteConnection);

                    SQLiteParameter pn1 = new SQLiteParameter("p1", DbType.Int32);
                    pn1.Value = DBfyzStavZmena; // DBprijem - DBvydej;
                    SQLiteParameter pn2 = new SQLiteParameter("p2", DbType.Int32);
                    pn2.Value = DBucetStavZmena; // DBprijem - DBvydej;
                    SQLiteParameter pn3 = new SQLiteParameter("p3", DbType.Int32);
                    pn3.Value = DBporadi;

                    cmd1.Parameters.Add(pn1);
                    cmd1.Parameters.Add(pn2);
                    cmd1.Parameters.Add(pn3);

                    cmd1.Transaction = transaction;
                    cmd1.ExecuteNonQuery();

                    SQLiteCommand cmd2 = new SQLiteCommand(commandString2, myDBConn as SQLiteConnection);

                    SQLiteParameter p1 = new SQLiteParameter("p1", DbType.Int32);
                    p1.Value = DBporadi;
                    SQLiteParameter p2 = new SQLiteParameter("p2", DbType.String);
                    p2.Value = DBJK;
                    SQLiteParameter p3 = new SQLiteParameter("p3", DbType.Date);
                    p3.Value = DBdatum;
                    SQLiteParameter p4 = new SQLiteParameter("p4", DbType.String);
                    p4.Value = DBpoznamka;
                    SQLiteParameter p5 = new SQLiteParameter("p5", DbType.Int32);
                    p5.Value = DBprijem;
                    SQLiteParameter p6 = new SQLiteParameter("p6", DbType.Int32);
                    p6.Value = DBvydej;
                    SQLiteParameter p7 = new SQLiteParameter("p7", DbType.Int32);
                    p7.Value = zustatek +DBprijem - DBvydej;
                    SQLiteParameter p8 = new SQLiteParameter("p8", DbType.String);
                    p8.Value = DBosCislo;
                    SQLiteParameter p9 = new SQLiteParameter("p9", DbType.String);
                    p9.Value = "";
                    SQLiteParameter p10 = new SQLiteParameter("p10", DbType.Int32);
                    p10.Value = 0;
                    SQLiteParameter p11 = new SQLiteParameter("p11", DbType.String);
                    p11.Value = DBstav;
                    SQLiteParameter p12 = new SQLiteParameter("p12", DbType.Int32);
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
                        (transaction as SQLiteTransaction).Commit();
                    }


                }
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as SQLiteTransaction).Rollback();
                    }
                    return -1;
                }
                return 0;
            }
            return 0;
        }




        public override Int32 addNewLineZmenyAndPujceno(Int32 DBparPoradi, string DBJK, DateTime DBdatum, Int32 DBks, string DBpoznamka, string DBvevCislo,
                                                        string DBosCislo, string DBjmeno, string DBprijmeni, string DBnazev, double DBcena)
        {
            SQLiteTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandReadString1 = "SELECT poradi, zustatek from zmeny where parporadi = ? ORDER BY poradi DESC";
                string commandReadString2 = "SELECT fyzstav from naradi where poradi = ? ";
                string commandReadString3 = "SELECT poradi FROM tabseq WHERE nazev = 'pujceno'";


                string commandString1 = "UPDATE naradi set fyzstav = fyzstav - ?  where poradi = ? ";
                string commandString2 = "INSERT INTO zmeny (parporadi, pomozjk, datum, poznamka, prijem, vydej, zustatek, zapkarta, vevcislo, pocivc, stav, poradi )" +
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";

                string commandString5 = "INSERT INTO pujceno ( poradi, oscislo, nporadi, zporadi, pjmeno, pprijmeni, pnazev, pjk, pdatum, pks, pcena, stavks )" +
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";

                string commandString6 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'pujceno'";
                Int32 pujcPoradi = 0;

                try
                {
                    try
                    {
                        transaction = (myDBConn as SQLiteConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }

                    SQLiteCommand cmdr = new SQLiteCommand(commandReadString1, myDBConn as SQLiteConnection);
                    SQLiteParameter px = new SQLiteParameter("px", DbType.Int32);
                    px.Value = DBparPoradi;
                    cmdr.Parameters.Add(px);

                    SQLiteCommand cmdr2 = new SQLiteCommand(commandReadString2, myDBConn as SQLiteConnection);
                    SQLiteParameter prm = new SQLiteParameter("prm", DbType.Int32);
                    prm.Value = DBparPoradi;
                    cmdr2.Parameters.Add(prm);
                    SQLiteDataReader seqReader2 = cmdr2.ExecuteReader();
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
                            (transaction as SQLiteTransaction).Rollback();
                        }
                        return -1;
                    }

                    if (fyzstav < DBks)
                    // pozadavek na odpis vice ks nez je existujici stav na vydejne
                    {
                        if (transaction != null)
                        {
                            (transaction as SQLiteTransaction).Rollback();
                        }
                        return -2;
                    }


                    Int32 poradi;
                    Int32 zustatek;

                    SQLiteDataReader seqReader1 = cmdr.ExecuteReader();
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


                    SQLiteCommand cmdSeq2 = new SQLiteCommand(commandReadString3, myDBConn as SQLiteConnection);
                    SQLiteDataReader seqReader3 = cmdSeq2.ExecuteReader();
                    seqReader3.Read();
                    pujcPoradi = seqReader3.GetInt32(0);
                    seqReader3.Close();

                    // tab naradi

                    SQLiteCommand cmd1 = new SQLiteCommand(commandString1, myDBConn as SQLiteConnection);
                    SQLiteParameter pn1 = new SQLiteParameter("pn1", DbType.Int32);
                    pn1.Value = DBks; // DBprijem - DBvydej;
                    SQLiteParameter pn2 = new SQLiteParameter("pn2", DbType.Int32);
                    pn2.Value = DBparPoradi;
                    cmd1.Parameters.Add(pn1);
                    cmd1.Parameters.Add(pn2);
                    cmd1.Transaction = transaction;
                    cmd1.ExecuteNonQuery();

                    //  tab zmeny
                    SQLiteCommand cmd2 = new SQLiteCommand(commandString2, myDBConn as SQLiteConnection);
                    SQLiteParameter p1 = new SQLiteParameter("p1", DbType.Int32);
                    p1.Value = DBparPoradi;
                    SQLiteParameter p2 = new SQLiteParameter("p2", DbType.String);
                    p2.Value = DBJK;
                    SQLiteParameter p3 = new SQLiteParameter("p3", DbType.Date);
                    p3.Value = DBdatum;
                    SQLiteParameter p4 = new SQLiteParameter("p4", DbType.String);
                    p4.Value = DBpoznamka;
                    SQLiteParameter p5 = new SQLiteParameter("p5", DbType.Int32);
                    p5.Value = 0;
                    SQLiteParameter p6 = new SQLiteParameter("p6", DbType.Int32);
                    p6.Value = DBks;
                    SQLiteParameter p7 = new SQLiteParameter("p7", DbType.Int32);
                    p7.Value = zustatek - DBks;
                    SQLiteParameter p8 = new SQLiteParameter("p8", DbType.String);
                    p8.Value = DBosCislo;
                    SQLiteParameter p9 = new SQLiteParameter("p9", DbType.String);
                    p9.Value = DBvevCislo;
                    SQLiteParameter p10 = new SQLiteParameter("p10", DbType.Int32);
                    p10.Value = 0;
                    SQLiteParameter p11 = new SQLiteParameter("p11", DbType.String);
                    p11.Value = "U";
                    SQLiteParameter p12 = new SQLiteParameter("p12", DbType.Int32);
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


                    SQLiteCommand cmd = new SQLiteCommand(commandString5, myDBConn as SQLiteConnection);

                    SQLiteParameter pp0 = new SQLiteParameter("pp0", DbType.Int32);
                    pp0.Value = pujcPoradi;
                    SQLiteParameter pp1 = new SQLiteParameter("pp1", DbType.String);
                    pp1.Value = DBosCislo;
                    SQLiteParameter pp2 = new SQLiteParameter("pp2", DbType.Int32);
                    pp2.Value = DBparPoradi;
                    SQLiteParameter pp3 = new SQLiteParameter("pp3", DbType.Int32);
                    pp3.Value = poradi; // DBzmPoradi;
                    SQLiteParameter pp4 = new SQLiteParameter("pp4", DbType.String);
                    pp4.Value = DBjmeno;
                    SQLiteParameter pp5 = new SQLiteParameter("pp5", DbType.String);
                    pp5.Value = DBprijmeni;
                    SQLiteParameter pp6 = new SQLiteParameter("pp6", DbType.String);
                    pp6.Value = DBnazev;
                    SQLiteParameter pp7 = new SQLiteParameter("pp7", DbType.String);
                    pp7.Value = DBJK;
                    SQLiteParameter pp8 = new SQLiteParameter("pp8", DbType.Date);
                    pp8.Value = DBdatum;
                    SQLiteParameter pp9 = new SQLiteParameter("pp9", DbType.Int32);
                    pp9.Value = DBks;
                    SQLiteParameter pp10 = new SQLiteParameter("pp10", DbType.Double);
                    pp10.Value = DBcena;
                    SQLiteParameter pp11 = new SQLiteParameter("pp11", DbType.Int32);
                    pp11.Value = DBks;

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
                    cmd.Parameters.Add(pp11);
                    cmd.ExecuteNonQuery();

                    SQLiteCommand cmdSeq3 = new SQLiteCommand(commandString6, myDBConn as SQLiteConnection);
                    cmdSeq3.ExecuteNonQuery();

                    if (transaction != null)
                    {
                        (transaction as SQLiteTransaction).Commit();
                    }

                }
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as SQLiteTransaction).Rollback();
                    }
                    return -1;
                }
                return pujcPoradi;
            }
                return -1;
        }



        public override Int32 addNewLineZmenyAndPoskozeno(Int32 DBporadi, string DBJK, DateTime DBdatum, Int32 DBvydej, string DBpoznamka,
                                                          string osCislo, string DBjmeno, string DBprijmeni, string DBstredisko, string DBprovoz,
                                                          string DBnazev, string DBrozmer, string DBkonto, double DBcena, double DBcelkCena, string DBcsn, string DBcisZak)
        {
            SQLiteTransaction transaction = null;

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
                        transaction = (myDBConn as SQLiteConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }

                    SQLiteCommand cmdr2 = new SQLiteCommand(commandStringRead2, myDBConn as SQLiteConnection);

                    SQLiteParameter px2 = new SQLiteParameter("px2", DbType.Int32);
                    px2.Value = DBporadi;
                    cmdr2.Parameters.Add(px2);
                    cmdr2.Transaction = transaction;
                    SQLiteDataReader myReader2 = cmdr2.ExecuteReader();
                    // true fyzstav exist -- zaznam mohl bzt meyitom smazan
                    if (myReader2.Read() == true)
                    {
                        Int32 fyzstav = myReader2.GetInt32(myReader2.GetOrdinal("fyzstav"));
                        Int32 ucetstav = myReader2.GetInt32(myReader2.GetOrdinal("ucetstav"));
                        myReader2.Close();
                        if ((fyzstav < DBvydej) || (ucetstav < DBvydej))
                        {
                            if (transaction != null)
                            {
                                (transaction as SQLiteTransaction).Rollback();
                            }
                            return -2;
                        }

                        // poradi pro zmeny
                        SQLiteCommand cmdr1 = new SQLiteCommand(commandStringRead1, myDBConn as SQLiteConnection);
                        SQLiteParameter px = new SQLiteParameter("px", DbType.Int32);
                        px.Value = DBporadi;
                        cmdr1.Parameters.Add(px);

                        Int32 poradi;
                        Int32 zustatek;

                        cmdr1.Transaction = transaction;
                        SQLiteDataReader myReader = cmdr1.ExecuteReader();
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
                        SQLiteCommand cmdSeq1 = new SQLiteCommand(commandStringRead3, myDBConn as SQLiteConnection);
                        SQLiteDataReader seqReader = cmdSeq1.ExecuteReader();
                        seqReader.Read();
                        Int32 poradiPoskozeno = seqReader.GetInt32(0);
                        seqReader.Close();

                        SQLiteCommand cmd1 = new SQLiteCommand(commandString1, myDBConn as SQLiteConnection);

                        SQLiteParameter pn1 = new SQLiteParameter("p1", DbType.Int32);
                        pn1.Value = DBvydej;
                        SQLiteParameter pn2 = new SQLiteParameter("p2", DbType.Int32);
                        pn2.Value = DBvydej;
                        SQLiteParameter pn3 = new SQLiteParameter("p3", DbType.Int32);
                        pn3.Value = DBporadi;

                        cmd1.Parameters.Add(pn1);
                        cmd1.Parameters.Add(pn2);
                        cmd1.Parameters.Add(pn3);

                        cmd1.Transaction = transaction;
                        cmd1.ExecuteNonQuery();

                        SQLiteCommand cmd2 = new SQLiteCommand(commandString2, myDBConn as SQLiteConnection);

                        SQLiteParameter p1 = new SQLiteParameter("p1", DbType.Int32);
                        p1.Value = DBporadi;
                        SQLiteParameter p2 = new SQLiteParameter("p2", DbType.String);
                        p2.Value = DBJK;
                        SQLiteParameter p3 = new SQLiteParameter("p3", DbType.Date);
                        p3.Value = DBdatum;
                        SQLiteParameter p4 = new SQLiteParameter("p4", DbType.String);
                        p4.Value = DBpoznamka;
                        SQLiteParameter p5 = new SQLiteParameter("p5", DbType.Int32);
                        p5.Value = 0;
                        SQLiteParameter p6 = new SQLiteParameter("p6", DbType.Int32);
                        p6.Value = DBvydej;
                        SQLiteParameter p7 = new SQLiteParameter("p7", DbType.Int32);
                        p7.Value = zustatek - DBvydej;
                        SQLiteParameter p8 = new SQLiteParameter("p8", DbType.String);
                        p8.Value = osCislo;
                        SQLiteParameter p9 = new SQLiteParameter("p9", DbType.String);
                        p9.Value = "";
                        SQLiteParameter p10 = new SQLiteParameter("p10", DbType.Int32);
                        p10.Value = 0;
                        SQLiteParameter p11 = new SQLiteParameter("p11", DbType.String);
                        p11.Value = "O";
                        SQLiteParameter p12 = new SQLiteParameter("p12", DbType.Int32);
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
                        SQLiteCommand cmd3 = new SQLiteCommand(commandString5, myDBConn as SQLiteConnection);

                        SQLiteParameter pz0 = new SQLiteParameter("pz0", DbType.Int32);
                        pz0.Value = poradiPoskozeno;
                        SQLiteParameter pz1 = new SQLiteParameter("pz1", DbType.String); // prijmeni
                        pz1.Value = DBprijmeni;
                        SQLiteParameter pz2 = new SQLiteParameter("pz2", DbType.Int32);
                        pz2.Value = osCislo;
                        SQLiteParameter pz3 = new SQLiteParameter("pz3", DbType.String);
                        pz3.Value = DBstredisko;
                        SQLiteParameter pz4 = new SQLiteParameter("pz4", DbType.String);
                        pz4.Value = DBprovoz;
                        SQLiteParameter pz5 = new SQLiteParameter("pz5", DbType.String);
                        pz5.Value = DBcisZak;
                        SQLiteParameter pz6 = new SQLiteParameter("pz6", DbType.String);
                        pz6.Value = DBnazev;
                        SQLiteParameter pz7 = new SQLiteParameter("pz7", DbType.String);
                        pz7.Value = DBJK;
                        SQLiteParameter pz8 = new SQLiteParameter("pz8", DbType.String);
                        pz8.Value = DBrozmer;
                        SQLiteParameter pz9 = new SQLiteParameter("pz9", DbType.Int32);
                        pz9.Value = DBvydej;
                        SQLiteParameter pz10 = new SQLiteParameter("pz10", DbType.Double);
                        pz10.Value = DBcena;
                        SQLiteParameter pz11 = new SQLiteParameter("pz11", DbType.Date);
                        pz11.Value = DBdatum;
                        SQLiteParameter pz12 = new SQLiteParameter("pz12", DbType.String);
                        pz12.Value = DBcsn;
                        SQLiteParameter pz13 = new SQLiteParameter("pz13", DbType.String);
                        pz13.Value = DBjmeno;
                        SQLiteParameter pz14 = new SQLiteParameter("pz14", DbType.Double);
                        pz14.Value = DBcelkCena;
                        SQLiteParameter pz15 = new SQLiteParameter("pz15", DbType.String);
                        pz15.Value = ""; // DBvevCislo;
                        SQLiteParameter pz16 = new SQLiteParameter("pz16", DbType.String);
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

                        SQLiteCommand cmd4 = new SQLiteCommand(commandString4, myDBConn as SQLiteConnection);
                        cmd4.Transaction = transaction;
                        cmd4.ExecuteNonQuery();


                    }
                    else
                    {
                        myReader2.Close();
                        if (transaction != null)
                        {
                             (transaction as SQLiteTransaction).Rollback();
                        }
                        return -1;
                    }

                   if (transaction != null)
                   {
                      (transaction as SQLiteTransaction).Commit();
                   }

                }
                catch (Exception)
                {
                    // doslo k chybe
                    if (transaction != null)
                    {
                        (transaction as SQLiteTransaction).Rollback();
                    }
                    return -1;  // chyba
                }
                return 0;  // ok
            }
            return -1;  // databaze neni otevrena
        }



        public override Hashtable getNaradiZmenyLine(Int32 poradi, Hashtable DBRow)
        {
            if (DBRow == null) DBRow = new Hashtable();

            string DBSelect = "SELECT n.poradi, n.fyzstav as fyzstav, n.ucetstav as ucetstav, z.zustatek as zmeny_zustatek FROM naradi  n, zmeny z " +
                              "WHERE z.poradi = (SELECT MAX(s.poradi) FROM zmeny s WHERE s.parporadi = " + poradi.ToString() +" GROUP BY s.parporadi) " +
                              "AND z.parporadi = n.poradi and z.parporadi = " + poradi.ToString();

            return getDBLine(DBSelect, DBRow);
        }


        public override Hashtable getDBLine(string DBSelect, Hashtable DBRow)
        {
            if (DBIsOpened())
            {
                SQLiteCommand cmdr0 = new SQLiteCommand(DBSelect, myDBConn as SQLiteConnection);
                SQLiteDataReader myReader = cmdr0.ExecuteReader();

                if (myReader.Read())
                {
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


    }
}
