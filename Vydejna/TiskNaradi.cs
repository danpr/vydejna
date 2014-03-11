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

        public TiskNaradi(Hashtable DBRow)
            : base(DBRow)
        {
            setPreview();
            tiskDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(Tisk);

        }

        public override void Tisk(object sender, PrintPageEventArgs e)
        {
            Font tiskFont1 = new Font("Verdana", 15);
            e.Graphics.DrawString("Nazev :", tiskFont1, Brushes.Black, new PointF(50, 50));
            e.Graphics.DrawString("Číslo položky :", tiskFont1, Brushes.Black, new PointF(50, 70));

            e.Graphics.DrawLine(Pens.Black, new Point(10, 100), new Point(200, 100));
        }
    }
}
