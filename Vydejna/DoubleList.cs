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
    public partial class DoubleList : Form
    {
        public DoubleList(List<String> mainStringList, List<String> selectStringList)
        {
            InitializeComponent();
            this.CancelButton = this.buttonCancel;
            this.AcceptButton = this.buttonOK;

            loadListBox(listBox2, selectStringList);
            loadListBox(listBox1, mainStringList);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void loadListBox(ListBox lb, List<String> ls)
        {
            if (lb != null)
            {
                foreach (string name in ls)
                {
                        lb.Items.Add(name);
                }
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems != null)
            {
                listBox2.Items.Add(listBox1.SelectedItem.ToString());
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                if (listBox2.Items.Count > 1)
                {
                    listBox1.Items.Add(listBox2.SelectedItem.ToString());
                    listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                }
            }
        }


        public List<String> getSelectedItems()
        {
            List<String> selectedItems = new List<String>();
            foreach (string name in listBox2.Items)
            {
                selectedItems.Add(name);
            }
            return selectedItems;
        }
        
    }
}
