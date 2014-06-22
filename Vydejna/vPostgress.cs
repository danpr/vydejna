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



        public override DataTable loadDataTableSestavaPosStrediska(DateTime dateFrom, DateTime dateTo)
        {
            return loadDataTable("select dilna, round(sum(cena * pocetks)::numeric,3) as cena, 0 as procenta from poskozeno where datum >= ? and datum <= ? group by dilna order by dilna", dateFrom, dateTo);
        }

        public virtual DataTable loadDataTableSestavaPosOsobyZaStred(DateTime dateFrom, DateTime dateTo, string stredisko)
        {
            return loadDataTable("select krjmeno, jmeno, oscislo, round(sum(cena * pocetks)::numeric,3) as cena  from poskozeno where datum >= ? and datum <= ? and dilna = ? group by oscislo,krjmeno,jmeno order by oscislo", dateFrom, dateTo, stredisko);
        }

        public virtual DataTable loadDataTableSestavaPosZaOsobu(DateTime dateFrom, DateTime dateTo, string oscislo)
        {
            return loadDataTable("select nazev, csn, jk, datum, pocetks, round(cena::numeric ,3) as cena, round((cena *pocetks)::numeric,3) as celkcena  from poskozeno where datum >= ? and datum <= ? and oscislo = ?  order by datum", dateFrom, dateTo, oscislo);
        }

        public virtual DataTable loadDataTableSestavaPosZakazkaZaStred(DateTime dateFrom, DateTime dateTo, string stredisko)
        {
            return loadDataTable("select vyrobek, round(sum(cena * pocetks)::numeric,3) as cena  from poskozeno where datum >= ? and datum <= ? and dilna = ? group by vyrobek order by vyrobek", dateFrom, dateTo, stredisko);
        }

        public virtual DataTable loadDataTableSestavaPosZakazka(DateTime dateFrom, DateTime dateTo)
        {
            return loadDataTable("select vyrobek, round(sum(cena * pocetks)::numeric,3) as cena  from poskozeno where datum >= ? and datum <= ?  group by vyrobek order by vyrobek", dateFrom, dateTo);
        }


        public virtual DataTable loadDataTableSestavaPosKonto(DateTime dateFrom, DateTime dateTo)
        {
            return loadDataTable("select konto, round(sum(cena * pocetks)::numeric,3) as cena  from poskozeno where datum >= ? and datum <= ?  group by konto order by konto", dateFrom, dateTo);
        }

        public virtual DataTable loadDataTableSestavaPosZaKonto(DateTime dateFrom, DateTime dateTo, string konto)
        {
            return loadDataTable("select nazev, csn, jk, datum, pocetks, round(cena::numeric ,3) as cena, round((cena *pocetks)::numeric,3) as celkcena  from poskozeno where datum >= ? and datum <= ? and konto = ?  order by datum", dateFrom, dateTo, konto);
        }

        public virtual DataTable loadDataTableSestavaPosZaZakazku(DateTime dateFrom, DateTime dateTo, string vyrobek)
        {
            return loadDataTable("select nazev, csn, jk, datum, pocetks, round(cena::numeric ,3) as cena, round((cena *pocetks)::numeric,3) as celkcena  from poskozeno where datum >= ? and datum <= ? and vyrobek = ?  order by datum", dateFrom, dateTo, vyrobek);
        }



    }
}
