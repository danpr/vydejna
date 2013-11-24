using System;
using System.Collections;
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
        public class ColumnInfo
        {
            public string varColumnType;
            public string name;

            public ColumnInfo (string varColumnType, string name)
            {
            this.varColumnType = varColumnType;
            this.name = name;
            }
        }


        private DataGridView myDataGridView;
        private ArrayList comboBoxColumnInfo;

        public Prohledavani(DataGridView myDataGridView)
        {
            InitializeComponent();

            comboBoxColumnInfo = new ArrayList();

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

        DataTable mdt = (DataTable)myDataGridView.DataSource;


        comboBoxColumns.Items.Clear();
        for (int i = 0; i < myDataGridView.ColumnCount; i++ )
        {
            if ((myDataGridView.Columns[i].Visible))
            {

 //               string hs = mdt.Columns[i].DataType.ToString();
                string ns = mdt.Columns[i].DataType.ToString().Substring(mdt.Columns[i].DataType.ToString().IndexOf(".") + 1);
                if ((ns == "Int64") && (ns == "Int32") && (ns == "Int16") && (ns == "Double"))
                {
                    ns = "Numeric";
                }
                ColumnInfo myColumnInfo = new ColumnInfo(ns, myDataGridView.Columns[i].Name);
                comboBoxColumns.Items.Add(myDataGridView.Columns[i].HeaderText.ToString());
                comboBoxColumnInfo.Add(myColumnInfo);

            }

        }
    }

        private void comboBoxColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            // vybrani
            if (comboBoxColumns.SelectedIndex != -1)
            {
                textBoxString.Enabled = false;
                ColumnInfo myColumnInfo = (ColumnInfo)comboBoxColumnInfo[comboBoxColumns.SelectedIndex];
                string myType = Convert.ToString(myColumnInfo.varColumnType);
                if (myType == "System.String")
                {
                    textBoxString.Enabled = true;
                }
 
            }
        }


    }
}
