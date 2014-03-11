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

            PrintDialog tiskDlg = new PrintDialog();

        }

        protected void setPreview()
        {
            tiskDoc = new PrintDocument();

            tiskDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(Tisk);


            PrintPreviewDialog nahled = new PrintPreviewDialog();
            nahled.Document = tiskDoc;

            nahled.ShowDialog();
        }


        protected void setPrint()
        {
            tiskDoc = new PrintDocument();

            PrintDialog tiskDlg = new PrintDialog();
            tiskDlg.Document = tiskDoc;

            tiskDlg.AllowSomePages = false;
            //tiskDlg.PrinterSettings.FromPage = 1;
            //tiskDlg.PrinterSettings.ToPage = 10;
            tiskDlg.AllowPrintToFile = false;


            if (tiskDlg.ShowDialog() == DialogResult.OK)
            {

                PrinterSettings nastaveniTisku = new PrinterSettings();
                nastaveniTisku.PrinterName = tiskDlg.PrinterSettings.PrinterName;
                nastaveniTisku.Duplex = tiskDlg.PrinterSettings.Duplex;

                tiskDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(Tisk);
                //tiskDoc.PrinterSettings = nastaveniTisku;


                PrintPreviewDialog nahled = new PrintPreviewDialog();
                nahled.Document = tiskDoc;
                nahled.ShowDialog();
            }
        }


         public virtual void Tisk(object sender, PrintPageEventArgs e)
        {

        }


    }
}
