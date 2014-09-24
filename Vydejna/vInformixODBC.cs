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
                    MessageBox.Show("Tabulky byly smazány.");
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
                }
            }
        }
        //ALTER TABLE gsvady LOCK MODE (ROW)   ALTER TABLE gsvady LOCK MODE (PAGE)


        public override Boolean ZamykaniStranek()
        {
            return setStyleLockDB(true);
        }

        private Boolean setStyleLockDB ( Boolean page)
        {
            openDB();
            if (DBIsOpened())
            {
                OdbcCommand cmdNaradi;
                OdbcCommand cmdKarta;
                OdbcCommand cmdVraceno;
                OdbcCommand cmdPoskozeno;
                OdbcCommand cmdOsoby;
                OdbcCommand cmdZmeny;
                OdbcCommand cmdPujceno;
                if (page == true)
                {
                    cmdNaradi = new OdbcCommand("ALTER TABLE naradi LOCK MODE (PAGE)", myDBConn as OdbcConnection);
                    cmdKarta = new OdbcCommand("ALTER TABLE karta LOCK MODE (PAGE)", myDBConn as OdbcConnection);
                    cmdVraceno = new OdbcCommand("ALTER TABLE vraceno LOCK MODE (PAGE)", myDBConn as OdbcConnection);
                    cmdPoskozeno = new OdbcCommand("ALTER TABLE poskozeno LOCK MODE (PAGE)", myDBConn as OdbcConnection);
                    cmdOsoby = new OdbcCommand("ALTER TABLE osoby LOCK MODE (PAGE)", myDBConn as OdbcConnection);
                    cmdZmeny = new OdbcCommand("ALTER TABLE zmeny LOCK MODE (PAGE)", myDBConn as OdbcConnection);
                    cmdPujceno = new OdbcCommand("ALTER TABLE pujceno LOCK MODE (PAGE)", myDBConn as OdbcConnection);
                }
                else
                {
                    cmdNaradi = new OdbcCommand("ALTER TABLE naradi LOCK MODE (ROW)", myDBConn as OdbcConnection);
                    cmdKarta = new OdbcCommand("ALTER TABLE karta LOCK MODE (ROW)", myDBConn as OdbcConnection);
                    cmdVraceno = new OdbcCommand("ALTER TABLE vraceno LOCK MODE (ROW)", myDBConn as OdbcConnection);
                    cmdPoskozeno = new OdbcCommand("ALTER TABLE poskozeno LOCK MODE (ROW)", myDBConn as OdbcConnection);
                    cmdOsoby = new OdbcCommand("ALTER TABLE osoby LOCK MODE (ROW)", myDBConn as OdbcConnection);
                    cmdZmeny = new OdbcCommand("ALTER TABLE zmeny LOCK MODE (ROW)", myDBConn as OdbcConnection);
                    cmdPujceno = new OdbcCommand("ALTER TABLE pujceno LOCK MODE (ROW)", myDBConn as OdbcConnection);
                }

                try
                {
                    cmdNaradi.ExecuteNonQuery();
                    cmdKarta.ExecuteNonQuery();
                    cmdVraceno.ExecuteNonQuery();
                    cmdPoskozeno.ExecuteNonQuery();
                    cmdOsoby.ExecuteNonQuery();
                    cmdZmeny.ExecuteNonQuery();
                    cmdPujceno.ExecuteNonQuery();
                    MessageBox.Show("Nastavení zamku bylo změněno.");
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
                }
            }
            return true;
        }

        public override Boolean ZamykaniRadek()
        {
            return setStyleLockDB(false);
        }


    }
}
