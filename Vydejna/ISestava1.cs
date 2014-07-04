using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Drawing.Printing;
using System.Drawing;



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

        string getNameStrategy();

        DataTable loadDataPrintTable();

        Int32 getRowsOnPrintPage();

        void printLine(PrintPageEventArgs e, Hashtable DBRow, Int32 posY);

        void printHeader(PrintPageEventArgs e, Int32 posY);

    }
}
