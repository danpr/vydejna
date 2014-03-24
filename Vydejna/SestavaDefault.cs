using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vydejna
{
    public partial class SestavaDefault : Form
    {

        protected vDatabase myDataBase;


        public SestavaDefault(vDatabase myDataBase, string nadpis, string vyber)
        {
            InitializeComponent();
            this.myDataBase = myDataBase;

//            CancelButton = buttonCancel;

            dataGridViewSestava.MultiSelect = false;
            dataGridViewSestava.ReadOnly = true;

            dataGridViewSestava.RowHeadersVisible = false;
            dataGridViewSestava.AllowUserToAddRows = false;
            dataGridViewSestava.AllowUserToDeleteRows = false;
            dataGridViewSestava.AllowUserToResizeRows = false;
            dataGridViewSestava.AllowUserToOrderColumns = true;
            dataGridViewSestava.Columns.Clear();
            dataGridViewSestava.DataSource = null;



            this.Text = nadpis;
            if (vyber == "")
            {
                hideTextVyber();
            }
            else
            {
                labelVyber.Text = vyber;
            }
        }

        public virtual DataTable loadDataTable()
        {
            return null;
        }

        public virtual void loadData()
        {
            dataGridViewSestava.DataSource = loadDataTable();
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
        }
    }
}
