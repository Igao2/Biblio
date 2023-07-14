using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Biblio
{
    public partial class Registro : Form
    {
        public Registro()
        {
            InitializeComponent();
        }
        private DataTable livros = new DataTable();
        private void Registro_Load(object sender, EventArgs e)
        {
            try
            {
                Conection con = new Conection();
                con.Conectar();
                string sql = "SELECT * FROM Livros";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, con.sQLite);
                adapter.Fill(livros);
                con.Desconectar();
                dataGridView1.DataSource = livros;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Conection con = new Conection();
                con.Conectar();
                string sql = "SELECT * FROM Livros Where Livro Like '%"+textBox1.Text+"%'";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, con.sQLite);
                DataTable comp = new DataTable();
                adapter.Fill(comp);
                con.Desconectar();
                dataGridView1.DataSource = comp;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                Conection con = new Conection();
                con.Conectar();
                string sql = "INSERT INTO Livros VALUES('" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "')";
                SQLiteCommand command = new SQLiteCommand(sql, con.sQLite);
                string[] insert =
                {
                    textBox3.Text,
                    textBox4.Text,
                    textBox5.Text,
                    textBox6.Text,

                };
                livros.Rows.Add(insert);
                command.ExecuteNonQuery();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = livros;
        }
    }
}
