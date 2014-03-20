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
    class TiskNaradi : TiskDefault
    {
        const Int32 hightRow = 7;

        public TiskNaradi(vDatabase myDB, Hashtable DBRow)
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

            e.Graphics.DrawString( Convert.ToString(pageNumber), tiskFont9, Brushes.Black, new PointF(20, 7));
            e.Graphics.DrawString(DateTime.Today.ToString("d"), tiskFont9, Brushes.Black, new PointF(183, 7));

            e.Graphics.DrawString("Název nářadí :", tiskFont11, Brushes.Black, new PointF(10, 17));
            e.Graphics.DrawString("Číslo položky :", tiskFont11, Brushes.Black, new PointF(10, 24));
            e.Graphics.DrawString("Norma ČSN :", tiskFont11, Brushes.Black, new PointF(10, 31));
            e.Graphics.DrawString("Norma DIN :", tiskFont11, Brushes.Black, new PointF(10, 38));
            e.Graphics.DrawString("Rozměr :", tiskFont11, Brushes.Black, new PointF(80, 38));
            e.Graphics.DrawString("Výrobce :", tiskFont11, Brushes.Black, new PointF(10, 45));
            e.Graphics.DrawString("Cena/ks :", tiskFont11, Brushes.Black, new PointF(10, 52));
            e.Graphics.DrawString("Účet :", tiskFont11, Brushes.Black, new PointF(80, 52));
            e.Graphics.DrawString("Účetní cena :", tiskFont11, Brushes.Black, new PointF(150, 52));
            e.Graphics.DrawString("Min. stav :", tiskFont11, Brushes.Black, new PointF(10, 59));
            e.Graphics.DrawString("Fyz. stav stav :", tiskFont11, Brushes.Black, new PointF(80, 59));
            e.Graphics.DrawString("Účetní stav :", tiskFont11, Brushes.Black, new PointF(150, 59));
            e.Graphics.DrawString("Poznámka :", tiskFont11, Brushes.Black, new PointF(10, 66));


            e.Graphics.DrawString("Datum", tiskFont9, Brushes.Black, new PointF(5, 76));
            e.Graphics.DrawString("Stav", tiskFont9, Brushes.Black, new PointF(25, 76));
            e.Graphics.DrawString("Poznámka", tiskFont9, Brushes.Black, new PointF(50, 76));
            e.Graphics.DrawString("Interni", tiskFont9, Brushes.Black, new PointF(90, 74));
            e.Graphics.DrawString("ev. číslo", tiskFont9, Brushes.Black, new PointF(90, 78));
            e.Graphics.DrawString("Příjem", tiskFont9, Brushes.Black, new PointF(120, 74));
            e.Graphics.DrawString("ks", tiskFont9, Brushes.Black, new PointF(120, 78));
            e.Graphics.DrawString("Výdej", tiskFont9, Brushes.Black, new PointF(140, 74));
            e.Graphics.DrawString("ks", tiskFont9, Brushes.Black, new PointF(140, 78));
            e.Graphics.DrawString("Stav", tiskFont9, Brushes.Black, new PointF(160, 74));
            e.Graphics.DrawString("ks", tiskFont9, Brushes.Black, new PointF(160, 78));
            e.Graphics.DrawString("Osobní", tiskFont9, Brushes.Black, new PointF(180, 74));
            e.Graphics.DrawString("číslo", tiskFont9, Brushes.Black, new PointF(180, 78));

            if (DBRow.Contains("nazev"))
                e.Graphics.DrawString(Convert.ToString(DBRow["nazev"]), tiskFont11, Brushes.Black, new PointF(40, 17));
            if (DBRow.Contains("jk"))
                e.Graphics.DrawString(Convert.ToString(DBRow["jk"]), tiskFont11, Brushes.Black, new PointF(40, 24));
            if (DBRow.Contains("normacsn"))
                e.Graphics.DrawString(Convert.ToString(DBRow["normacsn"]), tiskFont11, Brushes.Black, new PointF(38, 31));
            if (DBRow.Contains("normadin"))
                e.Graphics.DrawString(Convert.ToString(DBRow["normadin"]), tiskFont11, Brushes.Black, new PointF(38, 38));
            if (DBRow.Contains("rozmer"))
                e.Graphics.DrawString(Convert.ToString(DBRow["rozmer"]), tiskFont11, Brushes.Black, new PointF(100, 38));
            if (DBRow.Contains("vyrobce"))
                e.Graphics.DrawString(Convert.ToString(DBRow["vyrobce"]), tiskFont11, Brushes.Black, new PointF(30, 45));
            if (DBRow.Contains("cena"))
                e.Graphics.DrawString(Convert.ToString(DBRow["cena"]), tiskFont11, Brushes.Black, new PointF(30, 52));
            if (DBRow.Contains("analucet"))
                e.Graphics.DrawString(Convert.ToString(DBRow["analucet"]), tiskFont11, Brushes.Black, new PointF(100, 52));
            if (DBRow.Contains("celkcena"))
                e.Graphics.DrawString(Convert.ToString(DBRow["celkcena"]), tiskFont11, Brushes.Black, new PointF(180, 52));
            if (DBRow.Contains("minimum"))
                e.Graphics.DrawString(Convert.ToString(DBRow["minimum"]), tiskFont11, Brushes.Black, new PointF(35, 59));
            if (DBRow.Contains("fyzstav"))
                e.Graphics.DrawString(Convert.ToString(DBRow["fyzstav"]), tiskFont11, Brushes.Black, new PointF(115, 59));
            if (DBRow.Contains("ucetstav"))
                e.Graphics.DrawString(Convert.ToString(DBRow["ucetstav"]), tiskFont11, Brushes.Black, new PointF(180, 59));
            if (DBRow.Contains("poznamka"))
                e.Graphics.DrawString(Convert.ToString(DBRow["poznamka"]), tiskFont11, Brushes.Black, new PointF(40, 66));

           e.Graphics.DrawRectangle(Pens.Black,new Rectangle(5,15,195,57));
           e.Graphics.DrawLine(Pens.Black, new Point(5, 83), new Point(200, 83));
        }


        protected override void printLine(PrintPageEventArgs e, Int32 line)
        {
            DateTime mydate = Convert.ToDateTime(dataTableRows.Rows[DTnumberSelectedRow]["datum"]);
            e.Graphics.DrawString(mydate.Date.ToString("d"), tiskFont9, Brushes.Black, new PointF(5, line * hightRow + 85));
            e.Graphics.DrawString(Convert.ToString(dataTableRows.Rows[DTnumberSelectedRow]["stav"]), tiskFont9, Brushes.Black, new PointF(25, line * hightRow + 85));
            e.Graphics.DrawString(Convert.ToString(dataTableRows.Rows[DTnumberSelectedRow]["poznamka"]), tiskFont9, Brushes.Black, new PointF(50, line * hightRow + 85));
            e.Graphics.DrawString(Convert.ToString(dataTableRows.Rows[DTnumberSelectedRow]["vevcislo"]), tiskFont9, Brushes.Black, new PointF(90, line * hightRow + 85));
            e.Graphics.DrawString(Convert.ToString(dataTableRows.Rows[DTnumberSelectedRow]["prijem"]), tiskFont9, Brushes.Black, new PointF(120, line * hightRow + 85));
            e.Graphics.DrawString(Convert.ToString(dataTableRows.Rows[DTnumberSelectedRow]["vydej"]), tiskFont9, Brushes.Black, new PointF(140, line * hightRow + 85));
            e.Graphics.DrawString(Convert.ToString(dataTableRows.Rows[DTnumberSelectedRow]["zustatek"]), tiskFont9, Brushes.Black, new PointF(160, line * hightRow + 85));
            e.Graphics.DrawString(Convert.ToString(dataTableRows.Rows[DTnumberSelectedRow]["zapkarta"]), tiskFont9, Brushes.Black, new PointF(180, line * hightRow + 85));
        }


        protected override DataTable loadDataTable()
        {
            if (DBRow.Contains("poradi"))
            {
                return myDB.loadDataTableZmeny(Convert.ToInt32(DBRow["poradi"]));
            }
            else
            {
                return null;
            }
        }

    
    }
}
