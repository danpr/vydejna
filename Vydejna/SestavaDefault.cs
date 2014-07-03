using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing;

namespace Vydejna
{
    public partial class SestavaDefault : Form
    {

        const Int32 hightRow = 7;


        private enum evenStateEnum { enable, disable };
        private evenStateEnum evenState = evenStateEnum.disable;

        protected vDatabase myDataBase;
        protected ISestava1 strategie;

        // ---  tisk dat
        protected PrintDocument tiskDoc;

        protected Font tiskFont25 = new Font("Verdana", 25);
        protected Font tiskFont11 = new Font("Verdana", 11);
        protected Font tiskFont11b = new Font("Verdana", 11, FontStyle.Bold);
        protected Font tiskFont9 = new Font("Verdana", 9);

        protected Pen PenDefault = new Pen(Brushes.Black);
        protected Pen Pen3 = new Pen(Brushes.Black, 0.3F);
        protected Pen Pen5 = new Pen(Brushes.Black, 0.5F);

        protected Int32 pageNumber;  // cislo stranky
        protected Int32 DTnumberSelectedRow; // cislo vybrane radky v datatable
        protected Int32 DTRowCount; // pocet radku v datatable

        protected Int32 RowsOnPage = 1;

        protected DataTable dataTablePrintRows;


        public SestavaDefault(vDatabase myDataBase, ISestava1 strategie, Font myFont)
        {
            evenState = evenStateEnum.disable;
            InitializeComponent();

            // jak menit meritko
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.Font = myFont;

            this.strategie = strategie;
            this.myDataBase = myDataBase;


            dataGridViewSestava.MultiSelect = false;
            dataGridViewSestava.ReadOnly = true;
            dataGridViewSestava.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewSestava.RowHeadersVisible = false;
            dataGridViewSestava.AllowUserToAddRows = false;
            dataGridViewSestava.AllowUserToDeleteRows = false;
            dataGridViewSestava.AllowUserToResizeRows = false;
            dataGridViewSestava.AllowUserToOrderColumns = false;
            dataGridViewSestava.Columns.Clear();
            dataGridViewSestava.DataSource = null;

            this.Text = strategie.getWindowHeader();
            if (strategie.existTextVyber())
            {
                labelVyber.Text = strategie.getTextVyberLabel();
            }
            else
            {
                hideTextVyber();
            }
        }


        public virtual void loadData()
        {
            dataGridViewSestava.DataSource = strategie.loadDataTable(myDataBase, dateTimePickerFrom.Value, dateTimePickerTo.Value,textBoxVyber.Text);
            Hashtable headerRow = strategie.getHeaderLabels();
            foreach (DictionaryEntry pair in headerRow)
            {
                dataGridViewSestava.Columns[Convert.ToString( pair.Key)].HeaderText = Convert.ToString( pair.Value);
            }



        }

        protected void hideTextVyber()
        {
            labelVyber.Hide();
            labelVyber.Enabled = false;
            textBoxVyber.Hide();
            textBoxVyber.Enabled = false;
        }

        protected DateTime getDateFrom()
        {
            return dateTimePickerFrom.Value;
        }


        protected DateTime getDateTo()
        {
            return dateTimePickerTo.Value;
        }


        private void buttonRetry_Click(object sender, EventArgs e)
        {
            loadData();
            makeSum();
            strategie.makeSumProcent(dataGridViewSestava.DataSource as DataTable);
            setColumnWidth();
            evenState = evenStateEnum.enable; // povolime ukladat zmeny sloupcu

        }

        private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            dataGridViewSestava.DataSource = null;
        }

        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {
            dataGridViewSestava.DataSource = null;
        }

        private void textBoxVyber_TextChanged(object sender, EventArgs e)
        {
            dataGridViewSestava.DataSource = null;
        }


        protected virtual void makeSum()
        {
            labelCelkem.Text = Convert.ToString(strategie.makeSum(dataGridViewSestava.DataSource as DataTable));
        }

