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

        string textVyberLabel();

        string windowHeader();

        void makeSum(string column);

        DataTable loadDataTable(vDatabase myDataBase, DateTime dateTimeFrom, DateTime dateTimeTo);
    }
}
