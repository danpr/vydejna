using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Windows.Forms;



namespace Vydejna
{
    class vPostgress : vODBC
    {
        public vPostgress(string dataBaseName, string serverAddress, string serverName, string port, string locale, string driver, string userName, string password)
            : base(dataBaseName, serverAddress, serverName, port, locale, driver, userName, password)
        {

            dBConnectStr = String.Format("Driver={{PostgreSQL UNICODE}};Server={0,1};Port={1,1};Database={2,1};Uid={3,1};Pwd={4,1}", serverAddress, port, dataBaseName, userName, password);
        
        }


        public override void openDB()
        {

            try
            {
                myDBConn = new OdbcConnection(dBConnectStr); 
//                myDBConn = new OdbcConnection("Driver={PostgreSQL UNICODE};Server=192.168.1.30;Port=5432;Database=vydejna;Uid=dan;Pwd=aloondra");
//                myDBConn = new OdbcConnection("DSN=vydejna");
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
                OdbcCommand cmdVacuum = new OdbcCommand("VACUUM", myDBConn as OdbcConnection);
                try
                {
                    cmdNaradi.ExecuteNonQuery();
                    cmdKarta.ExecuteNonQuery();
                    cmdSequence.ExecuteNonQuery();
                    cmdVraceno.ExecuteNonQuery();
                    cmdPoskozeno.ExecuteNonQuery();
                    cmdOsoby.ExecuteNonQuery();
                    cmdZmeny.ExecuteNonQuery();
                    cmdVacuum.ExecuteNonQuery();
                    MessageBox.Show("Tabulky byly smazány");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cmdVacuum.Dispose();
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
                                         decimal DBcelkcena,  long DBucetstav, long DBfyzstav,
                                         string DBrozmer, string DBanalucet, decimal DBucetkscen, DateTime DBkdatum)
        {


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

                    OdbcCommand cmdr = new OdbcCommand(commandStringSeq1, myDBConn as OdbcConnection);

                    cmdr.Transaction = transaction;

                    OdbcDataReader myReader = cmdr.ExecuteReader();
                    myReader.Read();
                    Int32 maxporadi = myReader.GetInt32(0);
                    myReader.Close();
                    maxporadi++;


                    OdbcCommand cmd = new OdbcCommand(commandString2, myDBConn as OdbcConnection);

                    OdbcParameter p0 = new OdbcParameter("p0", OdbcType.NChar);
                    p0.Value = maxporadi;
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
                    p19.Value = new DateTime(0);
                    OdbcParameter p20 = new OdbcParameter("p20", OdbcType.NChar);
                    p20.Value = "";
                    OdbcParameter p21 = new OdbcParameter("p21", OdbcType.NChar);
                    p21.Value = "";
                    OdbcParameter p22 = new OdbcParameter("p22", OdbcType.NChar);
                    p22.Value = "";
                    OdbcParameter p23 = new OdbcParameter("p23", OdbcType.NChar);
                    p23.Value = "";
                    OdbcParameter p24 = new OdbcParameter("p24", OdbcType.NChar);
                    p24.Value = "";
                    OdbcParameter p25 = new OdbcParameter("p25", OdbcType.Double);
                    p25.Value = DBucetkscen;
                    OdbcParameter p26 = new OdbcParameter("p26", OdbcType.NChar);
                    p26.Value = "";
                    OdbcParameter p27 = new OdbcParameter("p27", OdbcType.NChar);
                    p27.Value = "";
                    OdbcParameter p28 = new OdbcParameter("p28", OdbcType.Date);
                    p28.Value = DBkdatum;
                    OdbcParameter p29 = new OdbcParameter("p29", OdbcType.NChar);
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
                    transaction = (myDBConn as OdbcConnection).BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

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

                        OdbcParameter p1 = new OdbcParameter("p1", OdbcType.NChar);
                        p1.Value = DBprijmeni;
                        OdbcParameter p2 = new OdbcParameter("p2", OdbcType.NChar);
                        p2.Value = DBjmeno;
                        OdbcParameter p3 = new OdbcParameter("p3", OdbcType.NChar);
                        p3.Value = DBulice;
                        OdbcParameter p4 = new OdbcParameter("p4", OdbcType.NChar);
                        p4.Value = DBmesto;
                        OdbcParameter p5 = new OdbcParameter("p5", OdbcType.NChar);
                        p5.Value = DBpsc;
                        OdbcParameter p6 = new OdbcParameter("p6", OdbcType.NChar);
                        p6.Value = DBtelHome;
                        OdbcParameter p7 = new OdbcParameter("p7", OdbcType.NChar);
                        p7.Value = DBosCislo;
                        OdbcParameter p8 = new OdbcParameter("p8", OdbcType.NChar);
                        p8.Value = DBoddeleni;
                        OdbcParameter p9 = new OdbcParameter("p9", OdbcType.NChar);
                        p9.Value = DBtelZam;
                        OdbcParameter p10 = new OdbcParameter("p10", OdbcType.NChar);
                        p10.Value = DBstredisko;
                        OdbcParameter p11 = new OdbcParameter("p11", OdbcType.NChar);
                        p11.Value = "";
                        OdbcParameter p12 = new OdbcParameter("p12", OdbcType.NChar);
                        p12.Value = DBpracoviste;
                        OdbcParameter p13 = new OdbcParameter("p13", OdbcType.NChar);
                        p13.Value = DBcisZnamky;
                        OdbcParameter p14 = new OdbcParameter("p14", OdbcType.NChar);
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





    }
}
