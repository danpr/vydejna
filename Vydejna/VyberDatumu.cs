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
    public partial class VyberDatumu : Form
    {
        public VyberDatumu()
        {
            InitializeComponent();
        }

        public DateTime dateFrom
        {
            get { return dateTimePickerFrom.Value;}
            set { dateTimePickerFrom.Value = value;}
        }

        public DateTime dateTo
        {
            get { return dateTimePickerTo.Value; }
            set { dateTimePickerTo.Value = value; }
        }


    }
}
