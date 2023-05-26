using System;
using System.Windows.Forms;

namespace Kursach
{
    public partial class RequestTable : Form
    {
        public RequestTable()
        {
            InitializeComponent();
            Request.Create();
        }

        private void RequestTable_Resize(object sender, EventArgs e)
        {
            dataGridView1.Height = this.Height - 125;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
                column.Width = (this.Width - 65) / 5;

            button2.Width = this.Width - 35;
        }

        private void RequestTable_Load(object sender, EventArgs e)
        {
            RequestTable_Resize(sender, e);
            Request.ViewTable(dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Request.Clear();
            Request.ViewTable(dataGridView1);
        }
    }
}
