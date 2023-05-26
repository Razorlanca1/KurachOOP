using System;
using System.Windows.Forms;

namespace Kursach
{
    public partial class HelloThere : Form
    {
        public HelloThere()
        {
            InitializeComponent();
        }

        private void HelloThere_Load(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            timer.Tick += delegate
            {
                this.Close();
            };
            timer.Interval = 3000;
            timer.Start();
        }
    }
}
