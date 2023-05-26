using System;
using System.Windows.Forms;

namespace Kursach
{
    public partial class ModalWindow : Form
    {
        string name;
        int count;
        int price;
        string ill;

        public string GetName()
        {
            return name;
        }

        public int GetCount()
        {
            return count;
        }

        public int GetPrice()
        {
            return price;
        }

        public string GetIll()
        {
            return ill;
        }

        public void SetName(string name)
        {
            textBox1.Text = name;
        }

        public void SetCount(int count)
        {
            numericUpDown1.Value = count;
        }

        public void SetPrice(int price)
        {
            numericUpDown2.Value = price;
        }

        public void SetIll(string ill)
        {
            textBox2.Text = ill;
        }

        public ModalWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.name = textBox1.Text.Trim();
            this.count = (int)numericUpDown1.Value;
            this.price = (int)numericUpDown2.Value;
            this.ill = textBox2.Text.Trim();
            if (this.name != "" && this.ill != "")
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else if (this.name == "")
                MessageBox.Show("Введеено не корректное имя");
            else
                MessageBox.Show("Введеено не корректная болезнь");
        }
    }
}
