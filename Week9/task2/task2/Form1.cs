using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = dataGridView2.CurrentCell.RowIndex;
            dataGridView2.Rows.RemoveAt(index);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            for(int i = 0; i<dataGridView2.Rows.Count; i++)
            {
                if ((string)dataGridView2.Rows[i].Cells[0].Value == input)
                {
                    dataGridView2.ClearSelection();
                    dataGridView2.Rows[i].Selected = true;
                }
            }
        }
    }
}
