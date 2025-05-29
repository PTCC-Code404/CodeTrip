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
    public class TransporteRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");
        public void Cadastrar(Transporte transporte)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into Transporte (Logradouro_End_Transporte, Numero_End_Transporte, Bairro_End_Transporte, Complemento_End_Transporte, Cidade_Nome, UF_Estado) values(@Logradouro_End, @Numero_End, @Bairro_End, @Complemento_End, @Cidade_Nome, @UF_Estado)", conexao);

                cmd.Parameters.Add("@logradouro_end", MySqlDbType.VarChar).Value = transporte.Logradouro_End_Transporte;
                cmd.Parameters.Add("@numero_end", MySqlDbType.VarChar).Value = transporte.Numero_End_Transporte;
                cmd.Parameters.Add("@bairro_end", MySqlDbType.VarChar).Value = transporte.Bairro_End_Transporte;
                cmd.Parameters.Add("@complemento_end", MySqlDbType.VarChar).Value = transporte.Complemento_End_Transporte;
                cmd.Parameters.Add("@cidade_nome", MySqlDbType.VarChar).Value = transporte.Cidade_Nome;
                cmd.Parameters.Add("@uf_estado", MySqlDbType.VarChar).Value = transporte.UF_Estado;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public bool Atualizar(Transporte transporte)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("Update Transporte set Id_Transp=@id, Logradouro_End_Transporte=@logradouro_end, Numero_End_Transporte=@numero_end, Bairro_End_Transporte=@bairro_end, Complemento_End_Transporte=@complemento_end, Cidade_Nome=@cidade_nome, UF_Estado=@uf_estado" + " where Id_Transp=@id ", conexao);
                    cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = transporte.Id_Transp;
                    cmd.Parameters.Add("@logradouro_end", MySqlDbType.VarChar).Value = transporte.Logradouro_End_Transporte;
                    cmd.Parameters.Add("@numero_end", MySqlDbType.VarChar).Value = transporte.Numero_End_Transporte;
                    cmd.Parameters.Add("@bairro_end", MySqlDbType.VarChar).Value = transporte.Bairro_End_Transporte;
                    cmd.Parameters.Add("@complemento_end", MySqlDbType.VarChar).Value = transporte.Complemento_End_Transporte;
                    cmd.Parameters.Add("@cidade_nome", MySqlDbType.VarChar).Value = transporte.Cidade_Nome;
                    cmd.Parameters.Add("@uf_estado", MySqlDbType.VarChar).Value = transporte.UF_Estado;
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

        public IEnumerable<Transporte> TodosTransportes()
        {
            List<Transporte> TransporteLista = new List<Transporte>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from Transporte", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    TransporteLista.Add(
                                new Transporte
                                {
                                    Id_Transp = Convert.ToInt32(dr["Id_Transp"]),
                                    Logradouro_End_Transporte = ((string)dr["Logradouro_End_Transporte"]),
                                    Numero_End_Transporte = ((string)dr["Numero_End_Transporte"]),
                                    Bairro_End_Transporte = ((string)dr["Bairro_End_Transporte"]),
                                    Complemento_End_Transporte = ((string)dr["Complemento_End_Transporte"]),
                                    Cidade_Nome = ((string)dr["Cidade_Nome"]),
                                    UF_Estado = ((string)dr["UF_Estado"]),
                                });
                }
                return TransporteLista;
            }
        }

        public Transporte ObterTransporte(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Transporte WHERE Id_Transp=@id", conexao);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;
                Transporte transporte = new Transporte();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    transporte.Id_Transp = Convert.ToInt32(dr["Id_Transp"]);
                    transporte.Logradouro_End_Transporte = ((string)dr["Logradouro_End_Transporte"]);
                    transporte.Numero_End_Transporte = ((string)dr["Numero_End_Transporte"]);
                    transporte.Bairro_End_Transporte = ((string)dr["Bairro_End_Transporte"]);
                    transporte.Complemento_End_Transporte = ((string)dr["Complemento_End_Transporte"]);
                    transporte.Cidade_Nome = ((string)dr["Cidade_Nome"]);
                    transporte.UF_Estado = ((string)dr["UF_Estado"]);
                }
                return transporte;
            }
        }

        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM Transporte WHERE Id_Transp=@id", conexao);
                cmd.Parameters.AddWithValue("@id", id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}
