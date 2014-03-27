using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Vydejna
{
    class StrategiePosOsobyZaStred : ISestava1
    {
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
            return myDataBase.loadDataTableSestavaPosOsobyZaStred(dateTimeFrom, dateTimeTo,text1);
        }

            
    }
}
