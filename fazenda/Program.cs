using System;
using System.Collections.Generic;
using System.IO;
using FazendaGado;

class Program
{
    static void cadastrarGado(List<Gado> animais)
    {
        Gado novoAnimal = new Gado();

        Console.WriteLine("*** Dados do animal ***");

        Console.Write("Código: ");
        novoAnimal.codigo = int.Parse(Console.ReadLine());

        Console.Write("Leite produzido por semana (litros): ");
        novoAnimal.leite = double.Parse(Console.ReadLine());

        Console.Write("Alimento consumido por semana (kg): ");
        novoAnimal.alim = double.Parse(Console.ReadLine());

        Console.Write("Mês de nascimento: ");
        novoAnimal.nasc.mes = int.Parse(Console.ReadLine());

        Console.Write("Ano de nascimento: ");
        novoAnimal.nasc.ano = int.Parse(Console.ReadLine());

        novoAnimal.abate = 'N';
        animais.Add(novoAnimal);
    }

    static void mostrarAnimal(Gado animal)
    {
        Console.WriteLine($"Código: {animal.codigo}");
        Console.WriteLine($"Leite/semana: {animal.leite:F2} litros");
        Console.WriteLine($"Alimento/semana: {animal.alim:F2} kg");
        Console.WriteLine($"Nascimento: {animal.nasc.mes:D2}/{animal.nasc.ano}");
        Console.WriteLine($"Abate: {animal.abate}");
        Console.WriteLine("----------");
    }

    static void mostrarAnimais(List<Gado> animais)
    {
        if (animais.Count == 0)
        {
            Console.WriteLine("Nenhum animal cadastrado.");
        }

        foreach (Gado animal in animais)
        {
            mostrarAnimal(animal);
        }
    }

    static int calcularIdade(Gado animal)
    {
        int idade = DateTime.Now.Year - animal.nasc.ano;

        if (DateTime.Now.Month < animal.nasc.mes)
        {
            idade--;
        }

        return idade;
    }

    static void definirAbate(List<Gado> animais)
    {
        foreach (Gado animal in animais)
        {
            if (calcularIdade(animal) > 5 || animal.leite < 40)
            {
                animal.abate = 'S';
            }
            else
            {
                animal.abate = 'N';
            }
        }

        Console.WriteLine("Situação de abate atualizada.");
    }

    static double totalLeite(List<Gado> animais)
    {
        double total = 0;

        foreach (Gado animal in animais)
        {
            total += animal.leite;
        }

        return total;
    }

    static double totalAlimento(List<Gado> animais)
    {
        double total = 0;

        foreach (Gado animal in animais)
        {
            total += animal.alim;
        }

        return total;
    }

    static void listarAbate(List<Gado> animais)
    {
        bool encontrou = false;

        foreach (Gado animal in animais)
        {
            if (animal.abate == 'S')
            {
                mostrarAnimal(animal);
                encontrou = true;
            }
        }

        if (!encontrou)
        {
            Console.WriteLine("Nenhum animal marcado para abate. Atualize a situação pelo menu.");
        }
    }

    static void salvarDados(List<Gado> animais, string arquivo)
    {
        StreamWriter writer = new StreamWriter(arquivo);

        foreach (Gado animal in animais)
        {
            writer.WriteLine($"{animal.codigo};{animal.leite};{animal.alim};{animal.nasc.mes};{animal.nasc.ano};{animal.abate}");
        }

        writer.Dispose();

        Console.WriteLine("Dados salvos.");
    }

    static void carregarDados(List<Gado> animais, string arquivo)
    {
        if (!File.Exists(arquivo))
        {
            Console.WriteLine("Arquivo não encontrado.");
            return;
        }

        animais.Clear();

        string[] linhas = File.ReadAllLines(arquivo);

        foreach (string linha in linhas)
        {
            string[] campos = linha.Split(';');

            if (campos.Length == 6)
            {
                Gado animal = new Gado();

                animal.codigo = int.Parse(campos[0]);
                animal.leite = double.Parse(campos[1]);
                animal.alim = double.Parse(campos[2]);
                animal.nasc.mes = int.Parse(campos[3]);
                animal.nasc.ano = int.Parse(campos[4]);
                animal.abate = char.Parse(campos[5]);

                animais.Add(animal);
            }
        }

        Console.WriteLine("Dados carregados.");
    }

    static int menu()
    {
        Console.WriteLine("=== CONTROLE DA FAZENDA ===");
        Console.WriteLine("1 - Cadastrar animal");
        Console.WriteLine("2 - Listar animais");
        Console.WriteLine("3 - Atualizar indicação de abate");
        Console.WriteLine("4 - Total de leite por semana");
        Console.WriteLine("5 - Total de alimento por semana");
        Console.WriteLine("6 - Listar animais para abate");
        Console.WriteLine("7 - Salvar dados");
        Console.WriteLine("8 - Carregar dados");
        Console.WriteLine("0 - Sair");

        Console.Write("Escolha: ");
        return int.Parse(Console.ReadLine());
    }

    static void Main()
    {
        List<Gado> animais = new List<Gado>();
        int opcao;

        do
        {
            opcao = menu();

            switch (opcao)
            {
                case 1:
                    cadastrarGado(animais);
                    break;

                case 2:
                    mostrarAnimais(animais);
                    break;

                case 3:
                    definirAbate(animais);
                    break;

                case 4:
                    Console.WriteLine($"Total de leite por semana: {totalLeite(animais):F2} litros");
                    break;

                case 5:
                    Console.WriteLine($"Total de alimento por semana: {totalAlimento(animais):F2} kg");
                    break;

                case 6:
                    listarAbate(animais);
                    break;

                case 7:
                    salvarDados(animais, "gado.txt");
                    break;

                case 8:
                    carregarDados(animais, "gado.txt");
                    break;

                case 0:
                    salvarDados(animais, "gado.txt");
                    Console.WriteLine("Saindo...");
                    break;
            }

            if (opcao != 0)
            {
                Console.WriteLine("Pressione uma tecla. ");
                Console.ReadKey();
                Console.Clear();
            }

        } while (opcao != 0);
    }
}
