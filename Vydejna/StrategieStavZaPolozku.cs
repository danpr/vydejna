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
    class StrategieStavZaPolozku : ISestava1
    {
        protected Font tiskFont9 = new Font("Verdana", 9);
        protected Font tiskFont9b = new Font("Verdana", 9, FontStyle.Bold);
        protected Font tiskFont11b = new Font("Verdana", 11, FontStyle.Bold);

        protected Pen Pen3 = new Pen(Brushes.Black, 0.3F);
        protected Pen Pen5 = new Pen(Brushes.Black, 0.5F);

        public Hashtable getHeaderLabels()
        {
            Hashtable headerLabels = new Hashtable();
            headerLabels.Add("prijem", "Příjmuto");
            headerLabels.Add("vydej", "Vydáno");
            headerLabels.Add("datum", "Datum");
            headerLabels.Add("zapkarta", "Karta");
            headerLabels.Add("poznamka", "Poznámka");
            return headerLabels;
        }


        public Boolean existTextVyber()
        {
            return true;
        }

        public string getTextVyberLabel()
        {
            return "Položka";
        }

        public string getWindowHeader()
        {
            return "Seznam změn za položku";
        }

        public Decimal makeSum(DataTable dt)
        {
            if (dt.Columns.Contains("prijem"))
            {
                Decimal suma = 0;

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    suma = suma + Convert.ToDecimal(dt.Rows[x]["prijem"]);
                }
                return suma;
            }
            
            return 0;
        }

        public string getSumPreLabel()
        {
            return "Přijmuto : ";
        }


        public Decimal makeSumExt2(DataTable dt)
        {
            if (dt.Columns.Contains("vydej"))
            {
                Decimal suma = 0;

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    suma = suma + Convert.ToDecimal(dt.Rows[x]["vydej"]);
                }
                return suma;
            }

            return 0;
        }

        public string getSumPreLabelExt2()
        {
            return "Vydáno : ";
        }



        public void makeSumProcent(DataTable dt)
        {
        }

        public DataTable loadDataTable(vDatabase myDataBase, DateTime dateTimeFrom, DateTime dateTimeTo, string text1)
        {
            return myDataBase.loadDataTableSestavaStavZaPolozku(dateTimeFrom, dateTimeTo, text1);
        }

        public string getNameStrategy()
        {
            return "stavzapol";
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
            return 7;
        }


        public void printLine(PrintPageEventArgs e, Hashtable DataRow, Int32 posY)
        {
            e.Graphics.DrawString(Convert.ToString(DataRow["prijem"]), tiskFont9, Brushes.Black, new PointF(10, posY));
            e.Graphics.DrawString(Convert.ToString(DataRow["vydej"]), tiskFont9, Brushes.Black, new PointF(45, posY));
            e.Graphics.DrawString(Convert.ToString(DataRow["zapkarta"]), tiskFont9, Brushes.Black, new PointF(80, posY));
            e.Graphics.DrawString(Convert.ToString(DataRow["poznamka"]), tiskFont9, Brushes.Black, new PointF(120, posY));
            e.Graphics.DrawLine(Pen5, new Point(5, posY + 5), new Point(200, posY + 5));

        }

        public void printHeader(PrintPageEventArgs e, Int32 posY)
        {
            e.Graphics.DrawString(getWindowHeader(), tiskFont11b, Brushes.Black, new PointF(60, posY));
            e.Graphics.DrawString("Příjem", tiskFont9b, Brushes.Black, new PointF(10, posY + 10));
            e.Graphics.DrawString("Výdej", tiskFont9b, Brushes.Black, new PointF(45, posY + 10));
            e.Graphics.DrawString("Karta", tiskFont9b, Brushes.Black, new PointF(80, posY + 10));
            e.Graphics.DrawString("Poznámka", tiskFont9b, Brushes.Black, new PointF(120, posY + 10));
            e.Graphics.DrawLine(Pen3, new Point(5, posY + 15), new Point(200, posY + 15));

        }


    }
}

