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
    class StrategiePosOsobyZaStred : ISestava1
    {
        public Hashtable getHeaderLabels()
        {
            Hashtable headerLabels = new Hashtable();
            headerLabels.Add("krjmeno", "Jméno");
            headerLabels.Add("jmeno", "Přijmení");
            headerLabels.Add("oscislo", "Os. číslo");
            headerLabels.Add("cena", "Cena");
            return headerLabels;
        }

        public Boolean existTextVyber()
        {
            return true;
        }

        public string getTextVyberLabel()
        {
            return "Středisko";
        }

        public string getWindowHeader()
        {
            return "Vyhodnocení poškozenek dle pracovníků";
        }

        public Decimal makeSum(DataTable dt)
        {
                if (dt.Columns.Contains("cena"))
                {
                    Decimal suma = 0;

                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        suma = suma + Convert.ToDecimal(dt.Rows[x]["cena"]);
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
            return myDataBase.loadDataTableSestavaPosOsobyZaStred(dateTimeFrom, dateTimeTo,text1);
        }

        public string getNameStrategy()
        {
            return "oszastred";
        }

        public DataTable loadDataPrintTable()
        {
            return null;
        }

        public Int32 getRowsOnPrintPage()
        {
            return 20;
        }

        public void printLine(PrintPageEventArgs e, Hashtable DataRow, Int32 posY)
        {

        }

        public void printHeader(PrintPageEventArgs e, Int32 posY)
        {

        }

            
    }
}
