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
    public partial class ZapujceneNaradiInfo : Form
    {
        private Hashtable DBRow;

        public ZapujceneNaradiInfo(Hashtable DBRow)
        {
            InitializeComponent();
            this.DBRow = DBRow;
            setData();
        }


        private void setData()
        {
            if (DBRow.Contains("nazev")) labelNazev.Text = Convert.ToString(DBRow["nazev"]);
            else labelNazev.Text = "";
            if (DBRow.Contains("rozmer")) labelRozmer.Text = Convert.ToString(DBRow["rozmer"]);
            else labelRozmer.Text = "";
            if (DBRow.Contains("jk")) labelJK.Text = Convert.ToString(DBRow["jk"]);
            else labelJK.Text = "";
            if (DBRow.Contains("normacsn")) labelCSN.Text = Convert.ToString(DBRow["normacsn"]);
            else labelCSN.Text = "";
            //----------------------
            if (DBRow.Contains("pnazev")) labelPNazev.Text = Convert.ToString(DBRow["pnazev"]);
            else labelPNazev.Text = "";


        }
    }
}
