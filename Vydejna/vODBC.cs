using System;
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
    class vODBC : vDatabase
    {

        private string commandStringUsers = "create table uzivatele (userid varchar(15) PRIMARY KEY,  password char(40), jmeno varchar(15), prijmeni varchar(15), admin char(1), permission char(60));";

        private string commandStringSetting = "create table nastaveni ( setid varchar(15) PRIMARY KEY,  permission char(1), permission_hs char(20), permission_hi int, userid char(15), datum date);";


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
            //            openDB();
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

                OdbcCommand cmdUsers = new OdbcCommand("DROP TABLE uzivatele", myDBConn as OdbcConnection);
                try
                {
                    cmdUsers.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cmdUsers.Dispose();
                }

                OdbcCommand cmdSettings = new OdbcCommand("DROP TABLE nastaveni", myDBConn as OdbcConnection);
                try
                {
                    cmdSettings.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cmdSettings.Dispose();
                }
                //             myDBConn.Close();
                //             myDBConn.Dispose();
            }
        }



        public override void CreateTables()
        {
            string commandStringSequence = "create table tabseq ( nazev char (15), poradi integer);";
            string commandStringSequenceInit1 = "INSERT INTO tabseq ( nazev, poradi) VALUES ('naradi', 1)";
            string commandStringSequenceInit2 = "INSERT INTO tabseq ( nazev, poradi) VALUES ('vraceno', 1)";
            string commandStringSequenceInit3 = "INSERT INTO tabseq ( nazev, poradi) VALUES ('poskozeno', 1)";
            string commandStringSequenceInit4 = "INSERT INTO tabseq ( nazev, poradi) VALUES ('pujceno', 1)";



            string commandStringKarta = "create table karta ( poradi integer, nazev varchar(60), jk varchar(15), normacsn varchar (15)," +
                      "normadin varchar(15), vyrobce varchar(40), cena float, poznamka varchar(60), minimum integer," +
                      "celkcena float, ucetstav integer, fyzstav integer, rozmer varchar(20), analucet varchar(5), tdate date," +
                      "stredisko varchar(5), druh varchar(3), odpis varchar(3));";



            string commandStringNaradi = "create table naradi ( poradi integer, nazev varchar(60), jk varchar(15), normacsn varchar (15)," +
                      "normadin varchar(15), vyrobce varchar(40), cena float, poznamka varchar(60), minimum integer," +
                      "celkcena float, ucetstav integer, fyzstav integer, rozmer varchar(20), analucet varchar(5), tdate date," +
                      "stredisko varchar(5), druh varchar(3), odpis varchar(3), ucetkscen float, kdatum date, kodd varchar(2));";



            string commandStringPoskozeno = "create table poskozeno ( poradi integer, jmeno varchar(15), oscislo varchar(8), dilna varchar(15)," +
                      "pracoviste varchar(20), vyrobek varchar(15),nazev varchar(60), jk varchar(15), rozmer varchar(25)," +
                      "pocetks integer, cena float, datum date, csn varchar(15), krjmeno varchar(15)," +
                      "celkcena float, vevcislo varchar(12), konto varchar(15) );";



            string commandStringVraceno = "create table vraceno ( poradi integer, jmeno varchar(15), oscislo varchar(8), dilna varchar(15)," +
                      "pracoviste varchar(20), vyrobek varchar(15), nazev varchar(60), jk varchar(15), rozmer varchar(25)," +
                      "pocetks integer, cena float, datum date, csn varchar(15), krjmeno varchar(15)," +
                      "celkcena float, vevcislo varchar(12), konto varchar(15) );";


            string commandStringOsoby = "create table osoby ( jmeno varchar(15), prijmeni varchar(15), ulice varchar(20)," +
                      "mesto varchar(25), psc varchar(7),telhome varchar(15), oscislo varchar(8), odeleni varchar(20)," +
                      "telzam varchar(15), stredisko varchar(10), pujsoub varchar(12), pracoviste varchar(10), cisznamky varchar(5), poznamka varchar(120) );";



            string commandStringZmeny = "create table zmeny ( parporadi integer, pomozjk varchar(15), datum date, poznamka varchar(22)," +
                      "prijem integer, vydej integer, zustatek integer, zapkarta varchar(8), vevcislo varchar(12)," +
                      "pocivc integer, stav varchar(1), poradi integer );";


            string commandStringPujceno = "create table pujceno (poradi integer, oscislo varchar(8), nporadi integer, zporadi integer, stavks integer, pjmeno varchar(15)," +
                      "pprijmeni varchar(15), pnazev varchar(60), pjk varchar(15), pdatum date, pks integer, pcena float);";


            //            openDB();
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
                //            OdbcCommand cmdUsers = new OdbcCommand(commandStringUsers, myDBConn as OdbcConnection);
                //            OdbcCommand cmdSettings = new OdbcCommand(commandStringSetting, myDBConn as OdbcConnection);

                if (!(tableUzivateleExist())) CreateTableUzivatele();
                if (!(tableNastaveniExist())) CreateTableNastaveni();

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
                    //                    cmdUsers.ExecuteNonQuery();
                    //                    cmdSettings.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    //                    myDBConn.Close();
                    //                    cmdSettings.Dispose();
                    //                    cmdUsers.Dispose();
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
                    //                    myDBConn.Dispose();
                }
            }

        }

        public override void DeleteTables()
        {
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
        }



        public override void CreateIndexes()
        {
            openDB();
            if (DBIsOpened())
            {
                string commandStringIn1 = "CREATE UNIQUE INDEX naradiPorIN ON naradi (poradi)";
                string commandStringIn2 = "CREATE UNIQUE INDEX  kartaPorIN ON karta (poradi)";
                string commandStringIn3 = "CREATE UNIQUE INDEX  vracenoPorIN ON vraceno (poradi)";
                string commandStringIn4 = "CREATE UNIQUE INDEX  poskozenoPorIN ON poskozeno (poradi)";
                string commandStringIn5 = "CREATE UNIQUE INDEX  osobyPorIN ON osoby (oscislo)";
                string commandStringIn6 = "CREATE UNIQUE INDEX  zmenyPorIN ON zmeny(parporadi,poradi)";
                string commandStringIn7 = "CREATE UNIQUE INDEX  pujcenoPorIN ON (poradi)";
                string[] commandStrings = new String[7] {commandStringIn1, commandStringIn2, commandStringIn3,
                         commandStringIn4, commandStringIn5, commandStringIn6, commandStringIn7};
                Int32 indexErrCount = 0;
                foreach (string commandStringIn in commandStrings)
                {
                    OdbcCommand cmdIndex = new OdbcCommand(commandStringIn, myDBConn as OdbcConnection);
                    try
                    {
                        cmdIndex.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        indexErrCount++;
                    }
                    finally
                    {
                        cmdIndex.Dispose();
                    }
                }
            }
        }


        public override void DropIndexes()
        {
            openDB();
            if (DBIsOpened())
            {
                string commandStringIn1 = "DROP INDEX naradiPorIN";
                string commandStringIn2 = "DROP INDEX kartaPorIN";
                string commandStringIn3 = "DROP INDEX vracenoPorIN";
                string commandStringIn4 = "DROP INDEX poskozenoPorIN";
                string commandStringIn5 = "DROP INDEX osobyPorIN";
                string commandStringIn6 = "DROP INDEX zmenyPorIN";
                string commandStringIn7 = "DROP INDEX pujcenoPorIN";

                string[] commandStrings = new String[7] {commandStringIn1, commandStringIn2, commandStringIn3,
                         commandStringIn4, commandStringIn5, commandStringIn6, commandStringIn7};
                Int32 indexErrCount = 0;
                foreach (string commandStringIn in commandStrings)
                {

                    OdbcCommand cmdIndex = new OdbcCommand(commandStringIn, myDBConn as OdbcConnection);
                    try
                    {
                        cmdIndex.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        indexErrCount++;
                    }
                    finally
                    {
                        cmdIndex.Dispose();
                    }
                }
            }
        }



        public override Boolean ZamykaniStranek()
        {
            return false;
        }

        public override Boolean ZamykaniRadek()
        {
            return false;
        }


        public override Int32 VycisteniTabulek()
        {
            return -1;
        }



        public override void CreateTableUzivatele()
        {
            if (DBIsOpened())
            {
                OdbcCommand cmdUsers = new OdbcCommand(commandStringUsers, myDBConn as OdbcConnection);
                try
                {
                    cmdUsers.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cmdUsers.Dispose();
                }
            }
        }


        public override void CreateTableNastaveni()
        {
            if (DBIsOpened())
            {
                OdbcCommand cmdUsers = new OdbcCommand(commandStringSetting, myDBConn as OdbcConnection);
                try
                {
                    cmdUsers.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cmdUsers.Dispose();
                }
            }
        }




        public override Int32 addLineKaret(string DBnazev, string DBJK, string DBnormacsn, string DBnormadin,
                                         string DBvyrobce, double DBcena, string DBpoznamka, int DBminstav,
                                         double DBcelkcena, int DBucetstav, int DBfyzstav,
                                         string DBrozmer, string DBanalucet, DateTime DBdate, string DBstredisko,
                                         string DBdruhp, string DBodpis)
        {
            string commandStringSeq1 = "SELECT poradi FROM tabseq WHERE nazev = 'naradi'";
            string commandStringSeq2 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'naradi'";

            string commandString = "INSERT INTO karta ( poradi, nazev, jk, normacsn, normadin, vyrobce, cena," +
                  " poznamka, minimum, celkcena, ucetstav, fyzstav, rozmer, analucet, tdate, stredisko," +
                  " druh, odpis) " +
                  "VALUES ( ? ,?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";


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
                OdbcParameter p22 = new OdbcParameter("p22", OdbcType.NChar);
                p22.Value = DBdruhp;
                OdbcParameter p23 = new OdbcParameter("p23", OdbcType.NChar);
                p23.Value = DBodpis;


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
                cmd.Parameters.Add(p22);
                cmd.Parameters.Add(p23);
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
                                         string DBdruhp, string DBodpis, double DBucetkscen, DateTime DBkdatum, string DBkodd)
        {

            string commandStringSeq1 = "SELECT poradi FROM tabseq WHERE nazev = 'naradi'";
            string commandStringSeq2 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'naradi'";

            string commandString = "INSERT INTO naradi ( poradi, nazev, jk, normacsn, normadin, vyrobce, cena," +
                  " poznamka, minimum, celkcena, ucetstav, fyzstav, rozmer, analucet, tdate, stredisko, druh," +
                  " odpis, ucetkscen, kdatum, kodd ) " +
                  "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";

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
                OdbcParameter p22 = new OdbcParameter("p22", OdbcType.NChar);
                p22.Value = DBdruhp;
                OdbcParameter p23 = new OdbcParameter("p23", OdbcType.NChar);
                p23.Value = DBodpis;
                OdbcParameter p25 = new OdbcParameter("p25", OdbcType.Double);
                p25.Value = DBucetkscen;
                OdbcParameter p28 = new OdbcParameter("p26", OdbcType.Date);
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
                cmd.Parameters.Add(p22);
                cmd.Parameters.Add(p23);
                cmd.Parameters.Add(p25);
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




        // pridani nove polozky do tabulky naradi
        public override Int32 addNewLineNaradi(string DBnazev, string DBJK, string DBnormacsn, string DBnormadin,
                                         string DBvyrobce, decimal DBcena, string DBpoznamka, long DBminstav,
                                         decimal DBcelkcena, long DBucetstav, long DBfyzstav,
                                         string DBrozmer, string DBanalucet, decimal DBucetkscen, DateTime DBkdatum)
        {
            string commandReadString0 = "SELECT count(*) as countporadi from naradi";
            string commandReadString1 = "SELECT MAX(poradi) as maxporadi from naradi";
            string commandString1 = "UPDATE  tabseq set poradi = ? WHERE nazev = 'naradi'";

            string commandString2 = "INSERT INTO naradi ( poradi, nazev, jk, normacsn, normadin, vyrobce, cena," +
                  " poznamka, minimum, celkcena,  ucetstav, fyzstav, rozmer, analucet, tdate, stredisko, druh," +
                  " odpis, ucetkscen, kdatum, kodd ) " +
                  "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";

            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                try
                {
                    Int32 maxporadi;

                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }



                    OdbcCommand cmd0 = new OdbcCommand(commandReadString0, myDBConn as OdbcConnection);
                    cmd0.Transaction = transaction;
                    OdbcDataReader myReader0 = cmd0.ExecuteReader();
                    myReader0.Read();
                    Int32 countporadi = myReader0.GetInt32(0);
                    myReader0.Close();

                    if (countporadi == 0) maxporadi = 1;
                    else
                    {


                        OdbcCommand cmd1 = new OdbcCommand(commandReadString1, myDBConn as OdbcConnection);
                        cmd1.Transaction = transaction;
                        OdbcDataReader myReader1 = cmd1.ExecuteReader();
                        myReader1.Read();
                        maxporadi = myReader1.GetInt32(0);
                        myReader1.Close();
                        maxporadi++;
                    }

                    OdbcCommand cmd = new OdbcCommand(commandString2, myDBConn as OdbcConnection);

                    cmd.Parameters.AddWithValue("@poradi", maxporadi).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("nazev", DBnazev);
                    cmd.Parameters.AddWithValue("@jk", DBJK);
                    cmd.Parameters.AddWithValue("@normacsn", DBnormacsn);
                    cmd.Parameters.AddWithValue("@normadin", DBnormadin);
                    cmd.Parameters.AddWithValue("@vyrobce", DBvyrobce);
                    cmd.Parameters.AddWithValue("@cena", DBcena).DbType = DbType.Double;
                    cmd.Parameters.AddWithValue("@poznamka", DBpoznamka);
                    cmd.Parameters.AddWithValue("@minimum", DBminstav).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@celkcena", DBcelkcena).DbType = DbType.Double;
                    cmd.Parameters.AddWithValue("@ucetstav", DBucetstav).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@fyzstav", DBfyzstav).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@rozmer", DBrozmer);
                    cmd.Parameters.AddWithValue("@analucet", DBanalucet);
                    cmd.Parameters.AddWithValue("@tdate", new DateTime(0));
                    cmd.Parameters.AddWithValue("@stredisko", "");
                    cmd.Parameters.AddWithValue("@druh", "");
                    cmd.Parameters.AddWithValue("@odpis", "");
                    cmd.Parameters.AddWithValue("@ucetkscen", DBucetkscen).DbType = DbType.Double;
                    cmd.Parameters.AddWithValue("@kdatum", DBkdatum);
                    cmd.Parameters.AddWithValue("@kodd", "");

                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();

                    OdbcCommand cmdSeq2 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);

                    cmdSeq2.Parameters.AddWithValue("@poradi", maxporadi + 1).DbType = DbType.Int32; //ukazuje prvni volne
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




        // pridani nove polozky do tabulky zmeny
        public override Int32 addNewLineZmenyAndPrijmuto(Int32 DBporadi, string DBJK, DateTime DBdatum, Int32 DBprijem, decimal DBcena, string DBpoznamka)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandReadString1 = "SELECT poradi, zustatek from zmeny where parporadi = ? ORDER BY poradi DESC";
                string commandReadString2 = "SELECT ucetkscen, celkcena, ucetstav from naradi where poradi = ? ";
                string commandStringRead3 = "SELECT permission FROM nastaveni WHERE setid = \'prumucetcena\'";
                string commandString1 = "UPDATE naradi set fyzstav = fyzstav + ?, ucetstav = ucetstav + ?, celkcena = ?, cena = ?, ucetkscen = ? where poradi = ? ";
                string commandString2 = "INSERT INTO zmeny (parporadi, pomozjk, datum, poznamka, prijem, vydej, zustatek, zapkarta, vevcislo, pocivc, stav, poradi )" +
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";

                try
                {
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }

                    OdbcCommand cmdr1 = new OdbcCommand(commandReadString1, myDBConn as OdbcConnection);
                    cmdr1.Parameters.AddWithValue("@parporadi", DBporadi).DbType = DbType.Int32;
                    cmdr1.Transaction = transaction;
                    Int32 poradi;
                    Int32 zustatek;

                    OdbcDataReader myReader1 = cmdr1.ExecuteReader();
                    // true osCisloExist
                    if (myReader1.Read() == true)
                    {
                        poradi = myReader1.GetInt32(0) + 1;
                        zustatek = myReader1.GetInt32(1);
                    }
                    else
                    {
                        poradi = 1;
                        zustatek = 0;
                    }
                    myReader1.Close();



                    OdbcCommand cmdr2 = new OdbcCommand(commandReadString2, myDBConn as OdbcConnection);
                    cmdr2.Parameters.AddWithValue("@poradi", DBporadi).DbType = DbType.Int32;
                    cmdr2.Transaction = transaction;

                    decimal celkCena;
                    decimal ucetCenaKs;
                    Int32 ucetstav;

                    OdbcDataReader myReader2 = cmdr2.ExecuteReader();
                    // true osCisloExist
                    if (myReader2.Read() == true)
                    {
                        ucetCenaKs = myReader2.GetDecimal(0);
                        celkCena = myReader2.GetDecimal(1);
                        ucetstav = myReader2.GetInt32(2);
                    }
                    else
                    {
                        myReader2.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Commit();
                        }
                        return -2; // data neexistuji

                    }
                    myReader2.Close();


                    OdbcCommand cmdr3 = new OdbcCommand(commandStringRead3, myDBConn as OdbcConnection);
                    cmdr3.Transaction = transaction;
                    OdbcDataReader myReader3 = cmdr3.ExecuteReader();
                    Boolean prumerUcetCenaEnabled = false;

                    if (myReader3.Read() == true)
                    {
                        string permision = myReader3.GetString(0);
                        myReader3.Close();
                        if (permision == "A")
                        {
                            prumerUcetCenaEnabled = true;
                        }
                    }
                    else
                    {
                        // radka neexistuje
                        myReader3.Close();
                    }


                    // normalni rezim;
                    DBcena = Math.Round(DBcena, 2);
                    if ((ucetCenaKs == 0) && (celkCena == 0))
                    {
                        ucetCenaKs = Math.Round(DBcena, 2); // inicializace ucetni ceny za kus
                    }
                    decimal celkCenaZvyseni = Math.Round((ucetCenaKs * DBprijem), 2);
                    decimal ucetcenaCelkNova = Math.Round((celkCena + celkCenaZvyseni), 2);

                    if (prumerUcetCenaEnabled) // kdyz je pouzita prumerovana cen musime spocitat nove ucetni ceny
                    {
                        celkCenaZvyseni = DBcena * DBprijem;
                        ucetCenaKs = Math.Round(((ucetCenaKs * ucetstav) + celkCenaZvyseni) / (ucetstav + DBprijem), 2);
                        ucetcenaCelkNova = Math.Round((ucetCenaKs * (DBprijem + ucetstav)), 2);
                    }


                    OdbcCommand cmd1 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);

                    cmd1.Parameters.AddWithValue("@fyzstav", DBprijem).DbType = DbType.Int32;
                    cmd1.Parameters.AddWithValue("@ucetstav", DBprijem).DbType = DbType.Int32;
                    cmd1.Parameters.AddWithValue("@celkcena", ucetcenaCelkNova).DbType = DbType.Double;// .DbType = DbType.Double;
                    cmd1.Parameters.AddWithValue("@cena", DBcena).DbType = DbType.Double;//.DbType = DbType.Double;
                    cmd1.Parameters.AddWithValue("@ucetkscen", ucetCenaKs).DbType = DbType.Double;//.DbType = DbType.Double;
                    cmd1.Parameters.AddWithValue("@poradi", DBporadi).DbType = DbType.Int32;

                    cmd1.Transaction = transaction;
                    cmd1.ExecuteNonQuery();


                    OdbcCommand cmd2 = new OdbcCommand(commandString2, myDBConn as OdbcConnection);

                    cmd2.Parameters.AddWithValue("@parporadi", DBporadi).DbType = DbType.Int32;
                    cmd2.Parameters.AddWithValue("@pomozjk", DBJK);
                    cmd2.Parameters.AddWithValue("@datum", DBdatum);
                    cmd2.Parameters.AddWithValue("@poznamka", DBpoznamka);
                    cmd2.Parameters.AddWithValue("@prijem", DBprijem).DbType = DbType.Int32;
                    cmd2.Parameters.AddWithValue("@vydej", 0).DbType = DbType.Int32;
                    cmd2.Parameters.AddWithValue("@zustatek", zustatek + DBprijem).DbType = DbType.Int32;
                    cmd2.Parameters.AddWithValue("@zapkarta", "");
                    cmd2.Parameters.AddWithValue("@vevcislo", "");
                    cmd2.Parameters.AddWithValue("@pocivc", 0).DbType = DbType.Int32;
                    cmd2.Parameters.AddWithValue("@stav", "P");
                    cmd2.Parameters.AddWithValue("@poradi", poradi).DbType = DbType.Int32;

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
                    return -1;
                }
                return 0;
            }
            return 0;
        }



        public override Boolean editNewLineZmeny(Int32 DBParPoradi, Int32 DBPoradi, string DBPoznamka, string DBVevcislo)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandString1 = "UPDATE zmeny SET poznamka =  ?, vevcislo = ? WHERE parporadi = ? AND poradi = ? ";
                try
                {
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }

                    OdbcCommand cmd1 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);
                    cmd1.Parameters.AddWithValue("@poznamka", DBPoznamka);
                    cmd1.Parameters.AddWithValue("@vevcislo", DBVevcislo);
                    cmd1.Parameters.AddWithValue("@parporadi", DBParPoradi).DbType = DbType.Int32;
                    cmd1.Parameters.AddWithValue("@poradi", DBPoradi).DbType = DbType.Int32;
                    cmd1.Transaction = transaction;
                    Int32 errCode = cmd1.ExecuteNonQuery();
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
                return true;
            }
            return false;
        }




        public override Int32 addNewLineZmenyAndVraceno(Int32 DBporadi, DateTime DBdatum, Int32 DBks, string DBpoznamka, string DBosCislo)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {

                string commandReadString1 = "SELECT rtrim(vevcislo) as vevcislo FROM zmeny WHERE poradi = ? AND parporadi = ? ";
                string commandReadString2 = "SELECT nporadi, zporadi, stavks FROM pujceno WHERE poradi = ? ";
                string commandReadString3 = "SELECT poradi, zustatek from zmeny where parporadi = ? ORDER BY poradi DESC";
                string commandReadString4 = "SELECT poradi FROM tabseq WHERE nazev = 'vraceno'";
                string commandReadString5 = "SELECT rtrim(nazev) as nazev, rtrim(jk) as jk, rtrim(rozmer) as rozmer, rtrim(normacsn) as normacsn, cena, celkcena  FROM naradi WHERE poradi = ? ";
                string commandReadString6 = "SELECT jmeno, prijmeni, odeleni, stredisko, pracoviste FROM osoby WHERE oscislo = ? ";

                string commandString1 = "UPDATE naradi SET fyzstav = fyzstav + ? WHERE poradi = ? ";

                string commandString2 = "INSERT INTO zmeny (parporadi, pomozjk, datum, poznamka, prijem, vydej, zustatek, zapkarta, vevcislo, pocivc, stav, poradi )" +
                    "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";

                string commandString3 = "UPDATE pujceno SET stavks = stavks - ? WHERE poradi = ? ";
                string commandString4 = "DELETE FROM pujceno WHERE poradi = ? ";
                string commandString5 = "INSERT INTO vraceno ( poradi, jmeno, oscislo, dilna, pracoviste, vyrobek, nazev, jk, rozmer, pocetks, cena, datum, csn, krjmeno, celkcena, vevcislo, konto) " +
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";
                string commandString6 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'vraceno'";

                try
                {
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }
                    Int32 parPoradi;
                    Int32 zmenPoradi;
                    Int32 pujcKs;


                    // soucasny stav pujceno
                    OdbcCommand cmdr2 = new OdbcCommand(commandReadString2, myDBConn as OdbcConnection);
                    cmdr2.Parameters.AddWithValue("@poradi", DBporadi).DbType = DbType.Int32;
                    cmdr2.Transaction = transaction;
                    OdbcDataReader pujcReader = cmdr2.ExecuteReader();
                    if (pujcReader.Read())
                    {
                        parPoradi = pujcReader.GetInt32(pujcReader.GetOrdinal("nporadi"));
                        zmenPoradi = pujcReader.GetInt32(pujcReader.GetOrdinal("zporadi"));
                        pujcKs = pujcReader.GetInt32(pujcReader.GetOrdinal("stavks"));
                    }
                    else
                    {
                        pujcReader.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -1;  // chyba
                    }
                    pujcReader.Close();

                    if (pujcKs < DBks)
                    {
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -2;  // pozadavek na odepsani vice kusu nez je mozno
                    }

                    string zmenyVevcislo;

                    OdbcCommand cmdr7 = new OdbcCommand(commandReadString1, myDBConn as OdbcConnection);
                    cmdr7.Parameters.AddWithValue("@poradi", zmenPoradi).DbType = DbType.Int32;
                    cmdr7.Parameters.AddWithValue("@parporadi", parPoradi).DbType = DbType.Int32;
                    cmdr7.Transaction = transaction;
                    OdbcDataReader zmenyReader = cmdr7.ExecuteReader();
                    if (zmenyReader.Read())
                    {
                        zmenyVevcislo = zmenyReader.GetString(zmenyReader.GetOrdinal("vevcislo"));
                    }
                    else
                    {
                        zmenyReader.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -1;  // chyba
                    }
                    zmenyReader.Close();



                    string naradiNazev;
                    string naradiJK;
                    string naradiRozmer;
                    string naradiCSN;
                    double naradiCena;
                    double naradiCelkCena;

                    OdbcCommand cmdr5 = new OdbcCommand(commandReadString5, myDBConn as OdbcConnection);
                    cmdr5.Parameters.AddWithValue("@poradi", parPoradi).DbType = DbType.Int32;
                    cmdr5.Transaction = transaction;
                    OdbcDataReader naradiReader = cmdr5.ExecuteReader();
                    if (naradiReader.Read())
                    {
                        naradiNazev = naradiReader.GetString(naradiReader.GetOrdinal("nazev"));
                        naradiJK = naradiReader.GetString(naradiReader.GetOrdinal("jk"));
                        naradiRozmer = naradiReader.GetString(naradiReader.GetOrdinal("rozmer"));
                        naradiCSN = naradiReader.GetString(naradiReader.GetOrdinal("normacsn"));
                        naradiCena = naradiReader.GetDouble(naradiReader.GetOrdinal("cena"));
                        naradiCelkCena = naradiReader.GetDouble(naradiReader.GetOrdinal("celkcena"));
                    }
                    else
                    {
                        naradiReader.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -1;
                    }
                    naradiReader.Close();

                    string osobyJmeno;
                    string osobyPrijmeni;
                    string osobyOddeleni;
                    string osobyPracoviste;
                    string osobyStredisko;


                    OdbcCommand cmdr6 = new OdbcCommand(commandReadString6, myDBConn as OdbcConnection);
                    cmdr6.Parameters.AddWithValue("@oscislo", DBosCislo);
                    cmdr6.Transaction = transaction;
                    OdbcDataReader osobyReader = cmdr6.ExecuteReader();
                    if (osobyReader.Read())
                    {
                        osobyJmeno = osobyReader.GetString(osobyReader.GetOrdinal("jmeno"));
                        osobyPrijmeni = osobyReader.GetString(osobyReader.GetOrdinal("prijmeni"));
                        osobyOddeleni = osobyReader.GetString(osobyReader.GetOrdinal("odeleni"));
                        osobyStredisko = osobyReader.GetString(osobyReader.GetOrdinal("stredisko"));
                        osobyPracoviste = osobyReader.GetString(osobyReader.GetOrdinal("pracoviste"));
                    }
                    else
                    {
                        osobyReader.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -1;
                    }
                    osobyReader.Close();

                    Int32 newZmenyPoradi;
                    Int32 zustatek;
                    // cislo poradi pro novy zaznam a stav podle zmen

                    OdbcCommand cmdr3 = new OdbcCommand(commandReadString3, myDBConn as OdbcConnection);
                    cmdr3.Parameters.AddWithValue("poradi", parPoradi).DbType = DbType.Int32;
                    cmdr3.Transaction = transaction;
                    OdbcDataReader zmenTailReader = cmdr3.ExecuteReader();
                    if (zmenTailReader.Read() == true)
                    {
                        newZmenyPoradi = zmenTailReader.GetInt32(zmenTailReader.GetOrdinal("poradi")) + 1;
                        zustatek = zmenTailReader.GetInt32(zmenTailReader.GetOrdinal("zustatek")); //zmeny.stav - posledni
                        if (zustatek < 0)
                        {
                            zmenTailReader.Close();
                            if (transaction != null)
                            {
                                (transaction as OdbcTransaction).Rollback();
                            }
                            return -4;  // zadne zaznamy ve zmenach
                        }

                    }
                    else
                    {
                        zmenTailReader.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -3;  // zadne zaznamy ve zmenach
                    }
                    zmenTailReader.Close();

                    // tab naradi zvetsi fyz. stav

                    OdbcCommand cmd1 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);
                    cmd1.Parameters.AddWithValue("@fyzstav", DBks).DbType = DbType.Int32;
                    cmd1.Parameters.AddWithValue("@poradi", parPoradi).DbType = DbType.Int32;

                    cmd1.Transaction = transaction;
                    Int32 errCode = cmd1.ExecuteNonQuery();

                    //  tab zmeny novy zaznam
                    OdbcCommand cmd2 = new OdbcCommand(commandString2, myDBConn as OdbcConnection);

                    cmd2.Parameters.AddWithValue("@parporadi", parPoradi).DbType = DbType.Int32;
                    cmd2.Parameters.AddWithValue("@pomozjk", naradiJK);
                    cmd2.Parameters.AddWithValue("@datum", DBdatum);
                    cmd2.Parameters.AddWithValue("@poznamka", DBpoznamka);
                    cmd2.Parameters.AddWithValue("@prijem", DBks).DbType = DbType.Int32;
                    cmd2.Parameters.AddWithValue("@vydej", 0).DbType = DbType.Int32;
                    cmd2.Parameters.AddWithValue("@zustatek", zustatek + DBks).DbType = DbType.Int32;
                    cmd2.Parameters.AddWithValue("@zapkarta", DBosCislo);
                    cmd2.Parameters.AddWithValue("@vevcislo", zmenyVevcislo);
                    cmd2.Parameters.AddWithValue("@pocivc", 0);
                    cmd2.Parameters.AddWithValue("@stav", "R");
                    cmd2.Parameters.AddWithValue("@poradi", newZmenyPoradi).DbType = DbType.Int32;
                    cmd2.Transaction = transaction;
                    errCode = cmd2.ExecuteNonQuery();

                    if (pujcKs != DBks)
                    {
                        // tab pujceno zmena stavu
                        OdbcCommand cmd3 = new OdbcCommand(commandString3, myDBConn as OdbcConnection);
                        cmd3.Parameters.AddWithValue("@stavks", DBks).DbType = DbType.Int32;
                        cmd3.Parameters.AddWithValue("@poradi", DBporadi).DbType = DbType.Int32;
                        cmd3.Transaction = transaction;
                        errCode = cmd3.ExecuteNonQuery();
                    }
                    else
                    {
                        // tab pujceno smazani
                        OdbcCommand cmd4 = new OdbcCommand(commandString4, myDBConn as OdbcConnection);
                        cmd4.Parameters.AddWithValue("@poradi", DBporadi).DbType = DbType.Int32;
                        cmd4.Transaction = transaction;
                        errCode = cmd4.ExecuteNonQuery();
                    }


                    Int32 newVracenoPoradi;
                    // cislo poradi pro novy zaznam

                    OdbcCommand cmdr4 = new OdbcCommand(commandReadString4, myDBConn as OdbcConnection);
                    cmdr4.Transaction = transaction;
                    OdbcDataReader vracNewPoradiReader = cmdr4.ExecuteReader();
                    if (vracNewPoradiReader.Read() == true)
                    {
                        newVracenoPoradi = vracNewPoradiReader.GetInt32(vracNewPoradiReader.GetOrdinal("poradi")) + 1;
                    }
                    else
                    {
                        vracNewPoradiReader.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -1;
                    }
                    vracNewPoradiReader.Close();

                    OdbcCommand cmd5 = new OdbcCommand(commandString5, myDBConn as OdbcConnection);
                    // "INSERT INTO vraceno ( poradi, jmeno, oscislo, dilna, pracoviste, vyrobek, nazev, jk, rozmer, pocetks, cena, datum, csn, krjmeno, celkcena, vevcislo, konto) "
                    cmd5.Parameters.AddWithValue("poradi", newZmenyPoradi).DbType = DbType.Int32;
                    cmd5.Parameters.AddWithValue("jmeno", osobyPrijmeni);
                    cmd5.Parameters.AddWithValue("oscislo", DBosCislo);
                    cmd5.Parameters.AddWithValue("dilna", osobyStredisko);
                    cmd5.Parameters.AddWithValue("pracoviste", osobyOddeleni);
                    cmd5.Parameters.AddWithValue("vyrobek", "");
                    cmd5.Parameters.AddWithValue("nazev", naradiNazev);
                    cmd5.Parameters.AddWithValue("jk", naradiJK);
                    cmd5.Parameters.AddWithValue("rozmer", naradiRozmer);
                    cmd5.Parameters.AddWithValue("pocetks", DBks).DbType = DbType.Int32;
                    cmd5.Parameters.AddWithValue("cena", naradiCena).DbType = DbType.Double;
                    cmd5.Parameters.AddWithValue("datum", DBdatum);
                    cmd5.Parameters.AddWithValue("csn", naradiCSN);
                    cmd5.Parameters.AddWithValue("krjmeno", osobyJmeno);
                    cmd5.Parameters.AddWithValue("celkcena", naradiCelkCena).DbType = DbType.Double;
                    cmd5.Parameters.AddWithValue("vevcislo", zmenyVevcislo);
                    cmd5.Parameters.AddWithValue("konto", "");
                    cmd5.Transaction = transaction;
                    cmd5.ExecuteNonQuery();

                    OdbcCommand cmd6 = new OdbcCommand(commandString6, myDBConn as OdbcConnection);
                    cmd6.Transaction = transaction;
                    cmd6.ExecuteNonQuery();

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



        public override Int32 addNewLineZmenyAndVracenoAndPoskozeno(Int32 DBporadi, DateTime DBdatum, Int32 DBks, string DBpoznamka, string DBosCislo, string DBKonto, string DBcisZak)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {

                string commandReadString0 = "SELECT count(*) as countporadi from poskozeno";
                string commandReadString1 = "SELECT rtrim(vevcislo) as vevcislo FROM zmeny WHERE poradi = ? AND parporadi = ? ";
                string commandReadString2 = "SELECT nporadi, zporadi, stavks FROM pujceno WHERE poradi = ? ";
                string commandReadString3 = "SELECT poradi, zustatek from zmeny where parporadi = ? ORDER BY poradi DESC";
                string commandReadString4 = "SELECT poradi FROM tabseq WHERE nazev = 'vraceno'";
                string commandReadString5 = "SELECT rtrim(nazev) as nazev, rtrim(jk) as jk, rtrim(rozmer) as rozmer, rtrim(normacsn) as normacsn, cena, celkcena, ucetstav  FROM naradi WHERE poradi = ? ";
                string commandReadString6 = "SELECT jmeno, prijmeni, odeleni, stredisko, pracoviste FROM osoby WHERE oscislo = ? ";
                ///------------
//                string commandReadString7 = "SELECT poradi FROM tabseq WHERE nazev = 'poskozeno'";
                string commandReadString7a = "SELECT MAX(poradi) FROM poskozeno";

                string commandString1 = "UPDATE naradi SET ucetstav = ucetstav - ?, celkcena = celkcena - (ucetkscen * ?) WHERE poradi = ? ";

                string commandString2 = "INSERT INTO zmeny (parporadi, pomozjk, datum, poznamka, prijem, vydej, zustatek, zapkarta, vevcislo, pocivc, stav, poradi )" +
                    "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";

                string commandString3 = "UPDATE pujceno SET stavks = stavks - ? WHERE poradi = ? ";
                string commandString4 = "DELETE FROM pujceno WHERE poradi = ? ";
                string commandString5 = "INSERT INTO vraceno ( poradi, jmeno, oscislo, dilna, pracoviste, vyrobek, nazev, jk, rozmer, pocetks, cena, datum, csn, krjmeno, celkcena, vevcislo, konto) " +
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";
                string commandString6 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'vraceno'";

                ///--------------------------------

//                string commandString8 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'poskozeno'";
                string commandString8a = "UPDATE  tabseq set poradi = ? WHERE nazev = 'poskozeno'";
                string commandString9 = "INSERT INTO poskozeno ( poradi, jmeno, oscislo, dilna, pracoviste, vyrobek, nazev, jk, rozmer, pocetks, cena, datum, csn, krjmeno, celkcena, vevcislo, konto) " +
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
                    Int32 parPoradi;
                    Int32 zmenPoradi;
                    Int32 pujcKs;


                    // soucasny stav pujceno
                    OdbcCommand cmdr2 = new OdbcCommand(commandReadString2, myDBConn as OdbcConnection);
                    cmdr2.Parameters.AddWithValue("@poradi", DBporadi).DbType = DbType.Int32;
                    cmdr2.Transaction = transaction;
                    OdbcDataReader pujcReader = cmdr2.ExecuteReader();
                    if (pujcReader.Read())
                    {
                        parPoradi = pujcReader.GetInt32(pujcReader.GetOrdinal("nporadi"));
                        zmenPoradi = pujcReader.GetInt32(pujcReader.GetOrdinal("zporadi"));
                        pujcKs = pujcReader.GetInt32(pujcReader.GetOrdinal("stavks"));
                    }
                    else
                    {
                        pujcReader.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -1;  // chyba
                    }
                    pujcReader.Close();

                    if (pujcKs < DBks)
                    {
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -2;  // pozadavek na odepsani vice kusu nez je mozno
                    }

                    string zmenyVevcislo;

                    OdbcCommand cmdr7 = new OdbcCommand(commandReadString1, myDBConn as OdbcConnection);
                    cmdr7.Parameters.AddWithValue("@poradi", zmenPoradi).DbType = DbType.Int32;
                    cmdr7.Parameters.AddWithValue("@parporadi", parPoradi).DbType = DbType.Int32;
                    cmdr7.Transaction = transaction;
                    OdbcDataReader zmenyReader = cmdr7.ExecuteReader();
                    if (zmenyReader.Read())
                    {
                        zmenyVevcislo = zmenyReader.GetString(zmenyReader.GetOrdinal("vevcislo"));
                    }
                    else
                    {
                        zmenyReader.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -1;  // chyba
                    }
                    zmenyReader.Close();



                    string naradiNazev;
                    string naradiJK;
                    string naradiRozmer;
                    string naradiCSN;
                    double naradiCena;
                    double naradiCelkCena;
                    Int32 naradiUcetStav;

                    OdbcCommand cmdr5 = new OdbcCommand(commandReadString5, myDBConn as OdbcConnection);
                    cmdr5.Parameters.AddWithValue("@poradi", parPoradi).DbType = DbType.Int32;
                    cmdr5.Transaction = transaction;
                    OdbcDataReader naradiReader = cmdr5.ExecuteReader();
                    if (naradiReader.Read())
                    {
                        naradiNazev = naradiReader.GetString(naradiReader.GetOrdinal("nazev"));
                        naradiJK = naradiReader.GetString(naradiReader.GetOrdinal("jk"));
                        naradiRozmer = naradiReader.GetString(naradiReader.GetOrdinal("rozmer"));
                        naradiCSN = naradiReader.GetString(naradiReader.GetOrdinal("normacsn"));
                        naradiCena = naradiReader.GetDouble(naradiReader.GetOrdinal("cena"));
                        naradiCelkCena = naradiReader.GetDouble(naradiReader.GetOrdinal("celkcena"));
                        naradiUcetStav = naradiReader.GetInt32(naradiReader.GetOrdinal("ucetstav"));
                        if (naradiUcetStav < DBks)
                        {
                            // nemohu odepsat vice nez je ucetni stav
                            naradiReader.Close();
                            if (transaction != null)
                            {
                                (transaction as OdbcTransaction).Rollback();
                            }
                            return -3;
                        }
                    }
                    else
                    {
                        naradiReader.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -1;
                    }
                    naradiReader.Close();

                    string osobyJmeno;
                    string osobyPrijmeni;
                    string osobyOddeleni;
                    string osobyPracoviste;
                    string osobyStredisko;


                    OdbcCommand cmdr6 = new OdbcCommand(commandReadString6, myDBConn as OdbcConnection);
                    cmdr6.Parameters.AddWithValue("@oscislo", DBosCislo);
                    cmdr6.Transaction = transaction;
                    OdbcDataReader osobyReader = cmdr6.ExecuteReader();
                    if (osobyReader.Read())
                    {
                        osobyJmeno = osobyReader.GetString(osobyReader.GetOrdinal("jmeno"));
                        osobyPrijmeni = osobyReader.GetString(osobyReader.GetOrdinal("prijmeni"));
                        osobyOddeleni = osobyReader.GetString(osobyReader.GetOrdinal("odeleni"));
                        osobyStredisko = osobyReader.GetString(osobyReader.GetOrdinal("stredisko"));
                        osobyPracoviste = osobyReader.GetString(osobyReader.GetOrdinal("pracoviste"));
                    }
                    else
                    {
                        osobyReader.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -1;
                    }
                    osobyReader.Close();

                    Int32 newZmenyPoradi;
                    Int32 zustatek;
                    // cislo poradi pro novy zaznam a stav podle zmen

                    //commandReadString3 = "SELECT poradi, zustatek from zmeny where parporadi = ? ORDER BY poradi DESC";
                    OdbcCommand cmdr3 = new OdbcCommand(commandReadString3, myDBConn as OdbcConnection);
                    cmdr3.Parameters.AddWithValue("poradi", parPoradi).DbType = DbType.Int32;
                    cmdr3.Transaction = transaction;
                    OdbcDataReader zmenTailReader = cmdr3.ExecuteReader();
                    if (zmenTailReader.Read() == true)
                    {
                        newZmenyPoradi = zmenTailReader.GetInt32(zmenTailReader.GetOrdinal("poradi")) + 1;
                        zustatek = zmenTailReader.GetInt32(zmenTailReader.GetOrdinal("zustatek")); //zmeny.stav - posledni
                        if (zustatek < 0)
                        {
                            zmenTailReader.Close();
                            if (transaction != null)
                            {
                                (transaction as OdbcTransaction).Rollback();
                            }
                            return -4;  // zadne zaznamy ve zmenach
                        }

                    }
                    else
                    {
                        zmenTailReader.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -5;  // zadne zaznamy ve zmenach
                    }
                    zmenTailReader.Close();



                    Int32 newVracenoPoradi;
                    // cislo poradi pro novy zaznam

                    OdbcCommand cmdr4 = new OdbcCommand(commandReadString4, myDBConn as OdbcConnection);
                    cmdr4.Transaction = transaction;
                    OdbcDataReader vracNewPoradiReader = cmdr4.ExecuteReader();
                    if (vracNewPoradiReader.Read() == true)
                    {
                        newVracenoPoradi = vracNewPoradiReader.GetInt32(vracNewPoradiReader.GetOrdinal("poradi")) + 1;
                    }
                    else
                    {
                        vracNewPoradiReader.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -1;
                    }
                    vracNewPoradiReader.Close();


                    Int32 newPoskozenoPoradi;
                    // cislo poradi pro novy zaznam

                    OdbcCommand cmd0 = new OdbcCommand(commandReadString0, myDBConn as OdbcConnection);
                    cmd0.Transaction = transaction;
                    OdbcDataReader myReader0 = cmd0.ExecuteReader();
                    if (myReader0.Read() == true)
                    {
                        Int32 countporadi = myReader0.GetInt32(0);
                        myReader0.Close();

                        if (countporadi == 0) newPoskozenoPoradi = 1;
                        else
                        {

                            OdbcCommand cmdr8 = new OdbcCommand(commandReadString7a, myDBConn as OdbcConnection);
                            cmdr8.Transaction = transaction;
                            OdbcDataReader vracNewPoskPoradiReader = cmdr8.ExecuteReader();
                            if (vracNewPoskPoradiReader.Read() == true)
                            {
                                //                        newPoskozenoPoradi = vracNewPoskPoradiReader.GetInt32(vracNewPoskPoradiReader.GetOrdinal("poradi")) + 1;
                                newPoskozenoPoradi = vracNewPoskPoradiReader.GetInt32(0) + 1;
                            }
                            else
                            {
                                vracNewPoskPoradiReader.Close();
                                if (transaction != null)
                                {
                                    (transaction as OdbcTransaction).Rollback();
                                }
                                return -1;
                            }
                            vracNewPoskPoradiReader.Close();
                        }
                    }
                    else
                    {
                        myReader0.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -1;
                    }

                    // tab naradi zmensi ucet. stav

                    OdbcCommand cmd1 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);
                    cmd1.Parameters.AddWithValue("@ucetstav", DBks).DbType = DbType.Int32;
                    cmd1.Parameters.AddWithValue("@celkcena", DBks).DbType = DbType.Int32;
                    cmd1.Parameters.AddWithValue("@poradi", parPoradi).DbType = DbType.Int32;

                    cmd1.Transaction = transaction;
                    Int32 errCode = cmd1.ExecuteNonQuery();

                    //  tab zmeny novy zaznam
                    OdbcCommand cmd2 = new OdbcCommand(commandString2, myDBConn as OdbcConnection);

                    cmd2.Parameters.AddWithValue("@parporadi", parPoradi).DbType = DbType.Int32;
                    cmd2.Parameters.AddWithValue("@pomozjk", naradiJK);
                    cmd2.Parameters.AddWithValue("@datum", DBdatum);
                    cmd2.Parameters.AddWithValue("@poznamka", DBpoznamka);
                    cmd2.Parameters.AddWithValue("@prijem", DBks).DbType = DbType.Int32;
                    cmd2.Parameters.AddWithValue("@vydej", 0).DbType = DbType.Int32;
                    cmd2.Parameters.AddWithValue("@zustatek", zustatek + DBks).DbType = DbType.Int32;
                    cmd2.Parameters.AddWithValue("@zapkarta", DBosCislo);
                    cmd2.Parameters.AddWithValue("@vevcislo", zmenyVevcislo);
                    cmd2.Parameters.AddWithValue("@pocivc", 0);
                    cmd2.Parameters.AddWithValue("@stav", "R");
                    cmd2.Parameters.AddWithValue("@poradi", newZmenyPoradi);
                    cmd2.Transaction = transaction;
                    errCode = cmd2.ExecuteNonQuery();

                    if (pujcKs != DBks)
                    {
                        // tab pujceno zmena stavu
                        OdbcCommand cmd3 = new OdbcCommand(commandString3, myDBConn as OdbcConnection);
                        cmd3.Parameters.AddWithValue("@stavks", DBks).DbType = DbType.Int32;
                        cmd3.Parameters.AddWithValue("@poradi", DBporadi).DbType = DbType.Int32;
                        cmd3.Transaction = transaction;
                        errCode = cmd3.ExecuteNonQuery();
                    }
                    else
                    {
                        // tab pujceno smazani
                        OdbcCommand cmd4 = new OdbcCommand(commandString4, myDBConn as OdbcConnection);
                        cmd4.Parameters.AddWithValue("@poradi", DBporadi).DbType = DbType.Int32;
                        cmd4.Transaction = transaction;
                        errCode = cmd4.ExecuteNonQuery();
                    }


                    // pridani do tabulky vraceno
                    OdbcCommand cmd5 = new OdbcCommand(commandString5, myDBConn as OdbcConnection);
                    cmd5.Parameters.AddWithValue("poradi", newVracenoPoradi).DbType = DbType.Int32;
                    cmd5.Parameters.AddWithValue("jmeno", osobyPrijmeni);
                    cmd5.Parameters.AddWithValue("oscislo", DBosCislo);
                    cmd5.Parameters.AddWithValue("dilna", osobyStredisko);
                    cmd5.Parameters.AddWithValue("pracoviste", osobyOddeleni);
                    cmd5.Parameters.AddWithValue("vyrobek", DBcisZak);
                    cmd5.Parameters.AddWithValue("nazev", naradiNazev);
                    cmd5.Parameters.AddWithValue("jk", naradiJK);
                    cmd5.Parameters.AddWithValue("rozmer", naradiRozmer);
                    cmd5.Parameters.AddWithValue("pocetks", DBks).DbType = DbType.Int32;
                    cmd5.Parameters.AddWithValue("cena", naradiCena).DbType = DbType.Double;
                    cmd5.Parameters.AddWithValue("datum", DBdatum);
                    cmd5.Parameters.AddWithValue("csn", naradiCSN);
                    cmd5.Parameters.AddWithValue("krjmeno", osobyJmeno);
                    cmd5.Parameters.AddWithValue("celkcena", naradiCelkCena).DbType = DbType.Double;
                    cmd5.Parameters.AddWithValue("vevcislo", zmenyVevcislo);
                    cmd5.Parameters.AddWithValue("konto", DBKonto);
                    cmd5.Transaction = transaction;
                    cmd5.ExecuteNonQuery();

                    OdbcCommand cmd6 = new OdbcCommand(commandString6, myDBConn as OdbcConnection);
                    cmd6.Transaction = transaction;
                    cmd6.ExecuteNonQuery();


                    //  tab zmeny novy zaznam pro zmeny - poskozeno
                    OdbcCommand cmd7 = new OdbcCommand(commandString2, myDBConn as OdbcConnection);
                    cmd7.Parameters.AddWithValue("@parporadi", parPoradi).DbType = DbType.Int32;
                    cmd7.Parameters.AddWithValue("@pomozjk", naradiJK);
                    cmd7.Parameters.AddWithValue("@datum", DBdatum);
                    cmd7.Parameters.AddWithValue("@poznamka", DBpoznamka);
                    cmd7.Parameters.AddWithValue("@prijem", 0);
                    cmd7.Parameters.AddWithValue("@vydej", DBks).DbType = DbType.Int32;
                    cmd7.Parameters.AddWithValue("@zustatek", zustatek).DbType = DbType.Int32; // jen zustatek
                    cmd7.Parameters.AddWithValue("@zapkarta", DBosCislo);
                    cmd7.Parameters.AddWithValue("@vevcislo", zmenyVevcislo);
                    cmd7.Parameters.AddWithValue("@pocivc", 0);
                    cmd7.Parameters.AddWithValue("@stav", "O");
                    cmd7.Parameters.AddWithValue("@poradi", newZmenyPoradi + 1).DbType = DbType.Int32;
                    cmd7.Transaction = transaction;
                    errCode = cmd7.ExecuteNonQuery();


                    // pridani do tabulky poskozeno
                    OdbcCommand cmd8 = new OdbcCommand(commandString9, myDBConn as OdbcConnection);
                    cmd8.Parameters.AddWithValue("@pporadi", newPoskozenoPoradi).DbType = DbType.Int32;
                    cmd8.Parameters.AddWithValue("@jmeno", osobyPrijmeni);
                    cmd8.Parameters.AddWithValue("@oscislo", DBosCislo);
                    cmd8.Parameters.AddWithValue("@dilna", osobyStredisko);
                    cmd8.Parameters.AddWithValue("@pracoviste", osobyOddeleni);
                    cmd8.Parameters.AddWithValue("@vyrobek", DBcisZak);
                    cmd8.Parameters.AddWithValue("@nazev", naradiNazev);
                    cmd8.Parameters.AddWithValue("@jk", naradiJK);
                    cmd8.Parameters.AddWithValue("@rozmer", naradiRozmer);
                    cmd8.Parameters.AddWithValue("@pocetks", DBks).DbType = DbType.Int32;
                    cmd8.Parameters.AddWithValue("@cena", naradiCena).DbType = DbType.Double;
                    cmd8.Parameters.AddWithValue("@datum", DBdatum);
                    cmd8.Parameters.AddWithValue("@csn", naradiCSN);
                    cmd8.Parameters.AddWithValue("@krjmeno", osobyJmeno);
                    cmd8.Parameters.AddWithValue("@celkcena", naradiCelkCena).DbType = DbType.Double;
                    cmd8.Parameters.AddWithValue("@vevcislo", zmenyVevcislo);
                    cmd8.Parameters.AddWithValue("@konto", DBKonto);
                    cmd8.Transaction = transaction;
                    errCode = cmd8.ExecuteNonQuery();

                    OdbcCommand cmd9 = new OdbcCommand(commandString8a, myDBConn as OdbcConnection);
                    cmd9.Parameters.AddWithValue("@poradi", newPoskozenoPoradi + 1).DbType = DbType.Int32;
                    cmd9.Transaction = transaction;
                    cmd9.ExecuteNonQuery();

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



        public override Int32 addNewLineZmenyAndPujceno(Int32 DBparPoradi, DateTime DBdatum, Int32 DBks, string DBpoznamka, string DBvevCislo, string DBosCislo)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandReadString1 = "SELECT poradi, zustatek from zmeny where parporadi = ? ORDER BY poradi DESC";
                string commandReadString2 = "SELECT fyzstav from naradi where poradi = ? ";
                string commandReadString3 = "SELECT poradi FROM tabseq WHERE nazev = 'pujceno'";

                string commandReadString5 = "SELECT rtrim(nazev) as nazev, rtrim(jk) as jk, cena  FROM naradi WHERE poradi = ? ";
                string commandReadString6 = "SELECT jmeno, prijmeni FROM osoby WHERE oscislo = ? ";


                string commandString1 = "UPDATE naradi set fyzstav = fyzstav - ?  where poradi = ? ";
                string commandString2 = "INSERT INTO zmeny (parporadi, pomozjk, datum, poznamka, prijem, vydej, zustatek, zapkarta, vevcislo, pocivc, stav, poradi )" +
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";

                string commandString3 = "INSERT INTO pujceno ( poradi, oscislo, nporadi, zporadi, pjmeno, pprijmeni, pnazev, pjk, pdatum, pks, pcena, stavks )" +
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";

                string commandString4 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'pujceno'";

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

                    OdbcCommand cmdr2 = new OdbcCommand(commandReadString2, myDBConn as OdbcConnection);
                    cmdr2.Parameters.AddWithValue("@poradi", DBparPoradi).DbType = DbType.Int32;
                    cmdr2.Transaction = transaction;
                    OdbcDataReader seqReader2 = cmdr2.ExecuteReader();
                    int fyzstav = 0;

                    if (seqReader2.Read() == true)
                    {
                        fyzstav = seqReader2.GetInt32(0); // naradi.fyzstav
                    }
                    else
                    {
                        seqReader2.Close();
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
                        seqReader2.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -2;
                    }
                    seqReader2.Close();


                    Int32 poradi;
                    Int32 zustatek;

                    OdbcCommand cmdr1 = new OdbcCommand(commandReadString1, myDBConn as OdbcConnection);
                    cmdr1.Parameters.AddWithValue("@parporadi", DBparPoradi).DbType = DbType.Int32;
                    cmdr1.Transaction = transaction;
                    OdbcDataReader seqReader1 = cmdr1.ExecuteReader();
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

                    string osobyJmeno;
                    string osobyPrijmeni;

                    OdbcCommand cmdr6 = new OdbcCommand(commandReadString6, myDBConn as OdbcConnection);
                    cmdr6.Parameters.AddWithValue("@oscislo", DBosCislo);
                    cmdr6.Transaction = transaction;
                    OdbcDataReader osobyReader = cmdr6.ExecuteReader();
                    if (osobyReader.Read())
                    {
                        osobyJmeno = osobyReader.GetString(osobyReader.GetOrdinal("jmeno"));
                        osobyPrijmeni = osobyReader.GetString(osobyReader.GetOrdinal("prijmeni"));
                    }
                    else
                    {
                        osobyReader.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -1;
                    }
                    osobyReader.Close();

                    string naradiNazev;
                    string naradiJK;
                    double naradiCena;

                    OdbcCommand cmdr5 = new OdbcCommand(commandReadString5, myDBConn as OdbcConnection);
                    cmdr5.Parameters.AddWithValue("@poradi", DBparPoradi).DbType = DbType.Int32;
                    cmdr5.Transaction = transaction;
                    OdbcDataReader naradiReader = cmdr5.ExecuteReader();
                    if (naradiReader.Read())
                    {
                        naradiNazev = naradiReader.GetString(naradiReader.GetOrdinal("nazev"));
                        naradiJK = naradiReader.GetString(naradiReader.GetOrdinal("jk"));
                        naradiCena = naradiReader.GetDouble(naradiReader.GetOrdinal("cena"));
                    }
                    else
                    {
                        naradiReader.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -1;
                    }
                    naradiReader.Close();


                    // zjisti poradi pro pujceno
                    OdbcCommand cmdSeq2 = new OdbcCommand(commandReadString3, myDBConn as OdbcConnection);
                    cmdSeq2.Transaction = transaction;
                    OdbcDataReader seqReader3 = cmdSeq2.ExecuteReader();
                    seqReader3.Read();
                    pujcPoradi = seqReader3.GetInt32(0);
                    seqReader3.Close();

                    // tab naradi

                    OdbcCommand cmd1 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);
                    cmd1.Parameters.AddWithValue("@fyzstav", DBks).DbType = DbType.Int32;
                    cmd1.Parameters.AddWithValue("@poradi", DBparPoradi).DbType = DbType.Int32;
                    cmd1.Transaction = transaction;
                    cmd1.ExecuteNonQuery();

                    //  tab zmeny
                    OdbcCommand cmd2 = new OdbcCommand(commandString2, myDBConn as OdbcConnection);
                    // "INSERT INTO zmeny (parporadi, pomozjk, datum, poznamka, prijem, vydej, zustatek, zapkarta, vevcislo, pocivc, stav, poradi )" +
                    cmd2.Parameters.AddWithValue("@parporadi", DBparPoradi).DbType = DbType.Int32;
                    cmd2.Parameters.AddWithValue("@pomozjk", naradiJK);
                    cmd2.Parameters.AddWithValue("@datum", DBdatum);
                    cmd2.Parameters.AddWithValue("@poznamka", DBpoznamka);
                    cmd2.Parameters.AddWithValue("@prijem", 0).DbType = DbType.Int32;
                    cmd2.Parameters.AddWithValue("@vydej", DBks).DbType = DbType.Int32;
                    cmd2.Parameters.AddWithValue("@zustatek", zustatek - DBks).DbType = DbType.Int32;
                    cmd2.Parameters.AddWithValue("@zapkarta", DBosCislo);
                    cmd2.Parameters.AddWithValue("@vevcislo", DBvevCislo);
                    cmd2.Parameters.AddWithValue("@pocivc", 0);
                    cmd2.Parameters.AddWithValue("@stav", "U");
                    cmd2.Parameters.AddWithValue("@poradi", poradi).DbType = DbType.Int32;
                    cmd2.Transaction = transaction;
                    cmd2.ExecuteNonQuery();

                    //pujceno
                    // poradi, oscislo, nporadi, zporadi, pjmeno, pprijmeni, pnazev, pjk, pdatum, pks, pcena, stavks
                    OdbcCommand cmd = new OdbcCommand(commandString3, myDBConn as OdbcConnection);
                    cmd.Parameters.AddWithValue("@poradi", pujcPoradi).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@oscislo", DBosCislo);
                    cmd.Parameters.AddWithValue("@nporadi", DBparPoradi).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@zporadi", poradi).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@pjmeno", osobyJmeno);
                    cmd.Parameters.AddWithValue("@pprijmeni", osobyPrijmeni);
                    cmd.Parameters.AddWithValue("@pnazev", naradiNazev);
                    cmd.Parameters.AddWithValue("@pjk", naradiJK);
                    cmd.Parameters.AddWithValue("@pdatum", DBdatum);
                    cmd.Parameters.AddWithValue("@pks", DBks).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@pcena", naradiCena).DbType = DbType.Double;
                    cmd.Parameters.AddWithValue("@stavks", DBks).DbType = DbType.Int32;

                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();

                    OdbcCommand cmdSeq3 = new OdbcCommand(commandString4, myDBConn as OdbcConnection);
                    cmdSeq3.Transaction = transaction;
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
                return pujcPoradi;
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


        public override Int32 editNewLineZnackaNaradi(Int32 poradi, string DBKodd)
        {
            string commandString2 = "UPDATE naradi set kodd = ? where  poradi = ?";

            OdbcTransaction transaction = null;
            if (DBIsOpened())
            {
                try
                {
                    transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                    OdbcCommand cmd = new OdbcCommand(commandString2, myDBConn as OdbcConnection);

                    cmd.Parameters.AddWithValue("@kodd", DBKodd);
                    cmd.Parameters.AddWithValue("@poradi", poradi).DbType = DbType.Int32;

                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Commit();
                    }
                    return 0;

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
            return -0;
        }



        public override Boolean moveNaradiToNewKaret(Int32 DBporadi)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandReadString1 = "select oscislo from pujceno where nporadi = ?";
                string commandString2 = "DELETE FROM naradi  where poradi = ? ";
                string commandString1 = "INSERT INTO karta ( poradi, nazev, jk, normacsn, normadin, vyrobce, cena, poznamka, minimum, celkcena, ucetstav, fyzstav, rozmer, analucet, tdate, stredisko, druh, odpis ) " +
                      "SELECT poradi, nazev, jk, normacsn, normadin, vyrobce, cena, poznamka, minimum, celkcena, ucetstav, fyzstav, rozmer, analucet, tdate, stredisko, druh, odpis FROM naradi WHERE poradi = ?";


                try
                {
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }


                    OdbcCommand cmdr1 = new OdbcCommand(commandReadString1, myDBConn as OdbcConnection);
                    cmdr1.Parameters.AddWithValue("@poradi", DBporadi).DbType = DbType.Int32;
                    cmdr1.Transaction = transaction;
                    OdbcDataReader pujcReader = cmdr1.ExecuteReader();
                    if (pujcReader.Read()) // nalezeno v seznamu pujcenych
                    {
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return false;  // chyba
                    }



                    OdbcCommand cmd1 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);
                    OdbcParameter p1 = new OdbcParameter("p1", DbType.Int32);
                    p1.Value = DBporadi;
                    cmd1.Parameters.Add(p1);

                    cmd1.Transaction = transaction;
                    cmd1.ExecuteNonQuery();



                    OdbcCommand cmd2 = new OdbcCommand(commandString2, myDBConn as OdbcConnection);
                    OdbcParameter p2 = new OdbcParameter("p2", DbType.Int32);
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



        public override Int32 addNewLineZmenyAndPoskozeno(Int32 DBporadi, DateTime DBdatum, Int32 DBvydej, string DBpoznamka,
                                                          string osCislo, string DBjmeno, string DBprijmeni, string DBstredisko, string DBprovoz,
                                                          string DBkonto, double DBcena, double DBcelkCena, string DBcisZak)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandStringRead0 = "SELECT count(*) as countporadi from poskozeno";
                string commandStringRead1 = "SELECT poradi, zustatek from zmeny where parporadi = ? ORDER BY poradi DESC";
                string commandStringRead2 = "SELECT fyzstav, ucetstav, ucetkscen, rozmer, nazev, jk, normacsn FROM naradi where poradi = ? ";
