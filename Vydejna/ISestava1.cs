using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace Vydejna
{
    public interface ISestava1
    {
        Boolean existTextVyber();

        string getTextVyberLabel();

        string getWindowHeader();

        Decimal makeSum(DataTable dt, string column);

        void makeSumProcent(DataTable dt);

        DataTable loadDataTable(vDatabase myDataBase, DateTime dateTimeFrom, DateTime dateTimeTo, string text1);
    }
}
