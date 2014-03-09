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

        void Tisk(object sender, PrintPageEventArgs e)
        {
            Font tiskFont1 = new Font("Verdana", 20);
            e.Graphics.DrawString("Test", tiskFont1, Brushes.Black, new PointF(50, 50));

            e.Graphics.DrawLine(Pens.Black, new Point(10, 100), new Point(200, 100));
        }
    }
}
