public class Produto
{
    //Classe produto com codigo nome e valor do produto;
    public int Codigo { get; }
    public string Nome { get; }
    public double Valor { get; }

    public Produto(int codigo, string nome, double valor)
    {
        Codigo = codigo;
        Nome = nome;
        Valor = valor;
    }
}