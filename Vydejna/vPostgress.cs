using System;
using System.Data;
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
                OdbcCommand cmd1Sequence = new OdbcCommand("UPDATE tabseq SET poradi=1 WHERE nazev = 'naradi'", myDBConn as OdbcConnection);
                OdbcCommand cmd2Sequence = new OdbcCommand("UPDATE tabseq SET poradi=1 WHERE nazev = 'vraceno'", myDBConn as OdbcConnection);
                OdbcCommand cmd3Sequence = new OdbcCommand("UPDATE tabseq SET poradi=1 WHERE nazev = 'poskozeno'", myDBConn as OdbcConnection);
                OdbcCommand cmd4Sequence = new OdbcCommand("UPDATE tabseq SET poradi=1 WHERE nazev = 'pujceno'", myDBConn as OdbcConnection);
                OdbcCommand cmdVraceno = new OdbcCommand("DELETE from vraceno", myDBConn as OdbcConnection);
                OdbcCommand cmdPoskozeno = new OdbcCommand("DELETE from poskozeno", myDBConn as OdbcConnection);
                OdbcCommand cmdOsoby = new OdbcCommand("DELETE from osoby", myDBConn as OdbcConnection);
                OdbcCommand cmdZmeny = new OdbcCommand("DELETE from zmeny", myDBConn as OdbcConnection);
                OdbcCommand cmdPujceno = new OdbcCommand("DELETE from pujceno", myDBConn as OdbcConnection);
                OdbcCommand cmdVacuum = new OdbcCommand("VACUUM", myDBConn as OdbcConnection);
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



        public override DataTable loadDataTableSestavaPosStrediska(DateTime dateFrom, DateTime dateTo)
        {
            return loadDataTable("select dilna, round(sum(cena * pocetks)::numeric,3) as cena, 0 as procenta from poskozeno where datum >= ? and datum <= ? group by dilna order by dilna", dateFrom, dateTo);
        }

        public override DataTable loadDataTableSestavaPosOsobyZaStred(DateTime dateFrom, DateTime dateTo, string stredisko)
        {
            return loadDataTable("select krjmeno, jmeno, oscislo, round(sum(cena * pocetks)::numeric,3) as cena  from poskozeno where datum >= ? and datum <= ? and dilna = ? group by oscislo,krjmeno,jmeno order by oscislo", dateFrom, dateTo, stredisko);
        }

        public override DataTable loadDataTableSestavaPosZaOsobu(DateTime dateFrom, DateTime dateTo, string oscislo)
        {
            return loadDataTable("select nazev, csn, jk, datum, pocetks, round(cena::numeric ,3) as cena, round((cena *pocetks)::numeric,3) as celkcena  from poskozeno where datum >= ? and datum <= ? and oscislo = ?  order by datum", dateFrom, dateTo, oscislo);
        }

        public override DataTable loadDataTableSestavaPosZakazkaZaStred(DateTime dateFrom, DateTime dateTo, string stredisko)
        {
            return loadDataTable("select vyrobek, round(sum(cena * pocetks)::numeric,3) as cena  from poskozeno where datum >= ? and datum <= ? and dilna = ? group by vyrobek order by vyrobek", dateFrom, dateTo, stredisko);
        }

        public override DataTable loadDataTableSestavaPosZakazka(DateTime dateFrom, DateTime dateTo)
        {
            return loadDataTable("select vyrobek, round(sum(cena * pocetks)::numeric,3) as cena  from poskozeno where datum >= ? and datum <= ?  group by vyrobek order by vyrobek", dateFrom, dateTo);
        }


        public override DataTable loadDataTableSestavaPosKonto(DateTime dateFrom, DateTime dateTo)
        {
            return loadDataTable("select konto, round(sum(cena * pocetks)::numeric,3) as cena  from poskozeno where datum >= ? and datum <= ?  group by konto order by konto", dateFrom, dateTo);
        }

        public override DataTable loadDataTableSestavaPosZaKonto(DateTime dateFrom, DateTime dateTo, string konto)
        {
            return loadDataTable("select nazev, csn, jk, datum, pocetks, round(cena::numeric ,3) as cena, round((cena *pocetks)::numeric,3) as celkcena  from poskozeno where datum >= ? and datum <= ? and konto = ?  order by datum", dateFrom, dateTo, konto);
        }

        public override DataTable loadDataTableSestavaPosZaZakazku(DateTime dateFrom, DateTime dateTo, string vyrobek)
        {
            return loadDataTable("select nazev, csn, jk, datum, pocetks, round(cena::numeric ,3) as cena, round((cena *pocetks)::numeric,3) as celkcena  from poskozeno where datum >= ? and datum <= ? and vyrobek = ?  order by datum", dateFrom, dateTo, vyrobek);
        }

        public override Int32 VycisteniTabulek()
        {
            Int32  returnCode = 0;
            openDB();
            if (DBIsOpened())
            {
                OdbcCommand cmdVacuum = new OdbcCommand("VACUUM FULL", myDBConn as OdbcConnection);
                OdbcCommand cmdAnalyze = new OdbcCommand("ANALYZE", myDBConn as OdbcConnection);
                OdbcCommand cmdReindexN = new OdbcCommand("REINDEX TABLE naradi", myDBConn as OdbcConnection);
                OdbcCommand cmdReindexZ = new OdbcCommand("REINDEX TABLE zmeny", myDBConn as OdbcConnection);
                OdbcCommand cmdReindexP = new OdbcCommand("REINDEX TABLE pujceno", myDBConn as OdbcConnection);
                OdbcCommand cmdReindexO = new OdbcCommand("REINDEX TABLE osoby", myDBConn as OdbcConnection);
                try
                {
                    cmdVacuum.ExecuteNonQuery();
                    cmdAnalyze.ExecuteNonQuery();
                    cmdReindexN.ExecuteNonQuery();
                    cmdReindexZ.ExecuteNonQuery();
                    cmdReindexP.ExecuteNonQuery();
                    cmdReindexO.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    returnCode = -2; // operace se nezdarila
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cmdReindexO.Dispose();
                    cmdReindexP.Dispose();
                    cmdReindexZ.Dispose();
                    cmdReindexN.Dispose();
                    cmdAnalyze.Dispose();
                    cmdVacuum.Dispose();
                }
            }
            return returnCode;
        }

        public override string getDBTypAndName()
        {
            return string.Format("{0}  \"{1}:POSTGRESSQL-ODBC\"", dBName, dBServerAddress);
        }

        public override Int32 addNewLineZmenyAndPoskozeno(Int32 DBporadi, DateTime DBdatum, Int32 DBvydej, string DBpoznamka,
                                                          string osCislo, string DBjmeno, string DBprijmeni, string DBstredisko, string DBprovoz,
                                                          string DBkonto, double DBcena, double DBcelkCena, string DBcisZak)
        {
            OdbcTransaction transaction = null;

            if (DBIsOpened())
            {
                string commandStringRead1 = "SELECT poradi, zustatek from zmeny where parporadi = ? ORDER BY poradi DESC";
                string commandStringRead2 = "SELECT fyzstav, ucetstav, ucetkscen, rozmer, nazev, jk, normacsn FROM naradi where poradi = ? ";

                string commandString1 = "UPDATE naradi set fyzstav = fyzstav - ?, ucetstav = ucetstav - ?, celkcena = round ( CAST(celkcena - (ucetkscen * ?) as numeric) ,2)  where poradi = ? ";
                string commandString2 = "INSERT INTO zmeny (parporadi, pomozjk, datum, poznamka, prijem, vydej, zustatek, zapkarta, vevcislo, pocivc, stav, poradi )" +
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";
                string commandString4 = "UPDATE  tabseq set poradi = ? WHERE nazev = 'poskozeno'";

                string commandString5 = "INSERT INTO poskozeno ( poradi, jmeno, oscislo, dilna, pracoviste, vyrobek, nazev, jk, rozmer, pocetks, cena, datum, csn, krjmeno, celkcena, vevcislo, konto) " +
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";

                string commandString6 = "UPDATE naradi set celkcena = 0 where poradi = ? AND celkcena < 0";

                Int32 newPoskozenoPoradi = 0;

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
                        decimal ucetkscen = Convert.ToDecimal(myReader2.GetValue(myReader2.GetOrdinal("ucetkscen")));
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
                        //

                        newPoskozenoPoradi = getPoskozenoNewIndex(transaction, false);
                        if (newPoskozenoPoradi == -1)
                        {
                            if (transaction != null)
                            {
                                (transaction as OdbcTransaction).Rollback();
                            }
                            return -1;  // chyba
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
//                        string commandString5 = "INSERT INTO poskozeno ( poradi, jmeno, oscislo, dilna, pracoviste, vyrobek, nazev, jk, rozmer, pocetks, cena, datum, csn, krjmeno, celkcena, vevcislo, konto) " +
//                              "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? )";

                        OdbcCommand cmd3 = new OdbcCommand(commandString5, myDBConn as OdbcConnection);
                        cmd3.Parameters.AddWithValue("@poradi", newPoskozenoPoradi).DbType = DbType.Int32;
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
                        cmd3.Parameters.AddWithValue("@celkcena", ucetkscen * DBvydej).DbType = DbType.Double;
                        cmd3.Parameters.AddWithValue("@vevcislo", "");
                        cmd3.Parameters.AddWithValue("@konto", DBkonto);
                        cmd3.Transaction = transaction;
                        cmd3.ExecuteNonQuery();

                        OdbcCommand cmd4 = new OdbcCommand(commandString4, myDBConn as OdbcConnection);
                        cmd4.Parameters.AddWithValue("@poradi", newPoskozenoPoradi + 1).DbType = DbType.Int32;
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


    }
}
