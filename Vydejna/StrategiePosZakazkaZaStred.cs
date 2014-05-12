using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;


namespace Vydejna
{
    class StrategiePosZakazkaZaStred : ISestava1
    {

        public Hashtable getHeaderLabels()
        {
            Hashtable headerLabels = new Hashtable();
            headerLabels.Add("vyrobek", "Zakázka");
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
            return "Vyhodnocení poškozenek dle zakazky za středisko";
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
            return myDataBase.loadDataTableSestavaPosZakazkaZaStred(dateTimeFrom, dateTimeTo, text1);
        }
    
    }
}
