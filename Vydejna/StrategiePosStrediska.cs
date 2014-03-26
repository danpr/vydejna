using System;
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

        public string textVyberLabel()
        {
            return "Středisko";
        }

        public string windowHeader()
        {
            return "Vyhodnocení poškozenek dle střediska";

        }

        public void makeSum(string column)
        {
        }

        public DataTable loadDataTable(vDatabase myDataBase, DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            return myDataBase.loadDataTableSestavaPosStrediska(dateTimeFrom, dateTimeTo);
        }

    }
}
