﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Vydejna
{
    class StrategiePosStrediska : ISestava1
    {
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

            Decimal suma = makeSum(dt, "cena");
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


        public DataTable loadDataTable(vDatabase myDataBase, DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            return myDataBase.loadDataTableSestavaPosStrediska(dateTimeFrom, dateTimeTo);
        }

    }
}
