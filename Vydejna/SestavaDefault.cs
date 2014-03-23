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
        public SestavaDefault(string nadpis, string vyber)
        {
            InitializeComponent();
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

        public void hideTextVyber()
        {
            labelVyber.Hide();
            labelVyber.Enabled = false;
            textBoxVyber.Hide();
            textBoxVyber.Enabled = false;
        }

        private void buttonRetry_Click(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
