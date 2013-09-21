using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vydejna
{
    public partial class PrijemkaMaterialu : Form
    {



        public PrijemkaMaterialu()
        {
            InitializeComponent();
        }


        public PrijemkaMaterialu(Hashtable DBRow, vDatabase myDataBase)
        {
            InitializeComponent();
        }



        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
