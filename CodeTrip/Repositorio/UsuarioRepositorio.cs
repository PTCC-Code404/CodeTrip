using System.Data;
using CodeTrip.Models;
using MySql.Data.MySqlClient;

namespace CodeTrip.Repositorio
{

    public class UsuarioRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");



        public Usuario ObterUsuario(string email)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new("SELECT * FROM Usuario WHERE Email_Usuario = @email", conexao);
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;

                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    Usuario usuario = null;
                    if (dr.Read())
                    {

                        usuario = new Usuario
                        {
                            Id_Usuario = Convert.ToInt32(dr["Id_Usuario"]),
                            Nome_Usuario = dr["Nome_Usuario"].ToString(),
                            Email_Usuario = dr["Email_Usuario"].ToString(),
                            Senha_Usuario = dr["Senha_Usuario"].ToString()
                        };
                    }
                    return usuario;
                }
            }
        }
    }



}
