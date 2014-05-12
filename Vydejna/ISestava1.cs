using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;


namespace Vydejna
{
    public interface ISestava1
    {
        Boolean existTextVyber();

        string getTextVyberLabel();

        string getWindowHeader();

        Decimal makeSum(DataTable dt);

        void makeSumProcent(DataTable dt);

        DataTable loadDataTable(vDatabase myDataBase, DateTime dateTimeFrom, DateTime dateTimeTo, string text1);

        Hashtable getHeaderLabels();
    }
}
