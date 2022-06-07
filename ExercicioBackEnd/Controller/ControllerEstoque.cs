using ExercicioBackEnd.Model;
using ExercicioBackEnd.View;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercicioBackEnd.Controller
{
    internal class ControllerEstoque : IControlable
    {

        ViewEstoque view = new();


        private const string enderecoBanco =
        "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=DB;Integrated Security=True;Pooling=False";

        #region Sql Queries


        private const string sqlInserirComprarOuVender =
           @"INSERT INTO [TBCOMPRA_E_VENDA]
                (
                    [VALOR],
                    [NUMERO_CLIENTE],
                    [NUMERO_PRODUTO]
                )    
                 VALUES
                (
                    @VALOR,
                    @NUMERO_CLIENTE,
                    @NUMERO_PRODUTO
                );";


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
                    @NOME,
                    @QUANTIDADE,
                    @DATA_CADASTRO,
                    @DATA_ALTERACAO
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @"UPDATE [TBESTOQUE]	
		        SET
			        [NOME] = @NOME,
                    [QUANTIDADE] = @QUANTIDADE,
                    [DATA_ALTERACAO] = @DATA_ALTERACAO
		        WHERE
			        [NUMERO] = @NUMERO";



        private const string sqlExcluir =
            @"DELETE FROM [TBESTOQUE]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
            @"SELECT 
                 [NUMERO],
		        [NOME],
                    [QUANTIDADE],
                    [DATA_CADASTRO],
                    [DATA_ALTERACAO]
	            FROM 
		            [TBESTOQUE]";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
                     [NUMERO],
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


        public void CompraEVenda(int valor,int clienteId,int ProdutoId)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlInserirComprarOuVender, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("VALOR", valor);
            comandoSelecao.Parameters.AddWithValue("NUMERO_CLIENTE", clienteId);
            comandoSelecao.Parameters.AddWithValue("NUMERO_PRODUTO", ProdutoId);

            conexaoComBanco.Open();
          comandoSelecao.ExecuteNonQuery();


            conexaoComBanco.Close();

        }




        private Estoque ConverterParaRegistro(SqlDataReader leitorDisciplina)
        {
            int numero = Convert.ToInt32(leitorDisciplina["NUMERO"]);
            string nome = Convert.ToString(leitorDisciplina["NOME"]);
            int quantidade = Convert.ToInt32(leitorDisciplina["QUANTIDADE"]);
            DateTime dataCadastro = Convert.ToDateTime(leitorDisciplina["DATA_CADASTRO"]);
            var dataAlteracao = leitorDisciplina["DATA_ALTERACAO"] == DBNull.Value ? default : Convert.ToDateTime(leitorDisciplina["DATA_ALTERACAO"]);

            var registro = new Estoque(numero, nome, quantidade, dataCadastro);

            if (dataAlteracao != default)
                registro.dataAlteracao = dataAlteracao;


            return registro;
        }

        private static void ConfigurarParametrosRegistro(Estoque registro, SqlCommand cmdInserir)
        {
            cmdInserir.Parameters.AddWithValue("NUMERO", registro.numero);
            cmdInserir.Parameters.AddWithValue("NOME", registro.nome);
            cmdInserir.Parameters.AddWithValue("QUANTIDADE", registro.quantidade);
            cmdInserir.Parameters.AddWithValue("DATA_CADASTRO", registro.dataCadastro);

            if (registro.dataAlteracao != default)
            cmdInserir.Parameters.AddWithValue("DATA_ALTERACAO", registro.dataAlteracao);
            else
                cmdInserir.Parameters.AddWithValue("DATA_ALTERACAO", DBNull.Value);
        }

    }
}
