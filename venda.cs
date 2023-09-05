public class Venda
{
    //classe venda com codigo, nome do cliente e total da comanda
    public int Codigo { get; }
    public string NomeCliente { get; }
    public double TotalComanda { get; }

    public Venda(int codigo, string nomeCliente, double totalComanda)
    {
        Codigo = codigo;
        NomeCliente = nomeCliente;
        TotalComanda = totalComanda;
    }
}