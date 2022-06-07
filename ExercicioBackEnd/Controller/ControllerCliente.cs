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
    internal class ControllerCliente : IControlable
    {

        ViewClienteFornecedor view = new();

        private const string enderecoBanco =
           "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=DB;Integrated Security=True;Pooling=False";

        #region Sql Queries

        private const string sqlInserir =
            @"INSERT INTO [TBCLIENTE]
                (
                    [NOME]
                )    
                 VALUES
                (
                    @NOME
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @"UPDATE [TBCLIENTE]	
		        SET
			        [NOME] = @NOME
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluir =
            @"DELETE FROM [TBCLIENTE]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
            @"SELECT 
		            [NUMERO], 
		            [NOME] 
	            FROM 
		            [TBCLIENTE]";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
		            [NUMERO], 
		            [NOME]
	            FROM 
		            [TBCLIENTE]
		        WHERE
                    [NUMERO] = @NUMERO";

        #endregion

        public void Inserir()
        {
            Cliente novoRegistro = (Cliente)view.Cadastro<Cliente>();

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
            Cliente Registro = (Cliente)view.Cadastro<Cliente>();
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
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();

        }

        public void SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            List<Cliente> registros = new List<Cliente>();

            while (leitor.Read())
            {
                Cliente registro = ConverterParaRegistro(leitor);

                registros.Add(registro);
            }

            conexaoComBanco.Close();

            view.MostrarTodos<Cliente>(registros);
        }

        public Base SelecionarPorNumero()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", view.PegaId());

            conexaoComBanco.Open();
            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            Cliente registro = null;
            if (leitor.Read())
                registro = ConverterParaRegistro(leitor);

            conexaoComBanco.Close();

            return registro;
        }

        private Cliente ConverterParaRegistro(SqlDataReader leitorDisciplina)
        {
            int numero = Convert.ToInt32(leitorDisciplina["NUMERO"]);
            string nome = Convert.ToString(leitorDisciplina["NOME"]);

            var registro = new Cliente(numero, nome);

            return registro;
        }

        private static void ConfigurarParametrosRegistro(Cliente registro, SqlCommand cmdInserir)
        {
            cmdInserir.Parameters.AddWithValue("NUMERO", registro.numero);
            cmdInserir.Parameters.AddWithValue("NOME", registro.nome);
        }



    }
}
