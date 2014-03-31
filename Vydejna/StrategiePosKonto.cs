using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;


namespace Vydejna
{
    class StrategiePosKonto : ISestava1
    {
        public Hashtable getHeaderLabels()
        {
            Hashtable headerLabels = new Hashtable();
            headerLabels.Add("konto", "Konto");
            headerLabels.Add("cena", "Cena");
            return headerLabels;
        }

        public Boolean existTextVyber()
        {
            return false;
        }

        public string getTextVyberLabel()
        {
            return "";
        }

        public string getWindowHeader()
        {
            return "Vyhodnocení poškozenek dle zakázky";
        }
        public Decimal makeSum(DataTable dt, string column)
        {
            if (column.Trim() != "")
            {
                if (dt.Columns.Contains(column))
                {
                    Decimal suma = 0;

                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        suma = suma + Convert.ToDecimal(dt.Rows[x][column]);
                    }
                    return suma;
                }
            }
            return 0;
        }

        public void makeSumProcent(DataTable dt)
        {
        }

        public DataTable loadDataTable(vDatabase myDataBase, DateTime dateTimeFrom, DateTime dateTimeTo, string text1)
        {
            return myDataBase.loadDataTableSestavaPosKonto(dateTimeFrom, dateTimeTo);
        }

    }
}
