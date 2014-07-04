using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Drawing.Printing;
using System.Drawing;

namespace Vydejna
{
    class StrategiePosStrediska : ISestava1
    {

        protected Font tiskFont9 = new Font("Verdana", 9);
        protected Font tiskFont9b = new Font("Verdana", 9, FontStyle.Bold);
        protected Font tiskFont11b = new Font("Verdana", 11, FontStyle.Bold);

        protected Pen Pen3 = new Pen(Brushes.Black, 0.3F);
        protected Pen Pen5 = new Pen(Brushes.Black, 0.5F);

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

        public DataTable loadDataPrintTable()
        {
            return null;
        }

        public Int32 getRowsOnPrintPage()
        {
            return 36;
        }

        public void printLine(PrintPageEventArgs e, Hashtable DBRow, Int32 posY)
        {

            e.Graphics.DrawString(Convert.ToString(DBRow["dilna"]),tiskFont9, Brushes.Black, new PointF(25, posY));
            e.Graphics.DrawString(Convert.ToString(DBRow["cena"]), tiskFont9, Brushes.Black, new PointF(75, posY));
            e.Graphics.DrawString(Convert.ToString(DBRow["procenta"]), tiskFont9, Brushes.Black, new PointF(165, posY));
            e.Graphics.DrawLine(Pen5, new Point(5, posY + 5), new Point(200, posY + 5));


        }

        public void printHeader(PrintPageEventArgs e, Int32 posY)
        {

            e.Graphics.DrawString(getWindowHeader(), tiskFont11b, Brushes.Black, new PointF(60, posY));
            
            e.Graphics.DrawString("Pracoviště", tiskFont9b, Brushes.Black, new PointF(25, posY+10));
            e.Graphics.DrawString("Cena", tiskFont9b, Brushes.Black, new PointF(75, posY+10));
            e.Graphics.DrawString("Procenta", tiskFont9b, Brushes.Black, new PointF(165, posY+10));
            e.Graphics.DrawLine(Pen3, new Point(5, posY + 15), new Point(200, posY+15));

        }


    }
}
