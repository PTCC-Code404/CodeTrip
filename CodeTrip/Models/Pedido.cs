namespace CodeTrip.Models
{
    public class Pedido
    {
        public int? Id_Pedido { get; set; }
        public int? Id_Usuario { get; set; }
        public string? CPF_Cli { get; set; }
        public int? Id_Transp { get; set; }
        public int? Id_End_Transporte { get; set; }
        public int? Id_Hospedagem { get; set; }
        public int? Id_Pagamento { get; set; }
    }
}
