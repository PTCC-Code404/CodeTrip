﻿using MySql.Data.MySqlClient;
using System.Data;
using CodeTrip.Models;
using static Mysqlx.Expect.Open.Types.Condition.Types;
using MySqlX.XDevAPI;
using Mysqlx.Crud;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;

namespace CodeTrip.Repositorio
{
    public class HospedagemRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        public void Cadastrar(Hospedagem hospedagem)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into Hospedagem (Nome_Hospedagem, Tipo_Hospedagem, Pensao, Logradouro_Endereço_Hospedagem, Numero_Endereço_Hospedagem, Bairro_Endereço_Hospedagem, Complemento_Endereço_Hospedagem, Cidade_Nome, UF_Estado) values(@nome, @tipo, @pensao, @logradouro, @numero, @bairro, @complemento, @cidade_nome, @uf_estado)", conexao);

                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = hospedagem.Nome_Hospedagem;
                cmd.Parameters.Add("@tipo", MySqlDbType.Int32).Value = hospedagem.Id_Tipo_Hospedagem;
                cmd.Parameters.Add("@pensao", MySqlDbType.Int32).Value = hospedagem.Id_Pensao;
                cmd.Parameters.Add("@logradouro", MySqlDbType.VarChar).Value = hospedagem.Logradouro_Endereço_Hospedagem;
                cmd.Parameters.Add("@numero", MySqlDbType.VarChar).Value = hospedagem.Numero_Endereço_Hospedagem;
                cmd.Parameters.Add("@bairro", MySqlDbType.VarChar).Value = hospedagem.Bairro_Endereço_Hospedagem;
                cmd.Parameters.Add("@complemento", MySqlDbType.VarChar).Value = hospedagem.Complemento_Endereço_Hospedagem;
                cmd.Parameters.Add("@cidade_nome", MySqlDbType.VarChar).Value = hospedagem.Cidade_Nome;
                cmd.Parameters.Add("@uf_estado", MySqlDbType.VarChar).Value = hospedagem.UF_Estado;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }


        public bool Atualizar(Hospedagem hospedagem)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE hospedagem SET Nome_Hospedagem = @nome, Id_Tipo_Hospedagem = @tipo, Id_Pensao = @pensao, Logradouro_Endereço_Hospedagem = @logradouro, Numero_Endereço_Hospedagem = @numero, Bairro_Endereço_Hospedagem = @bairro, Complemento_Endereço_Hospedagem = @complemento, Cidade_Nome = @cidade_nome, UF_Estado = @uf_estado" + " where Id_Hospedagem=@id ", conexao);
                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = hospedagem.Id_Hospedagem;
                    cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = hospedagem.Nome_Hospedagem;
                    cmd.Parameters.Add("@tipo", MySqlDbType.Int32).Value = hospedagem.Id_Tipo_Hospedagem;
                    cmd.Parameters.Add("@pensao", MySqlDbType.Int32).Value = hospedagem.Id_Pensao;
                    cmd.Parameters.Add("@logradouro", MySqlDbType.VarChar).Value = hospedagem.Logradouro_Endereço_Hospedagem;
                    cmd.Parameters.Add("@numero", MySqlDbType.VarChar).Value = hospedagem.Numero_Endereço_Hospedagem;
                    cmd.Parameters.Add("@bairro", MySqlDbType.VarChar).Value = hospedagem.Bairro_Endereço_Hospedagem;
                    cmd.Parameters.Add("@complemento", MySqlDbType.VarChar).Value = hospedagem.Complemento_Endereço_Hospedagem;
                    cmd.Parameters.Add("@cidade_nome", MySqlDbType.VarChar).Value = hospedagem.Cidade_Nome;
                    cmd.Parameters.Add("@uf_estado", MySqlDbType.VarChar).Value = hospedagem.UF_Estado;
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

        public IEnumerable<Hospedagem> TodasHospedagens()
        {
            List<Hospedagem> HospedagemLista = new List<Hospedagem>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from Hospedagem", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    HospedagemLista.Add(
                                new Hospedagem
                                {
                                    Id_Hospedagem = Convert.ToInt32(dr["Id_Hospedagem"]),
                                    Nome_Hospedagem = (string)dr["Nome_Hospedagem"],
                                    Id_Tipo_Hospedagem = Convert.ToInt32(dr["Id_Tipo_Hospedagem"]),
                                    Id_Pensao = Convert.ToInt32(dr["Id_Pensao"]),
                                    Logradouro_Endereço_Hospedagem = (string)dr["Logradouro_Endereço_Hospedagem"],
                                    Numero_Endereço_Hospedagem = (string)dr["Numero_Endereço_Hospedagem"],
                                    Bairro_Endereço_Hospedagem = (string)dr["Bairro_Endereço_Hospedagem"],
                                    Complemento_Endereço_Hospedagem = (string)dr["Complemento_Endereço_Hospedagem"],
                                    Cidade_Nome = (string)dr["Cidade_Nome"],
                                    UF_Estado = (string)dr["UF_Estado"]
                                });
                }
                return HospedagemLista;
            }
        }

        public Hospedagem ObterHospedagem(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Hospedagem WHERE Id_Hospedagem=@id", conexao);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;
                Hospedagem hospedagem = new Hospedagem();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    hospedagem.Id_Hospedagem = Convert.ToInt32(dr["Id_Hospedagem"]);
                    hospedagem.Nome_Hospedagem = (string)(dr["Nome_Hospedagem"]);
                    hospedagem.Id_Tipo_Hospedagem = Convert.ToInt32(dr["Id_Tipo_Hospedagem"]);
                    hospedagem.Id_Pensao = Convert.ToInt32(dr["Id_Pensao"]);
                    hospedagem.Logradouro_Endereço_Hospedagem = (string)(dr["Logradouro_Endereço_Hospedagem"]);
                    hospedagem.Numero_Endereço_Hospedagem = (string)(dr["Numero_Endereço_Hospedagem"]);
                    hospedagem.Bairro_Endereço_Hospedagem = (string)(dr["Bairro_Endereço_Hospedagem"]);
                    hospedagem.Complemento_Endereço_Hospedagem = (string)(dr["Complemento_Endereço_Hospedagem"]);
                    hospedagem.Cidade_Nome = (string)(dr["Cidade_Nome"]);
                    hospedagem.UF_Estado = (string)(dr["UF_Estado"]);
                }
                return hospedagem;
            }
        }

        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM Hospedagem WHERE Id_Hospedagem=@id", conexao);
                cmd.Parameters.AddWithValue("@id", id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}
