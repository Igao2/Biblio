using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblio
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            Form1 func = new Form1() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true }; ;
            this.panel1.Controls.Add(func);
            func.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            Registro func = new Registro() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true }; ;
            this.panel1.Controls.Add(func);
            func.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            Ficha func = new Ficha() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true }; ;
            this.panel1.Controls.Add(func);
            func.Show();
        }
    }
}
