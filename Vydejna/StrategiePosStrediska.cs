using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace Vydejna
{
    class StrategiePosStrediska : ISestava1
    {
        public Hashtable getHeaderLabels()
        {
            Hashtable headerLabels = new Hashtable();
            headerLabels.Add("dilna", "Pracoviště");
            headerLabels.Add("cena", "Cena");
            headerLabels.Add("procenta", "Procenta");
            return headerLabels;
        }


        public Boolean existTextVyber()
        {
            return false;
        }

        public string getTextVyberLabel()
        {
            return "Středisko";
        }

        public string getWindowHeader()
        {
            return "Vyhodnocení poškozenek dle střediska";
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

            Decimal suma = makeSum(dt);
            if (suma > 0)
            {
                if ((dt.Columns.Contains("cena")) && (dt.Columns.Contains("procenta")))
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        dt.Rows[x]["procenta"] = (Convert.ToDecimal(dt.Rows[x]["cena"]) / suma) * 100;
                    }
                }
            }
        }


        public DataTable loadDataTable(vDatabase myDataBase, DateTime dateTimeFrom, DateTime dateTimeTo, string text1)
        {
            return myDataBase.loadDataTableSestavaPosStrediska(dateTimeFrom, dateTimeTo);
        }

        public string getNameStrategy()
        {
            return "posstred";
        }


    }
}
