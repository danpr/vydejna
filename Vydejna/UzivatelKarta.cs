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
    public partial class UzivatelKarta : Form
    {
        public UzivatelKarta()
        {
            InitializeComponent();

            this.CancelButton = buttonCancel;
            this.AcceptButton = buttonOK;
            radioButton1.Checked = true;


        }
    }
}
