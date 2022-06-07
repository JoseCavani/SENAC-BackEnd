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
    internal class ControllerFornecedor : IControlable
    {
        ViewClienteFornecedor view = new();
        private const string enderecoBanco =
     "Data Source=(LocalDB)\\MSSqlLocalDB;" +
     "Initial Catalog=GeradorTeste;" +
     "Integrated Security=True;" +
     "Pooling=False";

        #region Sql Queries

        private const string sqlInserir =
            @"INSERT INTO [TBFORNECEDOR]
                (
                    [NOME]
                )    
                 VALUES
                (
                    @NOME
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @"UPDATE [TBFORNECEDOR]	
		        SET
			        [NOME] = @NOME
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluir =
            @"DELETE FROM [TBFORNECEDOR]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
            @"SELECT 
		            [NUMERO], 
		            [NOME] 
	            FROM 
		            [TBFORNECEDOR]";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
		            [NUMERO], 
		            [NOME]
	            FROM 
		            [TBFORNECEDOR]
		        WHERE
                    [NUMERO] = @NUMERO";

        #endregion

        public void Inserir()
        {
          Fornecedor novoRegistro = (Fornecedor)view.Cadastro<Fornecedor>();

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
            Fornecedor Registro = (Fornecedor)view.Cadastro<Fornecedor>();
            Registro.numero = view.PegaId();

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

            comandoExclusao.Parameters.AddWithValue("NUMERO", view.PegaId());

            conexaoComBanco.Open();
             comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();

        }

        public void SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            List<Fornecedor> registros = new List<Fornecedor>();

            while (leitor.Read())
            {
                Fornecedor registro = ConverterParaRegistro(leitor);

                registros.Add(registro);
            }

            conexaoComBanco.Close();

            view.MostrarTodos<Fornecedor>(registros);
        }

        public Base SelecionarPorNumero()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", view.PegaId());

            conexaoComBanco.Open();
            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            Fornecedor registro = null;
            if (leitor.Read())
                registro = ConverterParaRegistro(leitor);

            conexaoComBanco.Close();

            return registro;
        }

        private Fornecedor ConverterParaRegistro(SqlDataReader leitorDisciplina)
        {
            int numero = Convert.ToInt32(leitorDisciplina["NUMERO"]);
            string nome = Convert.ToString(leitorDisciplina["NOME"]);

            var registro = new Fornecedor(numero, nome);

            return registro;
        }

        private static void ConfigurarParametrosRegistro(Fornecedor registro, SqlCommand cmdInserir)
        {
            cmdInserir.Parameters.AddWithValue("NUMERO", registro.numero);
            cmdInserir.Parameters.AddWithValue("NOME", registro.nome);
        }


    }
}
