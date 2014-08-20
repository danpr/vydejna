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
    class StrategiePosOsobyZaStred : ISestava1
    {

        protected Font tiskFont9 = new Font("Verdana", 9);
        protected Font tiskFont9b = new Font("Verdana", 9, FontStyle.Bold);
        protected Font tiskFont11b = new Font("Verdana", 11, FontStyle.Bold);

        protected Pen Pen3 = new Pen(Brushes.Black, 0.3F);
        protected Pen Pen5 = new Pen(Brushes.Black, 0.5F);
        
        public Hashtable getHeaderLabels()
        {
            Hashtable headerLabels = new Hashtable();
            headerLabels.Add("krjmeno", "Jméno");
            headerLabels.Add("jmeno", "Přijmení");
            headerLabels.Add("oscislo", "Os. číslo");
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
            return "Vyhodnocení poškozenek dle pracovníků";
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
            return myDataBase.loadDataTableSestavaPosOsobyZaStred(dateTimeFrom, dateTimeTo,text1);
        }

        public string getNameStrategy()
        {
            return "oszastred";
        }

        public DataTable loadDataPrintTable()
        {
            return null;
        }

        public Int32 getRowsOnPrintPage()
        {
            return 36;
        }

        public Int32 getHighRowOnPrintPage()
        {
            return 7;
        }

        public void printLine(PrintPageEventArgs e, Hashtable DBRow, Int32 posY)
        {
            e.Graphics.DrawString(Convert.ToString(DBRow["krjmeno"]), tiskFont9, Brushes.Black, new PointF(25, posY));
            e.Graphics.DrawString(Convert.ToString(DBRow["jmeno"]), tiskFont9, Brushes.Black, new PointF(75, posY));
            e.Graphics.DrawString(Convert.ToString(DBRow["oscislo"]), tiskFont9, Brushes.Black, new PointF(125, posY));
            e.Graphics.DrawString(Convert.ToString(DBRow["cena"]), tiskFont9, Brushes.Black, new PointF(155, posY));
            e.Graphics.DrawLine(Pen5, new Point(5, posY + 5), new Point(200, posY + 5));

        }

        public void printHeader(PrintPageEventArgs e, Int32 posY)
        {
            e.Graphics.DrawString(getWindowHeader(), tiskFont11b, Brushes.Black, new PointF(60, posY));
            e.Graphics.DrawString("Jmeno", tiskFont9b, Brushes.Black, new PointF(25, posY + 10));
            e.Graphics.DrawString("Prijmeni", tiskFont9b, Brushes.Black, new PointF(75, posY + 10));
            e.Graphics.DrawString("Os. číslo", tiskFont9b, Brushes.Black, new PointF(125, posY + 10));
            e.Graphics.DrawString("Cena", tiskFont9b, Brushes.Black, new PointF(155, posY + 10));
            e.Graphics.DrawLine(Pen3, new Point(5, posY + 15), new Point(200, posY + 15));

        }

            
    }
}
