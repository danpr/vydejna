using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing;
using System.Data;



namespace Vydejna
{
    

    class TiskDefault
    {

        protected vDatabase myDB;

        protected Hashtable DBRow;
        protected PrintDocument tiskDoc;

        protected Font tiskFont11 = new Font("Verdana", 11);
        protected Font tiskFont9 = new Font("Verdana", 9);

        protected Int32 pageNumber;  // cislo stranky
        protected Int32 DTnumberSelectedRow; // cislo vybrane radky v datatable
        protected Int32 DTRowCount; // pocet radku v datatable

        protected Int32 RowsOnPage = 1;


        protected DataTable dataTableRows;



        public TiskDefault(vDatabase myDB, Hashtable DBRow)
        {
            this.myDB = myDB;
            this.DBRow = DBRow;
            RowsOnPage = 1;

            PrintDialog tiskDlg = new PrintDialog();

        }

        protected void setPreview()
        {
            tiskDoc = new PrintDocument();           

            tiskDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(Tisk);
            tiskDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(BeginTisk);

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
                tiskDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(BeginTisk);
                //tiskDoc.PrinterSettings = nastaveniTisku;

                PrintPreviewDialog nahled = new PrintPreviewDialog();
                nahled.Document = tiskDoc;
                nahled.ShowDialog();
            }
        }


        protected virtual DataTable loadDataTable()
        {
            return null;
        }

        protected virtual void printHeader(PrintPageEventArgs e)
        {
        }

        protected virtual void printLine(PrintPageEventArgs e, Int32 line)
        {
        }

        public virtual void Tisk(object sender, PrintPageEventArgs e)
        {
            Int32 lineOnPage = 0;

            printHeader(e);

            if (dataTableRows != null)
            {
                while ((lineOnPage < RowsOnPage) && (DTnumberSelectedRow < DTRowCount))
                {
                    printLine(e, lineOnPage);
                    lineOnPage++;
                    DTnumberSelectedRow++;
                }
            }

            if (DTnumberSelectedRow != DTRowCount)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
            pageNumber++;
        }

         public virtual void BeginTisk(object sender, PrintEventArgs e)
         {
             pageNumber = 1;
             DTnumberSelectedRow = 0;

             dataTableRows = null;

             dataTableRows = loadDataTable();

             loadDataTable();

             if (dataTableRows != null)
             {
                 DTRowCount = dataTableRows.Rows.Count;
             }
         }

    }
}
