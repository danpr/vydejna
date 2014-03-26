﻿using System;
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

        protected ISestava1 strategie;

//        public SestavaDefault(vDatabase myDataBase, string nadpis, string vyber)
        public SestavaDefault(vDatabase myDataBase, ISestava1 strategie)

    {
            InitializeComponent();
            this.strategie = strategie;
            this.myDataBase = myDataBase;


            dataGridViewSestava.MultiSelect = false;
            dataGridViewSestava.ReadOnly = true;
            dataGridViewSestava.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewSestava.RowHeadersVisible = false;
            dataGridViewSestava.AllowUserToAddRows = false;
            dataGridViewSestava.AllowUserToDeleteRows = false;
            dataGridViewSestava.AllowUserToResizeRows = false;
            dataGridViewSestava.AllowUserToOrderColumns = true;
            dataGridViewSestava.Columns.Clear();
            dataGridViewSestava.DataSource = null;



            this.Text = strategie.windowHeader();
            if (strategie.existTextVyber())
            {
                labelVyber.Text = strategie.textVyberLabel();
            }
            else
            {
                hideTextVyber();
            }
        }

        public virtual DataTable loadDataTable()
        {
            return null;
        }

        public virtual void loadData()
        {
            dataGridViewSestava.DataSource = strategie.loadDataTable(myDataBase, dateTimePickerFrom.Value, dateTimePickerTo.Value);
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
        }

        protected void makeSum(string column)
        {
            if (column.Trim() != "")
            {
                if ((dataGridViewSestava.DataSource as DataTable).Columns.Contains(column))
                {
                    Int32 suma = 0;

                    for (int x = 0; x < (dataGridViewSestava.DataSource as DataTable).Rows.Count; x++)
                    {
                    suma = suma + Convert.ToInt32((dataGridViewSestava.DataSource as DataTable).Rows[x][column]);

                    }
                    labelCelkem.Text = Convert.ToString(suma);
                }
            }
        }





    }
}
