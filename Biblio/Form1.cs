using Microsoft.Data.Sqlite;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private DataTable alunos = new DataTable();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Conection con = new Conection();
                con.Conectar();
                string sql = "INSERT INTO Aluno VALUES('" + textBox1.Text + "','" + textBox2.Text + "')";
                SQLiteCommand command = new SQLiteCommand(sql, con.sQLite);
                string[] aii = { textBox1.Text, textBox2.Text };


                alunos.Rows.Add(aii);
                command.ExecuteNonQuery();
                textBox1.Clear();
                textBox2.Clear();
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message,"Alerta do Sistema",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Conection con = new Conection();
                con.Conectar();
                string sql = "SELECT * FROM Aluno";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql,con.sQLite);
                adapter.Fill(alunos);
                con.Desconectar();
                dataGridView1.DataSource = alunos;
                
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
