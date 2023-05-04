using AlunoWFA.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AlunoWFA.Telas
{
    public partial class TelaLogin : Form
    {
        private List<Aluno> _alunos;

        public TelaLogin()
        {
            InitializeComponent();
            _alunos = Aluno.GerarAlunos();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                Aluno alunoLogado = Aluno.RealizarLogin(txtEmail.Text, txtSenha.Text);
                TelaPrincipal telaPrincipal = new TelaPrincipal(alunoLogado, _alunos);
                txtSenha.Text = "";
                txtEmail.Text = "";
                this.Hide();
                telaPrincipal.ShowDialog();
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