        protected void makeSum(string column)
        {
            if (column.Trim() != "")
            {
                if ((dataGridViewSestava.DataSource as DataTable).Columns.Contains(column))
                {
                    Decimal suma = 0;

                    for (int x = 0; x < (dataGridViewSestava.DataSource as DataTable).Rows.Count; x++)
                    {
                    suma = suma + Convert.ToDecimal((dataGridViewSestava.DataSource as DataTable).Rows[x][column]);

                    }
                    labelCelkem.Text = Convert.ToString(suma);
                }
            }
        }


        private void setHeaderLabels()
        {
          Hashtable headerLabels = strategie.getHeaderLabels();
          for (Int32 i = 0; i < headerLabels.Count; i++)
          {
//              DataGridView.Columns["poradi"].HeaderText = "Pořadí";

          }



        }

        private void dataGridViewSestava_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            // ulozeni velikosti
            if (evenState == evenStateEnum.enable)
            {
                
                if (strategie.getNameStrategy() != "")
                {
                    ConfigReg.saveSettingWindowTableColumnWidth("REPORT", strategie.getNameStrategy(), e.Column.Name, e.Column.Width);
                }
            }

        }

        private void SestavaDefault_Load(object sender, EventArgs e)
        {
//            evenState = evenStateEnum.enable;
        }

        public void setColumnWidth()
        {
            Hashtable DBTableInfo = ConfigReg.loadSettingWindowTableColumnWidth("REPORT", strategie.getNameStrategy());
            if (DBTableInfo != null)
            {
                for (Int32 i = 0; i < dataGridViewSestava.Columns.Count; i++)
                {
                    string myColumnName = dataGridViewSestava.Columns[i].Name;
                    if (DBTableInfo.ContainsKey(myColumnName))
                    {
                        try
                        {
                            dataGridViewSestava.Columns[i].Width = Convert.ToInt32(DBTableInfo[myColumnName]);
                        }
                        catch { }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // tisk
            RowsOnPage = strategie.getRowsOnPrintPage();
            setPrint();
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

        public virtual void BeginTisk(object sender, PrintEventArgs e)
        {
            pageNumber = 1;
            DTnumberSelectedRow = 0;
            DTRowCount = dataGridViewSestava.Rows.Count;
        }


        public virtual void Tisk(object sender, PrintPageEventArgs e)
        {
            Int32 lineOnPage = 0;

            printHeader(e);

            while ((lineOnPage < RowsOnPage) && (DTnumberSelectedRow < DTRowCount))
            {
                printLine(e, lineOnPage, DTnumberSelectedRow);
                lineOnPage++;
                DTnumberSelectedRow++;
            }

            if (DTnumberSelectedRow != DTRowCount)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
            pageNumber++;
        }

        protected virtual DataTable loadDataPrintTable()
        {
            return strategie.loadDataPrintTable();
        }

        protected virtual void printHeader(PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;

            e.Graphics.DrawString("Strana :", tiskFont9, Brushes.Black, new PointF(5, 7));
            e.Graphics.DrawString("Datum :", tiskFont9, Brushes.Black, new PointF(168, 7));

            e.Graphics.DrawString(Convert.ToString(pageNumber), tiskFont9, Brushes.Black, new PointF(20, 7));
            e.Graphics.DrawString(DateTime.Today.ToString("d"), tiskFont9, Brushes.Black, new PointF(183, 7));

        }

        protected virtual void printLine(PrintPageEventArgs e, Int32 line, Int32 numberSelectedRow)
        {
            Hashtable DBRow = new Hashtable();
            DataGridViewRow myRow = dataGridViewSestava.Rows[numberSelectedRow];
            for (int i = 0; i < dataGridViewSestava.ColumnCount; i++)
            {
                if (DBRow.ContainsKey(dataGridViewSestava.Columns[i].Name))
                {
                    DBRow.Remove(dataGridViewSestava.Columns[i].Name);
                }
                DBRow.Add(dataGridViewSestava.Columns[i].Name, myRow.Cells[i].Value);
            }
            strategie.printLine(e, DBRow, line * hightRow + 85);

        }


    }
}
