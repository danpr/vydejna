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
            buttonOK.Enabled = false;
            setDefaultDate();
            setButtonOk();
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

        private void setDefaultDate()
        {
            dateTimePickerFrom.Value = DateTime.Now;
            dateTimePickerTo.Value = DateTime.Now;
        }

        public DateTime dateFromValue
        {
            get { return dateTimePickerFrom.Value; }
            set { dateTimePickerFrom.Value = value; }
        }


        public DateTime dateToValue
        {
            get { return dateTimePickerTo.Value; }
            set { dateTimePickerTo.Value = value; }
        }


       private void setButtonOk()
       {
           if (dateTimePickerFrom.Value > dateTimePickerTo.Value)
           {
               buttonOK.Enabled = false;
           }
           else
           {
               buttonOK.Enabled = true;
           }
       }


       private void dateTimePicker_ValueChanged(object sender, EventArgs e)
       {
           setButtonOk();
       }

        

    }
}
