using System;
using System.Collections.Generic;
using ColecaoJogos;

class Program
{
    static void cadastrarJogo(List<Jogo> jogos)
    {
        Jogo novoJogo = new Jogo();
        Console.WriteLine("*** Cadastro de Jogo ***");
        Console.Write("Título: ");
        novoJogo.titulo = Console.ReadLine();
        Console.Write("Console: ");
        novoJogo.console = Console.ReadLine();
        Console.Write("Ano: ");
        novoJogo.ano = int.Parse(Console.ReadLine());
        Console.Write("Ranking: ");
        novoJogo.ranking = int.Parse(Console.ReadLine());
        novoJogo.emprestimo.emprestado = 'N';
        novoJogo.emprestimo.data = "";
        novoJogo.emprestimo.nomePessoa = "";
        jogos.Add(novoJogo);
    }

    static void mostrarJogo(Jogo jogo)
    {
        Console.WriteLine($"Título: {jogo.titulo}");
        Console.WriteLine($"Console: {jogo.console}");
        Console.WriteLine($"Ano: {jogo.ano}");
        Console.WriteLine($"Ranking: {jogo.ranking}");
        Console.WriteLine($"Emprestado: {jogo.emprestimo.emprestado}");
        Console.WriteLine("----------");
    }

    static void mostrarJogos(List<Jogo> jogos)
    {
        if (jogos.Count == 0)
        {
            Console.WriteLine("Nenhum jogo cadastrado.");
        }
        foreach (Jogo jogo in jogos)
        {
            mostrarJogo(jogo);
        }
    }

    static bool buscarJogo(List<Jogo> jogos, string titulo)
    {
        bool encontrou = false;
        foreach (Jogo jogo in jogos)
        {
            if (jogo.titulo.ToUpper().Contains(titulo.ToUpper()))
            {
                mostrarJogo(jogo);
                encontrou = true;
            }
        }
        return encontrou;
    }

    static void jogosPorConsole(List<Jogo> jogos, string consoleBusca)
    {
        bool encontrou = false;
        foreach (Jogo jogo in jogos)
        {
            if (jogo.console.ToUpper().Contains(consoleBusca.ToUpper()))
            {
                mostrarJogo(jogo);
                encontrou = true;
            }
        }
        if (!encontrou)
        {
            Console.WriteLine("Nenhum jogo desse console.");
        }
    }

    static int buscarIndice(List<Jogo> jogos, string titulo)
    {
        for (int i = 0; i < jogos.Count; i++)
        {
            if (jogos[i].titulo.ToUpper().Equals(titulo.ToUpper()))
            {
                return i;
            }
        }
        return -1;
    }

    static void realizarEmprestimo(List<Jogo> jogos, string titulo)
    {
        int indice = buscarIndice(jogos, titulo);
        if (indice == -1)
        {
            Console.WriteLine("Jogo não encontrado.");
            return;
        }
        if (jogos[indice].emprestimo.emprestado == 'S')
        {
            Console.WriteLine("Este jogo já está emprestado.");
            return;
        }
        Console.Write("Nome de quem pegou o jogo: ");
        jogos[indice].emprestimo.nomePessoa = Console.ReadLine();
        jogos[indice].emprestimo.data = DateTime.Now.ToString("dd/MM/yyyy");
        jogos[indice].emprestimo.emprestado = 'S';
        Console.WriteLine("Empréstimo registrado.");
    }

    static void devolverJogo(List<Jogo> jogos, string titulo)
    {
        int indice = buscarIndice(jogos, titulo);
        if (indice == -1)
        {
            Console.WriteLine("Jogo não encontrado.");
            return;
        }
        if (jogos[indice].emprestimo.emprestado == 'N')
        {
            Console.WriteLine("Este jogo não está emprestado.");
            return;
        }
        jogos[indice].emprestimo.emprestado = 'N';
        jogos[indice].emprestimo.nomePessoa = "";
        jogos[indice].emprestimo.data = "";
        Console.WriteLine("Jogo devolvido.");
    }

    static void mostrarEmprestados(List<Jogo> jogos)
    {
        bool encontrou = false;
        foreach (Jogo jogo in jogos)
        {
            if (jogo.emprestimo.emprestado == 'S')
            {
                Console.WriteLine($"Jogo: {jogo.titulo}");
                Console.WriteLine($"Pessoa: {jogo.emprestimo.nomePessoa}");
                Console.WriteLine($"Data: {jogo.emprestimo.data}");
                Console.WriteLine("----------");

                encontrou = true;
            }
        }
        if (!encontrou)
        {
            Console.WriteLine("Nenhum jogo emprestado.");
        }
    }

    static int menu()
    {
        Console.WriteLine("=== COLEÇÃO DE JOGOS ===");
        Console.WriteLine("1 - Cadastrar jogo");
        Console.WriteLine("2 - Buscar por título");
        Console.WriteLine("3 - Listar jogos");
        Console.WriteLine("4 - Listar por console");
        Console.WriteLine("5 - Emprestar jogo");
        Console.WriteLine("6 - Devolver jogo");
        Console.WriteLine("7 - Mostrar emprestados");
        Console.WriteLine("0 - Sair");
        Console.Write("Escolha: ");
        return int.Parse(Console.ReadLine());
    }

    static void Main()
    {
        List<Jogo> jogos = new List<Jogo>();
        int opcao;
        do
        {
            opcao = menu();
            switch (opcao)
            {
                case 1:
                    cadastrarJogo(jogos);
                    break;
                case 2:
                    Console.Write("Título: ");
                    if (!buscarJogo(jogos, Console.ReadLine()))
                    {
                        Console.WriteLine("Jogo não encontrado.");
                    }
                    break;
                case 3:
                    mostrarJogos(jogos);
                    break;
                case 4:
                    Console.Write("Console: ");
                    jogosPorConsole(jogos, Console.ReadLine());
                    break;
                case 5:
                    Console.Write("Título do jogo: ");
                    realizarEmprestimo(jogos, Console.ReadLine());
                    break;
                case 6:
                    Console.Write("Título do jogo: ");
                    devolverJogo(jogos, Console.ReadLine());
                    break;
                case 7:
                    mostrarEmprestados(jogos);
                    break;
                case 0:
                    Console.WriteLine("Saindo...");
                    break;
            }
            if (opcao != 0)
            {
                Console.WriteLine("Pressione uma tecla...");
                Console.ReadKey();
                Console.Clear();
            }
        } while (opcao != 0);
    }
}