using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlunoWFA.Classes
{
    internal class Conexao
    {
        #region Variáveis
        private static string _strConexao = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=TurmaN17;Integrated Security=True";

        public SqlConnection conexao = new SqlConnection(_strConexao);
        public SqlCommand comando;
        public SqlDataAdapter da;
        public SqlDataReader dr;
        public DataSet ds;

        #endregion

        #region Construtores

        public Conexao(string query)
        {
            comando = new SqlCommand(query, conexao);
            da = new SqlDataAdapter(query, conexao);
            ds = new DataSet();
        }


        #endregion

        #region Métodos

        public void abrirConexao()
        {
            if (conexao.State == ConnectionState.Open)
            {
                conexao.Close();
            }
            conexao.Open();
        }

        public void FecharConexao()
        {
            if (conexao.State == ConnectionState.Open)
            {
                conexao.Close();
            }
        }

        #endregion
    }
}
