using System;
using System.Collections.Generic;
using Biblioteca;

class Program
{
    static void cadastrarLivro(List<Livro> livros)
    {
        Livro novoLivro = new Livro();

        Console.WriteLine("*** Cadastro de Livro ***");

        Console.Write("Título: ");
        novoLivro.titulo = Console.ReadLine();

        Console.Write("Autor: ");
        novoLivro.autor = Console.ReadLine();

        Console.Write("Ano: ");
        novoLivro.ano = int.Parse(Console.ReadLine());

        Console.Write("Prateleira: ");
        novoLivro.prateleira = Console.ReadLine();

        livros.Add(novoLivro);
    }

    static void mostrarLivros(List<Livro> livros)
    {
        if (livros.Count == 0)
        {
            Console.WriteLine("Nenhum livro cadastrado.");
            return;
        }

        foreach (Livro livro in livros)
        {
            mostrarUmLivro(livro);
        }
    }

    static void mostrarUmLivro(Livro livro)
    {
        Console.WriteLine($"Título: {livro.titulo}");
        Console.WriteLine($"Autor: {livro.autor}");
        Console.WriteLine($"Ano: {livro.ano}");
        Console.WriteLine($"Prateleira: {livro.prateleira}");
        Console.WriteLine("----------");
    }

    static bool buscarLivro(List<Livro> livros, string tituloBusca)
    {
        bool encontrou = false;

        foreach (Livro livro in livros)
        {
            if (livro.titulo.ToUpper().Contains(tituloBusca.ToUpper()))
            {
                Console.WriteLine($"Título: {livro.titulo}");
                Console.WriteLine($"Prateleira: {livro.prateleira}");

                encontrou = true;
            }
        }

        return encontrou;
    }

    static void livrosMaisNovos(List<Livro> livros, int ano)
    {
        bool encontrou = false;

        foreach (Livro livro in livros)
        {
            if (livro.ano > ano)
            {
                mostrarUmLivro(livro);
                encontrou = true;
            }
        }

        if (!encontrou)
        {
            Console.WriteLine("Nenhum livro mais novo que o ano informado.");
        }
    }

    static int menu()
    {
        Console.WriteLine("=== BIBLIOTECA ===");
        Console.WriteLine("1 - Cadastrar livro");
        Console.WriteLine("2 - Buscar livro por título");
        Console.WriteLine("3 - Mostrar todos os livros");
        Console.WriteLine("4 - Listar livros mais novos que um ano");
        Console.WriteLine("0 - Sair");

        Console.Write("Escolha: ");
        return int.Parse(Console.ReadLine());
    }

    static void Main()
    {
        List<Livro> livros = new List<Livro>();
        int opcao;
        do
        {
            opcao = menu();
            switch (opcao)
            {
                case 1:
                    cadastrarLivro(livros);
                    break;
                case 2:
                    Console.Write("Título para buscar: ");
                    if (!buscarLivro(livros, Console.ReadLine()))
                    {
                        Console.WriteLine("Livro não encontrado.");
                    }
                    break;
                case 3:
                    mostrarLivros(livros);
                    break;
                case 4:
                    Console.Write("Ano de referência: ");
                    livrosMaisNovos(livros, int.Parse(Console.ReadLine()));
                    break;
                case 0:
                    Console.WriteLine("Saindo...");
                    break;
            }

            if (opcao != 0)
            {
                Console.WriteLine("Pressione uma tecla para sair..");
                Console.ReadKey();
                Console.Clear();
            }
        } while (opcao != 0);
    }
}
