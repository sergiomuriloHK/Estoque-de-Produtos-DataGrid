using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Estoque_de_Produtos
{
    public partial class EstoqueDeProdutos : Form
    {
        public EstoqueDeProdutos()
        {
            InitializeComponent();
        }

        private MySqlConnectionStringBuilder conexaoBanco()
        {
            MySqlConnectionStringBuilder conexaoBD = new MySqlConnectionStringBuilder();
            conexaoBD.Server = "localhost";
            conexaoBD.Database = "estoque de produtos";
            conexaoBD.UserID = "root";
            conexaoBD.Password = "";
            conexaoBD.SslMode = 0;
            return conexaoBD;
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaConexaoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexaoBD.Open();

                MySqlCommand comandoMySql = realizaConexaoBD.CreateCommand();

                comandoMySql.CommandText = "INSERT INTO produtos (prNome,prQuantidade,prCategoria,prPreco)" +
                    "VALUES('" + textBoxNome.Text + "', '" + Convert.ToInt32(textBoxQntd.Text) + "', '" + textBoxCategoria.Text + "', '" + "R$ " + textBoxPreco.Text + "')";
                comandoMySql.ExecuteNonQuery();

                realizaConexaoBD.Close();
                atualizarDataGrid();
                limparCampos();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection!");
                Console.WriteLine(ex.Message);
            }
        }

        private void buttonLimpar_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void limparCampos()
        {
            textBoxNome.Clear();
            textBoxCategoria.Clear();
            textBoxPreco.Clear();
            textBoxQntd.Clear();
        }


        private void atualizarDataGrid()
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaConexaoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexaoBD.Open();

                MySqlCommand comandoMySql = realizaConexaoBD.CreateCommand();
                comandoMySql.CommandText = "SELECT * FROM produtos";
                MySqlDataReader reader = comandoMySql.ExecuteReader();

                dataGridProdutos.Rows.Clear();

                while (reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridProdutos.Rows[0].Clone();
                    row.Cells[0].Value = reader.GetString(0);
                    row.Cells[1].Value = reader.GetString(1);
                    row.Cells[2].Value = reader.GetString(2);
                    row.Cells[3].Value = reader.GetString(3);
                    row.Cells[4].Value = reader.GetString(4);
                    dataGridProdutos.Rows.Add(row);
                }

                realizaConexaoBD.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection!");
                Console.WriteLine(ex.Message);
            }
        }

        private void EstoqueDeProdutos_Load(object sender, EventArgs e)
        {
            atualizarDataGrid();
        }
    }
}
