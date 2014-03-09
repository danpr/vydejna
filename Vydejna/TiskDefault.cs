using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace Vydejna
{
    

    class TiskDefault
    {
        private Hashtable DBRow;
        protected PrintDocument tiskDoc;


        public TiskDefault(Hashtable DBRow)
        {
            this.DBRow = DBRow;
        }

        protected void setPreview()
        {
            PrintPreviewDialog nahled = new PrintPreviewDialog();
            tiskDoc = new PrintDocument();

            PrinterSettings nastaveniTisku = new PrinterSettings();
            nastaveniTisku.PrinterName = "Microsoft XPS Document Writer";
            nastaveniTisku.Duplex = Duplex.Horizontal;
            tiskDoc.PrinterSettings = nastaveniTisku;

            nahled.Document = tiskDoc;
            nahled.ShowDialog();
        }

    }
}
