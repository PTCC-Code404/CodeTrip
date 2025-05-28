namespace CodeTrip.Models
{
    public class Hospedagem
    {
        public int? Id_Hospedagem { get; set; }
        public string? Nome_Hospedagem { get; set; }
        public int? Id_Tipo_Hospedagem { get; set; }
        public int? Id_Pensao { get; set; }
        public string? Logradouro_Endereço_Hospedagem { get; set; }
        public string? Numero_Endereço_Hospedagem { get; set; }
        public string? Bairro_Endereço_Hospedagem { get; set; }
        public string? Complemento_Endereço_Hospedagem { get; set; }
        public string? Cidade_Nome { get; set; }
        public string? UF_Estado { get; set; }
    }
}

