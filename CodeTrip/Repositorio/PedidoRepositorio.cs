using MySql.Data.MySqlClient;
using System.Data;
using CodeTrip.Models;
using static Mysqlx.Expect.Open.Types.Condition.Types;
using MySqlX.XDevAPI;
using Mysqlx.Crud;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;

namespace CodeTrip.Repositorio
{
    public class PedidoRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        public void Cadastrar(Pedido pedido)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into Pedido (Id_Usuario, CPF_Cli, Id_Transp, Id_End_Transporte, Id_Hospedagem, Id_Pagamento) values(@Id_Usuario, @CPF_Cli, @Id_Transp, @Id_End_Transporte, @Id_Hospedagem, @Id_Pagamento)", conexao);

                cmd.Parameters.Add("@Id_Usuario", MySqlDbType.VarChar).Value = pedido.Id_Usuario;
                cmd.Parameters.Add("@CPF_Cli", MySqlDbType.VarChar).Value = pedido.CPF_Cli;
                cmd.Parameters.Add("@Id_Transp", MySqlDbType.VarChar).Value = pedido.Id_Transp;
                cmd.Parameters.Add("@Id_End_Transporte", MySqlDbType.VarChar).Value = pedido.Id_End_Transporte;
                cmd.Parameters.Add("@Id_Hospedagem", MySqlDbType.VarChar).Value = pedido.Id_Hospedagem;
                cmd.Parameters.Add("@Id_Pagamento", MySqlDbType.VarChar).Value = pedido.Id_Pagamento;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public bool Atualizar(Pedido pedido)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("Update Pedido set Id_Usuario=@Id_Usuario, CPF_Cli=@CPF_Cli, Id_Transp=@Id_Transp, Id_End_Transporte=@Id_End_Transporte, Id_Hospedagem=@Id_Hospedagem, Id_Pagamento=@Id_Pagamento" + " where Id_Pedido=@id ", conexao);
                    cmd.Parameters.Add("@Id_Pedido", MySqlDbType.Int32).Value = pedido.Id_Pedido;
                    cmd.Parameters.Add("@Id_Usuario", MySqlDbType.Int32).Value = pedido.Id_Usuario;
                    cmd.Parameters.Add("@CPF_Cli", MySqlDbType.VarChar).Value = pedido.CPF_Cli;
                    cmd.Parameters.Add("@Id_Transp", MySqlDbType.Int32).Value = pedido.Id_Transp;
                    cmd.Parameters.Add("@Id_End_Transporte", MySqlDbType.Int32).Value = pedido.Id_End_Transporte;
                    cmd.Parameters.Add("@Id_Hospedagem", MySqlDbType.Int32).Value = pedido.Id_Hospedagem;
                    cmd.Parameters.Add("@Id_Pagamento", MySqlDbType.Int32).Value = pedido.Id_Pagamento;
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao atualizar cliente: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<Pedido> TodosPedidos()
        {
            List<Pedido> PedidoLista = new List<Pedido>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from Pedido", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    PedidoLista.Add(
                                new Pedido
                                {
                                    Id_Pedido = Convert.ToInt32(dr["Id_Pedido"]),
                                    Id_Usuario = Convert.ToInt32(dr["Id_Usuario"]),
                                    CPF_Cli = ((string)dr["CPF_Cli"]),
                                    Id_Transp = Convert.ToInt32(dr["Id_Transp"]),
                                    Id_End_Transporte = Convert.ToInt32(dr["Id_End_Transporte"]),
                                    Id_Hospedagem = Convert.ToInt32(dr["Id_Hospedagem"]),
                                    Id_Pagamento = Convert.ToInt32(dr["Id_Pagamento"]),
                                });
                }
                return PedidoLista;
            }
        }

        public Pedido ObterPedido(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Pedido WHERE Id_Pedido=@id", conexao);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;
                Pedido pedido = new Pedido();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    pedido.Id_Pedido = Convert.ToInt32(dr["Id_Pedido"]),
                    pedido.Id_Usuario = Convert.ToInt32(dr["Id_Usuario"]),
                    pedido.CPF_Cli = ((string)dr["CPF_Cli"]),
                    pedido.Id_Transp = Convert.ToInt32(dr["Id_Transp"]),
                    pedido.Id_End_Transporte = Convert.ToInt32(dr["Id_End_Transporte"]),
                    pedido.Id_Hospedagem = Convert.ToInt32(dr["Id_Hospedagem"]),
                    pedido.Id_Pagamento = Convert.ToInt32(dr["Id_Pagamento"]),
                }
                return pedido;
            }
        }

        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM Pedido WHERE Id_Pedido=@id", conexao);
                cmd.Parameters.AddWithValue("@id", id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}
