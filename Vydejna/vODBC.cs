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
                      "pracoviste varchar(20), vyrobek varchar(15),nazev varchar(60), jk varchar(15), rozmer varchar(25)," +
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
            OdbcCommand cmdUsers = new OdbcCommand(commandStringUsers, myDBConn as OdbcConnection);

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
                    cmdUsers.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
//                    myDBConn.Close();
                    cmdUsers.Dispose();
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



        public override Int32 addLineKaret(string DBnazev,string DBJK, string DBnormacsn, string DBnormadin,
                                         string DBvyrobce, double DBcena, string DBpoznamka, int DBminstav,
                                         double DBcelkcena, int DBucetstav, int DBfyzstav,
                                         string DBrozmer, string DBanalucet, DateTime DBdate, string DBstredisko,
                                         string DBdruhp, string DBodpis)
        {
            string commandStringSeq1 = "SELECT poradi FROM tabseq WHERE nazev = 'naradi'";
            string commandStringSeq2 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'naradi'";

            string commandString = "INSERT INTO karta ( poradi, nazev, jk, normacsn, normadin, vyrobce, cena,"+
                  " poznamka, minimum, celkcena, ucetstav, fyzstav, rozmer, analucet, tdate, stredisko,"+
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

            string commandString = "INSERT INTO naradi ( poradi, nazev, jk, normacsn, normadin, vyrobce, cena,"+
                  " poznamka, minimum, celkcena, ucetstav, fyzstav, rozmer, analucet, tdate, stredisko, druh,"+
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




        // pridani nove polozky do tabulky naradi
        public override Int32 addNewLineNaradi(string DBnazev, string DBJK, string DBnormacsn, string DBnormadin,
                                         string DBvyrobce, decimal DBcena, string DBpoznamka, long DBminstav,
                                         decimal DBcelkcena, long DBucetstav, long DBfyzstav,
                                         string DBrozmer, string DBanalucet, decimal DBucetkscen, DateTime DBkdatum)
        {
            string commandReadString0 = "SELECT count(*) as countporadi from naradi";
            string commandReadString1 = "SELECT MAX(poradi) as maxporadi from naradi";
            string commandString1 = "UPDATE  tabseq set poradi = poradi +1 WHERE nazev = 'naradi'";

            string commandString2 = "INSERT INTO naradi ( poradi, nazev, jk, normacsn, normadin, vyrobce, cena,"+
                  " poznamka, minimum, celkcena,  ucetstav, fyzstav, rozmer, analucet, tdate, stredisko, druh,"+
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
                    OdbcDataReader myReader0 = cmd0.ExecuteReader();
                    myReader0.Read();
                    Int32 countporadi = myReader0.GetInt32(0);
                    myReader0.Close();

                    if (countporadi == 0) maxporadi = 1;
                    else
                    {


                        OdbcCommand cmd1 = new OdbcCommand(commandReadString1, myDBConn as OdbcConnection);
                        OdbcDataReader myReader1 = cmd1.ExecuteReader();
                        myReader1.Read();
                        maxporadi = myReader1.GetInt32(0);
                        myReader1.Close();
                        maxporadi++;
                    }

                    OdbcCommand cmd = new OdbcCommand(commandString2, myDBConn as OdbcConnection);

                    cmd.Parameters.AddWithValue("@poradi", maxporadi);
                    cmd.Parameters.AddWithValue("nazev", DBnazev);
                    cmd.Parameters.AddWithValue("@jk", DBJK);
                    cmd.Parameters.AddWithValue("@normacsn", DBnormacsn);
                    cmd.Parameters.AddWithValue("@normadin", DBnormadin);
                    cmd.Parameters.AddWithValue("@vyrobce", DBvyrobce);
                    cmd.Parameters.AddWithValue("@cena", DBcena);
                    cmd.Parameters.AddWithValue("@poznamka", DBpoznamka);
                    cmd.Parameters.AddWithValue("@minimum", DBminstav);
                    cmd.Parameters.AddWithValue("@celkcena", DBcelkcena);
                    cmd.Parameters.AddWithValue("@ucetstav", DBucetstav);
                    cmd.Parameters.AddWithValue("@fyzstav", DBfyzstav);
                    cmd.Parameters.AddWithValue("@rozmer", DBrozmer);
                    cmd.Parameters.AddWithValue("@analucet", DBanalucet);
                    cmd.Parameters.AddWithValue("@tdate", new DateTime(0));
                    cmd.Parameters.AddWithValue("@stredisko", "");
                    cmd.Parameters.AddWithValue("@druh", "");
                    cmd.Parameters.AddWithValue("@odpis", "");
                    cmd.Parameters.AddWithValue("@ucetkscen", DBucetkscen);
                    cmd.Parameters.AddWithValue("@kdatum", DBkdatum);
                    cmd.Parameters.AddWithValue("@kodd", "");

                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();

                    OdbcCommand cmdSeq2 = new OdbcCommand(commandString1, myDBConn as OdbcConnection);

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
                    cmd1.Parameters.AddWithValue("@parporadi", DBParPoradi);
                    cmd1.Parameters.AddWithValue("@poradi", DBPoradi);
                    cmd1.Transaction = transaction;
                    Int32 errCode = cmd1.ExecuteNonQuery();
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
                    cmdr2.Parameters.AddWithValue("@poradi",DBporadi);
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
                    cmdr7.Parameters.AddWithValue("@poradi", zmenPoradi);
                    cmdr7.Parameters.AddWithValue("@parporadi", parPoradi);
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
                    cmdr5.Parameters.AddWithValue("@poradi", parPoradi);
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
                    cmdr3.Parameters.AddWithValue("poradi", parPoradi);
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
                    cmd1.Parameters.AddWithValue("@fyzstav", DBks);
                    cmd1.Parameters.AddWithValue("@poradi", parPoradi);

                    cmd1.Transaction = transaction;
                    Int32 errCode = cmd1.ExecuteNonQuery();

                    //  tab zmeny novy zaznam
                    OdbcCommand cmd2 = new OdbcCommand(commandString2, myDBConn as OdbcConnection);

                    cmd2.Parameters.AddWithValue("@parporadi",parPoradi);
                    cmd2.Parameters.AddWithValue("@pomozjk",naradiJK);
                    cmd2.Parameters.AddWithValue("@datum",DBdatum);
                    cmd2.Parameters.AddWithValue("@poznamka",DBpoznamka);
                    cmd2.Parameters.AddWithValue("@prijem",DBks);
                    cmd2.Parameters.AddWithValue("@vydej",0);
                    cmd2.Parameters.AddWithValue("@zustatek",zustatek + DBks);
                    cmd2.Parameters.AddWithValue("@zapkarta",DBosCislo);
                    cmd2.Parameters.AddWithValue("@vevcislo",zmenyVevcislo);
                    cmd2.Parameters.AddWithValue("@pocivc",0);
                    cmd2.Parameters.AddWithValue("@stav","R");
                    cmd2.Parameters.AddWithValue("@poradi", newZmenyPoradi);
                    cmd2.Transaction = transaction;
                    errCode = cmd2.ExecuteNonQuery();

                    if (pujcKs != DBks)
                    {
                        // tab pujceno zmena stavu
                        OdbcCommand cmd3 = new OdbcCommand(commandString3, myDBConn as OdbcConnection);
                        cmd3.Parameters.AddWithValue("@stavks", DBks);
                        cmd3.Parameters.AddWithValue("@poradi", DBporadi);
                        cmd3.Transaction = transaction;
                        errCode = cmd3.ExecuteNonQuery();
                    }
                    else
                    {
                       // tab pujceno smazani
                        OdbcCommand cmd4 = new OdbcCommand(commandString4, myDBConn as OdbcConnection);
                        cmd4.Parameters.AddWithValue("@poradi", DBporadi);
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
                    cmd5.Parameters.AddWithValue("poradi",newZmenyPoradi);
                    cmd5.Parameters.AddWithValue("jmeno",osobyPrijmeni);
                    cmd5.Parameters.AddWithValue("oscislo",DBosCislo);
                    cmd5.Parameters.AddWithValue("dilna",osobyStredisko);
                    cmd5.Parameters.AddWithValue("pracoviste",osobyOddeleni);
                    cmd5.Parameters.AddWithValue("vyrobek","");
                    cmd5.Parameters.AddWithValue("nazev",naradiNazev);
                    cmd5.Parameters.AddWithValue("jk",naradiJK);
                    cmd5.Parameters.AddWithValue("rozmer",naradiRozmer);
                    cmd5.Parameters.AddWithValue("pocetks",DBks);
                    cmd5.Parameters.AddWithValue("cena",naradiCena);
                    cmd5.Parameters.AddWithValue("datum",DBdatum);
                    cmd5.Parameters.AddWithValue("csn",naradiCSN);
                    cmd5.Parameters.AddWithValue("krjmeno",osobyJmeno);
                    cmd5.Parameters.AddWithValue("celkcena",naradiCelkCena);
                    cmd5.Parameters.AddWithValue("vevcislo",zmenyVevcislo);
                    cmd5.Parameters.AddWithValue("konto","");
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
                    cmdr2.Parameters.AddWithValue("@poradi",DBparPoradi);
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
                    cmdr1.Parameters.AddWithValue("@parporadi",DBparPoradi);
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
                    cmdr5.Parameters.AddWithValue("@poradi", DBparPoradi);
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
                    cmd1.Parameters.AddWithValue("@fyzstav", DBks);
                    cmd1.Parameters.AddWithValue("@poradi", DBparPoradi);
                    cmd1.Transaction = transaction;
                    cmd1.ExecuteNonQuery();

                    //  tab zmeny
                    OdbcCommand cmd2 = new OdbcCommand(commandString2, myDBConn as OdbcConnection);
// "INSERT INTO zmeny (parporadi, pomozjk, datum, poznamka, prijem, vydej, zustatek, zapkarta, vevcislo, pocivc, stav, poradi )" +
                    cmd2.Parameters.AddWithValue("@parporadi",DBparPoradi);
                    cmd2.Parameters.AddWithValue("@pomozjk",naradiJK );
                    cmd2.Parameters.AddWithValue("@datum",DBdatum );
                    cmd2.Parameters.AddWithValue("@poznamka",DBpoznamka );
                    cmd2.Parameters.AddWithValue("@prijem", 0);
                    cmd2.Parameters.AddWithValue("@vydej", DBks);
                    cmd2.Parameters.AddWithValue("@zustatek", zustatek - DBks);
                    cmd2.Parameters.AddWithValue("@zapkarta", DBosCislo);
                    cmd2.Parameters.AddWithValue("@vevcislo", DBvevCislo);
                    cmd2.Parameters.AddWithValue("@pocivc", 0);
                    cmd2.Parameters.AddWithValue("@stav", "U");
                    cmd2.Parameters.AddWithValue("@poradi", poradi);
                    cmd2.Transaction = transaction;
                    cmd2.ExecuteNonQuery();

                    //pujceno
                    // poradi, oscislo, nporadi, zporadi, pjmeno, pprijmeni, pnazev, pjk, pdatum, pks, pcena, stavks
                    OdbcCommand cmd = new OdbcCommand(commandString3, myDBConn as OdbcConnection);
                    cmd.Parameters.AddWithValue("@poradi",pujcPoradi);
                    cmd.Parameters.AddWithValue("@oscislo",DBosCislo);
                    cmd.Parameters.AddWithValue("@nporadi",DBparPoradi);
                    cmd.Parameters.AddWithValue("@zporadi",poradi);
                    cmd.Parameters.AddWithValue("@pjmeno",osobyJmeno);
                    cmd.Parameters.AddWithValue("@pprijmeni",osobyPrijmeni);
                    cmd.Parameters.AddWithValue("@pnazev",naradiNazev);
                    cmd.Parameters.AddWithValue("@pjk",naradiJK);
                    cmd.Parameters.AddWithValue("@pdatum",DBdatum );
                    cmd.Parameters.AddWithValue("@pks",DBks );
                    cmd.Parameters.AddWithValue("@pcena",naradiCena);
                    cmd.Parameters.AddWithValue("@stavks",DBks);

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
                    cmdr1.Parameters.AddWithValue("@poradi", DBporadi);
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
            string commandString = "INSERT INTO pujceno ( poradi, oscislo, nporadi, zporadi, stavks, pjmeno, pprijmeni, pnazev, pjk, pdatum, pks, pcena )" +
                  "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";

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
                    MessageBox.Show("Neexistuje zmena stavu pro vypujcení. Poradi/Nazev:" + DBparPoradi.ToString() + " - " + DBnazev + " OSCislo: "+ DBosCislo + " - " + DBPrijmeni);
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
                p9.Value = DBdatum ;
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


    }
}

