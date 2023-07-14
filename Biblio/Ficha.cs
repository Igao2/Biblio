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
    public partial class Ficha : Form
    {
        public Ficha()
        {
            InitializeComponent();
        }
        private DataTable registros = new DataTable();
        private DataTable alunos = new DataTable();
        private DataTable livros = new DataTable();
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string status = "Empréstimo";
            try
            {
                Conection con = new Conection();
                con.Conectar();
                string sql = "INSERT INTO Registros(CodAluno,CodLivro,Data,Status,NumCopia) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','"+maskedTextBox1.Text+"','"+status+"','"+textBox3.Text+"')";
                SQLiteCommand command = new SQLiteCommand(sql, con.sQLite);
                
                command.ExecuteNonQuery();

                
                string nomeAluno = "";
                string nomeLivro = "";
                for(int i = 0; i<alunos.Rows.Count;i++)
                {
                    DataRow linhaAlunos = alunos.Rows[i];
                    if (linhaAlunos.ItemArray[0].ToString()==textBox1.Text)
                    {
                        nomeAluno=linhaAlunos.ItemArray[1].ToString();
                    }
                }
                int num = registros.Rows.Count + 1;
                for(int j = 0; j<livros.Rows.Count;j++)
                {
                    DataRow linhaLivros = livros.Rows[j];
                    if (linhaLivros.ItemArray[0].ToString() == textBox2.Text)
                    {
                        nomeLivro = linhaLivros.ItemArray[1].ToString();
                    }
                    
                }
                string[] dados =
                {
                    num.ToString(),
                    nomeAluno,
                    nomeLivro,
                    maskedTextBox1.Text,
                    status

                };
                registros.Rows.Add(dados);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                maskedTextBox1.Clear();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ficha_Load(object sender, EventArgs e)
        {
            try
            {
                Conection con = new Conection();
                con.Conectar();
                string sql = "SELECT NumRegistro,Aluno.NomeAluno as Aluno,Livros.Livro,Data,Status,NumCopia From Registros " +
                    "Inner Join Aluno on Aluno.CodAluno = Registros.CodAluno " +
                    "Inner Join Livros on Livros.CodLivro=Registros.CodLivro";
                string sqL = "Select * from Aluno";
                string Sql = "Select * from Livros";
                   
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, con.sQLite);
                SQLiteDataAdapter sadapter = new SQLiteDataAdapter(sqL, con.sQLite);
                SQLiteDataAdapter Sadapter = new SQLiteDataAdapter(Sql, con.sQLite);
                adapter.Fill(registros);
                sadapter.Fill(alunos);
                Sadapter.Fill(livros);
                con.Desconectar();
                dataGridView1.DataSource = registros;
                DateTime dt = DateTime.Today;
                DataTable lero = new DataTable();
                lero.Columns.Add("Número de Registro");
                lero.Columns.Add("Nome do Livro");
                lero.Columns.Add("Quantidade de Dias de Atraso");
                lero.Columns.Add("Número da Cópia");
                for (int i = 0; i < registros.Rows.Count; i++)
                {
                    DataRow linhaRegistro = registros.Rows[i];
                    string b = linhaRegistro.ItemArray[3].ToString();
                    string c = linhaRegistro.ItemArray[5].ToString();
                    string status = linhaRegistro.ItemArray[4].ToString();
                    DateTime data = DateTime.Parse(b);
                    if(status=="Empréstimo"&&data<dt)
                    {
                        
                        ListViewItem item = new ListViewItem();
                        int a = (int)dt.Subtract(data).TotalDays;

                        string[] add =
                        {
                            linhaRegistro.ItemArray[0].ToString(),
                            linhaRegistro.ItemArray[2].ToString(),
                            a.ToString(),
                            c
                        };
                        lero.Rows.Add(add);
                        

                    }
                    dataGridView2.DataSource = lero;

                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            int linha = dataGridView1.SelectedCells[0].RowIndex;
            int cod = 0; 
            try
            {
                for(int i = 0;i<dataGridView1.RowCount;i++)
                {
                    if(i==linha)
                    {
                        cod = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                    }
                }

                string Devolvido = "Devolvido";
                Conection con = new Conection();
                con.Conectar();
                string sql = "UPDATE Registros SET Status = '"+Devolvido+"' WHERE NumRegistro = '" + cod + "'";
                SQLiteCommand command = new SQLiteCommand(sql, con.sQLite);
                dataGridView1.SelectedRows[0].Cells[4].Value = "Devolvido";
                command.ExecuteNonQuery();
                
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Conection con = new Conection();
                con.Conectar();
                DataTable table = new DataTable();
                string sql = "SELECT NumRegistro,Aluno.NomeAluno as Aluno,Livros.Livro,Data,Status,NumCopia From Registros " +
                    "Inner Join Aluno on Aluno.CodAluno = Registros.CodAluno " +
                    "Inner Join Livros on Livros.CodLivro=Registros.CodLivro " +
                    "WHERE Aluno.NomeAluno LIKE '%" + textBox4.Text + "%'";
                SQLiteDataAdapter adptaer = new SQLiteDataAdapter(sql, con.sQLite);
                adptaer.Fill(table);
                dataGridView1.DataSource= table;
                con.Desconectar();
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = registros;
        }
    }
}
