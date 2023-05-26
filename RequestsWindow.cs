using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Kursach
{
    public partial class RequestsWindow : Form
    {
        public RequestsWindow()
        {
            InitializeComponent();
            Request.Create();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string human = textBox3.Text.Trim();
            string name = textBox1.Text.Trim();
            string ill = textBox2.Text.Trim();
            int count = (int)numericUpDown1.Value;
            int weste;
            if (name != "" && Regex.IsMatch(human, "^([А-ЯA-Z]|[А-ЯA-Z][\\x27а-яa-z]{1,}|[А-ЯA-Z][\\x27а-яa-z]{1,}\\-([А-ЯA-Z][\\x27а-яa-z]{1,}|(оглы)|(кызы)))\\040[А-ЯA-Z][\\x27а-яa-z]{1,}(\\040[А-ЯA-Z][\\x27а-яa-z]{1,})?$"))
            {
                int[] result = Pharmacy.Request(name, ill, count);
                count = result[0];
                weste = result[1];
                if (count == 0)
                {
                    MessageBox.Show("Покупка успешно совершена. Было потрачено " + weste + ".", "Оповещение");
                    Close();
                    return;
                }

                if (MessageBox.Show("Куплено " + ((int)numericUpDown1.Value - count) + " лекарств. Было потрачено " + weste + ". Оставить заявку на оставшиеся " + count + "?",
                    "Оповещение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Request.Add(human, name, ill, count);

                Close();
            }
            else if (name == "")
                MessageBox.Show("Введено не корректное название", "Error");
            else
                MessageBox.Show("Введено не корректное имя", "Error");
        }
    }
}
