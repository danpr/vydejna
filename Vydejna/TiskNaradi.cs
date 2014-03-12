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

            e.Graphics.DrawString("Strana :", tiskFont2, Brushes.Black, new PointF(50, 30));
            e.Graphics.DrawString("Datum :", tiskFont2, Brushes.Black, new PointF(450, 30));


            e.Graphics.DrawString("Nazev nářadí :", tiskFont1, Brushes.Black, new PointF(50, 50));
            e.Graphics.DrawString("Číslo položky :", tiskFont1, Brushes.Black, new PointF(50, 70));
            e.Graphics.DrawString("Norma ČSN :", tiskFont1, Brushes.Black, new PointF(50, 90));
            e.Graphics.DrawString("Norma DIN :", tiskFont1, Brushes.Black, new PointF(50, 110));
            e.Graphics.DrawString("Rozměr :", tiskFont1, Brushes.Black, new PointF(250, 110));
            e.Graphics.DrawString("Výrobce :", tiskFont1, Brushes.Black, new PointF(50, 130));
            e.Graphics.DrawString("Cena/ks :", tiskFont1, Brushes.Black, new PointF(50, 150));
            e.Graphics.DrawString("Účet :", tiskFont1, Brushes.Black, new PointF(150, 150));
            e.Graphics.DrawString("Účetní cena :", tiskFont1, Brushes.Black, new PointF(250, 150));
            e.Graphics.DrawString("Min. stav :", tiskFont1, Brushes.Black, new PointF(50, 170));
            e.Graphics.DrawString("Fyz. stav stav :", tiskFont1, Brushes.Black, new PointF(150, 170));
            e.Graphics.DrawString("Účetní stav :", tiskFont1, Brushes.Black, new PointF(250, 170));
            e.Graphics.DrawString("Poznámka :", tiskFont1, Brushes.Black, new PointF(50, 190));


            e.Graphics.DrawString("Datum", tiskFont2, Brushes.Black, new PointF(30, 240));
            e.Graphics.DrawString("Stav", tiskFont2, Brushes.Black, new PointF(80, 240));
            e.Graphics.DrawString("Poznámka", tiskFont2, Brushes.Black, new PointF(150, 240));
            e.Graphics.DrawString("Interni", tiskFont2, Brushes.Black, new PointF(250, 235));
            e.Graphics.DrawString("ev. číslo", tiskFont2, Brushes.Black, new PointF(250, 245));

            if (DBRow.Contains("nazev"))
                e.Graphics.DrawString(Convert.ToString(DBRow["nazev"]), tiskFont1, Brushes.Black, new PointF(200, 50));
            if (DBRow.Contains("jk"))
                e.Graphics.DrawString(Convert.ToString(DBRow["jk"]), tiskFont1, Brushes.Black, new PointF(200, 70));

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
