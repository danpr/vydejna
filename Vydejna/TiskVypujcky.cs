using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;
using System.Data;


namespace Vydejna
{
    class TiskVypujcky : TiskDefault
    {

        const Int32 hightRow = 7;

        public TiskVypujcky(vDatabase myDB, Hashtable DBRow)
            : base(myDB, DBRow)
        {
            RowsOnPage = 29;
            //            setPreview();
            setPrint(); // aktivuje tisk
        }


        protected override void printHeader(PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;

            e.Graphics.DrawString("Strana :", tiskFont9, Brushes.Black, new PointF(5, 7));
            e.Graphics.DrawString("Datum :", tiskFont9, Brushes.Black, new PointF(168, 7));

            e.Graphics.DrawString(Convert.ToString(pageNumber), tiskFont9, Brushes.Black, new PointF(20, 7));
            e.Graphics.DrawString(DateTime.Today.ToString("d"), tiskFont9, Brushes.Black, new PointF(183, 7));

            e.Graphics.DrawString("Seznam nářadí ", tiskFont11, Brushes.Black, new PointF(50, 25));

            e.Graphics.DrawString("Středisko:", tiskFont9, Brushes.Black, new PointF(150, 17));
            e.Graphics.DrawString("Provoz:", tiskFont9, Brushes.Black, new PointF(150, 25));

            e.Graphics.DrawString("Jméno", tiskFont9, Brushes.Black, new PointF(25, 47));
            e.Graphics.DrawString("Číslo známky", tiskFont9, Brushes.Black, new PointF(60, 47));
            e.Graphics.DrawString("Nářadí dle seznamu", tiskFont9, Brushes.Black, new PointF(100, 47));
            e.Graphics.DrawString("Vrácení nářadí", tiskFont9, Brushes.Black, new PointF(165, 47));
            e.Graphics.DrawString("Osobní číslo:", tiskFont9, Brushes.Black, new PointF(56, 63));
            e.Graphics.DrawString("Dne", tiskFont9, Brushes.Black, new PointF(90, 55));
            e.Graphics.DrawString("Vydal", tiskFont9, Brushes.Black, new PointF(108, 55));
            e.Graphics.DrawString("Převzal", tiskFont9, Brushes.Black, new PointF(133, 55));
            e.Graphics.DrawString("Dne", tiskFont9, Brushes.Black, new PointF(159, 55));
            e.Graphics.DrawString("Vydal", tiskFont9, Brushes.Black, new PointF(175, 55));




            e.Graphics.DrawString("Datum", tiskFont9, Brushes.Black, new PointF(5, 81));
            e.Graphics.DrawString("Název", tiskFont9, Brushes.Black, new PointF(25, 81));
            e.Graphics.DrawString("Rozměr", tiskFont9, Brushes.Black, new PointF(50, 81));

            string celeJmeno = "";
            if (DBRow.Contains("jmeno"))
            {
                if (DBRow.Contains("prijmeni"))
                {
                    celeJmeno = Convert.ToString(DBRow["jmeno"]) + " " + Convert.ToString(DBRow["prijmeni"]);
                }
                else
                {
                    celeJmeno = Convert.ToString(DBRow["jmeno"]);
                }
            }
            else
            {
                celeJmeno = Convert.ToString(DBRow["jmeno"]);
            }

//            e.Graphics.DrawString(celeJmeno, tiskFont9, Brushes.Black, new PointF(8, 58));
            if (DBRow.Contains("jmeno"))
                e.Graphics.DrawString(Convert.ToString(DBRow["jmeno"]), tiskFont11, Brushes.Black, new PointF(10, 55));
            if (DBRow.Contains("prijmeni"))
                e.Graphics.DrawString(Convert.ToString(DBRow["prijmeni"]), tiskFont11, Brushes.Black, new PointF(10, 63));

            e.Graphics.DrawString(DateTime.Today.ToString("d"), tiskFont9, Brushes.Black, new PointF(88, 63));

            if (DBRow.Contains("oscislo"))
                e.Graphics.DrawString(Convert.ToString(DBRow["oscislo"]), tiskFont9, Brushes.Black, new PointF(77, 63));
            if (DBRow.Contains("cisznamky"))
                e.Graphics.DrawString(Convert.ToString(DBRow["cisznamky"]), tiskFont9, Brushes.Black, new PointF(65, 55));
            if (DBRow.Contains("stredisko"))
                e.Graphics.DrawString(Convert.ToString(DBRow["stredisko"]), tiskFont9, Brushes.Black, new PointF(168, 17));
            if (DBRow.Contains("odeleni"))
                e.Graphics.DrawString(Convert.ToString(DBRow["odeleni"]), tiskFont9, Brushes.Black, new PointF(163, 25));


            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(5, 15, 195, 54));
            e.Graphics.DrawLine(Pens.Black, new Point(5, 31), new Point(200, 31));
            e.Graphics.DrawLine(Pens.Black, new Point(149, 23), new Point(200, 23));

