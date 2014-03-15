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
        private Hashtable DBRow;

        private Int32 pageNumber;
        private Int32 DTnumberSelectedRow;
        private DataTable dataTableZmeny;
        private Int32  DTRowCount;

        public TiskNaradi(vDatabase myDB, Hashtable DBRow)
            : base(myDB, DBRow)
        {
            this.DBRow = DBRow;
            setPreview();
            tiskDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(Tisk);

        }


        private void printHeader(PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            Font tiskFont1 = new Font("Verdana", 11);
            Font tiskFont2 = new Font("Verdana", 9);

            e.Graphics.DrawString("Strana :", tiskFont2, Brushes.Black, new PointF(10, 7));
            e.Graphics.DrawString("Datum :", tiskFont2, Brushes.Black, new PointF(150, 7));

            e.Graphics.DrawString( Convert.ToString(pageNumber), tiskFont2, Brushes.Black, new PointF(25, 7));
            e.Graphics.DrawString(DateTime.Today.ToString("d"), tiskFont2, Brushes.Black, new PointF(165, 7));

            e.Graphics.DrawString("Nazev nářadí :", tiskFont1, Brushes.Black, new PointF(10, 17));
            e.Graphics.DrawString("Číslo položky :", tiskFont1, Brushes.Black, new PointF(10, 24));
            e.Graphics.DrawString("Norma ČSN :", tiskFont1, Brushes.Black, new PointF(10, 31));
            e.Graphics.DrawString("Norma DIN :", tiskFont1, Brushes.Black, new PointF(10, 38));
            e.Graphics.DrawString("Rozměr :", tiskFont1, Brushes.Black, new PointF(80, 38));
            e.Graphics.DrawString("Výrobce :", tiskFont1, Brushes.Black, new PointF(10, 45));
            e.Graphics.DrawString("Cena/ks :", tiskFont1, Brushes.Black, new PointF(10, 52));
            e.Graphics.DrawString("Účet :", tiskFont1, Brushes.Black, new PointF(80, 52));
            e.Graphics.DrawString("Účetní cena :", tiskFont1, Brushes.Black, new PointF(150, 52));
            e.Graphics.DrawString("Min. stav :", tiskFont1, Brushes.Black, new PointF(10, 59));
            e.Graphics.DrawString("Fyz. stav stav :", tiskFont1, Brushes.Black, new PointF(80, 59));
            e.Graphics.DrawString("Účetní stav :", tiskFont1, Brushes.Black, new PointF(150, 59));
            e.Graphics.DrawString("Poznámka :", tiskFont1, Brushes.Black, new PointF(10, 66));


            e.Graphics.DrawString("Datum", tiskFont2, Brushes.Black, new PointF(5, 75));
            e.Graphics.DrawString("Stav", tiskFont2, Brushes.Black, new PointF(30, 75));
            e.Graphics.DrawString("Poznámka", tiskFont2, Brushes.Black, new PointF(70, 75));
            e.Graphics.DrawString("Interni", tiskFont2, Brushes.Black, new PointF(150, 72));
            e.Graphics.DrawString("ev. číslo", tiskFont2, Brushes.Black, new PointF(150, 78));

            if (DBRow.Contains("nazev"))
                e.Graphics.DrawString(Convert.ToString(DBRow["nazev"]), tiskFont1, Brushes.Black, new PointF(40, 17));
            if (DBRow.Contains("jk"))
                e.Graphics.DrawString(Convert.ToString(DBRow["jk"]), tiskFont1, Brushes.Black, new PointF(40, 24));
            if (DBRow.Contains("normacsn"))
                e.Graphics.DrawString(Convert.ToString(DBRow["normacsn"]), tiskFont1, Brushes.Black, new PointF(38, 31));
            if (DBRow.Contains("normadin"))
                e.Graphics.DrawString(Convert.ToString(DBRow["normadin"]), tiskFont1, Brushes.Black, new PointF(38, 38));
            if (DBRow.Contains("rozmer"))
                e.Graphics.DrawString(Convert.ToString(DBRow["rozmer"]), tiskFont1, Brushes.Black, new PointF(100, 38));
            if (DBRow.Contains("vyrobce"))
                e.Graphics.DrawString(Convert.ToString(DBRow["vyrobce"]), tiskFont1, Brushes.Black, new PointF(30, 45));
            if (DBRow.Contains("cena"))
                e.Graphics.DrawString(Convert.ToString(DBRow["cena"]), tiskFont1, Brushes.Black, new PointF(30, 52));
            if (DBRow.Contains("analucet"))
                e.Graphics.DrawString(Convert.ToString(DBRow["analucet"]), tiskFont1, Brushes.Black, new PointF(100, 52));
            if (DBRow.Contains("celkcena"))
                e.Graphics.DrawString(Convert.ToString(DBRow["celkcena"]), tiskFont1, Brushes.Black, new PointF(180, 52));
            if (DBRow.Contains("minimum"))
                e.Graphics.DrawString(Convert.ToString(DBRow["minimum"]), tiskFont1, Brushes.Black, new PointF(35, 59));
            if (DBRow.Contains("fyzstav"))
                e.Graphics.DrawString(Convert.ToString(DBRow["fyzstav"]), tiskFont1, Brushes.Black, new PointF(115, 59));
            if (DBRow.Contains("ucetstav"))
                e.Graphics.DrawString(Convert.ToString(DBRow["ucetstav"]), tiskFont1, Brushes.Black, new PointF(180, 59));
            if (DBRow.Contains("poznamka"))
                e.Graphics.DrawString(Convert.ToString(DBRow["poznamka"]), tiskFont1, Brushes.Black, new PointF(40, 66));


            
        }


        public override void BeginTisk(object sender, PrintEventArgs e)
        {
            pageNumber = 1;

            //DataTable loadDataTableZmeny
            dataTableZmeny = null;
            if (DBRow.Contains("poradi"))
            {
                dataTableZmeny = myDB.loadDataTableZmeny(Convert.ToInt32(DBRow["poradi"]));
                DTnumberSelectedRow = 0;
                DTRowCount = dataTableZmeny.Rows.Count;
            }

        }



        public override void Tisk(object sender, PrintPageEventArgs e)
        {
            Int32 printRowsOnPage = 0;
            Int32 RowsOnPage = 18;
            Font tiskFont2 = new Font("Verdana", 9);

            printHeader(e);

            if (dataTableZmeny != null)
            {


                while ((printRowsOnPage < RowsOnPage) && (DTnumberSelectedRow < DTRowCount))
                {


                    string mydate = Convert.ToString( dataTableZmeny.Rows[DTnumberSelectedRow]["datum"]);

 //                   if (DBRow.Contains("poznamka"))
                        e.Graphics.DrawString(Convert.ToString(dataTableZmeny.Rows[DTnumberSelectedRow]["datum"]), tiskFont2, Brushes.Black, new PointF(5, printRowsOnPage * 8 + 85));
                        e.Graphics.DrawString(Convert.ToString(dataTableZmeny.Rows[DTnumberSelectedRow]["stav"]), tiskFont2, Brushes.Black, new PointF(40, printRowsOnPage * 8 + 85));
                        e.Graphics.DrawString(Convert.ToString(dataTableZmeny.Rows[DTnumberSelectedRow]["poznamka"]), tiskFont2, Brushes.Black, new PointF(80, printRowsOnPage * 8 + 85));
                        printRowsOnPage++;
                    DTnumberSelectedRow++;
                }
            }

            if (DTnumberSelectedRow != DTRowCount)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
            pageNumber++;
        }
    }
}
