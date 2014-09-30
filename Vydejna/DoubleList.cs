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
            loadListBox(listBox2, null, selectStringList);
            loadListBox(listBox1, listBox2, mainStringList);
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


        private void loadListBox(ListBox lb, ListBox lbChild,  List<String> ls)
        {
            if (lb != null)
            {
                foreach (string name in ls)
                {
                    if (lbChild == null)
                    {
                        lb.Items.Add(name);
                    }
                    else
                    {
                        Int32 lbIndex = lbChild.Items.IndexOf(name);
                        if (lbChild.Items.IndexOf(name) == -1)
                        {
                            lb.Items.Add(name);
                        }
                    }
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

     

        
    }
}
