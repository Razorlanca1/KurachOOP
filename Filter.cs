using System.Windows.Forms;

namespace Kursach
{
    public partial class Filter : Form
    {
        public Filter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            string name = textBox1.Text.Trim();

            if (numericUpDown2.Value - 1 < numericUpDown1.Value)
            {
                MessageBox.Show("Задан неверный интервал количества", "Error");
                return;
            }

            if (numericUpDown4.Value - 1 < numericUpDown3.Value)
            {
                MessageBox.Show("Задан неверный интервал цены", "Error");
                return;
            }

            string ill = textBox2.Text.Trim();

            for (int i = PharmacyWindow.table.Rows.Count - 1; i > -1; --i)
            {
                DataGridViewRow row = PharmacyWindow.table.Rows[i];
                if ((!row.Cells[1].Value.ToString().Contains(name) && name != "") ||
                    int.Parse(row.Cells[2].Value.ToString()) < numericUpDown1.Value || int.Parse(row.Cells[2].Value.ToString()) > numericUpDown2.Value ||
                    int.Parse(row.Cells[3].Value.ToString()) < numericUpDown3.Value || int.Parse(row.Cells[3].Value.ToString()) > numericUpDown4.Value ||
                    (!row.Cells[4].Value.ToString().Contains(ill) && ill != ""))
                    PharmacyWindow.table.Rows.Remove(row);
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
