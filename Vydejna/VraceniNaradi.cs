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
    public partial class VraceniNaradi : Form
    {
        public VraceniNaradi(Hashtable DBRow)
        {
            InitializeComponent();

            buttonOK.Enabled = false;

            labelPrijmeni.Text = Convert.ToString(DBRow["prijmeni"]);
            labelJmeno.Text = Convert.ToString(DBRow["jmeno"]);
            labelOsCislo.Text = Convert.ToString(DBRow["oscislo"]);
            labelStredisko.Text = Convert.ToString(DBRow["stredisko"]);
            labelProvoz.Text = Convert.ToString(DBRow["odeleni"]);

        }
    }
}
