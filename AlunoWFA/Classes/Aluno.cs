using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlunoWFA.Classes
{
    public class Aluno
    {
        #region Propriedades

        public int Matricula { get; set; }
        public string Nome { get; set; }
        public DateTime DtNascimento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }

        #endregion

        #region Construtores

        public Aluno()
        {

        }

        public Aluno(int matricula, string nome, DateTime dtNascimento, string email, string senha, bool ativo)
        {
            Matricula = matricula;
            Nome = nome;
            DtNascimento = dtNascimento;
            Email = email;
            Senha = senha;
            Ativo = ativo;
        }

        #endregion

        #region Metodos

        public static List<Aluno> GerarAlunos()
        {
            List<Aluno> alunos = new List<Aluno>();

            alunos.Add(new Aluno(1, "Wellington Vaz", DateTime.Now, "wellington@", "123", true));
            alunos.Add(new Aluno(2, "Tainara Rodrigues", DateTime.Now, "tainara@", "123", true));
            alunos.Add(new Aluno(3, "Hulk Salchisha", DateTime.Now, "hulk@", "123", true));
            return alunos;
        }


        public static Aluno RealizarLogin(string email, string senha)
        {
            string query = String.Format($"SELECT * FROM Aluno WHERE Email = '{email}'");
            Conexao cn = new Conexao(query);
            Aluno alunoLogado = new Aluno();

            try
            {
                cn.abrirConexao();
                cn.dr = cn.comando.ExecuteReader(); //Para Select 
                if (cn.dr.HasRows)
                {

                    while (cn.dr.Read())
                    {
                        alunoLogado.Matricula = (int)cn.dr[0];
                        alunoLogado.Nome = (string)cn.dr[1];
                        alunoLogado.DtNascimento = Convert.ToDateTime(cn.dr[2]);
                        alunoLogado.Email = (string)cn.dr[3];
                        alunoLogado.Senha = (string)cn.dr[4];
                        alunoLogado.Ativo = (bool)cn.dr[5];
                    }

                    if (alunoLogado.Ativo == true)
                    {
                        if (alunoLogado != null && alunoLogado.Senha == senha)
                        {
                            return alunoLogado;
                        }
                        else
                        {
                            throw new Exception("E-mail e/ousenha inválidos!");
                        }
                    }
                    else
                    {
                        throw new Exception("O usuário está bloqueado");
                    }
                }
                else
                {
                    throw new Exception("E-mail e/ousenha inválidos!");
                }
            }
            catch (Exception)
            {
                throw;
            }

            finally
            {
                cn.FecharConexao();
            }

        }

        public List<Aluno> Cadastrar(List<Aluno> alunos)
        {
            alunos.Add(this);
            return alunos;
        }

        public List<Aluno> Editar(List<Aluno> alunos)
        {
            alunos.RemoveAll(a => a.Matricula == this.Matricula);
            alunos.Add(this);
            return alunos.OrderBy(a => a.Matricula).ToList();
        }

        public List<Aluno> excluir(List<Aluno> alunos)
        {
            alunos.RemoveAll(a => a.Matricula == this.Matricula);
            return alunos.OrderBy(a => a.Matricula).ToList();
        }

        public int BloquearAluno ()
        {

        }

        #endregion
    }
}