            e.Graphics.DrawLine(Pens.Black, new Point(5, 45), new Point(200, 45));
            e.Graphics.DrawLine(Pens.Black, new Point(88, 53), new Point(200, 53));
            e.Graphics.DrawLine(Pens.Black, new Point(55, 61), new Point(200, 61));


            e.Graphics.DrawLine(Pens.Black, new Point(149, 15), new Point(149, 45));
            e.Graphics.DrawLine(Pens.Black, new Point(88, 32), new Point(88, 68));
            e.Graphics.DrawLine(Pens.Black, new Point(55, 45), new Point(55, 68));
            e.Graphics.DrawLine(Pens.Black, new Point(157, 45), new Point(157, 68));
            e.Graphics.DrawLine(Pens.Black, new Point(173, 53), new Point(173, 68));
            e.Graphics.DrawLine(Pens.Black, new Point(131, 53), new Point(131, 68));
            e.Graphics.DrawLine(Pens.Black, new Point(106, 53), new Point(106, 68));



            e.Graphics.DrawLine(Pens.Black, new Point(5, 88), new Point(200, 88));

        }


        protected override void printLine(PrintPageEventArgs e, Int32 line)
        {
            DateTime mydate = Convert.ToDateTime(dataTableRows.Rows[DTnumberSelectedRow]["datum"]);
            e.Graphics.DrawString(mydate.Date.ToString("d"), tiskFont9, Brushes.Black, new PointF(5, line * hightRow + 90));
            e.Graphics.DrawString(Convert.ToString(dataTableRows.Rows[DTnumberSelectedRow]["nazev"]), tiskFont9, Brushes.Black, new PointF(25, line * hightRow + 90));
//            e.Graphics.DrawString(Convert.ToString(dataTableRows.Rows[DTnumberSelectedRow]["poznamka"]), tiskFont9, Brushes.Black, new PointF(50, line * hightRow + 85));
//            e.Graphics.DrawString(Convert.ToString(dataTableRows.Rows[DTnumberSelectedRow]["vevcislo"]), tiskFont9, Brushes.Black, new PointF(90, line * hightRow + 85));
//            e.Graphics.DrawString(Convert.ToString(dataTableRows.Rows[DTnumberSelectedRow]["prijem"]), tiskFont9, Brushes.Black, new PointF(120, line * hightRow + 85));
//            e.Graphics.DrawString(Convert.ToString(dataTableRows.Rows[DTnumberSelectedRow]["vydej"]), tiskFont9, Brushes.Black, new PointF(140, line * hightRow + 85));
//            e.Graphics.DrawString(Convert.ToString(dataTableRows.Rows[DTnumberSelectedRow]["zustatek"]), tiskFont9, Brushes.Black, new PointF(160, line * hightRow + 85));
//            e.Graphics.DrawString(Convert.ToString(dataTableRows.Rows[DTnumberSelectedRow]["zapkarta"]), tiskFont9, Brushes.Black, new PointF(180, line * hightRow + 85));
        }


        protected override DataTable loadDataTable()
        {
            if (DBRow.Contains("oscislo"))
            {
                return myDB.loadDataTableVypujcenoNaOsobuNext(Convert.ToString(DBRow["oscislo"]));
            }
            else
            {
                return null;
            }
        }



    }
}
