using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;


namespace Vydejna
{
    class TiskNaradi : TiskDefault
    {
        private Hashtable DBRow;

        public TiskNaradi(Hashtable DBRow)
            : base(DBRow)
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


            e.Graphics.DrawString("Nazev nářadí :", tiskFont1, Brushes.Black, new PointF(10, 17));
            e.Graphics.DrawString("Číslo položky :", tiskFont1, Brushes.Black, new PointF(10, 24));
            e.Graphics.DrawString("Norma ČSN :", tiskFont1, Brushes.Black, new PointF(10, 31));
            e.Graphics.DrawString("Norma DIN :", tiskFont1, Brushes.Black, new PointF(10, 38));
            e.Graphics.DrawString("Rozměr :", tiskFont1, Brushes.Black, new PointF(100, 38));
            e.Graphics.DrawString("Výrobce :", tiskFont1, Brushes.Black, new PointF(10, 45));
            e.Graphics.DrawString("Cena/ks :", tiskFont1, Brushes.Black, new PointF(10, 52));
            e.Graphics.DrawString("Účet :", tiskFont1, Brushes.Black, new PointF(70, 52));
            e.Graphics.DrawString("Účetní cena :", tiskFont1, Brushes.Black, new PointF(30, 52));
            e.Graphics.DrawString("Min. stav :", tiskFont1, Brushes.Black, new PointF(10, 59));
            e.Graphics.DrawString("Fyz. stav stav :", tiskFont1, Brushes.Black, new PointF(80, 59));
            e.Graphics.DrawString("Účetní stav :", tiskFont1, Brushes.Black, new PointF(130, 59));
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
                e.Graphics.DrawString(Convert.ToString(DBRow["rozmer"]), tiskFont1, Brushes.Black, new PointF(120, 38));
            if (DBRow.Contains("vyrobce"))
                e.Graphics.DrawString(Convert.ToString(DBRow["vyrobce"]), tiskFont1, Brushes.Black, new PointF(30, 45));


        }

        public override void Tisk(object sender, PrintPageEventArgs e)
        {

            printHeader(e);
        
            // while (count < pocetRadekNaStrance)
            //{
            // count++
            //}

//            if (line != prazdna tabulka)
//                ev.HasMorePages = true;
//            else
//                ev.HasMorePages = false;

        }
    }
}
