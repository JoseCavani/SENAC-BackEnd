using Exercicio1.Model;
using Exercicio1.View;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio1.Controller
{
    internal class ControllerEstoque : IControlable
    {

        ViewEstoque view = new();


        private const string enderecoBanco =
        "Data Source=(LocalDB)\\MSSqlLocalDB;" +
        "Initial Catalog=GeradorTeste;" +
        "Integrated Security=True;" +
        "Pooling=False";

        #region Sql Queries

        private const string sqlInserir =
            @"INSERT INTO [TBESTOQUE]
                (
                    [NOME],
                    [QUANTIDADE],
                    [DATA_CADASTRO],
                    [DATA_ALTERACAO]
                )    
                 VALUES
                (
                    @NOME
                    @QUANTIDADE
                    @DATA_CADASTRO
                    @DATA_ALTERACAO
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @"UPDATE [TBESTOQUE]	
		        SET
			        [NOME] = @NOME,
                    [QUANTIDADE] = @QUANTIDADE,
                    [DATA_CADASTRO] = @DATA_CADASTRO,
                    [DATA_ALTERACAO] = @DATA_ALTERACAO
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluir =
            @"DELETE FROM [TBESTOQUE]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
            @"SELECT 
		        [NOME],
                    [QUANTIDADE],
                    [DATA_CADASTRO],
                    [DATA_ALTERACAO]
	            FROM 
		            [TBESTOQUE]";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
		             [NOME],
                    [QUANTIDADE],
                    [DATA_CADASTRO],
                    [DATA_ALTERACAO]
	            FROM 
		            [TBESTOQUE]
		        WHERE
                    [NUMERO] = @NUMERO";

        #endregion

        public void Inserir()
        {
            Estoque novoRegistro = view.Cadastro();

            SqlConnection conexao = new SqlConnection(enderecoBanco);
            SqlCommand cmdInserir = new SqlCommand(sqlInserir, conexao);

            ConfigurarParametrosRegistro(novoRegistro, cmdInserir);
            conexao.Open();

            var numero = cmdInserir.ExecuteScalar();

            novoRegistro.numero = Convert.ToInt32(numero);
            conexao.Close();

        }

        public void Editar()
        {

            Estoque Registro = view.Cadastro();
            Registro.numero = view.pegaId();
            Registro.dataAlteracao = DateTime.Now;


            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosRegistro(Registro, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();
        }

        public void Excluir()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("NUMERO", view.pegaId());

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();

        }

        public void SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            List<Estoque> registros = new List<Estoque>();

            while (leitor.Read())
            {
                Estoque registro = ConverterParaRegistro(leitor);

                registros.Add(registro);
            }

            conexaoComBanco.Close();

            view.MostrarTodos(registros);
        }

        public Base SelecionarPorNumero()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", view.pegaId());

            conexaoComBanco.Open();
            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            Estoque registro = null;
            if (leitor.Read())
                registro = ConverterParaRegistro(leitor);

            conexaoComBanco.Close();

            return registro;
        }

        private Estoque ConverterParaRegistro(SqlDataReader leitorDisciplina)
        {
            int numero = Convert.ToInt32(leitorDisciplina["NUMERO"]);
            string nome = Convert.ToString(leitorDisciplina["NOME"]);
            int quantidade = Convert.ToInt32(leitorDisciplina["QUANTIDADE"]);
            DateTime dataCadastro = Convert.ToDateTime(leitorDisciplina["DATA_CADASTRO"]);
            DateTime dataAlteracao = Convert.ToDateTime(leitorDisciplina["DATA_ALTERACAO"]);

            var registro = new Estoque(numero, nome, quantidade, dataCadastro);



            return registro;
        }

        private static void ConfigurarParametrosRegistro(Estoque registro, SqlCommand cmdInserir)
        {
            cmdInserir.Parameters.AddWithValue("NUMERO", registro.numero);
            cmdInserir.Parameters.AddWithValue("NOME", registro.nome);
            cmdInserir.Parameters.AddWithValue("QUANTIDADE", registro.quantidade);
            cmdInserir.Parameters.AddWithValue("DATA_CADASTRO", registro.dataCadastro);
            cmdInserir.Parameters.AddWithValue("DATA_ALTERACAO", registro.dataAlteracao);
        }

    }
}
