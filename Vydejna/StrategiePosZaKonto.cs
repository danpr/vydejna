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
    class StrategiePosZaKonto : ISestava1
    {
        protected Font tiskFont9 = new Font("Verdana", 9);
        protected Font tiskFont9b = new Font("Verdana", 9, FontStyle.Bold);
        protected Font tiskFont11b = new Font("Verdana", 11, FontStyle.Bold);

        protected Pen Pen3 = new Pen(Brushes.Black, 0.3F);
        protected Pen Pen5 = new Pen(Brushes.Black, 0.5F);

        public Hashtable getHeaderLabels()
        {
            Hashtable headerLabels = new Hashtable();
            headerLabels.Add("nazev", "Název");
            headerLabels.Add("csn", "Norma ČSN");
            headerLabels.Add("jk", "Číslo položky");
            headerLabels.Add("datum", "Datum");
            headerLabels.Add("pocetks", "Počet ks");
            headerLabels.Add("cena", "Cena");
            headerLabels.Add("celkcena", "Cena celkem");
            return headerLabels;
        }

        public Boolean existTextVyber()
        {
            return true;
        }

        public string getTextVyberLabel()
        {
            return "Konto";
        }

        public string getWindowHeader()
        {
            return "Seznam poškozenek za konto";
        }

        public Decimal makeSum(DataTable dt)
        {
                if (dt.Columns.Contains("celkcena"))
                {
                    Decimal suma = 0;

                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        suma = suma + Convert.ToDecimal(dt.Rows[x]["celkcena"]);
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
            return myDataBase.loadDataTableSestavaPosZaKonto(dateTimeFrom, dateTimeTo, text1);
        }
        public string getNameStrategy()
        {
            return "poszakonto";
        }

        public DataTable loadDataPrintTable()
        {
            return null;
        }

        public Int32 getRowsOnPrintPage()
        {
            return 17;
        }

        public Int32 getHighRowOnPrintPage()
        {
            return 14;
        }

        public void printLine(PrintPageEventArgs e, Hashtable DataRow, Int32 posY)
        {
            e.Graphics.DrawString(Convert.ToString(DataRow["nazev"]), tiskFont9, Brushes.Black, new PointF(10, posY));
            e.Graphics.DrawString(Convert.ToString(DataRow["csn"]), tiskFont9, Brushes.Black, new PointF(10, posY + 5));
            e.Graphics.DrawString(Convert.ToString(DataRow["jk"]), tiskFont9, Brushes.Black, new PointF(65, posY + 5));
            e.Graphics.DrawString(Convert.ToString(DataRow["datum"]), tiskFont9, Brushes.Black, new PointF(100, posY + 5));
            e.Graphics.DrawString(Convert.ToString(DataRow["pocetks"]), tiskFont9, Brushes.Black, new PointF(140, posY + 5));
            e.Graphics.DrawString(Convert.ToString(DataRow["cena"]), tiskFont9, Brushes.Black, new PointF(160, posY + 5));
            e.Graphics.DrawString(Convert.ToString(DataRow["celkcena"]), tiskFont9, Brushes.Black, new PointF(180, posY + 5));
            e.Graphics.DrawLine(Pen5, new Point(5, posY + 10), new Point(200, posY + 10));
        }

        public void printHeader(PrintPageEventArgs e, Int32 posY)
        {
            e.Graphics.DrawString(getWindowHeader(), tiskFont11b, Brushes.Black, new PointF(60, posY));
            e.Graphics.DrawString("Název/ČSN", tiskFont9b, Brushes.Black, new PointF(10, posY + 10));
            //            e.Graphics.DrawString("ČSN", tiskFont9b, Brushes.Black, new PointF(55, posY + 10));
            e.Graphics.DrawString("Číslo položky", tiskFont9b, Brushes.Black, new PointF(65, posY + 10));
            e.Graphics.DrawString("Datum", tiskFont9b, Brushes.Black, new PointF(100, posY + 10));
            e.Graphics.DrawString("Ks", tiskFont9b, Brushes.Black, new PointF(140, posY + 10));
            e.Graphics.DrawString("Cena", tiskFont9b, Brushes.Black, new PointF(160, posY + 10));
            e.Graphics.DrawString("Celkem", tiskFont9b, Brushes.Black, new PointF(180, posY + 10));
            e.Graphics.DrawLine(Pen3, new Point(5, posY + 15), new Point(200, posY + 15));
        }

    }
}
