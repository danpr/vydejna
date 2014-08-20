using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Drawing.Printing;
using System.Drawing;


namespace Vydejna
{
    class StrategiePosZaKonto : ISestava1
    {
        public Hashtable getHeaderLabels()
        {
            Hashtable headerLabels = new Hashtable();
            headerLabels.Add("nazev", "Název");
            headerLabels.Add("csn", "Norma ČSN");
            headerLabels.Add("jk", "Číslo položky");
            headerLabels.Add("datum", "Datum");
            headerLabels.Add("pocetks", "Počet ks");
            headerLabels.Add("cena", "Cena");
            headerLabels.Add("celkcena", "Cena celkem");
            return headerLabels;
        }

        public Boolean existTextVyber()
        {
            return true;
        }

        public string getTextVyberLabel()
        {
            return "Konto";
        }

        public string getWindowHeader()
        {
            return "Seznam poškozenek za konto";
        }

        public Decimal makeSum(DataTable dt)
        {
                if (dt.Columns.Contains("celkcena"))
                {
                    Decimal suma = 0;

                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        suma = suma + Convert.ToDecimal(dt.Rows[x]["celkcena"]);
                    }
                    return suma;
                }
            return 0;
        }

        public void makeSumProcent(DataTable dt)
        {
        }

        public DataTable loadDataTable(vDatabase myDataBase, DateTime dateTimeFrom, DateTime dateTimeTo, string text1)
        {
            return myDataBase.loadDataTableSestavaPosZaKonto(dateTimeFrom, dateTimeTo, text1);
        }
        public string getNameStrategy()
        {
            return "poszakonto";
        }

        public DataTable loadDataPrintTable()
        {
            return null;
        }

        public Int32 getRowsOnPrintPage()
        {
            return 20;
        }

        public Int32 getHighRowOnPrintPage()
        {
            return 7;
        }

        public void printLine(PrintPageEventArgs e, Hashtable DataRow, Int32 posY)
        {

        }

        public void printHeader(PrintPageEventArgs e, Int32 posY)
        {

        }

    }
}
