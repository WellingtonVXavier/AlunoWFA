using AlunoWFA.Classes;

namespace AlunoWFA
{
    public partial class TelaPrincipal : Form
    {
        private Aluno _alunoLogado;
        private List<Aluno> _alunos;
        private Aluno? _alunoSelecionado;

        public TelaPrincipal(Aluno alunoLogado, List<Aluno> aluno)
        {
            InitializeComponent();
            _alunoLogado = alunoLogado;
            _alunos = aluno;
            btnCadastrar.Enabled = true;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;

        }
        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                Aluno novoAluno = new Aluno
                {
                    Matricula = Convert.ToInt32(textMatricula.Text),
                    Nome = textNome.Text,
                    DtNascimento = dtpDtNascimento.Value,
                    Email = textEmail.Text,
                    Senha = textSenha.Text,
                    Ativo = chkAtivo.Checked
                };
                LimparCampos();

                _alunos = novoAluno.Cadastrar(_alunos);
                CarregaDgvAlunos();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void LimparCampos()
        {
            textMatricula.Clear();
            textNome.Clear();
            dtpDtNascimento.Value = DateTime.Now;
            textEmail.Clear();
            textSenha.Clear();
            chkAtivo.Checked = false;
        }

        private void ConfiguraDgvAlunos()
        {
            dgvAlunos.Columns.Add("Id", "Matrícula");
            dgvAlunos.Columns.Add("Nome", "Nome");
            dgvAlunos.Columns.Add("DtNasc", "Data de Nascimento");
            dgvAlunos.Columns.Add("email", "E-mail");
            dgvAlunos.Columns.Add("Ativo", "Status");

            dgvAlunos.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvAlunos.Columns["Id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAlunos.Columns["Nome"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvAlunos.Columns["DtNasc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvAlunos.Columns["DtNasc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAlunos.Columns["DtNasc"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAlunos.Columns["email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvAlunos.Columns["Ativo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
        }

        private void CarregaDgvAlunos()
        {
            dgvAlunos.Rows.Clear();

            foreach (Aluno aluno in _alunos)
            {
                dgvAlunos.Rows.Add(
                    aluno.Matricula,
                    aluno.Nome,
                    aluno.DtNascimento.ToString("dd/MM/yyyy"),
                    aluno.Email,
                    aluno.Ativo ? "Ativo" : "Inativo");
            }
        }

        private void TelaPrincipal_Load(Object sender, EventArgs e)
        {
            ConfiguraDgvAlunos();
            CarregaDgvAlunos();

            textMatricula.Text = ""; //_alunoLogado.Matricula.ToString();
            textNome.Text = ""; // _alunoLogado.Nome;
            dtpDtNascimento.Value = DateTime.Now; //_alunoLogado.DtNascimento;
            textEmail.Text = ""; // _alunoLogado.Email;
            textSenha.Text = ""; // _alunoLogado.Senha;
            chkAtivo.Checked = false; // _alunoLogado.Ativo;
            tslLogado.Text = "Usuário Logado: " + _alunoLogado.Nome;

            dgvAlunos.ClearSelection();
            btnEditar.Enabled = false;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            CarregaDgvAlunos();
            LimparCampos();
            btnCadastrar.Enabled = true;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            dgvAlunos.ClearSelection();
        }

        private void dgvAlunos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAlunos.SelectedRows.Count < 1)
                return;

            int index = 0;

            index = dgvAlunos.SelectedRows[0].Index;
            int matr = (int)dgvAlunos.Rows[index].Cells["Id"].Value;
            _alunoSelecionado = _alunos.Find(itemAluno => itemAluno.Matricula == matr);
            textMatricula.Text = _alunoSelecionado.Matricula.ToString();
            textNome.Text = _alunoSelecionado.Nome;
            dtpDtNascimento.Value = _alunoSelecionado.DtNascimento;
            textEmail.Text = _alunoSelecionado.Email;
            chkAtivo.Checked = _alunoSelecionado.Ativo;

            btnCadastrar.Enabled = false;
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            _alunoSelecionado.Nome = textNome.Text;
            _alunoSelecionado.Email = textEmail.Text;
            _alunoSelecionado.Senha = textSenha.Text;
            _alunoSelecionado.DtNascimento = dtpDtNascimento.Value;
            _alunoSelecionado.Ativo = chkAtivo.Checked;

            _alunoSelecionado.Editar(_alunos);
            CarregaDgvAlunos();

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show($"Você realmente deseja excluir: {_alunoSelecionado.Nome}?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                _alunos = _alunoSelecionado.excluir(_alunos);
                CarregaDgvAlunos();

                if (_alunos.Count == 0)
                {
                    btnNovo.PerformClick();
                }
            }
        }
    }
}
