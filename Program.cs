using System.Security.Cryptography;

class Program
{
    static void Main()
    {
        //acessar diretorio do do projeto para acessar os arquivos
        string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\"));
        string arquivoProdutos = Path.Combine(path, "produtos.txt"); //string com o diretorio do txt produtos
        string arquivoVendas = Path.Combine(path, "vendas.txt"); // //string com o diretorio do txt de vendas

        List<Produto> produtos = CarregarProdutos(arquivoProdutos);
        List<Venda> vendas = CarregarVendas(arquivoVendas);
        int codigoVenda = ObterProximoCodigoVenda(vendas);

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Iniciar comanda");
            Console.WriteLine("2. Lista de Vendas");
            Console.WriteLine("3. Sair");
            Console.Write("\nEscolha uma opção: ");
            int opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                Console.Clear();
                List<Produto> comanda = new List<Produto>();
                double totalComanda = 0;

                while (true)
                {
                    Console.WriteLine("\n\nLista de Produtos Disponíveis:\n");
                    ListarProdutos(produtos);

                    Console.Write("\nDigite o código do produto desejado ou 0 para finalizar a comanda: ");
                    int codigoProduto = int.Parse(Console.ReadLine());

                    Console.Clear();

                    if (codigoProduto == 0)
                    {
                        break;
                    }

                    Produto produtoSelecionado = EncontrarProdutoPorCodigo(produtos, codigoProduto);

                    if (produtoSelecionado != null)
                    {
                        comanda.Add(produtoSelecionado);
                        totalComanda += produtoSelecionado.Valor;
                        Console.WriteLine("Produto adicionado a comanda:" + produtoSelecionado.Nome);
                    }
                    else
                    {
                        Console.WriteLine("Produto não encontrado.");
                    }
                }

                if (comanda.Count > 0)
                {
                    Console.Write("Digite o nome do cliente: ");
                    string nomeCliente = Console.ReadLine();
                    Venda venda = new Venda(codigoVenda++, nomeCliente, totalComanda);
                    vendas.Add(venda);
                    SalvarVendas(vendas, arquivoVendas);
                    Console.WriteLine("Valor total: R$" + totalComanda + "\n\n");
                }
            }
            else if (opcao == 2)
            {
                Console.Clear();
                ListarVendas(vendas);
            }

            else if (opcao == 3)
            {
                SalvarVendas(vendas, arquivoVendas);
                break;
            }
            else
            {
                Console.WriteLine("Opção inválida.");
            }
        }
    }

    static List<Produto> CarregarProdutos(string arquivo)
    {
        List<Produto> produtos = new List<Produto>();

        using (StreamReader reader = new StreamReader(arquivo))
        {
            string linha;
            while ((linha = reader.ReadLine()) != null)
            {
                string[] partes = linha.Split('|');
                if (partes.Length == 3)
                {
                    int codigo = int.Parse(partes[0].Trim());
                    string nome = partes[1].Trim();
                    double valor = double.Parse(partes[2].Trim());
                    produtos.Add(new Produto(codigo, nome, valor));
                }
            }
        }
        return produtos;
    }

    static void ListarProdutos(List<Produto> produtos)
    {
        foreach (Produto produto in produtos)
        {
            Console.WriteLine(produto.Codigo + " " + produto.Nome + " R$ " + produto.Valor);
        }
    }

    static Produto EncontrarProdutoPorCodigo(List<Produto> produtos, int codigo)
    {
        return produtos.FirstOrDefault(p => p.Codigo == codigo);
    }

    static List<Venda> CarregarVendas(string arquivo)
    {
        List<Venda> vendas = new List<Venda>();

        using (StreamReader reader = new StreamReader(arquivo))
        {
            string linha;
            while ((linha = reader.ReadLine()) != null)
            {
                string[] partes = linha.Split('|');
                if (partes.Length == 3)
                {
                    int codigo = int.Parse(partes[0].Trim());
                    string nomeCliente = partes[1].Trim();
                    double totalComanda = double.Parse(partes[2].Trim());
                    vendas.Add(new Venda(codigo, nomeCliente, totalComanda));
                }
            }
        }
        return vendas;
    }

    
    static void SalvarVendas(List<Venda> vendas, string arquivo)
    {

        using (StreamWriter writer = new StreamWriter(arquivo))
        {
            foreach (Venda venda in vendas)
            {
                writer.WriteLine(venda.Codigo + " | " + venda.NomeCliente + " | " + venda.TotalComanda);
            }
        }
    }

    //funcao para saber qual o codigo da proxima venda
    static int ObterProximoCodigoVenda(List<Venda> vendas)
    {
        int ultimoCodigo = 0;

        if (vendas.Count > 0)
        {
            ultimoCodigo = vendas.Max(v => v.Codigo);
        }
        return ultimoCodigo + 1;
    }

    //Funcao que retorna uma lista com todas as vendas e retorna um texto informando que nao ha nada caso nao existam vendas!
    static void ListarVendas(List<Venda> vendas)
    {
        if (vendas.Count > 0)
        {
            foreach (Venda venda in vendas)
            {
                Console.WriteLine(venda.Codigo + " " + venda.NomeCliente + " R$ " + venda.TotalComanda);
            }
        }
        else
        {
            Console.WriteLine("Nenhuma venda realizada");
        }
        Console.WriteLine("\nAperte qualquer tecla para voltar ao Menu!");
        Console.ReadKey();
        Console.Clear();
    }
}