//                string commandStringRead3 = "SELECT poradi FROM tabseq WHERE nazev = \'poskozeno\'";
                string commandStringRead3a = "SELECT MAX(poradi) FROM poskozeno";


                string commandString1 = "UPDATE naradi set fyzstav = fyzstav - ?, ucetstav = ucetstav - ?, celkcena = celkcena - (ucetkscen * ?)  where poradi = ? ";
                string commandString2 = "INSERT INTO zmeny (parporadi, pomozjk, datum, poznamka, prijem, vydej, zustatek, zapkarta, vevcislo, pocivc, stav, poradi )" +
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";
//                string commandString4 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'poskozeno'";
                string commandString4a = "UPDATE  tabseq set poradi = ? WHERE nazev = 'poskozeno'";

                string commandString5 = "INSERT INTO poskozeno ( poradi, jmeno, oscislo, dilna, pracoviste, vyrobek, nazev, jk, rozmer, pocetks, cena, datum, csn, krjmeno, celkcena, vevcislo, konto) " +
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";

                string commandString6 = "UPDATE naradi set celkcena = 0 where poradi = ? AND celkcena < 0";


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
                    // true fyzstav exist -- zaznam mohl byt mezitim smazan
                    if (myReader2.Read() == true)
                    {
                        Int32 fyzstav = myReader2.GetInt32(myReader2.GetOrdinal("fyzstav"));
                        Int32 ucetstav = myReader2.GetInt32(myReader2.GetOrdinal("ucetstav"));
                        decimal ucetkscen = myReader2.GetDecimal(myReader2.GetOrdinal("ucetkscen"));
                        string rozmer = myReader2.GetString(myReader2.GetOrdinal("rozmer"));
                        string nazev = myReader2.GetString(myReader2.GetOrdinal("nazev"));
                        string jk = myReader2.GetString(myReader2.GetOrdinal("jk"));
                        string csn = myReader2.GetString(myReader2.GetOrdinal("normacsn"));

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
                            poradi = myReader.GetInt32(0) + 1; // zjistime nove poradi - vraci nejvyssi poradi
                            zustatek = myReader.GetInt32(1);
                        }
                        else
                        {
                            poradi = 1;
                            zustatek = fyzstav;
                        }
                        myReader.Close();

                        // zjisteni   poradi pro tabulku poskozeneho naradi

                        OdbcCommand cmd0 = new OdbcCommand(commandStringRead0, myDBConn as OdbcConnection);
                        cmd0.Transaction = transaction;
                        OdbcDataReader myReader0 = cmd0.ExecuteReader();
                        Int32 poradiPoskozeno;
                        if (myReader0.Read() == true)
                        {
                            Int32 countporadi = myReader0.GetInt32(0);
                            myReader0.Close();
                            if (countporadi == 0) poradiPoskozeno = 1;
                            else
                            {
                                OdbcCommand cmdSeq1 = new OdbcCommand(commandStringRead3a, myDBConn as OdbcConnection);
                                cmdSeq1.Transaction = transaction;
                                OdbcDataReader seqReader = cmdSeq1.ExecuteReader();
                                if (seqReader.Read() == true)
                                {
                                    //                        Int32 poradiPoskozeno = seqReader.GetInt32(0);
                                    poradiPoskozeno = seqReader.GetInt32(0) + 1;
                                    seqReader.Close();
                                }
                                else
                                {
                                    seqReader.Close();
                                    if (transaction != null)
                                    {
                                        (transaction as OdbcTransaction).Rollback();
                                    }
                                    return -1;
                                }
                            }
                        }
                        else
                        {
                            myReader0.Close();
                            if (transaction != null)
                            {
                                (transaction as OdbcTransaction).Rollback();
                            }
                            return -1;
                        }
                        OdbcCommand cmd1 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);
                        cmd1.Parameters.AddWithValue("@fyzstav", DBvydej).DbType = DbType.Int32;
                        cmd1.Parameters.AddWithValue("@ucetstav", DBvydej).DbType = DbType.Int32;
                        cmd1.Parameters.AddWithValue("@celkcena", DBvydej).DbType = DbType.Double;
                        cmd1.Parameters.AddWithValue("@poradi", DBporadi).DbType = DbType.Int32;
                        cmd1.Transaction = transaction;
                        cmd1.ExecuteNonQuery();

                        // opravi pripadne zaporny stav celkove ceny
                        OdbcCommand cmd6 = new OdbcCommand(commandString6, myDBConn as OdbcConnection);
                        cmd6.Parameters.AddWithValue("@poradi", DBporadi).DbType = DbType.Int32;
                        cmd6.Transaction = transaction;
                        cmd6.ExecuteNonQuery();


                        OdbcCommand cmd2 = new OdbcCommand(commandString2, myDBConn as OdbcConnection);

                        cmd2.Parameters.AddWithValue("@parporadi", DBporadi).DbType = DbType.Int32;
                        cmd2.Parameters.AddWithValue("@pomozjk", jk);
                        cmd2.Parameters.AddWithValue("@datum", DBdatum);
                        cmd2.Parameters.AddWithValue("@poznamka", DBpoznamka);
                        cmd2.Parameters.AddWithValue("@prijem", 0).DbType = DbType.Int32;
                        cmd2.Parameters.AddWithValue("@vydej", DBvydej).DbType = DbType.Int32;
                        cmd2.Parameters.AddWithValue("@zustatek", zustatek - DBvydej).DbType = DbType.Int32;
                        cmd2.Parameters.AddWithValue("@zapkarta", osCislo);
                        cmd2.Parameters.AddWithValue("@vevcislo", "");
                        cmd2.Parameters.AddWithValue("@pocivc", 0);
                        cmd2.Parameters.AddWithValue("@stav", "O");
                        cmd2.Parameters.AddWithValue("@poradi", poradi).DbType = DbType.Int32;

                        cmd2.Transaction = transaction;
                        cmd2.ExecuteNonQuery();

                        // pridani radku do tabulky zruseneho materialu
                        OdbcCommand cmd3 = new OdbcCommand(commandString5, myDBConn as OdbcConnection);
                        //  string commandString5 = "INSERT INTO poskozeno ( poradi, jmeno, oscislo, dilna, pracoviste, vyrobek, nazev, jk, rozmer,
                        //pocetks, cena, datum, csn, krjmeno, celkcena, vevcislo, konto) " +
                        cmd3.Parameters.AddWithValue("@poradi", poradiPoskozeno).DbType = DbType.Int32;
                        cmd3.Parameters.AddWithValue("@jmeno", DBprijmeni);
                        cmd3.Parameters.AddWithValue("@oscislo", osCislo);
                        cmd3.Parameters.AddWithValue("@dilna", DBstredisko);
                        cmd3.Parameters.AddWithValue("@pracoviste", DBprovoz);
                        cmd3.Parameters.AddWithValue("@vyrobek", DBcisZak);
                        cmd3.Parameters.AddWithValue("@nazev", nazev);
                        cmd3.Parameters.AddWithValue("@jk", jk);
                        cmd3.Parameters.AddWithValue("@rozmer", rozmer);
                        cmd3.Parameters.AddWithValue("@pocetks", DBvydej).DbType = DbType.Int32;
                        cmd3.Parameters.AddWithValue("@cena", DBcena).DbType = DbType.Double;
                        cmd3.Parameters.AddWithValue("@datum", DBdatum);
                        cmd3.Parameters.AddWithValue("@csn", csn);
                        cmd3.Parameters.AddWithValue("@krjmeno", DBjmeno);
                        //                        cmd3.Parameters.AddWithValue("@celkcena", DBcelkCena);
                        cmd3.Parameters.AddWithValue("@celkcena", ucetkscen * DBvydej).DbType = DbType.Double;
                        cmd3.Parameters.AddWithValue("@vevcislo", "");
                        cmd3.Parameters.AddWithValue("@konto", DBkonto);

                        cmd3.Transaction = transaction;
                        cmd3.ExecuteNonQuery();

                        OdbcCommand cmd4 = new OdbcCommand(commandString4a, myDBConn as OdbcConnection);
                        cmd4.Parameters.AddWithValue("@poradi", poradiPoskozeno + 1).DbType = DbType.Int32;
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


        public override Boolean deleteLinePoskozene(Int32 poradi)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandString1 = "DELETE FROM poskozeno WHERE poradi = ? ";

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
                    cmd1.Parameters.AddWithValue("@poradi", poradi).DbType = DbType.Int32;
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


        public override Boolean deleteLineVracene(Int32 poradi)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandString1 = "DELETE FROM vraceno WHERE poradi = ? ";

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
                    cmd1.Parameters.AddWithValue("@poradi", poradi).DbType = DbType.Int32;
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
            string commandString = "INSERT INTO pujceno ( poradi, oscislo, nporadi, zporadi, stavks, pjmeno, pprijmeni, pnazev, pjk, pdatum, pks, pcena )" +
                  "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";

            string commandStringSeq1 = "SELECT poradi FROM tabseq WHERE nazev = 'pujceno'";
            string commandStringSeq2 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'pujceno'";
            //            string commandStringSeq3 = "SELECT poradi FROM zmeny WHERE parporadi = ? AND zapkarta = ? AND stav = 'U' AND datum = ? ";
            string commandStringSeq3 = "SELECT poradi FROM zmeny WHERE parporadi = ? AND stav = 'U' AND zapkarta = ?";


            if (DBIsOpened())
            {

                try
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
                        seqReader2.Close();
                        MessageBox.Show("Neexistuje zmena stavu pro vypujcení. Poradi/Nazev:" + DBparPoradi.ToString() + " - " + DBnazev + " OSCislo: " + DBosCislo + " - " + DBPrijmeni);
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
                    OdbcParameter p4 = new OdbcParameter("p4", OdbcType.Int);
                    p4.Value = DBks;
                    OdbcParameter p5 = new OdbcParameter("p5", OdbcType.NChar);
                    p5.Value = DBjmeno;
                    OdbcParameter p6 = new OdbcParameter("p6", OdbcType.NChar);
                    p6.Value = DBPrijmeni;
                    OdbcParameter p7 = new OdbcParameter("p7", OdbcType.NChar);
                    p7.Value = DBnazev;
                    OdbcParameter p8 = new OdbcParameter("p8", OdbcType.NChar);
                    p8.Value = DBjk;
                    OdbcParameter p9 = new OdbcParameter("p9", OdbcType.Date);
                    p9.Value = DBdatum;
                    OdbcParameter p10 = new OdbcParameter("p10", OdbcType.Int);
                    p10.Value = DBks;
                    OdbcParameter p11 = new OdbcParameter("p11", OdbcType.Double);
                    p11.Value = DBks;

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

                    OdbcCommand cmdSeq2 = new OdbcCommand(commandStringSeq2, myDBConn as OdbcConnection);
                    cmdSeq2.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Chyba:" + DBparPoradi.ToString() + " - " + DBnazev + " OSCislo: " + DBosCislo + " - " + DBPrijmeni);
                }
            }
        }


        public override Int32 addNewLineUzivatele(string DBuserid, string DBpasswdHash, string DBjmeno, string DBprijmeni, string DBpermission,
                       Boolean DBadmin)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandStringRead1 = "SELECT userid FROM uzivatele WHERE userid = ? ";
                string commandString1 = "INSERT INTO uzivatele (userid, password, jmeno, prijmeni, admin, permission )" +
                      "VALUES ( ?, ?, ?, ?, ?, ? )";

                try
                {
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }

                    OdbcCommand cmdr1 = new OdbcCommand(commandStringRead1, myDBConn as OdbcConnection);
                    cmdr1.Transaction = transaction;
                    cmdr1.Parameters.AddWithValue("@userid", DBuserid);
                    OdbcDataReader myReader = cmdr1.ExecuteReader();
                    // true osCisloExist
                    if (myReader.Read() == true)
                    {
                        myReader.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -2; // uzivatel existuje
                    }
                    myReader.Close();
                    OdbcCommand cmd1 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);
                    cmd1.Parameters.AddWithValue("@userid", DBuserid);
                    cmd1.Parameters.AddWithValue("@password", DBpasswdHash);
                    cmd1.Parameters.AddWithValue("@jmeno", DBjmeno);
                    cmd1.Parameters.AddWithValue("@prijmeni", DBprijmeni);
                    if (DBadmin)
                    {
                        cmd1.Parameters.AddWithValue("@admin", "A");
                    }
                    else
                    {
                        cmd1.Parameters.AddWithValue("@admin", "N");
                    }
                    cmd1.Parameters.AddWithValue("@permission", DBpermission);
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
                return 0;  // ok
            }
            return -1;
        }



        public override Int32 editNewLineUzivatele(string DBuserid, string DBjmeno, string DBprijmeni, string DBpermission,
                               Boolean DBadmin)
        {
            OdbcTransaction transaction = null;
            if (DBIsOpened())
            {
                string commandStringRead1 = "SELECT userid FROM uzivatele WHERE userid = ? ";
                string commandString1 = "UPDATE uzivatele set jmeno = ?, prijmeni = ?, admin = ?, permission = ? WHERE userid = ?";

                try
                {
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }

                    OdbcCommand cmdr1 = new OdbcCommand(commandStringRead1, myDBConn as OdbcConnection);
                    cmdr1.Transaction = transaction;
                    cmdr1.Parameters.AddWithValue("@userid", DBuserid);
                    OdbcDataReader myReader = cmdr1.ExecuteReader();
                    // true osCisloExist
                    if (myReader.Read() != true)
                    {
                        myReader.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -2; // uzivatel neexistuje
                    }
                    myReader.Close();
                    OdbcCommand cmd1 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);
                    cmd1.Parameters.AddWithValue("@jmeno", DBjmeno);
                    cmd1.Parameters.AddWithValue("@prijmeni", DBprijmeni);
                    if (DBadmin)
                    {
                        cmd1.Parameters.AddWithValue("@admin", "A");
                    }
                    else
                    {
                        cmd1.Parameters.AddWithValue("@admin", "N");
                    }
                    cmd1.Parameters.AddWithValue("@permission", DBpermission);
                    cmd1.Parameters.AddWithValue("@userid", DBuserid);
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
                return 0;  // ok
            }
            return -1;

        }

        public override Int32 editNewLinePasswordUzivatele(string DBuserid, string DBpasswdHash)
        {
            OdbcTransaction transaction = null;
            if (DBIsOpened())
            {
                string commandStringRead1 = "SELECT userid FROM uzivatele WHERE userid = ? ";
                string commandString1 = "UPDATE uzivatele set password = ? WHERE userid = ?";

                try
                {
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }

                    OdbcCommand cmdr1 = new OdbcCommand(commandStringRead1, myDBConn as OdbcConnection);
                    cmdr1.Transaction = transaction;
                    cmdr1.Parameters.AddWithValue("@userid", DBuserid);
                    OdbcDataReader myReader = cmdr1.ExecuteReader();
                    // true osCisloExist
                    if (myReader.Read() != true)
                    {
                        myReader.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -2; // uzivatel neexistuje
                    }
                    myReader.Close();
                    OdbcCommand cmd1 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);
                    cmd1.Parameters.AddWithValue("@password", DBpasswdHash);
                    cmd1.Parameters.AddWithValue("@userid", DBuserid);
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
                return 0;  // ok
            }
            return -1;


        }


        public override Int32 deleteLineUzivatele(string DBuserid)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandStringRead1 = "SELECT userid FROM uzivatele WHERE userid = ? ";
                string commandString1 = "DELETE FROM uzivatele WHERE userid = ?";

                try
                {
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }

                    OdbcCommand cmdr1 = new OdbcCommand(commandStringRead1, myDBConn as OdbcConnection);
                    cmdr1.Transaction = transaction;
                    cmdr1.Parameters.AddWithValue("@userid", DBuserid);
                    OdbcDataReader myReader = cmdr1.ExecuteReader();
                    // true osCisloExist
                    if (myReader.Read() != true)
                    {
                        myReader.Close();
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -2; // uzivatel neexistuje
                    }
                    myReader.Close();
                    OdbcCommand cmd1 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);
                    cmd1.Parameters.AddWithValue("@userid", DBuserid);
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
                return 0;  // ok
            }
            return -1;
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


        public override DataTable loadDataTable(string DBSelect, DateTime dateFrom, DateTime dateTo)
        {
            if (DBIsOpened())
            {
                OdbcCommand cmdr1 = new OdbcCommand(DBSelect, myDBConn as OdbcConnection);
                cmdr1.Parameters.AddWithValue("@dateFrom", dateFrom);
                cmdr1.Parameters.AddWithValue("@dateTo", dateTo);
                OdbcDataAdapter myDataAdapter = new OdbcDataAdapter(cmdr1);
                DataTable DBDataTable = new DataTable();
                DBDataTable.Locale = System.Globalization.CultureInfo.InvariantCulture;
                myDataAdapter.Fill(DBDataTable);
                myDataAdapter.Dispose();
                return DBDataTable;
            }
            return null;
        }


        public override DataTable loadDataTable(string DBSelect, DateTime dateFrom, DateTime dateTo, string text1)
        {
            if (DBIsOpened())
            {
                OdbcCommand cmdr1 = new OdbcCommand(DBSelect, myDBConn as OdbcConnection);
                cmdr1.Parameters.AddWithValue("@dateFrom", dateFrom);
                cmdr1.Parameters.AddWithValue("@dateTo", dateTo);
                cmdr1.Parameters.AddWithValue("@text1", text1);
                OdbcDataAdapter myDataAdapter = new OdbcDataAdapter(cmdr1);
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


            string DBSelect = "SELECT n.poradi, n.fyzstav as fyzstav, n.ucetstav as ucetstav, n.cena as cena, n.celkcena as celkcena, z.zustatek as zmeny_zustatek FROM naradi  n, zmeny z " +
                              "WHERE z.poradi = (SELECT MAX(s.poradi) FROM zmeny s WHERE z.parporadi = s.parporadi GROUP BY s.parporadi) " +
                              "AND z.parporadi = n.poradi and z.parporadi = " + poradi.ToString();

            return getDBLine(DBSelect, DBRow);
        }

        public override Boolean tableUzivateleExist()
        {
            if (DBIsOpened())
            {
                DataTable dt = (myDBConn as OdbcConnection).GetSchema("Tables");
                for (Int32 i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i].ItemArray[2].ToString() == "uzivatele")
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public override Boolean tableExist(string tableName)
        {
            if (DBIsOpened())
            {
                DataTable dt = (myDBConn as OdbcConnection).GetSchema("Tables");
                for (Int32 i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i].ItemArray[2].ToString() == tableName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override Boolean setNastaveniItem(Boolean state, string item, string userID)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandStringRead1 = "SELECT setid FROM nastaveni WHERE setid = ? ";
                string commandString1 = "INSERT INTO nastaveni (setid, permission, permission_hs, permission_hi, userid, datum) VALUES (?,?,\"\",0,?,?)";
                string commandString2 = "UPDATE nastaveni set permission =?, userid =?, datum = ? where setid =?";
                string stateCode;
                if (state) stateCode = "A";
                else stateCode = "N";

                Boolean useInsert = false;

                try
                {
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }
                    OdbcCommand cmdr1 = new OdbcCommand(commandStringRead1, myDBConn as OdbcConnection);
                    cmdr1.Transaction = transaction;
                    cmdr1.Parameters.AddWithValue("@setid", item);
                    OdbcDataReader myReader = cmdr1.ExecuteReader();
                    // true setidExist
                    if (myReader.Read() != true)
                    {
                        useInsert = true;
                    }
                    myReader.Close();

                    if (useInsert)
                    {
                        //insert
                        OdbcCommand cmd1 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);
                        cmd1.Parameters.AddWithValue("@setid", item);
                        cmd1.Parameters.AddWithValue("@permission", stateCode);
                        cmd1.Parameters.AddWithValue("@userid", userID);
                        cmd1.Parameters.AddWithValue("@datum", DateTime.Now);
                        cmd1.Transaction = transaction;
                        cmd1.ExecuteNonQuery();

                    }
                    else
                    {
                        //update
                        OdbcCommand cmd2 = new OdbcCommand(commandString2, myDBConn as OdbcConnection);
                        cmd2.Parameters.AddWithValue("@permission", stateCode);
                        cmd2.Parameters.AddWithValue("@userid", userID);
                        cmd2.Parameters.AddWithValue("@datum", DateTime.Now);
                        cmd2.Parameters.AddWithValue("@setid", item);
                        cmd2.Transaction = transaction;
                        cmd2.ExecuteNonQuery();
                    }
                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Commit();
                    }

                }
                catch
                {
                    return false;
                }
            }
            return true;
        }


        public override Boolean getNastaveniItem(string item)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {

                string commandStringRead1 = "SELECT permission FROM nastaveni WHERE setid = ? ";
                try
                {
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }

                    OdbcCommand cmdr1 = new OdbcCommand(commandStringRead1, myDBConn as OdbcConnection);
                    cmdr1.Transaction = transaction;
                    cmdr1.Parameters.AddWithValue("@setid", item);
                    OdbcDataReader myReader = cmdr1.ExecuteReader();
                    Boolean returnvalue = false;

                    if (myReader.Read() == true)
                    {
                        // nalezeno
                        // useInsert = true;
                        string permision = myReader.GetString(0);
                        myReader.Close();
                        if (permision == "A")
                        {
                            returnvalue = true;
                        }
                    }
                    else
                    {
                        // radka neexistuje
                        myReader.Close();
                    }
                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Commit();
                    }
                    return returnvalue;
                }
                catch
                {
                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Commit();
                    }
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public override Int32 deleteLastPrijem(Int32 DBnaradiPoradi, Int32 DBzmenyPoradi, Int32 DBprijem)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandStringRead1 = "SELECT prijem, vydej, stav, parporadi, poradi FROM zmeny WHERE parporadi = ? AND poradi = (" +
                    "select max(poradi) from zmeny where parporadi = ?)";
                string commandStringRead2 = "SELECT fyzstav, ucetstav, ucetkscen, celkcena, cena  FROM naradi where poradi = ? ";
                string commandStringRead3 = "SELECT permission FROM nastaveni WHERE setid = \'prumucetcena\'";
                string commandString1 = "DELETE FROM zmeny where parporadi = ? AND poradi = ? ";
                string commandString2 = "UPDATE naradi SET fyzstav = fyzstav - ?, ucetstav = ucetstav - ?, celkcena = celkcena - ?, ucetkscen = ?  WHERE poradi = ? ";

                try
                {
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }

                    OdbcCommand cmdr1 = new OdbcCommand(commandStringRead1, myDBConn as OdbcConnection);
                    cmdr1.Parameters.AddWithValue("@poradi", DBnaradiPoradi).DbType = DbType.Int32;
                    cmdr1.Parameters.AddWithValue("@poradi", DBnaradiPoradi).DbType = DbType.Int32;
                    cmdr1.Transaction = transaction;
                    OdbcDataReader seqReader1 = cmdr1.ExecuteReader();
                    Int32 prijem = 0;
                    Int32 vydej = 0;
                    string stav = "";
                    Int32 zmenyPoradi = 0;
                    Int32 naradiPoradi = 0;

                    if (seqReader1.Read() == true)
                    {
                        prijem = seqReader1.GetInt32(seqReader1.GetOrdinal("prijem"));
                        vydej = seqReader1.GetInt32(seqReader1.GetOrdinal("vydej"));
                        stav = seqReader1.GetString(seqReader1.GetOrdinal("stav"));
                        zmenyPoradi = seqReader1.GetInt32(seqReader1.GetOrdinal("poradi"));
                        naradiPoradi = seqReader1.GetInt32(seqReader1.GetOrdinal("parporadi"));
                        seqReader1.Close();

                        if (zmenyPoradi != DBzmenyPoradi)
                        {
                            if (transaction != null)
                            {
                                (transaction as OdbcTransaction).Rollback();
                            }
                            return -9; // Zaznam o zmene neexistuje - zmena z jineho mista
                        }

                        if (stav != "P")
                        {
                            if (transaction != null)
                            {
                                (transaction as OdbcTransaction).Rollback();
                            }
                            return -3; // Posledni zaznam neni prijem
                        }


                        if (prijem <= 0)
                        {
                            if (transaction != null)
                            {
                                (transaction as OdbcTransaction).Rollback();
                            }
                            return -4; // Neexistuje spravna hodnota prijmu
                        }

                        if (vydej != 0)
                        {
                            if (transaction != null)
                            {
                                (transaction as OdbcTransaction).Rollback();
                            }
                            return -5; // Vydej musi byt nulovy
                        }

                    }
                    else
                    {
                        seqReader1.Close();
                        // material neexistuje zrusime transakci a navratime chybu
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -2; // Zaznam nexistuje
                    }

                    // test opravy prijmu

                    if (DBprijem != prijem)
                    {
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -8; // Nesouhlasi velikost prijmu
                    }

                    // test tabulky naradi
                    Int32 fyzstav = 0;
                    Int32 ucetstav = 0;
                    double ucetkscen = 0;
                    double celkcena = 0;
                    double cena = 0;


                    OdbcCommand cmdr2 = new OdbcCommand(commandStringRead2, myDBConn as OdbcConnection);
                    cmdr2.Parameters.AddWithValue("@poradi", DBnaradiPoradi).DbType = DbType.Int32;
                    cmdr2.Transaction = transaction;
                    OdbcDataReader seqReader2 = cmdr2.ExecuteReader();
                    if (seqReader2.Read() == true)
                    {
                        fyzstav = seqReader2.GetInt32(seqReader2.GetOrdinal("fyzstav"));
                        ucetstav = seqReader2.GetInt32(seqReader2.GetOrdinal("ucetstav"));
                        ucetkscen = seqReader2.GetDouble(seqReader2.GetOrdinal("ucetkscen"));
                        celkcena = seqReader2.GetDouble(seqReader2.GetOrdinal("celkcena"));
                        cena = seqReader2.GetDouble(seqReader2.GetOrdinal("cena"));
                        seqReader2.Close();
                    }
                    else
                    {
                        seqReader2.Close();
                        // material neexistuje zrusime transakci a navratime chybu
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -6; // Zaznam nexistuje
                    }


                    if ((ucetstav < prijem) || (fyzstav < prijem))
                    {
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        seqReader2.Close();
                        return -7; // ucetni nebo fyz stav stav nesmi byt mensi nez prijem
                    }

                    OdbcCommand cmdr3 = new OdbcCommand(commandStringRead3, myDBConn as OdbcConnection);
                    cmdr3.Transaction = transaction;
                    OdbcDataReader myReader3 = cmdr3.ExecuteReader();
                    Boolean prumerUcetCenaEnabled = false;
                    if (myReader3.Read() == true)
                    {
                        string permision = myReader3.GetString(0);
                        myReader3.Close();
                        if (permision == "A")
                        {
                            prumerUcetCenaEnabled = true;
                        }
                    }
                    else
                    {
                        // radka neexistuje
                        myReader3.Close();
                    }

                    OdbcCommand cmd1 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);
                    cmd1.Parameters.AddWithValue("@parporadi", DBnaradiPoradi).DbType = DbType.Int32;
                    cmd1.Parameters.AddWithValue("@poradi", DBzmenyPoradi).DbType = DbType.Int32;
                    cmd1.Transaction = transaction;
                    cmd1.ExecuteNonQuery();

                    // do prikazu davame jen rozdily
                    OdbcCommand cmd2 = new OdbcCommand(commandString2, myDBConn as OdbcConnection);
                    cmd2.Parameters.AddWithValue("@fyzstav", DBprijem).DbType = DbType.Double;
                    cmd2.Parameters.AddWithValue("@ucetstav", DBprijem).DbType = DbType.Double;
                    // celkova cena nemuze byt zaporna
                    // zde musi byt test podle zpusobu uctovani

                    if (prumerUcetCenaEnabled)
                    {
                        // je nutno spocita novou prumernou cenu
                        if (celkcena < (DBprijem * cena))
                        {
                            // celkova cena je nulova , ucetni cena za kus je tez nulova
                            // celkova cena nemuze byt zaporna
                            cmd2.Parameters.AddWithValue("@celkcena", 0).DbType = DbType.Double;
                            cmd2.Parameters.AddWithValue("@ucetkscen", 0).DbType = DbType.Double;
                        }
                        else
                        {
                            cmd2.Parameters.AddWithValue("@celkcena", (DBprijem * cena)).DbType = DbType.Double;
                            double newUcetKsCen = (celkcena - (DBprijem * cena)) / (fyzstav - DBprijem);
                            cmd2.Parameters.AddWithValue("@ucetkscen", newUcetKsCen).DbType = DbType.Double;
                        }
                    }
                    else
                    {  // neni pouzita prumerovana cena
                        // ucetni cena za kus se nemeni - nastavuje se rucne
                        if (celkcena < (DBprijem * ucetkscen))
                        {
                            // celkova cena nemuze byt zaporna
                            cmd2.Parameters.AddWithValue("@celkcena", 0).DbType = DbType.Double;
                        }
                        else
                        {
                            cmd2.Parameters.AddWithValue("@celkcena", (DBprijem * ucetkscen)).DbType = DbType.Double;
                        }
                        cmd2.Parameters.AddWithValue("@ucetkscen", ucetkscen).DbType = DbType.Double;
                    }
                    // konec testu
                    cmd2.Parameters.AddWithValue("@poradi", DBnaradiPoradi).DbType = DbType.Int32;
                    cmd2.Transaction = transaction;
                    cmd2.ExecuteNonQuery();

                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Commit();
                    }
                    return 0;

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
            else
            {
                return -1;
            }
        }


        public override Int32 correctNaradiZmeny(Int32 DBparPoradi,  Int32 DBoldFyzstav, Int32 DBnewFyzStav, Int32 DBoldUcetStav, Int32 DBnewUcetStav, zmenyCorrectLine[] newZmeny)
        {

            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandStringRead1 = "SELECT count (*) as countz, MAX(poradi) as maxz FROM zmeny WHERE parporadi = ?";
                string commandStringRead2 = "SELECT prijem, vydej, zustatek, stav, poradi, * FROM zmeny zmeny WHERE parporadi = ?";
                string commandStringRead3 = "SELECT fyzstav, ucetstav FROM naradi WHERE poradi = ? ";
                string commandString1 = "UPDATE zmeny set zustatek = ? where parporadi = ? AND poradi = ? ";
                string commandString2 = "UPDATE naradi SET fyzstav = ?, ucetstav = ? WHERE poradi = ? ";

                Int32 newZmenyCount =  newZmeny.Length;

                if (newZmenyCount == 0)
                {
                    return -2; //neni zadny zaznam ve zmenach
                }

                try
                {
                    try
                    {
                        transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    }
                    catch
                    {
                    }

                    OdbcCommand cmdr1 = new OdbcCommand(commandStringRead1, myDBConn as OdbcConnection);
                    cmdr1.Parameters.AddWithValue("@parporadi", DBparPoradi).DbType = DbType.Int32;
                    cmdr1.Transaction = transaction;
                    OdbcDataReader seqReader1 = cmdr1.ExecuteReader();
                    Int32 countz = 0;
                    Int32 maxz = 0;

                    if (seqReader1.Read() == true)
                    {
                        countz = seqReader1.GetInt32(seqReader1.GetOrdinal("countz"));
                        maxz = seqReader1.GetInt32(seqReader1.GetOrdinal("maxz"));
                        seqReader1.Close();

                        if (countz != newZmenyCount)
                        {
                            if (transaction != null)
                            {
                                (transaction as OdbcTransaction).Rollback();
                            }
                            return -4; // Doslo ko zmenam v tabulce zmen - pocet
                        }

                        zmenyCorrectLine zcl = newZmeny[newZmenyCount - 1];
                        if (zcl .poradi != maxz)
                        {
                            if (transaction != null)
                            {
                                (transaction as OdbcTransaction).Rollback();
                            }
                            return -5; // Doslo ko zmenam v tabulce zmen - poradi
                        }
                    }
                    else
                    {
                        seqReader1.Close();
                        // material neexistuje zrusime transakci a navratime chybu
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -3; // Neexistuje zaznam zmen
                    }

                    OdbcCommand cmdr2 = new OdbcCommand(commandStringRead2, myDBConn as OdbcConnection);
                    cmdr2.Parameters.AddWithValue("@parporadi", DBparPoradi).DbType = DbType.Int32;
                    cmdr2.Transaction = transaction;
                    OdbcDataReader seqReader2 = cmdr2.ExecuteReader();

                    Int32 i = 0;
                    zmenyCorrectLine zcl2; ;

                    while (seqReader2.Read())
                    {
                        zcl2 = newZmeny[i];
                        if ((zcl2.poradi != seqReader2.GetInt32(seqReader2.GetOrdinal("poradi")))
                            || (zcl2.prijem != seqReader2.GetInt32(seqReader2.GetOrdinal("prijem")))
                            || (zcl2.vydej != seqReader2.GetInt32(seqReader2.GetOrdinal("vydej")))
                            || (zcl2.zustatek != seqReader2.GetInt32(seqReader2.GetOrdinal("zustatek")))
                            || (zcl2.stavcod != seqReader2.GetString(seqReader2.GetOrdinal("stav")))    )
                        {
                            seqReader2.Close();
                            if (transaction != null)
                            {
                                (transaction as OdbcTransaction).Rollback();
                            }
                            return -6; // tabulka zmen stavu byla zmenena - jina instance programu
                        }
                        i++;
                    }


                    OdbcCommand cmdr3 = new OdbcCommand(commandStringRead3, myDBConn as OdbcConnection);
                    cmdr3.Parameters.AddWithValue("@poradi", DBparPoradi).DbType = DbType.Int32;
                    cmdr3.Transaction = transaction;
                    OdbcDataReader seqReader3 = cmdr3.ExecuteReader();
                    Int32 aktFyzStav = 0;
                    Int32 aktUcetStav = 0;
                    if (seqReader3.Read() == true)
                    {
                        aktFyzStav = seqReader3.GetInt32(seqReader3.GetOrdinal("fyzstav"));
                        aktUcetStav = seqReader3.GetInt32(seqReader3.GetOrdinal("ucetstav"));
                        seqReader3.Close();

                        if ((DBoldFyzstav != aktFyzStav) || (DBoldUcetStav != aktUcetStav))
                        {
                            if (transaction != null)
                            {
                                (transaction as OdbcTransaction).Rollback();
                            }
                            return -8; // Zmena dat v tabulce materialu
                        }
                    }
                    else
                    {
                        seqReader3.Close();
                        // material neexistuje zrusime transakci a navratime chybu
                        if (transaction != null)
                        {
                            (transaction as OdbcTransaction).Rollback();
                        }
                        return -7; // Neexistuje zaznam materialu
                    }
                    
// opravime tabulku zmen
                    for (Int32 ii = 0; ii < newZmenyCount; ii++)
                    {
                        zcl2 = newZmeny[ii];
                        if (zcl2.zustatek != zcl2.novyZustatek)
                        {
                            // zapiseme data
                            OdbcCommand cmd1 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);
                            cmd1.Parameters.AddWithValue("@zustatek", zcl2.novyZustatek).DbType = DbType.Int32;
                            cmd1.Parameters.AddWithValue("@parporadi", DBparPoradi).DbType = DbType.Int32;
                            cmd1.Parameters.AddWithValue("@poradi", zcl2.poradi).DbType = DbType.Int32;
                            cmd1.Transaction = transaction;
                            cmd1.ExecuteNonQuery();
                        }
                    }

//opravime tabulku materialu
                    if ((DBoldFyzstav != DBnewFyzStav) || (DBoldUcetStav != DBnewUcetStav))
                    {
//                        zapiseme data
                        OdbcCommand cmd2 = new OdbcCommand(commandString2, myDBConn as OdbcConnection);
                        cmd2.Parameters.AddWithValue("@fyzstav", DBnewFyzStav).DbType = DbType.Int32;
                        cmd2.Parameters.AddWithValue("@ucetstav", DBnewUcetStav).DbType = DbType.Int32;
                        cmd2.Parameters.AddWithValue("@poradi", DBparPoradi).DbType = DbType.Int32;
                        cmd2.Transaction = transaction;
                        cmd2.ExecuteNonQuery();
                    }

                    if (transaction != null)
                    {
                        (transaction as OdbcTransaction).Commit();
                    }
                    return 0;
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
            else
            {
                return -1;
            }

        }


    }
}

