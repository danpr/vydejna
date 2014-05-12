using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace Vydejna
{
    class StrategiePosZaOsobu : ISestava1
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
            return "Os. číslo";
        }

        public string getWindowHeader()
        {
            return "Seznam poškozenek za osobu";
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
            return myDataBase.loadDataTableSestavaPosZaOsobu(dateTimeFrom, dateTimeTo, text1);
        }

    }
}
