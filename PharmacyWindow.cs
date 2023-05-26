using System;
using System.Windows.Forms;

namespace Kursach
{
    public partial class PharmacyWindow : Form
    {

        public PharmacyWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Pharmacy.Create();
            Pharmacy.ViewTable(dataGridView1);
            Form1_Resize(sender, e);
            table = dataGridView1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ModalWindow modal = new ModalWindow();
            DialogResult add = modal.ShowDialog();

            while (add == DialogResult.OK && Pharmacy.InDataBase(modal.GetName()))
            {
                MessageBox.Show("Имя лекарства должно быть уникальным", "Error");
                add = modal.ShowDialog();
            }
            if (!Pharmacy.InDataBase(modal.GetName()) && add == DialogResult.OK)
            {
                int[] result = Request.Update(modal.GetName(), modal.GetIll(), modal.GetCount(), modal.GetPrice());
                if (result[2] != 0)
                    MessageBox.Show("Было выполнено " + result[2].ToString() + " заявок на общую сумму " + result[1].ToString() +
                        ". Потрачено " + (modal.GetCount() - result[0]) + " лекарств.", "Оповещение");
                if (result[0] != 0)
                    Pharmacy.Add(modal.GetName(), result[0], modal.GetPrice(), modal.GetIll());
                Pharmacy.ViewTable(dataGridView1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pharmacy.Clear();
            Pharmacy.ViewTable(dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберете строку");
                return;
            }

            if (Pharmacy.IsEmpty())
            {
                MessageBox.Show("Таблица пуста");
                return;
            }

            ModalWindow modal = new ModalWindow();
            DataGridViewRow viewRow = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index];
            modal.SetName(viewRow.Cells[1].Value.ToString());
            modal.SetCount(int.Parse(viewRow.Cells[2].Value.ToString()));
            modal.SetPrice(int.Parse(viewRow.Cells[3].Value.ToString()));
            modal.SetIll(viewRow.Cells[4].Value.ToString());

            if (modal.ShowDialog() == DialogResult.OK)
            {
                int[] result = Request.Update(modal.GetName(), modal.GetIll(), modal.GetCount(), modal.GetPrice());
                if (result[2] != 0)
                    MessageBox.Show("Было выполнено " + result[2].ToString() + " заявок на общую сумму " + result[1].ToString() +
                        ". Потрачено " + (modal.GetCount() - result[0]) + " лекарств.", "Оповещение");
                if (result[0] != 0)
                    Pharmacy.Update(modal.GetName(), result[0], modal.GetPrice(), modal.GetIll(),
                    viewRow.Cells[0].Value.ToString());
                else
                    Pharmacy.Delete(viewRow.Cells[0].Value.ToString());
                Pharmacy.ViewTable(dataGridView1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберете строку");
                return;
            }

            if (Pharmacy.IsEmpty())
            {
                MessageBox.Show("Таблица пуста");
                return;
            }

            Pharmacy.Delete(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0].Value.ToString());
            Pharmacy.ViewTable(dataGridView1);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            dataGridView1.Height = this.Height - 125;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
                column.Width = (this.Width - 65) / 5;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Pharmacy.ViewTable(dataGridView1);
            new Filter().ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RequestsWindow request = new RequestsWindow();

            request.ShowDialog();
            Pharmacy.ViewTable(dataGridView1);
        }
    }
}