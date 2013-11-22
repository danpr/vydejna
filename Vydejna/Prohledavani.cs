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
    public partial class Prohledavani : Form
    {
        DataGridView myDataGridView;

        public Prohledavani(DataGridView myDataGridView)
        {
            InitializeComponent();

            this.myDataGridView = myDataGridView;
            textBoxString.Enabled = false;
            comboBoxNumeric.Enabled = false;
            numericUpDownNumeric.Enabled = false;
            comboBoxDate.Enabled = false;
            dateTimePickerDate.Enabled = false;
            loadComboBox();
        }

        private void loadComboBox()
    {
        comboBoxColumns.Items.Clear();
        for (int i = 0; i < myDataGridView.ColumnCount; i++ )
        {
            if ((myDataGridView.Columns[i].Visible))
            {
            comboBoxColumns.Items.Add(myDataGridView.Columns[i].HeaderText.ToString());
            }

        }
    }


    }
}
